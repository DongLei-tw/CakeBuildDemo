# A quick starter with Cake Build

## 0. Dependency

Since the C# project is built from .Net framework 4.7, please make sure you have [.Net framework 4.7](https://support.microsoft.com/en-us/help/3186497/the-net-framework-4-7-offline-installer-for-windows) Installed.


## 1. Setup Cake Build (on windows)

### a. Install the bootstrapper

Run following command line to download the bootstrapper script 

```powershell
Invoke-WebRequest https://cakebuild.net/download/bootstrapper/windows -OutFile build.ps1
```

or make a copy from the [source file ](https://github.com/cake-build/resources/blob/develop/build.ps1). 

### b. Create a Cake script

Add a cake script called `build.cake` to the same location as the bootstrapper script that you downloaded.

```powershell
var target = Argument("target", "Default");

Task("Default")
  .Does(() =>
{
  Information("Hello Cake!");
});

RunTarget(target);
```

### c. Test your script
Run the Cake script with powersehll 
```powershell
./build.ps1
```


## 2. Write your customise build

### a. Clean build derictory


```powershell
var target = Argument("target", "Default");

var buildDir = MakeAbsolute(Directory("./Build"));

Task("Default")
  .Does(() =>
{
  Information("Hello Cake!");

  CleanDirectory(buildDir);
});


RunTarget(target);
```

### b. Build solution

```powershell
...

var target = Argument("target", "Build");

...

Task("Build")
  .IsDependentOn("Default")
  .Does(() => {
    var settings = new MSBuildSettings()
                        .WithProperty("OutputPath", buildDir.FullPath);

    MSBuild("./CakeDemo.sln", settings);
});

...
```

### c. Run Unit Tests with Nunit console runner

```powershell
#tool nuget:?package=NUnit.ConsoleRunner&version=3.4.0
...

Task("Tests")
  .IsDependentOn("Build")
  .Does(() => {
    var testsPath = buildDir.FullPath + "/UnitTests.dll";
        NUnit3(testsPath, new NUnit3Settings {
            NoResults = true
        });
  });
...
```

### d. Code coverage report (OpenCover + ReportGenerator)

```powershell
#tool nuget:?package=OpenCover&version=4.7.922
#tool nuget:?package=ReportGenerator&version=4.0.0.0

var target = Argument("target", "ReportGenerator");

...

Task("OpenCover")
  .IsDependentOn("Build")
  .Does(() => {
    var testAssemblies = buildDir.FullPath + "/UnitTests.dll";
    var outputPath = buildDir.FullPath + "/GeneratedReports/";
    var outputFile = new FilePath(outputPath + "my-test-result.xml");

   Information("xml output path: {0}", outputFile);
   EnsureDirectoryExists(outputPath);
    
    OpenCover(tool => {
      tool.NUnit3(testAssemblies, new NUnit3Settings {
            NoResults = true
        });
    },
    outputFile,
    new OpenCoverSettings()
        .WithFilter("+[Cake*]*")
    );
  });

Task("ReportGenerator")
  .IsDependentOn("OpenCover")
  .Does(() => {
    var reportPath = buildDir.FullPath + "/GeneratedReports/ReportsHistory";
    var reportFilePath = buildDir.FullPath + "/GeneratedReports/my-test-result.xml";
    var reportOutPath = buildDir.FullPath + "/GeneratedReports/ReportGeneratorOutput";

    var reportGeneratorSettings = new ReportGeneratorSettings()
    {
      HistoryDirectory = new DirectoryPath(reportPath)
    };
 
    ReportGenerator(reportFilePath, reportOutPath, reportGeneratorSettings);
});

...
```

## 3. Extend your own build steps

https://cakebuild.net/docs/


