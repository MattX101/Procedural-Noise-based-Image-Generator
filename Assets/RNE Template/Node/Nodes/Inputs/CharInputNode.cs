using RNE.Template.Node.Pointer;
using TMPro;
using UnityEngine;
using Utils.IO.Serialization;

namespace RNE.Template.Node
{
    public class CharInputNode : RuntimeNodeEditor.Node.Node
    {
        [SerializeField] 
        private TMP_InputField _inputfield;
        
        protected override void CodeToExecute()
        {
            Outputs[0].GetComponent<CharOutputPointer>().Value = _inputfield.text.Length != 0 ? _inputfield.text[0] : ' ';
        }

        protected override void CodeToReset()
        {
            Outputs[0].GetComponent<CharOutputPointer>().Reset();
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
