using RuntimeNodeEditor.UI.Canvas.Node.UI;
using RNE.Template.Node.Pointer;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Utils.IO.Serialization;

namespace RNE.Template.Node
{
    public class ColorGradientNode : RuntimeNodeEditor.Node.Node
    {
        private Gradient Gradient
        {
            get;
            set;
        }

        [SerializeField]
        private RawImage _gradientPreview;

        [SerializeField]
        private UISlider[] _sliders;

        [SerializeField]
        private RawImage[] _images;

        private const int MaxGradientPoints = 8;

        private bool[] _inputPointerConnected = new bool[MaxGradientPoints];
        
        protected override void CodeToExecute()
        {
            for (int i = 0; i < MaxGradientPoints; i++)
            {                
                if (Inputs[i].ConnectedOutputPointer != null && _inputPointerConnected[i] == false)
                {
                    _sliders[i].gameObject.SetActive(true);
                    _images[i].gameObject.SetActive(true);

                    _inputPointerConnected[i] = true;

                    _sliders[i].Value = (float)i / (MaxGradientPoints - 1);
                }
                else if (Inputs[i].ConnectedOutputPointer == null && _inputPointerConnected[i] == true)
                {
                    _sliders[i].gameObject.SetActive(false);
                    _images[i].gameObject.SetActive(false);

                    _inputPointerConnected[i] = false;
                }
            }

            for (int i = 0; i < Inputs.Count; i++)
            {
                ExecuteInputConnection(i);
            }

            GeneratePreviewGradient();

            Outputs[0].GetComponent<ColorGradientOutputPointer>().Value = Gradient;
        }

        protected override void CodeToReset()
        {
            Outputs[0].GetComponent<ColorGradientOutputPointer>().Reset();
        }

        public void GeneratePreviewGradient()
        {
            Gradient = new Gradient();

            List<GradientColorKey> keys = new List<GradientColorKey>();
            for (int i = 0; i < MaxGradientPoints; i++)
            {
                if (_inputPointerConnected[i])
                {
                    GradientColorKey key = new GradientColorKey();
                    key.color = Inputs[i].ConnectedOutputPointer.GetComponent<ColorOutputPointer>().Value;
                    key.time = _sliders[i].Value;

                    keys.Add(key);

                    _images[i].color = key.color;
                    _sliders[i].ChangeHandleColor(key.color);
                }
            }

            GradientAlphaKey[] alpha = new GradientAlphaKey[2]
            {
                new GradientAlphaKey(1, 0),
                new GradientAlphaKey(1, 1)
            };

            Gradient.SetKeys(keys.ToArray(), alpha);

            Color[] gradientPreview = new Color[100];
            for (int i = 0; i < gradientPreview.Length; i++)
            {
                gradientPreview[i] = Gradient.Evaluate((float)i / gradientPreview.Length);
            }

            Texture2D texture = new Texture2D(100, 1);
            texture.wrapMode = TextureWrapMode.Clamp;
            texture.SetPixels(gradientPreview);
            texture.Apply();

            _gradientPreview.texture = texture;
        }

        public override void OnSave(FileWriter writer)
        {
            for (int i = 0; i < MaxGradientPoints; i++)
            {
                writer.Write(_inputPointerConnected[i]);
                writer.Write(_sliders[i].Value);
            }
        }

        public override void OnLoad(FileReader reader)
        {
            for (int i = 0; i < MaxGradientPoints; i++)
            {
                _inputPointerConnected[i] = reader.ReadBool();

                _images[i].gameObject.SetActive(_inputPointerConnected[i]);
                
                _sliders[i].gameObject.SetActive(_inputPointerConnected[i]);
                _sliders[i].Value = reader.ReadFloat();
            }
        }
    }
}
