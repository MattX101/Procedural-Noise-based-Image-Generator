using UnityEngine;

namespace RNE.Template.Node.Seed
{
    internal class SeedManager : MonoBehaviour
    {
        public static void Generate()
        {
            SeedData.Generate();
        }
    }
}
