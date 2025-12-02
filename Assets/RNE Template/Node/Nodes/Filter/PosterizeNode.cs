using RNE.Template.Node.Pointer;
using RNE.Template.Node.Pointer.Value;
using Utils.IO.Serialization;
using UnityEngine;
using UnityEngine.UI;

namespace RNE.Template.Node
{
    public class PosterizeNode : NodeWithPreview
    {
        [SerializeField] private Slider _posterizeSlider;

        [SerializeField]
        private ComputeShader _shader;

        private ComputeBuffer _textureBuffer;
        private int _kernel;

        protected override void CodeToExecute()
        {
            ExecuteInputConnection(0);

            _textureBuffer = PointerValue.GetTexture(Inputs[0]);
            if (_textureBuffer != null)
            {
                return;
            }

            _kernel = _shader.FindKernel("Posterize");
            _shader.SetFloats("step", 1.0f / (_posterizeSlider.value - 1));
            _shader.SetBuffer(_kernel, "colors", _textureBuffer);
            _shader.Dispatch(_kernel, Mathf.CeilToInt(_textureBuffer.count / 1024.0f), 1, 1);

            SetPreview(_textureBuffer);

            Outputs[0].GetComponent<TextureOutputPointer>().Buffer = _textureBuffer;
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

        private void OnDestroy()
        {
            _textureBuffer.Release();
        }
    }
}
