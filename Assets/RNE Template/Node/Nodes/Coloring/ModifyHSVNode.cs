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

        [SerializeField] private Slider _hueSlider;
        [SerializeField] private Slider _saturationSlider;
        [SerializeField] private Slider _valueSlider;

        [Space]

        [SerializeField] private RawImage _preview;

        protected override void CodeToExecute()
        {
            Color color = Color.black;

            if (Inputs[3].ConnectedOutputPointer)
            {
                ExecuteInputConnection(3);
                color = PointerValue.GetColor(Inputs[3]);

                float hue = 0.0f;
                if (Inputs[0].ConnectedOutputPointer)
                {
                    ExecuteInputConnection(0);
                    hue = PointerValue.GetFloat(Inputs[0]);
                }
                else
                {
                    hue = _hueSlider.value;
                }

                float saturation = 1.0f;
                if (Inputs[1].ConnectedOutputPointer)
                {
                    ExecuteInputConnection(1);
                    saturation = PointerValue.GetFloat(Inputs[1]);
                }
                else
                {
                    saturation = _saturationSlider.value;
                }

                float value = 0.5f;
                if (Inputs[2].ConnectedOutputPointer)
                {
                    ExecuteInputConnection(2);
                    value = PointerValue.GetFloat(Inputs[2]);
                }
                else
                {
                    value = _valueSlider.value;
                }

                HSV conversion = ColorConversion.RGBToHSV(color);
                color = ColorConversion.HSVToRGB(new HSV(
                    (conversion.Hue + hue) > 360 ? (conversion.Hue + hue) % 360.0f : 360.0f - Mathf.Abs((conversion.Hue + hue) % 360.0f),
                    Mathf.Clamp01(conversion.Saturation + saturation),
                    Mathf.Clamp01(conversion.Value + value)
                ));

                _hueSlider.value = hue % 360.0f;
                _saturationSlider.value = saturation;
                _valueSlider.value = value;
            }

            _preview.color = color;

            Outputs[0].GetComponent<ColorOutputPointer>().Value = color;
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