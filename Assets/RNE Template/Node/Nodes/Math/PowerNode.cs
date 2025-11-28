using RuntimeNodeEditor.Node.UI.Functions;
using RNE.Template.Node.Pointer;
using RNE.Template.Node.Pointer.Value;
using Utils.IO.Serialization;
using UnityEngine;
using TMPro;

namespace RNE.Template.Node
{
    public class PowerNode : RuntimeNodeEditor.Node.Node
    {
        [SerializeField] private TMP_InputField _valueInputField;
        [SerializeField] private TMP_InputField _powerInputField;

        [SerializeField] private TMP_InputField _outputInputField;

        protected override void CodeToExecute()
        {
            ExecuteInputConnection(0);
            ExecuteInputConnection(1);

            float value = 1.0f;
            float power = 2.0f;

            // Value
            if (Inputs[0].ConnectedOutputPointer)
            {
                PointerValue.GetFloat(Inputs[0], ref value);
            }
            else
            {
                if (_valueInputField.text.Length != 0)
                {
                    value = float.Parse(_valueInputField.text);
                }
            }

            // Power
            if (Inputs[1].ConnectedOutputPointer)
            {
                PointerValue.GetFloat(Inputs[1], ref power);
            }
            else
            {
                if (_powerInputField.text.Length != 0)
                {
                    power = float.Parse(_powerInputField.text);
                }
            }

            float output = Mathf.Pow(value, power);

            Outputs[0].GetComponent<FloatOutputPointer>().Value = output;

            _valueInputField.text = value.ToString();
            _powerInputField.text = power.ToString();
                
            _outputInputField.text = output.ToString();
        }

        protected override void CodeToReset()
        {
            Outputs[0].GetComponent<FloatOutputPointer>().Reset();
        }

        public override void OnSave(FileWriter writer)
        {
            writer.Write(_valueInputField.text);
            writer.Write(_powerInputField.text);
        }

        public override void OnLoad(FileReader reader)
        {
            _valueInputField.text = reader.ReadString();
            _powerInputField.text = reader.ReadString();
        }
    }
}
