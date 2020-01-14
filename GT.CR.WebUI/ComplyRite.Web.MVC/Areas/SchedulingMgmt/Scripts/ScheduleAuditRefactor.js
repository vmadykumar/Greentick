var dates = {
    clickedDate: ''
};
var selectedAreaCount = 0;
eventCall = 0;

EmptyAuditObject();

function EmptyAuditObject() {
    AuditSchedule = {
        AssignedToUUID: "",
        AssignedToUserName: "",
        AssignedToUserUUID: "",
        AuditCategory: "",
        AuditCity: "",
        AuditCode: "",
        AuditExpiryDateTime: "",
        AuditFBO: "",
        FBOCode: "",
        AuditImage: "",
        AuditLocation: "",
        AuditLocationCode: "",
        AuditName: "",
        AuditScheduledEndDateTime: "",
        AuditScheduledStartDateTime: "",
        AuditStatus: "",
        AuditType: "",
        CreatedBy: "",
        CreatedDateTime: "",
        frequency: 'once',
        AuditChecklistDto: [],
        ScheduleDates: []
    };
}

   


$(function () {
    
    // Holds the classes of dropdown fields for Select2
    let formControls = {
        'scheduleAuditDropdowns': {
            '.schedule-location': {
                'placeholder': 'Select a Location'
            },
            '.schedule-area': {
                'placeholder': 'Select a Area',
                'dropdownParent': $("#areas_and_checklists")
            },
            '.schedule-subarea': {
                'placeholder': 'Select a Sub-Area',
                'dropdownParent': $("#areas_and_checklists")
            },
            '.schedule-select-checklist': {
                'placeholder': 'Select a Checklist'
            },
            '.schedule-select-auditor': {
                'placeholder': 'Select a User'
            },
            '.occurence-select': {
                'placeholder': 'Select Occurences'
            },
            '.schedule-city': {
                'placeholder': 'Select a City'
            }
        }
    };

    $('.end-time, .expiry-time, .repeat-end-time').flatpickr({
        enableTime: true,
        noCalendar: true,
        dateFormat: "h:i K"
    });


    
    $('.start-time').flatpickr({
        enableTime: true,
        noCalendar: true,
        dateFormat: "h:i K"
    });

   

    $('#ScheduleAuditSideDiv').on('change', '.start-time', function () {

        const endTimeInstance = document.querySelector(".end-time")._flatpickr;
        const expiryTimeInstance = document.querySelector(".expiry-time")._flatpickr;

        endTimeInstance.setDate(moment($('.start-time').val(), 'Y-MM-D h:mm A').add(8, 'h').format('Y-MM-D h:mm A'));
        expiryTimeInstance.setDate(moment($('.end-time').val(), 'Y-MM-D h:mm A').add(2, 'h').format('Y-MM-D h:mm A'));


        let startTimeObj = moment($('.start-time').val(), 'Y-MM-D h:mm A');
        let endTimeObj = moment($('.end-time').val(), 'Y-MM-D h:mm A');
        let expiryTimeObj = moment($('.expiry-time').val(), 'Y-MM-D h:mm A');

        // Check if Start Time is greater than or same as End Time
        isEndTimeValid = startTimeObj.isAfter(endTimeObj) || startTimeObj.isSame(endTimeObj);
        isEndTimeValid === true ? $('.audit-time-validation-msg').html('End Time should be greater than Start Time') : $('.audit-time-validation-msg').html('');

        // Check if End Time is greater than End Time
        isExpiryTimeValid = endTimeObj.isAfter(expiryTimeObj);
        isExpiryTimeValid === true ? $('.audit-expiry-time-validation-msg').html('Expiry Time should be greater than or same as End Time') : $('.audit-expiry-time-validation-msg').html('');

        // To set the minimum time if current day is selected when editing schedule
        if ($('#OpenInEditMode').val() === "true") {
            const fp = document.querySelector('.start-time')._flatpickr
            if (startTimeObj.clone().startOf('day').isSame(moment().startOf('day'))) {
                fp.config.minTime = fp.config.minTime = moment().format('H:mm A');
            } else {
                fp.config.minTime = '';
            }
        }

    });

    $('#ScheduleAuditSideDiv').on('change', '.end-time', function () {

        //const endTimeInstance = document.querySelector(".end-time")._flatpickr;
        const expiryTimeInstance = document.querySelector(".expiry-time")._flatpickr;

        //endTimeInstance.setDate(moment($('.start-time').val()).add(8, 'h').format('Y-MM-D h:mm A'));
        expiryTimeInstance.setDate(moment($('.end-time').val(), 'Y-MM-D h:mm A').add(2, 'h').format('Y-MM-D h:mm A'));

        let startTimeObj = moment($('.start-time').val(), 'Y-MM-D h:mm A');
        let endTimeObj = moment($('.end-time').val(), 'Y-MM-D h:mm A');
        let expiryTimeObj = moment($('.expiry-time').val(), 'Y-MM-D h:mm A');

        // Check if Start Time is greater than or same as End Time
        isEndTimeValid = startTimeObj.isAfter(endTimeObj) || startTimeObj.isSame(endTimeObj);
        isEndTimeValid === true ? $('.audit-time-validation-msg').html('End Time should be greater than Start Time') : $('.audit-time-validation-msg').html('');

        // Check if End Time is greater than End Time
        isExpiryTimeValid = endTimeObj.isAfter(expiryTimeObj);
        isExpiryTimeValid === true ? $('.audit-expiry-time-validation-msg').html('Expiry Time should be greater than or same as End Time') : $('.audit-expiry-time-validation-msg').html('');
    });
    
    let startEndTimeChange = (dateFieldSelector, instance) => {
       
        let startTimeObj = moment($('.start-time').val(), 'Y-MM-D h:mm A');
        let endTimeObj = moment($('.end-time').val(), 'Y-MM-D h:mm A');
        let expiryTimeObj = moment($('.expiry-time').val(), 'Y-MM-D h:mm A');

        //console.log(startTimeObj, endTimeObj);

        // Check if Start Time is greater than both End Time and Expiry Time
        isEndExpiryTimeValid = startTimeObj.isAfter(endTimeObj) && startTimeObj.isAfter(expiryTimeObj);
        if (isEndExpiryTimeValid) {
            $('.audit-time-validation-msg').html('End Time should be greater than Start Time');
            $('.audit-expiry-time-validation-msg').html('Expiry Time should be greater than Start Time');
            return;
        }
        else {
            $('.audit-time-validation-msg').html('');
            $('.audit-expiry-time-validation-msg').html('');
        }


        // Check if Start Time is greater than or same as End Time
        isEndTimeValid = startTimeObj.isAfter(endTimeObj) || startTimeObj.isSame(endTimeObj);
        isEndTimeValid === true ? $('.audit-time-validation-msg').html('End Time should be greater than Start Time') : $('.audit-time-validation-msg').html('');
        
        // Check if End Time is greater than End Time
        isExpiryTimeValid = endTimeObj.isAfter(expiryTimeObj);
        isExpiryTimeValid === true ? $('.audit-expiry-time-validation-msg').html('Expiry Time should be greater than or same as End Time') : $('.audit-expiry-time-validation-msg').html('');

    };

    // Holds the classes of date fields for FlatPickr
   
    let dateControls = {
        '.end-time': {
            enableTime: true,
            noCalendar: false,
            disableMobile: "true",
            altInput: true,
            altFormat: "j M, Y h:i K",
            dateFormat: "Y-m-d h:i K",
           
            onChange: function () {
                let endDateSelector = $('.end-time');
                startEndTimeChange(endDateSelector);
            }
        },
        '.expiry-time': {
            enableTime: true,
            noCalendar: false,
            disableMobile: "true",
            altInput: true,
           
            altFormat: "j M, Y h:i K",
            dateFormat: "Y-m-d h:i K",
            onChange: startEndTimeChange
        },
        '.repeat-end-time': {
            //enableTime: true,
            noCalendar: false,
            disableMobile: "true",
            // Min/Max date is set before passing this object to whereever it is used.
            minDate: '',
            altInput: true,
           
            altFormat: "j M, Y ",
            dateFormat: "Y-m-d"
        },
        
        '.start-time': {
            enableTime: true,
            noCalendar: false,
            disableMobile: "true",
            // Min/Max date is set before passing this object to whereever it is used.
            minDate: '',
            //maxDate: '',
            altInput: true,
            altFormat: "j M, Y h:i K",
            dateFormat: "Y-m-d h:i K",
            //appendTo: $(".parent_startTime").get(0),
            onChange: function (selectedDates, dateStr, instance) {
                startEndTimeChange(dateStr, instance);
            }
        },
        '.recurrence-start-date': {
            enableTime: false,
            noCalendar: false,
            altFormat: "j M, Y",
            dateFormat: "Y-m-d"
           
        },
        '.recurrence-end-date': {
            enableTime: false,
            noCalendar: false,
            altFormat: "j M, Y",
            dateFormat: "Y-m-d"
            
        }
    };

    let dateControlInitObj = [];

    let generateDateControls = (dateControls) => {
        $.each(dateControls, function (dateControl, index) {
            let userConfig = dateControls[dateControl];
            if (dateControl === ".start-time") {
                userConfig.appendTo = $(".parent_startTime").get(0);
            }
            if (dateControl === ".end-time") {
                userConfig.appendTo = $(".parent_endTime").get(0);
            }
            let flatpickrInstance = $(dateControl).flatpickr(userConfig);
            dateControlInitObj.push(flatpickrInstance);
        })
    };

    // Helper Method to Sort Objects based on one of its properties
    const objCompare = function (a, b, property) {
        var nameA = a[property].toUpperCase(); // ignore upper and lowercase
        var nameB = b[property].toUpperCase(); // ignore upper and lowercase

        if (nameA < nameB) {
            return -1;
        }
        if (nameA > nameB) {
            return 1;
        }

        // names must be equal
        return 0;
    };

    // Method to get all the locations based on the uuid of the user
    const GetAllLocations = async (UUID, cityName) => {

        //const locations = await $.ajax({
        //    url: '/SchedulingMgmt/SchedulingManagement/GetAllLocationsByCityAndUUID',
        //    type: 'GET',
        //    data: { uuid: UUID, cityName: cityName }
        //});

        //const userAccounts = await $.ajax({
        //    url: '/SchedulingMgmt/SchedulingManagement/GetUserLocationsByUUID',
        //    type: 'GET',
        //    data: { uuid: UUID }
        //});

        
        //const locationsMapping = userAccounts.map(function (userAccount) {
        //    const locationsWOFBOCode = userAccount.Locations;
        //    const FBOCode = userAccount.LobCode;
        //    locationsWFBOCode = locationsWOFBOCode.map(function (location) {
        //        location.FBOCode = FBOCode;
        //        return location;
        //    });
        //    return locationsWFBOCode;
        //});

        //const locations = locationsMapping[0];

        const locations = getLocations(cityName);

        locations.sort(function (a, b) {
            return objCompare(a, b, 'LocationName');
        });


        //$('.schedule-location').empty();
        ////console.log(results);
        //$('.schedule-location').prepend("<option value=" + '' + ">" + '' + "</option>");
        locations.forEach(function (location, index) {
            ////console.log(location);
            //locations.push({ text: this.Text, value: this.Value });
            $('.schedule-location').append("<option value='" + location.LocationCode + "' fbo-code='" + location.LOBCode + "'>" + location.LocationName + "</option>");
        });

        

        //$('.schedule-location').select2({
        //    placeholder: 'Select a Location',
        //    allowClear: true
        //});


        // Older Implementationj
        //$.ajax({
        //    url: '/SchedulingMgmt/SchedulingManagement/GetUserLocationsByUUID',
        //    type: 'GET',
        //    data: { uuid: UUID }
        //}).done(function (results) {
        //    $('.schedule-location').empty();
        //    ////console.log(results);
        //    $('.schedule-location').prepend("<option value=" + '' + ">" + '' + "</option>");
        //    results.forEach(function (result, index) {
        //        ////console.log(result);
        //        //locations.push({ text: this.Text, value: this.Value });
        //        $('.schedule-location').append("<option value=" + result.LocationCode + ">" + result.LocationName + "</option>");
        //    });
        //    $('.schedule-location').select2({
        //        placeholder: 'Select a Location',
        //        allowClear: true
        //    });
        //});
    };
    
    // Method to get all the auditors for the location
    const GetAllAuditors = async (UUID, locationCode) => {
        //const auditors = await $.ajax({
        //    url: '/SchedulingMgmt/SchedulingManagement/GetAllAuditorsByLocation',
        //    type: 'GET',
        //    data: { uuid: UUID, locationCode: locationCode }
        //});

        const auditors = getUsers($('.schedule-city').val(), $('.schedule-location').val() );
        auditors.sort(function (a, b) {
            return objCompare(a, b, 'FirstName');
        });
                
        //$('.schedule-select-auditor').empty();
        $('.schedule-select-auditor').prepend("<option value=" + '' + ">" + '' + "</option>");
        auditors.forEach(function (auditor, index) {
            $('.schedule-select-auditor').append("<option value=" + auditor.UUID + " data-acc-alias='" + auditor.AccountAbbreviation + "'>" + auditor.FirstName + ' ' + auditor.LastName + "</option>");
        });
        $('.schedule-select-auditor').trigger('change');
        //$('.schedule-select-auditor').select2({
        //    placeholder: 'Select a User',
        //    allowClear: true
        //});

        // Old Implementation
        //    $.ajax({
        //    url: '/SchedulingMgmt/SchedulingManagement/GetAllAuditorsByLocation',
        //    type: 'GET',
        //    data: { uuid: UUID, locationCode: locationCode }
        //}).done(function (results) {
        //    let locations = [];
        //    ////console.log(results);
        //    $('.schedule-select-auditor').empty();
        //    $('.schedule-select-auditor').prepend("<option value=" + '' + ">" + '' + "</option>");
        //    results.forEach(function (result, index) {
        //        ////console.log(result);
        //        //locations.push({ text: this.Text, value: this.Value });
        //        $('.schedule-select-auditor').append("<option value=" + result.UUID + " data-acc-alias='" + result.AccountAbbreviation + "'>" + result.FirstName + ' ' + result.LastName + "</option>");
        //    });
        //    $('.schedule-select-auditor').select2({
        //        placeholder: 'Select an Auditor',
        //        allowClear: true
        //    });
        //});
    };
    const GetAllAuditorsForEdit = async (UUID, locationCode, preSelectedAuditor) => {
        $.ajax({
            url: '/SchedulingMgmt/SchedulingManagement/GetAllAuditorsByLocation',
            type: 'GET',
            data: { uuid: UUID, locationCode: locationCode },
            success: function (auditors) {
                $('.schedule-select-auditor').empty();
                $('.schedule-select-auditor').prepend("<option value=" + '' + ">" + '' + "</option>");
                auditors.forEach(function (auditor, index) {
                    $('.schedule-select-auditor').append("<option value=" + auditor.UUID + " data-acc-alias='" + auditor.AccountAbbreviation + "'>" + auditor.FirstName + ' ' + auditor.LastName + "</option>");
                });
                $('.schedule-select-auditor').select2({
                    placeholder: 'Select a User',
                    allowClear: true
                });
                $('.schedule-select-auditor').val(preSelectedAuditor).trigger('change');
            }
        });
    };

    // Method to get all the areas the auditor has access to.
    const getAreasByLocation = async (uuid, locationCode) => {
        //const areas = await $.ajax({
        //    url: '/SchedulingMgmt/SchedulingManagement/GetAllUserAreasByLocation',
        //    type: 'GET',
        //    data: { uuid: uuid /*'UU0000011'*/, locationCode: locationCode }
        //});
        await getAllAreasSubareasByLocation($('.schedule-select-auditor').val(), $('.schedule-location').val());
        
        if ($('#OpenInEditMode').val() === "true") {
            if (eventCall === 0) {
                eventCall++;
                eventHandlers();
                $('.ajax-loader').css("visibility", "hidden");
            }
        }
        const areas = getAreas();

        areas.sort(function (a, b) {
            return objCompare(a, b, 'AreaName');
        });

        $('.schedule-area').empty();
        $('.schedule-area').prepend("<option value=" + '' + ">" + '' + "</option>");
        areas.forEach(function (area, index) {
            $('.schedule-area').append("<option value=" + area.AreaCode + ">" + area.AreaName + "</option>");
        });
        $('.schedule-area').select2({
            placeholder: 'Select an Area',
            allowClear: true
        });

        // Old Implementation
        //$.ajax({
        //    url: '/SchedulingMgmt/SchedulingManagement/GetAllUserAreasByLocation',
        //    type: 'GET',
        //    data: { uuid: uuid /*'UU0000011'*/, locationCode: locationCode }
        //}).done(function (results) {
        //    let locations = [];
        //    ////console.log(results);
        //    $('.schedule-area').empty()
        //    $('.schedule-area').prepend("<option value=" + '' + ">" + '' + "</option>");
        //    results.forEach(function (result, index) {
        //        ////console.log(result);
        //        //locations.push({ text: this.Text, value: this.Value });
        //        $('.schedule-area').append("<option value=" + result.AreaCode + ">" + result.AreaName + "</option>");
        //    });
        //    $('.schedule-area').select2({
        //        placeholder: 'Select an Area',
        //        allowClear: true
        //    });
        //});
    };
    
    // Mehtod to get all the subareas for the selected location
    const getSubAreasAreasByUserAndLocation = async (uuid, locationCode, areacode) => {
        //const subareas = await $.ajax({
        //    url: '/SchedulingMgmt/SchedulingManagement/GetAllUserSubAreasbyLocation',
        //    type: 'GET',
        //    data: { uuid: uuid /*'UU0000011'*/, locationCode: locationCode/*'MCD0000001'*/, areacode: areacode }
        //});

        const subareas = getSubAreas($('.schedule-area').val());
        subareas.sort(function (a, b) {
            return objCompare(a, b, 'SubAreaName');
        });

        $('.schedule-subarea').prepend("<option value=" + '' + ">" + '' + "</option>");
        subareas.forEach(function (subarea, index) {
            $('.schedule-subarea').append("<option value=" + subarea.SubAreaCode + ">" + subarea.SubAreaName + "</option>");
        });
        $('.schedule-subarea').select2({
            placeholder: 'Select a Sub Area',
            allowClear: true
        });

        // Old Implementation
        //$.ajax({
        //    url: '/SchedulingMgmt/SchedulingManagement/GetAllUserSubAreasbyLocation',
        //    type: 'GET',
        //    data: { uuid: uuid /*'UU0000011'*/, locationCode: locationCode/*'MCD0000001'*/, areacode: areacode }
        //}).done(function (results) {
        //    let locations = [];
        //    ////console.log(results);
        //    $('.schedule-subarea').prepend("<option value=" + '' + ">" + '' + "</option>");
        //    results.forEach(function (result, index) {
        //        ////console.log(result);
        //        //locations.push({ text: this.Text, value: this.Value });
        //        $('.schedule-subarea').append("<option value=" + result.SubAreaCode + ">" + result.SubAreaName + "</option>");
        //    });
        //    $('.schedule-subarea').select2({
        //        placeholder: 'Select a Sub Area',
        //        allowClear: true
        //    });
        //});
    };

    // Method to get all the checklist for the area/subarea selected.
    const GetAllChecklistForAreaSubArea = async (subareacode) => {
        const checklists = await $.ajax({
            url: '/SchedulingMgmt/SchedulingManagement/GetAllChecklistForAreaSubArea',
            type: 'GET',
            data: { subareacode: subareacode }
        });

        checklists.sort(function (a, b) {
            return objCompare(a, b, 'ChecklistName');
        });

        checklists.forEach(function (checklist, index) {
            $('#my-select').append("<option value=" + checklist.ChecklistId + " checklist-code=" + checklist.ChecklistCode + ">" + checklist.ChecklistName + "</option>");
        });
        $('#my-select').multiSelect('refresh');

        // Old Implementation
        //$.ajax({
        //    url: '/SchedulingMgmt/SchedulingManagement/GetAllChecklistForAreaSubArea',
        //    type: 'GET',
        //    data: { subareacode: subareacode }
        //}).done(function (results) {
        //    let locations = [];
        //    ////console.log(results);
        //    $('#my-select').empty().multiSelect('refresh');
        //    //$('#my-select').prepend("<option value=" + '' + ">" + '' + "</option>");
        //    results.forEach(function (result, index) {
        //        ////console.log(result);
        //        //locations.push({ text: this.Text, value: this.Value });
        //        $('#my-select').append("<option value=" + result.ChecklistId + " checklist-code=" + result.ChecklistCode + ">" + result.ChecklistName + "</option>");
        //    });
        //    $('#my-select').multiSelect('refresh');
        //});
    };

    // Method to get all the cities based on the uuid of the user
    const GetAllCities = async (UUID) => {
        //const cities = await $.ajax({
        //    url: '/SchedulingMgmt/SchedulingManagement/GetAllUserCitiesByUUID',
        //    type: 'GET',
        //    data: { uuid: UUID }
        //});

        const cities = getCities();

        cities.sort();

        $('.schedule-city').empty();
        $('.schedule-city').prepend("<option value=" + '' + ">" + '' + "</option>");
        cities.forEach(function (city, index) {
            $('.schedule-city').append("<option value=" + city + ">" + city + "</option>");
        });
        $('.schedule-city').select2({
            placeholder: 'Select a City',
            allowClear: false
        });
        //$.ajax({
        //    url: '/SchedulingMgmt/SchedulingManagement/GetUserCitiesByUUID',
        //    type: 'GET',
        //    data: { uuid: UUID }
        //}).done(function (results) {
        //    $('.schedule-city').empty();
        //    ////console.log(results);
        //    $('.schedule-city').prepend("<option value=" + '' + ">" + '' + "</option>");
        //    results.forEach(function (result, index) {
        //        ////console.log(result);
        //        //locations.push({ text: this.Text, value: this.Value });
        //        $('.schedule-city').append("<option value=" + result.CityName + ">" + result.CityName + "</option>");
        //    });
        //    $('.schedule-city').select2({
        //        placeholder: 'Select a City',
        //        allowClear: true
        //    });
        //});
    };

    let RemoveAllAuditors = () => {
        $('.schedule-select-auditor').empty();
    };

    var beResults = null;
    // Get all the checks for a checklist by checklist id
    let getAllChecksByChecklistId = function (checklistId, subcode) {

        $.ajax({
            url: '/SchedulingMgmt/SchedulingManagement/GetAllChecksByChecklistID',
            type: 'GET',
            async: false,
            data: { checklistId: checklistId, subAreaCode: subcode }
        }).done(function (results) {
            let locations = [];
            //console.log(results);
            beResults = results;
        });
    };

   
    let schedulerHandlers = {

        // Get the Schedule Checklist partial form
        addScheduleAuditPartial: (date) => {
            $.ajax({
                url: '/SchedulingMgmt/SchedulingManagement/GetScheduleAddAudit',
                contentType: 'application/html; charset=utf-8',
                type: 'GET',
                dataType: 'html',
                beforeSend: function () {
                    $('.ajax-loader').css("visibility", "visible");
                },
                complete: function () {
                    $('.ajax-loader').css("visibility", "hidden");
                },
                success: function (result) {
                    EmptyAuditObject();
                    $('#ScheduleAuditSideDiv').html(result);
                    dates.clickedDate = date;
                    auditFormVariables.selectedAreaCount = 0;
                    let scheduleDate = date.format('MMMM Do YYYY');
                    let scheduleDay = date.format('dddd');
                    $('.audit-schedule-date').html(scheduleDate);
                    $('.audit-schedule-day').html(scheduleDay);
                    let startDate = date.format('Y-MM-D');
                    $('#start_date').val(startDate);
                    $('#created_date').val(moment().format('Y-MM-D'));
                    dateControls[".start-time"].minDate = moment(dates.clickedDate.format('Y-MM-D') + ' ' + moment().format('h:mm A'), 'Y-MM-D h:mm A').format('Y-MM-D h:mm A') /*dates.clickedDate.format();*/
                    dateControls[".start-time"].maxDate = moment(dates.clickedDate.format('Y-MM-D')).endOf('day').format('Y-MM-D h:mm A'); /*dates.clickedDate.format('Y-MM-D')*/;
                    //dateControls[".start-time"].defaultDate = moment(dates.clickedDate.format('Y-MM-D') + ' ' + moment().format('h:mm A')).format('Y-MM-D h:mm A'); /* dates.clickedDate.format();*/
                    dateControls[".start-time"].defaultDate = dateControls[".start-time"].minDate;

                    // Set the minimum time so it does not go below beyond current time for today's date.
                    dates.clickedDate.format('Y-MM-D') === moment().format("Y-MM-D") ? dateControls[".start-time"].minTime = Date.now() : dateControls[".start-time"].minTime = '';
                    dates.clickedDate.format('Y-MM-D') === moment().format("Y-MM-D") ? dateControls[".repeat-end-time"].minTime = Date.now() : dateControls[".repeat-end-time"].minTime = '';

                
                    let newDate = moment(dateControls[".start-time"].defaultDate, 'Y-MM-D h:mm A');

                    dateControls[".recurrence-start-date"].defaultDate = newDate.format('Y-MM-D');
                    dateControls[".recurrence-end-date"].defaultDate = newDate.clone().add(5, 'day').format('Y-MM-D h:mm A');
                    dateControls[".end-time"].defaultDate = newDate.add(8, 'h').format('Y-MM-D h:mm A');
                    dateControls[".expiry-time"].defaultDate = newDate.add(2, 'h').format('Y-MM-D h:mm A');
                    
                    dateControls[".end-time"].minDate = dates.clickedDate.format('Y-MM-D');
                    dateControls[".expiry-time"].minDate = dates.clickedDate.format('Y-MM-D');
                    dateControls[".repeat-end-time"].minDate = dates.clickedDate.format('Y-MM-D');


                    generateDateControls(dateControls);
                    
                    //helpers.formatDateFields(dateControls);
                    generateOccurenceOptions();
                    helpers.makeSelect2(formControls.scheduleAuditDropdowns);
                    //helpers.generateMulitSelectChecklist();
                    $('.schedule-area').select2({ dropdownParent: $("#areas_and_checklists") });
                    $('.schedule-subarea').select2({ dropdownParent: $("#areas_and_checklists") });
                    enableDisableFields();
                    $('#my-select').multiSelect({
                        selectableHeader: "<div class='custom-header'>Checklists</div>",
                        selectionHeader: "<div class='custom-header'>Selected Checklists</div>",
                        keepOrder: true
                        //afterSelect: () => {
                        //    selectedChecklistsCount = selectedChecklistsCount + 1;
                        //    $('.audit-checklists-count').html(selectedChecklistsCount);
                        //},
                        //afterDeselect: () => {
                        //    selectedChecklistsCount = selectedChecklistsCount - 1;
                        //    $('.audit-checklists-count').html(selectedChecklistsCount);
                        //}
                    });

                    //GetAllLocations(Header.UUID);
                    GetAllCities(Header.UUID);
                    $('[name=CreatedBy]').val(Header.UserFullName);

                    // To reset the index of the area, subarea and checklist selected
                    index = 0;
                    secondIndex = 0;

                    // scroll the form to top on load
                    let sideDivObj = document.getElementById('ScheduleAuditSideDiv');
                    sideDivObj.scrollTop = 0;

                    $('#OpenInEditMode').val('false');

                    // Run initial options for the repeat feature
                    AddRepeat.bindFlatpickrToRepeatFields();
                    AddRepeat.repeatObj.isRepeatSet = false;
                }
            });
        },

        // Get the Schedule Checklist partial form with data
        editScheduleAuditPartial: (date, id) => {
            $.ajax({
                url: '/SchedulingMgmt/SchedulingManagement/GetScheduleAddAudit',
                //data: { AuditInfoId: id},
                contentType: 'application/html; charset=utf-8',
                type: 'GET',
                dataType: 'html',
                beforeSend: function () {
                    $('.ajax-loader').css("visibility", "visible");
                },
                //complete: function () {
                //    $('.ajax-loader').css("visibility", "hidden");
                //},
                success: function (result) {
                    eventCall = 0;
                    $('#ScheduleAuditSideDiv').html(result);
                    dates.clickedDate = date;
                    auditFormVariables.selectedAreaCount = 0;
                    let scheduleDate = date.format('MMMM Do YYYY');
                    let scheduleDay = date.format('dddd');
                    $('.audit-schedule-date').html(scheduleDate);
                    $('.audit-schedule-day').html(scheduleDay);
                    let startDate = date.format('Y-MM-D');
                    $('#start_date').val(startDate);
                    $('#created_date').val(moment().format('Y-MM-D'));
                    dateControls[".start-time"].minDate = moment(dates.clickedDate.format('Y-MM-D') + ' ' + moment().format('h:mm A'), 'Y-MM-D h:mm A').format('Y-MM-D h:mm A') /*dates.clickedDate.format();*/
                    dateControls[".start-time"].maxDate = moment(dates.clickedDate.format('Y-MM-D')).endOf('day').format('Y-MM-D h:mm A'); /*dates.clickedDate.format('Y-MM-D')*/;
                    //dateControls[".start-time"].defaultDate = moment(dates.clickedDate.format('Y-MM-D') + ' ' + moment().format('h:mm A')).format('Y-MM-D h:mm A'); /* dates.clickedDate.format();*/
                    dateControls[".start-time"].defaultDate = dateControls[".start-time"].minDate;

                    // Set the minimum time so it does not go below beyond current time for today's date.
                    dates.clickedDate.format('Y-MM-D') === moment().format("Y-MM-D") ? dateControls[".start-time"].minTime = Date.now() : dateControls[".start-time"].minTime = '';
                    dates.clickedDate.format('Y-MM-D') === moment().format("Y-MM-D") ? dateControls[".repeat-end-time"].minTime = Date.now() : dateControls[".repeat-end-time"].minTime = '';


                    let newDate = moment(dateControls[".start-time"].defaultDate, 'Y-MM-D h:mm A');

                    dateControls[".end-time"].defaultDate = newDate.add(8, 'h').format('Y-MM-D h:mm A');
                    dateControls[".expiry-time"].defaultDate = newDate.add(2, 'h').format('Y-MM-D h:mm A');

                    dateControls[".end-time"].minDate = dates.clickedDate.format('Y-MM-D');
                    dateControls[".expiry-time"].minDate = dates.clickedDate.format('Y-MM-D');
                    dateControls[".repeat-end-time"].minDate = dates.clickedDate.format('Y-MM-D');
                 //   $('.repeat-modal-btn').addClass('disabled');
                    generateDateControls(dateControls);

                    //helpers.formatDateFields(dateControls);
                    generateOccurenceOptions();
                    helpers.makeSelect2(formControls.scheduleAuditDropdowns);
                    //helpers.generateMulitSelectChecklist();
                    $('.schedule-area').select2({ dropdownParent: $("#areas_and_checklists") });
                    $('.schedule-subarea').select2({ dropdownParent: $("#areas_and_checklists") });
                    enableDisableFields();
                    $('#my-select').multiSelect({
                        selectableHeader: "<div class='custom-header'>Select Checklists</div>",
                        selectionHeader: "<div class='custom-header'>Selected Checklists</div>",
                        //afterSelect: () => {
                        //    selectedChecklistsCount = selectedChecklistsCount + 1;
                        //    $('.audit-checklists-count').html(selectedChecklistsCount);
                        //},
                        //afterDeselect: () => {
                        //    selectedChecklistsCount = selectedChecklistsCount - 1;
                        //    $('.audit-checklists-count').html(selectedChecklistsCount);
                        //}
                    });

                    GetAllCities(Header.UUID);
                    $('[name=CreatedBy]').val(Header.UserFullName);

                    // To reset the index of the area, subarea and checklist selected
                    index = 0;
                    secondIndex = 0;

                    // scroll the form to top on load
                    let sideDivObj = document.getElementById('ScheduleAuditSideDiv');
                    sideDivObj.scrollTop = 0;

                    AddRepeat.bindFlatpickrToRepeatFields();

                    $.ajax({
                        url: '/SchedulingMgmt/Audit/GetAuditDetailsForEditByAuditId?AuditID=' + id,
                        type: 'GET',
                        success: async function (data) {
                            //console.log(data);
                           // $('.breadcrumb-dashboard').append('<a href="/SchedulingMgmt/SchedulingManagement/ScheduleAudit" title="Schedule">Schedule</a> > Edit Schedule');
                            await GetAllLocations(Header.UUID, data.auditCity);
                            AuditSchedule.AuditCity = data.auditCity;
                            AuditSchedule.AuditID = data.auditInfoID;
                            AuditSchedule.AuditCode = data.auditCode;
                            AuditSchedule.AuditStatus = "Active";
                            AuditSchedule.AuditName = data.auditName;
                            //AuditSchedule.CreatedBy = data.createdBy;
                            //AuditSchedule.CreatedDateTime = moment(data.createdDate + " " + data.createdTime).utc().format();
                            ////console.log(AuditSchedule);
                            $('#AuditTitle').val(data.auditName);
                            $('#PreSelectedScheduleLocation').val(data.assignedToUserUUID);
                            $('.schedule-city').val(data.auditCity).trigger('change');
                            $('.schedule-location').val(data.auditLocationCode).trigger('change');
                            $('.audit-form-submit-btn').text('Update');


                            // GET DISTINCT CHECKLIST DATA:
                            var tempArr = [];
                            $.each(data.checklistDetails, function (index, checklist) {
                                tempArr.push({
                                    $selectedSubAreaCode: checklist.subAreaCode,
                                    $selectedSubAreaName: checklist.subAreaName,
                                    $selectedAreaName: checklist.areaName
                                });
                            });

                            const grouped = tempArr.reduce(function (groups, current) {
                                const key = current.$selectedSubAreaCode;
                                groups[key] = (groups[key] || 0) + 1;
                                return groups;
                            }, {});

                            const result = Object.keys(grouped).map(key => ({ Code: key, Count: grouped[key] }));

                            var ChecklistData = tempArr.reduce(function (accumulator, currentValue) {
                                var matches = accumulator.filter(function (element) {
                                    return currentValue.$selectedSubAreaCode == element.$selectedSubAreaCode;
                                });
                                if (matches.length == 0) accumulator.push(currentValue);
                                return accumulator;
                            }, []);

                            $.each(ChecklistData, function (index, value) {
                                var countObj = result.find(function (current) {
                                    return current.Code == value.$selectedSubAreaCode;
                                });
                                value.$selectedChecklistsCount = countObj.Count;
                            });
                            ////console.log(ChecklistData);

                            var mainArr = [];
                            $.each(ChecklistData, function (index, value) {
                                var tempObj = {
                                    ChecklistData: value
                                }

                                tempObj.checkListDescriptions = [];
                                $.each(data.checklistDetails, function (index, checklist) {
                                    if (checklist.subAreaCode == value.$selectedSubAreaCode) {
                                        tempObj.checkListDescriptions.push(
                                            {
                                                checklistCode: checklist.checklistCode,
                                                checklistId: checklist.checklistID,
                                                checklistName: checklist.checklistName
                                            }
                                        );
                                    }
                                });

                                mainArr.push(tempObj);
                            });

                            ////console.log(mainArr);

                            $.each(mainArr, function (index, area) {
                                generateAreaChecklistCard(area.ChecklistData, area.checkListDescriptions, []);
                            });

                            auditFormVariables.selectedAreaCount = mainArr.length
                            $('.selected-area-count').html(auditFormVariables.selectedAreaCount);
                            $('.added-area-checklist').find('.card').removeClass('highlight');

                            $("#ScheduleAuditForm").attr("update-form", true);

                            dates.clickedDate = moment(data.auditStartDate + " " + data.auditStartTime);
                            dateControls[".start-time"].minDate = moment().format('Y-MM-D h:mm A') /*dates.clickedDate.format();*/
                            dateControls[".start-time"].maxDate = ""; //moment(dates.clickedDate.format('Y-MM-D')).endOf('day').format('Y-MM-D h:mm A'); /*dates.clickedDate.format('Y-MM-D')*/;
                            //dateControls[".start-time"].defaultDate = moment(dates.clickedDate.format('Y-MM-D') + ' ' + moment().format('h:mm A')).format('Y-MM-D h:mm A'); /* dates.clickedDate.format();*/
                            dateControls[".start-time"].defaultDate = moment(data.auditStartDate + " " + data.auditStartTime).format();

                            // Set the minimum time so it does not go below beyond current time for today's date.
                            //dates.clickedDate.format('Y-MM-D') === moment().format("Y-MM-D") ? dateControls[".start-time"].minTime = Date.now() : dateControls[".start-time"].minTime = '';
                            //dates.clickedDate.format('Y-MM-D') === moment().format("Y-MM-D") ? dateControls[".repeat-end-time"].minTime = Date.now() : dateControls[".repeat-end-time"].minTime = '';
                            //dateControls[".start-time"].minTime = Date.now();
                            moment(data.auditStartDate).format("Y-MM-D") === moment().format("Y-MM-D") ? dateControls[".start-time"].minTime = Date.now() : dateControls[".start-time"].minTime = '';

                            let newDate = moment(dateControls[".start-time"].defaultDate, 'Y-MM-D h:mm A');

                            dateControls[".end-time"].defaultDate = moment(data.auditEndDate + " " + data.auditEndTime).format();//newDate.add(8, 'h').format('Y-MM-D h:mm A');
                            dateControls[".expiry-time"].defaultDate = moment(data.auditExpiryDate + " " + data.auditExpiryTime).format()//newDate.add(2, 'h').format('Y-MM-D h:mm A');

                            dateControls[".end-time"].minDate = moment().format('Y-MM-D h:mm A');
                            dateControls[".expiry-time"].minDate = moment().format('Y-MM-D h:mm A');
                            dateControls[".repeat-end-time"].minDate = dates.clickedDate.format('Y-MM-D');

                            generateDateControls(dateControls);

                            $("td[data-date=" + dates.clickedDate.format('YYYY-MM-DD') + "]").addClass("clicked-day-highlight");

                            $('.start-time').on('change', function () {
                                if (moment($('.start-time').val()).isBefore(moment())) {
                                    return false;
                                    //flatpickr(".start-time", { defaultDate: moment(data.auditStartDate + " " + data.auditStartTime).format() });
                                }
                            });

                            //flatpickr(".start-time", { defaultDate: moment(data.auditStartDate + " " + data.auditStartTime).format() });
                        }
                    });
                }
            });
        },
        
        // Get the Schedule Checklist partial view to view all events
        viewScheduledAuditPartial: (date, isClickedDayLessThanToday) => {
            // Get the date in utc format to fetch the schedules
            let schedulesFrom = moment(date.format('Y-MM-D h:mm'), 'Y-M-D h:mm A').startOf('day').format(); // date.startOf('day').format();//moment(date.format('Y-MM-D h:mm')).startOf('day').format();
            let schedulesTo = moment(date.format('Y-MM-D h:mm'), 'Y-M-D h:mm A').endOf('day').format();
            $.ajax({
                url: '/SchedulingMgmt/SchedulingManagement/GetScheduleViewAudit',
                contentType: 'application/html; charset=utf-8',
                type: 'GET',
                data: { StartDateTime: schedulesFrom /*'UU0000011'*/, EndDateTime: schedulesTo },
                dataType: 'html',
                beforeSend: function () {
                    $('.ajax-loader').css("visibility", "visible");
                },
                complete: function () {
                    $('.ajax-loader').css("visibility", "hidden");
                    if ($('#OpenInEditMode').val() === "false") {
                        $('.ajax-loader').css("visibility", "hidden");
                    }
                },
                success: function (result) {
                    $('#ScheduleAuditSideDiv').html(result);
                    dates.clickedDate = moment(date.format('Y-MM-D'));
                    let scheduleDate = date.format('MMMM Do YYYY');
                    let scheduleDay = date.format('dddd');
                    $('.audit-schedule-date').html(scheduleDate);
                    $('.audit-schedule-day').html(scheduleDay); 
                    if (isClickedDayLessThanToday) {
                        $('.add-schedule-rightview').hide();
                    }

                    // scroll the div to the top after publishing a schedule.
                    let sideDivObj = document.getElementById('ScheduleAuditSideDiv');
                    sideDivObj.scrollTop = 0;

             


                    //if ($('#OpenInEditMode').val() === "true") {
                    //    if ($('.edit_option[id=' + $('#EditModeAuditID').val() + ']').length != 0) {
                    //        $('.edit_option[id=' + $('#EditModeAuditID').val() + ']').click();
                    //    } else {
                    //        $('.ajax-loader').css("visibility", "hidden");
                    //    }
                    //}
                }
            });
        },

        // div to show/hide on click of Occurences
        ToggleAuditOccurences: (selector) => {
            let occurence = $(selector).val();
            if (occurence === 'repeat')
                $('.audit-repeat-time').css('display', '');
            else
                $('.audit-repeat-time').css('display', 'none');

            // Some default code. Will be removed in the future or may brought in use also.
            if ($('').is(':checked')) {
                // if so, remove the element from the "Draggable Events" list
                $(this).remove();
            }
        },

        // Save the Checklist Schedule
        saveAuditScheduleOnSubmit: () => {
            // getFormData returns the formdata and stringified formdata in an object
            fdObj = helpers.getFormData();
            fd = fdObj.fd;
            data = fdObj.data;

            //console.log(fd);
            //console.log(data);
            helpers.printFormData(fd);

            // Call the Save Method of Controller to save the schedule
            $.ajax({
                type: 'POST',
                url: '/SchedulingMgmt/SchedulingManagement/SaveScheduleChecklist',
                data: data,
                success: function (results) {
                    let schedules = [];
                    //console.log(results);
                    results.forEach(function (result, index) {
                        let startDate = helpers.formatDate(result.ScheduleStartDate);
                        schedules.push({
                            title: '',
                            start: startDate,
                            startTime: moment().format('dd-mmm-yyyy h:mm A'),
                            allDay: false
                        });
                    });
                    GenerateCalendar(schedules);
                }
            });
        },

        // Get All the Schedules for the calendar on click of weeklist button
        //fetchWeekScheduleList: () => {
            
        //    });
            //if ($('#OpenInEditMode').val() === "true") {
            //    schedulerHandlers.editScheduleAuditPartial(moment(), $('#EditModeAuditID').val());
            //} else {
            //    schedulerHandlers.viewScheduledAuditPartial(moment(), false);
            //}
       // },

        // Get all the schedules and render it on the calendar
        fetchSchedulesAndRender: () => {
            jQuery.ajax({
                url: '/SchedulingMgmt/SchedulingManagement/GetAllPublishedSchedules',
                type: 'GET',
                beforeSend: function () {
                    $('.ajax-loader').css("visibility", "visible");
                },
                complete: function () {
                    //$('.ajax-loader').css("visibility", "hidden");
                },
                success: function (results) {
                    let schedules = [];
                    //console.log(results);
                    results.forEach(function (result, index) {
                        let scheduleStartDateTime = moment(result.auditStartDate + " " + result.auditStartTime).format('Y-MM-D h:mm A');
                        let scheduleEndDateTime = moment(result.auditEndDate + " " + result.auditEndTime).format('Y-MM-D h:mm A');
                        // debugger;
                        schedules.push({
                            title: result.auditName,
                            start: scheduleStartDateTime,
                            end: scheduleEndDateTime,
                            auditStartDate: result.auditStartDate,
                            auditStartTime: result.auditStartTime,
                            auditEndDate: result.auditEndDate,
                            auditEndTime: result.auditEndTime,
                            startTime: moment().format('dd-mmm-yyyy hh:mm A'),
                            endTime: moment().format('dd-mmm-yyyy hh:mm A'),
                            assignedTo: result.assignedToUserName,
                            location: result.auditLocation,

                            allDay: false
                        });
                    });
                    GenerateCalendar(schedules);
                    // schedulerHandlers.viewScheduledAuditPartial(moment(), false);
                }
            });
            //GenerateCalendar(null);          
            if ($('#OpenInEditMode').val() === "true") {
                schedulerHandlers.editScheduleAuditPartial(moment(), $('#EditModeAuditID').val());
            } else {
                schedulerHandlers.viewScheduledAuditPartial(moment(), false);
            }
        }
        };

 
    // Generate the Calendar
    let GenerateCalendar = (schedules) => {
        $('#AuditCalendar').fullCalendar('destroy');
        $('#AuditCalendar').fullCalendar({
            header: {
                left: 'month,today', // agendaWeek, agendaDay,
                center: 'title',
                //right: 'listWeek,listDay, prev,next',
                right: 'listWeek, prev,next'

            },
            // Sets the name of the fields we have on top of the calendar
            buttonText: {
                today: 'Today',
                month: 'Month',
                week: 'Week',
                day: 'Day',
                listWeek: 'Week\'s List',
              //  listDay: 'Day\'s List'
            },
            editable: false,
            droppable: true, // this allows things to be dropped onto the calendar

            // Get the right div on Checklist drop
            //drop: function (date, jsEvent, view) {
            //    schedulerUtilities.getAddSchedulePartial(date);
            //},
            // Sets the range on the calendar
            //validRange: function (nowDate) {
            //    return {
            //        start: nowDate,
            //        //end: nowDate.clone().add(1, 'months')
            //    };
            //},
          
            eventRender: function (event, element, view) {
               
                if (view.type === 'listWeek' /*|| view.type === 'listDay'*/) {
                    var toInject = [];
                    toInject.push(event);
                   //  font-size: 14px;     text-transform: lowercase;  font-weight: 600;
                    //toInject.push(event.answer_date);
                    for (var i = 0; i < toInject.length; i++) {
                        element.append(`<td class="ui-widget-content col s6 m4 l4">

                        <div class="">
                        <div class="table-card-data col-align-card"  style="    margin-top: 3px">`+ toInject[i].auditStartDate + `, <small>` + toInject[i].auditStartTime + `</small></div>
                        <div class="table-card-data col-align-card"style="    margin-top: 3px">`+ toInject[i].auditEndDate + `, <small>` + toInject[i].auditEndTime + `</small ></div >
                    </div></td><td class="ui-widget-content col s6 m4 l4">
                    <div class="">
                        <div class="table-card-title col-align-card text-wrap" style="text-overflow: ellipsis;    white-space: nowrap;    overflow: hidden; margin-bottom: 5px " title="`+toInject[i].title+`">`+ toInject[i].title + `</div>
                        <div class="table-card-data col-align-card" style="text-overflow: ellipsis;    white-space: nowrap;    overflow: hidden; margin-top: 3px" title="`+toInject[i].location+`">`+ toInject[i].location + `</div>
                    </div></td><td class="ui-widget-content col s6 m4 l4">
                    <div class="">
                       
                        <div class="table-card-data col-align-card" style=" text-overflow: ellipsis;    white-space: nowrap;    overflow: hidden;   margin-top: 3px" title="`+toInject[i].assignedTo+`">`+ toInject[i].assignedTo + `</div>
                    </div></td>
                    `);
                    
                                    

                     // element.append('<td class="ui-widget-content fc-list-item-name">' + toInject[i].assignedTo + '</td>');
                   
                      //console.log(event.assignedTo);
                    }
                }
                
            },
            eventAfterAllRender: function (view) {
                if (view.type === 'listWeek' /*|| view.type === 'listDay'*/) {
                    //console.log(view.type + ' change colspan');
                    //console.log(view)
                      $('.fc-list-item-time').html('');
                $('.fc-list-item-marker').html('');
                $('.fc-list-item-title').html('');
                    var tableSubHeaders = jQuery("td.ui-widget-header");
                    //console.log(tableSubHeaders);
                    var numberOfColumnsItem = jQuery('tr.fc-list-item');
                    var maxCol = 0;
                    var arrayLength = numberOfColumnsItem.length;
                    for (var i = 0; i < arrayLength; i++) {
                        maxCol = Math.max(maxCol, numberOfColumnsItem[i].children.length);
                    }
                    //console.log("number of items : " + maxCol);
                    tableSubHeaders.attr("colspan", maxCol);

                }
              
            },

            // Get the right div on Day Click
            dayClick: function (date, jsEvent, view) {
                //schedulerUtilities.getViewSchedulePartial(date);
                $(".clicked-day-highlight").removeClass("clicked-day-highlight");
                $("td[data-date=" + date.format('YYYY-MM-DD') + "]").addClass("clicked-day-highlight");
            
                let isClickedDayLessThanToday = date.isBefore(moment().startOf('day'));
                if (isClickedDayLessThanToday) {
                    schedulerHandlers.viewScheduledAuditPartial(date, isClickedDayLessThanToday);
                } else {
                    schedulerHandlers.viewScheduledAuditPartial(date, isClickedDayLessThanToday);
                }
                window.history.pushState("", "", '/schedulingMgmt/SchedulingManagement/ScheduleAudit');
            },
            eventClick: function (calEvent, jsEvent, view) {
                //alert('Event: ' + calEvent.assignedTo);
                //alert('Event Start: ' + calEvent.start);
                //alert('Event Start Time:' + calEvent.startTime);
            },
            // Event renders all the scheduled events on the calendar
            events: schedules,
            timezone: 'local',
            //eventLimit: true,
            //eventLimitText: 'Audits',
            //views: {
            //    month: {
            //        eventLimit: 6 // adjust to 6 only for agendaWeek/agendaDay
            //    }
            //},
            
            
            timeFormat: 'dd-mmm-yyyy hh:mm A',
            themeSystem: 'jquery-ui'

        });
    };





    // Event Handler for Multi-Days Select on Schedule Audit Page
    $("#ScheduleAuditSideDiv")
        .off('click', '.schedule-occurence-option')
        .on('click', '.schedule-occurence-option', function () {
            schedulerHandlers.ToggleAuditOccurences(this);
        });
    window.addEventListener('online', () => {
        $('.audit-form-submit-btn').removeClass('disabled')
        if (navigator.onLine) {
            M.toast({ html: 'You are in Online !', displayLength: '6000', classes: 'success-toast' })
        }
        $('.view-schedule-rightview').removeClass('disabled')
        $('.add-schedule-rightview').removeClass('disabled')
        $('.dropdown-trigger').removeClass('disabled')
        enableAuditorDependentFields();
        if ($('.schedule-subarea').val() !=='') {
            enableAreaDependentFields();
        }
    });
    window.addEventListener('offline', () => {
        $('.audit-form-submit-btn').addClass('disabled')
      if (!navigator.onLine) {
          M.toast({ html: 'You are in Offline !', displayLength: '6000', classes: 'failed-toast' })
        }
        $('.view-schedule-rightview').addClass('disabled')
        $('.add-schedule-rightview').addClass('disabled')
        $('.dropdown-trigger').addClass('disabled')
        disableAuditorDependentFields()
    });
    // Event Handler to switch to add schedule partial on click of View Button
    $("#ScheduleAuditSideDiv").off('click', '.add-schedule-rightview')
        .on('click', '.add-schedule-rightview', function () {
            schedulerHandlers.addScheduleAuditPartial(dates.clickedDate);
               


            //console.log($('#OpenInEditMode').val());;
        });

    // Event Handler to switch to edit schedule partial on click of Edit Button
    $('.page-content').on('click', '.edit_option', function () {
        console.log($(this).val());

        if (!navigator.onLine) {
            M.toast({ html: 'You are in Offline !', displayLength: '6000', classes: 'failed-toast' })
        }
        else if ($(this).val()) {
            $.alert({
                title: 'You Cannot Edit !',
                content: 'Auditor Started to Perform the Audit!',
                draggable: false
            });

        }else {
            schedulerHandlers.editScheduleAuditPartial(dates.clickedDate, $(this).attr('id'));
            $('#OpenInEditMode').val("true");
            window.history.pushState("", "", '/schedulingMgmt/SchedulingManagement/EditScheduledAudit?AuditID=' + $(this).attr('id'));
            //console.log($('#OpenInEditMode').val());
        }
    });

    // Event Handler to switch to View schedule partial on click of Add Button
    $("#ScheduleAuditSideDiv").off('click', '.view-schedule-rightview')
        .on('click', '.view-schedule-rightview', function () {
            if ($('#OpenInEditMode').val() === "true") {
                $("#ScheduleAuditForm").removeAttr("update-form");
              //  $('.repeat-modal-btn').addClass('disabled');
                $('#OpenInEditMode').val("false");
                window.history.pushState("", "", '/schedulingMgmt/SchedulingManagement/ScheduleAudit');                
            }
            schedulerHandlers.viewScheduledAuditPartial(dates.clickedDate);
            //console.log($('#OpenInEditMode').val());
        });

    // Refactor Audit Name
    $("#ScheduleAuditSideDiv").on('change', '.audit-name', function () {
        AuditSchedule.AuditName = $(this).val();
    });

    // Fill Locations on select of City
    $("#ScheduleAuditSideDiv").on('change', '.schedule-city', async function () {

      

        if ($('.schedule-city').val() !== null && $('.schedule-city').val().length > 0) {
            $('.city-validation-msg').addClass('hidden');
        }
        
        if ($(this).val() !== "") {
            $('.schedule-location').html('');
            AuditSchedule.AuditCity = $(this).val();
            $('.schedule-location').empty();
            $('.schedule-location').prepend("<option value=" + '' + ">" + '' + "</option>");
            await GetAllLocations(Header.UUID, $(this).val());

            $('.schedule-select-auditor').empty();
            $('.schedule-area').empty();
            $('.schedule-subarea').empty();
            $('#my-select').empty().multiSelect('refresh');
        }
        if ($('#OpenInEditMode').val() === "true") {
           // $('.repeat-modal-btn').addClass('disabled');
            $('.repeat-modal-btn').addClass('hidden');
            return;
        }
        clearAddedChecklists();
    });


    // Fill areas on select of location
    $("#ScheduleAuditSideDiv").on('change', '.schedule-location', async function () {
        if ($(this).val() !== "" && $(this).val() !== null) {

            // Refactor
            AuditSchedule.AuditLocation = $(this).children('option:selected').text();
            AuditSchedule.AuditLocationCode = $(this).children('option:selected').val();
            AuditSchedule.FBOCode = $(this).children('option:selected').attr('fbo-code');

            $('[name=AuditLocation]').val($(this).children('option:selected').text());
            //getAreasByLocation(Header.UUID, $(this).val());
            disableAuditorDependentFields();

            if ($('#PreSelectedScheduleLocation').val() != "") {
                GetAllAuditorsForEdit(Header.UUID, $(this).val(), $('#PreSelectedScheduleLocation').val());
            } else {
                $('.schedule-select-auditor').empty();
                $('.schedule-select-auditor').prepend("<option value=" + '' + ">" + 'Loading...' + "</option>");
                await GetAllAuditors(Header.UUID, $(this).val());
            }

            $('.schedule-area').empty();
            $('.schedule-subarea').empty();
            $('#my-select').empty().multiSelect('refresh');
        }
        else
            $('.schedule-select-auditor').empty();
        
        $('.schedule-area').empty();
        $('.schedule-subarea').empty();
        $('#my-select').empty().multiSelect('refresh');
        clearAddedChecklists();
    });

    // Fill sub-area on select of area
    $("#ScheduleAuditSideDiv").on('change', '.schedule-area', function () {


        if ($('.schedule-area').val() !== null && $('.schedule-area').val().length > 0) {
            $('.area-validation-msg').addClass('hidden');
        }

        if ($(this).val() !== "") {
            // Clear  the multi-select box when area is cleared in the dropdown
            $('#my-select').empty().multiSelect('refresh');
            $('.schedule-subarea').empty();
            $scheduleLocation = $('.schedule-location').val();
            getSubAreasAreasByUserAndLocation($('.schedule-select-auditor').val(), $scheduleLocation, $(this).val());
        }

        
        $('.audit-checklists-validation-msg').html('');
    });

    $("#ScheduleAuditSideDiv").on('change', '#my-select', function () {
        if ($('#my-select').val() !== '' || $('#my-select').val() !== null) {
            $('.audit-checklists-validation-msg').empty();
        }
    });
    
    let auditorUUID = '';
    // Fill UUID of User
    $("#ScheduleAuditSideDiv").on('change', '.schedule-select-auditor',function () {
        if ($(this).val() !== "") {

            // Refactor 
            AuditSchedule.AssignedToUserName = $(this).children('option:selected').text();
            AuditSchedule.AssignedToUserUUID = $(this).children('option:selected').val();
            AuditSchedule.AuditFBO = $(this).children('option:selected').attr('data-acc-alias');
            AuditSchedule.AuditImage = $(this).children('option:selected').attr('data-acc-alias') + '.png';

            $('[name=AssignedToUserName]').val($(this).children('option:selected').text());
            $('[name=AuditFBO]').val($(this).children('option:selected').attr('data-acc-alias'));
            $('[name=AuditImage]').val($(this).children('option:selected').attr('data-acc-alias') + '.png');
            auditorUUID = $(this).children('option:selected').val();
            //assignAuditorLOBCode($(this).children('option:selected').val());
            getAreasByLocation(auditorUUID, $('.schedule-location').val());
            
            $('.schedule-subarea').empty();
            $('#my-select').empty().multiSelect('refresh');
        }
        else {
            $('[name=AssignedToUserName]').val('');
            $('[name=AuditFBO]').val('');
        }
        
    });

    // Fill checklists on select of sub-area
    $("#ScheduleAuditSideDiv").on('change', '.schedule-subarea', function () {

        if ($('.schedule-subarea').val() !== null && $('.schedule-subarea').val() !== '') {
            $('.subarea-validation-msg').addClass('hidden');
        }

        if ($(this).val() !== "") {
            $('#myselect').html('');
            GetAllChecklistForAreaSubArea($(this).val());
        }

        // Clears the multi-select checklist
        //if ($('.schedule-subarea').val() === null || $('.schedule-subarea').val() === '') {
        //    $('#my-select').empty().multiSelect('refresh');
        //}

        $('#my-select').empty().multiSelect('refresh');

        $('.audit-checklists-validation-msg').html('');
    });

    $("#ScheduleAuditSideDiv").on('change', 'input[name=auditFrequency]', function () {
        AuditSchedule['frequency'] = $('[name="auditFrequency"]:checked').val();
        AuditSchedule['frequency'] === 'once' ? AuditSchedule['repeatType'] = '' : '';
    });

    $("#ScheduleAuditSideDiv").on('change', '.occurence-select', function () {
        AuditSchedule['repeatType'] = $('.occurence-select :selected').val();
    });

    



    $('#my-select').on('change', function () {
        if ($('#my-select').val().length !== 0) {
            $('.audit-checklists-validation-msg').html('');
        }
    });

    //$('#ScheduleAuditSideDiv').on('click', '.area-subarea-count' , function () {
        //$(this).find('.area-checklist').slideToggle();
    //});
    
    // Fetch all the events, load the calender and render it on the calendar when the page loads.
    schedulerHandlers.fetchSchedulesAndRender();

    // $(selector).on('change', function () {
    //            $(this).val() === '' ? $(dependentSelector).attr('disabled', true) : $(dependentSelector).attr('disabled', false);
    //});


    /*Author:       Abhishek Anshu
     * Date:        Dec 24th, 2018
     * Description: Method to disable/enable dropdown fields on change in values of dropdown
     * */
    let enableDisableFields = () => {
        $('.schedule-city').on('change', function () {
            $(this).val() === "" ? disableCityDependentFields() : enableCityDependentFields();
        });

        $('.schedule-location').on('change', function () {
            $(this).val() === "" ? disableLocationDependentFields() : enableLocationDependentFields();
        });

        $('.schedule-select-auditor').on('change', function () {
            $(this).val() === "" ? disableAuditorDependentFields() : enableAuditorDependentFields();
        });
        

        $('.schedule-area').on('change', function () {
            $(this).val() === "" ? disableAreaDependentFields() : enableAreaDependentFields();
        });
    };

    const eventHandlers = function () {
        // Remove validation messages on selection or inputs
        $('#ScheduleAuditForm').on("keyup change blur click", '[data-required]', function () {
            if ($('input[name="AuditName"]') !== null && $('input[name="AuditName"]').val().length > 0) {
                $('.audit-name-validation-msg').addClass('hidden');
            }

            if ($('.schedule-city') !== null && $('.schedule-city').val().length > 0) {
                $('.city-validation-msg').addClass('hidden');
            }

            if ($('.schedule-location').val() !== null && $('.schedule-location').val().length > 0) {
                $('.location-validation-msg').addClass('hidden');
            }

            if ($('.schedule-select-auditor').val() !== null && $('.schedule-select-auditor').val().length > 0) {
                $('.auditor-validation-msg').addClass('hidden');
            }

            if ($('.schedule-area').val() !== null && $('.schedule-area').val().length > 0) {
                $('.area-validation-msg').addClass('hidden');
            }

            if ($('.schedule-subarea').val() !== null && $('.schedule-subarea').val() !== '') {
                $('.subarea-validation-msg').addClass('hidden');
            }

            if ($('.start-time').val() !== null && $('.start-time').val() !== '') {
                $('.start-time-validation-msg').addClass('hidden');
            }

            if ($('.end-time').val() !== null && $('.end-time').val() !== '') {
                $('.end-time-validation-msg').addClass('hidden');
            }

            if ($('.expiry-time').val() !== null && $('.expiry-time').val() !== '' ) {
                $('.expiry-time-validation-msg').addClass('hidden');
            }

            if ($('.recurrence-end-date').val() !== null && $('.recurrence-end-date').val() !== '') {
                $('.recurrence-end-date-validation-msg').addClass('hidden');
            }

            if ($('.recurrence-start-date').val() !== null && $('.recurrence-start-date').val() !== '') {
                $('.recurrence-start-date-validation-msg').addClass('hidden');
            }

            const startDateMoment = moment($('.recurrence-start-date').val());
            const endDateMoment = moment($('.recurrence-end-date').val());
            if (endDateMoment.isAfter(startDateMoment)) {
                $('.recurrence-date-validation-msg').html('');
            }

            if ($('input[name=WeeklyDays]:checked').length > 0) {
                $('.recurrence-weekly-days').addClass('hidden');
            }
        });
    }

    if ($('#OpenInEditMode').val() === "false") {
        eventHandlers();
    }
    

    /* 
        Author:         Abhishek Anshu
        Date:           Apr 29th, 2019
        Description:    Method to clear the selected checklists from the UI and the AuditSchedule object.
     */
    const clearAddedChecklists = () => {
        AuditSchedule.AuditChecklistDto = [];
        $('.added-area-checklist').html('');
        $('.selected-area-count').html('0');
        auditFormVariables.selectedAreaCount = 0;
    };

    /* 
     * Author:          Abhishek Anshu
     * Date:            Jan 1st, 2019
     * Description:     Method to validate the Audit Schedule Form
    */
    let ValidateScheduleAuditForm = () => {
        isValid = true;

        let auditTitle = $('input[name="AuditName"]');
        if (auditTitle.val() === '' || auditTitle === null) {
            $('.audit-name-validation-msg').removeClass('hidden');
            let auditNameValidationText = $(auditTitle).attr('validation-msg');
            $('.audit-name-validation-msg').html(auditNameValidationText);
            isValid = false;
        }

        let citySelector = $('.schedule-city');
        if (citySelector.val() === '') {
                $('.city-validation-msg').removeClass('hidden');
            let cityValidationText = citySelector.attr('validation-msg');
                $('.city-validation-msg').html(cityValidationText);
                isValid = false;
        }

        if ($('.schedule-location').val() === '' || $('.schedule-select-auditor').val() === null) {
            $('.location-validation-msg').removeClass('hidden');
            let locationValidationText = $('.schedule-location').attr('validation-msg');
            $('.location-validation-msg').html(locationValidationText);
            isValid = false;
        }

        if ($('.schedule-select-auditor').val() === '' || $('.schedule-select-auditor').val() === null) {
            $('.auditor-validation-msg').removeClass('hidden');
            let auditorValidationText = $('.schedule-select-auditor').attr('validation-msg');
            $('.auditor-validation-msg').html(auditorValidationText);
            isValid = false;
        }

        //if ($('.schedule-area').val() === '' || $('.schedule-area').val() === null) {
        //    $('.area-validation-msg').removeClass('hidden');
        //    let areaValidationText = $('.schedule-area').attr('validation-msg');
        //    $('.area-validation-msg').html(areaValidationText);
        //    isValid = false;
        //}

        //if ($('.schedule-subarea').val() === '' || $('.schedule-subarea').val() === null) {
        //    $('.subarea-validation-msg').removeClass('hidden');
        //    let subareaValidationText = $('.schedule-subarea').attr('validation-msg');
        //    $('.subarea-validation-msg').html(subareaValidationText);
        //    isValid = false;
        //}

        if ($('.start-time').val() === '' || $('.start-time').val() === null) {
            $('.start-time-validation-msg').removeClass('hidden');
            let startTimeValidationText = $('.start-time').attr('validation-msg');
            $('.start-time-validation-msg').html(startTimeValidationText);
            isValid = false;
        }

        if ($('.end-time').val() === '' || $('.end-time').val() === null) {
            $('.end-time-validation-msg').removeClass('hidden');
            let endTimeValidationText = $('.end-time').attr('validation-msg');
            $('.end-time-validation-msg').html(endTimeValidationText);
            isValid = false;
        }

        if ($('.expiry-time').val() === '' || $('.expiry-time').val() === null) {
            $('.expiry-time-validation-msg').removeClass('hidden');
            let expiryTimeValidationText = $('.expiry-time').attr('validation-msg');
            $('.expiry-time-validation-msg').html(expiryTimeValidationText);
            isValid = false;
        }

        if (auditFormVariables.selectedAreaCount === 0) {
            $('.area-modal-validation-msg').removeClass('hidden');
            isValid = false;
        }


        //Validation for Date Fields
        let startTimeObj = moment($('.start-time').val(), 'Y-MM-D h:mm A');
        let endTimeObj = moment($('.end-time').val(), 'Y-MM-D h:mm A');
        //console.log(startTimeObj, endTimeObj);
        // Check if Start Time is greater/lesser than End Time
        isStartTimeValid = startTimeObj.isAfter(endTimeObj) || startTimeObj.isSame(endTimeObj);
        isStartTimeValid === true ? isValid = false : '';
        let expiryTimeObj = moment($('.expiry-time').val(), 'Y-MM-D h:mm A');
        isEndTimeValid = endTimeObj.isAfter(expiryTimeObj);
        isEndTimeValid === true ? isValid = false : '';

        // Check if end time is valid at the time of publishing audit i.e. it should be greater than current time.
        let isEndTimeAtSubmitValid =
            moment($('.end-time').val(), 'Y-MM-D h:mm A').isAfter(moment()) &&
            moment($('.expiry-time').val(), 'Y-MM-D h:mm A').isAfter(moment()) //&&
           // moment($('.start-time').val(), 'Y-MM-D h:mm A').isAfter(moment());
        isEndTimeAtSubmitValid === true ? '' : isValid = false;

        if (isEndTimeAtSubmitValid === false) {
            if (!moment($('.end-time').val(), 'Y-MM-D h:mm A').isAfter(moment())) {
                $('.audit-time-validation-msg').html('End Time should be greater than Current Time');
            }
            else if (!moment($('.expiry-time').val(), 'Y-MM-D h:mm A').isAfter(moment())) {
                $('.audit-expiry-time-validation-msg').html('Expiry Time should be greater than Current Time');
            }
            // else if (!moment($('.start-time').val(), 'Y-MM-D h:mm A').isAfter(moment())) {
              //  $('.audit-start-time-validation-msg').html('Start Time should be greater than Current Time');
           // }
        }

        return isValid;
    };

    /*
     * Author:      Abhishek Anshu
     * Date:        Feb. 15th 2019
     * Description: Fill the remaining fields of AuditSchedule Object
     */
    let fillScheduleAuditObj = function () {
        AuditSchedule.AuditScheduledStartDateTime = $('.start-time').val();
        AuditSchedule.AuditScheduledEndDateTime = $('.end-time').val();
        AuditSchedule.AuditExpiryDateTime = $('.expiry-time').val();
        AuditSchedule.CreatedBy = Header.UserFullName;
    };

    /*
     * Author:      Praveen KM
     * Date:        Dec. 28, 2018   
     * Description: Event Handler to post the data to the controller to save and publish the Audit Schedule.
     * Modified:    Feb 14th, 2019, Anshu
     */
    $("#ScheduleAuditForm").on('submit', function (event) {
        event.preventDefault();   
        if (!navigator.onLine) {
            M.toast({ html: 'You are in Offline !', displayLength: '6000', classes: 'failed-toast' });

        } else {

            let arr = [];
            arr.push(AuditSchedule);

            let isFormValid = true;
            isFormValid = ValidateScheduleAuditForm();

            let scheduleAuditFD = new FormData($('#ScheduleAuditForm')[0]);
            for (var pair of scheduleAuditFD.entries()) {
                //if (pair[0] === "AuditScheduledStartDateTime") { pair[1] = moment(pair[1]).utcOffset(330).utc().format(); }
                //console.log(pair[0] + ', ' + pair[1]);
            }

            if (isFormValid === true) {
                AuditSchedule.AuditScheduledStartDateTime = $('.start-time').val();
                AuditSchedule.AuditScheduledEndDateTime = $('.end-time').val();
                AuditSchedule.AuditExpiryDateTime = $('.expiry-time').val();
                AuditSchedule.CreatedBy = Header.UserFullName;
                AuditSchedule.AuditInfoID = AuditSchedule.AuditID;


                let schedulesList = '';
                if (AuditSchedule.frequency === 'repeat') {
                    schedulesList = AddRepeat.createRepeatingSchedules(AuditSchedule);
                }
                else if (AuditSchedule.frequency === 'once') {
                    schedulesList = [];
                    AuditSchedule.AuditScheduledStartDateTime = moment($('.start-time').val()).utc().format();
                    AuditSchedule.AuditScheduledEndDateTime = moment($('.end-time').val()).utc().format();
                    AuditSchedule.AuditExpiryDateTime = moment($('.expiry-time').val()).utc().format();
                    //if (AuditSchedule.CreatedDateTime == "") {
                    AuditSchedule.CreatedDateTime = moment().utc().format();
                    //}
                    schedulesList.push(AuditSchedule);
                }

                AuditSchedule.AuditChecklistDto.forEach(function (checklist, index) {
                    checklist.checklistScheduledStartDateTime = AuditSchedule.AuditScheduledStartDateTime;
                    checklist.checklistScheduledEndDateTime = AuditSchedule.AuditScheduledEndDateTime;
                });

                var SaveAuditUrl = "";
                if ($('#OpenInEditMode').val() === 'false') {
                    SaveAuditUrl = "/SchedulingMgmt/SchedulingManagement/PostAuditData";
                } else {
                    SaveAuditUrl = "/SchedulingMgmt/SchedulingManagement/PostUpdatedAuditData";
                }

                //fillScheduleAuditObj();
                $('.audit-form-submit-btn').addClass('disabled');
                $.ajax({
                    type: 'POST',
                    url: SaveAuditUrl,
                    //contentType: "application/json; charset=utf-8",
                    data: { scheduleJSON: JSON.stringify(schedulesList)/*, repeatData: scheduleRepeatData*/ },
                    //data: ({ schedules: AuditSchedule }),//, repeatData: scheduleRepeatData}),
                    cache: false,
                    beforeSend: function () {
                        $('.ajax-loader').css("visibility", "visible");
                    },
                    //complete: function () {
                    //    //$('.ajax-loader').css("visibility", "hidden");
                    //},
                    success: function (result) {
                        //M.toast({ html: 'Audit Saved Successfully !' });
                        var obj = result;
                        if (obj !== null) {
                            $.ajax({
                                url: '/SchedulingMgmt/SchedulingManagement/PublishAuditSchedule',
                                type: 'POST',
                                async: false,
                                data: { auditCodes: result },
                                success: function () {
                                    index = 0;
                                    secondIndex = 0;
                                    EmptyAuditObject();
                                    //resetAddAuditScheduleForm();
                                    schedulerHandlers.viewScheduledAuditPartial(dates.clickedDate);
                                    //$('.view-schedule-rightview').trigger('click');
                                 

                                    jQuery.ajax({
                                        url: '/SchedulingMgmt/SchedulingManagement/GetAllPublishedSchedules',
                                        type: 'GET',

                                        complete: function () {
                                            $('.fc-month-button').trigger('click');
                                        },
                                        success: function (results) {
                                            let schedules = [];
                                            //console.log(results);
                                            results.forEach(function (result, index) {
                                                let scheduleStartDateTime = moment(result.auditStartDate + " " + result.auditStartTime).format('Y-MM-D h:mm A');
                                                let scheduleEndDateTime = moment(result.auditEndDate + " " + result.auditEndTime).format('Y-MM-D h:mm A');
                                                // debugger;
                                                schedules.push({
                                                    title: result.auditName,
                                                    start: scheduleStartDateTime,
                                                    end: scheduleEndDateTime,
                                                    auditStartDate: result.auditStartDate,
                                                    auditStartTime: result.auditStartTime,
                                                    auditEndDate: result.auditEndDate,
                                                    auditEndTime: result.auditEndTime,
                                                    startTime: moment().format('dd-mmm-yyyy hh:mm A'),
                                                    endTime: moment().format('dd-mmm-yyyy hh:mm A'),
                                                    assignedTo: result.assignedToUserName,
                                                    location: result.auditLocation,

                                                    allDay: false
                                                });
                                            });
                                            GenerateCalendar(schedules);
                                         
                                        }
                                    });
                                   
                                    

                                    if ($('#OpenInEditMode').val() === "true") {
                                        M.toast({ html: 'Audit Updated Successfully!', displayLength: '6000', classes: 'success-toast' });
                                        window.history.pushState("", "", '/schedulingMgmt/SchedulingManagement/ScheduleAudit');
                                    } else {
                                        M.toast({ html: 'Audit Published Successfully!', displayLength: '6000', classes: 'success-toast' });
                                    }

                                },
                                error: function () {
                                    // resetAddAuditScheduleForm();
                                    //$('.view-schedule-rightview').trigger('click');
                                    M.toast({ html: 'Audit Publish Failed ! Please reload the form and try again!', displayLength: '4000', classes: 'failed-toast' });
                                    $('.audit-form-submit-btn').removeClass('disabled');
                                    $('.ajax-loader').css("visibility", "hidden");
                                }
                            });

                        } // end of if


                    },
                    error: function (error) {
                        M.toast({ html: 'Audit Save Failed !', displayLength: '4000', classes: 'failed-toast' });
                        $('.audit-form-submit-btn').removeClass('disabled');
                        $('.ajax-loader').css("visibility", "hidden");
                    }
                });// AJAX ends here  
            } else {
                M.toast({ html: 'Fill all the Fields correctly !' });
            }

        }

    

        //scheduleDates = [{
        //    ScheduleStartDateTime: moment($('.start-time').val()),
        //    ScheduleEndDateTime: moment($('.end-time').val()),
        //    ScheduleExpiryDateTime: moment($('.expiry-time').val()),
        //    ScheduleRepeatEndDateTime: moment($('.repeat-end-time').val())
        //}];
        //AuditSchedule.ScheduleDates = scheduleDates;

      


        //const a = moment($('.start-time').val());
        //const b = moment($('.end-time').val())
        //const c = moment($('.start-time').val()).add(10, 'd');
        //getDatesForRepeat.daily(a.clone(), b.clone(), c.clone());
        //getDatesForRepeat.weekdays(a.clone(), b.clone(), c.clone());
        //getDatesForRepeat.weekly(a.clone(), b.clone(), c.clone());
        //getDatesForRepeat.quarterly(a.clone(), b.clone(), c.clone().add(4, 'quarter'));
        //getDatesForRepeat.annualy(a.clone(), b.clone(), c.clone().add(3, 'year'));
        //getDatesForRepeat.firstDay(a.clone(), b.clone(), c.clone().add(1, 'year'));
        //getDatesForRepeat.lastDay(a.clone(), b.clone(), c.clone().add(6, 'month'));

        //const scheduleObj = createRepeatingSchedules(AuditSchedule,
            //{ repeatType: 'daily', startDateTime: a, endDateTime: b, repeatEndDate: c });

    });

    function disableCityDependentFields() {
        //$('.schedule-select-auditor, .schedule-area, .schedule-subarea').attr('disabled', true);
    }

    function enableCityDependentFields() {
        $('.schedule-location').attr('disabled', false);
        $('.schedule-select-auditor, .schedule-area, .schedule-subarea').attr('disabled', true);
    }

    function disableLocationDependentFields() {
        $('.schedule-select-auditor, .schedule-area, .schedule-subarea').attr('disabled', true);
    }

    function enableLocationDependentFields() {
        $('.schedule-select-auditor').attr('disabled', false);
    }

    function disableAuditorDependentFields() {
        $('.schedule-area, .schedule-subarea').attr('disabled', true);
    }

    function enableAuditorDependentFields() {
        $('.schedule-area').attr('disabled', false);
    }
    
    function disableAreaDependentFields() {
        $('.schedule-subarea').attr('disabled', true);
    }

    function enableAreaDependentFields() {
        $('.schedule-subarea').attr('disabled', false);
    }

    /*
     * Author:      Abhishek Anshu
     * Date:        Dec 22nd, 2018
     * Description: Add the occurence options dynamically to the occurence dropdown
     */
    let generateOccurenceOptions = () => {
        let weeklyOn = dates.clickedDate.format('dddd');
        let scheduleDate = dates.clickedDate.format('D');
        let annualyOn = dates.clickedDate.format('MMMM');

        let occurenceOptions = `
            <option value='daily'>Daily</option>
            <option value='weekdays'>Mon - Fri</option>
            <option value="weekly">Weekly on ${weeklyOn}</option>
            <option value="monthly">Monthly on ${scheduleDate}</option>
            <option value="quarterly">Quarterly on on ${scheduleDate}</option>
            <option value="annualy">Annualy on ${annualyOn} ${scheduleDate}</option>
        `;
        $('.occurence-select').append(occurenceOptions);

    };

    // Method to reset the Schedule Audit Form
    let resetAddAuditScheduleForm = function (event) {
        if (event !== undefined) {
            event.preventDefault();
        }

        // Reset the Audit title field
        $('#AuditTitle').val('');

        // Clear the Select2 Dropdowns
        $('.schedule-location, .schedule-area, .schedule-subarea, .occurence-select, .schedule-select-auditor, .schedule-city').val(null).trigger('change');

        /* Clear the Flatpickr Date Pickers - Respects the formatting set by instance and keeps it after reseting
            Clearing the Flatipickr fields requires the field instance if the plugin options need to be preserved after resetting
            dateControllObj contains the instance of the date input fields used on the Scheduling Audit form.
        .*/
        dateControlInitObj.forEach(function (dateControlInstance, index) {
            dateControlInstance.clear();

            // Clear the Flatpickr Date Pickers - following statement resets all the formatting set by options too
            //$('.start-time, .end-time, .expiry-time, .schedule-end-time').flatpickr().clear();

        });
    };

    /*
     * Author:      Abhishek Anshu
     * Date:        Dec 26th, 2018
     * Description: Event Handler to reset the create audit form
     */
    $("#ScheduleAuditSideDiv").off('click', '.audit-form-reset-btn')
        .on('click', '.audit-form-reset-btn', function (event) {
            event.preventDefault();
            $('#OpenInEditMode').val("false");
            window.history.pushState("", "", '/schedulingMgmt/SchedulingManagement/ScheduleAudit');
            schedulerHandlers.viewScheduledAuditPartial(dates.clickedDate);
            //console.log($('#OpenInEditMode').val());
            //$('.view-schedule-rightview').trigger('click');

            /*
            //Reset the Audit title field
            $('#AuditTitle').val('');

            //Clear the Select2 Dropdowns
            $('.schedule-location, .schedule-area, .schedule-subarea, .occurence-select, .schedule-select-auditor').val(null).trigger('change');

            //Clear the Flatpickr Date Pickers - Respects the formatting set by instance and keeps it after reseting
            dateControlInitObj.forEach(function (dateControlInstance, index) {
                dateControlInstance.clear();

                //Clear the Flatpickr Date Pickers - following statement resets all the formatting set by options too
                //$('.start-time, .end-time, .expiry-time, .schedule-end-time').flatpickr().clear();

            });
            $('.view-schedule-rightview').trigger('click');
            */
        });

    // Modal Validation
     auditFormVariables = {
        selectedAreaCount : 0
    };

    var index = 0;
    var secondIndex = 0;
    $("#ScheduleAuditSideDiv").off('click', '.area-checklist-add-btn')
        .on('click', '.area-checklist-add-btn', function () {           
            let $selectedAreaCode = $('.schedule-area').select2().val();
            let $selectedAreaName = $('.schedule-area option:selected').text();
            
            let $selectedSubAreaCode = $('.schedule-subarea').select2().val();
            let $selectedSubAreaName = $('.schedule-subarea option:selected').text();
            let $selectedChecklistsCount = 0/*$('#my-select').val().length*/;
            let $selectedChecklists = [];
            let $selectedChecklistsCode = [];

            // Refactor Selected Checklist into an object
            for (let i = 0; i < $('#my-select option:selected').length; i++) {
                const selectedChecklist = $('#my-select option:selected')[i];
                const checklistId = selectedChecklist.value;
                const checklistCode = selectedChecklist.getAttribute('checklist-code');
                const checklistName = selectedChecklist.innerText;
                $selectedChecklists.push({ checklistId: checklistId, checklistCode: checklistCode, checklistName: checklistName });
            }

            // Refactor: Commented for Refactor test
            //for (let i = 0; i < $('#my-select option:selected').length; i++) {
            //    $selectedChecklists.push($('#my-select option:selected')[i].innerText);
            //    $selectedChecklistsCode.push($('#my-select option:selected')[i].value);
            //}


            if ($selectedAreaCode === '' || $selectedAreaCode === null) {
                //$('.audit-area-validation-msg').html('Please enter the area');
                $('.area-validation-msg').removeClass('hidden').html('Select an area');
            } else if ($selectedSubAreaCode === '' || $selectedSubAreaCode === null) {
                //$('.audit-subarea-validation-msg').html('Please select the checklists');
                $('.subarea-validation-msg').removeClass('hidden').html('Select a subarea');
            }else if ($('#my-select').val().length === 0) {
                $('.audit-checklists-validation-msg').html('Please select the checklists');
            }
            else {
                $selectedChecklistsCount = $('#my-select').val().length;

                var AreaObj = { Area: "", Checklists: [] };
                var AreaObjSelected = { Area: "", Checklists: [] };
                var Exists = false;

                $.each(AuditSchedule.AuditChecklistDto, function (i, v) {
                    if ($selectedSubAreaCode === v.SubAreaCode) {
                        Exists = true;
                        AreaObj.Area = v.SubAreaCode;
                        AreaObj.Checklists.push(v.ChecklistCode);
                    }
                });

                if (Exists) {

                    $.each($selectedChecklists, function (i, v) {
                        AreaObjSelected.Area = $selectedSubAreaCode;
                        AreaObjSelected.Checklists.push(v.checklistCode);
                    });

                    if (AreaObjSelected.Checklists.every(function (val) { return AreaObj.Checklists.indexOf(val) >= 0; })) {
                        M.toast({ html: 'This is a duplicate checklist(s)!', displayLength: '3000', classes: 'failed-toast', completeCallback: function () { $('.area-checklist-add-btn').removeClass('disabled'); } });
                        $(this).addClass('disabled');
                        return false;
                    } else if (AreaObj.Checklists.every(function (val) { return AreaObjSelected.Checklists.indexOf(val) >= 0; })) {
                        $("#" + AreaObj.Area).click();
                    } else {
                        $.each(AreaObj.Checklists, function (i, code) {
                            var flag = true;
                            $.each($selectedChecklists, function (j, s) {
                                if (code === s.checklistCode) {
                                    flag = false;
                                }
                            });
                            if (flag) {
                                $selectedChecklists.push({
                                    checklistId: $("option[checklist-code='" + code + "']").val(),
                                    checklistCode: code,
                                    checklistName: $("[checklist-code='" + code + "']")[0].innerText,
                                });
                            }
                        });
                        $("#" + AreaObj.Area).click();
                        $selectedChecklistsCount = $selectedChecklists.length;
                    }
                }

                // remove the validation text if present
                $('.area-modal-validation-msg').addClass('hidden');

                //selectedAreaCount = selectedAreaCount + 1;
                auditFormVariables.selectedAreaCount = auditFormVariables.selectedAreaCount + 1;
                generateAreaChecklistCard({ $selectedAreaCode, $selectedSubAreaCode, $selectedSubAreaName, $selectedChecklistsCount, $selectedAreaName }, $selectedChecklists, $selectedChecklistsCode);
                $('.selected-area-count').html(auditFormVariables.selectedAreaCount);

                $('.schedule-area, .schedule-subarea').val(null).trigger('change');
                $('.schedule-area').select2({
                    placeholder: 'Select Area',
                    allowClear: true
                });
                $('.schedule-subarea').select2({
                    placeholder: 'Select Area',
                    allowClear: true
                });

                // Empty the multi-select Select Box
                $('#my-select').empty().multiSelect('refresh');
            }



            $('#my-select option:selected').each(function (i, v) {
                $selectedChecklists.push(v.text);
            });
            let $hiddenAreaInput = $('<input>', {
                type: 'hidden',
                id: 'foo',
                name: 'schedule_area_1',
                value: $selectedAreaCode
            }).appendTo('.audit-hidden-fields');
            ////console.log($selectedChecklists);
            ////console.log($selectedAreaName);
            //generateAreaChecklistCard({ $selectedArea, $selectedSubArea, $selectedChecklistsCount, $selectedAreaName });
        });

    //Refactor 
    
    function generateAreaChecklistCard(ChecklistData, checkListDescriptions, checkListDescriptionCode) {

        // Refactor Test Code
        /*
         * Date:        Feb 14th, 2019
         * Author:      Anshu
         * Description: Fetch the Checks and add to the Checklist, and add the Checklist Object to the AuditSchedule Object
         */
        let addAuditChecksToAuditChecklist = async function (AuditChecklist) {
            let results = await $.ajax({
                url: '/SchedulingMgmt/SchedulingManagement/GetAllChecksByChecklistID',
                type: 'GET',
                contentType: 'json',
                data: { checklistId: AuditChecklist.ChecklistID, subAreaCode: AuditChecklist.SubAreaCode }
            });

            results.forEach(function (result) {
                AuditChecklist.AuditCheckDto.push({
                    CheckID: result.CheckID,
                    CheckCode: result.CheckCode
                });
            });
            AuditSchedule.AuditChecklistDto.push(AuditChecklist);
        };

        /*
        * Date:         Feb 14th, 2019
        * Author:       Anshu
        * Description:  Iterate through all the selected Checklists and add Checks to them.
        */
        checkListDescriptions.forEach(function (checklists, index) {
            let AuditChecklist = {
                ChecklistCode: checklists.checklistCode,
                ChecklistID: checklists.checklistId,
                AreaCode: ChecklistData.$selectedAreaCode,
                AreaName: ChecklistData.$selectedAreaName,
                SubAreaCode: ChecklistData.$selectedSubAreaCode,
                SubAreaName: ChecklistData.$selectedSubAreaName,
                AuditCheckDto: []
            };
            // Following line adds the checklist to the AuditChecklistDTO array in the schedule object
            // Comment the following line if checks has to be added to the checklist and then add to the schedule object.
            AuditSchedule.AuditChecklistDto.push(AuditChecklist);
            // Call this method to add Checks to the checklist and to add the checklist to the schedule object.
            //addAuditChecksToAuditChecklist(AuditChecklist);
        });





        let innerCardTemplate = '';
        checkListDescriptions.forEach(function (value, iterator) {
            innerCardTemplate = innerCardTemplate + '<div class="col l12 s12 m12 ">' + value.checklistName + '</div>' +
                '<input style="display:none" name="AuditChecklistDto[' + index + '].ChecklistCode" value="' + value + '" />' +
                '<input style = "display:none" name = "AuditChecklistDto[' + index + '].AreaName" value = "' + ChecklistData.$selectedAreaName + '" />' +
                '<input style = "display:none" name = "AuditChecklistDto[' + index + '].AreaCode" value = "' + ChecklistData.$selectedArea + '" />' +
                '<input style = "display:none" name = "AuditChecklistDto[' + index + '].SubAreaCode" value = "' + ChecklistData.$selectedSubArea + '" />' +
                '<input style = "display:none" name = "AuditChecklistDto[' + index + '].SubAreaName" value = "' + ChecklistData.$selectedSubAreaName + '" />';
            index++;
        });
        innerCardTemplate = `<div style="font-size:82%;"> ${innerCardTemplate} </div>`;
        let innerCardTemplateCode = '';

        checkListDescriptionCode.forEach(function (value, iterator) {
            innerCardTemplateCode = innerCardTemplateCode + '<input style="display:none" name="AuditChecklistDto[' + secondIndex + '].ChecklistID" value="' + value + '"/><br/>';
            beResults = null;
            getAllChecksByChecklistId(value, ChecklistData.$selectedSubArea);
            beResults.forEach(function (secondValue, secondIterartor) {
                innerCardTemplateCode = innerCardTemplateCode +
                    '<input style="display:none" name="AuditChecklistDto[' + secondIndex + '].AuditCheckDto[' + secondIterartor + '].CheckID" value="' + secondValue.CheckID + '"/>' +
                    '<input style="display:none" name="AuditChecklistDto[' + secondIndex + '].AuditCheckDto[' + secondIterartor + '].CheckCode" value="' + secondValue.CheckCode + '"/>';
            });
            secondIndex++;
        });
        let cardTemplate = `<div class="col s12 ">
                                <div class="card horizontal area-subarea-count" style="box-sizing: border-box;margin-top: 0.8rem;">
                                    <div class="clear-sub-area">
                                        <i id="${ChecklistData.$selectedSubAreaCode}" class="material-icons">clear</i>
                                    </div>
                                    <div class="card-stacked">
                                        <div class="card-content" style="padding:10px;">
                                            <div class=''>
                                                <div class="col l10" style="padding:0px; margin-bottom:10px;">${ChecklistData.$selectedAreaName} - ${ChecklistData.$selectedSubAreaName}</div>
                                                <div class="col l2 center-align selected-area-checklists" style="padding:0px">
                                                <b>${ChecklistData.$selectedChecklistsCount}</b>
                                                 </div>   
                                                <div class="area-checklist">
                                                    ${innerCardTemplate}
                                                    ${innerCardTemplateCode}
                                
                                                </div>
                                
                                                </div>
                                        </div> 
                                    </div> 
                                </div>
                            </div>`;
            //
            //$('.added-area-checklist').prepend(cardTemplate).effect('highlight', {}, 2000);
            $('.added-area-checklist').prepend(cardTemplate).find(':first').children('.card').addClass('highlight');
            setTimeout(
                function () { $('.added-area-checklist').find(':first').children('.card').removeClass('highlight', 2000, 'swing'); },
                100
            );
            //auditFormVariables.selectedAreaCount = auditFormVariables.selectedAreaCount + 1;
            $('#my-select').multiSelect('deselect_all');
        }

        $('#ScheduleAuditSideDiv').on('click', '.clear-sub-area', function () {
            var id = $(this).find('i').attr('id');
            $(this).closest('.card').remove();

            for (var i = AuditSchedule.AuditChecklistDto.length - 1; i >= 0; i--) {
                if (AuditSchedule.AuditChecklistDto[i].SubAreaCode === id) {
                    AuditSchedule.AuditChecklistDto.splice(i, 1);
                }
            }
            
            auditFormVariables.selectedAreaCount = auditFormVariables.selectedAreaCount - 1;
            $('.selected-area-count').html(auditFormVariables.selectedAreaCount);
        });

        // Set active button highlight color in Calendar toolbar
        $('#AuditCalendar').on('click', '.fc-month-button, .fc-today-button, .fc-listWeek-button, .fc-listDay-button', function () {
            $('.ui-button').removeClass('active-button');
            $(this).addClass('active-button');
        });

        // Current Day should be highlighted when returning to Month view from anyother view on the fullcalendar
        $('#AuditCalendar').on('click', '.fc-month-button', function () {
            $(".clicked-day-highlight").removeClass("clicked-day-highlight");
            $("td[data-date=" + dates.clickedDate.format('YYYY-MM-DD') + "]").addClass("clicked-day-highlight");
        });
});

 //-------------------------------------------------------------------------------------------------------
        // Performance Optimisation Code
        // Getting all the data at once and storing it in a single object.
