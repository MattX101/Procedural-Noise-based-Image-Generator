using RuntimeNodeEditor.Node.UI.Functions;
using RNE.Template.Node.Pointer;
using UnityEngine;
using TMPro;

namespace RNE.Template.Node
{
    public class Vector3InputNode : RuntimeNodeEditor.Node.Node
    {
        protected override void CodeToExecute()
        {
            Outputs[0].GetComponent<Vector3OutputPointer>().Value = Vector3.zero;

            TMP_InputField xField = Elements.inputFields[0];
            if (xField.text.Length != 0)
            {
                float x = InputFieldToFloat.Get(xField.text);

                Outputs[1].GetComponent<FloatOutputPointer>().Value = x;
                Outputs[0].GetComponent<Vector3OutputPointer>().Value.x = x;
            }

            TMP_InputField yField = Elements.inputFields[1];
            if (yField.text.Length != 0)
            {
                float y = InputFieldToFloat.Get(yField.text);

                Outputs[2].GetComponent<FloatOutputPointer>().Value = y;
                Outputs[0].GetComponent<Vector3OutputPointer>().Value.y = y;
            }

            TMP_InputField zField = Elements.inputFields[2];
            if (zField.text.Length != 0)
            {
                float z = InputFieldToFloat.Get(zField.text);

                Outputs[3].GetComponent<FloatOutputPointer>().Value = z;
                Outputs[0].GetComponent<Vector3OutputPointer>().Value.z = z;
            }
        }

        protected override void CodeToReset()
        {
            Outputs[0].GetComponent<Vector3OutputPointer>().Reset();
        }
    }
}
