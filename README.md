### Publish (create a single .exe file)
dotnet publish -r win-x64 -c Release /p:PublishSingleFile=true

### Run example
.\Console.exe "Example\test.xml" "Example\output.xml" "auraportal" "ap"