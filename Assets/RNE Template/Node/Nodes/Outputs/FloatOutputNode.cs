using RNE.Template.Node.Pointer.Value;
using TMPro;
using UnityEngine;

namespace RNE.Template.Node
{
    public class FloatOutputNode : RuntimeNodeEditor.Node.Node
    {
        [SerializeField] 
        private TMP_InputField _inputfield;
        
        protected override void CodeToExecute()
        {
            ExecuteInputConnection(0);
            _inputfield.text = PointerValue.GetFloat(Inputs[0]).ToString();
        }
    }
}
