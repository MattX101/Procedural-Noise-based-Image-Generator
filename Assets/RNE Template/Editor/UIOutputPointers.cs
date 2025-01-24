using UnityEditor;

#if UNITY_EDITOR
namespace RNE.Template.Editor
{
    internal partial class UIPrefabMenu
    {
        private const string OutputPointersPath = "Assets/RNE Template/Assets/Prefabs/Pointers/Outputs/";

        // ---
        // Int
        [MenuItem(GameObjectPath + "Pointers/Output/Int/Blank Int", false, 0), MenuItem(AssetsPath + "Pointers/Output/Int/Blank Int", false, 0)]
        private static void CreateBlankIntOutputPointer() => CreateUIPrefab(OutputPointersPath + "Int/Blank Int Output Pointer");

        [MenuItem(GameObjectPath + "Pointers/Output/Int/Int", false, 0), MenuItem(AssetsPath + "Pointers/Output/Int/Int", false, 0)]
        private static void CreateIntOutputPointer() => CreateUIPrefab(OutputPointersPath + "Int/Int Output Pointer");

        [MenuItem(GameObjectPath + "Pointers/Output/Int/Mini Int", false, 0), MenuItem(AssetsPath + "Pointers/Output/Int/Mini Int", false, 0)]
        private static void CreateMiniIntOutputPointer() => CreateUIPrefab(OutputPointersPath + "Int/Mini Int Output Pointer");
        // ---

        // ---
        // Float
        [MenuItem(GameObjectPath + "Pointers/Output/Float/Blank Float", false, 1), MenuItem(AssetsPath + "Pointers/Output/Float/Blank Float", false, 1)]
        private static void CreateBlankFloatOutputPointer() => CreateUIPrefab(OutputPointersPath + "Float/Blank Float Output Pointer");

        [MenuItem(GameObjectPath + "Pointers/Output/Float/Float", false, 1), MenuItem(AssetsPath + "Pointers/Output/Float/Float/Float", false, 1)]
        private static void CreateFloatOutputPointer() => CreateUIPrefab(OutputPointersPath + "Float/Float Output Pointer");

        [MenuItem(GameObjectPath + "Pointers/Output/Float/Mini Float", false, 1), MenuItem(AssetsPath + "Pointers/Output/Float/Mini Float", false, 1)]
        private static void CreateMiniFloatOutputPointer() => CreateUIPrefab(OutputPointersPath + "Float/Mini Float Output Pointer");
        // ---

        [MenuItem(GameObjectPath + "Pointers/Output/Vector2", false, 2), MenuItem(AssetsPath + "Pointers/Output/Vector2", false, 2)]
        private static void CreateVector2OutputPointer() => CreateUIPrefab(OutputPointersPath + "Vector2 Output Pointer");

        [MenuItem(GameObjectPath + "Pointers/Output/Vector3", false, 3), MenuItem(AssetsPath + "Pointers/Output/Vector3", false, 3)]
        private static void CreateVector3OutputPointer() => CreateUIPrefab(OutputPointersPath + "Vector3 Output Pointer");

        [MenuItem(GameObjectPath + "Pointers/Output/Bool", false, 4), MenuItem(AssetsPath + "Pointers/Output/Bool", false, 4)]
        private static void CreateBoolOutputPointer() => CreateUIPrefab(OutputPointersPath + "Bool Output Pointer");

        [MenuItem(GameObjectPath + "Pointers/Output/Char", false, 5), MenuItem(AssetsPath + "Pointers/Output/Char", false, 5)]
        private static void CreateCharOutputPointer() => CreateUIPrefab(OutputPointersPath + "Char Output Pointer");
        
        // ---
        // String
        [MenuItem(GameObjectPath + "Pointers/Output/String/String", false, 6), MenuItem(AssetsPath + "Pointers/Output/String/String", false, 6)]
        private static void CreateStringOutputPointer() => CreateUIPrefab(OutputPointersPath + "String/String Output Pointer");

        [MenuItem(GameObjectPath + "Pointers/Output/String/Mini String", false, 6), MenuItem(AssetsPath + "Pointers/Output/String/Mini String", false, 6)]
        private static void CreateMiniStringOutputPointer() => CreateUIPrefab(OutputPointersPath + "String/Mini String Output Pointer");
        // ---

        [MenuItem(GameObjectPath + "Pointers/Output/Color", false, 7), MenuItem(AssetsPath + "Pointers/Output/Color", false, 7)]
        private static void CreateColorOutputPointer() => CreateUIPrefab(OutputPointersPath + "Color Output Pointer");

        [MenuItem(GameObjectPath + "Pointers/Output/Texture", false, 8), MenuItem(AssetsPath + "Pointers/Output/Texture", false, 8)]
        private static void CreateTextureOutputPointer() => CreateUIPrefab(OutputPointersPath + "Texture Output Pointer");

        [MenuItem(GameObjectPath + "Pointers/Output/Noise", false, 9), MenuItem(AssetsPath + "Pointers/Output/Noise", false, 9)]
        private static void CreateNoiseOutputPointer() => CreateUIPrefab(OutputPointersPath + "Noise Output Pointer");

        [MenuItem(GameObjectPath + "Pointers/Output/Warp Profile", false, 10), MenuItem(AssetsPath + "Pointers/Output/Warp Profile", false, 10)]
        private static void CreateWarpProfileOutputPointer() => CreateUIPrefab(OutputPointersPath + "Warp Profile Output Pointer");

        [MenuItem(GameObjectPath + "Pointers/Output/Cellular Profile", false, 11), MenuItem(AssetsPath + "Pointers/Output/Cellular Profile", false, 11)]
        private static void CreateCellularProfileOutputPointer() => CreateUIPrefab(OutputPointersPath + "Cellular Profile Output Pointer");

        [MenuItem(GameObjectPath + "Pointers/Output/Color Gradient", false, 12), MenuItem(AssetsPath + "Pointers/Output/Color Gradient", false, 12)]
        private static void CreateColorGradientOutputPointer() => CreateUIPrefab(OutputPointersPath + "Color Gradient Output Pointer");
    }
}
#endif
