using RuntimeNodeEditor.Node.UI.Functions;
using RNE.Template.Node.Pointer;
using RNE.Template.Node.Pointer.Value;
using Utils.IO.Serialization;
using UnityEngine;
using TMPro;

namespace RNE.Template.Node
{
    public class MathNode : RuntimeNodeEditor.Node.Node
    {
        [SerializeField]
        private TMP_Dropdown _dropdown;

        [Space]

        [SerializeField] private TMP_InputField _aInputField;
        [SerializeField] private TMP_InputField _bInputField;

        [Space]

        [SerializeField] private TMP_InputField _outInputField;

        private float _a, _b;

        protected override void CodeToExecute()
        {
            ExecuteInputConnection(0);
            ExecuteInputConnection(1);

            _a = Inputs[0].ConnectedOutputPointer ? PointerValue.GetFloat(Inputs[0]) : InputFieldToFloat.Get(_aInputField.text);
            _b = Inputs[1].ConnectedOutputPointer ? PointerValue.GetFloat(Inputs[1]) : InputFieldToFloat.Get(_bInputField.text);

            float value = _dropdown.value switch
            {
                0 => _a + _b,
                1 => _a - _b,
                2 => _a * _b,
                3 => _a / _b,
                _ => _a + _b,
            };

            Outputs[0].GetComponent<FloatOutputPointer>().Value = value;

            _aInputField.text = _a.ToString();
            _bInputField.text = _b.ToString();
            _outInputField.text = value.ToString();
        }

        protected override void CodeToReset()
        {
            Outputs[0].GetComponent<FloatOutputPointer>().Reset();
        }

        public override void OnSave(FileWriter writer)
        {
            writer.Write(_dropdown.value);
            writer.Write(_aInputField.text);
            writer.Write(_bInputField.text);
        }

        public override void OnLoad(FileReader reader)
        {
            _dropdown.value = reader.ReadInt();
            _aInputField.text = reader.ReadString();
            _bInputField.text = reader.ReadString();
        }
    }
}
