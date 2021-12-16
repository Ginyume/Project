using System;
using OpenHardwareMonitor.Hardware;
using System.IO;
using System.Threading;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace PPSU_hwmonitor_c
{
    class Program
    {
        static float cpuTemp;
        static float cpuUsage;
        static float cpuPowerDrawPackage;
        static float cpuFrequency;
        static float gpuTemp;
        static float gpuUsage;
        static float gpuCoreFrequency;
        static float gpuMemoryFrequency;
        

        static Computer c = new Computer()
        {
            GPUEnabled = true,
            CPUEnabled = true,
            //RAMEnabled = true
            //MainboardEnabled = true
            //FanControllerEnabled = true
            //HDDEnabled = true
        };

        static void ReportSystemInfo()
        {
            string path = @"E:\Visual Studio\Proje\veriler.txt";
            foreach (var hardware in c.Hardware)
            {
                
                if (hardware.HardwareType == HardwareType.CPU)
                {
                    hardware.Update();
                    foreach (var sensor in hardware.Sensors)
                        if (sensor.SensorType == SensorType.Temperature && sensor.Name.Contains("CPU Package"))
                        {
                            
                            cpuTemp = sensor.Value.GetValueOrDefault();
                            Console.WriteLine("cpuTemp: " + sensor.Value.GetValueOrDefault());
                            using (StreamWriter sw = File.AppendText(path))
                            {
                                sw.WriteLine("cpuTemp: " + sensor.Value.GetValueOrDefault());
                            }

                        }
                        else if (sensor.SensorType == SensorType.Load && sensor.Name.Contains("CPU Total"))
                        {
                            
                            cpuUsage = sensor.Value.GetValueOrDefault();
                            Console.WriteLine("cpuUsage: " + sensor.Value.GetValueOrDefault());
                            using (StreamWriter sw = File.AppendText(path))
                            {
                                sw.WriteLine("cpuUsage: " + sensor.Value.GetValueOrDefault());
                            }

                        }
                        else if (sensor.SensorType == SensorType.Power && sensor.Name.Contains("CPU Package"))
                        {
                            
                            cpuPowerDrawPackage = sensor.Value.GetValueOrDefault(); 
                            Console.WriteLine("CPU Power Draw - Package: " + sensor.Value.GetValueOrDefault());
                            using (StreamWriter sw = File.AppendText(path))
                            {
                                sw.WriteLine("CPU Power Draw - Package: " + sensor.Value.GetValueOrDefault());
                            }
                        }
                        else if (sensor.SensorType == SensorType.Clock && sensor.Name.Contains("CPU Core #1"))
                        {
                            
                            cpuFrequency = sensor.Value.GetValueOrDefault();
                            Console.WriteLine("cpuFrequency: " + sensor.Value.GetValueOrDefault());
                            using (StreamWriter sw = File.AppendText(path))
                            {
                                sw.WriteLine("cpuFrequency: " + sensor.Value.GetValueOrDefault());
                            }
                        }
                }

                if (hardware.HardwareType == HardwareType.GpuAti || hardware.HardwareType == HardwareType.GpuNvidia)
                {
                    hardware.Update();
                    foreach (var sensor in hardware.Sensors)
                        if (sensor.SensorType == SensorType.Temperature && sensor.Name.Contains("GPU Core"))
                        {
                            gpuTemp = sensor.Value.GetValueOrDefault();
                            Console.WriteLine("gpuTemp: " + sensor.Value.GetValueOrDefault());
                            using (StreamWriter sw = File.AppendText(path))
                            {
                                sw.WriteLine("gpuTemp: " + sensor.Value.GetValueOrDefault());
                            }

                        }
                        else if (sensor.SensorType == SensorType.Load && sensor.Name.Contains("GPU Core"))
                        {
                            gpuUsage = sensor.Value.GetValueOrDefault();
                            Console.WriteLine("gpuUsage: " + sensor.Value.GetValueOrDefault());
                            using (StreamWriter sw = File.AppendText(path))
                            {
                                sw.WriteLine("gpuUsage: " + sensor.Value.GetValueOrDefault());
                            }
                        }
                        else if (sensor.SensorType == SensorType.Clock && sensor.Name.Contains("GPU Core"))
                        {
                            gpuCoreFrequency = sensor.Value.GetValueOrDefault();
                            Console.WriteLine("gpuCoreFrequency: " + sensor.Value.GetValueOrDefault());
                            using (StreamWriter sw = File.AppendText(path))
                            {
                                sw.WriteLine("gpuCoreFrequency: " + sensor.Value.GetValueOrDefault());
                            }
                        }
                        else if (sensor.SensorType == SensorType.Clock && sensor.Name.Contains("GPU Memory"))
                        {
                            gpuMemoryFrequency = sensor.Value.GetValueOrDefault();
                            Console.WriteLine("gpuMemoryFrequency: " + sensor.Value.GetValueOrDefault());
                            using (StreamWriter sw = File.AppendText(path))
                            {
                                sw.WriteLine("gpuMemoryFrequency: " + sensor.Value.GetValueOrDefault());
                            }
                        }

                }
                

            }
        }

        static void Main(string[] args)
        {
            c.Open();
            
            while (true)
            {
                ReportSystemInfo();
                Console.WriteLine("------------------------------------------------------------");
                Thread.Sleep(2000);
            }
            
         
        }
    }
}