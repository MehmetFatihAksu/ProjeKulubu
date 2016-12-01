var idler = [];
function MultiDataDelete() {

    if ($("input[type=checkbox]:checked").length == 0) {
        alert("Lütfen silmek istediğiniz veriyi seçiniz...");
    } else {

        var elemCount = $("input[type=checkbox]:checked").length;

        for (var i = 0; i < elemCount; i++) {

            var elemId = $("input[type=checkbox]:checked")[i].valueOf();
            idler[i] = elemId;
        }

        $.ajax({
            type: "POST",
            url: "/Project/CompleteProjectMultipleDelete",
            data: idler,
            ajaxasync: true,
            traditional: true,
            success: function (response) {
                alert("Seçilen Projeler Başarıyla Silindi");
                window.location.href = "/Project/CompleteProjects";
            },
            error: function (response) {
                console.log("Hata Oluştu");
            }
        });
    }
}

$(document).ready(function () {
    var elem = $("#StepOne");
    elem.addClass("active");
});


function StepControl1() {
    var maincontent = CKEDITOR.instances.Content.getData();

    if ($("#PName").val() == "" || $("#PBudget").val() == "" || maincontent.length == 0) {
        alert("Lütfen eksiksiz veri girişi yapınız..");
    } else {
        var elem = $("#StepOne");
        elem.removeClass("active");
        var elem2 = $("#StepTwo");
        elem2.addClass("active");
        elem2.attr("aria-expanded", "true");
        $("#tab1-1").removeClass("active");
        var dataDiv = $("#tab1-2");
        dataDiv.addClass("active");
    }
}

function StepControl2() {

    if ($("#PTime").val() == "" || $("#PPhone").val() == "" || $("#PEMail").val() == "" || $("#PLocation").val() == "" || $("#PAdress").val() == "") {
        alert("Lütfen eksiksiz veri girişi yapınız..");

    } else {
        var elem2 = $("#StepTwo");
        elem2.removeClass("active");
        var elem3 = $("#StepFour");
        elem3.addClass("active");
        elem3.attr("aria-expanded", "true");
        var dataDiv = $("#tab1-2");
        dataDiv.removeClass("active");
        var dataDiv2 = $("#tab1-4");
        dataDiv2.addClass("active");

        $("#LblIsim").text($("#PName").val());
        $("#LblType").text($("#Ptype").val());
        $("#LblBugdet").text($("#PBudget").val());
        $("#LblBirim").text($("#PBirim").val());
        $("#LblYear").text($("#PYear").val());
        $("#LblTime").text($("#PTime").val());
        $("#LblPhone").text($("#PPhone").val());
        $("#LblAdress").text($("#PAdress").val());
        $("#LblLocation").text($("#PLocation").val());
        $("#LblEmail").text($("#PEMail").val());
    }

}


var _counter = 0;
function AddPictureForm() {

    _counter++;
    if (_counter <= 4) {

        var elem = $("#tab1-3").find(".box-content");

        elem.append("<div class='form-group' id='data" + _counter + "'>" +
                                        "<label class='col-sm-3 col-lg-2 control-label'>* Proje Resmi:</label>" +
                                        "<div class='col-sm-9 col-lg-10 controls'>" +
                                            "<div class='fileupload fileupload-new' data-provides='fileupload'>" +
                                                "<div class='fileupload-new img-thumbnail' style='width: 200px; height: 150px;'>" +
                                                    "<img src='/Content/noimage.gif' alt=''>" +
                                                "</div>" +
                                                "<div class='fileupload-preview fileupload-exists img-thumbnail' style='max-width: 200px; max-height: 150px; line-height: 20px;'></div>" +
                                                "<div>" +
                                                    "<span class='btn btn-default btn-file'>" +
                                                        "<span class='fileupload-new'>Resim Seç</span>" +
                                                        "<span class='fileupload-exists'>Değiştir</span>" +

                                                        "<input required='' oninvalid='this.setCustomValidity('Ofis Resimlerini Boş Geçemezsiniz')' type='file' accept='image/*' oninput='setCustomValidity('')' name='uploadFile" + _counter + "' id='uploadFile" + _counter + "' name='CommentPicture' class='file-input'>" +
                                                    "</span>" +
                                                    "<a href='#' class='btn btn-default fileupload-exists' data-dismiss='fileupload'>Sil</a>" +
                                                "</div>" +
                                            "</div>" +

                                            "<span class='help-inline' onclick='AddPictureForm()' style='color:#0090ff; cursor:pointer;'>Daha Fazla Resim Ekle</span>" +
                                            "<span class='help-inline' onclick='DeletePictureForm(" + _counter + ")' dataId='data" + _counter + "' style='color: #ff0000;cursor: pointer;margin-left: 15px;'>İptal Et</span>" +

                                        "</div>" +
                                    "</div>");
    } else {

        alert("Maksimum 5 Proje Resmi Ekleyebilirsiniz...");
        _counter = 0;
    }

}

function DeletePictureForm(_counter) {

    var elem = $(".box-content").find("#data" + _counter + "");
    elem.fadeOut(1000);
    _counter = 0;
}
function OnlyNumber() {
    if ((event.keyCode < 48) || (event.keyCode > 57)) {
        return false;
    }
}
