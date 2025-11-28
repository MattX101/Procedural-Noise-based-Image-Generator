using RNE.Template.Node.Pointer;
using RNE.Template.Node.Pointer.Value;
using Utils.Colors.Coloring;
using UnityEngine;
using UnityEngine.UI;

namespace RNE.Template.Node
{
    public class SplitRGBNode : RuntimeNodeEditor.Node.Node
    {
        [Space]

        [SerializeField] private RawImage _sourceImage;
        [SerializeField] private RawImage _redImage;
        [SerializeField] private RawImage _greenImage;
        [SerializeField] private RawImage _blueImage;

        [Space]

        [SerializeField]
        private ComputeShader _shader;

        private RenderTexture _sourceRender;
        private RenderTexture _redRender;
        private RenderTexture _greenRender;
        private RenderTexture _blueRender;

        protected override void CodeToExecute()
        {
            if (!Inputs[0].ConnectedOutputPointer)
            {
                return;
            }

            ExecuteInputConnection(0);

            ComputeBuffer sourceBuffer = PointerValue.GetTexture(Inputs[0]);
            ComputeBuffer redBuffer = new ComputeBuffer(ProjectData.Length, sizeof(float));
            ComputeBuffer greenBuffer = new ComputeBuffer(ProjectData.Length, sizeof(float));
            ComputeBuffer blueBuffer = new ComputeBuffer(ProjectData.Length, sizeof(float));

            int kernel = _shader.FindKernel("Split");
            
            _shader.SetBuffer(kernel, "source", sourceBuffer);
            _shader.SetBuffer(kernel, "red", redBuffer);
            _shader.SetBuffer(kernel, "green", greenBuffer);
            _shader.SetBuffer(kernel, "blue", blueBuffer);

            _shader.Dispatch(kernel, Mathf.CeilToInt(ProjectData.Length / 1024.0f), 1, 1);

            SetPreview(ref _sourceRender, _sourceImage, sourceBuffer);

            ComputeBuffer previewBuffer = new ComputeBuffer(ProjectData.Length, sizeof(float) * 4);
            Coloring.ColoringGPU(ref previewBuffer, redBuffer, Color.white);
            SetPreview(ref _redRender, _redImage, previewBuffer);

            Coloring.ColoringGPU(ref previewBuffer, greenBuffer, Color.white);
            SetPreview(ref _greenRender, _greenImage, previewBuffer);

            Coloring.ColoringGPU(ref previewBuffer, blueBuffer, Color.white);
            SetPreview(ref _blueRender, _blueImage, previewBuffer);
            
            previewBuffer.Release();

            Outputs[0].GetComponent<NoiseOutputPointer>().Buffer = redBuffer;
            Outputs[1].GetComponent<NoiseOutputPointer>().Buffer = greenBuffer;
            Outputs[2].GetComponent<NoiseOutputPointer>().Buffer = blueBuffer;
        }

        protected override void CodeToReset()
        {
            Outputs[0].GetComponent<NoiseOutputPointer>().Reset();
            Outputs[1].GetComponent<NoiseOutputPointer>().Reset();
            Outputs[2].GetComponent<NoiseOutputPointer>().Reset();
        }

        protected void SetPreview(ref RenderTexture render, RawImage image, ComputeBuffer buffer)
        {
            render = new RenderTexture(ProjectData.Resolution, ProjectData.Resolution, 0, RenderTextureFormat.ARGB32);
            render.wrapMode = TextureWrapMode.Clamp;
            render.filterMode = FilterMode.Point;
            render.enableRandomWrite = true;
            render.Create();

            RenderTexture(render, buffer);

            image.texture = render;
        }

        private void RenderTexture(RenderTexture render, ComputeBuffer buffer)
        {
            int kernel = ProjectData.Shader.FindKernel("RenderTexture");
            ProjectData.Shader.SetBuffer(kernel, "colors", buffer);
            ProjectData.Shader.SetTexture(kernel, "render", render);
            ProjectData.Shader.SetInt("resX", ProjectData.Resolution);
            ProjectData.Shader.SetInt("resY", ProjectData.Resolution);
            ProjectData.Shader.Dispatch(
                kernel,
                Mathf.CeilToInt((float)ProjectData.Resolution / 32.0f),
                Mathf.CeilToInt((float)ProjectData.Resolution / 32.0f),
                1);
        }

        private void OnDestroy()
        {
            if (_sourceRender != null)
            {
                _sourceRender.Release();
            }

            if (_redRender != null)
            {
                _redRender.Release();
            }

            if (_greenRender != null)
            {
                _greenRender.Release();
            }

            if (_blueRender != null)
            {
                _blueRender.Release();
            }
        }
    }
}
