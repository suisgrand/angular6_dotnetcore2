Using following technologies :
-	Asp.net core 2.2
-	Angular 6 (Single Page Architecture)

The database is composed of 2 objects :  
-	TaskUser
-	Login
-	Where many taskUsers can belong to 1 login object
-	 InMemory: I used netCore built-in in-memory feature. The in-memory database is created on the startup.cs file, along with the login record where user-id = “test” and password is “pwd123” 

Code Architeture : 
-	Server
-	Entity framework. The dbContext object is under the « persistence » folder
-	Dependency injection
o	3 objects are used for dependency injections :
	UnitOfWork (used to save any change(s)), loginRepository, TaskUserRepository
-	repository pattern
o	There is a IRepository folder (for the interfaces) and a Repository folder (code implementation)
o	I chose to have a specific repository for each object
-	There are 2 controllers for this test : for login and the taskUser
-	I created taskUserResource (resource folder) when a new taskUser is being submitted.
-	Angular 6 :
-	5 components used for this project :
o	Home (defaulted when creating a new dotnet solution with angular)
o	navMenu (same)
o	login
o	tasks
o	task-detail
-	i created 2 services (1 for tasks, 1 for login) to handle any Api call between the web page and the server.
-	I created 2 typescript models (1 for login, 1 for taskUser)

Security  (JWT):
-	The JWT mechanism in configured on the startup.cs file
-	On the loginController, assuming that the user logins succesfully, a token will be created and sent back to the web page
-	next, the web page would save that token locally (on user’s browser)
-	then, on each api request when an authentication is needed, the web page will wrap up the API request with the token saved locally. I used the angular HttpInterceptor feature.
