source "$(pwd)/common.sh"

# Get the version
commonPropsXml="${rootFolder}common.props"
version=`grep -E -o -e '<Version>.+</Version>' $commonPropsXml | sed 's/<Version>//g'|sed 's/<\/Version>//g'|awk '{print $1}'`

# Publish all packages
for  project in ${projects[@]};do 
    projectName=${project##*"/"}
    dotnet nuget push "${projectName}.${version}.nupkg" -s https://api.nuget.org/v3/index.json --api-key "API_KEY"
done

# Go back to the pack folder
cd $packFolder
