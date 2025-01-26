using RNE.Template.Node.Pointer.Value;
using UnityEngine;

namespace RNE.Template.Node.Pointer.ProjectData
{
    public static class PointerColor
    {
        public static Color PickColor(ValueType valueType)
        {
            return valueType switch
            {
                ValueType.Int => Colors.Int,
                ValueType.Float => Colors.Float,
                ValueType.Vector2 => Colors.Vector2,
                ValueType.Vector3 => Colors.Vector3,
                ValueType.Char => Colors.Char,
                ValueType.String => Colors.String,
                ValueType.Bool => Colors.Bool,
                ValueType.Color => Colors.Color,
                _ => Colors.Null
            };
        }
    }
}
