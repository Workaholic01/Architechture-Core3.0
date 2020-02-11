var WT = WT || {};

WT.Common = {
    rootURL: "",
    ViewId: "",
    InvalidDate: "01/01/1753",
    openFileURL: "",
    OpenFile: function () {
        var URL = 'Handlers/OpenFileHandler.ashx?filename=' + encodeURIComponent(WT.Common.openFileURL);
        window.open(URL);
    },
};
$(function () {
    $.fn.scrollView = function () {
        return this.each(function () {
            $('html, body').animate({
                scrollTop: $(this).offset().top
            }, 300);
        });
    }

    Number.prototype.padLeft = function (n, str) {
        return Array(n - String(this).length + 1).join(str || '0') + this;
    }
    if (!ace.vars['touch']) {
        $('.chosen-select').chosen({
            allow_single_deselect: true,
            search_contains: true
        });
        //resize the chosen on window resize
        $(window)
            .off('resize.chosen')
            .on('resize.chosen', function () {
                $('.chosen-select').each(function () {
                    var $this = $(this);
                    $this.next().css({ 'width': $this.parent().width() });
                })
            }).trigger('resize.chosen');
        //resize chosen on sidebar collapse/expand
        $(document).on('settings.ace.chosen', function (e, event_name, event_val) {
            if (event_name != 'sidebar_collapsed') return;
            $('.chosen-select').each(function () {
                var $this = $(this);
                $this.next().css({ 'width': $this.parent().width() });
            })
        });
    }
});