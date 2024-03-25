using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Helper
{
    public class TxtHelper
    {
        // 创建文本文件
        public static void CreateFile(string filePath)
        {
            try
            {
                // 如果文件不存在，则创建文件
                if (!File.Exists(filePath))
                {
                    using (StreamWriter sw = File.CreateText(filePath))
                    {
                        Console.WriteLine($"文件 {filePath} 创建成功！");
                    }
                }
                else
                {
                    Console.WriteLine($"文件 {filePath} 已经存在！");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"创建文件时发生错误：{ex.Message}");
            }
        }

        // 写入文本内容
        public static void WriteToFile(string filePath, string content)
        {
            try
            {
                // 追加文本内容到文件
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.WriteLine(content);
                    Console.WriteLine("内容写入成功！");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"写入文件时发生错误：{ex.Message}");
            }
        }

        public static void WriteToTxt(string path, string content, bool isAppend = true)
        {
            // 如果文件不存在，则创建文件
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    Console.WriteLine($"文件 {path} 创建成功！");
                }
            }
            //【1】创建文件流
            FileStream fileStream = new FileStream(path, isAppend ? FileMode.Append : FileMode.Create);

            //【2】创建写入器
            StreamWriter streamWriter = new StreamWriter(fileStream);

            //【3】以流的形式写入数据
            streamWriter.Write(content);

            //【4】关闭写入器
            streamWriter.Close();

            //【5】关闭文件流
            fileStream.Close();
        }

        // 读取文本内容
        public static void ReadFile(string filePath)
        {
            try
            {
                // 读取文件的所有文本内容
                string content = File.ReadAllText(filePath);
                Console.WriteLine($"文件内容：\n{content}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"读取文件时发生错误：{ex.Message}");
            }
        }
    }
}

