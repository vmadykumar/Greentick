$website = "UAT_GT_Audit"
Stop-WebSite -Name "$website"
Start-WebSite -Name "$website"