#if UNITY_EDITOR
using UnityEditor.UIElements;

namespace Karpik.Quests
{
    public class UxmlConverterId : UxmlAttributeConverter<Id>
    {
        public override Id FromString(string value)
        {
            return new Id(value);
        }

        public override string ToString(Id value)
        {
            return value.ToString();
        }
    }
}
#endif