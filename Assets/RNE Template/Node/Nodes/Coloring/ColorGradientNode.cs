using RuntimeNodeEditor.UI.Canvas.Node.UI;
using RNE.Template.Node.Pointer;
using Utils.IO.Serialization;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace RNE.Template.Node
{
    public class ColorGradientNode : RuntimeNodeEditor.Node.Node
    {
        private Gradient Gradient
        {
            get;
            set;
        }

        private List<GradientColorKey> _gradientColorKeys;
        private GradientColorKey _gradientColorKey;
        private GradientAlphaKey[] _gradientAlphaKey;

        [SerializeField]
        private RawImage _gradientPreview;

        [SerializeField]
        private UISlider[] _sliders;

        [SerializeField]
        private RawImage[] _images;

        private const int MaxGradientPoints = 8;

        private bool _haltPreviewGeneration = false;

        private Color[] _gradientPreviewColorArray;
        private Texture2D _previewGradientTexture;

        protected override void Init()
        {
            Gradient = new Gradient();
            
            _gradientColorKey = new GradientColorKey();
            _gradientColorKeys = new List<GradientColorKey>();
            _gradientAlphaKey = new GradientAlphaKey[2]
            {
                new GradientAlphaKey(1, 0),
                new GradientAlphaKey(1, 1)
            };

            _gradientPreviewColorArray = new Color[100];
            _previewGradientTexture = new Texture2D(100, 1);
            _previewGradientTexture.wrapMode = TextureWrapMode.Clamp;
        }

        protected override void CodeToExecute()
        {
            for (int i = 0; i < MaxGradientPoints; i++)
            {
                if (Inputs[i].ConnectedOutputPointer == null)
                {
                    _sliders[i].gameObject.SetActive(false);
                    _images[i].gameObject.SetActive(false);

                    continue;
                }

                _sliders[i].gameObject.SetActive(true);
                _images[i].gameObject.SetActive(true);
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
            if (_haltPreviewGeneration)
                return;
                
            _gradientColorKeys.Clear();
            for (int i = 0; i < MaxGradientPoints; i++)
            {
                if (Inputs[i].ConnectedOutputPointer)
                {
                    _gradientColorKey.color = Inputs[i].ConnectedOutputPointer.GetComponent<ColorOutputPointer>().Value;
                    _gradientColorKey.time = _sliders[i].Value;

                    _gradientColorKeys.Add(_gradientColorKey);

                    _images[i].color = _gradientColorKey.color;
                    _sliders[i].ChangeHandleColor(_gradientColorKey.color);
                }
            }
            Gradient.SetKeys(_gradientColorKeys.ToArray(), _gradientAlphaKey);

            for (int i = 0; i < _gradientPreviewColorArray.Length; i++)
            {
                _gradientPreviewColorArray[i] = Gradient.Evaluate((float)i / _gradientPreviewColorArray.Length);
            }

            _previewGradientTexture.SetPixels(_gradientPreviewColorArray);
            _previewGradientTexture.Apply();

            _gradientPreview.texture = _previewGradientTexture;
        }

        public override void OnSave(FileWriter writer)
        {
            for (int i = 0; i < MaxGradientPoints; i++)
            {
                writer.Write(_sliders[i].gameObject.activeSelf);
                writer.Write(_sliders[i].Value);
            }
        }

        public override void OnLoad(FileReader reader)
        {
            _haltPreviewGeneration = true;

            for (int i = 0; i < MaxGradientPoints; i++)
            {
                _images[i].gameObject.SetActive(reader.ReadBool());
                _sliders[i].gameObject.SetActive(_images[i].IsActive());

                _sliders[i].Value = reader.ReadFloat();
            }

            _haltPreviewGeneration = false;
        }
    }
}
