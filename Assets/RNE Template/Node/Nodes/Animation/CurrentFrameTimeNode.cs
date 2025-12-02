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

        private int _currentFrame = 0;
        private float _targetFramerate, _currentFrameTime = 0.0f;

        protected override void CodeToExecute()
        {
            ExecuteInputConnection(0);
            ExecuteInputConnection(1);

            _currentFrame = Inputs[0].ConnectedOutputPointer ? PointerValue.GetInt(Inputs[0]) : InputFieldToInt.Get(_currentFrameInputfield.text);
            _targetFramerate = Inputs[1].ConnectedOutputPointer ? PointerValue.GetFloat(Inputs[1]) : InputFieldToFloat.Get(_targetFramerateInputfield.text);

            _currentFrameTime = _currentFrame * _targetFramerate;

            Outputs[0].GetComponent<FloatOutputPointer>().Value = _currentFrameTime;

            _currentFrameInputfield.text = _currentFrame.ToString();
            _targetFramerateInputfield.text = _targetFramerate.ToString();
            _targetFrameTimeInputfield.text = _currentFrameTime.ToString();
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
