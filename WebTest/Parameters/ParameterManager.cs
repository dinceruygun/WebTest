using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using WebSiteTest.Properties;

namespace WebSiteTest.Parameters
{
    public static class ParameterManager
    {
        private static string _parametersFile;

        private static string ParametersFile
        {
            get
            {
                if (_parametersFile != null) return _parametersFile;

                SaveFileDialog save = new SaveFileDialog
                {
                    Filter = Resources.ParameterManager_SaveParameters_Test_File___test,
                    OverwritePrompt = true,
                    CreatePrompt = true
                };

                if (save.ShowDialog() == DialogResult.OK) _parametersFile = save.FileName;

                save = null;

                return _parametersFile; 
            }
        }


        public static void SaveParameters(TestParameters parameters, bool saveAs)
        {
            if (saveAs) _parametersFile = null;

            if (ParametersFile == null) return;
            var pData = Newtonsoft.Json.JsonConvert.SerializeObject(parameters);

            StreamWriter data = new StreamWriter(ParametersFile);
            data.WriteLine(pData);
            data.Close();
        }

        public static TestParameters OpenParameters()
        {
            OpenFileDialog open = new OpenFileDialog
            {
                Filter = Resources.ParameterManager_SaveParameters_Test_File___test,
            };

            if (open.ShowDialog() == DialogResult.OK)
            {
                string pData = File.ReadAllText(open.FileName, Encoding.UTF8);
                var tParams = Newtonsoft.Json.JsonConvert.DeserializeObject<TestParameters>(pData);
                _parametersFile = open.FileName;

                return tParams;
            }
            else
            {
                return null;
            }
        }
    }
}
