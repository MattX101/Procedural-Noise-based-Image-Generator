using RNE.Template.Node.Pointer;
using RNE.Template.Node.Pointer.Value;
using UnityEngine;
using UnityEngine.UI;

namespace RNE.Template.Node
{
    public class ColoringNode : RuntimeNodeEditor.Node.Node
    {
        [SerializeField]
        private RawImage _image;

        private Color[] _result;

        protected override void CodeToExecute()
        {
            _result = new Color[128 * 128];

            if (!Inputs[0].ConnectedOutputPointer && !Inputs[1].ConnectedOutputPointer)
            {
                for (int i = 0; i < _result.Length; i++)
                {
                    _result[i] = Color.black;
                }
            }
            else if (Inputs[0].ConnectedOutputPointer && !Inputs[1].ConnectedOutputPointer)
            {
                ExecuteInputConnection(0);
                _result = PointerValue.GetColorArray(Inputs[0]);
            }
            else if (!Inputs[0].ConnectedOutputPointer && Inputs[1].ConnectedOutputPointer)
            {
                ExecuteInputConnection(1);
                for (int i = 0; i < _result.Length; i++)
                {
                    _result[i] = Color.black;
                }
            }
            else
            {
                ExecuteInputConnection(0);
                ExecuteInputConnection(1);

                Color[] input = PointerValue.GetColorArray(Inputs[0]);
                Color mod = PointerValue.GetColor(Inputs[1]);

                for (int i = 0; i < input.Length; i++)
                {
                    _result[i] = input[i] * mod;
                }
            }

            Texture2D texture = new Texture2D(128, 128);
            texture.wrapMode = TextureWrapMode.Clamp;
            texture.filterMode = FilterMode.Point;
            texture.SetPixels(_result);
            texture.Apply();

            _image.texture = texture;

            Outputs[0].GetComponent<ColorArrayOutputPointer>().Values = _result;
        }

        protected override void CodeToReset()
        {
            if (_result != null)
            {
                int length = _result.Length;
                _result = new Color[length];
            }

            Outputs[0].GetComponent<ColorArrayOutputPointer>().Reset();
        }
    }
}
