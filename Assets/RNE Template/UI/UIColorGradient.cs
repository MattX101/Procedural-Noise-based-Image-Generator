using RuntimeNodeEditor.UI.Canvas.Node.UI;
using Utils.IO.Serialization;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace RNE.Template.UI
{
    /// <summary>
    /// TODO : Rework UIColorGradient script
    /// </summary>
    public class UIColorGradient : UIColourPicker
    {
        public Gradient Gradient
        {
            get;
            private set;
        }

        [SerializeField]
        private RawImage _gradientPreview;

        [SerializeField]
        private UISlider[] _sliders;

        [SerializeField]
        private Image[] _images;

        private int _currentActivePoint = 0;
        private int _activePoints = 2;

        private const int MaxGradientPoints = 8;

        //private bool _skip = false;

        private List<GradientColorKey> _keys = new List<GradientColorKey>();
        private GradientAlphaKey[] _alpha = new GradientAlphaKey[2]
        {
            new GradientAlphaKey(1, 0),
            new GradientAlphaKey(1, 1)
        };
        
        private Color[] _gradientPreviewColors = new Color[100];
        private Texture2D _texture;

        private void Init()
        {
            Gradient = new Gradient();

            _texture = new Texture2D(100, 1);
            _texture.wrapMode = TextureWrapMode.Clamp;

            _gradientPreview.texture = _texture;
        }

        public void ChangePreviewImage(Image image)
        {
            /*if (!_skip)
            {
                //
            }*/
            //ResetDropdown();

            SetImage(image);

            //SetSliders(image.color.r, image.color.g, image.color.b);

            //OnSliderValueChange();
        }

        public void GeneratePreviewGradient()
        {
            if (Gradient == null)
            {
                Init();
            }

            _keys.Clear();
            for (int i = 0; i < MaxGradientPoints; i++)
            {
                if (_sliders[i].gameObject.activeSelf)
                {
                    _keys.Add(
                        new GradientColorKey(
                            _sliders[i].Color,
                            _sliders[i].Value)
                        );
                }
            }
            Gradient.SetKeys(_keys.ToArray(), _alpha);

            for (int i = 0; i < _gradientPreviewColors.Length; i++)
            {
                _gradientPreviewColors[i] = Gradient.Evaluate((float)i / _gradientPreviewColors.Length);
            }

            _texture.SetPixels(_gradientPreviewColors);
            _texture.Apply();
        }

        public void SetActive(int index)
        {
            _currentActivePoint = index;
        }

        public void Add()
        {
            if (_activePoints >= MaxGradientPoints)
            {
                return;
            }

            for (int i = 0; i < MaxGradientPoints; i++)
            {
                if (!_sliders[i].gameObject.activeSelf)
                {
                    _currentActivePoint = i;
                    break;
                }
            }

            _sliders[_currentActivePoint].gameObject.SetActive(true);
            _sliders[_currentActivePoint].SetMaxValue(1.0f);
            _sliders[_currentActivePoint].Value = 0.5f;
            _activePoints++;
        }

        public void Remove()
        {
            if (_activePoints <= 2)
                return;

            _sliders[_currentActivePoint].gameObject.SetActive(false);
            _sliders[_currentActivePoint].SetMaxValue(2.0f);
            _sliders[_currentActivePoint].Value = 2.0f;
            _activePoints--;

            for (int i = MaxGradientPoints - 1; i >= 0; i--)
            {
                if (_sliders[i].gameObject.activeSelf)
                {
                    _currentActivePoint = i;
                    break;
                }
            }
        }

        protected override void CodeToSave(FileWriter writer)
        {
            writer.Write(_currentActivePoint);
            writer.Write(_activePoints);

            foreach (UISlider slider in _sliders)
            {
                writer.Write(slider.gameObject.activeSelf);
            }

            foreach (UISlider slider in _sliders)
            {
                writer.Write(slider.Value);
            }

            foreach (Image image in _images)
            {
                writer.Write(image.color.r);
                writer.Write(image.color.g);
                writer.Write(image.color.b);
            }
        }

        protected override void CodeToLoad(FileReader reader)
        {
            //_skip = true;

            _currentActivePoint = reader.ReadInt();
            _activePoints = reader.ReadInt();

            foreach (UISlider slider in _sliders)
            {
                slider.gameObject.SetActive(reader.ReadBool());
            }

            foreach (UISlider slider in _sliders)
            {
                slider.Value = reader.ReadFloat();
            }

            foreach (Image image in _images)
            {
                image.color = new Color(reader.ReadFloat(), reader.ReadFloat(), reader.ReadFloat(), 1);
            }

            GeneratePreviewGradient();

            //_skip = false;
        }
    }
}
