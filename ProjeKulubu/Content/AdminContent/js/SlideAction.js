$(document).ready(function () {
    $("#dataAnswer").hide();
});


$("#dataQuestion").on("click", function () {

    $("#dataAnswer").slideToggle(1000);
});
