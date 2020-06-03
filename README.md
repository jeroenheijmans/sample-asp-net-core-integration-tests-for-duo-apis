# Sample Duo APIs with Integration Tests

Demonstrates how to run integration tests with one API calling another API.

## Tests

The main magic, and purpose of this repository, is in running the tests.

```sh
dotnet test SampleDuoApis.FooApi.Tests/SampleDuoApis.FooApi.Tests.csproj
```
Output should be along these lines:

```none
Starting test execution, please wait...

A total of 1 test files matched the specified pattern.
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: /home/jeroen/git/prive/sample-asp-net-core-integration-tests-for-duo-apis/SampleDuoApis.FooApi
info: SampleDuoApis.FooApi.RandomBeerController[0]
      Being asked for a random beer
info: SampleDuoApis.FooApi.BarService[0]
      Randomly picked id to retrieve beef for: fw
info: SampleDuoApis.FooApi.RandomBeerController[0]
      Randomly picked beer with id: fw

Test Run Successful.
Total tests: 1
     Passed: 1
 Total time: 1.1207 Seconds
```

This is _magical_ because it:

1. Spins up an in-memory `BarApi` server
1. Spins up an in-memory `FooApi` server
1. Overwrites the "typed HttpClient" `BarService` DI registration for `FooApi` (with a special `HttpClient` injected that "secretly" routes HTTP to the in-memory `BarApi`)
1. Runs a test that calls `FooApi`, which calls the `BarApi` via a `HttpClient` towards the second in-memory API

This is in contrast with `FooApi` calling a _running_ `BarApi`.

## Running

The APIs mimick a "real" setup.
You can also test this real setup by:

1. `dotnet run -p SampleDuoApis.BarApi/SampleDuoApis.BarApi.csproj`
1. `dotnet run -p SampleDuoApis.FooApi/SampleDuoApis.FooApi.csproj`
1. Then `curl http://localhost:5002`

This calls the running `FooApi` (at port `5002`), which in turn calls `BarApi` (at port `5000`).

In short: `Foo` is a "facade" for `Bar`.