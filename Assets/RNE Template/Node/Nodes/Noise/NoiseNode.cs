using RNE.Template.Node.Pointer;
using RNE.Template.Node.Pointer.Value;
using RNE.Template.Node.Seed;
using TMPro;
using Utils.Noise;
using Utils.Noise.Profiles;
using Utils.Colors.Coloring;
using UnityEngine;
using UnityEngine.UI;
using Utils.IO.Serialization;

namespace RNE.Template.Node
{
    public class NoiseNode : NodeWithPreview
    {
        [SerializeField] private TMP_Dropdown _noiseTypeDropdown;
        [SerializeField] private TMP_Dropdown _fractalTypeDropdown;
        
        [Space]
        
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
        
        [SerializeField] private Slider _octaves;
        [SerializeField] private Slider _lacunarity;
        [SerializeField] private Slider _gain;
        [SerializeField] private Slider _weightedStrength;
        
        [Space]
        
        [SerializeField] private TMP_InputField _pingPongStrength;
        
        private float[] _values;
        private Color[] _colors;

        protected override void CodeToExecute()
        {
            for (int i = 0; i < Inputs.Count; i++)
            {
                ExecuteInputConnection(i);
            }

            NoiseProfile noiseProfile = new NoiseProfile();
            WarpProfile warpProfile = null;

            switch (_noiseTypeDropdown.value)
            {
                case 0:
                    noiseProfile.SetNoiseType_Perlin();
                    break;
                case 1:
                    noiseProfile.SetNoiseType_OpenSimplex();
                    break;
                case 2:
                    noiseProfile.SetNoiseType_OpenSimplexS();
                    break;
                case 3:
                    noiseProfile.SetNoiseType_Value();
                    break;
                case 4:
                    noiseProfile.SetNoiseType_ValueCubic();
                    break;
                case 5:
                    noiseProfile.SetNoiseType_Cellular();
                    break;
                default:
                    noiseProfile.SetNoiseType_Perlin();
                    break;
            }

            switch (_fractalTypeDropdown.value)
            {
                case 0:
                    noiseProfile.SetFractalType_FBm();
                    break;
                case 1:
                    noiseProfile.SetFractalType_Ridged();
                    break;
                case 2:
                    noiseProfile.SetFractalType_PingPong();
                    break;
                default:
                    noiseProfile.SetFractalType_FBm();
                    break;
            };

            // Seed
            if (Inputs[0].ConnectedOutputPointer)
            {
                PointerValue.GetInt(Inputs[0], ref noiseProfile.seed);
                _seedField.text = PointerValue.GetInt(Inputs[0]).ToString();
            }
            else
            {
                if (_seedField.text.Length != 0)
                {
                    noiseProfile.seed = int.Parse(_seedField.text);
                }
                else
                {
                    _seedField.text = "0";
                    noiseProfile.seed = 0;
                }
            }
            noiseProfile.seed += SeedData.Seed;

            // Universal Scale
            if (Inputs[1].ConnectedOutputPointer)
            {
                PointerValue.GetFloat(Inputs[1], ref noiseProfile.universalScale);
                _unvirsalScale.text = PointerValue.GetFloat(Inputs[1]).ToString();
            }
            else
            {
                if (_unvirsalScale.text.Length != 0)
                {
                    noiseProfile.universalScale = int.Parse(_unvirsalScale.text);
                }
                else
                {
                    _unvirsalScale.text = "1";
                    noiseProfile.universalScale = 1;
                }
            }

            // Toggle 3D
            if (Inputs[2].ConnectedOutputPointer)
            {
                _is3DToggle.isOn = PointerValue.GetBool(Inputs[2]);
            }

            // Scale
            if (Inputs[3].ConnectedOutputPointer)
            {
                PointerValue.GetVector3(Inputs[3], ref noiseProfile.scale);
                
                _scaleX.text = noiseProfile.scale.x.ToString();
                _scaleY.text = noiseProfile.scale.y.ToString();
                _scaleZ.text = noiseProfile.scale.z.ToString();
            }
            else
            {
                if (_scaleX.text.Length != 0)
                {
                    noiseProfile.scale.x = float.Parse(_scaleX.text);
                }
                else
                {
                    _scaleX.text = "1";
                    noiseProfile.scale.x = 1;
                }

                if (_scaleY.text.Length != 0)
                {
                    noiseProfile.scale.y = float.Parse(_scaleY.text);
                }
                else
                {
                    _scaleY.text = "1";
                    noiseProfile.scale.y = 1;
                }

                if (_scaleZ.text.Length != 0)
                {
                    noiseProfile.scale.z = float.Parse(_scaleZ.text);
                }
                else
                {
                    _scaleZ.text = "1";
                    noiseProfile.scale.z = 1;
                }
            }

            // Offset
            if (Inputs[4].ConnectedOutputPointer)
            {
                PointerValue.GetVector3(Inputs[4], ref noiseProfile.offset);
                
                _offsetX.text = noiseProfile.offset.x.ToString();
                _offsetY.text = noiseProfile.offset.y.ToString();
                _offsetZ.text = noiseProfile.offset.z.ToString();
            }
            else
            {
                if (_offsetX.text.Length != 0)
                {
                    noiseProfile.offset.x = float.Parse(_offsetX.text);
                }
                else
                {
                    _offsetX.text = "0";
                    noiseProfile.offset.x = 0;
                }

                if (_offsetY.text.Length != 0)
                {
                    noiseProfile.offset.y = float.Parse(_offsetY.text);
                }
                else
                {
                    _offsetY.text = "0";
                    noiseProfile.offset.y = 0;
                }

                if (_offsetZ.text.Length != 0)
                {
                    noiseProfile.offset.z = float.Parse(_offsetZ.text);
                }
                else
                {
                    _offsetZ.text = "0";
                    noiseProfile.offset.z = 0;
                }
            }

            // Octaves
            if (Inputs[5].ConnectedOutputPointer)
            {
                PointerValue.GetInt(Inputs[5], ref noiseProfile.octaves);
                _octaves.value = PointerValue.GetInt(Inputs[5]);
            }
            else
            {
                noiseProfile.octaves = (int)_octaves.value;
            }

            // Lacunarity
            if (Inputs[6].ConnectedOutputPointer)
            {
                PointerValue.GetFloat(Inputs[6], ref noiseProfile.lacunarity);
                _lacunarity.value = PointerValue.GetFloat(Inputs[6]);
            }
            else
            {
                noiseProfile.lacunarity = _lacunarity.value;
            }

            // Gain
            if (Inputs[7].ConnectedOutputPointer)
            {
                PointerValue.GetFloat(Inputs[7], ref noiseProfile.gain);
                _gain.value = PointerValue.GetFloat(Inputs[7]);
            }
            else
            {
                noiseProfile.gain = _gain.value;
            }

            // Weighted Strength
            if (Inputs[8].ConnectedOutputPointer)
            {
                PointerValue.GetFloat(Inputs[8], ref noiseProfile.weightedStrength);
                _weightedStrength.value = PointerValue.GetFloat(Inputs[8]);
            }
            else
            {
                noiseProfile.weightedStrength = _weightedStrength.value;
            }

            // Ping Pong
            if (Inputs[9].ConnectedOutputPointer)
            {
                PointerValue.GetFloat(Inputs[9], ref noiseProfile.pingPongStrength);
                _pingPongStrength.text = PointerValue.GetFloat(Inputs[9]).ToString();
            }
            else
            {
                if (_pingPongStrength.text.Length != 0)
                {
                    noiseProfile.pingPongStrength = float.Parse(_pingPongStrength.text);
                }
                else
                {
                    _pingPongStrength.text = "1";
                    noiseProfile.pingPongStrength = 1;
                }
            }

            // Cellular Profile
            if (Inputs[10].ConnectedOutputPointer)
            {
                if (TryGetComponent(out CellularProfileNode cellularProfile))
                {
                    if (cellularProfile.ReturnType)
                    {
                        switch (cellularProfile.ReturnType.value)
                        {
                            case 0: noiseProfile.SetCellular_Cell(); break;
                            case 1: noiseProfile.SetCellular_Distance(); break;
                            case 2: noiseProfile.SetCellular_Distance2(); break;
                            case 3: noiseProfile.SetCellular_Distance2Add(); break;
                            case 4: noiseProfile.SetCellular_Distance2Sub(); break;
                            case 5: noiseProfile.SetCellular_Distance2Mul(); break;
                            case 6: noiseProfile.SetCellular_Distance2Div(); break;
                            default: noiseProfile.SetCellular_Cell(); break;
                        }
                    }
                    else
                    {
                        noiseProfile.SetCellular_Cell();
                    }

                    if (cellularProfile.DistanceType)
                    {
                        switch (cellularProfile.DistanceType.value)
                        {
                            case 0: noiseProfile.SetCellularDistanceFunction_Euclidean(); break;
                            case 1: noiseProfile.SetCellularDistanceFunction_EuclideanSq(); break;
                            case 2: noiseProfile.SetCellularDistanceFunction_Manhattan(); break;
                            case 3: noiseProfile.SetCellularDistanceFunction_Hybrid(); break;
                            default: noiseProfile.SetCellularDistanceFunction_Euclidean(); break;
                        }
                    }
                    else
                    {
                        noiseProfile.SetCellularDistanceFunction_Euclidean();
                    }

                    noiseProfile.jitter = cellularProfile.Jitter ? cellularProfile.Jitter.value : 1.0f;
                }
                else
                {
                    noiseProfile.SetCellular_Cell();
                    noiseProfile.SetCellularDistanceFunction_Euclidean();
                    
                    noiseProfile.jitter = 1.0f;
                }
            }
            //

            _values = new float[ProjectData.Length];
            _colors = new Color[ProjectData.Length];

            noiseProfile.warp = Inputs[11].ConnectedOutputPointer;
            noiseProfile.Init();

            if (Inputs[11].ConnectedOutputPointer)
            {
                warpProfile = PointerValue.GetWarpProfile(Inputs[11]);
                warpProfile.Init();
            }

            ComputeBuffer noiseBuffer = new ComputeBuffer(ProjectData.Length, sizeof(float));
            FastNoise2DGPU.GenerateNoise(ref noiseBuffer, ProjectData.Resolution, ProjectData.Resolution, _is3DToggle.isOn, noiseProfile, warpProfile);
            Outputs[0].GetComponent<NoiseOutputPointer>().Buffer = noiseBuffer;

            ComputeBuffer colorsBuffer = new ComputeBuffer(ProjectData.Length, sizeof(float) * 4);
            Coloring.ColoringGPU(ref colorsBuffer, noiseBuffer, Color.white);

            SetPreview(colorsBuffer);
            colorsBuffer.Release();
        }

