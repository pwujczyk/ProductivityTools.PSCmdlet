using DescriptionLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace PSCmdletProductivityTools
{
    public abstract class PSCmdletPT
    {
        [Parameter]
        [Description("Shows this menu")]
        public SwitchParameter Help { get; set; }

        private List<PSBaseCommandPT> CommandList = new List<PSBaseCommandPT>();

        protected void AddCommand(PSBaseCommandPT command)
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
            Console.WriteLine(line);
        }

        private void PrintPropertiesDescription()
        {
            var s = this.GetType().GetProperties();
            foreach (var item in s)
            {
                if (item.CustomAttributes.Any(x => x.AttributeType.Name == "ParameterAttribute"))
                {
                    string description = this.GetType().PropertyDescription(item.Name);
                    string line = $"{item.Name} - {description}";
                    Console.WriteLine(line);
                }
            }
        }
    }
}
