using RNE.Template.Node.Pointer;
using RNE.Template.Node.Pointer.Value;
using Utils.Colors.Blend;
using UnityEngine;
using UnityEngine.UI;

namespace RNE.Template.Node
{
    public class BlendNode : RuntimeNodeEditor.Node.Node
    {
        [SerializeField]
        private RawImage _image;

        private Color[] _result;

        private delegate Color BlendFormula(Color a, Color b);

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

                BlendFormula formula = GetFormula(Elements.dropdowns[0].value);
                for (int i = 0; i < a.Length; i++)
                {
                    _result[i] = formula(a[i], b[i]);
                }
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

        private BlendFormula GetFormula(int index)
        {
            return index switch
            {
                /// Arithmetic
                (int)Blends.Add => Mix.Add,
                (int)Blends.Subtract => Mix.Subtract,
                (int)Blends.Multiply => Mix.Multiply,
                (int)Blends.Divide => Mix.Divide,
                (int)Blends.Average => Mix.Average,

                /// Darken
                (int)Blends.Darken => Mix.Darken,
                (int)Blends.ColourBurn => Mix.ColorBurn,
                (int)Blends.LinearBurn => Mix.LinearBurn,
                (int)Blends.GammaDark => Mix.GammaDark,

                /// Lighten
                (int)Blends.Lighten => Mix.Lighten,
                (int)Blends.Shine => Mix.Shine,
                (int)Blends.ColourDodge => Mix.ColourDodge,
                (int)Blends.Screen => Mix.Screen,
                (int)Blends.Overlay => Mix.Overlay,
                (int)Blends.SoftLight => Mix.SoftLight,
                (int)Blends.HardLight => Mix.HardLight,
                // Vivid Light
                (int)Blends.LinearLight => Mix.LinearLight,
                // Pin Light
                (int)Blends.HardMix => Mix.HardMix,
                (int)Blends.Tint => Mix.Tint,
                (int)Blends.GammaLight => Mix.GammaLight,
                (int)Blends.GammaIllumination => Mix.GammaIllumination,

                /// Other
                (int)Blends.Exclusion => Mix.Exclusion,
                (int)Blends.Difference => Mix.Difference,
                (int)Blends.Negation => Mix.Negation,

                ///
                (int)Blends.Hue => Mix.Hue,
                (int)Blends.Saturation => Mix.Saturation,
                (int)Blends.Color => Mix.Color,
                (int)Blends.Luminosity => Mix.Luminosity,

                _ => Mix.Add
            };
        }
    }
}
