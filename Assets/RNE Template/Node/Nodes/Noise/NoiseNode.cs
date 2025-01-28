using RNE.Template.Node.Pointer;
using RNE.Template.Node.Pointer.Value;
using RNE.Template.Node.Seed;
using Utils.Noise;
using Utils.Noise.Profiles;
using Utils.Colors.Coloring;
using UnityEngine;

namespace RNE.Template.Node
{
    public class NoiseNode : NodeWithPreview
    {
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

            switch (Elements.dropdowns[0].value)
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

            switch (Elements.dropdowns[1].value)
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
                Elements.SetInputField(Elements.inputFields[0], PointerValue.GetInt(Inputs[0]).ToString());
            }
            else
            {
                noiseProfile.seed = int.Parse(Elements.inputFields[0].text);
            }
            noiseProfile.seed += SeedData.Seed;

            // Universal Scale
            if (Inputs[1].ConnectedOutputPointer)
            {
                PointerValue.GetFloat(Inputs[1], ref noiseProfile.universalScale);
                Elements.SetInputField(Elements.inputFields[1], PointerValue.GetFloat(Inputs[1]).ToString());
            }
            else
            {
                noiseProfile.universalScale = float.Parse(Elements.inputFields[1].text);
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
                PointerValue.GetVector3(Inputs[3], ref noiseProfile.scale);

                Elements.SetInputField(Elements.inputFields[2], noiseProfile.scale.x.ToString());
                Elements.SetInputField(Elements.inputFields[3], noiseProfile.scale.y.ToString());
                Elements.SetInputField(Elements.inputFields[4], noiseProfile.scale.z.ToString());
            }
            else
            {
                noiseProfile.scale.x = float.Parse(Elements.inputFields[2].text);
                noiseProfile.scale.y = float.Parse(Elements.inputFields[3].text);
                noiseProfile.scale.z = float.Parse(Elements.inputFields[4].text);
            }

            // Offset
            if (Inputs[4].ConnectedOutputPointer)
            {
                PointerValue.GetVector3(Inputs[4], ref noiseProfile.offset);

                Elements.SetInputField(Elements.inputFields[5], noiseProfile.offset.x.ToString());
                Elements.SetInputField(Elements.inputFields[6], noiseProfile.offset.y.ToString());
                Elements.SetInputField(Elements.inputFields[7], noiseProfile.offset.z.ToString());
            }
            else
            {
                noiseProfile.offset.x = float.Parse(Elements.inputFields[5].text);
                noiseProfile.offset.y = float.Parse(Elements.inputFields[6].text);
                noiseProfile.offset.z = float.Parse(Elements.inputFields[7].text);
            }

            // Octaves
            if (Inputs[5].ConnectedOutputPointer)
            {
                PointerValue.GetInt(Inputs[5], ref noiseProfile.octaves);
                Elements.SetSlider(Elements.sliders[0], PointerValue.GetInt(Inputs[5]));
            }
            else
            {
                noiseProfile.octaves = (int)Elements.sliders[0].value;
            }

            // Lacunarity
            if (Inputs[6].ConnectedOutputPointer)
            {
                PointerValue.GetFloat(Inputs[6], ref noiseProfile.lacunarity);
                Elements.SetSlider(Elements.sliders[1], PointerValue.GetFloat(Inputs[6]));
            }
            else
            {
                noiseProfile.lacunarity = Elements.sliders[1].value;
            }

            // Gain
            if (Inputs[7].ConnectedOutputPointer)
            {
                PointerValue.GetFloat(Inputs[7], ref noiseProfile.gain);
                Elements.SetSlider(Elements.sliders[2], PointerValue.GetFloat(Inputs[7]));
            }
            else
            {
                noiseProfile.gain = Elements.sliders[2].value;
            }

            // Weighted Stregth
            if (Inputs[8].ConnectedOutputPointer)
            {
                PointerValue.GetFloat(Inputs[8], ref noiseProfile.weightedStrength);
                Elements.SetSlider(Elements.sliders[3], PointerValue.GetFloat(Inputs[8]));
            }
            else
            {
                noiseProfile.weightedStrength = Elements.sliders[3].value;
            }

            // Ping Pong
            if (Inputs[9].ConnectedOutputPointer)
            {
                PointerValue.GetFloat(Inputs[9], ref noiseProfile.pingPongStrength);
                Elements.SetInputField(Elements.inputFields[8], PointerValue.GetFloat(Inputs[9]).ToString());
            }
            else
            {
                noiseProfile.pingPongStrength = float.Parse(Elements.inputFields[8].text);
            }

            // Cellular Profile
            if (Inputs[10].ConnectedOutputPointer)
            {
                switch (Inputs[10].ConnectedOutputPointer.Node.Elements.dropdowns[0].value)
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

                switch (Inputs[10].ConnectedOutputPointer.Node.Elements.dropdowns[1].value)
                {
                    case 0: noiseProfile.SetCellularDistanceFunction_Euclidean(); break;
                    case 1: noiseProfile.SetCellularDistanceFunction_EuclideanSq(); break;
                    case 2: noiseProfile.SetCellularDistanceFunction_Manhattan(); break;
                    case 3: noiseProfile.SetCellularDistanceFunction_Hybrid(); break;
                    default: noiseProfile.SetCellularDistanceFunction_Euclidean(); break;
                }

                noiseProfile.jitter = Inputs[10].ConnectedOutputPointer.Node.Elements.sliders[0].value;
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
            FastNoise2DGPU.GenerateNoise(ref noiseBuffer, ProjectData.Resolution, ProjectData.Resolution, is3D, noiseProfile, warpProfile);
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
    }
}
