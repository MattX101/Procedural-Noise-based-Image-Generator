using UnityEngine;

namespace RNE.Template
{
    public static class ProjectData
    {
        public static ComputeShader Shader;

        public static bool Export = false;
        public static string ExportPath = null;

        // Current
        public static bool inBuildMode = false;

        public static int Resolution => inBuildMode ? ExportResolution : PreviewResolution;
        public static int Length => inBuildMode ? ExportLength : PreviewLength;

        // Preview
        private static int _previewResolution = 128;
        private static int _previewLength = 128 * 128;

        internal static int PreviewResolution
        {
            get
            {
                return _previewResolution;
            }
            set
            {
                _previewResolution = value;
                _previewLength = _previewResolution * _previewResolution;
            }
        }

        internal static int PreviewLength => _previewLength;

        // Export
        private static int _exportResolution = 1024;
        private static int _exportLength = 1024 * 1024;

        internal static int ExportResolution
        {
            get
            {
                return _exportResolution;
            }
            set
            {
                _exportResolution = value;
                _exportLength = _exportResolution * _exportResolution;
            }
        }

        internal static int ExportLength => _exportLength;
    }
}
