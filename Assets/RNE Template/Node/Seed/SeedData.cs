using UnityEngine;

namespace RNE.Template.Node.Seed
{
    internal static class SeedData
    {
        private static int _seed = 0;
        internal static int Seed => _seed;

        private static System.Random _rnd = new System.Random(0);

        internal static void Generate()
        {
            _seed = _rnd.Next(int.MinValue, int.MaxValue);
            Debug.Log("Generated Seed: " + Seed);
        }
    }
}
