# TestTask1
📄 How to Use the "AddComment" Revit Add-in
Follow these steps to install and run your Revit add-in correctly:

1. 🔧 Modify the .addin File
Open the .addin file included with your project and update the Assembly path to point to the compiled .dll file on your computer.
Example:
<Assembly>C:\Path\To\Your\Compiled\DLL\TestTask1.dll</Assembly>
✅ Make sure the path is correct and points to the actual location of your built DLL.

2. 📁 Place the .addin File in the Revit Add-ins Folder
Copy your modified .addin file to the Revit Add-ins folder corresponding to your Revit version:

C:\ProgramData\Autodesk\Revit\Addins\2023
ℹ️ Replace 2023 with your version of Revit, if different.

3. ▶️ Run Revit and Open a Test Project
Start Autodesk Revit.
Open a project file that contains at least one rebar element.
Your add-in should execute automatically when the document opens (if you're using DocumentOpened event).
Check the comments field of your rebars — it should now contain custom text like:

Ø12, Grade 60, L=2000
