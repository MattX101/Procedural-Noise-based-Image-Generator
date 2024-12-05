using RNE.Template.Node.Pointer;
using RNE.Template.Node.Pointer.Value;
using UnityEngine;
using UnityEngine.UI;

namespace RNE.Template.Node
{
    public class NoiseNode : RuntimeNodeEditor.Node.Node
    {
        [SerializeField]
        private RawImage _image;

        private Color[] _result;

        protected override void CodeToExecute()
        {
            ExecuteInputConnection(0);

            System.Random rand = new System.Random(PointerValue.GetInt(Inputs[0]));
            float xOffset = rand.Next(-10000, 10000);
            float yOffset = rand.Next(-10000, 10000);

            int res = 128;
            _result = new Color[res * res];

            float s = 0.025f;

            for (int y = 0, i = 0; y < res; y++)
            {
                for (int x = 0; x < res; x++, i++)
                {
                    _result[i] = Color.white * Mathf.PerlinNoise((x + xOffset) * s, (y + yOffset) * s);
                    _result[i].a = 1;
                }
            }

            Texture2D texture = new Texture2D(res, res);
            texture.SetPixels(_result);
            texture.Apply();
            _image.texture = texture;
        }

        protected override void DataToGetAndSet()
        {
            Outputs[0].GetComponent<ColorArrayOutputPointer>().Values = _result;
            Elements.SetInputField(Elements.inputFields[0], PointerValue.GetInt(Inputs[0]).ToString());
        }

        protected override void CodeToReset()
        {
            if (_result != null)
            {
                int length = _result.Length;
                _result = new Color[length];
            }

            Outputs[0].GetComponent<ColorArrayOutputPointer>().Reset();
            Elements.SetInputField(Elements.inputFields[0], PointerValue.GetInt(Inputs[0]).ToString());
        }
    }
}
