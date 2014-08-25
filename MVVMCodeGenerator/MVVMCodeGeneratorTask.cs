using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace MVVMCodeGenerator
{
    public class MVVMCodeGeneratorTask : Task
    {
        [Required]
        public string InputPath { get; set; }

        [Required]
        public string OutputPath { get; set; }

        [Required]
        public string NameSpace { get; set; }

        public override bool Execute()
        {
            string className = Path.GetFileNameWithoutExtension(InputPath);
            string fileContent;
            using (StreamReader sr = new StreamReader(InputPath))
            {
                fileContent = sr.ReadToEnd();
            }

            string outputClassName = Path.GetFileNameWithoutExtension(OutputPath);
            string output;
            if (outputClassName == ("I" + className)) // Interface - for example ITenant
                output = MVVMBasicGenerator.doInterfaceConversion(NameSpace, className, HTPropertyDefinition.fromFile(fileContent));
            else if (outputClassName == ("Partial" + className)) // PartialObject - for example PartialTenant
                output = MVVMBasicGenerator.doPartialObjectConversion(NameSpace, className, HTPropertyDefinition.fromFile(fileContent));
            else
                output = MVVMBasicGenerator.doConversion(NameSpace, className, HTPropertyDefinition.fromFile(fileContent));

            using (StreamWriter sw = new StreamWriter(OutputPath))
            {
                sw.Write(output);
            }
            return true;
        }
    }
}
