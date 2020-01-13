const RepeatSchedule = {};
const RepeatData = {};

// Closure/Modular Pattern
function ReccurrenceUtilities() {
    // Private Section

    // Calculate the dates for daily schedule
    const scheduleDaily = () => {};

    // Calculate the dates for weekly schedule
    const scheduleWeekly = () => {};

    // Calculate the dates for monthly schedule
    const scheduleMonthly = () => {};

    // Public Section
    return {
        ScheduleDaily: scheduleDaily,
        ScheduleWeekly: scheduleWeekly,
        ScheduleMonthly: scheduleMonthly
    };
}

const recurrenceUtilities = new ReccurrenceUtilities();

(function (RepeatData, RepeatSchedule, recurrenceUtilities) {
    
    // Private Section

    // Public Section

   

 


})(RepeatData, RepeatSchedule, recurrenceUtilities);

const AddRepeat = (function () {

    // Obj to hold the Repeat Data
    let repeat = {
        repeatType: 'daily',
        selectedRepeatType: '',
        isRepeatSet: false,
        startTime: '',
        endTime: '',
        expiryTime: '',
        expiryDuration: 2,
        recurrenceStartDate: '',
        recurrenceEndDate: '',
        recurrenceExpiryDate: '',
        recurrenceOccurences: 0,
        scheduleDuration: 0,

        repeatPattern: {
            activeDaysOfWeek: [],
            numberOfDays: 0
        }
    };

    let repeatConfig = {
        'daily': {
            'weekday': false,
            'interval' : 1
        },
        'weekly': {
            'days': [],
            'interval': 1
        },
        'monthly': {
            'date': 0,
            'interval': 1,
            'custom': false,
            'dayFrequency': '',
            'day': '',
            'dayInterval': 0
        },
        'yearly': {}
    };

    // Validate if the date is valid for repeat
    const isDateValidForRepeat = (startDate, repeatendDate) => !startDate.isAfter(repeatendDate.endOf('day'));

    //TODO
    const getDateByDay = function (dayNo, day, date) {
        // Find the req no day of given month
        let firstDay = date.clone().date(1);
        let reqDay = {};
        let nthDay = 0;
        while (firstDay.isBefore(date.clone().add(1, 'month').date(1))) {
            if (firstDay.format('dddd') === day) {
                nthDay++;
                reqDay = firstDay.clone();
            }
            if (nthDay == dayNo)
                break;
            firstDay.add(1, 'day');
        }
        return reqDay;
    };

    const getLastDateByDay = function (dayNo, day, date) {
        let lastDay = date.clone().endOf('month');
        let reqDay = {};

        if (lastDay.format('dddd') === day) {
            reqDay = lastDay;
            return reqDay;
        }

        while (true) {

            if (lastDay.format('dddd') === day) {
                reqDay = lastDay;
                break;
            } else {
                lastDay.subtract(1, 'day');
            }
        }
        return reqDay;
    };

    // Get the nth Occurence of a day of week for a month
    const daysNthOccurence = function (date) {
        //const ocurrenceMap = ['first', 'second', 'third', 'fourth', 'last'];
        let count = 0;
        const newDate = date.clone().date(1);
        const thresholdDay = date.clone();
        for (let i = 0; i < 31; i++) {
            if (newDate.day() === thresholdDay.day()) {
                count++;
            }
            newDate.add(1, 'day')
            if (newDate.isAfter(thresholdDay)) {
                break;
            }
        }
        // 
        return count;
    }

    const adjustDate = function (thisDate, thisMonth, thisYear) {
        var lastDateOfThisMonth = moment((thisMonth +1).toString() + '-1-' + thisYear).endOf('month').date();
        if (thisDate > lastDateOfThisMonth) {
            thisDate = lastDateOfThisMonth;
        }
        return thisDate;
    } 

    // Object holding various methods to calculate dates
    const getDatesForRepeat = {
        daily: (dailyConfig) => {
            const { weekday, interval } = dailyConfig;

            let startDateTime = moment(`${repeat.recurrenceStartDate} ${repeat.startTime}`);
            //let endDateTime = moment(`${repeat.recurrenceStartDate} ${repeat.endTime}`);
            let endDateTime = startDateTime.clone().add(repeat.scheduleDuration, 'hour');
            let expiryDateTime = endDateTime.clone().add(2, 'hour');
            //let endDateTime = moment(`${repeat.recurrenceEndDate} ${repeat.endTime}`);

            recurrenceEndDate = moment(repeat.recurrenceEndDate).endOf('day');
            //let expiryDateTime = moment(`${repeat.recurrenceExpiryDate} ${repeat.expiryTime}`);
            
            const scheduleDates = [];
            const scheduleDuration = endDateTime.diff(startDateTime);
            const expiryDuration = expiryDateTime.diff(endDateTime);
            
            while (isDateValidForRepeat(startDateTime, recurrenceEndDate)) {
                if (weekday === true && (startDateTime.format('dddd') === 'Saturday'
                    || startDateTime.format('dddd') === 'Sunday')) {
                    startDateTime.add(1, 'day');
                    continue;
                }
                const scheduleStartDateTime = startDateTime.clone().utc().format();
                const scheduleEndDateTime = endDateTime.clone().utc().format();
                const scheduleExpiryDateTime = expiryDateTime.clone().utc().format();

                scheduleDates.push({ scheduleStartDateTime: scheduleStartDateTime, scheduleEndDateTime: scheduleEndDateTime, scheduleExpiryDateTime: scheduleExpiryDateTime });

                startDateTime = startDateTime.add(interval, 'day');
                endDateTime = startDateTime.clone().add(scheduleDuration);
                expiryDateTime = endDateTime.clone().add(expiryDuration);
            }
            return scheduleDates;
        },
        weekly: (weeklyConfig) => {
            const { days, interval } = weeklyConfig;

            let startDateTime = moment(`${repeat.recurrenceStartDate} ${repeat.startTime}`);
            //let endDateTime = moment(`${repeat.recurrenceStartDate} ${repeat.endTime}`);
            let endDateTime = startDateTime.clone().add(repeat.scheduleDuration, 'hour');
            let expiryDateTime = endDateTime.clone().add(2, 'hour');
            //let endDateTime = moment(`${repeat.recurrenceEndDate} ${repeat.endTime}`);

            recurrenceEndDate = moment(repeat.recurrenceEndDate).endOf('day');
            //let expiryDateTime = moment(`${repeat.recurrenceExpiryDate} ${repeat.expiryTime}`);

            const scheduleDates = [];
            const scheduleDuration = endDateTime.diff(startDateTime);
            const expiryDuration = expiryDateTime.diff(endDateTime);

            while (isDateValidForRepeat(startDateTime, recurrenceEndDate)) {
                const isDayValidForRepeat = days.some(function (day) { return day === startDateTime.format('dddd') })
                if (!isDayValidForRepeat) { 
                    if (startDateTime.day() === 0) {
                        startDateTime = startDateTime.add(interval, 'week').subtract(6, 'day')
                        endDateTime = startDateTime.clone().add(scheduleDuration);
                        expiryDateTime = endDateTime.clone().add(expiryDuration);
                    }
                    else {
                        startDateTime = startDateTime.add(1, 'day');
                        endDateTime = startDateTime.clone().add(scheduleDuration);
                        expiryDateTime = endDateTime.clone().add(expiryDuration);
                    }
                    continue;
                }
                const scheduleStartDateTime = startDateTime.clone().utc().format();
                const scheduleEndDateTime = endDateTime.clone().utc().format();
                const scheduleExpiryDateTime = expiryDateTime.clone().utc().format();

                scheduleDates.push({ scheduleStartDateTime: scheduleStartDateTime, scheduleEndDateTime: scheduleEndDateTime, scheduleExpiryDateTime: scheduleExpiryDateTime });

                startDateTime.day() === 0
                    ? startDateTime = startDateTime.add(interval, 'week').subtract(6, 'day')
                    : startDateTime = startDateTime.add(1, 'day');

                endDateTime = startDateTime.clone().add(scheduleDuration);
                expiryDateTime = endDateTime.clone().add(expiryDuration);
            }
            return scheduleDates;
        },

       
        // TODO: Implement Monthly Repeat Date Calculation
        monthly: (monthlyConfig) => {
            const { custom, date, interval } = monthlyConfig;
            const { dayFrequency, day, dayInterval } = monthlyConfig;
            const scheduleDates = [];

            const startDateTime = moment(`${repeat.recurrenceStartDate} ${repeat.startTime}`);
            const recurrenceEndDate = moment(repeat.recurrenceEndDate).endOf('day');

            const scheduleDuration = repeat.scheduleDuration;
            const expiryDuration = 2;

            if (monthlyConfig.custom === false) {
                let recurMonthYear = startDateTime.clone().startOf('month');

                // date 28, 29, 30, 31
                if (parseInt(date) < startDateTime.date()) {
                    // skip this month by interval
                    recurMonthYear.add(interval, 'month');
                }

                while (true) {

                    let adjustedUserDate = adjustDate(date, recurMonthYear.month(), recurMonthYear.year())
                    recurMonthYear.date(adjustedUserDate);
                    recurMonthYear = moment(recurMonthYear.format('Y-M-D') + ' ' + repeat.startTime);


                    if (!isDateValidForRepeat(recurMonthYear, recurrenceEndDate)) {
                        break;
                    }

                    const scheduleStartDateTime = recurMonthYear.clone().utc().format();
                    const scheduleEndDateTime = recurMonthYear.clone().add(scheduleDuration, 'hour').utc().format();
                    const scheduleExpiryDateTime = moment(scheduleEndDateTime).clone().add(expiryDuration, 'hour').utc().format();

                    scheduleDates.push({ scheduleStartDateTime: scheduleStartDateTime, scheduleEndDateTime: scheduleEndDateTime, scheduleExpiryDateTime: scheduleExpiryDateTime });

                    recurMonthYear = recurMonthYear.add(interval, 'month').date(1);
                }
            }
            else if (monthlyConfig.custom === true) {

                let recurMonthYear = startDateTime.clone().startOf('month');
                let date = getDateByDay(dayFrequency, day, recurMonthYear);
                // date 28, 29, 30, 31
                if (date.date() < startDateTime.date()) {
                    // skip this month by interval
                    recurMonthYear.add(interval, 'month');
                }

                while (true) {

                    let adjustedUserDate = getDateByDay(dayFrequency, day, recurMonthYear)
                    recurMonthYear.date(adjustedUserDate.date());
                    recurMonthYear = moment(recurMonthYear.format('Y-M-D') + ' ' + repeat.startTime);

                    if (!isDateValidForRepeat(recurMonthYear, recurrenceEndDate)) {
                        break;
                    }

                    const scheduleStartDateTime = recurMonthYear.clone().utc().format();
                    const scheduleEndDateTime = recurMonthYear.clone().add(scheduleDuration, 'hour').utc().format();
                    const scheduleExpiryDateTime = moment(scheduleEndDateTime).clone().add(expiryDuration, 'hour').utc().format();

                    scheduleDates.push({ scheduleStartDateTime: scheduleStartDateTime, scheduleEndDateTime: scheduleEndDateTime, scheduleExpiryDateTime: scheduleExpiryDateTime });

                    recurMonthYear = recurMonthYear.add(interval, 'month').date(1);
                }
            }
            
            return scheduleDates;
        }
/*
      
        */
    };

    // Obj to hold the Jquey Selectors
    const elements = {
        $formContainer: $("#ScheduleAuditSideDiv"),
        $recStartTime: $('.recurrence-start-time'), 
        $recEndTime: $('.recurrence-end-time')
    };

    // Array of date and time fields
    const repeatDateTimeFields = ['.recurrence-start-time', '.recurrence-end-time', '.recurrence-expiry-time',
        '.recurrence-start-date', '.recurrence-end-date'];
    const repeatTimeFields = ['.recurrence-start-time', '.recurrence-end-time', '.recurrence-expiry-time'];

    // Initialization Method
    const init = function () {
        bindEvents();
    };

    // Bind the events related to Repeat Feature
    const bindEvents = function () {
        toggleAndSetRepeatType();
        //bindDateValuesToKeys();
        bindselectedDailyValueToConfig();
        bindselectedMonthlyValueToConfig();
        bindDurationToTimes();
        //bindFlatPickrToInput();
    };

    const bindAndPopulateInputFields = function () {
        bindFlatPickrToInput();
        populateTimeFields();
    };

    const populateTimeFields = function () {
        const times = getTimeFortimeSelection();
        // TODO
        renderDropdowns(repeatTimeFields, times);

    };

    const renderDropdowns = function (repeatTimeFields, times) {

        // Create the time object to pass to the Select2 data
        let timeOptions = times.map(function (time) {
            return { id: time, text: time };
        });

        let durationOptions = [
            { id: '1', text: '1 hour' },
            { id: '2', text: '2 hours' }, { id: '3', text: '3 hours' },
            { id: '4', text: '4 hours' }, { id: '5', text: '5 hours' },
            { id: '6', text: '6 hours' }, { id: '7', text: '7 hours' },
            { id: '8', text: '8 hours' }, { id: '9', text: '9 hours' },
            { id: '10', text: '10 hours' }, { id: '11', text: '11 hours' },
            { id: '12', text: '12 hours' }, { id: '13', text: '13 hours' },
            { id: '14', text: '14 hours' }, { id: '15', text: '15 hours' },
            { id: '16', text: '16 hours' }, { id: '17', text: '17 hours' },
            { id: '18', text: '18 hours' }, { id: '24', text: '1 day' },
            { id: '48', text: '2 days' }, { id: '72', text: '3 days' },
            { id: '96', text: '4 days' }, { id: '168', text: '1 week' },
            { id: '336', text: '2 weeks' }
        ];

        //$('.recurrence-start-time, .recurrence-end-time').select2({
        //    data: timeOptions,
        //    placeholder: 'Select Time',
        //    allowClear: false,
        //    tags: true,
        //    dropdownParent: $('#RepeatModal'),
        //    createTag: function (param) {
        //        let timeRegEx = /((([1-9])|(1[0-2])):([0-5])[0-9]\s(A|P|a|p)(M|m))/;
        //        isEnteredTimeValid = timeRegEx.test(param.term);
        //        if (isEnteredTimeValid) {
        //            return {
        //                id: param.term,
        //                text: param.term
        //            };
        //        }
        //        return null;
        //    },
        //    insertTag: function (data, tag) {
        //        console.log(data, tag);
        //        data.push(tag);
        //    }
        //});

        //$('.recurrence-expiry-time').select2({
        //    placeholder: 'Select Expiry Time',
        //    dropdownParent: $('#RepeatModal'),
        //    width: '100%'
        //});

        $('.month-select-day-interval').select2({
            placeholder: 'Select interval',
            //dropdownParent: $('#RepeatModal'),
            width: '100%'
        });

        $('.month-select-day').select2({
            placeholder: 'Select day',
            //dropdownParent: $('#RepeatModal'),
            width: '100%'
        });


        // Just a workaround to get the default value in the dropdown
        //const endTime = moment(`${moment().format('Y-M-D')} ${$('.recurrence-end-time').val()}`);
        //const expiryTime = endTime.add(2, 'hour').format('h:mm A');
        //const expiryOption = `<option selected value="${expiryTime}">${expiryTime}</option>`
        //// Emptying it for now, but need to deselect the selected ones and select the new option
        //$('.recurrence-expiry-time').empty();
        //$('.recurrence-expiry-time').append('<option></option>');
        //$('.recurrence-expiry-time').append(expiryOption);

        $('.recurrence-schedule-duration').select2({
            data: durationOptions,
            dropdownParent: $('#RepeatModal'),
            placeholder: 'Select Duration',
            allowClear: false
        });
    };

    // get moment duration in hour and minutes
    const getDurationAsHoursMinutes = function (duration) {
        const hours = parseInt(duration);
        const minutes = parseInt((moment.duration(duration, 'hour').asMinutes()) % 60);
        let timeString = '';
        timeString = hours > 0 ? hours + ' hr' + (hours > 0 ? 's ' : ' ') : '';
        timeString = timeString + minutes + ' min' + (minutes > 0 ? 's' : '');
        return timeString;
    };
    

    //  Calculation of times to render in the dropdown
    const getTimeFortimeSelection = function () {
        let times = [];
        //let oldDate = moment().startOf('day');
        let oldDate = moment($('.start-time').val());//.startOf('day');
        let newDate = oldDate.clone();
        let nextDate = oldDate.clone().add(1, 'day');
        let timeIntervalInMinutes = 30;

        // Get all the time in the array
        while (newDate.isBefore(nextDate)) {
            let timeForDropdown = newDate.format('hh:mm A');
            times.push(timeForDropdown);
            newDate.add(timeIntervalInMinutes, 'minute');
        }
        return times;
    };
        
    // Get the Key Identifier for the Repeat data object from HTML
    const getKeyIdentifier = function (dateOrTimeField) {
        return $(dateOrTimeField).attr('key-identifier');
    };

    // Save the selected value in the DOM to the Key in the Repeat Object.
    const saveRepeatDataHandler = function () {
        keyIdentifier = getKeyIdentifier.call(this);
        repeat[keyIdentifier] = $(this).val();
    };

    // Method to bind Event to save the enterred Date and Time to the Repeat Object
    const bindDateValuesToKeys = function () {
        repeatDateTimeFields.forEach((dateOrTimeField) => {
            const dateTimeInput= $(dateOrTimeField).val();
            const keyIdentifier = getKeyIdentifier(dateOrTimeField);
            repeat[keyIdentifier] = dateTimeInput;
            //elements.$formContainer.on('change', dateOrTimeField, saveRepeatDataHandler);
        });
    };

    const setMinStartTime = function () {
        // If the Start Date of schedule recurrence is same  as today, set min time for Start Time as current Time
        isRecurStartToday = moment($('.recurrence-start-date').val()).isSame(moment(), 'day');
        const fp = document.querySelector('.recurrence-start-time')._flatpickr;
        if (isRecurStartToday) {
            fp.config.minTime = moment().format('H:mm A');
            fp.setDate(moment().format('h:mm A'));
        } else {
            fp.config.minTime = '';
        } 
       
    };

    // Method to bind event to Start Time and End Time Field to update the Duration Dropdown
    const bindDurationToTimes = function () {

        elements.$formContainer.on('change', '.recurrence-start-time', function () {
            $('#userSelectedDurationValue').val($('.recurrence-schedule-duration').val());
            const userSelectedScheduleDuration = $('#userSelectedDurationValue').val();

            let startTime = moment(`${moment($('.start-time').val()).format('Y-M-D')} ${$('.recurrence-start-time').val()}`);
            let endTime = startTime.clone().add(userSelectedScheduleDuration, 'hour');

            const endTimeInastance = document.querySelector('.recurrence-end-time')._flatpickr;
            endTimeInastance.setDate(endTime.format('h:mm A')); // SET END TIME
            $('.recurrence-expiry-time').val(endTime.clone().add(2, 'hour').format('h:mm A')); // SET EXPIRY TIME
        });

        elements.$formContainer.on('select2:select', '.recurrence-schedule-duration', function () {
            $('#userSelectedDurationValue').val($(this).val());
            let userSelectedScheduleDuration = $('#userSelectedDurationValue').val();

            let startTime = moment(`${moment($('.start-time').val()).format('Y-M-D')} ${$('.recurrence-start-time').val()}`);
            let endTime = startTime.clone().add(userSelectedScheduleDuration, 'hour');

            const endTimeInastance = document.querySelector('.recurrence-end-time')._flatpickr;
            endTimeInastance.setDate(endTime.format('h:mm A')); // SET END TIME
            $('.recurrence-expiry-time').val(endTime.clone().add(2, 'hour').format('h:mm A')); // SET EXPIRY TIME
        });

        elements.$formContainer.on('change', '.recurrence-end-time', function () {

            let userSelectedScheduleDuration = parseInt($('#userSelectedDurationValue').val());

            let startTime = moment(`${moment().format('Y-M-D')} ${$('.recurrence-start-time').val()}`);
            let endTime = moment(`${moment().format('Y-M-D')} ${$(this).val()}`);

            let durationDiff = 0;

            if (endTime.format('A') === startTime.format('A')) {
                if (moment.duration(endTime.diff(startTime)).asHours() < 0) {
                    durationDiff = 24 + moment.duration(endTime.diff(startTime)).asHours();
                } else {
                    durationDiff = moment.duration(endTime.diff(startTime)).asHours();
                    if (durationDiff == 0 && userSelectedScheduleDuration < 24) {
                        durationDiff += 0.084; // 5 MINS
                        endTime.add(0.084, 'hour'); // 5 MINS
                    }
                }
            } else {
                if (endTime.format('A') === 'AM' && startTime.format('A') === 'PM') {
                    durationDiff = 24 + moment.duration(endTime.diff(startTime)).asHours();
                } else {
                    durationDiff = moment.duration(endTime.diff(startTime)).asHours();
                }
            }

            if (userSelectedScheduleDuration >= 24) {
                durationDiff += userSelectedScheduleDuration;
            }

            let hasOption = false;
            $('.recurrence-schedule-duration').find('option').each(function (index, value) {
                if (value.value.toString() === durationDiff.toString()) {
                    hasOption = true;
                }
            });

            if (!hasOption) {
                $('.recurrence-schedule-duration .calculatedDuration').remove();
                //const scheduleDurationRound = Math.round(durationDiff * 100) / 100;
                let newOption = `<option class="calculatedDuration" selected value="${durationDiff}">${getDurationAsHoursMinutes(durationDiff)}</option>`;
                $('.recurrence-schedule-duration').append(newOption);
                $('.recurrence-schedule-duration').html($('.recurrence-schedule-duration').find('option').sort(function (x, y) {
                    return parseFloat($(x).val()) > parseFloat($(y).val()) ? 1 : -1;
                }));
                $('.recurrence-schedule-duration').val(durationDiff).trigger('change');
            } else {
                $('.recurrence-schedule-duration').val(durationDiff).trigger('change');
            }

            const endTimeInastance = document.querySelector('.recurrence-end-time')._flatpickr;
            endTimeInastance.setDate(endTime.format('h:mm A')); // SET END TIME
            $('.recurrence-expiry-time').val(endTime.clone().add(2, 'hour').format('h:mm A')); // SET EXPIRY TIME
        });  
    };

    // Save the selected value in the DOM to the Key in the repeatConfig Object for Daily recurrence
    const saveDailyDataHandler = function () {
        let $selectedOptionForDaily = $(this).val();
        if ($selectedOptionForDaily === 'daily') {
            repeatConfig['daily'].weekday = false;
        }
        else if ($selectedOptionForDaily === 'weekday') {
            repeatConfig['daily'].weekday = true;
        }
    };

    const saveMonthlyDataHandler = function () {
        let $selectedOptionForMonthly = $(this).val();
        if ($selectedOptionForMonthly === 'monthly-date') {
            repeatConfig['monthly'].custom = false;
        }
        else if ($selectedOptionForMonthly === 'monthly-day') {
            repeatConfig['monthly'].custom = true;
        }
    };

    // Method to bind Event to save the enterred Date and Time to the Config Object
    const bindselectedDailyValueToConfig = function () {
        elements.$formContainer.on('change', 'input[name=DailyRecur]', saveDailyDataHandler);
    };

    const bindselectedMonthlyValueToConfig = function () {
        elements.$formContainer.on('change', 'input[name=MonthlyRecur]', saveMonthlyDataHandler);
    };

    //elements.$formContainer.on('change', '.day-interval-count', function () {
    //    repeatConfig['daily'].interval = $(this).val();
    //});

  

    // Format the Date and Time Fields as Calendar
    const bindFlatPickrToInput = function (timeFieldConfig, dateFieldConfig) {
        //dateConfig = {
        //    enableTime: false,
        //    noCalendar: false,
        //    altInput: true,
        //    altFormat: "j M, Y",
        //    dateFormat: "Y-m-d"
        //};

        //const timeConfig = {
        //    enableTime: true,
        //    noCalendar: true,
        //    dateFormat: "h:i K"
        //};

        //$('.recurrence-start-time, .recurrence-end-time, .recurrence-expiry-time').flatpickr(timeConfig);

        //['.recurrence-start-time', '.recurrence-end-time', '.recurrence-expiry-time'].forEach((el) => {
        //    //$(el).flatpickr(timeConfig);
        //});

        //['.recurrence-start-date', '.recurrence-end-date'].forEach((el) => {
        //    $(el).flatpickr(dateConfig);
        //});
       
    };

    
   

    // Show the Repeat Pattern selection options
    showrecurrenceField = el => {
        let recurrencePattern = $(el).val();
        switch (recurrencePattern) {
            case 'daily': $('.recur-daily').removeClass('hidden');
                break;
            case 'weekly': $('.recur-weekly').removeClass('hidden');
                break;
            case 'monthly': $('.recur-monthly').removeClass('hidden');
                break;
            case 'yearly': $('.recur-yearly').removeClass('hidden');
                break;
            default: break;
        }
    };

    // Set the values for the RepeatData Object
    const setRepeatData = function () {

        // Values like daily, weekly, monthly, yearly
        repeat.repeatType = $(this).val();
    };

    // Method to Toggle and Set Repeat Type
    const toggleAndSetRepeatType = function () {
        elements.$formContainer.on('change', 'input[name=RepeatType]', function () {
            setRepeatData.call(this);
            $('.recurrence-value-item').addClass('hidden');
            showrecurrenceField(this);



            const recurEndDateConfig = {
                enableTime: false,
                noCalendar: false,
                altInput: true,
                altFormat: 'j M, Y',
                dateFormat: 'Y-m-d',
                defaultDate:'',
                minDate:''
            };

            if (this.value === 'daily') {
                recurEndDateConfig.defaultDate = moment($('.recurrence-start-date').val()).add('3', 'month').format('Y-MM-D');
                recurEndDateConfig.minDate= moment($('.recurrence-start-date').val()).format('Y-MM-D')
            } else if (this.value === 'weekly') {
                recurEndDateConfig.defaultDate = moment($('.recurrence-start-date').val()).add('6', 'month').format('Y-MM-D');
                recurEndDateConfig.minDate= moment($('.recurrence-start-date').val()).format('Y-MM-D')
            } else if (this.value === 'monthly') {
                recurEndDateConfig.defaultDate = moment($('.recurrence-start-date').val()).add('1', 'year').format('Y-MM-D');
                recurEndDateConfig.minDate= moment($('.recurrence-start-date').val()).format('Y-MM-D')
            } else if (this.value === 'yearly') {
                recurEndDateConfig.defaultDate = moment($('.recurrence-start-date').val()).add('3', 'year').format('Y-MM-D');
                recurEndDateConfig.minDate= moment($('.recurrence-start-date').val()).format('Y-MM-D')
            }

            // If repeat is already set, do not change the recurrence end date
            if(!repeat.isRepeatSet)
                $('.recurrence-end-date').flatpickr(recurEndDateConfig);
        });
    };

    // TODO: Change the message after making the schedule recur.
    const showRecurrenceInfo = function () {

        let recurrenceFrequency = '';
        switch (repeat.repeatType) {
            case 'daily': repeatConfig['daily'].weekday === false ?
                repeatConfig['daily'].interval > 1 ? recurrenceFrequency = `every ${repeatConfig['daily'].interval} days` : recurrenceFrequency = `every day`
                : recurrenceFrequency = 'every weekday';
                break;
            case 'weekly': repeatConfig['weekly'].days.length > 1 ?
                recurrenceFrequency = repeatConfig['weekly'].days.slice(0, -1).join(', ') + ' and ' + repeatConfig['weekly'].days.slice(-1)
                : recurrenceFrequency = repeatConfig['weekly'].days.slice(-1);

                repeatConfig['weekly'].interval > 1 ? recurrenceFrequency = `every ${repeatConfig['weekly'].interval} week(s) on ${recurrenceFrequency}` : recurrenceFrequency = `every ${recurrenceFrequency}`;
                break;
            case 'monthly': repeatConfig['monthly'].custom === false
                ? recurrenceFrequency = `every ${repeatConfig['monthly'].interval} month(s)`
                : '';
                repeatConfig['monthly'].custom === true
                    ? recurrenceFrequency = `the ${$('.month-select-day-interval').find('option:selected').text()} ${repeatConfig['monthly'].day} every ${repeatConfig['monthly'].interval} month(s)`
                    : '';
                break;
            default: break;
        }  
        let { startTime, endTime, recurrenceStartDate, recurrenceEndDate } = repeat;
        recurrenceStartDate = moment(recurrenceStartDate).format('D MMM Y');
        recurrenceEndDate = moment(recurrenceEndDate).format('MMM D Y');

        let msgStr = `Occurs ${recurrenceFrequency} effective ${recurrenceStartDate} until ${recurrenceEndDate} from ${startTime} to ${endTime}`
            //${repeat.repeatType === 'monthly' && repeatConfig['monthly'].custom === false
            //? 'day ' + repeatConfig['monthly'].date + ' of'
            //: `the ${$('.month-select-day-interval').children('option:selected').text()}
        /*${repeatConfig['monthly'].day} of`}*/ ;

        $('.time-dropdown-container').addClass('hidden');
        $('.recurrence-msg').removeClass('hidden').html(msgStr);
        $('.repeat-modal-btn').html('EDIT RECURRENCE');

    };

    // TODO: Implement the Validations
    const validateRepeat = function () {
        let isRepeatValid = true;

        // Validate Recurrence Start Date
        if ($('.recurrence-start-date').val() === '' || $('.recurrence-start-date').val() === null) {
            $('.recurrence-start-date-validation-msg').removeClass('hidden');
            let startTimeValidationText = $('.recurrence-start-date').attr('validation-msg');
            $('.recurrence-start-date-validation-msg').html(startTimeValidationText);
            isRepeatValid = false;
        }

        // Validate Recurrence End Date
        if ($('.recurrence-end-date').val() === '' || $('.recurrence-end-date').val() === null) {
            $('.recurrence-end-date-validation-msg').removeClass('hidden');
            let endTimeValidationText = $('.recurrence-end-date').attr('validation-msg');
            $('.recurrence-end-date-validation-msg').html(endTimeValidationText);
            isRepeatValid = false;
        }

        // Validate if all the checkboxes are checked in Weekly Repeat
        if (repeat.repeatType === 'weekly') {
            if ($('input[name=WeeklyDays]:checked').length <= 0) {
                $('.recurrence-weekly-days').removeClass('hidden');
                isRepeatValid = false;
            }
        }

        // Check if the recurrence start date is after the recurrence end date
        if ($('.recurrence-end-date').val() !== '' && $('.recurrence-start-date').val() !== '') {
            const startDateMoment = moment($('.recurrence-start-date').val());
            const endDateMoment = moment($('.recurrence-end-date').val());
            if (startDateMoment.isAfter(endDateMoment)) {
                $('.recurrence-date-validation-msg').html('Recurrence Start Date must be before Recurrence End Date');
                isRepeatValid = false;
            }
        }



        // Check if the duration of the schedule is longer than the recur intervals.
        if ($('.recurrence-schedule-duration').val() > repeatConfig[repeat.repeatType].interval * 24) {
            isRepeatValid = false;
            $('.set-repeat').addClass('disabled');
          
            $.alert({
                title: 'Alert!',
                content: 'The duration of the schedule must be shorter than how frequently it occurs!',
                draggable: false
            });
        
            $('.set-repeat').removeClass('disabled');
            //M.toast({//
            //    html: 'The duration of the schedule must be shorter than how frequently it occurs',
            //    displayLength: '3000',
            //    classes: 'failed-toast',
            //    completeCallback: function () {
            //        $('.set-repeat').removeClass('disabled');
            //    }
            //});
        }

        // Check if the date range selected has any valid dates
        if (isRepeatValid) {
            if ($('.recurrence-end-date').val() !== '' && $('.recurrence-start-date').val() !== '') {
                const dates = getDatesToScheduleOn(repeat.repeatType);
                if (dates.length < 1) {
                    isRepeatValid = false;
                    $('.set-repeat').addClass('disabled');
                    M.toast({
                        html: 'The date range does not contain any valid date(s)',
                        displayLength: '4000',
                        classes: 'failed-toast',
                        completeCallback: function () {
                            $('.set-repeat').removeClass('disabled');
                        }
                    });
                }
            }
        }

        return isRepeatValid;
    };

    // Replace 0 with 1 on entering 0 in the input boxes
    elements.$formContainer.on('blur', '.day-interval-count, .week-interval-count, .month-interval-count, .month-schedule-date', function () {
        if ($(this).val() < 1) {
            $(this).val(1);
        }
    });

    elements.$formContainer.on('change', '.recurrence-start-date', function () {
        const recurEndTimeInstance = document.querySelector(".recurrence-end-date")._flatpickr;
        const startDateMoment  = moment($('.recurrence-start-date').val());
        const endDateMoment = moment($('.recurrence-end-date').val());
        if (endDateMoment.isAfter(startDateMoment)) {
            recurEndTimeInstance.config.minDate = moment($('.recurrence-start-date').val()).format('Y-MM-D');
        }
        setMinStartTime();
    });

    // TODO: Perform all the calculations and validations after clicking on DONE button:
    elements.$formContainer.on('click', '.set-repeat', function (event) {
        bindDateValuesToKeys();

        // Save data for daily
        repeatConfig['daily'].interval = $('.day-interval-count').val();
        repeat.scheduleDuration = $('.recurrence-schedule-duration').val();

        // Save data for weekly
        repeatConfig['weekly'].days = [];
        $('input[name=WeeklyDays]:checked').each(function (key, value) { repeatConfig['weekly'].days.push(this.value) });
        //repeatConfig['weekly'].days = $('.day-of-week:checkbox:checked').map(function () { return this.value });
        repeatConfig['weekly'].interval = $('.week-interval-count').val();

        // Save data for monthly
        if (repeatConfig['monthly'].custom === false) {
            repeatConfig['monthly'].date = $('.month-schedule-date').val();
            repeatConfig['monthly'].interval = $('.month-interval-count').val();
        } else if (repeatConfig['monthly'].custom === true) {
            repeatConfig['monthly'].dayFrequency = $('.month-select-day-interval').val();
            repeatConfig['monthly'].day = $('.month-select-day').val();
            repeatConfig['monthly'].interval = $('.month-custom-interval-count').val();
        }



        repeat.scheduleDuration = $('.recurrence-schedule-duration').val();


        const isRepeatValid = validateRepeat();
        const repeatModalElem = document.getElementById('RepeatModal');
        const RepeatModalInstance = M.Modal.getInstance(repeatModalElem);
        if (!isRepeatValid) {
            //RepeatModalInstance.open();
            return;
        }

        // setting the audit frequency to repeat 
        AuditSchedule.frequency = 'repeat';
        repeat.isRepeatSet = true;

        // save the repeat type selected
        repeat.selectedRepeatType = $('input[name=RepeatType]:checked').val();
        
        // close the modal after setting the repeat options
        RepeatModalInstance.close();

        // show the recurring message
        showRecurrenceInfo();



    });

    elements.$formContainer.on('click', '.clear-repeat', function () {
        AuditSchedule.frequency = 'once';
        $('.recurrence-msg').empty().addClass('hidden');
        $('.time-dropdown-container').removeClass('hidden');
        $('.repeat-modal-btn').html('MAKE RECURRING');
        repeat.isRepeatSet = true;
        setRecurrencePatternToDefault();

        const timeConfig = {
            enableTime: true,
            noCalendar: true,
            dateFormat: "h:i K"
        };

        const recurStartTimeConfig = { ...timeConfig };      

        recurStartTimeConfig.defaultDate = moment($('.start-time').val()).format('h:mm A');
        recurStartTimeConfig.appendTo = $('.parent-recurrence-start-time').get(0);
        $('.recurrence-start-time').flatpickr(recurStartTimeConfig);

        $('.recurrence-start-date').flatpickr({
            enableTime: false,
            noCalendar: false,
            altInput: true,
            altFormat: 'j M, Y',
            dateFormat: 'Y-m-d',

            defaultDate: moment($('.start-time').val()).format('Y-MM-D'),
            minDate: moment($('.start-time').val()).format('Y-MM-D')
        });
        $('.recurrence-end-date').flatpickr({
            enableTime: false,
            noCalendar: false,
            altInput: true,
            altFormat: 'j M, Y',
            dateFormat: 'Y-m-d',

            defaultDate: moment($('.recurrence-start-date').val()).add('3', 'month').format('Y-MM-D'),
            minDate: moment($('.start-time').val()).format('Y-MM-D')
        });
      //  $('.clear-repeat').attr("disabled", true); 
    });
  

    // TODO: Setting initial values on basis of Single Day value on repeat
    elements.$formContainer.on('click', '.repeat-modal-btn', function () {
        // If Recurrence is already set, open the modal with the selected values
        if (repeat.isRepeatSet) { 
            const repeatModalElem = document.getElementById('RepeatModal');
            const RepeatModalInstance = M.Modal.getInstance(repeatModalElem);

            // To open the Modal with selected Repeat Type
            $('input[name=RepeatType]').each(function () {
                    if ($(this).val() === repeat.selectedRepeatType)
                    $(this).prop('checked', true).trigger('change');
            });

           $('.clear-repeat').attr("disabled", false); 
            RepeatModalInstance.open();
            return;
        }

       //$('.clear-repeat').attr("disabled", true);        

        // tabindex property needs to be removed from the model so that the
        // flatpickr-timepickr can workrecurrence-start-time
        $('#RepeatModal').removeAttr('tabindex');

        const timeConfig = {
            enableTime: true,
            noCalendar: true,
            dateFormat: "h:i K"
        };

        const recurStartTimeConfig = { ...timeConfig };
        const recurEndTimeConfig = { ...timeConfig };
        const recurExpiryTimeConfig = { ...timeConfig };

        recurStartTimeConfig.defaultDate = moment($('.start-time').val()).format('h:mm A');
        recurStartTimeConfig.appendTo = $('.parent-recurrence-start-time').get(0);
        
        recurEndTimeConfig.defaultDate = moment($('.end-time').val()).format('h:mm A');
        recurEndTimeConfig.appendTo = $('.parent-recurrence-end-time').get(0)
        recurExpiryTimeConfig.defaultDate = moment($('.expiry-time').val()).format('h:mm A');

        $('.recurrence-start-time').flatpickr(recurStartTimeConfig);
        $('.recurrence-end-time').flatpickr(recurEndTimeConfig);
        $('.recurrence-expiry-time').flatpickr(recurExpiryTimeConfig);

        //$('.recurrence-start-time').flatpickr{ appendTo: ${ '.parent-recurrence-start-time' }; };




        //$('.recurrence-start-time, .recurrence-end-time, .recurrence-expiry-time').flatpickr(timeConfig);


        $('.recurrence-start-date').flatpickr({
            enableTime: false,
            noCalendar: false,
            altInput: true,
            altFormat: 'j M, Y',
            dateFormat: 'Y-m-d',
             
            defaultDate: moment($('.start-time').val()).format('Y-MM-D'),
            minDate: moment($('.start-time').val()).format('Y-MM-D')
        });
        $('.recurrence-end-date').flatpickr({
            enableTime: false,
            noCalendar: false,
            altInput: true,
            altFormat: 'j M, Y',
            dateFormat: 'Y-m-d',
             
            defaultDate: moment($('.recurrence-start-date').val()).add('3', 'month').format('Y-MM-D'),
            minDate: moment($('.start-time').val()).format('Y-MM-D')
        });

        setRecurrencePatternToDefault();


        $('.recurrence-start-time').flatpickr(recurStartTimeConfig);
        $('.recurrence-end-time').flatpickr(recurEndTimeConfig);
        $('.recurrence-expiry-time').flatpickr(recurExpiryTimeConfig);

        $('.recurrence-schedule-duration').trigger('change');
        //$('.recurrence-end-time').trigger('change');


        // Hide the validation messages
        $('.recurrence-start-date-validation-msg').addClass('hidden');
        $('.recurrence-end-date-validation-msg').addClass('hidden');
    });

    const setRecurrencePatternToDefault = function () {

        // Set the Schedule time to the time filled in the Create Form
        const originalStartTime = moment(`${$('.start-time').val()}`);
        const originalEndTime = moment(`${$('.end-time').val()}`);

        const scheduleDuration = moment.duration(originalEndTime.diff(originalStartTime)).asHours();
        const recurEndTime = originalEndTime.format('h:mm A');

        let isDurationOptionFound = false;

        $('.recurrence-schedule-duration').find('option').each(function (key, value) {
            //$(this).removeAttr('selected');
            if ($(this).val() == scheduleDuration) {
                //$(this).attr('selected', true);
                isDurationOptionFound = true;
            }
        });

        if (!isDurationOptionFound) {
            //const scheduleDurationRound = Math.round(scheduleDuration * 100) / 100;
            let newOption = `<option value="${scheduleDuration}" selected>${getDurationAsHoursMinutes(scheduleDuration)}</option>`;

            $('.recurrence-end-time').append(`<option class="calculatedDuration" value="${recurEndTime}" selected>${recurEndTime}</option>`);
            $('.recurrence-schedule-duration').append(newOption);
            $('.recurrence-schedule-duration').html($('.recurrence-schedule-duration').find('option').sort(function (x, y) {
                return parseFloat($(x).val()) > parseFloat($(y).val()) ? 1 : -1;
            }));
            $('.recurrence-schedule-duration').val(durationDiff).trigger('change');
        } else {
            $('.recurrence-schedule-duration').val(scheduleDuration).trigger('change');
            
        }


        //Set the defaults for Daily
        $('input[name=DailyRecur]').each(function () {
            if (this.value === 'daily') {
                $(this).prop('checked', true);
                $(this).trigger('change');
            }
        });
        $('.day-interval-count').val('1');

        // Set the defaults for the Weekly Option
        let dayOfWeek = originalStartTime.format('dddd');
        $('.day-of-week').each(function (key, value) {
            $(this).prop('checked', false);
            if ($(this).val() === dayOfWeek) {
                $(this).prop('checked', true).trigger('change');
            }
        });
        $('.week-interval-count').val('1');

        // Set defaults for monthly
        const todayDate = originalStartTime.date();
        const nthDay = daysNthOccurence(originalStartTime);
        $('.month-schedule-date').val(todayDate);
        $('.month-interval-count').val('1');
        $('.month-custom-interval-count').val('1');
        $('.month-select-day').val(originalStartTime.format('dddd')).trigger('change');
        $('.month-select-day-interval').val(nthDay).trigger('change');


        // Open the Modal on Daily if Repeat is not set.
        $('input[name=RepeatType]').each(function () {
            if (this.value === 'daily') {
                $(this).prop('checked', true);
                $(this).trigger('change');
            }
        });

        setMinStartTime();
        $('#userSelectedDurationValue').val($('.recurrence-schedule-duration').val());
        $('.recurrence-start-time').trigger('change');
    }

    // Constructor to create a new schedule
    const Schedule = function (schedule) {
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
        this.FBOCode = schedule.FBOCode;
        return this;
    };

    // Method to get the List of Repeating Dates
    const getDatesToScheduleOn = function (repeatType) {
        switch (repeatType) {
            case 'daily': return getDatesForRepeat.daily(repeatConfig['daily']);

            //case 'weekdays': return getDatesForRepeat.weekdays(repeatData);

            case 'weekly': return getDatesForRepeat.weekly(repeatConfig['weekly']);

            //case 'quarterly': return getDatesForRepeat.quarterly(repeatData);

            case 'monthly': return getDatesForRepeat.monthly(repeatConfig['monthly']);

            //case 'yearly': return getDatesForRepeat.annualy(repeatData);

            //case 'firstDay': return getDatesForRepeat.firstDay(repeatData);

            //case 'lastDay': return getDatesForRepeat.lastDay(repeatData);

            default: return;
        }
    };

    const getRepeatingSchedules = function (schedule) {
        const datesToScheduleOn = getDatesToScheduleOn(repeat.repeatType);
        const schedules = [];
        //return datesToScheduleOn;

        // Create repeating audits
        datesToScheduleOn.forEach(function (date, index) {
            const newSchedule = new Schedule(schedule);
            const auditChecklists = JSON.parse(JSON.stringify(schedule.AuditChecklistDto));
            const auditChecklistDto = auditChecklists.map(function (checklist) {
                checklist.checklistScheduledStartDateTime = date.scheduleStartDateTime;
                checklist.checklistScheduledEndDateTime = date.scheduleEndDateTime;
                return checklist;
            });
            
            newSchedule.AuditScheduledStartDateTime = date.scheduleStartDateTime;
            newSchedule.AuditScheduledEndDateTime = date.scheduleEndDateTime;
            newSchedule.AuditExpiryDateTime = date.scheduleExpiryDateTime;
            newSchedule.CreatedBy = Header.UserFullName;
            newSchedule.AuditChecklistDto = auditChecklistDto;
            schedules.push(newSchedule);
        });
        return schedules;
    };

    init();

    return {
        obj: repeat,
        bindFlatpickrToRepeatFields: bindAndPopulateInputFields,
        createRepeatingSchedules: getRepeatingSchedules,
        repeatObj : repeat

    };
})();

