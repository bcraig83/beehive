# Introduction

A little sample application that highlights a few testing concepts in dot net core. First off, we are using the 'real' dependency graph for testing the application, and then only replacing the 'boundaries' with stubs or mocks. This makes this testing strategy much more resilient to refactoring.

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

The tests prove the basic functionality, including confirming that we are persisting the correct data to the database and that we are sending the correct parameter over to the "API".

# Version 2

A second version has been added, "Beehive.Api2" as well as the accompanying test project. The main differences:

- This has been written in .net 6.
- It uses a CustomWebApplicationFactory instead of a fixture.
- I've abandoned some of the clean architecture concepts, just to highlight a different architecture style. Specifically:
  - I have separated the application as per the template Web API project, as opposed to features and layers.
  - I have abandoned the repository and unit of work, instead leaning on the Db context to act as a repository.
- I've had to make the "Program" class partial, to allow it to be used in the CustomWebAplicationFactory.
- I've included extension methods to help with replacing services in the dependency graph.

I think this may be a preferred approach to the original. You can see how all of the tests have been implemented using this apporach, compared to the original. Also, this approach lets us test at the API level, which may be required if we are plugging into .net middleware.