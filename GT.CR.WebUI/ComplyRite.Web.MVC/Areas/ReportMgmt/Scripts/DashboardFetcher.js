$(function () {

    let dashboardEnvironment = $('#tableauViz').attr('dash-env');
    let tableauBaseURL = '';
    switch (dashboardEnvironment) {
        case 'Dev': tableauBaseURL = "https://test-tableau.pratian.com/trusted/";
            break;
        case 'Test': tableauBaseURL = "https://test-tableau.pratian.com/trusted/";
            break;
        case 'UAT': tableauBaseURL = "https://vyacta-report.pratian.com/trusted/";
            break;
        case 'Live': tableauBaseURL = "https://vyacta-report.pratian.com/trusted/";
            break;
        default:
    }
    let viz;
    function initializeViz(ele, token, dashIndex, dashName) {
        var placeholderDiv = document.getElementById(ele);
        var url = tableauBaseURL + token[0] + dashName;
        var options = {
            width: placeholderDiv.offsetWidth,
            height: placeholderDiv.offsetHeight,
            hideTabs: true,
            hideToolbar: false,
            refresh: true,
            "LocationFilter": JSON.parse($('#tableauViz').attr('user-locations'))
        };
        console.log(options);
        if (viz) { // If a viz object exists, delete it.
            viz.dispose();
        }

        viz = new tableau.Viz(placeholderDiv, url, options);
    }

    function GetTickets(dashIndex, dashName) {
        let count = 1;
        $.ajax({
            type: "GET",
            url: "/ReportMgmt/Report/GetTickets?num=" + count,
            contentType: 'text/html; charset=utf-8',
            success: function (data) {
                //tokens = data;
                //for (var i = 0; i < count; i++) {
                initializeViz("tableauViz", data, dashIndex, dashName);
                //floatMenuGenerator(i);
                //}
            },
            error: function () {
                alert("Error occured!!");
            }
        });
    }

    (function loadDashboard() {
        let dashName = $('#tableauViz').attr('dash-name');
        console.log(dashName + $('#tableauViz').attr('user-locations'));
        GetTickets(0, dashName);
    })();
});