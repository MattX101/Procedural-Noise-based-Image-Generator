using RuntimeNodeEditor.UI.Tooltip.Window;
using UnityEngine;

namespace RNE.Template.UI
{
    public class SettingsWindows : MonoBehaviour
    {
        [SerializeField]
        private Window _projectSettingsWindow;

        public void ToggleProjectSettings()
        {
            _projectSettingsWindow.Create();
        }
    }
}
