using RuntimeNodeEditor.Node.UI.Functions;
using RNE.Template.Node.Pointer;
using UnityEngine;
using TMPro;
using Utils.IO.Serialization;

namespace RNE.Template.Node
{
    public class Vector3InputNode : RuntimeNodeEditor.Node.Node
    {
        [SerializeField] private TMP_InputField _inputfieldX;
        [SerializeField] private TMP_InputField _inputfieldY;
        [SerializeField] private TMP_InputField _inputfieldZ;
        
        protected override void CodeToExecute()
        {
            Outputs[0].GetComponent<Vector3OutputPointer>().Value = Vector3.zero;

            if (_inputfieldX.text.Length != 0)
            {
                float x = InputFieldToFloat.Get(_inputfieldX.text);

                Outputs[1].GetComponent<FloatOutputPointer>().Value = x;
                Outputs[0].GetComponent<Vector3OutputPointer>().Value.x = x;
            }

            if (_inputfieldY.text.Length != 0)
            {
                float y = InputFieldToFloat.Get(_inputfieldY.text);

                Outputs[2].GetComponent<FloatOutputPointer>().Value = y;
                Outputs[0].GetComponent<Vector3OutputPointer>().Value.y = y;
            }

            if (_inputfieldZ.text.Length != 0)
            {
                float z = InputFieldToFloat.Get(_inputfieldZ.text);

                Outputs[3].GetComponent<FloatOutputPointer>().Value = z;
                Outputs[0].GetComponent<Vector3OutputPointer>().Value.z = z;
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
