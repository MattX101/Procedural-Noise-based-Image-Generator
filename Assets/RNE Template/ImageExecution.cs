using RuntimeNodeEditor.Node;
using UnityEngine;
using UnityEngine.UI;

namespace RNE.Template
{
    public class ImageExecution : NodeExecution
    {
        [SerializeField]
        private ComputeShader _shader;

        private bool _animate = false;

        [Space]

        [SerializeField] 
        private Image _animationButton;

        [SerializeField]
        private Sprite _playSprite;
        [SerializeField]
        private Sprite _pauseSprite;

        private enum Mode
        {
            Preview,
            Build,
            Export
        }
        private Mode _mode = Mode.Preview;

        private void Awake()
        {
            ProjectData.Shader = _shader;
            _animationButton.sprite = _animate ? _playSprite : _pauseSprite;
        }

        private void Update()
        {
            if (_animate)
            {
                switch (_mode)
                {
                    case Mode.Preview:
                        ExecutePreview();
                        break;
                    case Mode.Build:
                        ExecuteBuild();
                        break;
                    case Mode.Export:
                        ExecuteExport();
                        break;
                }

                ProjectData.IncrementFrame();
            }
        }

        public void ExecutePreview()
        {
            Debug.Log("Preview Execution");

            _mode = Mode.Preview;

            ProjectData.inBuildMode = false;

            Execute();
        }

        public void ExecuteBuild()
        {
            Debug.Log("Build Execution");

            _mode = Mode.Build;

            ProjectData.inBuildMode = true;

            Execute();

            ProjectData.inBuildMode = false;
        }

        public void ExecuteExport()
        {
            Debug.Log("Export Execution");

            _mode = Mode.Export;

            ProjectData.Export = true;
            ProjectData.inBuildMode = true;

            Execute();

            ProjectData.Export = false;
            ProjectData.inBuildMode = false;
        }

        public void ToggleAnimation()
        {
            _animate = !_animate;
            ProjectData.ResetFrameCounter();

            _animationButton.sprite = _animate ? _playSprite : _pauseSprite;
        }
    }
}
