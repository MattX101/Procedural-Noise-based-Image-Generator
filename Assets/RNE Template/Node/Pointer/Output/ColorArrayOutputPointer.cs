using RNE.Template.Node.Pointer.Value;
using RuntimeNodeEditor.Node.Pointer;
using UnityEngine;

namespace RNE.Template.Node.Pointer
{
    public class ColorArrayOutputPointer : OutputPointer
    {
        public Color[] Values;

        private void Awake()
        {
            Values = new Color[128 * 128];

            ValueTypeIndex = (int)ValueType.ColorArray;
        }

        protected override void ResetPointer()
        {
            Color[] colors = new Color[128 * 128];
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = Color.white;
            }
        }

        protected override Color GetLineColor()
        {
            return Data.Colors.ColorArray;
        }
    }
}