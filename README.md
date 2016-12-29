# net-core-styleguide
An opinionated .net core based style guide.
## Purpose
Getting a project out the door can be deceptively difficult.  Usually about half way into it you realize you may have bit off more than you can chew.  Things get further exacerbated when you realize you haven't done enough testing or you arent handling errors properly or your multi-tier architecture is turning into a plate of spaghetti.

I know your pain.

This style guide is a set of best practices based on the many mistakes I have made in the past.  If you are looking for more than a trivial project example to get your project started then this may be what you are looking for.

This project will try to adhere to Domain Driven Design(DDD) principles, Test Driven Development(TDD) *light*, and Command Query Responsibility Segregation(CQRS) patterns. TDD *light* is a term I use.  I try and write out the necessary tests as soon as I can wrap my head around the specifics of a feature.  I am not a TDD purist.

**Note:** This is a monolith based project that uses a single relational database for persistence.  At some point I will create a new repo and refactor this project as a micro services based project.

In **no way** am I personally taking credit for the contents of this style guide. These are all the things I have learned by standing on the proverbial *shoulders of giants*.  I will do my best to attribute others as best I can.
## Acknowledgments
##### Jimmy Bogard
##### Julie Lerman
##### Eric Evans
## Table of Contents
1. [The Project](#the-project)
2. [Solution Layout](#solution-layout)
3. [Framework Versioning](#framework-versioning)
4. [Entity Framework](#entity-framework)
5. [Domain Driven Design](#domain-driven-design)
6. [Startup Pipeline](#startup-pipeline)
7. [MediatR](#mediatr)
8. [Fluent Validation](#fluent-validation)
9. [Unit Tests](#unit-tests)
10. [Integration Tests](#integration-tests)

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


## Solution Layout
.Net Core (as it is currently named) has gone through several iterations [and appears to be preparing for yet another one](https://docs.microsoft.com/en-us/dotnet/articles/core/tools/project-json). What we want to do is keep our solution as simple as possible to allow for inevitable changes to the framework yet still robust enough for larger code bases and future upgrades.

The one big gotcha that I discovered here was that for your project to compile correctly (especially if you are using a CI build server) all of your projects need to be in a *single* parent folder.  For this project we will be putting all projects in *src* folder.  We will also create a *Testing* folder that resides in the *src* folder as well. 

.Net Core solutions contain a global.json file that tells the compiler which folders to look in for compiling.

### LMS.Core
This project will contain our domain classes, enums, and value objects.  Although this project references Entity Framework, it does so only because we also are referencing Delegate Decompiler which has a dependency on EF.  As you will see later, Delegate Decompiler makes working with computed properties a breeze.

If you don't really see a need for computed properties you can always leave these dependencies out.  The big take away is that we want our domain models to have as little dependency on our ORM as possible.  This will make re-factoring easier down the road.

The domain folder will contain only one level of sub-folders that represent the aggregate roots the domain.  These subfolders will house the domain classes.

### LMS.Data
For smaller solutions this project could be combined into the [core](*LMS.Core) project.  This however can be problematic for 2 reasons.

1. Small projects have a way of growing very fast.
2. EF has gone through quite a few changes over the years and is currently being completely [rewritten](https://github.com/aspnet/EntityFramework/wiki/Roadmap).  Having as few projects directly dependant on EF will make upgrading that much easier.

This project will contain our EF6 mapping, migration, configuration, and DbContext classes.

### LMS.Web
The meat of our project will be contained here.  This project will contain:

1. Request pipeline and routing
2. Authentication
3. Authorization
3. Validation
4. CQRS

You can view the basic layout from [this](https://github.com/trevorchunestudy/net-core-styleguide/tree/4684cc32a125683b566159158766e8d4fac2be5f) commit.
## Framework Versioning

## Entity Framework

add-migration init -StartUpProjectName StyleGuide.Data

## Domain Driven Design

## Startup Pipeline

## MediatR

## Fluent Validation

## Unit Tests
Code should be testable but not written for tests.  
## Integration Tests

