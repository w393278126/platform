using System;
using System.IO;

namespace Xn.Platform.Core.BinaryStore
{
    public class FileOperator : IDisposable
    {
        MemoryStream ms;
        public MemoryStream InnerStream
        {
            get
            {
                return ms;
            }

        }
        public FileOperator()
        {
            ms = new MemoryStream();
        }

        public FileOperator(MemoryStream s)
        {
            ms = s;
            ms.Seek(0, SeekOrigin.Begin);
        }

        public static FileOperator ReadFromFile(string filePath)
        {
            var data = File.ReadAllBytes(filePath);
            return new FileOperator(new MemoryStream(data));
        }


        public static FileOperator MergeTowFile(FileOperator file1, FileOperator file2)
        {
            var dest = new FileOperator();
            StreamOperator.MergeTow(file1.InnerStream, file2.InnerStream, dest.InnerStream);
            return dest;
        }

        public static void MergeTowFileTo(FileOperator file1, FileOperator file2, FileOperator dest)
        {
            StreamOperator.MergeTow(file1.InnerStream, file2.InnerStream, dest.InnerStream);
        }

        public void SetBit(uint offset, bool value)
        {
            StreamOperator.SetBit(ms, offset, value);
        }

        public bool GetBit(uint offset)
        {
            return StreamOperator.ReadBit(ms, offset);
        }

        public void Save(string filePath)
        {
            ms.Seek(0, SeekOrigin.Begin);
            using (var file = File.Create(filePath))
            {
                for (int i = 0; i < ms.Length; i++)
                {
                    var v = ms.ReadByte();
                    byte b = 0;
                    if (v > 0)
                        b = (byte)v;
                    file.WriteByte(b);
                }
            }
        }

        public void Merge(FileOperator source)
        {
            StreamOperator.MergeWith(source.InnerStream, ms);
        }

        public int Count()
        {
            return (int)StreamOperator.Count(ms);
        }

        public void Dispose()
        {
            ms.Dispose();
        }
    }
}