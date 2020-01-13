$(function () {
    $('.filterToggleClass').on('click', function () {
        debugger;
        $('.filterRow').fadeToggle("slow", "linear");
    });

    //$('[data-toggle="popover"]').popover();

    $("input[class='FileInputClass']").click(function () {
        debugger
        //$("#ImgIdForCheck").click(function () {
        $(this).siblings('input[type="file"]').click();
        //$("input[id='Img_file']").click(); 
    });

    /******* Multiselect & Highlighting of row Functionality ******/
    $('#SelectAllCheckBox').on('change', function () {
        $(this).prop('checked') === true ? $('.individualCheck[type="checkbox"]').prop('checked', true) : $('.individualCheck[type="checkbox"]').prop('checked', false);
    });

    $('.individualCheck, #SelectAllCheckBox').on('change', function () {
        var selectedRowsCount = $('.individualCheck[type="checkbox"]:checked').length;

        //Functionality to show activate/deactivate button
        if (selectedRowsCount > 0) {
            $('.multiselectRow').fadeIn("fast");
            $('.selectedCount').text(" (" + selectedRowsCount + ")");
        }
        else {
            $('.multiselectRow').fadeOut("fast");
            $('#SelectedRowsCount').text("");
        }

        //Select All Button functionality to be checked when all checkboxes are checked
        $('.individualCheck[type="checkbox"]:checked').length === 0 ? $('#SelectAllCheckBox[type="checkbox"]').prop('checked', false) : "";
        $('.individualCheck[type="checkbox"]:not(":checked")').length === 0 ? $('#SelectAllCheckBox[type="checkbox"]').prop('checked', true) : $('#SelectAllCheckBox[type="checkbox"]').prop('checked', false);

        //To highlight a row when it's checkbox is checked        
        if ($(this).prop('checked') === true) {
            $(this).prop('id') !== "SelectAllCheckBox" ? $(this).parents('tr').addClass("highlightRow") : $('.individualCheck[type="checkbox"]:checked').parents('tr').addClass("highlightRow");
        }
        else {
            $(this).prop('id') !== "SelectAllCheckBox" ? $(this).parents('tr').removeClass("highlightRow") : $('.individualCheck[type="checkbox"]').parents('tr').removeClass("highlightRow");
        }
    });

    $('#SelectAllInactiveCheckBox').on('change', function () {
        $(this).prop('checked') === true ? $('.individualInactiveCheck[type="checkbox"]').prop('checked', true) : $('.individualInactiveCheck[type="checkbox"]').prop('checked', false);
    });

    $('.individualInactiveCheck, #SelectAllInactiveCheckBox').on('change', function () {
        debugger;
        var selectedRowsCount = $('.individualInactiveCheck[type="checkbox"]:checked').length;

        //Functionality to show activate/deactivate button
        if (selectedRowsCount > 0) {
            $('.multiSelectInactiveRow').fadeIn("fast");
            $('.selectedInactiveRowCount').text(" (" + selectedRowsCount + ")");
        }
        else {
            $('.multiSelectInactiveRow').fadeOut("fast");
            $('#selectedInactiveRowCount').text("");
        }

        //Select All Button functionality to be checked when all checkboxes are checked
        $('.individualInactiveCheck[type="checkbox"]:checked').length === 0 ? $('#SelectAllInactiveCheckBox[type="checkbox"]').prop('checked', false) : "";
        $('.individualInactiveCheck[type="checkbox"]:not(":checked")').length === 0 ? $('#SelectAllInactiveCheckBox[type="checkbox"]').prop('checked', true) : $('#SelectAllInactiveCheckBox[type="checkbox"]').prop('checked', false);

        //To highlight a row when it's checkbox is checked
        if ($(this).prop('checked') === true) {
            $(this).prop('id') !== "SelectAllInactiveCheckBox" ? $(this).parents('tr').addClass("highlightRow") : $('.individualInactiveCheck[type="checkbox"]:checked').parents('tr').addClass("highlightRow");
        }
        else {
            $(this).prop('id') !== "SelectAllInactiveCheckBox" ? $(this).parents('tr').removeClass("highlightRow") : $('.individualInactiveCheck[type="checkbox"]').parents('tr').removeClass("highlightRow");
        }
    });
});