using RNE.Template.Node.Pointer.Value;
using RuntimeNodeEditor.Node.Pointer;
using UnityEngine;

namespace RNE.Template.Node.Pointer
{
    public class TextureOutputPointer : OutputPointer
    {
        public ComputeBuffer Buffer;

        private void Awake()
        {
            CreateBuffer();

            ValueTypeIndex = (int)ValueType.Texture;
        }

        private void OnDestroy()
        {
            Buffer.Release();
        }

        protected override void ResetPointer()
        {
            Buffer.Release();
            CreateBuffer();
        }

        private void CreateBuffer()
        {
            Buffer = new ComputeBuffer(Template.ProjectData.Length, sizeof(float) * 4);
        }

        protected override Color GetLineColor()
        {
            return ProjectData.Colors.Texture;
        }
    }
}