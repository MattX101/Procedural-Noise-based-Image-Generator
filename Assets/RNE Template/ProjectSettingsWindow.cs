using RuntimeNodeEditor.Node;
using RuntimeNodeEditor.UI.Tooltip.Window;
using Utils.IO;
using UnityEngine;
using TMPro;
using System.IO;

namespace RNE.Template.UI
{
    public class ProjectSettingsWindow : Window
    {
        [Space]

        [SerializeField] private TMP_Text _previewResText;
        [SerializeField] private TMP_Text _exportResText;

        [Space]

        [SerializeField] private TMP_InputField _previewResInput;
        [SerializeField] private TMP_InputField _exportResInput;

        [Space]

        private NodeExecution _nodeExecution;

        [Space]

        [SerializeField] private TMP_Text _exportPath;

        private readonly IOSelection _ioSelection = new IOSelection();

        private void Awake()
        {
            _nodeExecution = FindObjectOfType<NodeExecution>();
        }

        private void Start()
        {
            _previewResText.text = ProjectData.PreviewResolution.ToString();
            _exportResText.text = ProjectData.ExportResolution.ToString();

            _exportPath.text = ProjectData.ExportPath;
        }

        public void SetResolutions()
        {
            if (_previewResInput.text != null && _previewResInput.text.Length > 0)
            {
                ProjectData.PreviewResolution = int.Parse(_previewResInput.text);
            }

            if (_exportResInput.text != null && _exportResInput.text.Length > 0)
            {
                ProjectData.ExportResolution = int.Parse(_exportResInput.text);
            }

            _nodeExecution.Execute();
        }

        public void LocateExportPath()
        {
            string path = _ioSelection.SelectFolder();

            if (path != null && path.Length > 0)
            {
                ProjectData.ExportPath = path;
                _exportPath.text = path;
            }
        }
    }
}
