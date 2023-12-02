# About

Code which will write out all project names in a existing Visual Studio solution. Currently set to read the current solution.

Change this

```csharp
await GetProjectFiles(GetSolutionInfo().FullName);
```

To

```csharp
await GetProjectFiles(@"C:\DotNet\SomeSolution");
```

