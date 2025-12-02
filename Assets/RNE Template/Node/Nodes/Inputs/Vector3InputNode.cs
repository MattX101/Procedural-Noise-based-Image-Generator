using RuntimeNodeEditor.Node.UI.Functions;
using RNE.Template.Node.Pointer;
using RNE.Template.Node.Pointer.Value;
using Utils.IO.Serialization;
using UnityEngine;
using TMPro;

namespace RNE.Template.Node
{
    public class Vector3InputNode : RuntimeNodeEditor.Node.Node
    {
        [SerializeField] private TMP_InputField _inputfieldX;
        [SerializeField] private TMP_InputField _inputfieldY;
        [SerializeField] private TMP_InputField _inputfieldZ;

        private float _x, _y, _z;
        
        protected override void CodeToExecute()
        {
            Outputs[0].GetComponent<Vector3OutputPointer>().Value = Vector3.zero;

            if (Inputs[0].ConnectedOutputPointer != null)
            {
                ExecuteInputConnection(0);
                _x = PointerValue.GetFloat(Inputs[0]);
            
                _inputfieldX.text = _x.ToString();

                Outputs[1].GetComponent<FloatOutputPointer>().Value = _x;
                Outputs[0].GetComponent<Vector3OutputPointer>().Value.x = _x;
            }
            else if (_inputfieldX.text.Length != 0)
            {
                _x = InputFieldToFloat.Get(_inputfieldX.text);

                Outputs[1].GetComponent<FloatOutputPointer>().Value = _x;
                Outputs[0].GetComponent<Vector3OutputPointer>().Value.x = _x;
            }
            else
            {
                _inputfieldX.text = "0";
            }

            if (Inputs[1].ConnectedOutputPointer != null)
            {
                ExecuteInputConnection(1);
                _y = PointerValue.GetFloat(Inputs[1]);
            
                _inputfieldY.text = _y.ToString();

                Outputs[2].GetComponent<FloatOutputPointer>().Value = _y;
                Outputs[0].GetComponent<Vector3OutputPointer>().Value.y = _y;
            }
            else if (_inputfieldY.text.Length != 0)
            {
                _y = InputFieldToFloat.Get(_inputfieldY.text);

                Outputs[2].GetComponent<FloatOutputPointer>().Value = _y;
                Outputs[0].GetComponent<Vector3OutputPointer>().Value.y = _y;
            }
            else
            {
                _inputfieldY.text = "0";
            }

            if (Inputs[2].ConnectedOutputPointer != null)
            {
                ExecuteInputConnection(2);
                _z = PointerValue.GetFloat(Inputs[2]);
            
                _inputfieldZ.text = _z.ToString();

                Outputs[3].GetComponent<FloatOutputPointer>().Value = _z;
                Outputs[0].GetComponent<Vector3OutputPointer>().Value.z = _z;
            }
            else if (_inputfieldZ.text.Length != 0)
            {
                _z = InputFieldToFloat.Get(_inputfieldZ.text);

                Outputs[3].GetComponent<FloatOutputPointer>().Value = _z;
                Outputs[0].GetComponent<Vector3OutputPointer>().Value.z = _z;
            }
            else
            {
                _inputfieldZ.text = "0";
            }
        }

        protected override void CodeToReset()
        {
            Outputs[0].GetComponent<Vector3OutputPointer>().Reset();
        }

        public override void OnSave(FileWriter writer)
        {
            writer.Write(_inputfieldX.text);
            writer.Write(_inputfieldY.text);
            writer.Write(_inputfieldZ.text);
        }

        public override void OnLoad(FileReader reader)
        {
            _inputfieldX.text = reader.ReadString();
            _inputfieldY.text = reader.ReadString();
            _inputfieldZ.text = reader.ReadString();
        }
    }
}
