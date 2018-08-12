using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Xn.Platform.Core.Extensions
{
    internal static class Encodings
    {
        /// <summary>
        /// Returns UTF8 Encoding without BOM and throws on invalid bytes.
        /// </summary>
        public static readonly Encoding Utf8EncodingWithoutBom = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false, throwOnInvalidBytes: true);
   }
    public static class FileExtensions
    {
        public static string BasePath = @"d:\report";
        public const string Machine = "web2012111";
        public static string GetFilePath(DateTime day, string fileName, int index = 0)
        {
            return Path.Combine(BasePath, day.ToString("yyyyMMdd"), string.Format("{0}.{1}.{2}.{3}.dat", fileName, day.ToString("yyyyMMddHHmm"), index, Machine));
        }

        public static IList<string> GetFilePaths(DateTime day, string fileName)
        {
            var filePath = Path.Combine(BasePath, day.ToString("yyyyMMdd"));
            if(!Directory.Exists(filePath))
                return new string[0];
            var searchPattern = string.Format("{0}.{1}.{2}.{3}.dat", fileName, day.ToString("yyyyMMddHHmm"), "*", Machine);
            var fileNames = Directory.GetFiles(filePath, searchPattern);
            return fileNames;
        }

        public static void WriteFilePre(this string filePath)
        {
            var driectoryName = Path.GetDirectoryName(filePath);
            if (driectoryName != null)
            {
                if (!Directory.Exists(driectoryName))
                    Directory.CreateDirectory(driectoryName);
            }
            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        public static long ReadFile(this string filePath, Action<string> action)
        {
            long count = 0;
            if (!File.Exists(filePath))
                return count;
            using (var sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream)
                {
                    string data;
                    if (!string.IsNullOrEmpty(data = sr.ReadLine()))
                    {
                        count++;
                        action(data);
                    }
                }
            }
            return count;
        }

        public static long ReadFileUtf8(this string filePath, Action<string> action)
        {
            long count = 0;
            if (!File.Exists(filePath))
                return count;
            using (var sr = new StreamReader(filePath, Encoding.UTF8))
            {
                while (!sr.EndOfStream)
                {
                    string data;
                    if (!string.IsNullOrEmpty(data = sr.ReadLine()))
                    {
                        count++;
                        action(data);
                    }
                }
            }
            return count;
        }


        public static void WriteFile<T>(this IEnumerable<T> entities, string filePath, Func<T, string> func, string title = null)
        {
            WriteFilePre(filePath);
            if (!entities.Any())
                return;
            var isWrite = false;
            using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                using (var sw = new StreamWriter(fs, Encodings.Utf8EncodingWithoutBom))
                {
                    if(!string.IsNullOrEmpty(title))
                        sw.WriteLine(title);
                    foreach (var entity in entities)
                    {
                        var t = func(entity);
                        if (!string.IsNullOrEmpty(t))
                        {
                            sw.WriteLine(t);
                            isWrite = true;
                        }
                    }
                    sw.Flush();
                }
            }
            if (!isWrite)
                File.Delete(filePath);
        }

        public static void AppendFile<T>(this IEnumerable<T> entities, string filePath, Func<T, string> func)
        {
            using (var fs = new FileStream(filePath, FileMode.Append, FileAccess.Write))
            {
                using (var sw = new StreamWriter(fs))
                {
                    foreach (var entity in entities)
                    {
                        var t = func(entity);
                        if (!string.IsNullOrEmpty(t))
                        {
                            sw.WriteLine(t);
                        }
                    }
                    sw.Flush();
                }
            }
        }

        public static bool DeleteFile(this string path)
        {
            bool ret = false;
            try
            {
                var file = new FileInfo(path);
                if (file.Exists)
                {
                    file.Delete();
                    ret = true;
                }
                return ret;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}