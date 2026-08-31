using RNE.Template.Node.Pointer;
using RNE.Template.Node.Pointer.Value;
using Utils.IO.Serialization;
using Utils.Curves;
using UnityEngine;
using UnityEngine.UI;

namespace RNE.Template.Node
{
    public class BiasAndGainNode : NodeWithPreview
    {
        [SerializeField] private Slider _biasSlider;
        [SerializeField] private Slider _gainSlider;

        private ComputeBuffer _textureBuffer;

        protected override void CodeToExecute()
        {
            if (Inputs[0].ConnectedOutputPointer == null)
            {
                return;
            }

            ExecuteInputConnection(0);

            _textureBuffer = PointerValue.GetTexture(Inputs[0]);

            BiasAndGain.ModifyImage(ref _textureBuffer, _biasSlider.value, _gainSlider.value);
            SetPreview(_textureBuffer);

            Outputs[0].GetComponent<TextureOutputPointer>().Buffer = _textureBuffer;
        }

        protected override void CodeToReset()
        {
            Outputs[0].GetComponent<TextureOutputPointer>().Reset();
        }

        public override void OnSave(FileWriter writer)
        {
            writer.Write(_biasSlider.value);
            writer.Write(_gainSlider.value);
        }

        public override void OnLoad(FileReader reader)
        {
            _biasSlider.value = reader.ReadFloat();
            _gainSlider.value = reader.ReadFloat();
        }
    }
}
