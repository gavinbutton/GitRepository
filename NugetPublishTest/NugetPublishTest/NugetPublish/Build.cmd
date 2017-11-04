rem "C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\Common7\Tools\VsDevCmd.bat"
rem cd %cd%
rmdir "Z:\LocalPackages\nugetpublishtest\1.0.2" /S /Q
rmdir "C:\DATA\Documents\Visual Studio 2017\Projects\NugetPublishTest\packages\NugetPublishTest.1.0.2" /S /Q
nuget pack ..\NugetPublishTest.csproj -Prop Configuration=Release -IncludeReferencedProjects
nuget add NugetPublishTest.1.0.2.nupkg -source Z:\LocalPackages 
pause