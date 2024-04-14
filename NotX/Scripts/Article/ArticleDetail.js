function Choose_Cityname_onChange(Choose_ArticleId) {
    UpdateDetail(Choose_ArticleId)
}


function UpdateDetail(Choose_ArticleId) {
    $.ajax({
        url: "/Article/AddFavoriteNumber",
        type: "POST",
        data: {
            Choose_ArticleId: Choose_ArticleId
        },
        dataType: "json",
        async: false,
        success: function () {
            debugger;
            window.location.href = window.location.href;
        },
        error: function (jqXHR, textStatus, errorThrown) {
            debugger;
            _AddJsErrMessage("");
            _ShowJsErrMessageBox();
        }
    });
}