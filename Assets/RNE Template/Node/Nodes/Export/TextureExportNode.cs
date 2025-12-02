using RNE.Template.Node.Pointer.Value;
using UnityEngine;

namespace RNE.Template.Node
{
    public class TextureExportNode : NodeWithPreview
    {
        private ComputeBuffer _textureBuffer;

        private Color[] _colors;

        protected override void CodeToExecute()
        {
            ExecuteInputConnection(0);

            _textureBuffer = PointerValue.GetTexture(Inputs[0]);
            if (_textureBuffer == null)
            {
                return;
            }

            if (ProjectData.Export && ProjectData.ExportPath != null)
            {
                ProjectData.IncrementFrame();

                if (_colors == null || _colors.Length != ProjectData.Length)
                {
                    _colors = new Color[ProjectData.Length];
                }

                _textureBuffer.GetData(_colors);

                ExportToImage.Export(PreviewTexture.Generate(_colors), "Image_" + this.GetHashCode());
            }

            SetPreview(_textureBuffer);
        }

        private void OnDestroy()
        {
            _textureBuffer.Release();
        }
    }
}
