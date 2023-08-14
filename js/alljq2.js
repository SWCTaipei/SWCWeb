$(document).ready(function () {
  $(".te_input").click(function () {
    $(this).parent().parent().find(".te_color_all").addClass("active");
    $(this).parent().parent().find(".bu_color_all").removeClass("active");
    $(this).parent().parent().parent().find(".wifi_te").slideDown();
    $(this).parent().parent().parent().find(".wifi_bu").css("display", "none");
  });
  $(".bu_input").click(function () {
    $(this).parent().parent().find(".bu_color_all").addClass("active");
    $(this).parent().parent().find(".te_color_all").removeClass("active");
    $(this).parent().parent().parent().find(".wifi_bu").slideDown();
    $(this).parent().parent().parent().find(".wifi_te").css("display", "none");
  });

  $(".wifi_title h1").click(function () {
    $(this).addClass("active");
    $(this).siblings().removeClass("active");
  });

  $(".wifi_title_yes").click(function () {
    $(this).parent().parent().find(".wifi_yes").css("display", "flex");
    $(this).parent().parent().find(".wifi_no").hide();
  });
  $(".wifi_title_no").click(function () {
    $(this).parent().parent().find(".wifi_no").css("display", "flex");
    $(this).parent().parent().find(".wifi_yes").hide();
  });
});

// $(document).ready(function () {
//   $(".choose_input").click(function () {
//     $(this).parents().find(".choose_all").toggleClass("active");
//   });
// });
