
(function () {

    $("#StationCode, #StationDescription").on('focus', function () {
        var errorId = $(this).attr('id').replace('StationName', 'ErrStationName');
        $("#" + errorId).html("");
    });
    function validateInput(inputValue) {
        var pattern = /^[a-zA-Z]+[-_]*[a-zA-Z]*$/;
        return pattern.test(inputValue);
    }
    $("#StationCode, #StationDescription").on('keyup', function () {
        var inputValue = $(this).val();
        if (!validateInput(inputValue)) {
            $(this).val('');
            $(this).addClass('is-invalid');
        } else {
            $(this).removeClass('is-invalid');
        }
    });

    function PreSave() {
        var validform = true;
        if ($("#StationCode").val() == '') {
            $("#ErrStationCode").html("Required *");
            validform = false;
        }
        if ($("#StationDescription").val() == '') {
            $("#ErrStationDescription").html("Required *");
            validform = false;
        }
        return validform;
    }

    function ClearData() {
        $("#StationCode").val("");
        $("#StationDescription").val("");
    }

    $("#SaveStation").click(function () {
        if (PreSave()) {
            var Model = {};
            Model.StationCode = $("#StationCode").val();
            Model.StationDescription = $("#StationDescription").val();
            $.ajax({
                url: "/Station/Create",
                type: "Post",
                data: Model,
                success: function (response) {
                    if (response.isSaved) {
                        Swal.fire({
                            title: "Success!",
                            text: "Station Name Saved Successfully!",
                            icon: "success"
                        });
                        ClearData();
                    }
                    else {
                        Swal.fire({
                            icon: "error",
                            title: "Oops...",
                            text: "Something went wrong!"
                        });
                    }
                },
                failure: function (response) {
                    Swal.fire({
                        icon: "error",
                        title: "Oops...",
                        text: "Something went wrong!"
                    });
                }
            });
        }
    });

    $("#Back").click(function () {
        window.location.href = '/Station/Index';
    });

})(jQuery);