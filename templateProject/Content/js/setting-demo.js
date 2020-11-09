"use strict";

localStorage.setItem("sample", "test");
$(document).ready(function () {
    // Setting Color
    var temp_LogoHeader = localStorage.getItem("temp_LogoHeader");
    var temp_NavbarHeader = localStorage.getItem("temp_NavbarHeader");
    var temp_Sidebar = localStorage.getItem("temp_Sidebar");
    var temp_Background = localStorage.getItem("temp_Background");

    if (temp_LogoHeader != null && temp_LogoHeader != "") {
        var element = document.getElementById("logo-header");
        element.setAttribute("data-background-color", temp_LogoHeader);
    }

    if (temp_NavbarHeader != null && temp_NavbarHeader != "") {
        var element = document.getElementById("navbar-header");
        element.setAttribute("data-background-color", temp_NavbarHeader);
    }

    if (temp_Sidebar != null && temp_Sidebar != "") {
        var element = document.getElementById("sidebar");
        element.setAttribute("data-background-color", temp_Sidebar);
    }

    if (temp_Background != null && temp_Background != "") {
        var element = document.getElementById("body");
        element.setAttribute("data-background-color", temp_Background);
    }

$(window).resize(function () {
    $(window).width();
});

$('.changeBodyBackgroundFullColor').on('click', function () {
    if ($(this).attr('data-color') == 'default') {
        $('body').removeAttr('data-background-full');
    } else {
        $('body').attr('data-background-full', $(this).attr('data-color'));
    }

    $(this).parent().find('.changeBodyBackgroundFullColor').removeClass("selected");
    $(this).addClass("selected");
    layoutsColors();
});

$('.changeLogoHeaderColor').on('click', function () {
    if ($(this).attr('data-color') == 'default') {
        $('.logo-header').removeAttr('data-background-color');
    } else {
        $('.logo-header').attr('data-background-color', $(this).attr('data-color'));
    }

    $(this).parent().find('.changeLogoHeaderColor').removeClass("selected");
    $(this).addClass("selected");
    customCheckColor();
    layoutsColors();
    localStorage.setItem("temp_LogoHeader", $(this).attr('data-color'));
});

$('.changeTopBarColor').on('click', function () {
    if ($(this).attr('data-color') == 'default') {
        $('.main-header .navbar-header').removeAttr('data-background-color');
    } else {
        $('.main-header .navbar-header').attr('data-background-color', $(this).attr('data-color'));
    }

    $(this).parent().find('.changeTopBarColor').removeClass("selected");
    $(this).addClass("selected");
    layoutsColors();
    localStorage.setItem("temp_NavbarHeader", $(this).attr('data-color'));
});

$('.changeSideBarColor').on('click', function () {
    if ($(this).attr('data-color') == 'default') {
        $('.sidebar').removeAttr('data-background-color');
    } else {
        $('.sidebar').attr('data-background-color', $(this).attr('data-color'));
    }

    $(this).parent().find('.changeSideBarColor').removeClass("selected");
    $(this).addClass("selected");
    layoutsColors();
    localStorage.setItem("temp_Sidebar", $(this).attr('data-color'));
});

$('.changeBackgroundColor').on('click', function () {
    $('body').removeAttr('data-background-color');
    $('body').attr('data-background-color', $(this).attr('data-color'));
    $(this).parent().find('.changeBackgroundColor').removeClass("selected");
    $(this).addClass("selected");
    localStorage.setItem("temp_Background", $(this).attr('data-color'));
});

function customCheckColor() {
    var logoHeader = $('.logo-header').attr('data-background-color');
    if (logoHeader !== "white") {
        $('.logo-header .navbar-brand').attr('src', '../../assets/img/logo.svg');
    } else {
        $('.logo-header .navbar-brand').attr('src', '../../assets/img/logo2.svg');
    }
}


var toggle_customSidebar = false,
custom_open = 0;
    if (!toggle_customSidebar) {
        var toggle = $('.custom-template .custom-toggle');

        toggle.on('click', (function () {
            if (custom_open == 1) {
                $('.custom-template').removeClass('open');
                toggle.removeClass('toggled');
                custom_open = 0;
            } else {
                $('.custom-template').addClass('open');
                toggle.addClass('toggled');
                custom_open = 1;
            }
        })
        );
        toggle_customSidebar = true;
    }
})
