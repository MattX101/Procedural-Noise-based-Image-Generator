using RNE.Template.Node.Pointer;
using RNE.Template.Node.Pointer.Value;
using RNE.Template.Node.Seed;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils.IO.Serialization;
using Utils.Noise.Profiles;

namespace RNE.Template.Node
{
    public class WarpProfileNode : RuntimeNodeEditor.Node.Node
    {
        [SerializeField] private TMP_InputField _seedField;
        [SerializeField] private TMP_InputField _unvirsalScale;
        
        [Space]
        
        [SerializeField] private Toggle _is3DToggle;
        
        [Space]
        
        [SerializeField] private TMP_InputField _scaleX;
        [SerializeField] private TMP_InputField _scaleY;
        [SerializeField] private TMP_InputField _scaleZ;
        
        [Space]
        
        [SerializeField] private TMP_InputField _offsetX;
        [SerializeField] private TMP_InputField _offsetY;
        [SerializeField] private TMP_InputField _offsetZ;
        
        [Space]
        
        [SerializeField] private TMP_InputField _warpAmp;
        
        [Space]
        
        [SerializeField] private Slider _octaves;
        [SerializeField] private Slider _lacunarity;
        [SerializeField] private Slider _gain;
        [SerializeField] private Slider _weightedStrength;
        
        private WarpProfile _warpProfile;


        protected override void CodeToExecute()
        {
            for (int i = 0; i < Inputs.Count; i++)
            {
                ExecuteInputConnection(i);
            }

            _warpProfile = new WarpProfile();

            _warpProfile.SetNoiseType_Perlin();
            _warpProfile.SetFractalType_FBm();

            // Seed
            if (Inputs[0].ConnectedOutputPointer)
            {
                PointerValue.GetInt(Inputs[0], ref _warpProfile.seed);
                _seedField.text = PointerValue.GetInt(Inputs[0]).ToString();
            }
            else
            {
                _warpProfile.seed = int.Parse(_seedField.text);
            }
            _warpProfile.seed += SeedData.Seed;

            // Universal Scale
            if (Inputs[1].ConnectedOutputPointer)
            {
                PointerValue.GetFloat(Inputs[1], ref _warpProfile.universalScale);
                _unvirsalScale.text = PointerValue.GetFloat(Inputs[1]).ToString();
            }
            else
            {
                _warpProfile.universalScale = float.Parse(_unvirsalScale.text);
            }

            // Toggle 3D
            if (Inputs[2].ConnectedOutputPointer)
            {
                _is3DToggle.isOn = PointerValue.GetBool(Inputs[2]);
            }

            // Scale
            if (Inputs[3].ConnectedOutputPointer)
            {
                PointerValue.GetVector3(Inputs[3], ref _warpProfile.scale);

                _scaleX.text = _warpProfile.scale.x.ToString();
                _scaleY.text = _warpProfile.scale.y.ToString();
                _scaleZ.text = _warpProfile.scale.z.ToString();
            }
            else
            {
                _warpProfile.scale.x = float.Parse(_scaleX.text);
                _warpProfile.scale.y = float.Parse(_scaleY.text);
                _warpProfile.scale.z = float.Parse(_scaleZ.text);
            }

            // Offset
            if (Inputs[4].ConnectedOutputPointer)
            {
                PointerValue.GetVector3(Inputs[4], ref _warpProfile.offset);

                _offsetX.text = _warpProfile.offset.x.ToString();
                _offsetY.text = _warpProfile.offset.y.ToString();
                _offsetZ.text = _warpProfile.offset.z.ToString();
            }
            else
            {
                _warpProfile.offset.x = float.Parse(_offsetX.text);
                _warpProfile.offset.y = float.Parse(_offsetY.text);
                _warpProfile.offset.z = float.Parse(_offsetZ.text);
            }

            // Amp
            if (Inputs[5].ConnectedOutputPointer)
            {
                PointerValue.GetFloat(Inputs[5], ref _warpProfile.warpAmp);
                _warpAmp.text = PointerValue.GetFloat(Inputs[5]).ToString();
            }
            else
            {
                _warpProfile.warpAmp = float.Parse(_warpAmp.text);
            }

            // Octaves
            if (Inputs[6].ConnectedOutputPointer)
            {
                PointerValue.GetInt(Inputs[6], ref _warpProfile.octaves);
                _octaves.value = PointerValue.GetInt(Inputs[6]);
            }
            else
            {
                _warpProfile.octaves = (int)_octaves.value;
            }
            
            // Lacunarity
            if (Inputs[7].ConnectedOutputPointer)
            {
                PointerValue.GetFloat(Inputs[7], ref _warpProfile.lacunarity);
                _lacunarity.value = PointerValue.GetFloat(Inputs[7]);
            }
            else
            {
                _warpProfile.lacunarity = _lacunarity.value;
            }

            // Gain
            if (Inputs[8].ConnectedOutputPointer)
            {
                PointerValue.GetFloat(Inputs[8], ref _warpProfile.gain);
                _gain.value = PointerValue.GetFloat(Inputs[8]);
            }
            else
            {
                _warpProfile.gain = _gain.value;
            }

            // Weighted Strength
            if (Inputs[9].ConnectedOutputPointer)
            {
                PointerValue.GetFloat(Inputs[9], ref _warpProfile.weightedStrength);
                _weightedStrength.value = PointerValue.GetFloat(Inputs[9]);
            }
            else
            {
                _warpProfile.weightedStrength = _weightedStrength.value;
            }

            Outputs[0].GetComponent<WarpProfileOutputPointer>().WarpProfile = _warpProfile;
        }

        public override void OnSave(FileWriter writer)
        {
            writer.Write(_seedField.text);
            writer.Write(_unvirsalScale.text);
            
            writer.Write(_is3DToggle.isOn);
            
            writer.Write(_scaleX.text);
            writer.Write(_scaleY.text);
            writer.Write(_scaleZ.text);
            
            writer.Write(_offsetX.text);
            writer.Write(_offsetY.text);
            writer.Write(_offsetZ.text);
            
            writer.Write(_warpAmp.text);
            
            writer.Write(_octaves.value);
            writer.Write(_lacunarity.value);
            writer.Write(_gain.value);
            writer.Write(_weightedStrength.value);
        }

        public override void OnLoad(FileReader reader)
        {
            _seedField.text = reader.ReadString();
            _unvirsalScale.text = reader.ReadString();
            
            _is3DToggle.isOn = reader.ReadBool();
            
            _scaleX.text = reader.ReadString();
            _scaleY.text = reader.ReadString();
            _scaleZ.text = reader.ReadString();
            
            _offsetX.text = reader.ReadString();
            _offsetY.text = reader.ReadString();
            _offsetZ.text = reader.ReadString();
            
            _warpAmp.text = reader.ReadString();
            
            _octaves.value = reader.ReadFloat();
            _lacunarity.value = reader.ReadFloat();
            _gain.value = reader.ReadFloat();
            _weightedStrength.value = reader.ReadFloat();
        }
    }
}
