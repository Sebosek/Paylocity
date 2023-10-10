# What is this?

A project seed for a C# dotnet API ("PaylocityBenefitsCalculator"). 
It is meant to get you started on the Paylocity BackEnd Coding Challenge by taking some initial setup decisions away.

## Architecture
The service uses Onion Architecture style using CQRS architecture using mediator pattern.
Onion architecture uses multiple layers, quite similarly to N-layer architecture. However, the architecture is in the shape of a dart board.
In the center is the application domain made by entities, value objects, and domain exceptions.
On a layer above is an application layer. This layer contains application services responsible for handling the business use cases.
This layer also contains the domain service interfaces implemented in the outer layer called Infrastructure.
In the Onion Architecture, the application framework, in this case, ASP.NET, is just an implementation detail, and as such, it's just a part of the infrastructure.
One of the `Api` references are Guard Clauses; however, in most case, I've just used the `NullArgumentException` guard to check method pre-conditions.

## API design
The application came with a defined `v1` version in the URL, which is not entirely aligned with the RESTful approach and the expected result object.
The result object contains the `success` flag and `data` and `error` properties. However, I wouldn't recommend this approach because the HTTP status codes
already have defined semantic meaning, so this wrapper on result data is redundant.
This approach was quite popular ten years ago and/or at the Level 1 API maturity level by Leonard Richardson.
Therefore, the service contains `v2` endpoints and every new functionality is covered only there.
The `v1` schema is kept and working for backward compatibility (used by integration tests).
The goal of this challenge wasn't about API design, so most API endpoints aren't covered, only the necessary minimum.

## Requirements
The service can return employees with their dependents and dependents. To demonstrate some business restrictions, the service
contains a POST and DELETE operation to add and remove dependents. The paycheck calculation is probably pretty naive. The thing is, in the Czech Republic, we have a monthly paycheck, so bi-weekly paychecks are quite a rate. I would even say it is unique, even though Czech citizen law allows this form of payment.
With that said, I've consulted how it is supposed to work with OpenAI's Chat GPT, which gives me hints on handling a bi-weekly payment period during a year.

## Application flow
The controllers are (and should be) pretty slim. All application controllers inherit from `ApiBaseController`. Once the service receives a request,
the given action creates a command or query, which is handled by the application layer, and then, based on the business use case, the data are stored or read from
persistence. In this case, it is only in memory persistence. The result of the application use-case is usually `Either`, a monadic data structure that either contains a value or an error.
The result is returned to the consumer, either in the form of a successful HTTP status code (200, 201, 204) or in any form of an error HTTP status code (400, 404 for client errors) or 500 for internal service error.

## Tests
Part of the initial project was integration tests, not using the WebBuilder but rather running an instance of service.
The tests, as they are, pass without any change. The project is extended with a few unit and functional tests to cover paycheck functionality.
Intentionally, I've avoided using any mocking library. I was a long-time user of `Moq`; however, based on recent events, I'm not sure if I'm able,
or want to, use `Moq` any longer.
The current tests don't require something unique or special, so I've decided to go with plain Mock and Spies.

## Future plans
- Validations, there are currently (almost) no validations (FluentValidations)
- Split test project into two projects, one for unit tests and the second one for integration tests
- With that, I'm not fully happy with `ShouldExtensions`, it may get more attention
- Use `NSubstitude` for mocking
- The OpenAPI definition would also require more attention; the current config is a bare minimum
- Add authentication and even authorization would also be nice
- In-memory data storage is... NaiveIt wouldld be nice to store data persistently
