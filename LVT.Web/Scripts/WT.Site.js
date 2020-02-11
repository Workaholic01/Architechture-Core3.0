var WT = WT || {};
WT.Site = {
    handlerURL: "Handlers/SiteHandler.ashx",
    InvalidDate: "01/01/1753",
    isPostback: false,
    SetControls: function () {
        $('.scrollable').each(function () {
            var $this = $(this);
            $(this).ace_scroll({
                size: $this.attr('data-size') || 120
            });
        });
    }
};

$(document).ready(function () {
    WT.Site.SetControls();
});