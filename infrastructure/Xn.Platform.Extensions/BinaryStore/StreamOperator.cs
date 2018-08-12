using System.IO;

namespace Xn.Platform.Core.BinaryStore
{
    public static class StreamOperator
    {
        static byte[] ForOr = { 1, 2, 4, 8, 16, 32, 64, 128 };
        static byte[] ForAnd = { 254, 253, 251, 247, 239, 223, 191, 127 };

        public static void SetBit(Stream stream, uint offset, bool value)
        {
            var byteOffset = offset / 8;
            var positionInByte = offset % 8;
            stream.Seek(byteOffset, SeekOrigin.Begin);
            var origin = stream.ReadByte();
            if (stream.Position > byteOffset)
            {
                stream.Seek(-1, SeekOrigin.Current);
            }
            byte originalByte = 0;

            if (origin > 0)
            {
                originalByte = (byte)origin;
            }

            if (value)
            {
                var result = (byte)(originalByte | ForOr[positionInByte]);
                stream.WriteByte(result);
            }
            else
            {
                var result = (byte)(originalByte & ForAnd[positionInByte]);
                stream.WriteByte(result);
            }
        }

        public static bool ReadBit(Stream stream, uint offset)
        {
            var byteOffset = offset / 8;
            var positionInByte = offset % 8;
            if (byteOffset >= stream.Length)
            {
                return false;
            }
            stream.Seek(byteOffset, SeekOrigin.Begin);
            byte originalByte = (byte)stream.ReadByte();
            return (originalByte & ForOr[positionInByte]) > 0;
        }

        public static uint Count(Stream stream)
        {
            uint result = 0;
            byte b = 0;
            stream.Seek(0, SeekOrigin.Begin);
            while (stream.Position < stream.Length)
            {
                b = (byte)stream.ReadByte();
                for (int i = 0; i < 8; i++)
                {
                    if (((b >> i) & 1) > 0)
                    {
                        result++;
                    }
                }
            }
            return result;
        }

        public static void MergeTow(Stream origin1, Stream origin2, Stream dest)
        {
            var origin1IsBigger = origin1.Length > origin2.Length;
            var bigger = origin1IsBigger ? origin1 : origin2;
            var smaller = origin1IsBigger ? origin2 : origin1;

            for (var i = 0L; i < smaller.Length; i++)
            {
                dest.WriteByte((byte)((byte)smaller.ReadByte() | (byte)bigger.ReadByte()));
            }
            for (var i = smaller.Length; i < bigger.Length; i++)
            {
                dest.WriteByte((byte)bigger.ReadByte());
            }
        }
        public static void MergeWith(Stream mergeFrom, Stream mergeTo)
        {
            mergeFrom.Seek(0, SeekOrigin.Begin);
            mergeTo.Seek(0, SeekOrigin.Begin);
            for (int i = 0; i < mergeFrom.Length; i++)
            {
                if (i < mergeTo.Length)
                {
                    var b1 = (byte)mergeFrom.ReadByte();
                    var b2 = (byte)mergeTo.ReadByte();
                    mergeTo.Seek(-1, SeekOrigin.Current);
                    mergeTo.WriteByte((byte)(b1 | b2));
                }
                else
                {
                    var b1 = (byte)mergeFrom.ReadByte();
                    mergeTo.WriteByte(b1);
                }
            }
        }
    }
}
