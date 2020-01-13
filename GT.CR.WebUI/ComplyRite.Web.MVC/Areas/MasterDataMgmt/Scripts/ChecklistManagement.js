$(function () {
    // START: TOAST
    var feedback = $('#ViewBag_Feedback').val();
    var color = $('#ViewBag_Color').val();
 
    var toastConfig = {
        classes: 'rounded ' + ((color == "green") ? "toast-success" : "toast-error"),
        inDuration: '500',
        displayLength: '5000',
        html: feedback
    };
    if (feedback.length) {
        M.toast(toastConfig);
    }
    // END: TOAST

    // DROPDOWN CHANGE EVENTS:
    $("#locationSelectDropdown").on("change", function (event) {
        var UUID = $("#UUID").val();
        var LocationCode = this.value;

        $.ajax({
            url: "/ChecklistManagement/GetAreaByUUIDAndLocationCode?UUID=" + UUID + "&LocationCode=" + LocationCode,
            type: "GET",
            success: function (data) {
                var json = JSON.parse(data);
                $("#areaSelectDropdown").empty();
                $("#areaSelectDropdown").append("<option disabled selected>Select Area</option>");
                $.each(json, function (index, value) {
                    $("#areaSelectDropdown").append("<option value=" + value.AreaCode + ">" + value.AreaName + "</option>");
                });
                $("#areaSelectDropdown").removeAttr("disabled");
            }
        });
    });
    $("#areaSelectDropdown").on("change", function (event) {
        var UUID = $("#UUID").val();
        var LocationCode = $("#locationSelectDropdown").val();
        var AreaCode = this.value;

        $.ajax({
            url: "/ChecklistManagement/GetSubAreaByUUIDLocationCodeAndAreaCode?UUID=" + UUID + "&LocationCode=" + LocationCode + "&AreaCode=" + AreaCode,
            type: "GET",
            success: function (data) {
                var json = JSON.parse(data);
                $("#subAreaSelectDropdown").empty();
                $("#subAreaSelectDropdown").append("<option disabled selected>Select Sub Area</option>");
                $.each(json, function (index, value) {
                    $("#subAreaSelectDropdown").append("<option value=" + value.SubAreaCode + ">" + value.SubAreaName + "</option>");
                });
                $("#subAreaSelectDropdown").removeAttr("disabled");
            }
        });
    });

    function readURL(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('.preview-image-container').html(
                    '<div class="preview-image-component">' +
                    '<img src="' + e.target.result + '" height="50">' +
                    '<div class="clear-image"><i class="material-icons">clear</i></div>' +
                    '</div>'
                );
            }

            reader.readAsDataURL(input.files[0]);

        } else {
            $('.preview-image-container').html('<i class="material-icons preview-image-not-available">image</i>');
        }
    }

    // CREATE CHECKLIST ICON:
    $("#createNewChecklistIcon").change(function () {
        readURL(this); // PREVIEW IMAGE
        $("#createNewChecklistForm").validate().element("#createNewChecklistIcon"); // VALIDATE THE FILE
    });
    $('#createNewChecklistForm').on('click', '.clear-image i', function () {
        $("#createNewChecklistIcon")[0].value = ""; // DELETE FILE
        $(this).closest('.file-field').find('.file-path').removeClass('valid').val(''); // REMOVE INPUT STYLE
        $('.preview-image-container').html('<i class="material-icons preview-image-not-available">image</i>'); // REMOVE IMAGE PREVIEW
        $("#createNewChecklistForm").validate().element("#createNewChecklistIcon"); // VALIDATE THE FILE
    });
  
    // EDIT
    $("#editChecklistIcon").change(function () {
        $("#editChecklistForm").validate().element("#editChecklistIcon"); // VALIDATE THE FILE
        $("#editChecklistIcon").removeAttr('title');
        readURL(this); // PREVIEW IMAGE
        $('#oldImageName').val() != '' ? $('#deletedImage').val(true) : null; // FILE DELETED = TRUE
    });
    $('#editChecklistForm').on('click', '.clear-image i', function () {
        $("#editChecklistIcon").removeAttr('title');
        $("#editChecklistIcon")[0].value = ""; // DELETE FILE
        $('#oldImageName').val() != '' ? $('#deletedImage').val(true) : null; // FILE DELETED = TRUE
        $(this).closest('.file-field').find('.file-path').removeClass('valid').val(''); // REMOVE INPUT STYLE
        $('.preview-image-container').html('<i class="material-icons preview-image-not-available">image</i>'); // REMOVE IMAGE PREVIEW
        $("#editChecklistForm").validate().element("#editChecklistIcon"); // VALIDATE THE FILE
    });

    //// CHECK TYPES:
    //$('#CheckTypes').select2({ minimumResultsForSearch: -1 });
    //$('#CheckTypes').on('change', function () {
    //    var thisValue = $(this).val().toString().toLowerCase();
    //    if (thisValue === "") {
    //        $('#checksSelectDropdown').find('option').each(function (index, value) {
    //            $(value).attr('disabled', false);
    //        });
    //    } else {
    //        $('#checksSelectDropdown').find('option').each(function (index, value) {
    //            if (thisValue === $(value).data().checkType.toString().toLowerCase()) {
    //                $(value).attr('disabled', false);
    //            } else {
    //                $(value).attr('disabled', true);
    //            }
    //        });
    //    }

    //    $('#checksSelectDropdown').select2('destroy');
    //    $('#checksSelectDropdown').select2();
    //    $('#checksSelectDropdown').select2('open');
    //});

    // CHECKS DROPDOWN:

  
    $("#checksSelectDropdown").select2({
        placeholder: 'Select Checks...'
    });
    //$("#checksSelectDropdown").on('change', function () {
    //    $("#createNewChecklistForm").validate().element("#checksSelectDropdown"); // VALIDATE THE CHECKS
    //});
});

