using RNE.Template.Node.Pointer;
using RNE.Template.Node.Pointer.Value;
using Utils.IO.Serialization;
using UnityEngine;
using TMPro;

namespace RNE.Template.Node
{
    public class CosineNode : RuntimeNodeEditor.Node.Node
    {
        [SerializeField] private TMP_InputField _valueInputField;
        [SerializeField] private TMP_InputField _cosineInputField;

        private float _value, _cosine;

        protected override void CodeToExecute()
        {
            ExecuteInputConnection(0);

            _value = 1.0f;
            if (Inputs[0].ConnectedOutputPointer)
            {
                PointerValue.GetFloat(Inputs[0], ref _value);
            }
            else if (_valueInputField.text.Length != 0)
            {
                _value = float.Parse(_valueInputField.text);
            }
            
            _cosine = Mathf.Cos(_value);

            Outputs[0].GetComponent<FloatOutputPointer>().Value = _cosine;
            
            _valueInputField.text = _value.ToString();
            _cosineInputField.text = _cosine.ToString();
        }
        
        protected override void CodeToReset()
        {
            Outputs[0].GetComponent<FloatOutputPointer>().Reset();
        }

        public override void OnSave(FileWriter writer)
        {
            writer.Write(_valueInputField.text);
        }

        public override void OnLoad(FileReader reader)
        {
            _valueInputField.text = reader.ReadString();
        }
    }
}
