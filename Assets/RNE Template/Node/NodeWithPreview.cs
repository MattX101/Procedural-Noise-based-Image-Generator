using UnityEngine;
using UnityEngine.UI;

namespace RNE.Template.Node
{
    public class NodeWithPreview : RuntimeNodeEditor.Node.Node
    {
        [SerializeField]
        private RawImage _image;

        internal Texture2D Texture => _image.texture as Texture2D;

        protected void SetPreview(Texture2D texture)
        {
            _image.texture = texture;
        }
    }
}
