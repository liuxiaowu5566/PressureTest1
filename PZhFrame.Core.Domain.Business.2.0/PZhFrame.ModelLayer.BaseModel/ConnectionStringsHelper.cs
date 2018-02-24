using Microsoft.Extensions.Configuration;
using System.IO;

namespace PZhFrame.ModelLayer.BaseModels
{
    /// <summary>
    /// 获取配置文件的字符串
    /// </summary>
    public class ConnectionStringsHelper
    {
        IConfigurationRoot configApp = null;
        public ConnectionStringsHelper()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "AppSettings.json");
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile(path);
            configApp = builder.Build();
        }

        /// <summary>
        /// 获取ProviderName
        /// </summary>
        /// <returns></returns>
        public string SqlType()
        {
            return configApp.GetSection("ConnectionStrings:SysConnection:ProviderName").Value;
        }

        /// <summary>
        /// 获取数据库的链接字符串
        /// </summary>
        /// <returns></returns>
        public string ConnString()
        {
            return configApp.GetSection("ConnectionStrings:SysConnection:ConnectionString").Value;
        }

        /// <summary>
        /// 获取数据库名称
        /// </summary>
        /// <returns></returns>
        public string DataName()
        {
            return configApp.GetSection("ConnectionStrings:SysConnection:ConnectionString").Value.Split(';')[1].Split('=')[1];
        }
    }
}
