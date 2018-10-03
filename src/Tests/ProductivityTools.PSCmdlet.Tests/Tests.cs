using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProductivityTools.PSCmdlet.Tests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void CommandNotInvoked()
        {
            var cmdl = new SomeCmdlet();
            cmdl.DebugSomeCommand();
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void CommandInvoked()
        {
            var cmdl = new SomeCmdlet();
            cmdl.CmdletProperty = true;
            cmdl.DebugSomeCommand();
        }

        [TestMethod]
        [DataRow("", false)]
        [DataRow("notEmpty",true)]
        public void WritingOutput(string s, bool result)
        {
            bool writeTextInvoked = false;
            Action<string> writetext = (notimportant) => writeTextInvoked = true;
            WritingCmdlet commandWritesText = new WritingCmdlet();
            commandWritesText.WriteOutput = writetext;

            commandWritesText.WriteText = s;
            commandWritesText.DebugSomeCommand();
            Assert.AreEqual(writeTextInvoked, result);
        }

        [TestMethod]
        public void WriteHelp()
        {
            string helpText = string.Empty;
            Action<string> writetext = (s) => helpText += s;
            WritingCmdlet commandWritesText = new WritingCmdlet();
            commandWritesText.WriteOutput = writetext;

            commandWritesText.Help = true;
            commandWritesText.DebugSomeCommand();
            Assert.AreEqual(helpText, "[WritingCmdlet - ]WriteText - CustomHelpMessageHelp - ");
        }
    }
}
