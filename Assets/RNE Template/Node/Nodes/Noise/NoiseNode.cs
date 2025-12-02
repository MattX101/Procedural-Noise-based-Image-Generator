using RNE.Template.Node.Pointer;
using RNE.Template.Node.Pointer.Value;
using RNE.Template.Node.Seed;
using Utils.Noise;
using Utils.Noise.Profiles;
using Utils.Colors.Coloring;
using Utils.IO.Serialization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

        private NoiseProfile _noiseProfile;
        private WarpProfile _warpProfile;

        private ComputeBuffer _noiseBuffer, _colorsBuffer;

        protected override void Init()
        {
            if (_noiseProfile == null)
            {
                _noiseProfile = new NoiseProfile();
                _warpProfile = null;
            }

            _noiseBuffer = new ComputeBuffer(ProjectData.Length, sizeof(float));
            _colorsBuffer = new ComputeBuffer(ProjectData.Length, sizeof(float) * 4);
        }

        protected override void CodeToExecute()
        {
            if (_noiseBuffer.count != ProjectData.Length)
            {
                Init();
            }

            for (int i = 0; i < Inputs.Count; i++)
            {
                ExecuteInputConnection(i);
            }

            switch (_noiseTypeDropdown.value)
            {
                case 0:
                    _noiseProfile.SetNoiseType_Perlin();
                    break;
                case 1:
                    _noiseProfile.SetNoiseType_OpenSimplex();
                    break;
                case 2:
                    _noiseProfile.SetNoiseType_OpenSimplexS();
                    break;
                case 3:
                    _noiseProfile.SetNoiseType_Value();
                    break;
                case 4:
                    _noiseProfile.SetNoiseType_ValueCubic();
                    break;
                case 5:
                    _noiseProfile.SetNoiseType_Cellular();
                    break;
                default:
                    _noiseProfile.SetNoiseType_Perlin();
                    break;
            }

            switch (_fractalTypeDropdown.value)
            {
                case 0:
                    _noiseProfile.SetFractalType_FBm();
                    break;
                case 1:
                    _noiseProfile.SetFractalType_Ridged();
                    break;
                case 2:
                    _noiseProfile.SetFractalType_PingPong();
                    break;
                default:
                    _noiseProfile.SetFractalType_FBm();
                    break;
            }
            ;

            // Seed
            if (Inputs[0].ConnectedOutputPointer)
            {
                PointerValue.GetInt(Inputs[0], ref _noiseProfile.seed);
                _seedField.text = PointerValue.GetInt(Inputs[0]).ToString();
            }
            else
            {
                if (_seedField.text.Length != 0)
                {
                    _noiseProfile.seed = int.Parse(_seedField.text);
                }
                else
                {
                    _seedField.text = "0";
                    _noiseProfile.seed = 0;
                }
            }
            _noiseProfile.seed += SeedData.Seed;

            // Universal Scale
            if (Inputs[1].ConnectedOutputPointer)
            {
                PointerValue.GetFloat(Inputs[1], ref _noiseProfile.universalScale);
                _unvirsalScale.text = PointerValue.GetFloat(Inputs[1]).ToString();
            }
            else
            {
                if (_unvirsalScale.text.Length != 0)
                {
                    _noiseProfile.universalScale = int.Parse(_unvirsalScale.text);
                }
                else
                {
                    _unvirsalScale.text = "1";
                    _noiseProfile.universalScale = 1;
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
                PointerValue.GetVector3(Inputs[3], ref _noiseProfile.scale);

                _scaleX.text = _noiseProfile.scale.x.ToString();
                _scaleY.text = _noiseProfile.scale.y.ToString();
                _scaleZ.text = _noiseProfile.scale.z.ToString();
            }
            else
            {
                if (_scaleX.text.Length != 0)
                {
                    _noiseProfile.scale.x = float.Parse(_scaleX.text);
                }
                else
                {
                    _scaleX.text = "1";
                    _noiseProfile.scale.x = 1;
                }

                if (_scaleY.text.Length != 0)
                {
                    _noiseProfile.scale.y = float.Parse(_scaleY.text);
                }
                else
                {
                    _scaleY.text = "1";
                    _noiseProfile.scale.y = 1;
                }

                if (_scaleZ.text.Length != 0)
                {
                    _noiseProfile.scale.z = float.Parse(_scaleZ.text);
                }
                else
                {
                    _scaleZ.text = "1";
                    _noiseProfile.scale.z = 1;
                }
            }

            // Offset
            if (Inputs[4].ConnectedOutputPointer)
            {
                PointerValue.GetVector3(Inputs[4], ref _noiseProfile.offset);

                _offsetX.text = _noiseProfile.offset.x.ToString();
                _offsetY.text = _noiseProfile.offset.y.ToString();
                _offsetZ.text = _noiseProfile.offset.z.ToString();
            }
            else
            {
                if (_offsetX.text.Length != 0)
                {
                    _noiseProfile.offset.x = float.Parse(_offsetX.text);
                }
                else
                {
                    _offsetX.text = "0";
                    _noiseProfile.offset.x = 0;
                }

                if (_offsetY.text.Length != 0)
                {
                    _noiseProfile.offset.y = float.Parse(_offsetY.text);
                }
                else
                {
                    _offsetY.text = "0";
                    _noiseProfile.offset.y = 0;
                }

                if (_offsetZ.text.Length != 0)
                {
                    _noiseProfile.offset.z = float.Parse(_offsetZ.text);
                }
                else
                {
                    _offsetZ.text = "0";
                    _noiseProfile.offset.z = 0;
                }
            }

            // Octaves
            if (Inputs[5].ConnectedOutputPointer)
            {
                PointerValue.GetInt(Inputs[5], ref _noiseProfile.octaves);
                _octaves.value = PointerValue.GetInt(Inputs[5]);
            }
            else
            {
                _noiseProfile.octaves = (int)_octaves.value;
            }

            // Lacunarity
            if (Inputs[6].ConnectedOutputPointer)
            {
                PointerValue.GetFloat(Inputs[6], ref _noiseProfile.lacunarity);
                _lacunarity.value = PointerValue.GetFloat(Inputs[6]);
            }
            else
            {
                _noiseProfile.lacunarity = _lacunarity.value;
            }

            // Gain
            if (Inputs[7].ConnectedOutputPointer)
            {
                PointerValue.GetFloat(Inputs[7], ref _noiseProfile.gain);
                _gain.value = PointerValue.GetFloat(Inputs[7]);
            }
            else
            {
                _noiseProfile.gain = _gain.value;
            }

            // Weighted Strength
            if (Inputs[8].ConnectedOutputPointer)
            {
                PointerValue.GetFloat(Inputs[8], ref _noiseProfile.weightedStrength);
                _weightedStrength.value = PointerValue.GetFloat(Inputs[8]);
            }
            else
            {
                _noiseProfile.weightedStrength = _weightedStrength.value;
            }

            // Ping Pong
            if (Inputs[9].ConnectedOutputPointer)
            {
                PointerValue.GetFloat(Inputs[9], ref _noiseProfile.pingPongStrength);
                _pingPongStrength.text = PointerValue.GetFloat(Inputs[9]).ToString();
            }
            else
            {
                if (_pingPongStrength.text.Length != 0)
                {
                    _noiseProfile.pingPongStrength = float.Parse(_pingPongStrength.text);
                }
                else
                {
                    _pingPongStrength.text = "1";
                    _noiseProfile.pingPongStrength = 1;
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
                            case 0: _noiseProfile.SetCellular_Cell(); break;
                            case 1: _noiseProfile.SetCellular_Distance(); break;
                            case 2: _noiseProfile.SetCellular_Distance2(); break;
                            case 3: _noiseProfile.SetCellular_Distance2Add(); break;
                            case 4: _noiseProfile.SetCellular_Distance2Sub(); break;
                            case 5: _noiseProfile.SetCellular_Distance2Mul(); break;
                            case 6: _noiseProfile.SetCellular_Distance2Div(); break;
                            default: _noiseProfile.SetCellular_Cell(); break;
                        }
                    }
                    else
                    {
                        _noiseProfile.SetCellular_Cell();
                    }

                    if (cellularProfile.DistanceType)
                    {
                        switch (cellularProfile.DistanceType.value)
                        {
                            case 0: _noiseProfile.SetCellularDistanceFunction_Euclidean(); break;
                            case 1: _noiseProfile.SetCellularDistanceFunction_EuclideanSq(); break;
                            case 2: _noiseProfile.SetCellularDistanceFunction_Manhattan(); break;
                            case 3: _noiseProfile.SetCellularDistanceFunction_Hybrid(); break;
                            default: _noiseProfile.SetCellularDistanceFunction_Euclidean(); break;
                        }
                    }
                    else
                    {
                        _noiseProfile.SetCellularDistanceFunction_Euclidean();
                    }

                    _noiseProfile.jitter = cellularProfile.Jitter ? cellularProfile.Jitter.value : 1.0f;
                }
                else
                {
                    _noiseProfile.SetCellular_Cell();
                    _noiseProfile.SetCellularDistanceFunction_Euclidean();

                    _noiseProfile.jitter = 1.0f;
                }
            }
            //

            _noiseProfile.warp = Inputs[11].ConnectedOutputPointer;
            _noiseProfile.Init();

            if (Inputs[11].ConnectedOutputPointer)
            {
                _warpProfile = PointerValue.GetWarpProfile(Inputs[11]);
                _warpProfile.Init();
            }

            FastNoise2DGPU.GenerateNoise(ref _noiseBuffer, ProjectData.Resolution, ProjectData.Resolution, _is3DToggle.isOn, _noiseProfile, _warpProfile);
            Coloring.ColoringGPU(ref _colorsBuffer, _noiseBuffer, Color.white);

            SetPreview(_colorsBuffer);

            Outputs[0].GetComponent<NoiseOutputPointer>().Buffer = _noiseBuffer;
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

        public void OnDestroy()
        {
            _noiseBuffer.Release();
            _colorsBuffer.Release();
        }
    }
}
