$Date = Get-Date
$Date = $Date.ToString("yyyy-MMMM-dd_HH_mm_ss")
New-Item -ItemType Directory -Path "E:\UAT_GT\Applications\GTWeb\Backup\$Date"
$Source ="E:\UAT_GT\Applications\GTWeb\Live\*"
$Target = "E:\UAT_GT\Applications\GTWeb\Backup\$Date"
Copy-Item $Source $Target -recurse