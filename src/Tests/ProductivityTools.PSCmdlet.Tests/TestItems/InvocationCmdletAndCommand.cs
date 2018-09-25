using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.PSCmdlet.Tests
{
    //[Cmdlet(VerbsCommon.Get, "Work2")]
    public class SomeCommand : PSCommandPT<SomeCmdlet>
    {
        public SomeCommand(SomeCmdlet cmdlet) : base(cmdlet) { }
        protected override bool Condition => this.Cmdlet.CmdletProperty;

        protected override void Invoke()
        {
            throw new NotImplementedException();
        }
    }

    //[Cmdlet(VerbsCommon.Get, "Work2")]
    public class SomeCmdlet : PSCmdletPT
    {
        public bool CmdletProperty { get; set; }


        public SomeCmdlet()
        {
            this.AddCommand(new SomeCommand(this));

        }

        protected override void ProcessRecord()
        {
            this.ProcessCommands();
        }

        public void DebugSomeCommand()
        {
            this.ProcessRecord();
        }
    }
}
