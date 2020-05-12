<!--Category:C#,PowerShell--> 
 <p align="right">
    <a href="https://www.nuget.org/packages/ProductivityTools.PSCmdlet/"><img src="Images/Header/Nuget_border_40px.png" /></a>
    <a href="http://productivitytools.tech/pscmdlet/"><img src="Images/Header/ProductivityTools_green_40px_2.png" /><a> 
    <a href="https://www.github.com/pwujczyk/ProductivityTools.PSCmdlet"><img src="Images/Header/Github_border_40px.png" /></a>
</p>
<p align="center">
    <a href="http://http://productivitytools.tech/">
        <img src="Images/Header/LogoTitle_green_500px.png" />
    </a>
</p>

# PSCmdlet

It is base class for PowerShell Cmdlets. It provides structure of the commands and helps to write modular and clean.
<!--more-->

When starting Cmdlet at first code is nice and clean one method which make one thing. Next new switches starts to appear and code become messy. The main idea of PSCmdlet is to split functionality into many functions which can decide if they should execute or not.


## Quick Start

### Create Cmdlet class

 - Create new .NET Standard library
 - Add reference to **ProductivityTools.PSCmdlet**
 - Deliver from from **PSCmdlet.PSCmdletPT**
 - Add CmdletAttribute with module command **[Cmdlet(VerbsCommon.Get, "AssignedItems")]**

 Class name is not important. Value in the **Cmdlet** attribute will be used to invoke module from PowerShell (in our case Get-AssignedItems)

````c#
[Cmdlet(VerbsCommon.Get, "AssignedItems")]
public class TimeTrackingCmdlet :PSCmdlet.PSCmdletPT
{

}
````

### Generate ProcessRecord override

Generate ProcessRecord override. This method called when we are invoking the Cmdlet

![Generate override](Images/GenerateOverrides.png) 

Now you could write ``Console.Write("Hello")`` in this method and run it. To do it check **Debug** section.

![Generate override](Images/HelloWorld.png) 
```c#
    [Cmdlet(VerbsCommon.Get, "AssignedItems")]
    public class TimeTrackingCmdlet : PSCmdlet.PSCmdletPT
    {
        protected override void ProcessRecord()
        {
            Console.WriteLine("Hello");
            base.ProcessRecord();
        }
    }
```

### Create commands

Next step is creating commands. The best way to have it organized is to create new Commands directory and put all command there. 

![Commands in solution](Images/CommandsInSolution.png)

Each command should deliver from **PSCmdlet.PSCommandPT<T>**. Generic T should be type of Cmdlet so in described case: **TimeTrackingCmdlet**. Additionally command should implement abstract PSCmdletPT abstract methods.

- **Condition** tell us if command should be executed, lets start with ``true``. 
- **Invoke** is main body of command, for now lets write something on the screen.

```c#
    public class TimeTrackingCommandAll : PSCmdlet.PSCommandPT<TimeTrackingCmdlet>
    {
        public TimeTrackingCommandAll(TimeTrackingCmdlet cmdletType) : base(cmdletType)
        {
        }

        protected override bool Condition => true;

        protected override void Invoke()
        {
            Console.WriteLine("Hello from TimeTrackingCommandAll");
        }
    }
 ```

 Next commands should be used in PSCmdlet and use it:

```c#
    [Cmdlet(VerbsCommon.Get, "AssignedItems")]
    public class TimeTrackingCmdlet : PSCmdlet.PSCmdletPT
    {
        public TimeTrackingCmdlet()
        {
            base.AddCommand(new TimeTrackingCommandAll(this));
        }

        protected override void ProcessRecord()
        {
            Console.WriteLine("Hello");
            base.ProcessCommands();
            base.ProcessRecord();
        }
    }

```

If you would like to check how it is working check **debug** section.



## Help
Module by default adds Help parameter which writes content of the Parameter and Cmdlet attribute.

```c#
[Cmdlet(VerbsCommon.Get, "MasterConfiguration")]
public class GetMasterConfiguration :PSCmdlet.PSCmdletPT
{
     [Parameter(HelpMessage ="It prints whole configuration")]
     SwitchParameter All { get; set; }

     public GetMasterConfiguration()
     {
         this.AddCommand(new GetConfiguration(this));
     }

     protected override void ProcessRecord()
     {
        base.ProcessCommands();
     }
}
```


## Debugging

To be able to debug application first we need to publish our project. This is because by default VS doesn't copy all references to bin directory.

Invoke in the Package Manager ``dotnet publish``

In the project properties in the Start external program put:

```
C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe
```

And for the command line parameters something similar to:

```
-noexit -command "-noexit -command "import-module D:\GitHub\ProductivityTools.AzureDevOps.TimeTracking\ProductivityTools.AzureDevOps.TimeTracking\bin\Debug\netstandard2.0\publish\ProductivityTools.AzureDevOps.TimeTracking.dll"
```

![Debug properties](Images/DebugProperties.png)



Then after running project using F5, new PowerShell window will show up, thanks to application arguments, module will be imported and you will be able to use the command defined in ``cmdlet`` attribute.

This of course need from you make ``dotnet publish`` always before debugging. 

![Command invoke](Images/GetAssignedItemsFirst.png)