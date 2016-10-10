#tool "GitVersion.CommandLine"
#tool "xunit.runner.console"
#addin "Cake.DocFx"
#tool "docfx.msbuild"

#load "helpers.cake"

///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument<string>("target", "Default");
var configuration = Argument<string>("configuration", "Release");
var framework = Argument<string>("framework", "netstandard1.6,net451");

///////////////////////////////////////////////////////////////////////////////
// GLOBAL VARIABLES
///////////////////////////////////////////////////////////////////////////////

var solutionPath = File("./src/Cake.ServiceOrchestration.sln");
var solution = ParseSolution(solutionPath);
var projects = solution.Projects;
var projectPaths = projects.Select(p => p.Path.GetDirectory());
var frameworks = GetFrameworks(framework);
var testAssemblies = projects.Where(p => p.Name.Contains(".Tests")).Select(p => p.Path.GetDirectory() + "/bin/" + configuration + "/" + p.Name + ".dll");
var artifacts = "dist/";
var testResultsPath = MakeAbsolute(Directory(artifacts + "./test-results"));
GitVersion versionInfo = null;

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup(ctx =>
{
	// Executed BEFORE the first task.
	Information("Running tasks...");
	versionInfo = GitVersion();
	Information("Building for version {0}", versionInfo.FullSemVer);
    Information("Building against '{0}'", framework);
});

Teardown(ctx =>
{
	// Executed AFTER the last task.
	Information("Finished running tasks.");
});

///////////////////////////////////////////////////////////////////////////////
// TASK DEFINITIONS
///////////////////////////////////////////////////////////////////////////////


Task("Clean")
	.Does(() =>
{
	// Clean solution directories.
	foreach(var path in projectPaths)
	{
		Information("Cleaning {0}", path);
		CleanDirectories(path + "/**/bin/" + configuration);
		CleanDirectories(path + "/**/obj/" + configuration);
	}
	Information("Cleaning common files...");
	CleanDirectory(artifacts);
});

Task("Restore")
	.Does(() =>
{
	// Restore all NuGet packages.
	Information("Restoring solution...");
	NuGetRestore(solutionPath);
    foreach(var project in projects.Where(IsProject)) {
        DotNetCoreRestore(project.Path.GetDirectory() + "/project.json");
    }
});

Task("Build")
	.IsDependentOn("Clean")
	.IsDependentOn("Restore")
	.Does(() =>
{
	Information("Building solution...");
	CreateDirectory(artifacts + "lib/");
    foreach(var f in frameworks) {
        CreateDirectory(artifacts + "lib/" + f);
        DotNetCoreBuild("./src/Cake.ServiceOrchestration/", new DotNetCoreBuildSettings {
            Framework = f,
            Configuration = configuration,
            OutputDirectory = artifacts + "lib/" + f
	    });
    }
	
});

Task("Generate-Docs").Does(() => {
	DocFx("./docfx/docfx.json");
	Zip("./docfx/_site/", artifacts + "docfx.zip");
});

Task("Post-Build")
	.IsDependentOn("Build")
	.IsDependentOn("Generate-Docs")
	.Does(() =>
{
	/*CreateDirectory(artifacts + "build");
	foreach (var project in projects.Where(p => p.Type != "{2150E333-8FDC-42A3-9474-1A3956D46DE8}")) {
		CreateDirectory(artifacts + "build/" + project.Name);
		var path = project.Path.GetDirectory() + "bin" + configuration + "/" + framework + "/" + project.Name;
		Information(path);
		CopyFiles(GetFiles(path + ".xml"), artifacts + "build/" + project.Name);
	}*/
});

Task("Run-Unit-Tests")
	.IsDependentOn("Build")
	.Does(() =>
{
	if (testAssemblies.Any()) {
		CreateDirectory(testResultsPath);

		var settings = new XUnit2Settings {
			NoAppDomain = true,
			XmlReport = true,
			HtmlReport = true,
			OutputDirectory = testResultsPath,
		};
		settings.ExcludeTrait("Category", "Integration");

		XUnit2(testAssemblies, settings);
	}
});

Task("NuGet")
	.IsDependentOn("Post-Build")
	.IsDependentOn("Run-Unit-Tests")
	.Does(() => {
		CreateDirectory(artifacts + "package/");
		Information("Building NuGet package");
		var nuspecFiles = GetFiles("./*.nuspec");
		var versionNotes = ParseAllReleaseNotes("./ReleaseNotes.md").FirstOrDefault(v => v.Version.ToString() == versionInfo.MajorMinorPatch);
        var content = GetContent(frameworks, artifacts + "lib/");
        Information(string.Join(Environment.NewLine, content.Select(c => c.Source)));
		NuGetPack(nuspecFiles, new NuGetPackSettings() {
			Version = versionInfo.NuGetVersionV2,
			ReleaseNotes = versionNotes != null ? versionNotes.Notes.ToList() : new List<string>(),
			OutputDirectory = artifacts + "/package",
            Files = content
		});
	});

///////////////////////////////////////////////////////////////////////////////
// TARGETS
///////////////////////////////////////////////////////////////////////////////

Task("Default")
	.IsDependentOn("NuGet");

///////////////////////////////////////////////////////////////////////////////
// EXECUTION
///////////////////////////////////////////////////////////////////////////////

RunTarget(target);