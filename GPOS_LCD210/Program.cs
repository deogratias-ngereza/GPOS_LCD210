using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO.Ports;
using System.IO;
using System.Threading;

namespace GPOS_LCD210
{
    public class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("\n ------------------------------------");
            Console.WriteLine("   GPOLL\n   + By GrandMaster\n   + email:grand123grand1@gmail.com\n   + phone:+255688 059 688");
            Console.WriteLine("\n ------------------------------------");
            
            run();
            Console.ReadLine();
        }


        static void run()
        {
            try
            {
                //watch a path
                FileSystemWatcher watcher = new FileSystemWatcher();
                watcher.Path = getConfigurations().path_to_watch;//mainController.getConfigurations().source_path;
                watcher.Filter = getConfigurations().file_to_watch;
                //event handlers
                watcher.Changed += new FileSystemEventHandler(new_watcher_Updated);
                watcher.EnableRaisingEvents = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ EXCEPTION ] " + ex.Message);
            }

        }

        static void new_watcher_Updated(object sender, FileSystemEventArgs e)
        {
            try
            {
                Console.WriteLine("[ UPDATE ] " + e.Name + " :: " + e.FullPath);
                Thread.Sleep(100);
                //read the content of the file and push
                string data = File.ReadAllText(e.FullPath);

                //write to a serial port
                Console.WriteLine("\n Listening on " + (getConfigurations()).com_port);
                SerialPort _serialPort = new SerialPort((getConfigurations()).com_port, 9600, Parity.None, 8, StopBits.One);
                _serialPort.Handshake = Handshake.None;
                _serialPort.WriteTimeout = 500;

                // Makes sure serial port is open before trying to write  
                try
                {
                    if (!(_serialPort.IsOpen))
                        _serialPort.Open();
                   /* var msg = "--------------------####################";
                    var top = "TOTAL               ";
                    var bottom = "1,000,000           ";
                    Console.WriteLine("LEN1: " + top.Length + "\n");
                    Console.WriteLine("LEN2: " + bottom.Length + "\n");
                    //_serialPort.Write("SI\r\n");*/

                    Helper helper = new Helper();
                    var __data = helper.getCustomPriceToDisplay(data);
                    Console.WriteLine("DATA LEN : " + __data.Length + "\n");
                    if (__data.Length == 40) {
                        _serialPort.WriteLine(__data);
                    }
                    _serialPort.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR: " + ex.Message);
                    //MessageBox.Show("Error opening/writing to serial port :: " + ex.Message, "Error!");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("[ EXCEPTION ] " + ex.Message);
            }

        }



        public static string get_my_path()
        {
            var executing_path = AppDomain.CurrentDomain.BaseDirectory;
            string targetDir = string.Format(executing_path + @"\");
            return targetDir;
        }
        /*CURRENT USER OBJECT*/
        public static ConfigurationModel getConfigurations()
        {
            try
            {
                //open the file
                using (StreamReader r = new StreamReader(get_my_path() + @"config.json"))
                {
                    
                    string json = r.ReadToEnd();
                    ConfigurationModel configObject = Newtonsoft.Json.JsonConvert.DeserializeObject<ConfigurationModel>(json);
                    return configObject;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ CONFIG-ERROR ] " + ex.Message);
                return new ConfigurationModel();
            }
        }//


    }
}
