using System;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Xn.Platform.Core.Extensions
{
    public static class XmlExtensions
    {
        static Lazy<XmlWriterSettings> _settings = new Lazy<XmlWriterSettings>(() =>
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.NewLineChars = "\r\n";
            settings.Encoding = Encoding.UTF8;
            settings.IndentChars = "    ";
            return settings;
        });

        static Lazy<XmlSerializerNamespaces> _ns = new Lazy<XmlSerializerNamespaces>(() =>
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            return ns;
        });

        public static string ToXml(this object obj)
        {
            string result = null;
            using (var stream = new MemoryStream())
            {
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                using (var writer = XmlWriter.Create(stream, _settings.Value))
                {
                    serializer.Serialize(writer, obj, _ns.Value);
                }
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    stream.Position = 0;
                    result = reader.ReadToEnd();
                }
            }
            return result;
        }

        /// <summary>  
        /// �����л�XMLΪ��ʵ��  
        /// </summary>  
        /// <typeparam name="T"></typeparam>  
        /// <param name="xmlObj"></param>  
        /// <returns></returns>  
        public static T DeserializeXML<T>(string xmlObj)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StringReader reader = new StringReader(xmlObj))
            {
                return (T)serializer.Deserialize(reader);
            }
        }

        /// <summary>  
        /// ���л���ʵ��ΪXML  
        /// </summary>  
        /// <typeparam name="T"></typeparam>  
        /// <param name="obj"></param>  
        /// <returns></returns>  
        public static string SerializeXML<T>(T obj)
        {
            using (StringWriter writer = new StringWriter())
            {
                new XmlSerializer(obj.GetType()).Serialize((TextWriter)writer, obj);
                return writer.ToString();
            }
        }

        /// <summary>
        /// ��XML����ת�������ݼ�
        /// </summary>
        /// <param name="url">����XML���ݵ��ļ��� URL</param>
        public static DataSet XMLToDataSet(string url)
        {
            XmlTextReader reader = null;
            DataSet dataSet = new DataSet();
            try
            {
                reader = new XmlTextReader(url);
                dataSet.ReadXml(reader);
            }
            catch (Exception ex)
            {
                ex.Source += string.Format(".{0}", url);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return dataSet;
        }
    }
}