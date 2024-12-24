using RNE.Template.Node.Pointer;
using RNE.Template.Node.Pointer.Value;
using Utils.Noise.Profiles;

namespace RNE.Template.Node
{
    public class WarpProfileNode : RuntimeNodeEditor.Node.Node
    {
        private WarpProfile _warpProfile;

        protected override void CodeToExecute()
        {
            for (int i = 0; i < Inputs.Count; i++)
            {
                ExecuteInputConnection(i);
            }

            _warpProfile = new();

            _warpProfile.SetNoiseType_Perlin();
            _warpProfile.SetFractalType_FBm();

            // Seed
            if (Inputs[0].ConnectedOutputPointer)
            {
                PointerValue.GetInt(Inputs[0], ref _warpProfile.seed);
                Elements.SetInputField(Elements.inputFields[0], PointerValue.GetInt(Inputs[0]).ToString());
            }
            else
            {
                _warpProfile.seed = int.Parse(Elements.inputFields[0].text);
            }

            // Universal Scale
            if (Inputs[1].ConnectedOutputPointer)
            {
                PointerValue.GetFloat(Inputs[1], ref _warpProfile.universalScale);
                Elements.SetInputField(Elements.inputFields[1], PointerValue.GetFloat(Inputs[1]).ToString());
            }
            else
            {
                _warpProfile.universalScale = float.Parse(Elements.inputFields[1].text);
            }

            // Toggle 3D
            bool is3D = false;
            if (Inputs[2].ConnectedOutputPointer)
            {
                is3D = PointerValue.GetBool(Inputs[2]);
                Elements.SetBoolean(Elements.buttons[0], PointerValue.GetBool(Inputs[2]));
            }
            else
            {
                is3D = Elements.buttons[0].isOn;
            }

            // Scale
            if (Inputs[3].ConnectedOutputPointer)
            {
                PointerValue.GetVector3(Inputs[3], ref _warpProfile.scale);

                Elements.SetInputField(Elements.inputFields[2], _warpProfile.scale.x.ToString());
                Elements.SetInputField(Elements.inputFields[3], _warpProfile.scale.y.ToString());
                Elements.SetInputField(Elements.inputFields[4], _warpProfile.scale.z.ToString());
            }
            else
            {
                _warpProfile.scale.x = float.Parse(Elements.inputFields[2].text);
                _warpProfile.scale.y = float.Parse(Elements.inputFields[3].text);
                _warpProfile.scale.z = float.Parse(Elements.inputFields[4].text);
            }

            // Offset
            if (Inputs[4].ConnectedOutputPointer)
            {
                PointerValue.GetVector3(Inputs[4], ref _warpProfile.offset);

                Elements.SetInputField(Elements.inputFields[5], _warpProfile.offset.x.ToString());
                Elements.SetInputField(Elements.inputFields[6], _warpProfile.offset.y.ToString());
                Elements.SetInputField(Elements.inputFields[7], _warpProfile.offset.z.ToString());
            }
            else
            {
                _warpProfile.offset.x = float.Parse(Elements.inputFields[5].text);
                _warpProfile.offset.y = float.Parse(Elements.inputFields[6].text);
                _warpProfile.offset.z = float.Parse(Elements.inputFields[7].text);
            }

            // Amp
            if (Inputs[5].ConnectedOutputPointer)
            {
                PointerValue.GetFloat(Inputs[5], ref _warpProfile.warpAmp);
                Elements.SetInputField(Elements.inputFields[8], PointerValue.GetFloat(Inputs[5]).ToString());
            }
            else
            {
                _warpProfile.warpAmp = float.Parse(Elements.inputFields[8].text);
            }

            // Octaves
            if (Inputs[6].ConnectedOutputPointer)
            {
                PointerValue.GetInt(Inputs[6], ref _warpProfile.octaves);
                Elements.SetSlider(Elements.sliders[0], PointerValue.GetInt(Inputs[6]));
            }
            else
            {
                _warpProfile.octaves = (int)Elements.sliders[0].value;
            }

            // Lacunarity
            if (Inputs[7].ConnectedOutputPointer)
            {
                PointerValue.GetFloat(Inputs[7], ref _warpProfile.lacunarity);
                Elements.SetSlider(Elements.sliders[1], PointerValue.GetFloat(Inputs[7]));
            }
            else
            {
                _warpProfile.lacunarity = Elements.sliders[1].value;
            }

            // Gain
            if (Inputs[8].ConnectedOutputPointer)
            {
                PointerValue.GetFloat(Inputs[8], ref _warpProfile.gain);
                Elements.SetSlider(Elements.sliders[2], PointerValue.GetFloat(Inputs[8]));
            }
            else
            {
                _warpProfile.gain = Elements.sliders[2].value;
            }

            // Weighted Stregth
            if (Inputs[9].ConnectedOutputPointer)
            {
                PointerValue.GetFloat(Inputs[9], ref _warpProfile.weightedStregth);
                Elements.SetSlider(Elements.sliders[3], PointerValue.GetFloat(Inputs[9]));
            }
            else
            {
                _warpProfile.weightedStregth = Elements.sliders[3].value;
            }

            Outputs[0].GetComponent<WarpProfileOutputPointer>().WarpProfile = _warpProfile;
        }
    }
}
