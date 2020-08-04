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
  $('.detailsGrid_wrap').hide();
  $('.detailsGrid_wrap').eq(0).show();
  $(
    ".openh2 img, #Area04>h2, #Area05>h2, #Area01>h2, #Area02>h2, #Area03>h2"
  )
  .click(function(event) {
    $(this)
	  .parent()
      .next("div")
      .slideToggle();
    if (
      $(this)
        .children("img")
        .attr("src") == "img/btn-open.png"
    ) {
      $(this)
        .children("img")
        .attr("src", "img/btn-close.png");
    } else {
      $(this)
        .children("img")
        .attr("src", "img/btn-open.png");
    }
  });
  $(".menubar-open , .menubar-close").click(function(event) {
    event.preventDefault();
    $(".wrapmask").toggleClass("wrapmask-show");
    $(".leftmenu").toggleClass("leftmenu-move");
  });
  $(".cgPassBtn").click(function(event) {
    $(".cgPass").slideToggle();
    $(".cgPassn").toggleClass("cgPassnb");
  });
});

var newBR2 = document.createElement("br");
$("<br>").appendTo(document.querySelectorAll(".detailsGrid_orange_ctBR span"));

var imgSrc = document.querySelectorAll("td.applyGrid_imgadd img");
for (var i = 0; i < imgSrc.length; i++) {
  if ($(imgSrc[i]).attr("src") !== "") {
    $("<br>").insertAfter(imgSrc[i]);
    $("<br>").insertBefore(imgSrc[i]);
    $(imgSrc[i])
      .prev()
      .remove();
  } else {
    $("<p>").insertBefore(imgSrc[i]);
  }
}

var nameIpt = document.getElementById("TXTACTION").value;
var getPass = document.querySelector(".cgPass");
console.log(nameIpt);
if (nameIpt === "") {
  getPass.className = "cgPass";
  document.querySelector(
    ".cgPassn tr:first-child td:first-child"
  ).style.borderTop =
    "1px solid #000";
  document.querySelector(
    ".cgPassn tr:first-child td:last-child"
  ).style.borderTop =
    "1px solid #000";
}