using RNE.Template.Node.Pointer;
using UnityEngine;
using TMPro;

namespace RNE.Template.Node
{
    public class CurrentFrameNode : RuntimeNodeEditor.Node.Node
    {
        [Space]

        [SerializeField]
        private TMP_InputField _inputfield;
        
        protected override void CodeToExecute()
        {
            Outputs[0].GetComponent<IntOutputPointer>().Value = ProjectData.CurrentFrame;
            _inputfield.text = ProjectData.CurrentFrame.ToString();
        }

        protected override void CodeToReset()
        {
            Outputs[0].GetComponent<IntOutputPointer>().Reset();
        }
    }
}
