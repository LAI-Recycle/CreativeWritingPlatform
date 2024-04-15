function ChooseOrderCondition_onChange(selectElement)
{
    var ChooseOrderCondition = selectElement.value;
    window.location.href = "/Article/ArticleList?ChooseOrderCondition=" + ChooseOrderCondition;
}