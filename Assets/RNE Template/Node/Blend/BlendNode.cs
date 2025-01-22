using RNE.Template.Node.Pointer;
using RNE.Template.Node.Pointer.Value;
using Utils.Colors.Blend;
using UnityEngine;

namespace RNE.Template.Node
{
    public class BlendNode : NodeWithPreview
    {
        private Color[] _result;

        private delegate Color BlendFormula(Color a, Color b);

        protected override void CodeToExecute()
        {
            _result = new Color[PreviewTexture.Length];

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
                _result = PointerValue.GetColorArray(Inputs[1]);
            }
            else
            {
                ExecuteInputConnection(0);
                ExecuteInputConnection(1);

                Color[] a = PointerValue.GetColorArray(Inputs[0]);
                Color[] b = PointerValue.GetColorArray(Inputs[1]);

                BlendFormula formula = GetFormula(Elements.dropdowns[0].value);
                for (int i = 0; i < a.Length; i++)
                {
                    _result[i] = formula(a[i], b[i]);
                }
            }

            SetPreview(PreviewTexture.Generate(_result));
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
                (int)Blends.Add => MixCPU.Add,
                (int)Blends.Subtract => MixCPU.Subtract,
                (int)Blends.Multiply => MixCPU.Multiply,
                (int)Blends.Divide => MixCPU.Divide,
                (int)Blends.Average => MixCPU.Average,

                /// Darken
                (int)Blends.Darken => MixCPU.Darken,
                (int)Blends.ColorBurn => MixCPU.ColorBurn,
                (int)Blends.LinearBurn => MixCPU.LinearBurn,
                (int)Blends.GammaDark => MixCPU.GammaDark,

                /// Lighten
                (int)Blends.Lighten => MixCPU.Lighten,
                (int)Blends.Shine => MixCPU.Shine,
                (int)Blends.ColorDodge => MixCPU.ColorDodge,
                (int)Blends.Screen => MixCPU.Screen,
                (int)Blends.Overlay => MixCPU.Overlay,
                (int)Blends.SoftLight => MixCPU.SoftLight,
                (int)Blends.HardLight => MixCPU.HardLight,
                // Vivid Light
                (int)Blends.LinearLight => MixCPU.LinearLight,
                // Pin Light
                (int)Blends.HardMix => MixCPU.HardMix,
                (int)Blends.Tint => MixCPU.Tint,
                (int)Blends.GammaLight => MixCPU.GammaLight,
                (int)Blends.GammaIllumination => MixCPU.GammaIllumination,

                /// Other
                (int)Blends.Exclusion => MixCPU.Exclusion,
                (int)Blends.Difference => MixCPU.Difference,
                (int)Blends.Negation => MixCPU.Negation,

                ///
                (int)Blends.Hue => MixCPU.Hue,
                (int)Blends.Saturation => MixCPU.Saturation,
                (int)Blends.Color => MixCPU.Color,
                (int)Blends.Luminosity => MixCPU.Luminosity,

                _ => MixCPU.Add
            };
        }
    }
}
