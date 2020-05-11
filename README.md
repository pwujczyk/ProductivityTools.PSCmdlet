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

# ProductivityTools.PSCmdlet

It is base class for PowerShell Cmdlets. It provides structure of the commands and helps to write modular and clean.

After adding reference to ProductivityTools.PSCmdlet new class should be crated which will deliver from PSCmdlet.PSCmdletPT.

````c#
[Cmdlet(VerbsCommon.Get, "MasterConfiguration")]
public class GetMasterConfiguration :PSCmdlet.PSCmdletPT
{

}
````

Next step is creating commands. The best way to have it organized is to create new Commands directory and put all command there. Each command should deliver from PSCmdlet.PSCommandPT<T>. Generic T should be type of Cmdlet so in described case: GetDownloadExamination. Additionally command should implement abstract PSCmdletPT abstract methods.

For further purpose lets add some code to Invoke method

```c#
public class GetConfiguration : PSCmdlet.PSCommandPT<GetMasterConfiguration>
{
     protected override bool Condition => true;

     public GetConfiguration(GetMasterConfiguration getMasterConfiguration) : base(getMasterConfiguration) { }

     protected override void Invoke()
     {
         Console.Write("Printed");
     }
 }
 ```

 Next commands should be used in PSCmdlet and use it:

```c#
[Cmdlet(VerbsCommon.Get, "MasterConfiguration")]
public class GetMasterConfiguration :PSCmdlet.PSCmdletPT
{
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

To debug fast module in the project properties in the Start external program put:

```
C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe
```

And for the command line parameters something similar to:

```
-noexit -command "import-module D:\ProductivityTools.Module\src\ProductivityTools.Module\bin\Debug\ProductivityTools.Module.dll"
```

Then after running project using F5 new powershell window will show up and you will be able to use the command:


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
