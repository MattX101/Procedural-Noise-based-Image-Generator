using RuntimeNodeEditor.Node.UI.Functions;
using RNE.Template.Node.Pointer;
using RNE.Template.Node.Pointer.Value;
using Utils.IO.Serialization;
using UnityEngine;
using TMPro;

namespace RNE.Template.Node
{
    public class Vector2InputNode : RuntimeNodeEditor.Node.Node
    {
        [SerializeField] private TMP_InputField _inputfieldX;
        [SerializeField] private TMP_InputField _inputfieldY;
        
        protected override void CodeToExecute()
        {
            Outputs[0].GetComponent<Vector2OutputPointer>().Value = Vector2.zero;

            if (Inputs[0].ConnectedOutputPointer != null)
            {
                ExecuteInputConnection(0);
                float x = PointerValue.GetFloat(Inputs[0]);
            
                _inputfieldX.text = x.ToString();

                Outputs[1].GetComponent<FloatOutputPointer>().Value = x;
                Outputs[0].GetComponent<Vector3OutputPointer>().Value.x = x;
            }
            else if (_inputfieldX.text.Length != 0)
            {
                float x = InputFieldToFloat.Get(_inputfieldX.text);

                Outputs[1].GetComponent<FloatOutputPointer>().Value = x;
                Outputs[0].GetComponent<Vector2OutputPointer>().Value.x = x;
            }
            else
            {
                _inputfieldX.text = "0";
            }

            if (Inputs[1].ConnectedOutputPointer != null)
            {
                ExecuteInputConnection(1);
                float y = PointerValue.GetFloat(Inputs[1]);
            
                _inputfieldY.text = y.ToString();

                Outputs[2].GetComponent<FloatOutputPointer>().Value = y;
                Outputs[0].GetComponent<Vector3OutputPointer>().Value.y = y;
            }
            else if (_inputfieldY.text.Length != 0)
            {
                float y = InputFieldToFloat.Get(_inputfieldY.text);

                Outputs[2].GetComponent<FloatOutputPointer>().Value = y;
                Outputs[0].GetComponent<Vector2OutputPointer>().Value.y = y;
            }
            else
            {
                _inputfieldY.text = "0";
            }
        }

        protected override void CodeToReset()
        {
            Outputs[0].GetComponent<Vector2OutputPointer>().Reset();
        }

        public override void OnSave(FileWriter writer)
        {
            writer.Write(_inputfieldX.text);
            writer.Write(_inputfieldY.text);
        }

        public override void OnLoad(FileReader reader)
        {
            _inputfieldX.text = reader.ReadString();
            _inputfieldY.text = reader.ReadString();
        }
    }
}
