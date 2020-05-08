using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.PSCmdlet
{
    public abstract class PSCommandPT
    {
        public abstract void ProcessRecord();
    }

    public abstract class PSCommandPT<CmdletType> : PSCommandPT where CmdletType : PSCmdletPT
    {
        protected CmdletType Cmdlet;

        public PSCommandPT(CmdletType cmdletType)
        {
            this.Cmdlet = cmdletType;
        }

        protected void WriteOutput(string s)
        {
            this.Cmdlet.WriteOutput(s);
        }

        protected abstract bool Condition { get; }

        protected abstract void Invoke();

        public override void ProcessRecord()
        {
            if (this.Condition)
            {
                Invoke();
            }
        }
    }
}
