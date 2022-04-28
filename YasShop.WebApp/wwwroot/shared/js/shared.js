function ShowLoader() {
    $('.ip-loader').fadeIn(100);
}

function HideLoader() {
    $('.ip-loader').fadeOut(100);
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
function Test() {
    Swal.fire({
        title: 'Test',
        html: 'Html',
        icon: 'info',
        confirmButtonText: 'OK',
    }).then((result) => {
        if (result.isConfirmed) {
            alert("OK")
        }
    });
}