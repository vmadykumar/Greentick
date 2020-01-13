// Old Implementation with drag and drop view

$(document).ready(function () {

    dates = {
        clickedDate: ''
    };

    /* initialize the external events
    -----------------------------------------------------------------*/

    $('#external-events .fc-event').each(function () {

        // store data so the calendar knows to render an event upon drop
        $(this).data('event', {
            title: $.trim($(this).text()), // use the element's text as the event title
            stick: true // maintain when user navigates (see docs on the renderEvent method)
        });

        // make the event draggable using jQuery UI
        $(this).draggable({
            zIndex: 999,
            revert: true,      // will cause the event to go back to its
            revertDuration: 0  //  original position after the drag
        });

    });


    /* initialize the calendar
    -----------------------------------------------------------------*/
    let formControls = {
        selectChecklist: {
            inputClasses: ['.schedule-location', '.schedule-area', '.schedule-subarea'],
            placeholderText: ['Location', 'Area', 'Sub-Area']
        }
    };

    let calendarHandlers = {
        onAddScheduleDivLoad: (date) => {
            // Set the top date to clicked/dragged date
            dates.clickedDate = date;
            let scheduleDate = date.format('MMMM Do YYYY');
            let scheduleDay = date.format('dddd');
            $('.schedule-date').html(scheduleDate + '<br />' + scheduleDay);
            //$('.schedule-date').html(scheduleDate);

            // Checklist selection - search cum dropdown
            $(".checklist-select").select2({
                placeholder: "Select a Checklist",
                allowClear: true,
                width: '300px'
            });

            // Supervisor selection - search cum dropdown
            $(".user-select").select2({
                placeholder: "Select a Supervisor",
                allowClear: true,
                width: '300px'
            });

            $(".occurence-select").select2({
                placeholder: "Select Occurences",
                allowClear: true,
                width: '300px'
            });

            // Time only input box
            $(".start-time, .end-time, .expiry-time, .schedule-end-time").flatpickr({
                enableTime: true,
                noCalendar: true,
                dateFormat: "h:i K"
               
            });
        },
        makeSelect2: (inputObj) => {
            inputObj.inputClasses.forEach(function (value, index) {
                $(value).select2({
                    placeholder: inputObj.placeholderText[index],
                    allowClear: true,
                    width: 'resolve'
                });
            });
        }

    };

    calendarHandlers.makeSelect2(formControls.selectChecklist);

    let schedulerUtilities = {
        getAddSchedulePartial: (date) => {
            dates.clickedDate = date;
            $.ajax({
                url: '/SchedulingMgmt/SchedulingManagement/GetAddScheduleChecklistView',
                contentType: 'application/html; charset=utf-8',
                type: 'GET',
                dataType: 'html',
                success: function (result) {
                    $('#AddViewScheduleChecklist').html(result);
                    calendarHandlers.onAddScheduleDivLoad(date);
                    $('.schedule-tab').css('background-color', 'white');
                    $('.add-tab').css({ 'background-color': 'cornflowerblue', 'color': 'white' });
                },
                error: function (xhr, status) {
                    alert(status);
                }
            });

        },
        getViewSchedulePartial: (date) => {
            dates.clickedDate = date;
            $.ajax({
                url: '/SchedulingMgmt/SchedulingManagement/GetViewScheduleChecklistView',
                contentType: 'application/html; charset=utf-8',
                type: 'GET',
                dataType: 'html',
                success: function (result) {
                    $('#AddViewScheduleChecklist').html(result);
                    let scheduleDate = date.format('MMMM Do YYYY');
                    let scheduleDay = date.format('dddd');
                    $('.schedule-date').html(scheduleDate + '<br />' + scheduleDay);
                    $('.schedule-tab').css('background-color', 'white');
                    $('.view-tab').css({ 'background-color': 'cornflowerblue', 'color': 'white' });

                }
            });
            //.error(function (xhr, status) {
            //    alert(status);
            //});
        }
    };

    // FullCalender plugin
    $('#calendar').fullCalendar({
        header: {
            left: 'prev,next today',
            center: 'title',
            right: 'month,agendaWeek,agendaDay,listWeek,listDay'
        },
        editable: true,
        droppable: true, // this allows things to be dropped onto the calendar

        // Get the right div on Checklist drop
        drop: function (date, jsEvent, view) {
            schedulerUtilities.getAddSchedulePartial(date);
        },

        // Get the right div on Day Click
        dayClick: function (date, jsEvent, view) {
            schedulerUtilities.getViewSchedulePartial(date);
        },
        eventClick: function (calEvent, jsEvent, view) {
            console.log(calEvent, jsEvent, view);
        }
    });

    $('#AddViewScheduleChecklist').on('click', '.add-schedule-rightview', function () {
        schedulerUtilities.getAddSchedulePartial(dates.clickedDate);
    });

    $('#AddViewScheduleChecklist').off().on('click', '.view-tab', function () {
        console.log('view');
        schedulerUtilities.getViewSchedulePartial(dates.clickedDate);
    });
    $('#AddViewScheduleChecklist').on('click', '.add-tab', function () {
        schedulerUtilities.getAddSchedulePartial(dates.clickedDate);
    });

    $("#AddViewScheduleChecklist").on('click', '.schedule-occurence-option', function () {
        console.log('it works');
        let occurence = $(this).val();
        if (occurence === 'Repeat')
            $('.repeat-times').css('display', '');
        else
            $('.repeat-times').css('display', 'none');

        if ($('').is(':checked')) {
            // if so, remove the element from the "Draggable Events" list
            $(this).remove();
        }
    });
});
