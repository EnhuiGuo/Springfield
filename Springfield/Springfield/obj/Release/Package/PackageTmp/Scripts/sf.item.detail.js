$(document).ready(function () {

	var imagesVM = new Vue({
		el: '#imagesPanel',
		data: {
            images: "",
        },
        methods: {
            deleteImage: function (id, index) {
                 $.confirm({
                    title: "要删除么？",
                    content: "删除",
                    buttons: {
                        confirm: function () {
                            $.ajax({
                                type: "POST",
                                url: "/Item/DeleteImage",
                                data: { "id": id },
                                success: function (result) {
                                    if (result === "true") {
                                        imagesVM._data.images.splice(index, 1);
                                    }
                                }
                            });
                        },
                        cancel: function () {

                        },
                    }
                });
            },
        }
	});

	Dropzone.options.dropzoneForm = {
		acceptedFiles: ".jpeg,.jpg,.png,.gif",
		dictDefaultMessage: "请把照片拖到这里或者单击上传!",
		init: function () {
            this.on("success", function (file, response) {
                imagesVM._data.images.push(response);
                console.log(response);
			});

			this.on("error", function (file, response) {
				$(file.previewElement).find('.dz-error-message').text("文件只能是照片格式，谢谢");
			});

			this.on("complete", function (file) {
				setTimeout(function () {
					Dropzone.forElement("#dropzoneForm").removeFile(file);
				}, 10000)
			});
		}
	};

    getItemImages();

    //var slideIndex = 1;
    //showSlides(slideIndex);

    //plusSlides = function (n) {
    //    showSlides(slideIndex += n);
    //}

	function getItemImages()
	{
	    $.ajax({
	        type: "GET",
	        url: "/Item/GetItemImages",
	        data: { "itemId": itemId },
            success: function (result) {
                imagesVM._data.images = result;
	        }
	    });
    }

    //function showSlides(n) {
    //    var i;
    //    var slides = $('.image-slides');

    //    console.log(slides);

    //    var dots = $(".image-trumb");
    //    if (n > slides.length) { slideIndex = 1 }
    //    if (n < 1) { slideIndex = slides.length }
    //    for (i = 0; i < slides.length; i++) {
    //        slides[i].style.display = "none";
    //    }
    //    for (i = 0; i < dots.length; i++) {
    //        dots[i].className = dots[i].className.replace(" active", "");
    //    }
    //    slides[slideIndex - 1].style.display = "block";
    //    dots[slideIndex - 1].className += " active";
    //}
});