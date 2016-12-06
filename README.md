# net-core-styleguide
An opinionated .net core based style guide.
## Purpose
Getting a project out the door can be deceptively difficult.  Usually about half way into it you realize you may have bit off more than you can chew.  Things get further excerbated when you realize you haven't done enough testing or you arent handling errors properly or your multi-tier architecture is turning into a plate of spaghetti.

I know your pain.

This style guide is a set of best practices based on the many mistakes I have made in the past.  If you are looking for more than a trivial project example to get your project started then this may be what you are looking for.

In **no way** am I personally taking credit for the contents of this style guide. These are all the things I have learned by standing on the proverbial *shoulders of giants*.  I will do my best to attribute others as best I can.
## Table of Contents
1. [Solution Layout](*Solution Layout)
2. [Framework Versioning](*Framework Versioning)
3. [Entity Framework](*Entity Framework)
4. [Domain Driven Design](*Domain Driven Design)
5. [Startup Pipeline](*Startup Pipeline)
6. [MediatR](*MediatR)
7. [Fluent Validation](*Fluent Validation)
8. [Unit Tests](*Unit Tests)
9. [Integration Tests](*Integration Tests)

###Solution Layout
.Net Core (as it is currently named) has gone through several iterations [and appears to be preparing for yet another one](https://docs.microsoft.com/en-us/dotnet/articles/core/tools/project-json). What we want to do is keep our solution as simple as possible to allow for inevitable changes to the framework yet still robust enough for larger code bases and future upgrades.

The one big gotcha that I discovered here was that for your project to compile correctly (especially if you are using a CI build server) all of your projects need to be in a *single* parent folder.  For this project we will be putting all projects in *src* folder.  We will also create a *Testing* folder that resides in the *src* folder as well. 

.Net Core solutions contain a global.json file that tells the compiler which folders to look in for compiling.

#### StyleGuide.Core
This project will contain our domain classes, enums, and value objects.  Although this project references Entity Framework, it does so only because we also are referencing Delegate Decompiler which has a dependency on EF.  As you will see later, Delegate Decompiler makes working with computed properties a breeze.

The domain folder will contain only one level of sub-folders that represent the aggregate roots the domain.  These subfolders will house the domain classes.

#### StyleGuide.Data
For smaller solutions this project could be combined into the [core](*StyleGuid.Core) project.  This however can be problematic for 2 reasons.
1. Small project have a way of growing very fast.
2. EF has gone through quite a few changes over the years and is currently being completely [rewritten](*https://github.com/aspnet/EntityFramework/wiki/Roadmap).  Having as few projects directly dependant on EF will make upgrading that much easier.

This project will contain our EF6 mapping, migration, configuration, and DbContext classes.

## Framework Versioning

## Entity Framework

## Domain Driven Design

## Startup Pipeline

## MediatR

## Fluent Validation

## Unit Tests

## Integration Tests

