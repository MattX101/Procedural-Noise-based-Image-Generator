using UnityEditor;
using UnityEngine;

namespace RuntimeNodeEditor.Functions.Exit
{
    public class ExitApp : MonoBehaviour
    {
        public void Exit()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }
    }
}
