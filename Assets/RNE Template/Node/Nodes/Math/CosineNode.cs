using RuntimeNodeEditor.Node.UI.Functions;
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

        protected override void CodeToExecute()
        {
            ExecuteInputConnection(0);

            float value = 1.0f;
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
            
            float cosine = Mathf.Cos(value);

            Outputs[0].GetComponent<FloatOutputPointer>().Value = cosine;
            
            _valueInputField.text = value.ToString();
            _cosineInputField.text = cosine.ToString();
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
