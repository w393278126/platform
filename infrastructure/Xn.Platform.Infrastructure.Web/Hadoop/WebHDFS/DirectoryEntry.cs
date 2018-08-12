using Newtonsoft.Json.Linq;
using System;

namespace Xn.Platform.Infrastructure.Web.Hadoop.WebHDFS
{
    public class DirectoryEntry
    {
        private JObject Info { get; set; }

        public string AccessTime { get; set; }
        public string BlockSize { get; set; }
        public string Group { get; set; }
        public Int64 Length { get; set; }
        public string ModificationTime { get; set; }
        public string Owner { get; set; }
        public string PathSuffix { get; set; }

        public string Permission { get; set; }
        public int Replication { get; set; }

        public string Type { get; set; }

        public DirectoryEntry(JObject value)
        {
            AccessTime = value.Value<string>("accessTime");
            BlockSize = value.Value<string>("blockSize");
            Group = value.Value<string>("group");
            Length = value.Value<Int64>("length");
            ModificationTime = value.Value<string>("modificationTime");
            Owner = value.Value<string>("owner");
            PathSuffix = value.Value<string>("pathSuffix");
            Permission = value.Value<string>("permission");
            Replication = value.Value<int>("replication");
            Type = value.Value<string>("type");
            this.Info = value;
        }

        public override string ToString()
        {
            return this.Info.ToString();
        }
    }
}