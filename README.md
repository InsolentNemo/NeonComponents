
# NeonComponents

This library was made for creating some fancy visual components in C# based on the .NET Framework 4.8.
NeonComponents supports theme and language management by using .resx resource files as individual interface.




## Installation

There is no released version yet, so you have to compile this library by yourself.

You can compile this library with Visual Studio if you have no other compiler installed yet.
Just clone this repository and open this project as C#-project in Visual Studio and run it.
Your library will be saved in `/bin/Debug/NeonComponents.dll`.

At least add this library to your projects dependencies.
## Usage/Examples

Most of all public methods are documented.

Simple example for creating a form by using NeonForm:

```csharp
NeonForm myForm = new NeonForm();
myForm.Title = "My fancy NeonForm";
myForm.Maximizable = false;

NeonButton myButton = new NeonButton();
myButton.Text = "My fancy NeonButton";
myButton.Location = new Point(myForm.MainPanel.LeftSpace, myForm.MainPanel.TopSpace);

myForm.MainPanel.Controls.Add(myButton);
```

You're able to access the window state buttons.
By the following example you can add a window state button to the form.

```csharp
NeonWindowStateButton myWSB = new NeonWindowStateButton(myForm.WindowPanel, WindowStateButton.Custom);
myWSB.Text = "?";
myForm.WindowPanel.AddWindowStateButton(myWSB);
```


## Screenshots

#### Form from example

![App Screenshot](https://img001.prntscr.com/file/img001/BSlSVbq9TAqKv7HLuLVCgw.png)

#### My current project "Scourge" using NeonComponents

![App Screenshot](https://img001.prntscr.com/file/img001/EbuQRIe8SjO3itIRH-asOg.png)
## License

[GNU General Public License, Version 2](https://www.gnu.org/licenses/old-licenses/gpl-2.0.de)
