using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSCmdletProductivityTools
{
    public abstract class PSBaseCommandPT
    {
        public abstract void ProcessRecord();
    }

    public abstract class PSBaseCommandPT<CmdletType> : PSBaseCommandPT
    {
        protected CmdletType Cmdlet;

        public PSBaseCommandPT(CmdletType cmdletType)
        {
            this.Cmdlet = cmdletType;
        }

        protected abstract bool Condition { get; }

        protected abstract void Invoke();

        protected void InvokeCall(Action a)
        {
            a();
        }

        public override void ProcessRecord()
        {
            if (this.Condition)
            {
                Invoke();
            }
        }
    }

}