//-------------------------------------------------------------------------------------------------------

//let citiesLocationUsers = [];
// --------------- STUB -------------------
let citiesLocationUsers = [];
let areasAndSubareas = [];

// Method to get all the cities, locations and users based on the uuid and account of the logged in Manager
const getAllUserLocationByManager = async () => {
    const citiesLocationsAndUsers = await $.ajax({
        url: '/SchedulingMgmt/SchedulingManagement/GetAllLocationUserbyManager',
        type: 'GET',
        data: { uuid: Header.UUID, LOBCode: Header.LOBCode}
    });
    citiesLocationUsers = citiesLocationsAndUsers;
};


const getAllAreasSubareasByLocation = async (uuid, locationCode) => {
    const areasSubareas = await $.ajax({
        url: '/SchedulingMgmt/SchedulingManagement/GetAllUserAreasandSubAreasbyLocation',
        type: 'GET',
        data: { uuid: uuid, locationCode: locationCode },
        
    });
    areasAndSubareas = areasSubareas;
};

const getCities = () => {
    let cities = [];
    if (citiesLocationUsers.length > 0) {
        cities = citiesLocationUsers[0].Cities.map(function (city) {
            return city.City;
        });
    }
    return cities;
};

const getLocations = (cityName) => {
    const LOBCode = citiesLocationUsers[0].AccountCode;
    const city = citiesLocationUsers[0].Cities.filter(function (city) {
        return city.City === cityName;
    });

    const locations = city[0].LocationUsers.map(function (location) {
        return { LocationCode: location.LocationCode, LocationName: location.LocationName, LOBCode: LOBCode };
    });
    return locations;
};

