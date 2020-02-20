source "$(pwd)/common.sh"

# Rebuild all solutions
for solution in ${solutions[@]};do
    solutionFolder="${rootFolder}${solution}"
    cd $solutionFolder && dotnet restore
done

# Create all packages
for  project in ${projects[@]};do 
    projectFolder="${rootFolder}${project}"
    cd $projectFolder
    rm -r "${projectFolder}/bin/Release" && dotnet msbuild /t:pack /p:Configuration=Release /p:SourceLinkCreate=true
    if [ "$?" != 0 ] ; then
        echo "Packaging failed for the project: ${projectFolder}"
        exit $?
    fi

    # Copy nuget package
    projectName=${project##*"/"}
    projectPackPath="${projectFolder}/bin/Release/${projectName}.*.nupkg"
    mv $projectPackPath $packFolder
done
 
# Go back to the pack folder
cd $packFolder