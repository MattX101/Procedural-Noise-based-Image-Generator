using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace RNE.Template.Editor
{
    internal partial class UIPrefabMenu
    {
        private const string GameObjectPath = "GameObject/RNE Template/";
        private const string AssetsPath = "Assets/Create/RNE Template/";

        private static GameObject _prefab;
        private static GameObject _instance;

        private static void CreateUIPrefab(string path)
        {
            _prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path + ".prefab");

            if (!_prefab)
            {
                Debug.LogError("UI Prefab not found at the specified path.");

                return;
            }

            _instance = (GameObject)PrefabUtility.InstantiatePrefab(_prefab);

            if (Selection.activeGameObject)
            {
                _instance.transform.parent = Selection.activeGameObject.transform;
            }

            _instance.transform.localPosition = Vector3.zero;
            _instance.transform.localScale = Vector3.one;

            Selection.activeGameObject = _instance;
        }
    }
}
#endif
