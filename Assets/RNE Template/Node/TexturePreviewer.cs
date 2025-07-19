using RuntimeNodeEditor.UI.Canvas.Node.Components;
using UnityEngine;
using UnityEngine.UI;

namespace RNE.Template.Node
{
    internal class TexturePreviewer : MonoBehaviour
    {
        [SerializeField]
        private RawImage _preview;

        private NodeWithPreview _node;

        private void Update()
        {
            if (SelectionData.ActiveNodeUIIsNull)
                return;

            if (SelectionData.ActiveNode.TryGetComponent(out NodeWithPreview preview))
            {
                _node = preview;
            }

            SetPreview();
        }

        public void SetPreview()
        {
            if (_node)
            {
                _preview.texture = _node.Texture;
            }
        }
    }
}
