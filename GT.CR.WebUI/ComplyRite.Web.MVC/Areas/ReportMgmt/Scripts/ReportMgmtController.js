/// <reference path="commonserviceutils.js" />

function ReportMgmt(ServiceRepo) {
    this.ServiceRepo = ServiceRepo;
}
//var serviceRepository = new ServiceRepository();
//var ReportMgmt = new ReportMgmt(serviceRepository);

/* Anonymous Function
Calls after the document is ready/loaded */
$(function () {
    $('#navbar-greentickLogoId').removeClass('hidden');

    //Get the id's of all the canvas present in hygiene rating div
    HelperFunctions.GetGraphCanvasIds('hygieneRatingAuditAreas');

    //Get the id's of all the canvas present in responsible place to eat rating div
    HelperFunctions.GetGraphCanvasIds('responsiblePlaceToEatAuditAreas');

    //Iterate through the generatePieChartGraphObj and call all the functions to generate the graphs on load
    $.each(generatePieChartGraphs, function (i) { this() });

    //Call to generate the bar graph for hygiene rating section
    if (GetCanvasData.ConstantData.hygieneRatingAuditAreasArr.length!=0) {
    generateBarChartGraph.SectionwiseComplianceLevelForHygiene();
    }

    //Call to generate the bar graph for responsible place to eat section
    if (GetCanvasData.ConstantData.responsiblePlaceToEatAuditAreasArr.length != 0) {
        generateBarChartGraph.SectionwiseComplianceLevelForResponsiblePlaceToEat();
    }
    
})

var GetCanvasData = {
    /*Custom attributes that are defined in the html canvas*/
    CustomAttrsData: {
        yesData: '-yes-data',                //Text appended after the canvas id to become the custom attr containing data related to yes that is present in html canvas
        noData: '-no-data',                  //Text appended after the canvas id to become the custom attr containing data related to no that is present in html canvas
        label: 'label'                       //Text appended after the canvas id to become the custom attr containing label that is present in html canvas
    },
    /*Constant data that are used in the graphs*/
    ConstantData: {
        hygieneRatingAuditAreasArr: [],           //Array that contains all the id's of the canvas present in audit areas of hygiene rating
        responsiblePlaceToEatAuditAreasArr: [],   //Array that contains all the id's of the canvas present in audit areas of responsible place to eat      
        labelData: 'Compliance Level For ',      //Text that is appended for all the audit areas along with the label defined in html canvas        
        GreenBackgroundColor: "#18A05E",         //Yes data background color
        RedBackgroundColor: "#DD5144"           //No data background color
    }
}

/*Helpers that are used in graphs*/
var HelperFunctions = {
    /*
    GetGraphCanvasIds : Function to get all the id's from canvas that is present in a div
    Parameters: id - Parent div id conatining all the canvas
    */
    GetGraphCanvasIds: function (id) {
        $.each($('#' + id).find('canvas'), function (index, val) {
            GetCanvasData.ConstantData[id + 'Arr'].push(val.getAttribute('id'));
        })
    },
    GetDataByAttr: function (id, attribute) {
        return $('#' + id).attr(attribute);
    },
    GetDataByCustomAttr: function (id, attribute) {
        return $('#' + id).attr(id.split('-piechart')[0] + attribute);
    },
    GetPieChartDataset: function (id) {
        pieChartDataset = [HelperFunctions.GetDataByCustomAttr(id, GetCanvasData.CustomAttrsData.noData), HelperFunctions.GetDataByCustomAttr(id, GetCanvasData.CustomAttrsData.yesData)];
    }
}

/*Obj containing functions for generating pie charts of all sections and audit areas*/
var generatePieChartGraphs = {

    //Generate Hygiene rating main graph
    ComplianceLevelForHygiene: function () {
        if (GetCanvasData.ConstantData.hygieneRatingAuditAreasArr.length!=0) {
            chartUtils._DrawPieChartGraph("hygieneCompliance-piechart", [$('#hygieneCompliance-piechart').attr('hygieneCompliance-yes-data'), $('#hygieneCompliance-piechart').attr('hygieneCompliance-no-data')], 'Compliance Level For Hygiene');
        }
    },

    //Generate graphs off all the audit areas present in hygiene rating
    HygieneRatingGraphs: function () {
        if (GetCanvasData.ConstantData.hygieneRatingAuditAreasArr.length!=0) {
            $.each(GetCanvasData.ConstantData.hygieneRatingAuditAreasArr, function (index, val) {
                chartUtils._DrawPieChartGraph(val,
                    [HelperFunctions.GetDataByCustomAttr(val, GetCanvasData.CustomAttrsData.noData), HelperFunctions.GetDataByCustomAttr(val, GetCanvasData.CustomAttrsData.yesData)],
                    GetCanvasData.ConstantData.labelData + HelperFunctions.GetDataByAttr(val, GetCanvasData.CustomAttrsData.label));
            })
        }
    },

    //Generate responsible place to eat main pie chart graph
    ComplianceLevelForResponsiblePlaceToEat: function () {
        if (GetCanvasData.ConstantData.responsiblePlaceToEatAuditAreasArr.length!=0) {
            chartUtils._DrawPieChartGraph("responsiblePlaceToEat-piechart",
                [HelperFunctions.GetDataByCustomAttr('responsiblePlaceToEat-piechart', GetCanvasData.CustomAttrsData.noData), HelperFunctions.GetDataByCustomAttr('responsiblePlaceToEat-piechart', GetCanvasData.CustomAttrsData.yesData)],
                'Compliance Level For Cafeteria');
        }
    },

    //Generate pie chart graphs off all the audit areas present in responsible place to eat
    ResponsiblePlaceToEatGraphs: function () {
        if (GetCanvasData.ConstantData.responsiblePlaceToEatAuditAreasArr.length!=0) {
            $.each(GetCanvasData.ConstantData.responsiblePlaceToEatAuditAreasArr, function (index, val) {
                debugger;
                chartUtils._DrawPieChartGraph(val,
                    [HelperFunctions.GetDataByCustomAttr(val, GetCanvasData.CustomAttrsData.noData), HelperFunctions.GetDataByCustomAttr(val, GetCanvasData.CustomAttrsData.yesData)],
                    GetCanvasData.ConstantData.labelData + HelperFunctions.GetDataByAttr(val, GetCanvasData.CustomAttrsData.label));
            })
        }
    }
}

