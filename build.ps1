function Log {
    param (
        $StringOutput
    )

    $Time = Get-Date -Format "dd/MM/yyyy HH:mm"
    Write-Output "$Time $StringOutput"
}

function LogError {
    param (
        $StringOutput
    )

    $Time = Get-Date -Format "dd/MM/yyyy HH:mm"
    Write-Error "$Time $StringOutput"
}

$BuildDirectory = "$PSScriptRoot\build"
Set-Location -Path $PSScriptRoot
$Date = Get-Date

#Check build folder if it is exsist
if (-not(Test-Path -Path $BuildDirectory)){
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

#Build DotNet
Log("Running Dotnet Clean")
dotnet.exe clean

Log("Restoring Project")
dotnet.exe restore

Log("Building Project")
dotnet.exe publish --configuration Release --runtime win10-x64 --self-contained false --output "$BuildDirectory\LapsRemote" 

if (-not(Test-Path -Path "$BuildDirectory\LapsRemote\LapsRemote.exe")){
    LogError("Unable To Build The Project Make Sure You Have .net5.0")
}

#Create Archive
Log("Creating Portable Archive")
Get-ChildItem -Path "$BuildDirectory\LapsRemote" | Compress-Archive -CompressionLevel Fastest -DestinationPath "$BuildDirectory\LapsRemoteArchived.zip"

if (-not(Test-Path -Path "$BuildDirectory\LapsRemoteArchived.zip")){
    LogError("Unable To Create A Portable Archive")
}

#Inno Setup Installer

$InnoExecutableDirectory = "${env:ProgramFiles(x86)}\Inno Setup 6\ISCC.exe"

if (Test-Path -Path $InnoExecutableDirectory){
    Log("Inno Setup Excecutable Found. Creating Installer")
    Start-Process -FilePath $InnoExecutableDirectory -ArgumentList """$PSScriptRoot\InnoSetup.iss""" -NoNewWindow -Wait
    Log("Setup Created")
}
else {
    LogError("Inno Setup Not Found I ${env:ProgramFiles(x86)}\Inno Setup 6. Skipping...")
}

foreach ($ItemFile in Get-ChildItem $BuildDirectory | Where-Object {$_.Name.EndsWith(".exe") -or $_.Name.EndsWith(".zip")} | Get-FileHash){
    Log("$([System.IO.Path]::GetFileName($ItemFile.Path)) | $($ItemFile.Algorithm) | $($ItemFile.Hash)");
    "$([System.IO.Path]::GetFileName($ItemFile.Path)) | $($ItemFile.Algorithm) | $($ItemFile.Hash)" | Out-File "$BuildDirectory\LapsRemote_Hash.txt" -Encoding utf8
}

Log("Build Is Complete! You Can See Your Files Here $BuildDirectory")