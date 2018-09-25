using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.PSCmdlet.Tests
{
    public class CommandWritesText : PSCommandPT<WritingCmdlet>
    {
        public CommandWritesText(WritingCmdlet cmdlet) : base(cmdlet)
        {
        }

        protected override bool Condition => !string.IsNullOrEmpty(this.Cmdlet.WriteText);

        protected override void Invoke()
        {
            WriteOutput("String");
        }
    }


    public class WritingCmdlet : PSCmdletPT
    {
        public string WriteText { get; set; }

        public WritingCmdlet()
        {
            this.AddCommand(new CommandWritesText(this));
        }

        protected override void ProcessRecord()
        {
            base.ProcessCommands();
        }

        public void DebugSomeCommand()
        {
            this.ProcessRecord();
        }       
    }
}
