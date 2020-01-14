$Date = Get-Date
$Date = $Date.ToString("yyyy-MMMM-dd_HH_mm_ss")
New-Item -ItemType Directory -Path "E:\Live_GT\Application\GTWeb\Backup\$Date"
$Source ="E:\Live_GT\Application\GTWeb\Live\*"
$Target = "E:\Live_GT\Application\GTWeb\Backup\$Date"
Copy-Item $Source $Target -recurse