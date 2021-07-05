A little sample application that highlights a few testing concepts in dot net core. First off, we are using the 'real' dependency graph for testing the application, and then only replacing the 'boundaries'. This makes this testing strategy much more resilient to refactoring.

Some features include:

- Grabbing any class that you need to test, and automatically having it's dependency graph filled out.
- The ability to replace any interface with a stub, spy or mock. Examples of each can be seen in the tests.
- Some helper methods, such as clearing down the database. 

Most of this functionality can be found in the "Fixture" class.
