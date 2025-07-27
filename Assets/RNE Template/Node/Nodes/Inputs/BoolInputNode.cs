using RNE.Template.Node.Pointer;
using UnityEngine;
using UnityEngine.UI;
using Utils.IO.Serialization;

namespace RNE.Template.Node
{
    public class BoolInputNode : RuntimeNodeEditor.Node.Node
    {
        [SerializeField]
        private Toggle _toggle;
        
        protected override void CodeToExecute()
        {
            Outputs[0].GetComponent<BoolOutputPointer>().Value = _toggle.isOn;
        }

        protected override void CodeToReset()
        {
            Outputs[0].GetComponent<BoolOutputPointer>().Reset();
        }

        public override void OnSave(FileWriter writer)
        {
            writer.Write(_toggle.isOn);
        }

        public override void OnLoad(FileReader reader)
        {
            _toggle.isOn = reader.ReadBool();
        }
    }
}
