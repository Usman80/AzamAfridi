
(function () {

    $("#ExpenseTypeCode, #ExpenseTypeDescription").on('focus', function () {
        var errorId = $(this).attr('id').replace('ExpenseType', 'ErrExpenseType');
        $("#" + errorId).html("");
    });
    function validateInput(inputValue) {
        var pattern = /^[a-zA-Z]+[-_]*[a-zA-Z]*$/;
        return pattern.test(inputValue);
    }
    $("#ExpenseTypeCode, #ExpenseTypeDescription").on('keyup', function () {
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
        if ($("#ExpenseTypeCode").val() == '') {
            $("#ErrExpenseTypeCode").html("Required *");
            validform = false;
        }
        if ($("#ExpenseTypeDescription").val() == '') {
            $("#ErrExpenseTypeDescription").html("Required *");
            validform = false;
        }
        return validform;
    }

    function ClearData() {
        $("#ExpenseTypeCode").val("");
        $("#ExpenseTypeDescription").val("");
    }

    $("#SaveExpense").click(function () {
        if (PreSave())
        {
            var Model = {};
            Model.ExpenseTypeCode = $("#ExpenseTypeCode").val();
            Model.ExpenseTypeDescription = $("#ExpenseTypeDescription").val();
            $.ajax({
                url: "/ExpenseType/Create",
                type: "Post",
                data: Model,
                success: function (response) {
                    if (response.isSaved) {
                        Swal.fire({
                            title: "Success!",
                            text: "Expense Saved Successfully!",
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
        window.location.href = '/ExpenseType/Index';
    });

})(jQuery);