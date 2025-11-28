using RuntimeNodeEditor.Node.UI.Functions;
using RNE.Template.Node.Pointer;
using RNE.Template.Node.Pointer.Value;
using Utils.IO.Serialization;
using UnityEngine;
using TMPro;

namespace RNE.Template.Node
{
    public class CurrentFrameTimeNode : RuntimeNodeEditor.Node.Node
    {
        [Space]
        
        [SerializeField]
        private TMP_InputField _currentFrameInputfield;
        [SerializeField]
        private TMP_InputField _targetFramerateInputfield;

        [Space]
        
        [SerializeField]
        private TMP_InputField _targetFrameTimeInputfield;
        
        protected override void CodeToExecute()
        {
            ExecuteInputConnection(0);
            ExecuteInputConnection(1);

            int currentFrame = Inputs[0].ConnectedOutputPointer ? PointerValue.GetInt(Inputs[0]) : InputFieldToInt.Get(_currentFrameInputfield.text);
            float targetFramerate = Inputs[1].ConnectedOutputPointer ? PointerValue.GetFloat(Inputs[1]) : InputFieldToFloat.Get(_targetFramerateInputfield.text);

            float currentFrameTime = (float)currentFrame * targetFramerate;

            Outputs[0].GetComponent<FloatOutputPointer>().Value = currentFrameTime;

            _currentFrameInputfield.text = currentFrame.ToString();
            _targetFramerateInputfield.text = targetFramerate.ToString();
            _targetFrameTimeInputfield.text = currentFrameTime.ToString();
        }

        protected override void CodeToReset()
        {
            Outputs[0].GetComponent<FloatOutputPointer>().Reset();
        }

        public override void OnSave(FileWriter writer)
        {
            writer.Write(_currentFrameInputfield.text);
            writer.Write(_targetFramerateInputfield.text);
        }

        public override void OnLoad(FileReader reader)
        {
            _currentFrameInputfield.text = reader.ReadString();
            _targetFramerateInputfield.text = reader.ReadString();
        }
    }
}
