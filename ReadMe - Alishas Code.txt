Variables Named In my Part of the Code

LibraryMembers -> NewUser for any user created/accessed

dbo.LibraryMembers is used is signup and login process

LibraryMember has several variables;
	Username
	FirstName
	Surname
	LibraryID	(NEVER USED) 
	Email
	Role
	UserPass	
	Balance 	(NEVER USED)
	CardNumber	(NEVER USED)
	FirstQuestion
	FirstAnswer
	SecondQuestion
	SecondAnswer

The table defaults Role to user on create account

My namespace was PrototypeDatabase so any pages need to be changed to your namespace as well as any database strings. 

Test Accounts

Username	Password	Email
Admin		test		testadmin@test.com
Librarian	test		testlibrarian@test.com	
User		test		testuser@test.com