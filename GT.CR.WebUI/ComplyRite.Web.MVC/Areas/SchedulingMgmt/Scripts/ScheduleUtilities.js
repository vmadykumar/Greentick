// Common Code for Scheduling the Checklists and the Audits

const isDateValidForRepeat = (startDate, repeatendDate) => !startDate.isAfter(repeatendDate);

// Contains Methods to find out all the dates for a particular option
// All the methods need to be checked if there are correctly returning the dates
const getDatesForRepeat = {

    daily: (repeatData) => {
        let { startDateTime, endDateTime, expiryDateTime, repeatEndDate } = repeatData;
        const scheduleDates = [];
        const scheduleDuration = endDateTime.diff(startDateTime);
        const expiryDuration = expiryDateTime.diff(endDateTime);
        const scheduleExpiryDateTime = expiryDateTime.format();
        //scheduleDates.push({ scheduleStartDateTime: startDateTime, scheduleEndDateTime: endDateTime, scheduleExpiryDateTime: expiryDateTime });
        while (isDateValidForRepeat(startDateTime, repeatEndDate)) {
            const scheduleStartDateTime = startDateTime.utc().format();
            const scheduleEndDateTime = endDateTime.utc().format();
            const scheduleExpiryDateTime = expiryDateTime.clone().utc().format();
            scheduleDates.push({ scheduleStartDateTime: scheduleStartDateTime, scheduleEndDateTime: scheduleEndDateTime, scheduleExpiryDateTime: scheduleExpiryDateTime });
            startDateTime = startDateTime.add(1, 'day');
            endDateTime = startDateTime.clone().add(scheduleDuration);
            expiryDateTime = endDateTime.clone().add(expiryDuration);
        }
        return scheduleDates;
    },

    weekdays: (repeatData) => {
        let { startDateTime, endDateTime, expiryDateTime, repeatEndDate } = repeatData;
        const scheduleDates = [];
        const scheduleDuration = endDateTime.diff(startDateTime);
        const expiryDuration = expiryDateTime.diff(endDateTime);
        
        while (isDateValidForRepeat(startDateTime, repeatEndDate)) {
            if (!(startDateTime.day() === 6 || startDateTime.day() === 0)) {
                const scheduleStartDateTime = startDateTime.clone().utc().format();
                const scheduleEndDateTime = endDateTime.clone().utc().format();
                const scheduleExpiryDateTime = expiryDateTime.clone().utc().format();

                scheduleDates.push({ scheduleStartDateTime: scheduleStartDateTime, scheduleEndDateTime: scheduleEndDateTime, scheduleExpiryDateTime: scheduleExpiryDateTime });  
            }
            startDateTime = startDateTime.add(1, 'd');
            endDateTime = startDateTime.clone().add(scheduleDuration);
            expiryDateTime = endDateTime.clone().add(expiryDuration);
        }
        return scheduleDates;
    },

  
    weekly: (repeatData) => {
        let { startDateTime, endDateTime, expiryDateTime, repeatEndDate } = repeatData;
        const scheduleDates = [];
        const scheduleDuration = endDateTime.diff(startDateTime);
        const expiryDuration = expiryDateTime.diff(endDateTime);
        
        while (isDateValidForRepeat(startDateTime, repeatEndDate)) {
            const scheduleStartDateTime = startDateTime.clone().utc().format();
            const scheduleEndDateTime = endDateTime.clone().utc().format();
            const scheduleExpiryDateTime = expiryDateTime.clone().utc().format();
            scheduleDates.push({ scheduleStartDateTime: scheduleStartDateTime, scheduleEndDateTime: scheduleEndDateTime, scheduleExpiryDateTime: scheduleExpiryDateTime });  

            startDateTime = startDateTime.add(1, 'week');
            endDateTime = startDateTime.clone().add(scheduleDuration);
            expiryDateTime = endDateTime.clone().add(expiryDuration);
        }
        return scheduleDates;
    },

    // Method needs to be checked
    monthly: (repeatData) => {
        let { startDateTime, endDateTime, expiryDateTime, repeatEndDate } = repeatData;
        const dayNumber = startDateTime.format('D');
        const scheduleDates = [];
        const scheduleDuration = endDateTime.diff(startDateTime);
        const expiryDuration = expiryDateTime.diff(endDateTime);

        while (isDateValidForRepeat(startDateTime, repeatEndDate)) {
            const scheduleStartDateTime = startDateTime.clone().utc().format();
            const scheduleEndDateTime = endDateTime.clone().utc().format();
            const scheduleExpiryDateTime = expiryDateTime.clone().utc().format();
            scheduleDates.push({ scheduleStartDateTime: scheduleStartDateTime, scheduleEndDateTime: scheduleEndDateTime, scheduleExpiryDateTime: scheduleExpiryDateTime });

            startDateTime.clone().add(1, 'month').isSame(startDateTime.clone().add(1, 'month').date(dayNumber)) ?
                startDateTime.add(1, 'month') :
                startDateTime.add(2, 'month');

            endDateTime = startDateTime.clone().add(scheduleDuration);
            expiryDateTime = endDateTime.clone().add(expiryDuration);
        }
        return scheduleDates;
    },

    quarterly: (repeatData) => {
        let { startDateTime, endDateTime, expiryDateTime, repeatEndDate } = repeatData;
        const dayNumber = startDateTime.format('D');
        const scheduleDates = [];
        const scheduleDuration = endDateTime.diff(startDateTime);
        const expiryDuration = expiryDateTime.diff(endDateTime);

        while (isDateValidForRepeat(startDateTime, repeatEndDate)) {
            const scheduleStartDateTime = startDateTime.clone().utc().format();
            const scheduleEndDateTime = endDateTime.clone().utc().format();
            const scheduleExpiryDateTime = expiryDateTime.clone().utc().format();
            scheduleDates.push({ scheduleStartDateTime: scheduleStartDateTime, scheduleEndDateTime: scheduleEndDateTime, scheduleExpiryDateTime: scheduleExpiryDateTime });

            startDateTime.clone().add(1, 'quarter').isSame(startDateTime.clone().add(1, 'quarter').date(dayNumber)) ?
                startDateTime.add(1, 'quarter') :
                startDateTime.add(2, 'quarter');

            endDateTime = startDateTime.clone().add(scheduleDuration);
            expiryDateTime = endDateTime.clone().add(expiryDuration);
        }
        return scheduleDates;
    },

    annualy: (repeatData) => {
        let { startDateTime, endDateTime, expiryDateTime, repeatEndDate } = repeatData;
        const dayNumber = startDateTime.format('D');
        const scheduleDates = [];
        const scheduleDuration = endDateTime.diff(startDateTime);
        const expiryDuration = expiryDateTime.diff(endDateTime);

        while (isDateValidForRepeat(startDateTime, repeatEndDate)) {
            const scheduleStartDateTime = startDateTime.clone().utc().format();
            const scheduleEndDateTime = endDateTime.clone().utc().format();
            const scheduleExpiryDateTime = expiryDateTime.clone().utc().format();
            scheduleDates.push({ scheduleStartDateTime: scheduleStartDateTime, scheduleEndDateTime: scheduleEndDateTime, scheduleExpiryDateTime: scheduleExpiryDateTime });

            startDateTime.clone().add(1, 'year').isSame(startDateTime.clone().add(1, 'year').date(dayNumber)) ?
                startDateTime.add(1, 'year') :
                startDateTime.add(2, 'year');

            endDateTime = startDateTime.clone().add(scheduleDuration);
            expiryDateTime = endDateTime.clone().add(expiryDuration);
        }
        return scheduleDates;
    },

    firstDay: (repeatData) => {
        let { startDateTime, endDateTime, expiryDateTime, repeatEndDate } = repeatData;
        const scheduleDates = [];
        const scheduleDuration = endDateTime.diff(startDateTime);
        const expiryDuration = expiryDateTime.diff(endDateTime);

        while (isDateValidForRepeat(startDate, repeatEndDate)) {
            startDateTime.date(1);
            endDateTime = startDate.clone().add(scheduleDuration);
            expiryDateTime = endDateTime.clone().add(expiryDuration);

            const scheduleStartDateTime = startDateTime.clone().utc().format();
            const scheduleEndDateTime = endDateTime.clone().utc().format();
            const scheduleExpiryDateTime = expiryDateTime.clone().utc().format();

            scheduleDates.push({ scheduleStartDateTime: scheduleStartDateTime, scheduleEndDateTime: scheduleEndDateTime, scheduleExpiryDateTime: scheduleExpiryDateTime });
            startDateTime.add(1, 'month');
        }
        return scheduleDates;
    },

    lastDay: (repeatData) => {
        let { startDateTime, endDateTime, expiryDateTime, repeatEndDate } = repeatData;
        const scheduleDates = [];
        const scheduleDuration = endDateTime.diff(startDateTime);
        const expiryDuration = expiryDateTime.diff(endDateTime);

        while (isDateValidForRepeat(startDateTime, repeatEndDate)) {
            startDateTime.date(1).add(1, 'month').subtract(1, 'day');
            endDateTime = startDateTime.clone().add(scheduleDuration);
            expiryDateTime = endDateTime.clone().add(expiryDuration);

            const scheduleStartDateTime = startDateTime.clone().utc().format();
            const scheduleEndDateTime = endDateTime.clone().utc().format();
            const scheduleExpiryDateTime = expiryDateTime.clone().utc().format();
            
            scheduleDates.push({ scheduleStartDateTime: scheduleStartDateTime, scheduleEndDateTime: scheduleEndDateTime, scheduleExpiryDateTime: scheduleExpiryDateTime });
            startDateTime.add(1, 'day');                
        }
        return scheduleDates;
    }
};



