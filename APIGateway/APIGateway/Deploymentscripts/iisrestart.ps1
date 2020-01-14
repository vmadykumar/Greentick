$website = "UAT_GT_ComplyRite"
Stop-WebSite -Name "$website"
Start-WebSite -Name "$website"