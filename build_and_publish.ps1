$ErrorActionPreference = "Stop"

$workspaceRoot = $PSScriptRoot
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
Write-Host "Restoring Portal Packages..."
nuget restore (Join-Path $workspaceRoot "Portal\packages.config") -PackagesDirectory (Join-Path $workspaceRoot "packages")

Write-Host "Copying iTextSharp..."
$itextSharpPath = Join-Path $workspaceRoot "packages\iTextSharp.5.5.13.4\lib\itextsharp.dll"
# Path might be inside net48 folder or similar depending on package structure, but usually lib directly or lib/netxx
# Check common path pattern for this package version
$itextSharpPath = Join-Path $workspaceRoot "packages\iTextSharp.5.5.13.4\lib\itextsharp.dll" 
if (-not (Test-Path $itextSharpPath)) {
    # Try alternate path style
    $itextSharpPath = Join-Path $workspaceRoot "packages\iTextSharp.5.5.13.4\lib\net40-client\itextsharp.dll"
    if (-not (Test-Path $itextSharpPath)) {
        $itextSharpPath = Join-Path $workspaceRoot "packages\iTextSharp.5.5.13.4\lib\itextsharp.dll" # Fallback/Retry logic or just assume standard if simple
    }
}

# Actually, let's just do a wildcard search in the specific package folder to be safe
$packageDir = Join-Path $workspaceRoot "packages\iTextSharp.5.5.13.4"
$dlls = Get-ChildItem -Path $packageDir -Filter "itextsharp.dll" -Recurse
if ($dlls) {
    $dll = $dlls | Select-Object -First 1
    Copy-Item -Path $dll.FullName -Destination $portalBin -Force
    Write-Host "Copied iTextSharp from $($dll.FullName)"
}
else {
    Write-Warning "Could not find iTextSharp.dll in packages folder."
}

Write-Host "Copying System.Diagnostics.DiagnosticSource..."
$diagSourcePath = Join-Path $workspaceRoot "packages\System.Diagnostics.DiagnosticSource.4.7.1\lib\net46\System.Diagnostics.DiagnosticSource.dll"
if (Test-Path $diagSourcePath) {
    Copy-Item -Path $diagSourcePath -Destination $portalBin -Force
    Write-Host "Copied System.Diagnostics.DiagnosticSource from $diagSourcePath"
}
else {
    Write-Warning "Could not find System.Diagnostics.DiagnosticSource.dll at $diagSourcePath"
}

Write-Host "`n-------------------------------------------"
Write-Host "Build and Publish to Portal/Bin COMPLETED SUCCESSFULLY."
