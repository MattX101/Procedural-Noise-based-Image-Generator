using UnityEditor;

#if UNITY_EDITOR
namespace RNE.Template.Editor
{
    internal partial class UIPrefabMenu
    {
        private const string NodesPath = "Assets/RNE Template/Assets/Prefabs/Nodes/";

        [MenuItem(GameObjectPath + "Nodes/Boolean Node", false, 0), MenuItem(AssetsPath + "Nodes/Boolean Node", false, 0)]
        private static void CreateNode() => CreateUIPrefab(NodesPath + "Logic Gates/Boolean Node");
    }
}
#endif
