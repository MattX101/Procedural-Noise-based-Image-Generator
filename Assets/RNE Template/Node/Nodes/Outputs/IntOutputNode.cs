using RNE.Template.Node.Pointer.Value;
using TMPro;
using UnityEngine;

namespace RNE.Template.Node
{
    public class IntOutputNode : RuntimeNodeEditor.Node.Node
    {
        [SerializeField] 
        private TMP_InputField _inputfield;
        
        protected override void CodeToExecute()
        {
            ExecuteInputConnection(0);
            _inputfield.text = PointerValue.GetInt(Inputs[0]).ToString();
        }
    }
}
