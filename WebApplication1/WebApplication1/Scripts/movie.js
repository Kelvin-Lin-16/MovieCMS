$(function () {
    var submitAutoCOmpleteForm = function (event, ui) {
        var $input = $(this);
        $input.val(ui.item.label);
        var $form = $input.parents("form:first");
        $form.submit();
    }
    var createAutoComplete = function () {
        var $input = $(this);
        var options = {
            select: submitAutoCOmpleteForm,
            source: $input.attr("data-movies-autocomplete")
        }
        $input.autocomplete(options);
    }
    $("input[data-movies-autocomplete]").each(createAutoComplete);


    var getPage = function () {
        var $a = $(this);
        var options = {
            url: $a.attr("href"),
            data: $("form").serialize(),
            type:"get"
        };
        $.ajax(options).done(function (data) {
            var target = $a.parents("div.pagedList").attr("data-movies-target");
            $(target).replaceWith(data);
        });
        return false;
    }
    $(".body-content").on("click",".pagedList a",getPage);
});