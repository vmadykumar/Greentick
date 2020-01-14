 // Object to hold the date globaly
var dates = {
    clickedDate: ''
};

// Object to hold the data for Publishing the checklist
const AuditSchedule = {
    AssignedToUUID: "",
    AssignedToUserName: "",
    AssignedToUserUUID: "",
    ChecklistCategory: "",
    ChecklistCity: "",
    ChecklistCode: "",
    ChecklistExpiryDateTime: "",
    ChecklistFBO: "",
    ChecklistImage: "",
    ChecklistLocation: "",
    ChecklistLocationCode: "",
    ChecklistName: "",
    ChecklistScheduledEndDateTime: "",
    ChecklistScheduledStartDateTime: "",
    ChecklistStatus: "",
    ChecklistType: "",
    CreatedBy: "",
    CreatedDateTime: "",
    ChecklistChecklistDto: []
};

$(function () {

    // Holds the classes of dropdown fields for Select2
    let formControls = {
        'selectChecklistDropdowns': {
            '.schedule-location': {
                'placeholder': 'Select a Location'
            },
            '.schedule-area': {
                'placeholder': 'Select a Area'
            },
            '.schedule-subarea': {
                'placeholder': 'Select a Sub-Area'
            },
            '.schedule-select-checklist': {
                'placeholder': 'Select a Checklist'
            },
            '.schedule-select-supervisor': {
                'placeholder': 'Select a Supervisor'
            },
            '.occurence-select': {
                'placeholder': 'Select Occurences'
            }
        }
    };

    let dateControls = {
        '.end-time': {
            enableTime: true,
            noCalendar: false,
            altInput: true,
            altFormat: "j M, Y h:i K",
            dateFormat: "Y-m-d h:i K"
        },
        '.expiry-time': {
            enableTime: true,
            noCalendar: false,
            altInput: true,
            altFormat: "j M, Y h:i K",
            dateFormat: "Y-m-d h:i K"
        },
        '.schedule-end-time': {
            enableTime: true,
            noCalendar: false,
            altInput: true,
            altFormat: "j M, Y h:i K",
            dateFormat: "Y-m-d h:i K"
        },
        '.start-time': {
            enableTime: true,
            noCalendar: false,
            // Min/Max date is set before passing this object to whereever it is used.
            minDate: '',
            maxDate: '',
            altInput: true,
            altFormat: "j M, Y h:i K",
            dateFormat: "Y-m-d h:i K"
        }
    };

    /*
    * Author:      Abhishek Anshu
    * Date:        Dec 12, 2018
    * Summary:    
    */
    let helpers = {
        // Method to make the dropdowns Select2
        makeSelect2: (dropdownFields) => {
            for (let dropdownClass in dropdownFields) {
                $(dropdownClass).select2({
                    placeholder: dropdownFields[dropdownClass].placeholder,
                    allowClear: true
                });
            }
        },

        // Format a Date in Calendar Compatible Date
        formatDate: (date) => {
            // convert the date to a moment object
            date = moment(date);
            // format the date to moment supported date
            date = date.format();
            return date;
        },

        // Format the Date Fields with FlatPickr
        formatDateFields: (date) => {
            $('.end-time, .expiry-time, .schedule-end-time').flatpickr({
                enableTime: true,
                noCalendar: false,
                altInput: true,
                altFormat: "j M, Y h:i K",
                dateFormat: "Y-m-d h:i K"
            });

            $('.start-time').flatpickr({
                enableTime: true,
                noCalendar: false,
                minDate: dates.clickedDate,
                maxDate: dates.clickedDate,
                altInput: true,
                altFormat: "j M, Y h:i K",
                dateFormat: "Y-m-d h:i K"
            });
        },

        // Generate the FormData
        getFormData: () => {
            event.preventDefault();
            let fd = new FormData();
            let data = $("#" + 'ScheduleChecklistForm').serializeArray();
            $.each(data, function (key, input) {
                fd.append(input.name, input.value);
            });
            return { fd: fd, data: data };
        },

        // Print the formdata contents
        printFormData: (fd) => {
            for (var pair of fd.entries()) {
                console.log(pair[0] + ', ' + pair[1]);
            }
        }
    };

    /*
     * Author:              Abhishek Anshu
     * Date:                Dec. 11 2018
     * Summary:             Generate the Calendar with different options
     * Param {schedules}:   Contains all the schedules to be rendered on the calendar
     */
    let GenerateCalendar = (schedules) => {
        $('#ChecklistCalendar').fullCalendar('destroy');
        $('#ChecklistCalendar').fullCalendar({
            header: {
                left: 'prev,next today',
                center: 'title',
                right: 'month,agendaWeek,agendaDay,listWeek,listDay'
            },
            editable: false,
            droppable: true, // this allows things to be dropped onto the calendar

            // Get the right div on Checklist drop
            //drop: function (date, jsEvent, view) {
            //    schedulerUtilities.getAddSchedulePartial(date);
            //},

            // Get the right div on Day Click
            dayClick: function (date, jsEvent, view) {
                //schedulerUtilities.getViewSchedulePartial(date);
                schedulerHandlers.addScheduleChecklistPartial(date);
            },
            eventClick: function (calEvent, jsEvent, view) {
                alert('Event: ' + calEvent.title);
                alert('Event Start: ' + calEvent.start);
                alert('Event Start Time:' + calEvent.startTime);
            },
            events: schedules,
            timeFormat: 'h(:mm)'
        });

    };

    // Various Methods related to Scheduler
    let schedulerHandlers = {

        // Get the Schedule Checklist partial form
        addScheduleChecklistPartial: (date) => {
            $.ajax({
                url: '/SchedulingMgmt/SchedulingManagement/GetScheduleAddChecklist',
                contentType: 'application/html; charset=utf-8',
                type: 'GET',
                dataType: 'html',
                success: function (result) {
                    $('#ScheduleChecklistSideDiv').html(result);
                    dates.clickedDate = date;
                    let scheduleDate = date.format('MMMM Do YYYY');
                    let scheduleDay = date.format('dddd');
                    $('.checklist-schedule-date').html(scheduleDate);
                    $('.checklist-schedule-day').html(scheduleDay);
                    let startDate = date.format('Y-M-D');
                    $('#start_date').val(startDate);
                    $('#created_date').val(moment().format('Y-M-D'));
                    dateControls[".start-time"].minDate = helpers2.formatDate(dates.clickedDate);
                    dateControls[".start-time"].maxDate = helpers2.formatDate(dates.clickedDate);
                    helpers2.formatDateFields(dateControls);
                    helpers.makeSelect2(formControls.selectChecklistDropdowns);
                    dropdownHandlers.enableDisableSelect();
                }
            });
        },

        // Get the Schedule Checklist partial view to view all events
        viewScheduledChecklistPartial: (date) => {
            $.ajax({
                url: '/SchedulingMgmt/SchedulingManagement/GetScheduleViewChecklist',
                contentType: 'application/html; charset=utf-8',
                type: 'GET',
                dataType: 'html',
                success: function (result) {
                    $('#ScheduleChecklistSideDiv').html(result);
                    dates.clickedDate = date;
                    let scheduleDate = date.format('MMMM Do YYYY');
                    let scheduleDay = date.format('dddd');
                    $('.checklist-schedule-date').html(scheduleDate);
                    $('.checklist-schedule-day').html(scheduleDay);
                }
            });
        },

        // div to show/hide on click of Occurences
        ToggleChecklistOccurences: (selector) => {
            let occurence = $(selector).val();
            if (occurence === 'Repeat')
                $('.checklist-repeat-time').css('display', '');
            else
                $('.checklist-repeat-time').css('display', 'none');

            // Some default code. Will be removed in the future or may brought in use also.
            if ($('').is(':checked')) {
                // if so, remove the element from the "Draggable Events" list
                $(this).remove();
            }
        },

        // Save the Checklist Schedule
        saveChecklistScheduleOnSubmit: () => {
            // getFormData returns the formdata and stringified formdata in an object
            fdObj = helpers.getFormData();
            fd = fdObj.fd;
            data = fdObj.data;

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

        fetchSchedulesAndRender: () => {
            $.ajax({
                url: '/SchedulingMgmt/SchedulingManagement/GetScheduledChecklists',
                type: 'GET',
                success: function (results) {
                    let schedules = [];
                    console.log(results);
                    results.forEach(function (result, index) {
                        let scheduleStartDateTime = helpers.formatDate(result.ScheduleStartDate);;
                        schedules.push({
                            title: result.ChecklistName,
                            start: scheduleStartDateTime,
                            startTime: moment().format('h:mm a'),
                            allDay: false
                        });
                    });
                    GenerateCalendar(schedules);
                    schedulerHandlers.viewScheduledChecklistPartial(moment());
                }
            });
        }
    };



    // Event Handler for Multi-Days Select on ScheduleChecklist Page
    $("#ScheduleChecklistSideDiv")
        .on('click', '.schedule-occurence-option', function () {
            schedulerHandlers.ToggleChecklistOccurences(this);
        });

    // Event Handler to switch to add schedule partial on click of View Button
    $("#ScheduleChecklistSideDiv").off('click', '.add-schedule-rightview').on('click', '.add-schedule-rightview', function () {
        schedulerHandlers.addScheduleChecklistPartial(dates.clickedDate);
    });

    // Event Handler to switch to View schedule partial on click of Add Button
    $("#ScheduleChecklistSideDiv").off('click', '.view-schedule-rightview').on('click', '.view-schedule-rightview', function () {
        schedulerHandlers.viewScheduledChecklistPartial(dates.clickedDate);
    });

    /**
    * Author:      Abhishek Anshu
    * Date:        Dec. 11, 2018
    * Summary:     Method to Submit the schedules
    */
    $('#ScheduleChecklistForm').on('submit', function (event) {
        schedulerHandlers.saveChecklistScheduleOnSubmit();
    });

   

    let dropdownHandlers = {
        enableDisableSelect: (selector, dependentSelector) => {
            let enableDisableClasses = ['.schedule-location', '.schedule-area', '.schedule-subarea', '.schedule-select-checklist'];
            for (let i = 0; i < enableDisableClasses.length; i++) {
                for (let j = i + 1; j < enableDisableClasses.length; j++) {
                    $(enableDisableClasses[i]).on('change', function () {
                        if ($(enableDisableClasses[i]).val() === '') {
                            $(enableDisableClasses[j]).attr('disabled', true);
                        }
                        else {
                            $(enableDisableClasses[j]).attr('disabled', false);
                            return false;
                        }
                    });
                }
            }
        }
    };
            //$(selector).on('change', function () {
            //    $(this).val() === '' ? $(dependentSelector).attr('disabled', true) : $(dependentSelector).attr('disabled', false);
            //});

    schedulerHandlers.fetchSchedulesAndRender();
  
});