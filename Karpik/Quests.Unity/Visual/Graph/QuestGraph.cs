using System;
using System.Collections.Generic;
using System.Linq;
using Karpik.UIExtension;
using UnityEngine;
using UnityEngine.UIElements;

namespace Karpik.Quests.Unity
{
    [UxmlElement]
    public partial class QuestGraph : Karpik.UIExtension.Graph
    {
        public IGraph Graph { get; private set; }

        private IQuestNode _startLinkNode;
        
        public QuestGraph()
        {
            Graph = Quests.Instance.MainGraph;
            
            Modal.BackgroundColor = new Color(0, 0, 0, 0.3f);
            Modal.PushContext(this);
        }

        public void Init(IGraph graph)
        {
            Graph = graph ?? new Graph();
        }
        
        public QuestGraphData GetData()
        {
            var nodes = new List<IQuestNode>();
            foreach (var node in Nodes)
            {
                if (node is not IQuestNode questNode)
                {
                    continue;
                }

                nodes.Add(questNode);
            }

            return new QuestGraphData(nodes.Select(x => new QuestNodeData(x)).ToList());
        }

        public override void Save()
        {
            base.Save();
            Quests.Instance.QuestGraphData = GetData();
            Quests.Instance.Save();
        }
        
        public override void Load()
        {
            base.Load();
            Quests.Instance.Reload();
            
            Clear();
            Graph = Quests.Instance.MainGraph;
            
            LoadFromData();
        }

        private void Reset()
        {
            Graph.Setup();
            Save();
        }

        private void ClearQuests()
        {
            Graph.Clear();
            Clear();
        }

        private void LoadFromData()
        {
            int i = 0;
            var quests = Graph.Quests;
            foreach (var node in Quests.Instance.QuestGraphData.Nodes)
            {
                if (string.IsNullOrWhiteSpace(node.QuestId.Value))
                {
                    node.QuestId = quests.ElementAt(i).Id;
                }
                var questNode = new QuestNode(node);
                AddNode(questNode);
                i++;
            }

            foreach (var quest in quests)
            {
                var dependencies = Graph.GetDependenciesQuests(quest);
                foreach (var dependency in dependencies)
                {
                    var first = Nodes.First(x =>
                    {
                        if (x is IQuestNode node)
                        {
                            return node.QuestId == dependency.DependentQuest.Id;
                        }

                        return false;
                    });
                    
                    var second = Nodes.First(x =>
                    {
                        if (x is IQuestNode node)
                        {
                            return node.QuestId == dependency.DependencyQuest.Id;
                        }

                        return false;
                    });
                    
                    AddLine(first as IQuestNode, second as IQuestNode);
                }
            }
        }
        
        protected override void AddButtons(TopMenu menu)
        {
            menu.AddButton("Save", UserSave);
            menu.AddButton("Load", UserLoad);
            menu.AddButton("Reset", UserReset);
            menu.AddButton("Clear", UserClearQuests);
        }
        
        public override void AddNode<T>(T node)
        {
            if (node is not IQuestNode questNode)
            {
                throw new ArgumentException("Node must be IQuestNode", nameof(node));
            }
            
            base.AddNode(node);
            
            node.EnableContextMenu = true;
            node.AddContextMenu("Open", e => OnNodeOpening(questNode));
        }

        public override void RemoveNode(IGraphNode node)
        {
            if (node is IQuestNode questNode)
            {
                Graph.TryRemove(questNode.QuestId);
            }
            
            base.RemoveNode(node);
        }

        protected override void OnNodeRemoved(IGraphNode node)
        {
            base.OnNodeRemoved(node);
            if (node is not IQuestNode questNode) return;
            
            var dependencies = Graph.GetDependenciesQuests(questNode.QuestId);
            foreach (var dependency in dependencies)
            {
                TryRemoveLink(dependency.DependencyQuest.Id, dependency.DependentQuest.Id);
            }
                
            var dependents = Graph.GetDependentsQuests(questNode.QuestId);
            foreach (var dependent in dependents)
            {
                TryRemoveLink(dependent.DependencyQuest.Id, dependent.DependentQuest.Id);
            }
        }

        protected override void RemoveLine(Line line)
        {
            if (line.StartElement is IQuestNode first &&
                line.EndElement is IQuestNode second)
            {
                Graph.TryRemoveDependency(first.QuestId, second.QuestId);
            }
            
            base.RemoveLine(line);
        }

        protected override void EditClicked()
        {
            base.EditClicked();

            foreach (var line in Lines)
            {
                line.EnableContextMenu = EnableContextMenu;
            }
        }

        protected override void OnLineAdded(Line line)
        {
            base.OnLineAdded(line);
            if (line.StartElement is not IQuestNode first ||
                line.EndElement is not IQuestNode second)
            {
                return;
            }
            
            if (Graph.GetDependenciesQuests(first.QuestId).Count(x => x.DependencyQuest.Id == second.QuestId) == 0)
            {
                Graph.TryAddDependency(first.QuestId, second.QuestId, DependencyType.Completion);
            }
            
            if (Graph.IsCyclic())
            {
                RemoveLine(line);
                return;
            }

            line.AddContextMenu("Open Dependency", (e) =>
            {
                var d = new DependencyElement();
                d.Init(Graph.GetDependenciesQuests(first.QuestId)
                    .FirstOrDefault(x =>
                        x.DependencyQuest.Id == second.QuestId),
                    Graph);
                Modal.Start<ModalWindow>()
                    .Add(d)
                    .OnClose(OnModalClosed)
                    .Show();
            }, () => EnableContextMenu);
        }

        protected void OpenQuestMenu<T>(Id id) where T : QuestMenu, new()
        {
            var menu = Modal
                .Start<T>(this, "Quest")
                .OnClose(OnModalClosed)
                .Show();
            menu.InitQuest(Graph.GetQuest(id), EnableContextMenu);
            MenuOpened(menu);
        }

        protected bool TryRemoveLink(Id dependencyQuest, Id dependentQuest)
        {
            
            var line = Lines
                .Where(x => x.StartElement is IQuestNode)
                .Where(x => x.EndElement is IQuestNode)
                .FirstOrDefault(x => ((IQuestNode)x.StartElement).QuestId == dependentQuest
                                     && ((IQuestNode)x.EndElement).QuestId == dependencyQuest);
            if (line is null)
            {
                return false;
            }
            
            RemoveLine(line);

            return true;
        }

        protected virtual void MenuOpened(QuestMenu menu)
        {
            
        }

        protected virtual void OnNodeOpening(IQuestNode node)
        {
            OpenQuestMenu<QuestMenu>(node.QuestId);
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            Modal.PopContext();
        }

        private void OnModalClosed()
        {
            Save();
        }

        private void UserSave()
        {
            if (IsEditMode) Save();
        }
        
        private void UserLoad()
        {
            if (IsEditMode) Load();
        }
        
        private void UserReset()
        {
            if (IsEditMode) Reset();
        }
        
        private void UserClearQuests()
        {
            if (IsEditMode) ClearQuests();
        }
    }

    [Serializable]
    public class QuestGraphData
    {
        public List<QuestNodeData> Nodes { get; private set; }

        public QuestGraphData(List<QuestNodeData> nodes)
        {
            Nodes = nodes;
        }
    }
}
