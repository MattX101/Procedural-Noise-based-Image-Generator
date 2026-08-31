using RNE.Template.Node.Pointer;
using RNE.Template.Node.Pointer.Value;
using Utils.IO.Serialization;
using Utils.Colors.Blend;
using UnityEngine;
using TMPro;

namespace RNE.Template.Node
{
    public class BlendNode : NodeWithPreview
    {
        [SerializeField]
        private TMP_Dropdown _dropdown;

        private ComputeBuffer _colorBuffer;

        protected override void Init()
        {
            _colorBuffer = new ComputeBuffer(ProjectData.Length, sizeof(float) * 4);
        }

        protected override void CodeToExecute()
        {
            if ((Inputs[0].ConnectedOutputPointer && Inputs[1].ConnectedOutputPointer) == false)
            {
                return;
            }
            
            Init();

            if (Inputs[0].ConnectedOutputPointer && !Inputs[1].ConnectedOutputPointer)
            {
                ExecuteInputConnection(0);
                _colorBuffer = PointerValue.GetTexture(Inputs[0]);
            }
            else if (!Inputs[0].ConnectedOutputPointer && Inputs[1].ConnectedOutputPointer)
            {
                ExecuteInputConnection(1);
                _colorBuffer = PointerValue.GetTexture(Inputs[1]);
            }
            else
            {
                ExecuteInputConnection(0);
                ExecuteInputConnection(1);

                Mix.Blend(
                    ref _colorBuffer,
                    PointerValue.GetTexture(Inputs[0]),
                    PointerValue.GetTexture(Inputs[1]),
                    (Blends)_dropdown.value
                );
            }

            SetPreview(_colorBuffer);
            Outputs[0].GetComponent<TextureOutputPointer>().Buffer = _colorBuffer;
        }

        protected override void CodeToReset()
        {
            Outputs[0].GetComponent<TextureOutputPointer>().Reset();
        }

        public override void OnSave(FileWriter writer)
        {
            writer.Write(_dropdown.value);
        }

        public override void OnLoad(FileReader reader)
        {
            _dropdown.value = reader.ReadInt();
        }

        private void OnDestroy()
        {
            _colorBuffer.Release();
        }
    }
}
