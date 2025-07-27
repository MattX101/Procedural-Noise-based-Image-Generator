using RNE.Template.Node.Pointer.Value;
using UnityEngine;
using UnityEngine.UI;

namespace RNE.Template.Node
{
    public class BoolOutputNode : RuntimeNodeEditor.Node.Node
    {
        [SerializeField]
        private Toggle _toggle;

        protected override void CodeToExecute()
        {
            ExecuteInputConnection(0);
            _toggle.isOn = PointerValue.GetBool(Inputs[0]);
        }
    }
}
