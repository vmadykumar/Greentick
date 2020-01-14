$website = "UAT_GT_Web"
Stop-WebSite -Name "$website"
Start-WebSite -Name "$website"