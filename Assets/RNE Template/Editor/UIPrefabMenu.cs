using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace RNE.Template.Editor
{
    internal partial class UIPrefabMenu
    {
        private const string GameObjectPath = "GameObject/RNE Template/";
        private const string AssetsPath = "Assets/Create/RNE Template/";

        private const string ElementsPath = "Assets/RNE Template/Assets/Prefabs/Elements/";

        [MenuItem(GameObjectPath + "Elements/Colour Picker", false, 2), MenuItem(AssetsPath + "Elements/Colour Picker", false, 2)]
        private static void CreateColourPicker() => CreateUIPrefab(ElementsPath + "Colour Picker");

        private static void CreateUIPrefab(string path)
        {
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path + ".prefab");

            if (!prefab)
            {
                Debug.LogError("UI Prefab not found at the specified path.");

                return;
            }

            GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);

            if (Selection.activeGameObject)
            {
                instance.transform.parent = Selection.activeGameObject.transform;
            }

            instance.transform.localPosition = Vector3.zero;
            instance.transform.localScale = Vector3.one;

            Selection.activeGameObject = instance;
        }
    }
}
#endif
