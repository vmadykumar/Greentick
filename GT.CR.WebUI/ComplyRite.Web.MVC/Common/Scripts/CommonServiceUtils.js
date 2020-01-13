 /*********************************/
    //Section: Service Utilities 
    //Created by: Sharath K M    
    //Created on: August 23rd 2018    
 /*********************************/

/*------------- Creating service repository class that handles all the ajax and data calls ------------*/ 
var ServiceRepository = function () { };

/*----------------- GET Data From Service Method---------------------------*/
ServiceRepository.prototype.GetDataFromService = function (url) {
    var deferred;
    deferred = deferred || $.Deferred();
    $.ajax({
        type: 'GET',
        url: url,
        cache: false,
        processData: false,
        contentType: false,
        success: function (result) {
            deferred.resolve(result);
        },
        error: function (error) {
            deferred.reject(error);
        }
    });
    return deferred.promise();
}

/*----------------- POST Data To Service Method ---------------------------*/
ServiceRepository.prototype.PostDataToService = function (url, data) {
    var deferred;
    deferred = deferred || $.Deferred();
    $.ajax({
        type: 'POST',
        url: url,
        data: data,
        processData: false,
        contentType: false,
        dataType: "html",
        success: function (result) {
            deferred.resolve(result);
        },
        error: function (error) {
            deferred.reject(error);
        }
    });
    return deferred.promise();
}

/*------------------- GET JSON Data From Service Method-------------------------*/
ServiceRepository.prototype.GETJSONDataToService = function (url, data) {
    var deferred;
    deferred = deferred || $.Deferred();
    $.ajax({
        type: 'GET',
        url: url,
        data: data,
        cache: false,
        processData: false,
        contentType: "application/json",
        dataType: "html",
        success: function (result) {
            deferred.resolve(result);
        },
        error: function (error) {
            deferred.reject(error);
        }
    });
    return deferred.promise();
}

/*----------------- POST JSON Data To Service Method----------------------------*/
ServiceRepository.prototype.PostJSONDataToService = function (url, data) {
    var deferred;
    deferred = deferred || $.Deferred();
    $.ajax({
        type: 'POST',
        url: url,
        data: JSON.stringify(data),
        processData: false,
        contentType: "application/json",
        dataType: "html",
        success: function (result) {
            deferred.resolve(result);
        },
        error: function (error) {
            deferred.reject(error);
        }
    });
    return deferred.promise();
}

/*---------------- Get Data Using FormId ----------------------------*/
ServiceRepository.prototype.GetFormDataByFormId = function (formID) {
    try {
        var fd = new FormData();
        if (!($("#" + formID))) {
            throw "Form Id does not exist";
        }
        var data = $("#" + formID).serializeArray();
        $.each(data, function (key, input) {
            fd.append(input.name, input.value);
        });
        return fd;
    } catch (err) {
        console.log(err);
    }
}
