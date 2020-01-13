

$(function () {
    
    //To toggle the filter row for Published Checklist
    $(".tableFilterBtn").click(function () {
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