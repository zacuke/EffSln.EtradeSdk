$sourceDir = "openapi"
$destDir = "EffSln.EtradeSdk"

$cwd = (Get-Location).Path

Get-ChildItem -Recurse -Path $cwd -Filter *.yaml | ForEach-Object {
    $inputFile = $_.FullName.Substring($cwd.Length).TrimStart("\")  
    $outputFile = $inputFile -replace "^$sourceDir", $destDir
    $outputFile = [System.IO.Path]::ChangeExtension($outputFile, ".cs")

    $parts = $outputFile  -split '\\'
 
    $fileName = $_.BaseName 
 
    $namespace = "$($parts[0]).$($parts[1])"
    if ($parts[2] -notmatch "cs$") {
        $namespace = "$namespace.$($parts[2])"
    }

    Write-Host "Processing $inputFile -> $outputFile with namespace $namespace"
    nswag openapi2csclient /input:$inputFile /output:$outputFile /namespace:$namespace /JsonLibrary:SystemTextJson /ClassName:$fileName /TemplateDirectory:openapi/templates
}