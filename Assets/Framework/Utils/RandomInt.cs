namespace SplitSpheres.Framework.Utils
{
    public static class RandomInt
    {
        public static int GenerateNumber(int min, int max)
        {
            return new System.Random().Next(min, max);
        }
    }
}