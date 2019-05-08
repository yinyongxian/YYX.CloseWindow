using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace YYX.CloseWindow
{
    /// <summary>  
    /// xml序列化和反序列化  
    /// 尹永贤  
    /// 2015-11-4 16:26:04  
    /// yinyongxian@qq.com  
    /// </summary>  
    /// <typeparam name="T"></typeparam>  
    public static class XmlSerializerHelper<T> where T : class, new()
    {
        private static readonly string FilePath = Path.Combine(Application.StartupPath, TypeName + ".xml");
        private static string TypeName { get { return typeof(T).ToString().Substring(typeof(T).ToString().LastIndexOf('.') + 1); } }

        /// <summary>  
        /// xml反序列化类  
        /// </summary>
        public static T Load()
        {
            try
            {
                using (var fileStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
                {
                    var xmlSerializer = new XmlSerializer(typeof(T));
                    return xmlSerializer.Deserialize(fileStream) as T;
                }
            }
            catch (FileNotFoundException)
            {
                return new T();
            }
            catch (Exception)
            {
                MessageBox.Show(string.Format("加载{0}失败。", TypeName));
                return new T();
            }
        }

        /// <summary>  
        /// xml序列化类  
        /// </summary>  
        /// <param name="t"></param>  
        public static void Save(T t)
        {
            try
            {
                using (var fileStream = new FileStream(FilePath, FileMode.Create, FileAccess.Write))
                {
                    var xmlSerializer = new XmlSerializer(typeof(T));
                    xmlSerializer.Serialize(fileStream, t);
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception)
            {
                MessageBox.Show(string.Format("保存{0}失败。", TypeName));
            }
        }
    }
}
