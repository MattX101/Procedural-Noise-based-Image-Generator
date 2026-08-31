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

        private int _shaderKernel = 0;
        private ComputeBuffer _valuesBuffer;
        private ComputeBuffer _previewBuffer;

        protected override void Init()
        {
            _valuesBuffer = new ComputeBuffer(ProjectData.Length, sizeof(float));
            _previewBuffer = new ComputeBuffer(ProjectData.Length, sizeof(float) * 4);
        }

        protected override void CodeToExecute()
        {
            if (!Inputs[0].ConnectedOutputPointer)
            {
                return;
            }

            ExecuteInputConnection(0);
            
            Init();

            _shader.SetBuffer(_shaderKernel, "colors", PointerValue.GetTexture(Inputs[0]));
            _shader.SetBuffer(_shaderKernel, "values", _valuesBuffer);
            _shader.Dispatch(_shaderKernel, Mathf.CeilToInt(ProjectData.Length / 1024.0f), 1, 1);

            Coloring.Color(ref _previewBuffer, _valuesBuffer, Color.white);
            SetPreview(_previewBuffer);

            Outputs[0].GetComponent<NoiseOutputPointer>().Buffer = _valuesBuffer;
        }

        public void SetKernel()
        {
            _shaderKernel = _dropdown.value switch
            {
                0 => _shader.FindKernel("Average"),
                1 => _shader.FindKernel("Luminosity"),
                2 => _shader.FindKernel("Red"),
                3 => _shader.FindKernel("Green"),
                4 => _shader.FindKernel("Blue"),
                5 => _shader.FindKernel("Lowest"),
                6 => _shader.FindKernel("Highest"),
                _ => _shader.FindKernel("Average"),
            };
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

        void OnDestroy()
        {
            _valuesBuffer.Release();
            _previewBuffer.Release();
        }
    }
}
