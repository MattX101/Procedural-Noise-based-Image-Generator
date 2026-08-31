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

        [SerializeField]
        private RawImage _sourceImage, _redImage, _greenImage, _blueImage;

        [Space]

        [SerializeField]
        private ComputeShader _shader;

        private int _shaderKernel = 0;
        private ComputeBuffer _sourceBuffer, _redBuffer, _greenBuffer, _blueBuffer;
        private ComputeBuffer _previewBuffer;

        private int _renderKernel = 0;
        private RenderTexture _sourceRender, _redRender, _greenRender, _blueRender;

        protected override void Init()
        {
            _redBuffer = new ComputeBuffer(ProjectData.Length, sizeof(float));
            _greenBuffer = new ComputeBuffer(ProjectData.Length, sizeof(float));
            _blueBuffer = new ComputeBuffer(ProjectData.Length, sizeof(float));

            _previewBuffer = new ComputeBuffer(ProjectData.Length, sizeof(float) * 4);

            CreateRenderTexture(ref _sourceRender);
            CreateRenderTexture(ref _redRender);
            CreateRenderTexture(ref _greenRender);
            CreateRenderTexture(ref _blueRender);

            _shaderKernel = _shader.FindKernel("Split");
            _renderKernel = ProjectData.Shader.FindKernel("RenderTexture");
        }

        protected override void CodeToExecute()
        {
            if (!Inputs[0].ConnectedOutputPointer)
            {
                return;
            }

            ExecuteInputConnection(0);
            
            Init();

            _sourceBuffer = PointerValue.GetTexture(Inputs[0]);

            _shader.SetBuffer(_shaderKernel, "source", _sourceBuffer);
            _shader.SetBuffer(_shaderKernel, "red", _redBuffer);
            _shader.SetBuffer(_shaderKernel, "green", _greenBuffer);
            _shader.SetBuffer(_shaderKernel, "blue", _blueBuffer);
            _shader.Dispatch(_shaderKernel, Mathf.CeilToInt(ProjectData.Length / 1024.0f), 1, 1);

            SetPreview(ref _sourceRender, _sourceImage, _sourceBuffer);

            Coloring.Color(ref _previewBuffer, _redBuffer, Color.white);
            SetPreview(ref _redRender, _redImage, _previewBuffer);

            Coloring.Color(ref _previewBuffer, _greenBuffer, Color.white);
            SetPreview(ref _greenRender, _greenImage, _previewBuffer);

            Coloring.Color(ref _previewBuffer, _blueBuffer, Color.white);
            SetPreview(ref _blueRender, _blueImage, _previewBuffer);

            Outputs[0].GetComponent<NoiseOutputPointer>().Buffer = _redBuffer;
            Outputs[1].GetComponent<NoiseOutputPointer>().Buffer = _greenBuffer;
            Outputs[2].GetComponent<NoiseOutputPointer>().Buffer = _blueBuffer;
        }

        protected override void CodeToReset()
        {
            Outputs[0].GetComponent<NoiseOutputPointer>().Reset();
            Outputs[1].GetComponent<NoiseOutputPointer>().Reset();
            Outputs[2].GetComponent<NoiseOutputPointer>().Reset();
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
            _sourceBuffer.Release();
            _redBuffer.Release();
            _greenBuffer.Release();
            _blueBuffer.Release();

            _previewBuffer.Release();

            _sourceRender.Release();
            _redRender.Release();
            _greenRender.Release();
            _blueRender.Release();
        }
    }
}
