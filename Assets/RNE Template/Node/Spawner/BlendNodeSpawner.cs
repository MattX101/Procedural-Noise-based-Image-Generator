using RuntimeNodeEditor.UI.Canvas.Node.Factory;
using UnityEngine;

namespace RNE.Template.Node.Spawner
{
    public class BlendNodeSpawner : MonoBehaviour
    {
        [SerializeField]
        private FactoryManager _manager;

        [SerializeField]
        private GameObject _node;

        public void Spawn(int value)
        {
            BlendNode blend = _manager.ReturnSpawn(_node).GetComponent<BlendNode>();
            blend.Elements.SetDropdown(blend.Elements.dropdowns[0], value);
        }
    }
}
