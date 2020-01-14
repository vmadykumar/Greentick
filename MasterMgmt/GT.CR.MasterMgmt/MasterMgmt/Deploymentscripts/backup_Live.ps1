$Date = Get-Date
$Date = $Date.ToString("yyyy-MMMM-dd_HH_mm_ss")
New-Item -ItemType Directory -Path "E:\Live_GT\Service\MasterData\Backup\$Date"
$Source = "E:\Live_GT\Service\MasterData\Live\*"
$Target = "E:\Live_GT\Service\MasterData\Backup\$Date"
Copy-Item $Source $Target -recurse