/* Obj containing functions for generating bar charts of all sections and audit areas */
var generateBarChartGraph = {

    /* Generate hygiene rating main pie chart graph */
    SectionwiseComplianceLevelForHygiene: function () {
        var yesDataArr = [], noDataArr = [], labelArr = [];
        $.each(GetCanvasData.ConstantData.hygieneRatingAuditAreasArr, function (index, val) {
            yesDataArr.push(HelperFunctions.GetDataByCustomAttr(val, GetCanvasData.CustomAttrsData.yesData));
            noDataArr.push(HelperFunctions.GetDataByCustomAttr(val, GetCanvasData.CustomAttrsData.noData));
            labelArr.push(HelperFunctions.GetDataByAttr(val, GetCanvasData.CustomAttrsData.label));
        })
        chartUtils._DrawBarChartGraph("hygieneCompliance-barchart",
            labelArr, [{ label: "No", backgroundColor: GetCanvasData.ConstantData.RedBackgroundColor, data: noDataArr }, { label: "Yes", backgroundColor: GetCanvasData.ConstantData.GreenBackgroundColor, data: yesDataArr }],
            'Sectionwise Compliance Level For Hygiene (In %)')
    },

    /* Generate responsible place to eat main pie chart graph */
    SectionwiseComplianceLevelForResponsiblePlaceToEat: function () {
        var yesDataArr = [], noDataArr = [], labelArr = [];
        $.each(GetCanvasData.ConstantData.responsiblePlaceToEatAuditAreasArr, function (index, val) {
            yesDataArr.push(HelperFunctions.GetDataByCustomAttr(val, GetCanvasData.CustomAttrsData.yesData));
            noDataArr.push(HelperFunctions.GetDataByCustomAttr(val, GetCanvasData.CustomAttrsData.noData));
            labelArr.push(HelperFunctions.GetDataByAttr(val, GetCanvasData.CustomAttrsData.label));
        })
        chartUtils._DrawBarChartGraph("responsiblePlaceToEat-barchart",
            labelArr, [{ label: "No", backgroundColor: GetCanvasData.ConstantData.RedBackgroundColor, data: noDataArr }, { label: "Yes", backgroundColor: GetCanvasData.ConstantData.GreenBackgroundColor, data: yesDataArr }],
            'Sectionwise Compliance Level For Cafeteria (In %)')
    }
}

// A plugin that hides slices, given their indices, across all datasets.
//var hideSlicesPlugin = {
//    afterInit: function (chartInstance) {
//        // If `hiddenSlices` has been set.
//        if (chartInstance.config.data.hiddenSlices !== undefined) {
//            // Iterate all datasets.
//            for (var i = 0; i < chartInstance.data.datasets.length; ++i) {
//                // Iterate all indices of slices to be hidden.
//                chartInstance.config.data.hiddenSlices.forEach(function (index) {
//                    // Hide this slice for this dataset.
//                    chartInstance.getDatasetMeta(i).data[index].hidden = true;
//                });
//            }
//            chartInstance.update();
//        }
//    }
//};

//Chart.pluginService.register(hideSlicesPlugin);

var removeZeroVal = function (arr) {
    return $.each(arr, function (i, val) { if (val == 0) { arr.splice(i, 1) } });
}

