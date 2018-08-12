using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Xn.Platform.Infrastructure.Web.Hadoop.WebHDFS
{
    public interface IWebHdfsHttpClient
    {
        Task<string> Open(string path);

        Task<string> Create(string path, Stream content, bool overwrite);

        Task<string> Create(string path, string content, bool overwrite);

        Task<bool> Delete(string path, bool recursive);

        Task<DirectoryEntry> GetFileStatus(string path);

        Task<List<DirectoryEntry>> ListStatus(string path);

        Task<bool> MkDirs(string path);

        Task<bool> Rename(string path, string destination);

        /// <summary>
        /// Concat File(s)
        /// </summary>
        /// <param name="path"></param>
        /// <param name="sources"></param>
        /// <returns></returns>
        Task<bool> Concat(string path, string sources);

        /// <summary>
        /// Append to a File
        /// </summary>
        /// <param name="path"></param>
        /// <param name="sources"></param>
        /// <returns></returns>
        Task<bool> Append(string path, string content);

        Task<bool> Append(string path, Stream content);

        Task<string> CreateSymLink(string path, string destination, bool createParent);
    }
}