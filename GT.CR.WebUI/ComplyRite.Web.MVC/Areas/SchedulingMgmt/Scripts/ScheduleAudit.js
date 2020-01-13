var dates = {
    clickedDate: ''
};
var selectedAreaCount = 0;



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

    $('.start-time, .end-time, .expiry-time, .schedule-end-time').flatpickr({
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

        console.log(startTimeObj, endTimeObj);

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
        '.schedule-end-time': {
            enableTime: true,
            noCalendar: false,
            disableMobile: "true",
            minDate: '',
            altInput: true,
            altFormat: "j M, Y h:i K",
            dateFormat: "Y-m-d h:i K"
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
            onChange: function (selectedDates, dateStr, instance) {
                //UTCDate = moment(dateStr).utc().format();
                //$('input[name="AuditScheduledStartDateTime"]').val(UTCDate);
                startEndTimeChange(dateStr, instance);
            }
        }
    };

    let dateControlInitObj = [];

    let generateDateControls = (dateControls) => {
        $.each(dateControls, function (dateControl, index) {
            let userConfig = dateControls[dateControl];
            let flatpickrInstance = $(dateControl).flatpickr(userConfig);
            dateControlInitObj.push(flatpickrInstance);
        });
    };

    let getAreasByLocation = (uuid, locationCode) => {
        $.ajax({
            url: '/SchedulingMgmt/SchedulingManagement/GetAllUserAreasByLocation',
            type: 'GET',
            data: { uuid: uuid /*'UU0000011'*/, locationCode: locationCode }
        }).done(function (results) {
            let locations = [];
            //console.log(results);
            $('.schedule-area').empty()
            $('.schedule-area').prepend("<option value=" + '' + ">" + '' + "</option>");
            results.forEach(function (result, index) {
                //console.log(result);
                //locations.push({ text: this.Text, value: this.Value });
                $('.schedule-area').append("<option value=" + result.AreaCode + ">" + result.AreaName + "</option>");
            });
            $('.schedule-area').select2({
                placeholder: 'Select an Area',
                allowClear: true
            });
        });
    };

    let getSubAreasAreasByUserAndLocation = (uuid, locationCode, areacode) => {
        $.ajax({
            url: '/SchedulingMgmt/SchedulingManagement/GetAllUserSubAreasbyLocation',
            type: 'GET',
            data: { uuid: uuid /*'UU0000011'*/, locationCode: locationCode/*'MCD0000001'*/, areacode: areacode }
        }).done(function (results) {
            let locations = [];
            //console.log(results);
            $('.schedule-subarea').prepend("<option value=" + '' + ">" + '' + "</option>");
            results.forEach(function (result, index) {
                //console.log(result);
                //locations.push({ text: this.Text, value: this.Value });
                $('.schedule-subarea').append("<option value=" + result.SubAreaCode + ">" + result.SubAreaName + "</option>");
            });
            $('.schedule-subarea').select2({
                placeholder: 'Select a Sub Area',
                allowClear: true
            });
        });
    };

    // Method to get all the sub area for the area selected.
    let GetAllChecklistForAreaSubArea = (subareacode) => {
        $.ajax({
            url: '/SchedulingMgmt/SchedulingManagement/GetAllChecklistForAreaSubArea',
            type: 'GET',
            data: { subareacode: subareacode }
        }).done(function (results) {
            let locations = [];
            //console.log(results);
            $('#my-select').empty().multiSelect('refresh');
            //$('#my-select').prepend("<option value=" + '' + ">" + '' + "</option>");
            results.forEach(function (result, index) {
                //console.log(result);
                //locations.push({ text: this.Text, value: this.Value });
                $('#my-select').append("<option value=" + result.ChecklistId + ">" + result.ChecklistName + "</option>");
            });
            $('#my-select').multiSelect('refresh');
        });
    };

    // Method to get all the locations based on the uuid of the user
    let GetAllLocations = (UUID) => {
        $.ajax({
            url: '/SchedulingMgmt/SchedulingManagement/GetUserLocationsByUUID',
            type: 'GET',
            data: { uuid: UUID }
        }).done(function (results) {
            $('.schedule-location').empty();
            //console.log(results);
            $('.schedule-location').prepend("<option value=" + '' + ">" + '' + "</option>");
            results.forEach(function (result, index) {
                //console.log(result);
                //locations.push({ text: this.Text, value: this.Value });
                $('.schedule-location').append("<option value=" + result.LocationCode + ">" + result.LocationName + "</option>");
            });
            $('.schedule-location').select2({
                placeholder: 'Select a Location',
                allowClear: true
            });
        });
    };

    // Method to get all the cities based on the uuid of the user
    let GetAllCities = (UUID) => {
        $.ajax({
            url: '/SchedulingMgmt/SchedulingManagement/GetUserCitiesByUUID',
            type: 'GET',
            data: { uuid: UUID }
        }).done(function (results) {
            $('.schedule-city').empty();
            //console.log(results);
            $('.schedule-city').prepend("<option value=" + '' + ">" + '' + "</option>");
            results.forEach(function (result, index) {
                //console.log(result);
                //locations.push({ text: this.Text, value: this.Value });
                $('.schedule-city').append("<option value=" + result.CityName + ">" + result.CityName + "</option>");
            });
            $('.schedule-city').select2({
                placeholder: 'Select a City',
                allowClear: true
            });
        });
    };

    // Method to get all the auditors for the location
    let GetAllAuditors = (UUID, locationCode) => {
        $.ajax({
            url: '/SchedulingMgmt/SchedulingManagement/GetAllAuditorsByLocation',
            type: 'GET',
            data: { uuid: UUID, locationCode: locationCode }
        }).done(function (results) {
            let locations = [];
            //console.log(results);
            $('.schedule-select-auditor').empty();
            $('.schedule-select-auditor').prepend("<option value=" + '' + ">" + '' + "</option>");
            results.forEach(function (result, index) {
                //console.log(result);
                //locations.push({ text: this.Text, value: this.Value });
                $('.schedule-select-auditor').append("<option value=" + result.UUID + " data-acc-alias='" + result.AccountAbbreviation + "'>" + result.FirstName + ' ' + result.LastName + "</option>");
            });
            $('.schedule-select-auditor').select2({
                placeholder: 'Select an Auditor',
                allowClear: true
            });
        });
    };

    //let assignAuditorLOBCode = (uuid) => {
    //    $.ajax({
    //        url: '/SchedulingMgmt/SchedulingManagement/GetAuditorLobCode',
    //        type: 'GET',
    //        data: { uuid: uuid }
    //    }).done(function (auditLocationCode) {
    //        $('[name=AuditLocationCode]').val(auditLocationCode);

    //    });
    //};

    let RemoveAllAuditors = () => {
        $('.schedule-select-auditor').empty();
        $('.schedule-area').empty();
        $('.schedule-subarea').empty();
        $('#my-select').empty().multiSelect('refresh');
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
            console.log(results);
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
                success: function (result) {
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

                
                    let newDate = moment(dateControls[".start-time"].defaultDate, 'Y-MM-D h:mm A');

                    dateControls[".end-time"].defaultDate = newDate.add(8, 'h').format('Y-MM-D h:mm A');
                    dateControls[".expiry-time"].defaultDate = newDate.add(2, 'h').format('Y-MM-D h:mm A');
                    
                    dateControls[".end-time"].minDate = dates.clickedDate.format('Y-MM-D');
                    dateControls[".expiry-time"].minDate = dates.clickedDate.format('Y-MM-D');

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

                    GetAllLocations(Header.UUID);
                    GetAllCities(Header.UUID);
                    $('[name=CreatedBy]').val(Header.UserFullName);

                    // To reset the index of the area, subarea and checklist selected
                    index = 0;
                    secondIndex = 0;

                    // scroll the form to top on load
                    let sideDivObj = document.getElementById('ScheduleAuditSideDiv');
                    sideDivObj.scrollTop = 0;
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
                    
                }
            });
        },

        // div to show/hide on click of Occurences
        ToggleAuditOccurences: (selector) => {
            let occurence = $(selector).val();
            if (occurence === 'Repeat')
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

            console.log(fd);
            console.log(data);
            helpers.printFormData(fd);

            // Call the Save Method of Controller to save the schedule
            $.ajax({
                type: 'POST',
                url: '/SchedulingMgmt/SchedulingManagement/SaveScheduleChecklist',
                data: data,
                success: function (results) {
                    let schedules = [];
                    console.log(results);
                    results.forEach(function (result, index) {
                        let startDate = helpers.formatDate(result.ScheduleStartDate);
                        schedules.push({
                            title: '',
                            start: startDate,
                            startTime: moment().format('h:mm a'),
                            allDay: false
                        });
                    });
                    GenerateCalendar(schedules);
                }
            });
        },

        // Get all the schedules and render it on the calendar
        fetchSchedulesAndRender: () => {
            jQuery.ajax({
                url: '/SchedulingMgmt/SchedulingManagement/GetAllPublishedSchedules',
                type: 'GET',
                success: function (results) {
                    let schedules = [];
                    console.log(results);
                    results.forEach(function (result, index) {
                        let scheduleStartDateTime = moment(result.auditStartDate + " " + result.auditStartTime).format();
                        let scheduleEndDateTime = moment(result.auditEndDate + " " + result.auditEndTime).format();
                        schedules.push({
                            title: result.auditName,
                            start: scheduleStartDateTime,
                            end: scheduleEndDateTime,
                            startTime: moment().format('h:mm a'),
                            assignedTo: result.assignedToUserName,
                            allDay: false
                        });
                    });
                    GenerateCalendar(schedules);
                    schedulerHandlers.viewScheduledAuditPartial(moment(), false);
                }
            });
        }
    };

    // Generate the Calendar
    let GenerateCalendar = (schedules) => {
        $('#AuditCalendar').fullCalendar('destroy');
        $('#AuditCalendar').fullCalendar({
            header: {
                left: 'month,today', // agendaWeek, agendaDay,
                center: 'title',
                right: 'listWeek,listDay, prev,next'
            },
            // Sets the name of the fields we have on top of the calendar
            buttonText: {
                today: 'Today',
                month: 'Month',
                week: 'Week',
                day: 'Day',
                listWeek: 'Week\'s List',
                listDay: 'Day\'s List'
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
                if (view.type === 'listWeek' || view.type === 'listDay') {
                    var toInject = [];
                    toInject.push(event.assignedTo);
                    //toInject.push(event.answer_date);
                    for (var i = 0; i < toInject.length; i++) {
                        element.append('<td class="ui-widget-content fc-list-item-name">' + toInject[i] + '</td>');
                        //console.log(event.assignedTo);
                    }
                }
            },
            eventAfterAllRender: function (view) {
                if (view.type === 'listWeek' || view.type === 'listDay') {
                    console.log(view.type + ' change colspan');
                    console.log(view)
                    var tableSubHeaders = jQuery("td.ui-widget-header");
                    console.log(tableSubHeaders);
                    var numberOfColumnsItem = jQuery('tr.fc-list-item');
                    var maxCol = 0;
                    var arrayLength = numberOfColumnsItem.length;
                    for (var i = 0; i < arrayLength; i++) {
                        maxCol = Math.max(maxCol, numberOfColumnsItem[i].children.length);
                    }
                    console.log("number of items : " + maxCol);
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
                    schedulerHandlers.addScheduleAuditPartial(date);
                }
            },
            eventClick: function (calEvent, jsEvent, view) {
                //alert('Event: ' + calEvent.assignedTo);
                //alert('Event Start: ' + calEvent.start);
                //alert('Event Start Time:' + calEvent.startTime);
            },
            // Event renders all the scheduled events on the calendar
            events: schedules,
            //eventLimit: true,
            //eventLimitText: 'Audits',
            //views: {
            //    month: {
            //        eventLimit: 6 // adjust to 6 only for agendaWeek/agendaDay
            //    }
            //},
            timeFormat: 'h(:mm) A',
            themeSystem: 'jquery-ui'

        });
    };





    // Event Handler for Multi-Days Select on Schedule Audit Page
    $("#ScheduleAuditSideDiv")
        .off('click', '.schedule-occurence-option')
        .on('click', '.schedule-occurence-option', function () {
            schedulerHandlers.ToggleAuditOccurences(this);
        });

    // Event Handler to switch to add schedule partial on click of View Button
    $("#ScheduleAuditSideDiv").off('click', '.add-schedule-rightview')
        .on('click', '.add-schedule-rightview', function () {
            schedulerHandlers.addScheduleAuditPartial(dates.clickedDate);
        });

    // Event Handler to switch to View schedule partial on click of Add Button
    $("#ScheduleAuditSideDiv").off('click', '.view-schedule-rightview')
        .on('click', '.view-schedule-rightview', function () {
            schedulerHandlers.viewScheduledAuditPartial(dates.clickedDate);
        });

    // Event Handler when uesr clicks on the Publlish Schedule Button // 
    //$('#ScheduleAuditForm').on('submit', function (event) {
    //    schedulerHandlers.saveAuditScheduleOnSubmit();
    //});

    // Fill areas on select of location
    $("#ScheduleAuditSideDiv").on('change', '.schedule-location', function () {
        if ($(this).val() !== "") {
            $('[name=AuditLocation]').val($(this).children('option:selected').text());
            //getAreasByLocation(Header.UUID, $(this).val());
            disableAuditorDependentFields();
            GetAllAuditors(Header.UUID, $(this).val());
            $('.schedule-area').empty();
            $('.schedule-subarea').empty();
            $('#my-select').empty().multiSelect('refresh');

            // Hardcoding for AHC Demo
            if ($(this).children('option:selected').text() === 'Bangalore' || $(this).children('option:selected').text() === 'NBC' || $(this).children('option:selected').text() === 'Rubrik') {
                $('[name=AuditCity]').val('Bengaluru');
            }
            if ($(this).children('option:selected').text() === 'Lake City mall') {
                $('[name=AuditCity]').val('Mumbai');
            }
            if ($(this).children('option:selected').text() === 'Big India Mall') {
                $('[name=AuditCity]').val('Mumbai');
            }
            if ($(this).children('option:selected').text() === 'Main') {
                $('[name=AuditCity]').val('Spring Hill');
            }
            if ($(this).children('option:selected').text() === 'Epsilon/HUB 1' || $(this).children('option:selected').text() === 'Epsilon/HUB 2') {
                $('[name=AuditCity]').val('Bengaluru');
            }
        }
        else
            RemoveAllAuditors();
    });

    // Fill sub-area on select of area
    $("#ScheduleAuditSideDiv").on('change', '.schedule-area', function () {


        if ($('.schedule-area').val() !== null && $('.schedule-area').val().length > 0) {
            $('.area-validation-msg').addClass('hidden');
        }

        if ($(this).val() !== "") {
            $('.schedule-subarea').html('');
            $scheduleLocation = $('.schedule-location').val();
            getSubAreasAreasByUserAndLocation($('.schedule-select-auditor').val(), $scheduleLocation, $(this).val());
        }

        // Clear  the multi-select box when area is cleared in the dropdown
        $('#my-select').empty().multiSelect('refresh');
        $('.schedule-subarea').empty();

        $('.audit-checklists-validation-msg').html('');
    });

    $("#ScheduleAuditSideDiv").on('change', '#my-select', function () {
        if ($('#my-select').val() !== '' || $('#my-select').val() !== null) {
            $('.audit-checklists-validation-msg').empty();
        }
    });
    

    // Event Handler for the tooltip
    //$("#ScheduleAuditSideDiv").on('mouseover', '.tooltipped', function () {
        
    //});

    //// Fill sub-area on select of area
    //$("#ScheduleAuditSideDiv").on('change', '.schedule-location', function () {
    //    if ($(this).val() !== "") {
    //        $('.schedule-area').html('');
    //        getAreasByLocation($(this).val());
    //    }
    //});

    let auditorUUID = '';
    // Fill UUID of User
    $("#ScheduleAuditSideDiv").on('change', '.schedule-select-auditor', function () {
        if ($(this).val() !== "") {
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
            $('.schedule-area').empty();
            $('.schedule-subarea').empty();
            $('#my-select').empty().multiSelect('refresh');
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

    $('#my-select').on('change', function () {
        if ($('#my-select').val().length !== 0) {
            $('.audit-checklists-validation-msg').html('');
        }
    });

    $('#ScheduleAuditSideDiv').on('click', '.area-subarea-count' , function () {
        $(this).find('.area-checklist').slideToggle();
    });
    

    // Event Handler to restrict selection of date less than start date in the end date field
    //$("#ScheduleAuditSideDiv").on('change', '.start-time', function () {
    //    let startTime = moment($('.start-time').val()).format('L');
    //    $('.end-time').flatpickr().clear();

        
    //});

    // Fetch all the events, load the calender and render it on the calendar when the page loads.
    schedulerHandlers.fetchSchedulesAndRender();

    // $(selector).on('change', function () {
    //            $(this).val() === '' ? $(dependentSelector).attr('disabled', true) : $(dependentSelector).attr('disabled', false);
    //});


    /*Author:       Abhishek Anshu
     * Date:        Dec 24th, 2018
     * De scription: Method to disable/enable dropdown fields on change in values of dropdown
     * */
    let enableDisableFields = () => {
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

    // Remove validation messages on selection or inputs
    $('#ScheduleAuditForm').on("keyup change blur click", '[data-required]', function () {
        if ($('input[name="AuditName"]') !== null && $('input[name="AuditName"]').val().length > 0) {
            $('.audit-name-validation-msg').addClass('hidden');
        }

        //if ($('.schedule-city') !== null && $('.schedule-city').val().length > 0) {
        //    $('.city-validation-msg').addClass('hidden');
        //}

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

    });

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

        if ($('.schedule-location').val() === '') {
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
        console.log(startTimeObj, endTimeObj);
        // Check if Start Time is greater/lesser than End Time
        isStartTimeValid = startTimeObj.isAfter(endTimeObj) || startTimeObj.isSame(endTimeObj);
        isStartTimeValid === true ? isValid = false : '';
        let expiryTimeObj = moment($('.expiry-time').val(), 'Y-MM-D h:mm A');
        isEndTimeValid = endTimeObj.isAfter(expiryTimeObj);
        isEndTimeValid === true ? isValid = false : '';

        // Check if end time is valid at the time of publishing audit i.e. it should be greater than current time.
        let isEndTimeAtSubmitValid = moment($('.end-time').val(), 'Y-MM-D h:mm A').isAfter(moment()) && moment($('.expiry-time').val(), 'Y-MM-D h:mm A').isAfter(moment());
        isEndTimeAtSubmitValid === true ? '' : isValid = false;

        if (isEndTimeAtSubmitValid === false) {
            if (!moment($('.end-time').val(), 'Y-MM-D h:mm A').isAfter(moment())) {
                $('.audit-time-validation-msg').html('End Time should be greater than Current Time');
            }
            else if (!moment($('.expiry-time').val(), 'Y-MM-D h:mm A').isAfter(moment())) {
                $('.audit-expiry-time-validation-msg').html('Expiry Time should be greater than Current Time');
            }
        }

        return isValid;
    };

    /*
     * Author:      Praveen KM
     * Date:        Dec. 28, 2018   
     * Description: Event Handler to post the data to the controller to save and publish the Audit Schedule.
     */
    $("#ScheduleAuditForm").on('submit', function (event) {
        event.preventDefault();
        //if ($('.audit-expiry-time-validation-msg')[0].innerText !== "" || $('input [name="ChecklistDto[0].AreaName"]').length === 0) {
        //    M.toast({ html: 'Fill the Required Fields !' });
        //} 

        let isFormValid = true;
        isFormValid = ValidateScheduleAuditForm();

       
        let scheduleAuditFD = new FormData($('#ScheduleAuditForm')[0]);
        for (var pair of scheduleAuditFD.entries()) {
            if (pair[0] === "AuditScheduledStartDateTime") { pair[1] = moment(pair[1]).utcOffset(330).utc().format(); }
            console.log(pair[0] + ', ' + pair[1]);
        }

        if (isFormValid === true) {
            $('.audit-form-submit-btn').addClass('disabled');
            $.ajax({
                type: 'POST',
                url: "/SchedulingMgmt/SchedulingManagement/PostAuditData",
                data: new FormData($('#ScheduleAuditForm')[0]),
                processData: false,
                contentType: false,
                cache: false,
                beforeSend: function () {
                    $('.ajax-loader').css("visibility", "visible");
                },
                complete: function () {
                    $('.ajax-loader').css("visibility", "hidden");
                },
                success: function (result) {
                    //M.toast({ html: 'Audit Saved Successfully !' });
                    var obj = result;
                    if (obj !== null) {
                        obj.forEach(function (auditCode, index) {
                            $.ajax({
                                url: '/SchedulingMgmt/SchedulingManagement/PublishAuditSchedule',
                                type: 'POST',
                                async: false,
                                data: { auditId: obj },
                                success: function () {
                                    index = 0;
                                    secondIndex = 0;
                                    resetAddAuditScheduleForm();
                                    schedulerHandlers.viewScheduledAuditPartial(dates.clickedDate);
                                    //$('.view-schedule-rightview').trigger('click');
                                    M.toast({ html: 'Audit Published Successfully !', displayLength: '4000' });
                                },
                                error: function () {
                                    // resetAddAuditScheduleForm();
                                    //$('.view-schedule-rightview').trigger('click');
                                    M.toast({ html: 'Audit Publish Failed !', displayLength: '4000' });
                                    $('.audit-form-submit-btn').removeClass('disabled');
                                }
                            });
                        });
                        
                    } // end of if


                },
                error: function (error) {
                    M.toast({ html: 'Audit Save Failed !', displayLength: '4000' });
                    $('.audit-form-submit-btn').removeClass('disabled');
                }
            });// AJAX ends here  
        } else {
            M.toast({ html: 'Fill all the Fields correctly !' });
        }

    });

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
    const generateOccurenceOptions = () => {
        const dayName = dates.clickedDate.format('dddd');
        const date = dates.clickedDate.format('D');
        const monthName = dates.clickedDate.format('MMMM');

        const occurenceOptions = `
            <option value="weeklyOn">Weekly on ${dayName}</option>
            <option value="monthlyOn">Monthly on ${date}</option>
            <option value="quarterlyOn">Quarterly on ${date}</option>
            <option value="annualyOn">Annualy on ${monthName} ${date}</option>
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
            schedulerHandlers.viewScheduledAuditPartial(dates.clickedDate);
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
            let $selectedArea = $('.schedule-area').select2().val();
            let $selectedAreaName = $('.schedule-area option:selected').text();
            
            let $selectedSubArea = $('.schedule-subarea').select2().val();
            let $selectedSubAreaName = $('.schedule-subarea option:selected').text();
            let $selectedChecklistsCount = 0/*$('#my-select').val().length*/;
            let $selectedChecklists = [];
            let $selectedChecklistsCode = [];
            for (let i = 0; i < $('#my-select option:selected').length; i++) {
                $selectedChecklists.push($('#my-select option:selected')[i].innerText);
                $selectedChecklistsCode.push($('#my-select option:selected')[i].value);
            }
            if ($selectedArea === '' || $selectedArea === null) {
                //$('.audit-area-validation-msg').html('Please enter the area');
                $('.area-validation-msg').removeClass('hidden').html('Select an area');
            } else if ($selectedSubArea === '' || $selectedSubArea === null) {
                //$('.audit-subarea-validation-msg').html('Please select the checklists');
                $('.subarea-validation-msg').removeClass('hidden').html('Select a subarea');
            }else if ($('#my-select').val().length === 0) {
                $('.audit-checklists-validation-msg').html('Please select the checklists');
            }
            else {
                // remove the validation text if present
                $('.area-modal-validation-msg').addClass('hidden');

                //selectedAreaCount = selectedAreaCount + 1;
                $selectedChecklistsCount = $('#my-select').val().length;
                auditFormVariables.selectedAreaCount = auditFormVariables.selectedAreaCount + 1;
                generateAreaChecklistCard({ $selectedArea, $selectedSubArea, $selectedSubAreaName, $selectedChecklistsCount, $selectedAreaName }, $selectedChecklists, $selectedChecklistsCode);
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
                value: $selectedArea
            }).appendTo('.audit-hidden-fields');
            console.log($selectedChecklists);
            console.log($selectedAreaName);
            //generateAreaChecklistCard({ $selectedArea, $selectedSubArea, $selectedChecklistsCount, $selectedAreaName });
        });

    function generateAreaChecklistCard(ChecklistData, checkListDescriptions, checkListDescriptionCode) {
        let innerCardTemplate = '';
        checkListDescriptions.forEach(function (value, iterator) {
            innerCardTemplate = innerCardTemplate + '<div class="col l12 s12 m12 ">' + value + '</div>' +
                '<input style="display:none" name="AuditChecklistDto[' + index + '].ChecklistCode" value="' + value + '" />'  +
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
                                    <div class="card horizontal area-subarea-count" style="box-sizing: border-box;">
                                        <div class="card-stacked">
                                            <div class="card-content" style="padding:10px;">
                                                <div class=''>
                                                    <div class="col l10" style="padding:0px; margin-bottom:10px;">${ChecklistData.$selectedAreaName} - ${ChecklistData.$selectedSubAreaName}</div>
                                                    <div class="col l2 center-align selected-area-checklists" style="padding:0px">${ChecklistData.$selectedChecklistsCount}</div>   
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

