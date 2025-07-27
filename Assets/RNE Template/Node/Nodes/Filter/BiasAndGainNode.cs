using RNE.Template.Node.Pointer;
using RNE.Template.Node.Pointer.Value;
using Utils.Curves;
using UnityEngine;
using UnityEngine.UI;
using Utils.IO.Serialization;

namespace RNE.Template.Node
{
    public class BiasAndGainNode : NodeWithPreview
    {
        [SerializeField] private Slider _biasSlider;
        [SerializeField] private Slider _gainSlider;
        
        protected override void CodeToExecute()
        {
            ExecuteInputConnection(0);

            ComputeBuffer buffer = PointerValue.GetTexture(Inputs[0]);

            if (buffer != null)
            {
                BiasAndGainGPU.ModifyImage(ref buffer, _biasSlider.value, _gainSlider.value);

                Color[] colors = new Color[buffer.count];
                buffer.GetData(colors);
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
