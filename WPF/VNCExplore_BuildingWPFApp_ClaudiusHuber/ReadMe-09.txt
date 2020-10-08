09 - Validating User Input

Validation in WPF
Validate user input with INotifyDataErrorInfo
Display errors by using an error template
Refactor validation logic into a base class
Build logic to validate Data Annotations
Enable and disable the save button

Changes

Review Code changes and add notes.

Copy FriendOrganizer.Presentation.Friend08 to ...Friend09

FriendOrganizer.Presentation.Friend09
	Friend09DetailView.Xaml
		Added TargetNullValue

	Friend09DetailViewModel.cs
		Added FriendWrapper stuff

FriendOrganizer.Domain
	Friend05 -> Friend09
		Added [EmailAddress] DataAnnotation
	NB. This caused too many ripple effects.  Just added to Friend05.

VNCExplorer_FriendOrganizer
	Reference FriendOrganizer.Presentation.Friend09
	App.Xaml
		Added Application.Resources Style for TextBox
		Added DispatcherUnhandledException handler
			
	App.Xaml.cs - ConfigureModuleCatalog()
		Add Friend09Module
		Added DispatcherUnhandledException handler

	MainWindowDxLayout.Xaml
		Add Region09 stuff

VNCExplorer_FriendOrganizer.Core
	Add Region09, Region09Detail

