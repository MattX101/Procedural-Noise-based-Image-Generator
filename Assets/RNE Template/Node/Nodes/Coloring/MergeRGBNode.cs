using RNE.Template.Node.Pointer;
using RNE.Template.Node.Pointer.Value;
using Utils.Colors.Coloring;
using UnityEngine;
using UnityEngine.UI;

namespace RNE.Template.Node
{
    public class MergeRGBNode : RuntimeNodeEditor.Node.Node
    {
        [Space]

        [SerializeField] private RawImage _redImage;
        [SerializeField] private RawImage _greenImage;
        [SerializeField] private RawImage _blueImage;
        [SerializeField] private RawImage _resultImage;

        [Space]

        [SerializeField]
        private ComputeShader _shader;

        private RenderTexture _redRender;
        private RenderTexture _greenRender;
        private RenderTexture _blueRender;
        private RenderTexture _resultRender;

        protected override void CodeToExecute()
        {
            if (!Inputs[0].ConnectedOutputPointer)
            {
                return;
            }

            ExecuteInputConnection(0);
            ExecuteInputConnection(1);
            ExecuteInputConnection(2);

            ComputeBuffer redBuffer = PointerValue.GetNoise(Inputs[0]);
            ComputeBuffer greenBuffer = PointerValue.GetNoise(Inputs[1]);
            ComputeBuffer blueBuffer = PointerValue.GetNoise(Inputs[2]);
            ComputeBuffer resultBuffer = new ComputeBuffer(ProjectData.Length, sizeof(float) * 4);

            int kernel = _shader.FindKernel("Merge");
            
            _shader.SetBuffer(kernel, "red", redBuffer);
            _shader.SetBuffer(kernel, "green", greenBuffer);
            _shader.SetBuffer(kernel, "blue", blueBuffer);
            _shader.SetBuffer(kernel, "result", resultBuffer);

            _shader.Dispatch(kernel, Mathf.CeilToInt(ProjectData.Length / 1024.0f), 1, 1);

            ComputeBuffer previewBuffer = new ComputeBuffer(ProjectData.Length, sizeof(float) * 4);
            Coloring.ColoringGPU(ref previewBuffer, redBuffer, Color.white);
            SetPreview(ref _redRender, _redImage, previewBuffer);

            Coloring.ColoringGPU(ref previewBuffer, greenBuffer, Color.white);
            SetPreview(ref _greenRender, _greenImage, previewBuffer);

            Coloring.ColoringGPU(ref previewBuffer, blueBuffer, Color.white);
            SetPreview(ref _blueRender, _blueImage, previewBuffer);

            SetPreview(ref _resultRender, _resultImage, resultBuffer);
            
            previewBuffer.Release();

            Outputs[0].GetComponent<TextureOutputPointer>().Buffer = resultBuffer;
        }

        protected override void CodeToReset()
        {
            Outputs[0].GetComponent<TextureOutputPointer>().Reset();
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

            if (_resultRender != null)
            {
                _resultRender.Release();
            }
        }
    }
}
