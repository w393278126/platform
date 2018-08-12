namespace Xn.Platform.Core.Extensions
{
    public static class CryptoExtensions
    {
        public static bool BuffersAreEqual(byte[] buffer1, int buffer1Offset, int buffer1Count, byte[] buffer2, int buffer2Offset, int buffer2Count)
        {
            bool flag = buffer1Count == buffer2Count;
            for (int index = 0; index < buffer1Count; ++index)
                flag = flag & (int)buffer1[buffer1Offset + index] == (int)buffer2[buffer2Offset + index % buffer2Count];
            return flag;
        }

        /// <summary>
        /// 16进制字符串转成byte数组
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] HexToBinary(string data)
        {
            if (data == null || data.Length % 2 != 0)
                return (byte[])null;
            byte[] numArray = new byte[data.Length / 2];
            for (int index = 0; index < numArray.Length; ++index)
            {
                int num1 = _HexToInt(data[2 * index]);
                int num2 = _HexToInt(data[2 * index + 1]);
                if (num1 == -1 || num2 == -1)
                    return (byte[])null;
                numArray[index] = (byte)(num1 << 4 | num2);
            }
            return numArray;
        }

        /// <summary>
        /// byte数组转成16进制字符串，速度是ToString的10+倍
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string BinaryToHex(byte[] data)
        {
            if (data == null)
                return (string)null;
            char[] chArray = new char[checked(data.Length * 2)];
            for (int index = 0; index < data.Length; ++index)
            {
                byte num = data[index];
                chArray[2 * index] = _NibbleToHex((byte)((uint)num >> 4));
                chArray[2 * index + 1] = _NibbleToHex((byte)((uint)num & 15U));
            }
            return new string(chArray);
        }

        static int _HexToInt(char h)
        {
            if ((int)h >= 48 && (int)h <= 57)
                return (int)h - 48;
            if ((int)h >= 97 && (int)h <= 102)
                return (int)h - 97 + 10;
            if ((int)h < 65 || (int)h > 70)
                return -1;
            else
                return (int)h - 65 + 10;
        }

        static char _NibbleToHex(byte nibble)
        {
            return (int)nibble < 10 ? (char)((int)nibble + 48) : (char)((int)nibble - 10 + 65);
        }
    }
}