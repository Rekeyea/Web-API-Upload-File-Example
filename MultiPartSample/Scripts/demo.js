$(document).ready(function () {
    $("#btnUploadMultipart").on("click", function () {
        openFileDialogAndProccessResponse(sendFileMultipart);
    });
    $("#btnUploadBSON").on("click", function () {
        openFileDialogAndProccessResponse(sendFileBSON);
    });

    function openFileDialogAndProccessResponse(callback) {
        var input =
            $("<input type='file' accept='.png'/>")
            .appendTo("body")
            .css("visibility", "hidden")
            .on("change", function (evt) {
                //get the file info
                var file = evt.target.files[0];//here it's 0 because only one file is allowed
                //call callback with file info and let each function use the API as it needs
                callback(file);
                //in production it may be desirable to remove the input from de DOM
            });
        input.click();
    }

    function sendFileMultipart(file) {
        //create form data to submit to server 
        var formData = new FormData();
        //add actual data
        formData.append("file", file);
        $.ajax({
            url: "/api/file/multipart",
            data: formData,
            cache: false,
            contentType: false,
            processData: false,
            type: "POST",
            success: function (url) {
                $("#fileUploaded").attr("src", url);
            }
        });
    }

    function sendFileBSON(file) {
        var reader = new FileReader();
        
        reader.readAsArrayBuffer(file);
        reader.onload = function (evt) {
            var data = reader.result;
            var a = new Uint8Array(data);
            $.ajax({
                url: "/api/file/bson",
                data: a,
                contentType: false,
                processData: false,
                type: "POST",
                success: function (url) {
                    $("#fileUploaded").attr("src", url);
                }
            });
        };
    }

});