using System;
using System.IO;
using Microsoft.Win32;

namespace YYX.CloseWindow
{
    public static class WindowAutoRun
    {
        private const string Subkey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";


        public static void SetEnable(string nameGuid, string executablePath, bool runEnable)
        {
            if (File.Exists(executablePath) == false)
            {
                throw new InvalidOperationException("指定文件不存在。");
            }
            else
            {
                //打开子键
                using (var registryKey = Registry.LocalMachine.OpenSubKey(Subkey, true))
                {
                    if (registryKey == null)
                    {
                        return;
                    }

                    if (runEnable)
                    {
                        registryKey.SetValue(nameGuid, executablePath);
                    }
                    else
                    {
                        registryKey.SetValue(nameGuid, false);
                    }
                }
            }
        }

        public static bool GetEnable(string nameGuid, string executablePath)
        {
            if (File.Exists(executablePath) == false)
            {
                throw new InvalidOperationException("指定文件不存在。");
            }
            else
            {
                //打开子键
                using (var registryKey = Registry.LocalMachine.OpenSubKey(Subkey, false))
                {
                    if (registryKey == null)
                    {
                        return false;
                    }

                    //读取值
                    var value = registryKey.GetValue(nameGuid);

                    //判断启用状态
                    return executablePath.Equals(value);
                }
            }
        }
    }
}
