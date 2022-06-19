window.confirm = function (_text, _title, _yesAction = function () { }) {
    swal.fire({
        title: _title,
        text: _text,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6',
        confirmButtonText: _yesText,
        cancelButtonText: _noText
    }).then((result) => {
        if (result.isConfirmed) {
            _yesAction();
        }
    })
}

function ShowLoader() {
    $('.ip-loader').fadeIn(100);
}

function HideLoader() {
    $('.ip-loader').fadeOut(100);
}

function ForgeryToken() {
    return kendo.antiForgeryTokens()
}

function SendForm(_Url, _FormId, _CallBack = function (res) { }) {
    var form = $('#' + _FormId)[0];
    var _formData = new FormData(form);

    $.ajax({
        type: "post",
        enctype: "multipart/form-data",
        url: _Url,
        data: _formData,
        processData: false,
        contentType: false,
        cache: false,
        timeout: 600000,
        beforeSend: function (xhr) {
            ShowLoader();
            var _token = $("[name=__RequestVerificationToken]").val();
            xhr.setRequestHeader('XSRF-TOKEN', _token);
        },
        success: function (response) {
            _CallBack(response)
        },
        complete: function (data) {
            HideLoader();
        },
        error: function (err) {
            if (err.status == 500) {
                alert("Error 500");
                return;
            }
            //else {
            //    alert(err.status);
            //}

        }
    });
}

function sendData(_Url, _data, _CallBack = function (res) { }) {
    $.ajax({
        type: "post",
        enctype: "multipart/form-data",
        url: _Url,
        data: _data,
        timeout: 600000,
        beforeSend: function (xhr) {
            ShowLoader();
            var _token = $("[name=__RequestVerificationToken]").val();
            xhr.setRequestHeader('XSRF-TOKEN', _token);
        },
        success: function (response) {
            _CallBack(response)
        },
        complete: function (data) {
            HideLoader();
        },
        error: function (err) {
            if (err.status == 500) {
                alert("Error 500");
                return;
            }
            //else {
            //    alert(err.status);
            //}

        }
    });
}



function removeData(_url, _data = {}) {
    confirm(_deleteMsg, "", function (){
        sendData(_url,_data)
    });
}

