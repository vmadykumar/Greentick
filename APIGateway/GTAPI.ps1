param(
	[parameter (Mandatory=$true)] 
	[ValidateNotNullOrEmpty()]
	[string] $OutputPath
)

<#
	===================================================================================================
	===================================================================================================
	Exported functions
	===================================================================================================
	===================================================================================================
#>

function BuildSolution
{
	[CmdletBinding()]
	param()

	dotnet publish "GT.APIGateway.sln" -c Release -o $OutputPath

}

<#
	===================================================================================================
	===================================================================================================
	Start of script
	===================================================================================================
	===================================================================================================
#>

Clear-Host
$ScriptDirectory = $PSScriptRoot
Set-Location $ScriptDirectory

[int] $exitCode = 0
$oldVerbose = $VerbosePreference
$VerbosePreference = "SilentlyContinue" # "SilentlyContinue" "continue" 

try {
	BuildSolution
}
catch {
	Write-Output $_
	$exitCode = 1
}
finally {
	Remove-Module *
	Remove-Variable * -Exclude "exitCode" -ErrorAction SilentlyContinue
	$error.Clear(); 
	$VerbosePreference = $oldVerbose
	exit $exitCode
}