using RNE.Template.Node.Pointer;
using RNE.Template.Node.Pointer.Value;
using UnityEngine;
using UnityEngine.UI;
using Utils.IO.Serialization;

namespace RNE.Template.Node
{
    public class PosterizeNode : NodeWithPreview
    {
        [SerializeField] private Slider _posterizeSlider;
        
        [SerializeField]
        private ComputeShader _shader;

        protected override void CodeToExecute()
        {
            ExecuteInputConnection(0);

            ComputeBuffer buffer = PointerValue.GetTexture(Inputs[0]);

            if (buffer != null)
            {
                _shader.SetFloats("step", 1.0f / (_posterizeSlider.value - 1));

                int kernel = _shader.FindKernel("Posterize");
                _shader.SetBuffer(kernel, "colors", buffer);
                _shader.Dispatch(kernel, Mathf.CeilToInt(buffer.count / 1024.0f), 1, 1);

                SetPreview(buffer);

                Outputs[0].GetComponent<TextureOutputPointer>().Buffer = buffer;
            }
        }

        protected override void CodeToReset()
        {
            Outputs[0].GetComponent<TextureOutputPointer>().Reset();
        }

        public override void OnSave(FileWriter writer)
        {
            writer.Write(_posterizeSlider.value);
        }

        public override void OnLoad(FileReader reader)
        {
            _posterizeSlider.value = reader.ReadFloat();
        }
    }
}
