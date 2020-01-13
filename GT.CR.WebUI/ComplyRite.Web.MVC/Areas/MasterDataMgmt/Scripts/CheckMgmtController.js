//$(document).ready(function () {
$(function () {
    function saveFormData(formId, url) {
        var data = GetformData(formID);
        debugger;
        $.ajax({
            type: "POST",
            url: url,
            data: data,
            contentType: false,
            processData: false,
            success: function (out) {
                console.log(out);
                $("#PreviewModalBody").html(out);
            },
            error: function (msg) {
            }
        });
    }

    $('#proceedAnyway').on('click', function () {
        $('#summaryTextDivId').hide('slide', { direction: 'left' }, 600);
        $('#ReasonId').show('slide', { direction: 'right' }, 600);
        $('#proceedAnyway').addClass('hidden');
        $('#SubmitReason').removeClass('hidden');
    });

    $('#cancelInactivation').on('click', function () {
        $('#summaryTextDivId').show();
        $('#ReasonId').hide();
        $('#proceedAnyway').removeClass('hidden');
        $('#SubmitReason').addClass('hidden');
    });

    //$('#SaveNewCheckToDraft').on("click", function () {
    //    var data = GetformData('CreateNewCheckForm');
    //    debugger;
    //    $.ajax({
    //        type: "POST",
    //        url: '/CheckManagement/SaveCheckToDraft',
    //        data: data,
    //        contentType: false,
    //        processData: false,
    //        success: function (out) {
    //            console.log(out);
    //            $('.MainLayoutBody').html(out);
    //        },
    //        error: function (msg) {
    //        }
    //    });       
    //});
   
    $('.open-previewTemplate').on("click", function () {
        var data = GetformData('CreateNewCheckForm');
        debugger;
        $.ajax({
            type: "POST",
            url: '/CheckManagement/GetCheckPreviewPartial',
            data: data,
            contentType: false,
            processData: false,
            success: function (out) {
                console.log(out);
                $("#PreviewModalBody").html(out);
            },
            error: function (msg) {
            }
        });
        //$.ajax({
        //    url: this.href,
        //    type: 'GET',
        //    cache: false,
        //    data: { model: id },
        //    success: function (result) {
        //        $('#editModal').html(result).find('.modal').modal({
        //            show: true
        //        });
        //    }
        //});

    });

    //$('.open-inactivationConfirmation').on("click", function () {           
    //    $.get("/CheckManagement/GetInactivationSummary", function (data, status) {

    //    });
    //});


    


    // add multiple select / deselect functionality
    //$("#selectall").click(function () {
    //    $('.name').attr('checked', this.checked);
    //});

    // if all checkbox are selected, then check the select all checkbox
    // and viceversa
    //$(".name").click(function () {

    //    if ($(".name").length === $(".name:checked").length) {
    //        $("#selectall").attr("checked", "checked");
    //    } else {
    //        $("#selectall").removeAttr("checked");
    //    }

    //});

    


    //---------------- Method to get the form data ------------------//
    //------------------ Author : Sharath K M ----------------------//
    function GetformData(formID) {
        var fd = new FormData();
        var data = $("#" + formID).serializeArray();
        $.each(data, function (key, input) {
            fd.append(input.name, input.value);
        });
        return fd;
    }
});
function readURL(input, id) {
    debugger;
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#' + id)
                .attr('src', e.target.result);
            //.width(150)
            //.height(200);
        };

        reader.readAsDataURL(input.files[0]);
    }
}