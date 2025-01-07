
$inputDir = "openapi/Authorization"
$outputDir = "EffSln.EtradeSdk/Authorization"

# Get all YAML files in the input directory
Get-ChildItem -Path $inputDir -Filter *.yaml | ForEach-Object {
    # Extract the filename without extension
    $fileName = $_.BaseName

    # Construct the output file path
    $outputFile = Join-Path $outputDir ("$fileName.cs")

    $className = "${fileName}Client"
    #$responseName = "Response_$fileName"
    $namespace = "EffSln.EtradeSdk.${outputDir}${fileName}"
 
    # Run the NSwag command
    Write-Host "Processing $($_.FullName) -> $outputFile"
    nswag openapi2csclient /GenerateExceptionClasses:false  /input:$($_.FullName) /output:$outputFile /namespace:$namespace /JsonLibrary:SystemTextJson /ClassName:$className 
}