const getDatesToScheduleOn = function (repeatData) {
    let datesToScheduleOn = [];
 
    switch (repeatData.repeatType) {
        case 'daily': return getDatesForRepeat.daily(repeatData);

        case 'weekdays': return getDatesForRepeat.weekdays(repeatData);

        case 'weekly': return getDatesForRepeat.weekly(repeatData);

        case 'quarterly': return getDatesForRepeat.quarterly(repeatData);

        case 'monthly': return getDatesForRepeat.monthly(repeatData);

        case 'annualy': return getDatesForRepeat.annualy(repeatData);

        case 'firstDay': return getDatesForRepeat.firstDay(repeatData);

        case 'lastDay': return getDatesForRepeat.lastDay(repeatData);
             
        default: return;
             
    }
};

const createRepeatingSchedules = function (schedule, repeatData) {
    const datesToScheduleOn = getDatesToScheduleOn(repeatData);
    const schedules = [];
    //return datesToScheduleOn;

    // Create repeating audits
    datesToScheduleOn.forEach(function (date, index) {
        const newSchedule = new GetNewSchedule(schedule);        
        newSchedule.AuditScheduledStartDateTime = date.scheduleStartDateTime;
        newSchedule.AuditScheduledEndDateTime = date.scheduleEndDateTime;
        newSchedule.AuditExpiryDateTime = date.scheduleExpiryDateTime;
        newSchedule.CreatedBy = Header.UserFullName;
        schedules.push(newSchedule);
    });
    return schedules;
};

