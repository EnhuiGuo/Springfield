﻿$(document).ready(function () {

    openModal = function () {
        $('#imageModel').css("display", "block");
    }

    closeModal = function () {
        $('#imageModel').css("display", "none");
    }

    var slideIndex = 1;
    showSlides(slideIndex);

    plusSlides = function (n) {
        showSlides(slideIndex += n);
    }

    currentSlide = function (n) {
        showSlides(slideIndex = n);
    }

    function showSlides(n) {
        var i;
        var slides = $('.image-slides');

        console.log(slides);

        var dots = $(".image-trumb");
        if (n > slides.length) { slideIndex = 1 }
        if (n < 1) { slideIndex = slides.length }
        for (i = 0; i < slides.length; i++) {
            slides[i].style.display = "none";
        }
        for (i = 0; i < dots.length; i++) {
            dots[i].className = dots[i].className.replace(" active", "");
        }
        slides[slideIndex - 1].style.display = "block";
        dots[slideIndex - 1].className += " active";
    }
});