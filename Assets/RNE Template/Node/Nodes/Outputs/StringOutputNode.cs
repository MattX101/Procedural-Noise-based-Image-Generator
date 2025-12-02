using RNE.Template.Node.Pointer.Value;
using UnityEngine;
using TMPro;

namespace RNE.Template.Node
{
    public class StringOutputNode : RuntimeNodeEditor.Node.Node
    {
        [SerializeField] 
        private TMP_InputField _inputfield;
        
        protected override void CodeToExecute()
        {
            ExecuteInputConnection(0);
            _inputfield.text = PointerValue.GetString(Inputs[0]);
        }
    }
}
