using RNE.Template.Node.Pointer;
using RNE.Template.UI;
using UnityEngine;
using Utils.IO.Serialization;

namespace RNE.Template.Node
{
    public class ColorGradientNode : RuntimeNodeEditor.Node.Node
    {
        [SerializeField]
        private UIColorGradient _uiColorGradient;

        protected override void CodeToExecute()
        {
            Outputs[0].GetComponent<ColorGradientOutputPointer>().Value = _uiColorGradient.Gradient;
        }

        protected override void CodeToReset()
        {
            Outputs[0].GetComponent<ColorGradientOutputPointer>().Reset();
        }

        public override void OnSave(FileWriter writer)
        {
            _uiColorGradient.OnSave(writer);
        }

        public override void OnLoad(FileReader reader)
        {
            _uiColorGradient.OnLoad(reader);
        }
    }
}
