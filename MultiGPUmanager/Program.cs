using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MultiGPUmanager
{
    class Program
    {
        static void Main(string[] args)
        {
            String monitors = StartProcess("Assets/dc64cmd.exe", "-listmonitors");
            //Regex for Nvidia: MSI Optix MAG341CQ(\n|\r|\r\n)(.*)(\n|\r|\r\n)(.*)NVIDIA GeForce GTX 1050 Ti
            //Reegx for Radeon: MSI Optix MAG341CQ(\n|\r|\r\n)(.*)(\n|\r|\r\n)(.*)AMD Radeon RX 5700
            if (Regex.IsMatch(monitors, @"MSI Optix MAG341CQ(\n|\r|\r\n)(.*)(\n|\r|\r\n)(.*)AMD Radeon RX 5700"))
            {
                Console.WriteLine("Radeon GPU is in use. Switching to Nvidia");
                StartProcess("Assets/dc2.exe", "-secondary"); //dc2.exe gives no output
                StartProcess("Assets/WallpaperChanger.exe", "Assets/nvidia.jpg");

            }
            else
            {
                Console.WriteLine("Radeon GPU is NOT in use. Switching to Radeon");
                StartProcess("Assets/dc2.exe", "-primary"); //dc2.exe gives no output
                StartProcess("Assets/WallpaperChanger.exe", "Assets/radeon.jpg");
            }
        }

        public static String StartProcess(String FileName, String Arguments)
        {
            System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
            pProcess.StartInfo.FileName = FileName;
            pProcess.StartInfo.Arguments = Arguments;
            pProcess.StartInfo.UseShellExecute = false;
            pProcess.StartInfo.RedirectStandardOutput = true;
            pProcess.Start();
            return pProcess.StandardOutput.ReadToEnd();
        }
    }
}
