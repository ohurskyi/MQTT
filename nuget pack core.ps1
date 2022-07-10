$coreProj = ".\src\MessagingLibrary\MessagingLibrary.Core\MessagingLibrary.Core.csproj"
$artifacts = ".\artifacts"
$version = "1.0.1-alpha"

dotnet clean $coreProj -c Release
dotnet build $coreProj -c Release
dotnet pack $coreProj -c Release -p:packageVersion=$version -o $artifacts --no-build