const getUsers = (cityName, locationCode) => {
    const accountAbbreviation = citiesLocationUsers[0].AccountAbbreviation;
    const city = citiesLocationUsers[0].Cities.filter(function (city) {
        return city.City=== cityName;
    });

    const location = city[0].LocationUsers.filter(function (location) {
        return location.LocationCode === locationCode;
    });

    const usersList = location[0].Users.map(function (user) {
        return { UUID: user.UUID, AccountAbbreviation: accountAbbreviation, FirstName: user.FirstName, LastName: user.LastName};
    });

    return usersList;
};

const getAreas = () => {
    const areas = areasAndSubareas.map(function (area) {
        return { AreaCode: area.AreaCode, AreaName: area.AreaName };
    });
    return areas;
};

const getSubAreas = (areaCode) => {
    const area = areasAndSubareas.filter(function (area) {
        return area.AreaCode === areaCode;
    });


    const subAreas = area[0].SubAreas.map(function (subarea) {
        return { SubAreaCode: subarea.SubAreaCode, SubAreaName: subarea.SubAreaName };
    });
  
    //const subAreas = area.SubAreas.map(function (subarea) {
    //    return { SubAreaCode: subarea.SubAreaCode, SubAreaName: subarea.SubAreaName };
    //});

    return subAreas;
};

getAllUserLocationByManager();

//-----------------------------------------------------------
