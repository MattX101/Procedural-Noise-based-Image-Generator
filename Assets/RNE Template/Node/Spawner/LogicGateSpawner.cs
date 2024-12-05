using RuntimeNodeEditor.UI.Canvas.Node.Factory;
using UnityEngine;

namespace RNE.Template.Node.Spawner
{
    public class LogicGateSpawner : MonoBehaviour
    {
        [SerializeField]
        private FactoryManager _manager;

        [SerializeField]
        private GameObject _node;

        public void Spawn(int value)
        {
            LogicGateNode logic = _manager.ReturnSpawn(_node).GetComponent<LogicGateNode>();
            logic.Elements.SetDropdown(logic.Elements.dropdowns[0], value);
        }
    }
}
