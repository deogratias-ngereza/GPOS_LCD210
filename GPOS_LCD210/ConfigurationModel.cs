using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPOS_LCD210
{
    public class ConfigurationModel
    {
        public string com_port { get; set; }
        public string path_to_watch { get; set; }
        public string file_to_watch { get; set; }


        public ConfigurationModel()
        {
            this.com_port = "COM13";
            this.path_to_watch = @"C:\gpoll\";
            this.file_to_watch = "data.txt";
        }

    }
}
