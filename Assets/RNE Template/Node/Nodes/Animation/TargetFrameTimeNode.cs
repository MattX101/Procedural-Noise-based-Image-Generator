using RuntimeNodeEditor.Node.UI.Functions;
using RNE.Template.Node.Pointer;
using RNE.Template.Node.Pointer.Value;
using Utils.IO.Serialization;
using UnityEngine;
using TMPro;

namespace RNE.Template.Node
{
    public class TargetFrameTimeNode : RuntimeNodeEditor.Node.Node
    {
        [Space]
        
        [SerializeField]
        private TMP_InputField _framerateInputfield;

        [Space]

        [SerializeField]
        private TMP_InputField _frametimeInputfield;
        
        protected override void CodeToExecute()
        {
            ExecuteInputConnection(0);

            int framerate = Inputs[0].ConnectedOutputPointer ? PointerValue.GetInt(Inputs[0]) : InputFieldToInt.Get(_framerateInputfield.text);
            framerate = framerate <= 0 ? 30 : framerate;
            float frametime = 1.0f / (float)framerate;

            Outputs[0].GetComponent<FloatOutputPointer>().Value = frametime;

            _framerateInputfield.text = framerate.ToString();
            _frametimeInputfield.text = frametime.ToString();
        }

        protected override void CodeToReset()
        {
            Outputs[0].GetComponent<FloatOutputPointer>().Reset();
        }

        public override void OnSave(FileWriter writer)
        {
            writer.Write(_framerateInputfield.text);
        }

        public override void OnLoad(FileReader reader)
        {
            _framerateInputfield.text = reader.ReadString();
        }
    }
}
