using RNE.Template.Node.Pointer;
using RuntimeNodeEditor.Node.UI.Functions;
using TMPro;
using UnityEngine;
using Utils.IO.Serialization;

namespace RNE.Template.Node
{
    public class FloatInputNode : RuntimeNodeEditor.Node.Node
    {
        [SerializeField] 
        private TMP_InputField _inputfield;
        
        protected override void CodeToExecute()
        {
            Outputs[0].GetComponent<FloatOutputPointer>().Value = _inputfield.text.Length != 0 ? InputFieldToFloat.Get(_inputfield.text) : 0.0f;
        }

        protected override void CodeToReset()
        {
            Outputs[0].GetComponent<FloatOutputPointer>().Reset();
        }

        public override void OnSave(FileWriter writer)
        {
            writer.Write(_inputfield.text);
        }

        public override void OnLoad(FileReader reader)
        {
            _inputfield.text = reader.ReadString();
        }
    }
}
