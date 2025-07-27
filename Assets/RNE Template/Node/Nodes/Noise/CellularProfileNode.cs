using RNE.Template.Node.Pointer.Value;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils.IO.Serialization;

namespace RNE.Template.Node
{
    public class CellularProfileNode : RuntimeNodeEditor.Node.Node
    {
        [SerializeField] private TMP_Dropdown _returnType;
        public TMP_Dropdown ReturnType => _returnType;
        
        [SerializeField] private TMP_Dropdown _distanceType;
        public TMP_Dropdown DistanceType => _distanceType;

        [Space]
        
        [SerializeField] private Slider _jitter;
        public Slider Jitter => _jitter;
        
        [Space]
        
        [SerializeField]
        private TMP_Text _text;
        
        protected override void CodeToExecute()
        {
            ExecuteInputConnection(0);
            if (Inputs[0].ConnectedOutputPointer)
            {
                _text.text = PointerValue.GetFloat(Inputs[0]).ToString();
            }
        }

        public override void OnSave(FileWriter writer)
        {
            writer.Write(_returnType.value);
            writer.Write(_distanceType.value);
            
            writer.Write(_jitter.value);
        }

        public override void OnLoad(FileReader reader)
        {
            _returnType.value = reader.ReadInt();
            _distanceType.value = reader.ReadInt();
            
            _jitter.value = reader.ReadFloat();
        }
    }
}
