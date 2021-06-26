$BuildDirectory = "$PSScriptRoot\build"
Set-Location -Path $PSScriptRoot

function Log {
    param (
        $StringOutput
    )

    $Time = Get-Date -Format "dd/MM/yyyy HH:mm"
    Write-Output "$Time $StringOutput"
}

#Check build folder if it is exsist
if ((Test-Path -Path $BuildDirectory) -eq $false){
    Log("Creating Build Directory")
    New-Item $BuildDirectory -ItemType Directory
}
else {
    Log("Build Folder Found")
    foreach ($item in Get-ChildItem $BuildDirectory){
        Log("Deleting $item")
        Remove-Item "$BuildDirectory\$item" -Recurse
    }
}

# Do DotNetBuild
Log("Restoring Project")
dotnet.exe restore

Log("Building Project")
dotnet.exe publish --configuration Release --output $BuildDirectory
