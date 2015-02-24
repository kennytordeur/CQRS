
msbuild\msbuild ..\src\Torken.CQRS.Core\Torken.CQRS.Core.csproj /p:Configuration=Release 

nuget\nuget pack ..\src\Torken.CQRS.Core\Torken.CQRS.Core.csproj -IncludeReferencedProjects  -Prop Configuration=Release >>log.txt