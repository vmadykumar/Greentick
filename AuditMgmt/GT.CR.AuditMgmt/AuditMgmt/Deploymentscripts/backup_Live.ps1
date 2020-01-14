$Date = Get-Date
$Date = $Date.ToString("yyyy-MMMM-dd_HH_mm_ss")
New-Item -ItemType Directory -Path "E:\Live_GT\Service\Audit\Backup\$Date"
$Source ="E:\Live_GT\Service\Audit\Live\*"
$Target = "E:\Live_GT\Service\Audit\Backup\$Date"
Copy-Item $Source $Target -recurse