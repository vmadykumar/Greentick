$Date = Get-Date
$Date = $Date.ToString("yyyy-MMMM-dd_HH_mm_ss")
New-Item -ItemType Directory -Path "E:\Live_GT\Application\GT-ComplyRite\Backup\$Date"
$Source ="E:\Live_GT\Application\GT-ComplyRite\Live\*"
$Target = "E:\Live_GT\Application\GT-ComplyRite\Backup\$Date"
Copy-Item $Source $Target -recurse