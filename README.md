# net-core-e2e
An opinionated non-trivial .net core end to end sample project
## Purpose
Getting a project out the door can be deceptively difficult.  Usually about half way into it you realize you may have bit off more than you can chew.  Things get further exacerbated when you realize you haven't done enough testing or you arent handling errors properly or your multi-tier architecture is turning into a plate of spaghetti.

I know your pain.

This style guide is a set of best practices based on the many mistakes I have made in the past.  If you are looking for more than a trivial project example to get your project started then this may be what you are looking for.

This project will try to adhere to Domain Driven Design(DDD) principles, Test Driven Development(TDD) *light*, and Command Query Responsibility Segregation(CQRS) patterns. TDD *light* is a term I use.  I try and write out the necessary tests as soon as I can wrap my head around the specifics of a feature.  I am not a TDD purist.

This project is documented in the [wiki](https://github.com/trevorchunestudy/net-core-e2e/wiki). TODO: explain how the project is setup as a step-by-step via dev branch commits.

**Note:** This is a monolith based project that uses a single relational database for persistence.  At some point I will create a new repo and refactor this project as a micro services based project.

In **no way** am I personally taking credit for the contents of this style guide. These are all the things I have learned by standing on the proverbial *shoulders of giants*.  I will do my best to attribute others as best I can.
## Acknowledgments

##### Jimmy Bogard
* [A simple explanation of the CQRS/MediatR implementation patterns that solves the 'GetBy*' service soup](https://lostechies.com/jimmybogard/2016/10/27/cqrsmediatr-implementation-patterns/)
* [The MediatR library](https://github.com/jbogard/MediatR)
* [A testing pattern that will change your life!](https://lostechies.com/jimmybogard/2016/10/24/vertical-slice-test-fixtures-for-mediatr-and-asp-net-core/)
* [Jimmys new site](https://jimmybogard.com/)

##### Julie Lerman
* [The 'everything Entity Framework' expert's blog.](http://thedatafarm.com/blog/)
* [A great course on bounded contexts and EF.](https://www.pluralsight.com/courses/entity-framework-enterprise-update)

##### Eric Evans
* [The blue book that should be on every developers desk.](https://www.amazon.com/Domain-Driven-Design-Tackling-Complexity-Software/dp/0321125215)

##### Patrick Lioi
* [Testing guru on Los Techies.](https://lostechies.com/patricklioi/)
* [Fixie testing library](https://github.com/fixie/fixie)

##### Rick Strahl
* [A great resource for anything .net.](https://weblog.west-wind.com/)

## The Project
Our goal is to develop a learning management system(LMS) API.  This API will be consumed by an angular 2 SPA and in the future a native iOS and Android mobile app.  The project requirments are the following:

1. The LMS has 3 types of users:
  * Administrator
  * Instructor
  * Student
2. An Administrator can:
  * Create/Edit all types of users.
  * Create curriculum types
3. An Instructor can:
  * Create/Edit curriculum
  * Create/Edit modules
  * Assign modules to curriculum
  * Assign Students to curriculum
  * View Student module progress and scores
4. A Student can:
  * Take modules
  * View their progress and scores

TODO: complete the rest of  the project requirements outline.
