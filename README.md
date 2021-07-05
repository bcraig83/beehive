A little sample application that highlights a few testing concepts in dot net core. First off, we are using the 'real' dependency graph for testing the application, and then only replacing the 'boundaries'. This makes this testing strategy much more resilient to refactoring.

Some features include:

- Grabbing any class that you need to test, and automatically having it's dependency graph filled out.
- The ability to replace any interface with a stub, spy or mock. Examples of each can be seen in the tests.
- Some helper methods, such as clearing down the database. 

Most of this functionality can be found in the "Fixture" class.

The application follows the 'clean architecture' style of application.

I've also employed the "Unit of Work" pattern for bundling repositories together and allowing us to save changes to the database in a transactional manner.

The application is wired up in the standard way, from the "ConfigureServices" method in the Startup class. In the constructor for the "Fixture" class, we can see how this is exercised, and then various 'boundary' components are replaced, such as the database and the API client.

The imagined functionality here is quite simple, and largely implemented in the DrumService class.

- A request comes in to get a drum of honey.
- We call out to an (imagined) API via the DrumClient. This is simply a dummy implementation.
- The response is persisted into the database.
- The response is also returned to the caller.
