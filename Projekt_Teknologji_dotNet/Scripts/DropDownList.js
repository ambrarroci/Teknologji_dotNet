﻿$(document).ready(function () {
    $(function () {
        $('.main li').hover(
            function () {
                $('ul.sub', this).slideDown(500);
            },
            function () {
                $('ul.sub', this).slideUp(400);
            }
        )
    });
});