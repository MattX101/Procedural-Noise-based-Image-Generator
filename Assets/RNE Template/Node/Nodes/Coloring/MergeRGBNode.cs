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

        [SerializeField]
        private RawImage _redImage, _greenImage, _blueImage, _resultImage;

        [Space]

        [SerializeField]
        private ComputeShader _shader;

        private int _shaderKernel = 0;
        private ComputeBuffer _redBuffer, _greenBuffer, _blueBuffer, _resultBuffer;
        private ComputeBuffer _previewBuffer;

        private int _renderKernel = 0;
        private RenderTexture _redRender, _greenRender, _blueRender, _resultRender;

        protected override void Init()
        {
            _resultBuffer = new ComputeBuffer(ProjectData.Length, sizeof(float) * 4);
            _previewBuffer = new ComputeBuffer(ProjectData.Length, sizeof(float) * 4);

            CreateRenderTexture(ref _redRender);
            CreateRenderTexture(ref _greenRender);
            CreateRenderTexture(ref _blueRender);
            CreateRenderTexture(ref _resultRender);

            _shaderKernel = _shader.FindKernel("Merge");
            _renderKernel = ProjectData.Shader.FindKernel("RenderTexture");
        }

        protected override void CodeToExecute()
        {
            if (!Inputs[0].ConnectedOutputPointer)
            {
                return;
            }

            ExecuteInputConnection(0);
            ExecuteInputConnection(1);
            ExecuteInputConnection(2);

            if (_resultBuffer.count != ProjectData.Length)
            {
                Init();
            }

            _redBuffer = PointerValue.GetNoise(Inputs[0]);
            _greenBuffer = PointerValue.GetNoise(Inputs[1]);
            _blueBuffer = PointerValue.GetNoise(Inputs[2]);

            _shader.SetBuffer(_shaderKernel, "red", _redBuffer);
            _shader.SetBuffer(_shaderKernel, "green", _greenBuffer);
            _shader.SetBuffer(_shaderKernel, "blue", _blueBuffer);
            _shader.SetBuffer(_shaderKernel, "result", _resultBuffer);
            _shader.Dispatch(_shaderKernel, Mathf.CeilToInt(ProjectData.Length / 1024.0f), 1, 1);

            Coloring.ColoringGPU(ref _previewBuffer, _redBuffer, Color.white);
            SetPreview(ref _redRender, _redImage, _previewBuffer);

            Coloring.ColoringGPU(ref _previewBuffer, _greenBuffer, Color.white);
            SetPreview(ref _greenRender, _greenImage, _previewBuffer);

            Coloring.ColoringGPU(ref _previewBuffer, _blueBuffer, Color.white);
            SetPreview(ref _blueRender, _blueImage, _previewBuffer);

            SetPreview(ref _resultRender, _resultImage, _resultBuffer);

            Outputs[0].GetComponent<TextureOutputPointer>().Buffer = _resultBuffer;
        }

        protected override void CodeToReset()
        {
            Outputs[0].GetComponent<TextureOutputPointer>().Reset();
        }

        private void SetPreview(ref RenderTexture render, RawImage image, ComputeBuffer buffer)
        {
            RenderTexture(render, buffer);
            image.texture = render;
        }

        private void CreateRenderTexture(ref RenderTexture render)
        {
            render = new RenderTexture(ProjectData.Resolution, ProjectData.Resolution, 0, RenderTextureFormat.ARGB32);
            render.wrapMode = TextureWrapMode.Clamp;
            render.filterMode = FilterMode.Point;
            render.enableRandomWrite = true;
            render.Create();
        }

        private void RenderTexture(RenderTexture render, ComputeBuffer buffer)
        {
            ProjectData.Shader.SetBuffer(_renderKernel, "colors", buffer);
            ProjectData.Shader.SetTexture(_renderKernel, "render", render);
            ProjectData.Shader.SetInt("resX", ProjectData.Resolution);
            ProjectData.Shader.SetInt("resY", ProjectData.Resolution);
            ProjectData.Shader.Dispatch(
                _renderKernel,
                Mathf.CeilToInt((float)ProjectData.Resolution / 32.0f),
                Mathf.CeilToInt((float)ProjectData.Resolution / 32.0f),
                1);
        }

        private void OnDestroy()
        {
            _redBuffer.Release();
            _greenBuffer.Release();
            _blueBuffer.Release();
            _resultRender.Release();

            _previewBuffer.Release();

            _redRender.Release();
            _greenRender.Release();
            _blueRender.Release();
            _resultRender.Release();
        }
    }
}
