using System.Collections.Generic;

namespace SafeEncrypt
{
    public static class Extensions
    {
        public static List<byte> ToList(this byte[] array) => new List<byte>(array);

        public static bool InRange(this int value, int valueMin, int valueMax) => value >= valueMin && value <= valueMax;
    }
}