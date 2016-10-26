//var uploadComplete = function () {
//    var formData = new FormData();
//    formData.append('fileName', file.name);
//    formData.append('completed', true);

//    var xhr2 = new XMLHttpRequest();
//    xhr2.open("POST", "/Files/UploadComplete", true); //combine the chunks together
//    xhr2.send(formData);
//}

function upload(file, saveFileName) {
    var blob = file;
    var BYTES_PER_CHUNK = 77570; // sample chunk sizes.
    var SIZE = blob.size;

    //upload content
    var start = 0;
    var end = BYTES_PER_CHUNK;
    var completed = 0;
    var count = SIZE % BYTES_PER_CHUNK == 0 ? SIZE / BYTES_PER_CHUNK : Math.floor(SIZE / BYTES_PER_CHUNK) + 1;
    var iPercentage = 0;
    var displayPercentage = 0;
    $('#btnUploadGalleryImage').attr('disabled', 'disabled');
    while (start < SIZE) {
        var chunk = blob.slice(start, end);
        iPercentage = chunk.size / SIZE;
        iPercentage = parseInt(iPercentage * 100, 10);
        displayPercentage += iPercentage;
        $('#btnUploadGalleryImage').html('Uploading .... ' + displayPercentage + '%');
        var xhr = new XMLHttpRequest();
        xhr.onload = function () {
            completed = completed + 1;
            if (completed === count) {
                //uploadComplete();
                $('#btnUploadGalleryImage').removeAttr('disabled');
                $('#btnUploadGalleryImage').html('Upload');
            }
        };
        xhr.open("POST", "/Gallery/ProcessRequest?fileName=" + saveFileName, false);
        xhr.send(chunk);
        
        start = end;
        end = start + BYTES_PER_CHUNK;
        
    }
}