# csf
Demo C# framework for testing


### Run all tests
```bash
$ dotnet test
```

### Running tests by categories
```bash
$ dotnet test --filter TestCategory=API

$ dotnet test --filter TestCategory=UI

$ dotnet test --filter TestCategory=graphql
```



### [Filtering tests](https://docs.microsoft.com/en-us/dotnet/core/testing/selective-unit-tests?pivots=nunit)
```bash
dotnet test --filter UiDemoTest

dotnet test --filter Name~UiDemoTest

dotnet test --filter Priority=1
```
