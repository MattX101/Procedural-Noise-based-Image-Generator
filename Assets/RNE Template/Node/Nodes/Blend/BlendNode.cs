using RNE.Template.Node.Pointer;
using RNE.Template.Node.Pointer.Value;
using TMPro;
using Utils.Colors.Blend;
using UnityEngine;
using UnityEngine.UI;
using Utils.IO.Serialization;

namespace RNE.Template.Node
{
    public class BlendNode : NodeWithPreview
    {
        [SerializeField]
        private TMP_Dropdown _dropdown;
        
        protected override void CodeToExecute()
        {
            ComputeBuffer buffer = new ComputeBuffer(ProjectData.Length, sizeof(float) * 4);
            Color[] preview = new Color[ProjectData.Length];

            if (!Inputs[0].ConnectedOutputPointer && !Inputs[1].ConnectedOutputPointer)
            {
                for (int i = 0; i < preview.Length; i++)
                {
                    preview[i] = Color.black;
                }
            }
            else
            {
                if (Inputs[0].ConnectedOutputPointer && !Inputs[1].ConnectedOutputPointer)
                {
                    ExecuteInputConnection(0);
                    buffer = PointerValue.GetTexture(Inputs[0]);
                }
                else if (!Inputs[0].ConnectedOutputPointer && Inputs[1].ConnectedOutputPointer)
                {
                    ExecuteInputConnection(1);
                    buffer = PointerValue.GetTexture(Inputs[1]);
                }
                else
                {
                    ExecuteInputConnection(0);
                    ExecuteInputConnection(1);

                    MixGPU.Blend(
                        ref buffer,
                        PointerValue.GetTexture(Inputs[0]),
                        PointerValue.GetTexture(Inputs[1]),
                        (Blends)_dropdown.value
                    );
                }

                buffer.GetData(preview);
            }

            SetPreview(buffer);

            Outputs[0].GetComponent<TextureOutputPointer>().Buffer = buffer;
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
    }
}
