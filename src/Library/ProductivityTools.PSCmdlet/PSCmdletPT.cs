using DescriptionLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.PSCmdlet
{
    public abstract class PSCmdletPT: System.Management.Automation.PSCmdlet
    {
        const string HelpText = "Shows this menu";

        [Parameter]
        [Description(HelpText)]
        public SwitchParameter Help { get; set; }

        private List<PSCommandPT> CommandList = new List<PSCommandPT>();

        public Action<string> WriteOutput { get; set; } = (s) => Console.WriteLine(s);

        protected void AddCommand(PSCommandPT command)
        {
            this.CommandList.Add(command);
        }

        protected void ProcessCommands()
        {
            if (Help.IsPresent)
            {
                ShowHelp();
            }
            else
            {
                foreach (var item in CommandList)
                {
                    item.ProcessRecord();
                }
            }
        }

        private void ShowHelp()
        {
            MethodDescription();
            PrintPropertiesDescription();
        }

        private void MethodDescription()
        {
            var s = this.GetType();
            var description = s.Description();
            string line = $"[{s.Name} - {description}]";
            WriteOutput(line);
        }

        private void PrintPropertiesDescription()
        {
            var s = this.GetType().GetProperties();
            foreach (var item in s)
            {
                if (item.CustomAttributes.Any(x => x.AttributeType.Name == "ParameterAttribute"))
                {
                    ParameterAttribute parameter = (ParameterAttribute)item.GetCustomAttributes(typeof(ParameterAttribute), true).Single();
                    
                    //string description = this.GetType().GetCustomAttributes( .PropertyDescription(item.Name);
                    string description = parameter.HelpMessage;
                    string line = $"{item.Name} - {description}";
                    WriteOutput(line);
                }
            }
        }
    }
}
