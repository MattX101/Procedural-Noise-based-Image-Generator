using RNE.Template.Node.Pointer;
using RNE.Template.Node.Pointer.Value;
using Utils.IO.Serialization;
using UnityEngine;
using TMPro;

namespace RNE.Template.Node
{
    public class SquareRootNode : RuntimeNodeEditor.Node.Node
    {
        [SerializeField] private TMP_InputField _valueInputField;

        [SerializeField] private TMP_InputField _outputInputField;

        private float _value;
        private float _output;

        protected override void CodeToExecute()
        {
            ExecuteInputConnection(0);

            _value = 1.0f;

            // Value
            if (Inputs[0].ConnectedOutputPointer)
            {
                PointerValue.GetFloat(Inputs[0], ref _value);
            }
            else if (_valueInputField.text.Length != 0)
            {
                _value = float.Parse(_valueInputField.text);
            }

            _output = Mathf.Sqrt(_value);

            Outputs[0].GetComponent<FloatOutputPointer>().Value = _output;

            _valueInputField.text = _value.ToString();
            _outputInputField.text = _output.ToString();
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
