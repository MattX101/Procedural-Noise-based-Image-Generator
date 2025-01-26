using RuntimeNodeEditor.Node;
using UnityEngine;

namespace RNE.Template
{
    public class ImageExecution : NodeExecution
    {
        public void ExecutePreview()
        {
            Debug.Log("Preview Execution");

            ProjectData.inBuildMode = false;

            Execute();
        }

        public void ExecuteBuild()
        {
            Debug.Log("Build Execution");

            ProjectData.inBuildMode = true;

            Execute();

            ProjectData.inBuildMode = false;
        }

        public void ExecuteExport()
        {
            Debug.Log("Export Execution");

            ProjectData.Export = true;
            ProjectData.inBuildMode = true;

            Execute();

            ProjectData.Export = false;
            ProjectData.inBuildMode = false;
        }
    }
}
