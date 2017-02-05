# net-core-styleguide
An opinionated .net core based style guide.
## Purpose
Getting a project out the door can be deceptively difficult.  Usually about half way into it you realize you may have bit off more than you can chew.  Things get further exacerbated when you realize you haven't done enough testing or you arent handling errors properly or your multi-tier architecture is turning into a plate of spaghetti.

I know your pain.

This style guide is a set of best practices based on the many mistakes I have made in the past.  If you are looking for more than a trivial project example to get your project started then this may be what you are looking for.

This project will try to adhere to Domain Driven Design(DDD) principles, Test Driven Development(TDD) *light*, and Command Query Responsibility Segregation(CQRS) patterns. TDD *light* is a term I use.  I try and write out the necessary tests as soon as I can wrap my head around the specifics of a feature.  I am not a TDD purist.

This project is documented in the wiki. TODO: explain how the project is setup as a step-by-step via dev branch commits.

**Note:** This is a monolith based project that uses a single relational database for persistence.  At some point I will create a new repo and refactor this project as a micro services based project.

In **no way** am I personally taking credit for the contents of this style guide. These are all the things I have learned by standing on the proverbial *shoulders of giants*.  I will do my best to attribute others as best I can.
## Acknowledgments
TODO: add links and examples for:
##### Jimmy Bogard
##### Julie Lerman
##### Eric Evans
##### Patrick Lioi
##### Rick Strahl

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