function GetNewSchedule(schedule) {
    this.AssignedToUUID = schedule.AssignedToUUID;
    this.AssignedToUserName = schedule.AssignedToUserName;
    this.AssignedToUserUUID = schedule.AssignedToUserUUID;
    this.AuditCategory = schedule.AuditCategory;
    this.AuditCity = schedule.AuditCity;
    this.AuditCode = schedule.AuditCode;
    this.AuditExpiryDateTime = schedule.AuditExpiryDateTime;
    this.AuditFBO = schedule.AuditFBO;
    this.AuditImage = schedule.AuditImage;
    this.AuditLocation = schedule.AuditLocation;
    this.AuditLocationCode = schedule.AuditLocationCode;
    this.AuditName = schedule.AuditName;
    this.AuditScheduledEndDateTime = schedule.AuditScheduledEndDateTime;
    this.AuditScheduledStartDateTime = schedule.AuditScheduledStartDateTime;
    this.AuditStatus = schedule.AuditStatus;
    this.AuditType = schedule.AuditType;
    this.CreatedBy = schedule.CreatedBy;
    this.CreatedDateTime = moment().utc().format();
    this.AuditChecklistDto = schedule.AuditChecklistDto;
    return this;
}





//(function ($) {

//    $.fn.formatDateToMoment = function () {
//        // convert the date to a moment object
//        console.log(this);
        
//        return this.each(function () {
//            console.log(this);
//            date = moment(this);
//            // format the date to moment supported date
//            date = date.format();
//            return date;
//        });
//    };
//}(jQuery));



let helpers = {
    // // Method to make the dropdowns Select2
    makeSelect2: (dropdownFields) => {
        for (let dropdownClass in dropdownFields) {
            $(dropdownClass).select2({
                placeholder: dropdownFields[dropdownClass].placeholder,
                allowClear: true,
                dropdownParent: dropdownFields[dropdownClass].dropdownParent
            });
        }
    },

    generateMulitSelectChecklist: () => {
        let selectedChecklistsCount = 0;

        $('#my-select').multiSelect({
            selectableHeader: "<div class='custom-header'>Select Checklists</div>",
            selectionHeader: "<div class='custom-header'>Selected Checklists</div>",
            afterSelect: () => {
                selectedChecklistsCount = selectedChecklistsCount + 1;
                $('.audit-checklists-count').html(selectedChecklistsCount);
            },
            afterDeselect: () => {
                selectedChecklistsCount = selectedChecklistsCount - 1;
                $('.audit-checklists-count').html(selectedChecklistsCount);
            }
        });
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
    formatDateFields: (dateFields) => {

        let defaultConfig = {
            enableTime: true,
            noCalendar: false,
            altInput: true,
            altFormat: "j M, Y h:i K",
            dateFormat: "Y-m-d h:i K"
        };

        for (let dateField in dateFields) {
            let userConfig = dateFields[dateField];
            let flatpickrOptions = $.extend({}, defaultConfig, userConfig);
            $(dateField).flatpickr(flatpickrOptions);
        }

        //$('.end-time, .expiry-time, .schedule-end-time').flatpickr({
        //    enableTime: true,
        //    noCalendar: false,
        //    altInput: true,
        //    altFormat: "j M, Y h:i K",
        //    dateFormat: "Y-m-d h:i K"
        //});

        //$('.start-time').flatpickr({
        //    enableTime: true,
        //    noCalendar: false,
        //    minDate: date,
        //    maxDate: date,
        //    altInput: true,
        //    altFormat: "j M, Y h:i K",
        //    dateFormat: "Y-m-d h:i K"
        //});
    },

    // Generate the FormData
    getFormData: () => {
        event.preventDefault();
        let fd = new FormData();
        let data = $("#" + 'ScheduleAuditForm').serializeArray();
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