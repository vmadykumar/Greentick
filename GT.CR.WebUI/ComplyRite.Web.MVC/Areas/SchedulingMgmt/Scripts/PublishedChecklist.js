

$(function () {
     // Select all checkbox for Published Checklist functionality
    $(".selectAllPublishedSchedules").click(function () {
        if ($('.selectAllPublishedSchedules').prop('checked') == true) {
            $('.scheduleChecks').prop('checked', true);

            //display the activate/inactivate row in table
            $('.multiselectRow').css('display', '');
        }
        else {
            $('.scheduleChecks').prop('checked', false);
            $('.multiselectRow').css('display', 'none');
        }
    });


    //To Hide/Show the Active/Inactive row on selecting any check
    $('.scheduleChecks').click(function () {
        var total = $('input[name="scheduleChecks"]:checked').length;
        console.log(total);

        if (total > 1) {
            $('.multiselectRow').css('display', '');
        }
        else
            $('.multiselectRow').css('display', 'none');
        
    });


    //To toggle the filter row for Published Checklist
    $("#filterBtn").click(function () {
        $(".filterRow").toggle(
        );

        //To change the background colour of the Filter Btn on toggle
        if ($("#filterBtn").hasClass("filter-default")) {
            $("#filterBtn").removeClass("filter-default");
            $("#filterBtn").addClass("filter-selected");
        }
        else {
            $("#filterBtn").removeClass("filter-selected");
            $("#filterBtn").addClass("filter-default");
        }
        
    });


    //To initialize all models/ dropdown----------------------------------------
    $(function () {

        //initialize all modals
        $('.modal').modal();

        ////now you can open modal from code
        //$('#modal1').modal('open');

        //or by click on trigger
        $('.trigger-modal').modal();
        $('.dropdown-trigger').dropdown({ hover: true });
    }); // end of document ready

})