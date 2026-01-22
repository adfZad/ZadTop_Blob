$ErrorActionPreference = "Stop"

$workspaceRoot = "d:\Gravity\ZadTop1"
$portalBin = Join-Path $workspaceRoot "Portal\Bin"

Write-Host "Starting Build and Publish Process..."
Write-Host "Target Bin Directory: $portalBin"

# Ensure Bin exists
if (-not (Test-Path $portalBin)) {
    New-Item -ItemType Directory -Path $portalBin | Out-Null
    Write-Host "Created Bin directory."
}

# List of Class Library Projects
# Order matters less for dotnet build with ProjectReference, but good practice to be aware of dependencies.
$projects = @(
    "Modules\Been\Been.csproj",
    "Modules\Utilities\Utilities.csproj",
    "Modules\Data\Data.csproj",
    "Modules\Business\Business.csproj",
    "Modules\Pager\Pager.csproj",
    "Modules\PortalBase\PortalBase.csproj"
)

foreach ($projRelativePath in $projects) {
    $projPath = Join-Path $workspaceRoot $projRelativePath
    Write-Host "`n-------------------------------------------"
    Write-Host "Building Project: $projRelativePath"
    
    # Build
    dotnet build $projPath -c Release
    
    if ($LASTEXITCODE -ne 0) {
        Write-Error "Build failed for $projRelativePath"
        exit 1
    }
    
    # Locate Output
    $projDir = Split-Path $projPath -Parent
    $binDir = Join-Path $projDir "bin\Release"
    
    # Handle net48 subdirectory if present (SDK style projects often output here)
    # Handle net48 subdirectory if present AND contains the target DLL (to avoid stale folder)
    $dllName = [System.IO.Path]::GetFileNameWithoutExtension($projPath) + ".dll"
    # Adjust for discrepancies like "ZadHolding.Business" vs project file name "Business"
    # We can just look for *.dll
    
    if (-not (Get-ChildItem -Path $binDir -Filter "*.dll" -ErrorAction SilentlyContinue)) {
        if (Test-Path (Join-Path $binDir "net48")) {
            $binDir = Join-Path $binDir "net48"
        }
    }
    
    Write-Host "Copying artifacts from $binDir to $portalBin"
    
    # Copy DLL and PDB
    if (Test-Path $binDir) {
        Get-ChildItem -Path $binDir -Include *.dll, *.pdb -Recurse -File | ForEach-Object {
            $dest = Join-Path $portalBin $_.Name
            try {
                Copy-Item -Path $_.FullName -Destination $dest -Force -ErrorAction Stop
            }
            catch {
                Write-Warning "Could not copy $($_.Name): $($_.Exception.Message)"
            }
        }
    }
    else {
        Write-Warning "Output directory not found: $binDir"
    }
}

Write-Host "`n-------------------------------------------"
Write-Host "Build and Publish to Portal/Bin COMPLETED SUCCESSFULLY."
