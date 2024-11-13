using UnityEditor;
using UnityEngine;

namespace Karpik.Quests.Unity
{
    public class QuestGraphEditorWindow : EditorWindow
    {
        [MenuItem("Tools/Karpik/Quest Graph")]
        public static void ShowExample()
        {
            QuestGraphEditorWindow wnd = GetWindow<QuestGraphEditorWindow>();
            wnd.titleContent = new GUIContent("Quest Graph");
        }

        public void CreateGUI()
        {
            var graph = new DefaultQuestGraph();

            rootVisualElement.Add(graph);
            graph.Init(Quests.Instance.MainGraph);
            graph.Load();
        }
    }
}