// VALIDATION: FILE EXTENSION
$(function () {
    $.validator.unobtrusive.adapters.add('filetype', ['validtypes'], function (options) {
        options.rules['filetype'] = { validtypes: options.params.validtypes.split(',') };
        options.messages['filetype'] = options.message;
    });

    $.validator.addMethod("filetype", function (value, element, param) {
        for (var i = 0; i < element.files.length; i++) {
            var extension = getFileExtension(element.files[i].name);
            if ($.inArray(extension, param.validtypes) === -1) {
                return false;
            }
        }
        return true;
    });

    function getFileExtension(fileName) {
        if (/[.]/.exec(fileName)) {
            return /[^.]+$/.exec(fileName)[0].toLowerCase();
        }
        return null;
    }
});

// VALIDATION: FILE SIZE
$(function () {
    $.validator.unobtrusive.adapters.add('filesize', ['maxbytes'], function (options) {
        // Set up test parameters
        var params = {
            maxbytes: options.params.maxbytes
        };

        // Match parameters to the method to execute
        options.rules['filesize'] = params;
        if (options.message) {
            // If there is a message, set it for the rule
            options.messages['filesize'] = options.message;
        }
    });

    $.validator.addMethod("filesize", function (value, element, param) {
        if (value === "") {
            // no file supplied
            return true;
        }

        var maxBytes = parseInt(param.maxbytes);

        // use HTML5 File API to check selected file size
        // https://developer.mozilla.org/en-US/docs/Using_files_from_web_applications
        // http://caniuse.com/#feat=fileapi
        if (element.files != undefined && element.files[0] != undefined && element.files[0].size != undefined) {
            var filesize = parseInt(element.files[0].size);

            return filesize <= maxBytes;
        }

        // if the browser doesn't support the HTML5 file API, just return true
        // since returning false would prevent submitting the form 
        return true;
    });
});