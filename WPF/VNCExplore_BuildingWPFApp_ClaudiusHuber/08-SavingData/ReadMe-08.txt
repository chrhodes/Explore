08 - SavingData

The ICommand and MVVM
Add a SaveCommand Property to the FriendDetailViewModel
Save the Friend using the FriendDataService
Update the Navigation after Saving

Changes

Copy FriendOrganizer.Presentation.Friend07 to ...Friend08

VNCExplorer_FriendOrganizer
	Reference FriendOrganizer.Presentation.Friend09
	App.Xaml.cs - ConfigureModuleCatalog()
		Add Friend09Module


NB. We call it UpdateAsync not SaveAsync!

Review Code changes and add notes.