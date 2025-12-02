using RNE.Template.Node.Pointer;
using RNE.Template.Node.Pointer.Value;
using Utils.IO.Serialization;
using Utils.Colors;
using Utils.Colors.Model;
using UnityEngine;
using UnityEngine.UI;

namespace RNE.Template.Node
{
    public class ModifyHSVNode : RuntimeNodeEditor.Node.Node
    {
        [Space]

        [SerializeField]
        private Slider _hueSlider, _saturationSlider, _valueSlider;

        [Space]

        [SerializeField] private RawImage _preview;

        private float _hue = 0, _saturation = 0, _value = 0;

        private Color _previewColor = Color.black;
        private HSV _previewColorHSVConversion;

        protected override void CodeToExecute()
        {
            if (Inputs[3].ConnectedOutputPointer)
            {
                ExecuteInputConnection(3);
                _previewColor = PointerValue.GetColor(Inputs[3]);

                if (Inputs[0].ConnectedOutputPointer)
                {
                    ExecuteInputConnection(0);
                    _hue = PointerValue.GetFloat(Inputs[0]);
                }
                else
                {
                    _hue = _hueSlider.value;
                }

                if (Inputs[1].ConnectedOutputPointer)
                {
                    ExecuteInputConnection(1);
                    _saturation = PointerValue.GetFloat(Inputs[1]);
                }
                else
                {
                    _saturation = _saturationSlider.value;
                }

                if (Inputs[2].ConnectedOutputPointer)
                {
                    ExecuteInputConnection(2);
                    _value = PointerValue.GetFloat(Inputs[2]);
                }
                else
                {
                    _value = _valueSlider.value;
                }

                _previewColorHSVConversion = ColorConversion.RGBToHSV(_previewColor);
                _previewColor = ColorConversion.HSVToRGB(new HSV(
                    (_previewColorHSVConversion.Hue + _hue) > 360 ? (_previewColorHSVConversion.Hue + _hue) % 360.0f : 360.0f - Mathf.Abs((_previewColorHSVConversion.Hue + _hue) % 360.0f),
                    Mathf.Clamp01(_previewColorHSVConversion.Saturation + _saturation),
                    Mathf.Clamp01(_previewColorHSVConversion.Value + _value)
                ));

                _hueSlider.value = _hue % 360.0f;
                _saturationSlider.value = _saturation;
                _valueSlider.value = _value;
            }
            else
            {
                _hueSlider.value = 0;
                _saturationSlider.value = 0;
                _valueSlider.value = 0;
                
                _previewColor = Color.black;
            }

            _preview.color = _previewColor;
            Outputs[0].GetComponent<ColorOutputPointer>().Value = _previewColor;
        }

        protected override void CodeToReset()
        {
            _hueSlider.value = 0;
            _saturationSlider.value = 0;
            _valueSlider.value = 0;

            Outputs[0].GetComponent<ColorOutputPointer>().Reset();
        }

        public override void OnSave(FileWriter writer)
        {
            writer.Write(_hueSlider.value);
            writer.Write(_saturationSlider.value);
            writer.Write(_valueSlider.value);
        }

        public override void OnLoad(FileReader reader)
        {
            _hueSlider.value = reader.ReadFloat();
            _saturationSlider.value = reader.ReadFloat();
            _valueSlider.value = reader.ReadFloat();
        }
    }
}