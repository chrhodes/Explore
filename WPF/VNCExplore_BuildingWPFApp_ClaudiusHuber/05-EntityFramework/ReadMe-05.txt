05 - Entity Framework

Create a DbContext subclass
Add a code first migration
Add constraints
Create and seed the database

Changes

Moving things to CleanArchitecture Structure

FriendOrganizer.Domain
	Added Interface
		IFriend

FriendOrganizer.DataAccess04 is Being handled
	in Domain\FriendOrganizer.DomainServices.FriendDataService

		FriendDataService04Mock
		FriendDataService05

	and Persistence\FriendOrganizer.DataAccess05
		FriendOrganizerDbContext05
		Added EF code

FriendOrganizer.Model04 is Being handled
	in Domain\FriendOrganizer.Domain
		Model04
		Model05

	both now use IFriend interface
	