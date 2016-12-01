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
            url: "/Pictures/MultipleDelete",
            data: idler,
            ajaxasync: true,
            traditional: true,
            success: function (response) {
                alert("Seçilen Resimler Başarıyla Silindi");
                window.location.href = "/Pictures/PicturesIndex";
            },
            error: function (response) {
                console.log("Hata Oluştu");
            }
        });
    }
}