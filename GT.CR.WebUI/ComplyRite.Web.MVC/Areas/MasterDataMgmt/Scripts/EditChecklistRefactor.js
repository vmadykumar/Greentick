$(function () {

    if ($('#PreSelectedLocation').val() != "") {
        $.ajax({
            url: ' /ChecklistManagement/GetAreaByUUIDAndLocationCode',
            type: 'GET',
            beforeSend: function () {
                $('.ajax-loader-new').css("visibility", "visible");
            },

            data: {
                UUID: $('#UUID').val(), LocationCode: $('#PreSelectedLocation').val()
            },
            success: function (areas) {
                let list = JSON.parse(areas);
                $('#areaSelectDropdown').empty().removeAttr('disabled');
                $('#areaSelectDropdown').prepend("<option value=" + '' + ">" + 'Select Area' + "</option>");
                list.forEach(function (area, index) {
                    $('#areaSelectDropdown').append("<option value=" + area.AreaCode + ">" + area.AreaName + "</option>");
                });

                $('#areaSelectDropdown').val($('#PreSelectedArea').val());

                $.ajax({
                    url: ' /ChecklistManagement/GetSubAreaByUUIDLocationCodeAndAreaCode',
                    type: 'GET',
                    data: {
                        UUID: $('#UUID').val(), LocationCode: $('#PreSelectedLocation').val(), AreaCode: $('#PreSelectedArea').val()
                    },
                    complete: function () {
                        $('.ajax-loader-new').css("visibility", "hidden");
                    },

                    success: function (subAreas) {
                        let list = JSON.parse(subAreas);
                        $('#subAreaSelectDropdown').empty().removeAttr('disabled');
                        $('#subAreaSelectDropdown').prepend("<option value=" + '' + ">" + 'Select SubArea' + "</option>");
                        list.forEach(function (subArea, index) {
                            $('#subAreaSelectDropdown').append("<option value=" + subArea.SubAreaCode + ">" + subArea.SubAreaName + "</option>");
                        });

                        $('#subAreaSelectDropdown').val($('#PreSelectedSubArea').val());                 


                    }
                });
            }

        });




    } else {

        $("#areaSelectDropdown").empty();
        $("#subAreaSelectDropdown").empty();
     }

});
