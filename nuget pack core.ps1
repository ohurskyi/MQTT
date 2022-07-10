$coreProj = ".\src\MessagingLibrary\MessagingLibrary.Core\MessagingLibrary.Core.csproj"
$artifacts = ".\artifacts"
dotnet clean $coreProj -c Release
dotnet build $coreProj -c Release
dotnet pack $coreProj -c Release -o $artifacts --no-build