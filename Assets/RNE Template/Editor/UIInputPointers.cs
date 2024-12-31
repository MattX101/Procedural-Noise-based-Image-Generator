using UnityEditor;

#if UNITY_EDITOR
namespace RNE.Template.Editor
{
    internal partial class UIPrefabMenu
    {
        private const string InputPointersPath = "Assets/RNE Template/Assets/Prefabs/Pointers/Inputs/";

        // ---
        // Int
        [MenuItem(GameObjectPath + "Pointers/Input/Int/Blank Int", false, 0), MenuItem(AssetsPath + "Pointers/Input/Int/Blank Int", false, 0)]
        private static void CreateBlankIntInputPointer() => CreateUIPrefab(InputPointersPath + "Int/Blank Int Input Pointe");
        
        [MenuItem(GameObjectPath + "Pointers/Input/Int/Int", false, 0), MenuItem(AssetsPath + "Pointers/Input/Int/Int", false, 0)]
        private static void CreateIntInputPointer() => CreateUIPrefab(InputPointersPath + "Int/Int Input Pointer");
        
        [MenuItem(GameObjectPath + "Pointers/Input/Int/Mini Int", false, 0), MenuItem(AssetsPath + "Pointers/Input/Int/Mini Int", false, 0)]
        private static void CreateMiniIntInputPointer() => CreateUIPrefab(InputPointersPath + "Int/Mini Int Input Pointer");

        [MenuItem(GameObjectPath + "Pointers/Input/Int/Int Slider", false, 0), MenuItem(AssetsPath + "Pointers/Input/Int/Int Slider", false, 0)]
        private static void CreateIntSliderInputPointer() => CreateUIPrefab(InputPointersPath + "Int/Int Slider Input Pointer");
        // ---

        // ---
        // Float
        [MenuItem(GameObjectPath + "Pointers/Input/Float/Blank Float", false, 1), MenuItem(AssetsPath + "Pointers/Input/Float/Blank Float", false, 1)]
        private static void CreateBlankFloatInputPointer() => CreateUIPrefab(InputPointersPath + "Float/Blank Float Input Pointer");

        [MenuItem(GameObjectPath + "Pointers/Input/Float/Float", false, 1), MenuItem(AssetsPath + "Pointers/Input/Float/Float", false, 1)]
        private static void CreateFloatInputPointer() => CreateUIPrefab(InputPointersPath + "Float/Float Input Pointer");

        [MenuItem(GameObjectPath + "Pointers/Input/Float/Mini Float", false, 1), MenuItem(AssetsPath + "Pointers/Input/Float/Mini Float", false, 1)]
        private static void CreateMiniFloatInputPointer() => CreateUIPrefab(InputPointersPath + "Float/Mini Float Input Pointer");

        [MenuItem(GameObjectPath + "Pointers/Input/Float/Float Slider", false, 1), MenuItem(AssetsPath + "Pointers/Input/Float/Float Slider", false, 1)]
        private static void CreateFloatSliderInputPointer() => CreateUIPrefab(InputPointersPath + "Float/Float Slider Input Pointer");
        // ---

        [MenuItem(GameObjectPath + "Pointers/Input/Vector2", false, 2), MenuItem(AssetsPath + "Pointers/Input/Vector2", false, 2)]
        private static void CreateVector2InputPointer() => CreateUIPrefab(InputPointersPath + "Vector2 Input Pointer");

        [MenuItem(GameObjectPath + "Pointers/Input/Vector3", false, 3), MenuItem(AssetsPath + "Pointers/Input/Vector3", false, 3)]
        private static void CreateVector3InputPointer() => CreateUIPrefab(InputPointersPath + "Vector3 Input Pointer");

        [MenuItem(GameObjectPath + "Pointers/Input/Bool", false, 4), MenuItem(AssetsPath + "Pointers/Input/Bool", false, 4)]
        private static void CreateBoolInputPointer() => CreateUIPrefab(InputPointersPath + "Bool Input Pointer");

        [MenuItem(GameObjectPath + "Pointers/Input/Char", false, 5), MenuItem(AssetsPath + "Pointers/Input/Char", false, 5)]
        private static void CreateCharInputPointer() => CreateUIPrefab(InputPointersPath + "Char Input Pointer");

        // ---
        // String
        [MenuItem(GameObjectPath + "Pointers/Input/String/String", false, 6), MenuItem(AssetsPath + "Pointers/Input/String/String", false, 6)]
        private static void CreateStringInputPointer() => CreateUIPrefab(InputPointersPath + "String/String Input Pointer");

        [MenuItem(GameObjectPath + "Pointers/Input/String/Mini String", false, 6), MenuItem(AssetsPath + "Pointers/Input/String/Mini String", false, 6)]
        private static void CreateMiniStringInputPointer() => CreateUIPrefab(InputPointersPath + "String/Mini String Input Pointer");
        // ---

        [MenuItem(GameObjectPath + "Pointers/Input/Color", false, 7), MenuItem(AssetsPath + "Pointers/Input/Color", false, 7)]
        private static void CreateColorInputPointer() => CreateUIPrefab(InputPointersPath + "Color Input Pointer");

        [MenuItem(GameObjectPath + "Pointers/Input/Texture", false, 8), MenuItem(AssetsPath + "Pointers/Input/Texture", false, 8)]
        private static void CreateTextureInputPointer() => CreateUIPrefab(InputPointersPath + "Texture Input Pointer");

        [MenuItem(GameObjectPath + "Pointers/Input/Warp Profile", false, 9), MenuItem(AssetsPath + "Pointers/Input/Warp Profile", false, 9)]
        private static void CreateWarpProfileInputPointer() => CreateUIPrefab(InputPointersPath + "Warp Profile Input Pointer");

        [MenuItem(GameObjectPath + "Pointers/Input/Cellular Profile", false, 10), MenuItem(AssetsPath + "Pointers/Input/Cellular Profile", false, 10)]
        private static void CreateCellularProfileInputPointer() => CreateUIPrefab(InputPointersPath + "Cellular Profile Input Pointer");
        
        [MenuItem(GameObjectPath + "Pointers/Input/Color Gradient", false, 11), MenuItem(AssetsPath + "Pointers/Input/Color Gradient", false, 11)]
        private static void CreateColorGradientInputPointer() => CreateUIPrefab(InputPointersPath + "Color Gradient Input Pointer");
    }
}
#endif
