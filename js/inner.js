$(document).ready(function() {
    $('.openh2, #Area04>h2, #Area05>h2, #Area01>h2, #Area02>h2, #Area03>h2').click(function (event) {
        $(this).next('div').slideToggle();
        if ($(this).children('img').attr('src') == '../images/btn/btn-open.png') {
            $(this).children('img').attr('src', '../images/btn/btn-close.png');
        } else {
            $(this).children('img').attr('src', '../images/btn/btn-open.png');
        }
    });
    $('.menubar-open , .menubar-close').click(function(event) {
        event.preventDefault();
        $('.wrapmask').toggleClass('wrapmask-show');
        $('.leftmenu').toggleClass('leftmenu-move');
    });
    $('.cgPassBtn').click(function (event) {
        $('.cgPass').slideToggle();
        $('.cgPassn').toggleClass('cgPassnb');
    });
});

function openClass(aName, className) {
    var i, x, tablinks;
    x = document.getElementsByClassName("class");
    for (i = 0; i < x.length; i++) {
        x[i].style.display = "none";
    }
    tablinks = document.getElementsByClassName("tablink");
    for (i = 0; i < x.length; i++) {
        tablinks[i].classList.remove("activecolor");
    }
    document.getElementById(className).style.display = "block";
    //evt.currentTarget.classList.add("activecolor");
    mytest = document.getElementById(aName);
    mytest.classList.add("activecolor");
}

/*
var mybtn = document.getElementsByClassName("testbtn")[0];
mybtn.click();*/

/*�ӽг�*/
$(document).ready(function () {
    $('.navmenu > li > a').click(function (event) {
        //event.preventDefault();
        $(this).parent().siblings().find('ul').slideUp();/*���}�@�������@��*/
        $(this).parent().find('ul').slideToggle();
        $(this).next("ul").children("ol").show();

        //�s�W
        $(".navmenu ol").show();//�N�Ҧ�.cart���U��ol����
    });
});
/*�ӽг�*/


jQuery(function () {
    $(".flip").click(function () {
        $(".openlist").slideToggle(300);
    });
});
jQuery(function () {
    $(".flip2").click(function () {
        $(".openlist2").slideToggle(300);
    });
});


/*�^�쳻�ݦC*/
$(document).ready(function () {
    $('.go-top1').hide(0)

    $(window).scroll(function () {
        if ($(this).scrollTop() > 100) {
            $('.go-top1').fadeIn(200);
        } else {
            $('.go-top1').fadeOut(300);
        }
    });
    $('.go-top1').click(function () {
        event.preventDefault();

        $('html , body').animate({ scrollTop: 0 }, 500);
    });
});
/*�^�쳻�ݦC*/

var newBR2 = document.createElement('br');
$('<br>').appendTo(document.querySelectorAll('.detailsGrid_orange_ctBR span'));

var imgSrc = document.querySelectorAll('td.applyGrid_imgadd img');
for (var i = 0; i < imgSrc.length; i++) {
    if ($(imgSrc[i]).attr('src') !== '') {
        $('<br>').insertAfter(imgSrc[i]);
        $('<br>').insertBefore(imgSrc[i]);
        $(imgSrc[i]).prev().remove();
    } else {
        $('<p>').insertBefore(imgSrc[i]);
    }
}

var nameIpt = document.getElementById('TXTACTION').value;
var getPass = document.querySelector('.cgPass');
console.log(nameIpt);
if (nameIpt === '') {
    getPass.className = 'cgPass';
    document.querySelector('.cgPassn tr:first-child td:first-child').style.borderTop = '1px solid #000';
    document.querySelector('.cgPassn tr:first-child td:last-child').style.borderTop = '1px solid #000';
}