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

    // CREATE
    $("#createNewCheckImage").change(function () {
        readURL(this); // PREVIEW IMAGE
        $("#createNewCheckForm").validate().element("#createNewCheckImage"); // VALIDATE THE FILE
    });
    $('#createNewCheckForm').on('click', '.clear-image i', function () {
        $("#createNewCheckImage")[0].value = ""; // DELETE FILE
        $(this).closest('.file-field').find('.file-path').removeClass('valid').val(''); // REMOVE INPUT STYLE
        $('.preview-image-container').html('<i class="material-icons preview-image-not-available">image</i>'); // REMOVE IMAGE PREVIEW
        $("#createNewCheckForm").validate().element("#createNewCheckImage"); // VALIDATE THE FILE
    });

    // EDIT
    $("#editCheckImage").change(function () {
        $("#editNewCheckForm").validate().element("#editCheckImage"); // VALIDATE THE FILE
        $("#editCheckImage").removeAttr('title');
        readURL(this); // PREVIEW IMAGE
        $('#oldImageName').val() != '' ? $('#deletedImage').val(true) : null; // FILE DELETED = TRUE
    });
    $('#editNewCheckForm').on('click', '.clear-image i', function () {
        $("#editCheckImage").removeAttr('title');
        $("#editCheckImage")[0].value = ""; // DELETE FILE
        $('#oldImageName').val() != '' ? $('#deletedImage').val(true) : null; // FILE DELETED = TRUE
        $(this).closest('.file-field').find('.file-path').removeClass('valid').val(''); // REMOVE INPUT STYLE
        $('.preview-image-container').html('<i class="material-icons preview-image-not-available">image</i>'); // REMOVE IMAGE PREVIEW
        $("#editNewCheckForm").validate().element("#editCheckImage"); // VALIDATE THE FILE
    });

    // TITLE VALIDATION
    //$('#CheckTitle').on('paste', function () {
    //    $("form").validate().element("#CheckTitle"); // VALIDATE THE TITLE
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