        protected override void CodeToReset()
        {
            Outputs[0].GetComponent<NoiseOutputPointer>().Reset();
        }

        public override void OnSave(FileWriter writer)
        {
            writer.Write(_noiseTypeDropdown.value);
            writer.Write(_fractalTypeDropdown.value);
            
            writer.Write(_seedField.text);
            writer.Write(_unvirsalScale.text);
            
            writer.Write(_is3DToggle.isOn);
            
            writer.Write(_scaleX.text);
            writer.Write(_scaleY.text);
            writer.Write(_scaleZ.text);
            
            writer.Write(_offsetX.text);
            writer.Write(_offsetY.text);
            writer.Write(_offsetZ.text);
            
            writer.Write(_octaves.value);
            writer.Write(_lacunarity.value);
            writer.Write(_gain.value);
            writer.Write(_weightedStrength.value);
            
            writer.Write(_pingPongStrength.text);
        }

        public override void OnLoad(FileReader reader)
        {
            _noiseTypeDropdown.value = reader.ReadInt();
            _fractalTypeDropdown.value = reader.ReadInt();
            
            _seedField.text = reader.ReadString();
            _unvirsalScale.text = reader.ReadString();
            
            _is3DToggle.isOn = reader.ReadBool();
            
            _scaleX.text = reader.ReadString();
            _scaleY.text = reader.ReadString();
            _scaleZ.text = reader.ReadString();
            
            _offsetX.text = reader.ReadString();
            _offsetY.text = reader.ReadString();
            _offsetZ.text = reader.ReadString();
            
            _octaves.value = reader.ReadFloat();
            _lacunarity.value = reader.ReadFloat();
            _gain.value = reader.ReadFloat();
            _weightedStrength.value = reader.ReadFloat();
            
            _pingPongStrength.text = reader.ReadString();
        }
    }
}
