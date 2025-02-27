using System;

namespace Assets.Abstractions.Shared.Foundation
{
    /// <summary> Byte extensions. </summary>
    public static class ByteExtensions
    {
        /// <summary> Is the bit set? </summary>
        /// <param name="self">Value</param>
        /// <param name="pos">Bit to check</param>
        /// <returns>True if bit is set</returns>
        public static bool IsBitSet(this byte self, int pos)
        {
            return (self & (1 << pos)) != 0;
        }

        /// <summary> Sets a bit to 1. </summary>
        /// <param name="self">Value</param>
        /// <param name="pos">Bit to change</param>
        /// <returns>The new byte.</returns>
        public static byte SetBit(this byte self, int pos)
        {
            return (byte)(self | (1 << pos));
        }

        /// <summary> Sets a bit to 0. </summary>
        /// <param name="self">Value</param>
        /// <param name="pos">Bit to change</param>
        /// <returns>The new byte.</returns>
        public static byte UnsetBit(this byte self, int pos)
        {
            return (byte)(self & ~(1 << pos));
        }

        /// <summary> Change one bit. </summary>
        /// <param name="self">Value</param>
        /// <param name="pos">Bit to change</param>
        /// <returns>The new byte.</returns>
        public static byte ToggleBit(this byte self, int pos)
        {
            return (byte)(self ^ (1 << pos));
        }

        /// <summary> Byte a string. </summary>
        /// <param name="b">Value</param>
        /// <returns>String.</returns>
        public static string ToBinaryString(this byte b) => Convert.ToString(b, 2).PadLeft(8, '0');
    }
}