using RNE.Template.Node.Pointer;
using RNE.Template.Node.Pointer.Value;
using Utils.Colors.Coloring;
using Utils.IO.Serialization;
using UnityEngine;
using TMPro;

namespace RNE.Template.Node
{
    public class ToGrayscaleNode : NodeWithPreview
    {
        [Space]

        [SerializeField]
        private TMP_Dropdown _dropdown;

        [SerializeField]
        private ComputeShader _shader;

        protected override void CodeToExecute()
        {
            if (!Inputs[0].ConnectedOutputPointer)
            {
                return;
            }

            ExecuteInputConnection(0);

            ComputeBuffer colorsBuffer = PointerValue.GetTexture(Inputs[0]);
            ComputeBuffer valuesBuffer = new ComputeBuffer(ProjectData.Length, sizeof(float));

            int kernel = 0;
            if (_dropdown.value == 0)
            {
                kernel = _shader.FindKernel("Average");
            }
            else if (_dropdown.value == 1)
            {
                kernel = _shader.FindKernel("Luminosity");
            }
            else if (_dropdown.value == 2)
            {
                kernel = _shader.FindKernel("Red");
            }
            else if (_dropdown.value == 3)
            {
                kernel = _shader.FindKernel("Green");
            }
            else if (_dropdown.value == 4)
            {
                kernel = _shader.FindKernel("Blue");
            }
            else if (_dropdown.value == 5)
            {
                kernel = _shader.FindKernel("Lowest");
            }
            else if (_dropdown.value == 6)
            {
                kernel = _shader.FindKernel("Highest");
            }

            _shader.SetBuffer(kernel, "colors", colorsBuffer);
            _shader.SetBuffer(kernel, "values", valuesBuffer);

            _shader.Dispatch(kernel, Mathf.CeilToInt(colorsBuffer.count / 1024.0f), 1, 1);

            ComputeBuffer previewBuffer = new ComputeBuffer(ProjectData.Length, sizeof(float) * 4);
            Coloring.ColoringGPU(ref previewBuffer, valuesBuffer, Color.white);
            SetPreview(previewBuffer);
            previewBuffer.Release();

            Outputs[0].GetComponent<NoiseOutputPointer>().Buffer = valuesBuffer;
        }

        protected override void CodeToReset()
        {
            Outputs[0].GetComponent<NoiseOutputPointer>().Reset();
        }

        public override void OnSave(FileWriter writer)
        {
            writer.Write(_dropdown.value);
        }

        public override void OnLoad(FileReader reader)
        {
            _dropdown.value = reader.ReadInt();
        }
    }
}
