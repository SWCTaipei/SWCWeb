$(document).ready(function() {
    // OC subMenu
    $('.open').click(function(event) {
        $(this).parent().next('div').slideToggle();
        // $(this).parent().parent().find('.detailsBar_tb_wrap').slideToggle();

        if ($(this).attr('src') == '../images/btn/btn-open.png') {
            $(this).attr('src', '../images/btn/btn-close.png');
            // console.log('第2次，' + $(this).attr('src'));
        } else {
            $(this).attr('src', '../images/btn/btn-open.png');
            // console.log('第3次，' + $(this).attr('src'));
        }
    });
    // console.log('第1次，' + $('.open').attr('src'));

    // OC leftMenu - ctrl
    $('.menubar-open , .menubar-close').click(function(event) {
        event.preventDefault();
        $('.wrapmask').toggleClass('wrapmask-show');
        $('.leftmenu').toggleClass('leftmenu-move');
    });

    $('.detailsBar_small_600_addDiv').parent('div').addClass('detailsBar_small_600_div');
});

