/*  Soil and Water Conservation Platform Project is a web applicant tracking system which allows citizen can search, view and manage their SWC applicant case.
    Copyright (C) <2020>  <Geotechnical Engineering Office, Public Works Department, Taipei City Government>

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License as
    published by the Free Software Foundation, either version 3 of the
    License, or any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Affero General Public License for more details.

    You should have received a copy of the GNU Affero General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

$(document).ready(function() {
    // OC subMenu
    $('.open').click(function(event) {
        $(this).parent().next('div').slideToggle();
        // $(this).parent().parent().find('.detailsBar_tb_wrap').slideToggle();

        if ($(this).attr('src') == 'img/btn-open.png') {
            $(this).attr('src', 'img/btn-close.png');
            // console.log('第2次，' + $(this).attr('src'));
        } else {
            $(this).attr('src', 'img/btn-open.png');
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

