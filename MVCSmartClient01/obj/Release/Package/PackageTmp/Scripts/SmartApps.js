$('.deleteItem').click(function (e) {
    DoDelete(e);
});

function RemoveMe(elm, divControl) {
    document.getElementById(divControl).style.display = "none";
    $(elm).remove();
    var $el = $('#fileInput');
    $el.val('');
}

function DisplayMe1(elm) {
    myModal1.style.display = "block";
}

function DisplayMe2(elm) {
    myModal2.style.display = "block";
}

function DisplayMe3(elm) {
    myModal3.style.display = "block";
}

function DisplayMe4(elm) {
    myModal4.style.display = "block";
}

function DisplayMe5(elm) {
    myModal5.style.display = "block";
}

function DisplayMe6(elm) {
    myModal6.style.display = "block";
}

function CloseMe1() {
    myModal1.style.display = "none";
}

function CloseMe2() {
    myModal2.style.display = "none";
}

function CloseMe3() {
    myModal3.style.display = "none";
}

function CloseMe4() {
    myModal4.style.display = "none";
}

function CloseMe5() {
    myModal5.style.display = "none";
}

function CloseMe6() {
    myModal6.style.display = "none";
}

function openNav() {
    document.getElementById("mySidenav").style.width = "250px";
}

/* Set the width of the side navigation to 0 */
function closeNav() {
    document.getElementById("mySidenav").style.width = "0";
}

var progressBarStart = function () {
    $("#progressbar_container").show();
}

var progressBarUpdate = function (percentage) {
    $('#progressbar_label').html(percentage + "%");
    $("#progressbar").width(percentage + "%");
}

var progressBarComplete = function () {
    $("#progressbar_container").fadeOut(500);
}

var file;

$('#fileInput').change(function (e) {
    file = e.target.files[0];
});


var DoUpload = function (myFileName, divControl, delControl, NomorKontrol)
{
    debugger;
    var myFileExt = (file.name.match(/[^\\\/]\.([^.\\\/]+)$/) || [null]).pop();
    var myFile = myFileName + "." + myFileExt;
    var blob = file;
    var bytesPerChunk = 3757000;
    var size = blob.size;

    var start = 0;
    var end = bytesPerChunk;
    var completed = 0;
    var count = size % bytesPerChunk == 0 ? size / bytesPerChunk : Math.floor(size / bytesPerChunk) + 1;
    var counter = 0;
    progressBarStart();
    multiUpload(count, counter, blob, completed, start, end, bytesPerChunk, myFile, divControl, delControl, NomorKontrol);
}

var multiUpload = function (count, counter, blob, completed, start, end, bytesPerChunk, myFile, divControl, delControl, NomorKontrol) {
    counter = counter + 1;
    if (counter <= count) {
        var chunk = blob.slice(start, end);
        var xhr = new XMLHttpRequest();
        xhr.onload = function () {
            start = end;
            end = start + bytesPerChunk;
            if (count == counter) {
                uploadCompleted(myFile, divControl, delControl, NomorKontrol);
            } else {
                var percentage = (counter / count) * 100;
                progressBarUpdate(percentage);
                multiUpload(count, counter, blob, completed, start, end, bytesPerChunk, myFile, divControl, delControl, NomorKontrol);
            }
        }
        xhr.open("POST", "/UploadControlHelperNew/MultiUpload?id=" + counter.toString() + "&fileNameOri=" + file.name + "&myFileName=" + myFile, true);
        xhr.send(chunk);
    }
}

var DoDelete= function (e) {
    //e.preventDefault();
    alert($(this).data('data-id'));
    var $ctrl = $(this);
    if (confirm('Anda yakin menghapus lampiran ?')) {
        $.ajax({
            url: 'DeleteFile',
            type: 'POST',
            data: { id: $(this).data('id') }
        }).done(function (data) {
            if (data.Result == "OK") {
                $('.target').load('/TrxDataOrganisasi/_DaftarFile');
            }
            else if (data.Result.Message) {
                alert(data.Result.Message);
            }
        }).fail(function () {
            alert("There is something wrong. Please try again.");
        })
    }
};

var uploadCompleted = function (myFile, divControl, delControl, NomorKontrol) {
    var formData = new FormData();
    formData.append('fileName', myFile);
    formData.append('completed', true);

    var xhr2 = new XMLHttpRequest();
    xhr2.onload = function () {
        progressBarUpdate(100);
        progressBarComplete();
    }
    xhr2.open("POST", "/UploadControlHelperNew/UploadComplete?fileName=" + myFile + "&complete=" + 1, true);
    xhr2.send(formData);

    $('#' + divControl).html(
        '<a href="javascript:void(0);" id="' + divControl + '" onclick="DisplayMe112233(this)">File yang diupload</a>'.replace('112233', NomorKontrol));
    $('#' + delControl).html(
        '<a href="javascript:void(0);" id="' + delControl + '" onclick="RemoveMeLocal112233(this)">X</a>'.replace('112233', NomorKontrol));
    $('#' + divControl).show();
    $('#' + delControl).show();

    var modalStr = '<div class="modal-content"><span class="close" onclick="CloseMe112233()">&times;</span><object width=80% height=600 data="/Content/DocumentImages/AABBCCDDEEFFGGHHIIJJKK" class="mdlcontain"></object></div>'.replace('112233', NomorKontrol);
    var modalStrNew = modalStr.replace("AABBCCDDEEFFGGHHIIJJKK", myFile + "?" + new Date().getTime());
    var myModalStr = '#MyModal112233'.replace('112233', NomorKontrol);
    $(myModalStr).html(modalStrNew);
    alert('Upload berhasil !')
}

