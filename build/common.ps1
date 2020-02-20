# COMMON PATHS

$rootFolder = (Get-Item -Path "./" -Verbose).FullName

# List of solutions

$solutionPaths = (
    "../modules/identity",
    "../modules/identityserver",
    "../modules/ocelot-management"

)