$website = "UAT_GT_MasterData"
Stop-WebSite -Name "$website"
Start-WebSite -Name "$website"