var chartUtils = {
    /*
    _DrawPieChartGraph : Generic function to generate the piechart graph
            pieChartId : The id that is assigned to the canvas element in html
               objData : Array that contains data to generate pie chart (Yes & No values in this case)
            optionText : The text/label that is shown above the piechart
    */
    _DrawPieChartGraph: function (pieChartId, objData, optionText) {
        debugger;
        var dataObj = {
            labels: ["No", "Yes"],
            datasets: [{
                label: optionText + " (In %)",
                backgroundColor: [GetCanvasData.ConstantData.RedBackgroundColor, GetCanvasData.ConstantData.GreenBackgroundColor],
                data: objData
            }]
            //hiddenSlices: [1]
        };
        new Chart(document.getElementById(pieChartId), {
            type: 'pie',
            width: '400px',
            responsive: true,
            maintainAspectRatio: false,
            data: dataObj,
            options: {
                title: {
                    display: true,
                    text: optionText + " (In %)"
                },
                legend: {
                    onClick: function (e) {
                        e.stopPropagation();
                    }
                }   
            },
            color: function (context) {
                var index = context.dataIndex;
                var value = context.dataset.data[index] + "%";
                return 'rgb: [285, 225, 215]';
            }
        });
    },
    
    /*
   _DrawBarChartGraph : Generic function to generate the barchart graph
          barChartId  : The id that is assigned to the canvas element in html
          datasetsArr : Array that contains data to generate the bar chart of all the audit areas
          optionText  : The text/label that is shown above the barchart
   */
    _DrawBarChartGraph: function (barChartId, labelsArr, datasetsArr, optionText) {
        new Chart(document.getElementById(barChartId), {
            type: 'bar',
            data: {
                labels: labelsArr,
                datasets: datasetsArr
            },
            options: {
                title: {
                    display: true,
                    text: optionText,
                    font: '20px'
                },
                font: {
                    weight: 'bold'
                },
                scales: {
                    xAxes: [{
                        barPercentage: 0.45
                    }]
                }
            },
            label: {
                display: 'outside',
                field: 'used',
                orientation: 'horizontal',
                font: '14px news-gothic-std,sans-serif',
                fill: '#666666'
            }
        });
    }
}


/**
Print the report 
 * 
 */
var printReportData = function () {
    var title = '';
    var pieChartArr = [];
    var attr = $('#ReportIndexMainId').find('canvas').first().siblings('span').find('img').attr('src');

    if (typeof attr !== typeof undefined && attr !== false) {       //If the image is already generated before then hide the canvas and show the image
        $('#ReportIndexMainId').find('canvas').addClass('hidden');
        $('#ReportIndexMainId').find('canvas').siblings('span').find('img').removeClass('hidden');
    }
    else {   //If the image was not generated before then generate the image stream
        pieChartArr = [].concat.apply([], [GetCanvasData.ConstantData.hygieneRatingAuditAreasArr, GetCanvasData.ConstantData.responsiblePlaceToEatAuditAreasArr, ['hygieneCompliance-piechart', 'hygieneCompliance-barchart', 'responsiblePlaceToEat-piechart', 'responsiblePlaceToEat-barchart']]);
        $.each(pieChartArr, function (key, value) {
            generateImageAndHideCanvas(value);         //Generating image of all the canvas as the print does not support the rendering of canvas
        })
    }
    setTimeout(function () {
        var divToPrint = document.getElementById("ReportIndexMainId");
        $('#hiddenPrintDiv').empty();
        $('#hiddenPrintDiv').append(divToPrint.innerHTML);

        // Creating a window for printing
        title = "Audit Report";
        var mywindow = window.open('', $('#hiddenPrintDiv').html(), 'height=800,width=800,left=200');
        //mywindow.document.write('<center><b style="font-size:large">' + title + '</b></center></br>');
        mywindow.document.write('<html><head><title>' + title + "_" + $('#introIndexAuditDateId').text() + '</title>');
        mywindow.document.write('<link href="/Scripts/chartjs-plugin-datalabels.js" />');
        mywindow.document.write('<link rel="stylesheet" href="/Content/bootstrap.min.css" type="text/css" />');
        mywindow.document.write('<link href="/Scripts/Chart.bundle.min.js"/>');
        mywindow.document.write('<link href="/Areas/ReportMgmt/Scripts/ReportMgmtController.js"/>');
        mywindow.document.write('<link rel="stylesheet" href="/Content/font-awesome.min.css" type="text/css" />');
        mywindow.document.write('</head><body style="background-color:white"><div class="col-lg-12" >');
        mywindow.document.write($('#ReportIndexMainId').html());
        mywindow.document.write('</div></body></html>');
        mywindow.document.close();
        mywindow.focus();
        setTimeout(function () {
            mywindow.print();
            //Hiding all the images and displaying the canvas once the print view is generated
            $('#ReportIndexMainId').find('canvas').removeClass('hidden')
            $('#ReportIndexMainId').find('canvas').siblings('span').find('img').addClass('hidden')
            mywindow.close();
        }, 2000);
        return true;
    }, 1000)
}

/* Function to hide the canvas and display images while generating the print window */
var generateImageAndHideCanvas = function (id) {
    var url = document.getElementById(id).toDataURL();    // toDataURL() : Provides the image stream which can be used in the html image src to generate the image of the graph
    $('#' + id + '-img').attr('src', url);
    $('#' + id).addClass('hidden');
}