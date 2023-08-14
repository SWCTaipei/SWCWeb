var locaHref = location.href;
var locaBody = document.querySelector('body');
var newDom = document.createElement("div");

newDom.setAttribute('class', 'newDom');
newDom.innerHTML = '測試機';
if (locaHref.indexOf('211.') !== -1) {
    locaBody.appendChild(newDom);
}

$(function(){
    var $li = $('ul.tab-title li');
        $($li. eq(0) .addClass('active').find('a').attr('href')).siblings('.tab-inner').hide();
    
        $li.click(function(){
            $($(this).find('a'). attr ('href')).show().siblings ('.tab-inner').hide();
            $(this).addClass('active'). siblings ('.active').removeClass('active');
        });
    });