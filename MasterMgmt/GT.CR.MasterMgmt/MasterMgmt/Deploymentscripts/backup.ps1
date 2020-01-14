$Date = Get-Date
$Date = $Date.ToString("yyyy-MMMM-dd_HH_mm_ss")
New-Item -ItemType Directory -Path "E:\UAT_GT\Services\MasterData\Backup\$Date"
$Source ="E:\UAT_GT\Services\MasterData\Live\*"
$Target = "E:\UAT_GT\Services\MasterData\Backup\$Date"
Copy-Item $Source $Target -recurse