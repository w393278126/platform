using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Xn.Platform.Infrastructure.Web.Hadoop.WebHDFS
{
    public class WebHdfsHttpClient : IWebHdfsHttpClient
    {
        internal WebRequestHandler RequestHandler { get; set; }

        internal Uri WebHdfsUri { get; set; }

        public WebHdfsHttpClient(Uri webHdfsUri)
        {
            this.WebHdfsUri = webHdfsUri;
        }

        private HttpClient CreateHttpClient(bool allowsAutoRedirect = true)
        {
            if (RequestHandler != null)
            {
                return new HttpClient(RequestHandler);
            }
            return new HttpClient(new WebRequestHandler() { AllowAutoRedirect = allowsAutoRedirect });
        }

        internal Uri CreateRequestUri(WebHdfsOperation operation, string path, List<KeyValuePair<string, string>> parameters)
        {
            var paramString = string.Empty;
            if (parameters != null)
            {
                paramString = parameters.Aggregate("", (current, param) => current + string.Format("&{0}={1}", param.Key, param.Value));
            }
            var queryString = string.Format("{0}?op={1}{2}", path, operation, paramString);
            return new Uri(WebHdfsUri + queryString);
        }

        public async Task<string> Open(string path)
        {
            var client = this.CreateHttpClient();
            var resp = await client.GetAsync(this.CreateRequestUri(WebHdfsOperation.OPEN, path, null)).ConfigureAwait(false);
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadAsStringAsync().ConfigureAwait(false);
        }

        public async Task<string> Create(string path, Stream content, bool overwrite)
        {
            var client = this.CreateHttpClient(false);
            var parameters = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("overwrite", overwrite.ToString()) };
            var redir = await client.PutAsync(this.CreateRequestUri(WebHdfsOperation.CREATE, path, parameters), null).ConfigureAwait(false);

            content.Position = 0;
            var fileContent = new StreamContent(content);
            var create = await client.PutAsync(redir.Headers.Location, fileContent).ConfigureAwait(false);
            create.EnsureSuccessStatusCode();
            return create.Headers.Location.ToString();
        }

        public async Task<string> Create(string path, string content, bool overwrite)
        {
            return await this.Create(path, GetContent(content), overwrite).ConfigureAwait(false);
        }

        public async Task<bool> Delete(string path, bool recursive)
        {
            var client = this.CreateHttpClient();

            var parameters = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("recursive", recursive.ToString()) };
            var drop = await client.DeleteAsync(this.CreateRequestUri(WebHdfsOperation.DELETE, path, parameters)).ConfigureAwait(false);
            drop.EnsureSuccessStatusCode();

            var content = await drop.Content.ReadAsAsync<JObject>().ConfigureAwait(false);
            return content.Value<bool>("boolean");
        }

        public async Task<DirectoryEntry> GetFileStatus(string path)
        {
            var client = this.CreateHttpClient();

            var status = await client.GetAsync(this.CreateRequestUri(WebHdfsOperation.GETFILESTATUS, path, null)).ConfigureAwait(false);
            status.EnsureSuccessStatusCode();

            var filesStatusTask = await status.Content.ReadAsAsync<JObject>().ConfigureAwait(false);

            return new DirectoryEntry(filesStatusTask.Value<JObject>("FileStatus"));
        }

        public async Task<List<DirectoryEntry>> ListStatus(string path)
        {
            var client = this.CreateHttpClient();
            var status = await client.GetAsync(this.CreateRequestUri(WebHdfsOperation.LISTSTATUS, path, null)).ConfigureAwait(false);
            status.EnsureSuccessStatusCode();
            var filesStatusTask = await status.Content.ReadAsAsync<JObject>().ConfigureAwait(false);
            var fileStatus = filesStatusTask.Value<JObject>("FileStatuses").Value<JArray>("FileStatus");
            List<DirectoryEntry> result = new List<DirectoryEntry>();
            for (int i = 0; i < fileStatus.Count; i++)
            {
                result.Add(new DirectoryEntry(fileStatus[i].Value<JObject>("FileStatuses")));
            }
            return result;
        }

        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task<bool> MkDirs(string path)
        {
            var client = this.CreateHttpClient(false);
            var mkdir = await client.PutAsync(this.CreateRequestUri(WebHdfsOperation.MKDIRS, path, null), null).ConfigureAwait(false);
            mkdir.EnsureSuccessStatusCode();
            var content = await mkdir.Content.ReadAsAsync<JObject>().ConfigureAwait(false);
            return content.Value<bool>("boolean");
        }

        /// <summary>
        /// 重命名
        /// </summary>
        /// <param name="path"></param>
        /// <param name="destination">目标名称</param>
        /// <returns></returns>
        public async Task<bool> Rename(string path, string destination)
        {
            var client = this.CreateHttpClient(false);
            var parameters = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("destination", destination) };
            var rename = await client.PutAsync(this.CreateRequestUri(WebHdfsOperation.RENAME, path, parameters), null).ConfigureAwait(false);
            rename.EnsureSuccessStatusCode();

            var content = await rename.Content.ReadAsAsync<JObject>().ConfigureAwait(false);
            return content.Value<bool>("boolean");
        }

        public async Task<bool> Append(string path, Stream content)
        {
            var client = this.CreateHttpClient(false);
            var redir = await client.PostAsync(this.CreateRequestUri(WebHdfsOperation.APPEND, path, null), null).ConfigureAwait(false);
            var fileContent = new StreamContent(content);
            var append = await client.PostAsync(redir.Headers.Location, fileContent).ConfigureAwait(false);
            append.EnsureSuccessStatusCode();
            return append.IsSuccessStatusCode;
        }

        public async Task<bool> Append(string path, string content)
        {
            return await this.Append(path, GetContent(content)).ConfigureAwait(false);
        }

        public async Task<bool> Concat(string path, string sources)
        {
            var client = this.CreateHttpClient(false);
            var parameters = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("sources", sources) };
            var concat = await client.PostAsync(this.CreateRequestUri(WebHdfsOperation.CONCAT, path, parameters), null).ConfigureAwait(false);
            concat.EnsureSuccessStatusCode();
            var content = await concat.Content.ReadAsAsync<JObject>().ConfigureAwait(false);
            return content.Value<bool>("boolean");
        }

        public async Task<string> CreateSymLink(string path, string destination, bool createParent)
        {
            var client = this.CreateHttpClient(false);
            var parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("destination", destination),
                new KeyValuePair<string, string>("createParent", createParent.ToString())
            };
            var link = await client.PutAsync(this.CreateRequestUri(WebHdfsOperation.CREATESYMLINK, path, parameters), null).ConfigureAwait(false);
            link.EnsureSuccessStatusCode();
            return link.Headers.Location.ToString();
        }

        private static MemoryStream GetContent(string content)
        {
            byte[] byteArray = Encoding.ASCII.GetBytes(content);
            return new MemoryStream(byteArray);
        }
    }
}