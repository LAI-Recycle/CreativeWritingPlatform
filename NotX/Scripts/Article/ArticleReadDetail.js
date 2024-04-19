//喜歡
function Choose_Favorite_onChange(Choose_ArticleId) {
    UpdateFavoriteDetail(Choose_ArticleId)
}

function UpdateFavoriteDetail(Choose_ArticleId) {
    $.ajax({
        url: "/Article/AddFavoriteNumber",
        type: "POST",
        data: {
            Choose_ArticleId: Choose_ArticleId,
        },
        dataType: "json",
        async: false,
        success: function () {
            window.location.href = '/Article/ArticleReadDetail?Choose_ArticleId=' + Choose_ArticleId + '&ActionType=create'
        },
        error: function (jqXHR, textStatus, errorThrown) {
            _AddJsErrMessage("");
            _ShowJsErrMessageBox();
        }
    });
}

//收藏
function Choose_Collect_onChange(Choose_ArticleId) {
    var ActionType = "add";
    AddCollectDetail(Choose_ArticleId, ActionType)
}

function AddCollectDetail(Choose_ArticleId, ActionType) {
    $.ajax({
        url: "/Collect/CollectList",
        type: "POST",
        data: {
            ActionType: ActionType,
            Choose_CollectArticleId: Choose_ArticleId,
        },
        dataType: "json",
        async: false,
        success: function () {
            window.location.href = '/Article/ArticleReadDetail?Choose_ArticleId=' + Choose_ArticleId + '&ActionType=create'
        },
        error: function (jqXHR, textStatus, errorThrown) {
            _AddJsErrMessage("");
            _ShowJsErrMessageBox();
        }
    });
}

//不收藏
function Choose_unCollect_onChange( Choose_ArticleId) {
    var ActionType = "delete";
    UpdateCollectDetail( Choose_ArticleId, ActionType)
}

function UpdateCollectDetail( Choose_ArticleId, ActionType) {
    $.ajax({
        url: "/Collect/CollectList",
        type: "POST",
        data: {
            ActionType: ActionType,
            Choose_CollectArticleId: Choose_ArticleId,
        },
        dataType: "json",
        async: false,
        success: function () {
            window.location.href = '/Article/ArticleReadDetail?Choose_ArticleId=' + Choose_ArticleId + '&ActionType=create'
        },
        error: function (jqXHR, textStatus, errorThrown) {
            _AddJsErrMessage("");
            _ShowJsErrMessageBox();
        }
    });
}