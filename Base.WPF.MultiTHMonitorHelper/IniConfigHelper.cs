using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Base.Helper
{
    public class IniConfigHelper
    {
        /// <summary>
        /// 文件路径
        /// </summary>
        public static string IniPath = string.Empty;


        #region API函数声明

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key,
    string val, string filePath);

        //需要调用GetPrivateProfileString的重载
        [DllImport("kernel32", EntryPoint = "GetPrivateProfileString")]
        private static extern long GetPrivateProfileString(string section, string key,
            string def, StringBuilder retVal, int size, string filePath);

        [DllImport("kernel32", EntryPoint = "GetPrivateProfileString")]
        private static extern uint GetPrivateProfileStringA(string section, string key,
            string def, Byte[] retVal, int size, string filePath);

        #endregion

        #region 读取INI文件
        /// <summary>
        /// 根据节点及Key的值返回数据
        /// </summary>
        /// <param name="Section">节点</param>
        /// <param name="Key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="path">路径</param>
        /// <returns>返回值</returns>
        public static string ReadIniData(string Section, string Key, string NoText, string iniFilePath)
        {
            if (File.Exists(iniFilePath))
            {
                StringBuilder stringBuilder = new StringBuilder(10240);

                GetPrivateProfileString(Section, Key, NoText, stringBuilder, 10240, iniFilePath);

                return stringBuilder.ToString();
            }
            else
            {
                return string.Empty;
            }
        }


        /// <summary>
        /// 根据节点及Key的值返回数据
        /// </summary>
        /// <param name="Section">节点</param>
        /// <param name="Key">键</param>
        /// <param name="NoText">默认值</param>
        /// <returns>返回值</returns>
        public static string ReadIniData(string Section, string Key, string NoText)
        {
            return ReadIniData(Section, Key, NoText, IniPath);
        }

        #endregion

        #region 写入Ini文件

        /// <summary>
        /// 根据节点及Key的值写入数据
        /// </summary>
        /// <param name="Section">节点</param>
        /// <param name="Key">键</param>
        /// <param name="Value">值</param>
        /// <param name="path">路径</param>
        /// <returns>操作结果</returns>
        public static bool WriteIniData(string Section, string Key, string Value, string path)
        {
            long result = WritePrivateProfileString(Section, Key, Value, path);

            if (result == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 根据节点及Key的值写入数据
        /// </summary>
        /// <param name="Section">节点</param>
        /// <param name="Key">键</param>
        /// <param name="Value">值</param>
        /// <returns>操作结果</returns>
        public static bool WriteIniData(string Section, string Key, string Value)
        {
            return WriteIniData(Section, Key, Value, IniPath);
        }

        #endregion

        #region 读取所有的Sections

        /// <summary>
        /// 读取所有的Section
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>Section集合</returns>
        public static List<string> ReadSections(string path)
        {
            byte[] buffer = new byte[65536];

            uint length = GetPrivateProfileStringA(null, null, null, buffer, buffer.Length, path);

            int startIndex = 0;

            List<string> sections = new List<string>();

            for (int i = 0; i < length; i++)
            {
                if (buffer[i] == 0)
                {
                    sections.Add(Encoding.Default.GetString(buffer, startIndex, i - startIndex));
                    startIndex = i + 1;
                }
            }

            return sections;
        }

        /// <summary>
        /// 读取所有的Section
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>Section集合</returns>
        public static List<string> ReadSections()
        {
            byte[] buffer = new byte[65536];

            uint length = GetPrivateProfileStringA(null, null, null, buffer, buffer.Length, IniPath);

            int startIndex = 0;

            List<string> sections = new List<string>();

            for (int i = 0; i < length; i++)
            {
                if (buffer[i] == 0)
                {
                    sections.Add(Encoding.Default.GetString(buffer, startIndex, i - startIndex));
                    startIndex = i + 1;
                }
            }

            return sections;
        }



        #endregion

        #region 根据某个Section读取所有的Keys

        /// <summary>
        /// 根据某个Section读取所有的Keys
        /// </summary>
        /// <param name="section">某个section</param>
        /// <param name="path">路径</param>
        /// <returns>key的集合</returns>
        public static List<string> ReadKeys(string section, string path)
        {
            byte[] buffer = new byte[65536];

            uint length = GetPrivateProfileStringA(section, null, null, buffer, buffer.Length, path);

            int startIndex = 0;

            List<string> keys = new List<string>();

            for (int i = 0; i < length; i++)
            {
                if (buffer[i] == 0)
                {
                    keys.Add(Encoding.Default.GetString(buffer, startIndex, i - startIndex));
                    startIndex = i + 1;
                }
            }

            return keys;

        }

        /// <summary>
        /// 根据某个Section读取所有的Keys
        /// </summary>
        /// <param name="section">某个section</param>
        /// <param name="path">路径</param>
        /// <returns>key的集合</returns>
        public static List<string> ReadKeys(string section)
        {
            byte[] buffer = new byte[65536];

            uint length = GetPrivateProfileStringA(section, null, null, buffer, buffer.Length, IniPath);

            int startIndex = 0;

            List<string> keys = new List<string>();

            for (int i = 0; i < length; i++)
            {
                if (buffer[i] == 0)
                {
                    keys.Add(Encoding.Default.GetString(buffer, startIndex, i - startIndex));
                    startIndex = i + 1;
                }
            }

            return keys;

        }

        #endregion
    }
}
