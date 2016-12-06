# net-core-styleguide
An opinionated .net core based style guide.
## Purpose
Getting a project out the door can be deceptively difficult.  Usually about half way into it you realize you may have bit off more than you can chew.  Things get further excerbated when you realize you haven't done enough testing or you arent handling errors properly or your multi-tier architecture is turning into a plate of spaghetti.

I know your pain.

This style guide is a set of best practices based on the many mistakes I have made in the past.  If you are looking for more than a trivial project example to get your project started then this may be what you are looking for.

In **no way** am I personally taking credit for the contents of this style guide. These are all the things I have learned by standing on the proverbial *shoulders of giants*.  I will do my best to attribute others as best I can.
## Table of Contents
1. [Solution Layout](*Solution Layout)
2. Framework Versioning
3. Entity Framework
4. Domain Driven Design
5. Startup Pipeline
6. MediatR
7. Fluent Validation
8. Unit Tests
9. Integration Tests

###Solution Layout
A typical N-Tier type layout might look something like this.
'''
project
|    README.md
|
|____src
     |    App.Core
     |    App.Data
     |    App.Services
     |    App.Web
     |    App.Framework
     |    
     |____Testing
