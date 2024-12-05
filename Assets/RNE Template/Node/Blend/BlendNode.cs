using RNE.Template.Node.Pointer;
using RNE.Template.Node.Pointer.Value;
using UnityEngine;
using UnityEngine.UI;

namespace RNE.Template.Node
{
    public class BlendNode : RuntimeNodeEditor.Node.Node
    {
        [SerializeField]
        private RawImage _image;

        private Color[] _result;

        protected override void CodeToExecute()
        {
            if (!Inputs[0])
                return;

            ExecuteInputConnection(0);
            ExecuteInputConnection(1);

            if (Inputs[1])
            {
                Color[] a = PointerValue.GetColorArray(Inputs[0]);
                Color[] b = PointerValue.GetColorArray(Inputs[1]);

                _result = new Color[a.Length];
                Blend(a, b);
            }
            else
            {
                _result = PointerValue.GetColorArray(Inputs[0]);
            }

            Texture2D texture = new Texture2D(128, 128);
            texture.SetPixels(_result);
            texture.Apply();
            _image.texture = texture;
        }

        protected override void DataToGetAndSet()
        {
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

        private void Blend(Color[] a, Color[] b)
        {
            switch (Elements.dropdowns[0].value)
            {
                case 0:
                    Add(a, b);
                    break;
                case 1:
                    Subtract(a, b);
                    break;
                case 2:
                    Multiply(a, b);
                    break;
                case 3:
                    Divide(a, b);
                    break;
                case 4:
                    Average(a, b);
                    break;
                default:
                    break;
            }
        }

        private void Add(Color[] a, Color[] b)
        {
            for (int i = 0; i < a.Length; i++)
            {
                _result[i].r = Mathf.Clamp01(a[i].r + b[i].r);
                _result[i].g = Mathf.Clamp01(a[i].g + b[i].g);
                _result[i].b = Mathf.Clamp01(a[i].b + b[i].b);
                _result[i].a = 1;
            }
        }
        private void Subtract(Color[] a, Color[] b)
        {
            for (int i = 0; i < a.Length; i++)
            {
                _result[i].r = Mathf.Clamp01(a[i].r - b[i].r);
                _result[i].g = Mathf.Clamp01(a[i].g - b[i].g);
                _result[i].b = Mathf.Clamp01(a[i].b - b[i].b);
                _result[i].a = 1;
            }
        }
        private void Multiply(Color[] a, Color[] b)
        {
            for (int i = 0; i < a.Length; i++)
            {
                _result[i].r = Mathf.Clamp01(a[i].r * b[i].r);
                _result[i].g = Mathf.Clamp01(a[i].g * b[i].g);
                _result[i].b = Mathf.Clamp01(a[i].b * b[i].b);
                _result[i].a = 1;
            }
        }
        private void Divide(Color[] a, Color[] b)
        {
            for (int i = 0; i < a.Length; i++)
            {
                _result[i].r = Mathf.Clamp01(a[i].r / b[i].r);
                _result[i].g = Mathf.Clamp01(a[i].g / b[i].g);
                _result[i].b = Mathf.Clamp01(a[i].b / b[i].b);
                _result[i].a = 1;
            }
        }
        private void Average(Color[] a, Color[] b)
        {
            for (int i = 0; i < a.Length; i++)
            {
                _result[i].r = Mathf.Clamp01((a[i].r + b[i].r) / 2);
                _result[i].g = Mathf.Clamp01((a[i].g + b[i].g) / 2);
                _result[i].b = Mathf.Clamp01((a[i].b + b[i].b) / 2);
                _result[i].a = 1;
            }
        }
    }
}
