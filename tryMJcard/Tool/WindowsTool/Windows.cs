using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace tryMJcard.Tool.WindowsTool
{
    class Windows
    {
        /// <summary>
        /// 硬件信息
        /// </summary>
        /// <returns></returns>
        private static string GetSystemInfo()
        {
            ManagementClass mangnmt = new ManagementClass("Win32_LogicalDisk");
            ManagementObjectCollection mcol = mangnmt.GetInstances();
            string dskInfo = "";
            foreach (ManagementObject strt in mcol)
            {
                dskInfo += Convert.ToString(strt["VolumeSerialNumber"]);
            }
            string cpuInfo = string.Empty;
            ManagementClass mc = new ManagementClass("win32_processor");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                cpuInfo = mo.Properties["processorID"].Value.ToString();
                break;
            }
            return cpuInfo + dskInfo;
        }

        /// <summary>
        /// 获取CPUID
        /// </summary>
        /// <returns></returns>
        public static string GetCpuID()
        {
            try
            {
                ManagementClass mc = new ManagementClass("Win32_Processor");
                ManagementObjectCollection moc = mc.GetInstances();
                String strCpuID = null;
                foreach (ManagementObject mo in moc)
                {
                    strCpuID = mo.Properties["ProcessorId"].Value.ToString();
                    break;
                }
                return strCpuID;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 取第一块硬盘编号
        /// </summary>
        /// <returns></returns>
        public static string GetHardDiskID()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");
                String strHardDiskID = null;
                foreach (ManagementObject mo in searcher.Get())
                {
                    strHardDiskID = mo["SerialNumber"].ToString().Trim();
                    break;
                }
                return strHardDiskID;
            }
            catch
            {
                return "";
            }
        }//end

        /// <summary>
        /// 结束指定进程并删除指定目录文件
        /// </summary>
        /// <param name="PeocessName"></param>
        /// <param name="DirPath"></param>
        /// <returns></returns>
        public string DeleteFile(string PeocessName, string DirPath)
        {
            Process[] process = Process.GetProcesses();
            string WinShellStr = string.Empty;
            foreach (var item in process)
            {
                if (item.ProcessName.IndexOf(PeocessName) >= 0)
                {
                    Console.WriteLine("PID:" + item.Id);
                    WinShellStr += @"ntsd -c q -p " + item.Id + " \r\n";

                }
            }
            WinShellStr += @"del /f /s /q " + DirPath + " \r\n";

            string shellReStr = WinShell.Execute(WinShellStr, 20);
            return shellReStr;

        }
    }
}
