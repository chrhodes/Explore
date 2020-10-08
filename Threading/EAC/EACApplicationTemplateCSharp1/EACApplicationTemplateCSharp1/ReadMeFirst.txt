EAC Application Template ReadMe First
-------------------------------------
Once you have created a new solution using the EACApplicationTemplate{CSharp,VB} Visual Studio Template
there are a few things that need to be done to begin production your solution.

1. Change the Startup Project to the TestWindowConsole
	Project/Properties/Debug - Start External Program

2. Paste the contents of TestWindowConsoleParameter.xml <AppConfigData> element into the TestWindowConsole parameter window.

3. Run the application, which will load the TestWindowConsole, and select the assembly that was built by clicking the Browse button

4. Verify basic functionality by loading one of the following UserControl by selecting it from the Object drop-down list.
	- ucApplicationMain
		This UserControl inherits from ucScreenBase and ...