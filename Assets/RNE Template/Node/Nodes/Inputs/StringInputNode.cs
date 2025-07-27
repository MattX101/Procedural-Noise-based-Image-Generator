using RNE.Template.Node.Pointer;
using TMPro;
using UnityEngine;
using Utils.IO.Serialization;

namespace RNE.Template.Node
{
    public class StringInputNode : RuntimeNodeEditor.Node.Node
    {
        [SerializeField] 
        private TMP_InputField _inputfield;
        
        protected override void CodeToExecute()
        {
            Outputs[0].GetComponent<StringOutputPointer>().Value = _inputfield.text.Length != 0 ? _inputfield.text : "";
        }

        protected override void CodeToReset()
        {
            Outputs[0].GetComponent<StringOutputPointer>().Reset();
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
