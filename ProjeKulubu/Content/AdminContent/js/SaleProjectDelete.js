function ProjectDataDelete() {

    $.ajax({
        type: "POST",
        url: "/Project/SaleProjectDataDelete/",
        data: { id: $("#dataId").val() },
        ajaxasync: true,
        traditional: true,
        success: function (response) {
            alert("Seçilen Proje Başarıyla Silindi");
            window.location.href = "/Project/SaleProjects";
        },
        error: function (response) {
            console.log("Hata Oluştu");
        }
    });
}