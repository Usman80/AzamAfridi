$(document).ready(function () {
    $('#Weight,#Return_Weight').on('keypress', function (event) {
        var keyCode = event.which ? event.which : event.keyCode;
        var inputValue = $(this).val();
        if (inputValue.length >= 10 || /^[0-9]*$/.test(String.fromCharCode(keyCode))) {
            return true;
        } else {
            return false;
        }
    });

    function formatAmountWithCommas(amount) {
        return amount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    }

    $('#ToFare, #FromFare').on('keyup', function () {
        var amountInput = $(this).val().replace(/,/g, '');
        var amountPattern = /^\d+(\.\d{1,2})?$/;

        if (!amountPattern.test(amountInput)) {
            $(this).val("");
            return;
        }

        var formattedAmount = formatAmountWithCommas(amountInput);
        $(this).val(formattedAmount);
    });

    $("#AddExpenseTypes").click(function () {
        $.ajax({
            url: '/Expense/ExpenseOnRoute',
            success: function (partialView) {
                $('#ExpenseTypes').append(partialView);
                var count = 1;
                $('.Amount').each(function () {
                    if (this.id) {
                        this.id = this.id.split('-')[0] + "-" + count;
                        count++;
                    }
                });
                count = 1;
                $('.Expense_Date').each(function () {
                    if (this.id) {
                        this.id = this.id.split('-')[0] + "-" + count;
                        count++;
                    }
                });
                count = 1;
                $('.ExpenseTypeId').each(function () {
                    if (this.id) {
                        this.id = this.id.split('-')[0] + "-" + count;
                        count++;
                    }
                });
                count = 1;
                $('.ErrAmount').each(function () {
                    if (this.id) {
                        this.id = this.id.split('-')[0] + "-" + count;
                        count++;
                    }
                });
                count = 1;
                $('.ErrExpense_Date').each(function () {
                    if (this.id) {
                        this.id = this.id.split('-')[0] + "-" + count;
                        count++;
                    }
                });
            }
        });

    });

    $("#AddMaintanceVeh").click(function () {
        $.ajax({
            url: '/Expense/VehicleMaintance',
            success: function (partialView) {
                $('#MaintanceVeh').append(partialView);
                var count = 1;
                $('.Maintance_Price').each(function () {
                    if (this.id) {
                        this.id = this.id.split('-')[0] + "-" + count;
                        count++;
                    }
                });
                count = 1;
                $('.Maintance_Description').each(function () {
                    if (this.id) {
                        this.id = this.id.split('-')[0] + "-" + count;
                        count++;
                    }
                });
                count = 1;
                $('.ErrMaintance_Price').each(function () {
                    if (this.id) {
                        this.id = this.id.split('-')[0] + "-" + count;
                        count++;
                    }
                });
                count = 1;
                $('.ErrMaintance_Date').each(function () {
                    if (this.id) {
                        this.id = this.id.split('-')[0] + "-" + count;
                        count++;
                    }
                });
                count = 1;
                $('.Maintance_Date').each(function () {
                    if (this.id) {
                        this.id = this.id.split('-')[0] + "-" + count;
                        count++;
                    }
                });
            }
        });
    });

    function PreSave() {
        var validform = true;
        if ($("#BuiltyNo").val() == '') {
            $("#ErrBuiltyNo").html("Required *");
            validform = false;
        }
        if ($("#DriveName").val() == '') {
            $("#ErrDriveName").html("Required *");
            validform = false;
        }
        if ($("#TruckNo").val() == '') {
            $("#ErrTruckNo").html("Required *");
            validform = false;
        }
        if ($("#Start_Date").val() == '') {
            $("#ErrStart_Date").html("Required *");
            validform = false;
        }
        if ($("#Return_Date").val() == '') {
            $("#ErrReturn_Date").html("Required *");
            validform = false;
        }
        if (isNaN($("#Weight").val()) || $("#Weight").val() == '') {
            $("#ErrWeight").html("Required *");
            validform = false;
        }
        if (isNaN($("#Return_Weight").val()) || $("#Return_Weight").val() == '') {
            $("#ErrReturn_Weight").html("Required *");
            validform = false;
        }
        if (isNaN($("#FromFare").val().replace(/,/g, '')) || $("#FromFare").val().replace(/,/g, '') == '') {
            $("#ErrFromFare").html("Required *");
            validform = false;
        }
        if (isNaN($("#ToFare").val().replace(/,/g, '')) || $("#ToFare").val().replace(/,/g, '') == '') {
            $("#ErrToFare").html("Required *");
            validform = false;
        }
        var count = 1;
        $('.Amount').each(function () {
            var val = parseFloat($(this).val());
            if (isNaN(val) || val < 0) {
                $("#ErrAmount-" + count).html("Required *");
                validform = false;
            }
            count++;
        });
        $('.Expense_Date').each(function () {
            var val = $(this).val();
            if (val == '') {
                $("#ErrExpDate-" + count).html("Required *");
                validform = false;
            }
            count++;
        });
        return validform;
    }

    $("#SaveBuilty").click(function () {
        if (PreSave()) {
            var Model = {};
            Model.BuiltyNo = $("#BuiltyNo").val();
            Model.DriveName = $("#DriveName").val();
            Model.TruckNo = $("#TruckNo").val();
            Model.Start_Date = $("#Start_Date").val();
            Model.Weight = parseInt($("#Weight").val());
            Model.FromStation = $("#FromStation").val();
            Model.ToStation = $("#ToStation").val();
            Model.FromFare = parseFloat($("#FromFare").val().replace(/,/g, ''));
            Model.Return_Date = $("#Return_Date").val();
            Model.Return_Weight = parseInt($("#Return_Weight").val());
            Model.Return_FromStation = $("#Return_FromStation").val();
            Model.Return_ToStation = $("#Return_ToStation").val();
            Model.ToFare = parseFloat($("#ToFare").val().replace(/,/g, ''));
            Model.TotalFare = parseFloat($("#TotalFare").val().replace(/,/g, ''));
            Model.TotalExpense = parseFloat($("#TotalExpense").val().replace(/,/g, ''));
            Model.TotalIncome = parseFloat($("#TotalIncome").val().replace(/,/g, ''));
            Model.TotalMaintance = parseFloat($("#TotalMaintance").val().replace(/,/g, ''));
            Model.Expenses = [];
            Model.Vehicles = [];
            var count = 1;
            $('.Amount').each(function () {
                var ExpenseType = {};
                ExpenseType.ExpenseTypeId = parseInt($("#ExpenseTypeId-" + count).val());
                ExpenseType.Amount = parseFloat($("#Amount-" + count).val().replace(/,/g, ''));
                ExpenseType.Expense_Date = $("#Expense_Date-" + count).val();
                Model.Expenses.push(ExpenseType);
                count++;
            });

            $(".Maintance_Price").each(function () {
                var VchMaintance = {};
                VchMaintance.Maintance_Description = parseInt($("#Maintance_Description-" + count).val());
                VchMaintance.Maintance_Price = parseFloat($("#Maintance_Price-" + count).val());
                VchMaintance.Maintance_Date = $("#Maintance_Date-" + count).val();
                Model.Vehicles.push(VchMaintance);
                count++;
            });
            $.ajax({
                url: "/Expense/SaveBuilty",
                type: "Post",
                data: Model,
                success: function (response) {
                    if (response.isAlreadyExist) {
                        Swal.fire({
                            icon: "error",
                            title: "Duplicate!",
                            text: "Builty Number: " + $("#BuiltyNo").val() + " already exists in the system"
                        });
                    }
                    else if (response.isSaved) {
                        Swal.fire({
                            title: "Success!",
                            text: "Builty Saved Successfully!",
                            icon: "success"
                        });
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

    function CalculateTotalIncome() {
        var result = 0;
        var TotalFare = parseFloat($("#TotalFare").val().replace(/,/g, ''));
        if (isNaN(TotalFare)) {
            TotalFare = 0;
        }
        var TotalExpense = parseFloat($("#TotalExpense").val().replace(/,/g, ''));
        if (isNaN(TotalExpense)) {
            TotalExpense = 0;
        }
        var TotalMaintance = parseFloat($("#TotalMaintance").val().replace(/,/g, ''));
        if (isNaN(TotalMaintance)) {
            TotalMaintance = 0;
        }
        result = TotalFare - TotalExpense - TotalMaintance;
        var formattedAmount = formatAmountWithCommas(result);
        $('#TotalIncome').val(formattedAmount);
    }

    function CalculateTotalExpenseTypeAmount() {
        var sum = 0;
        $('.Amount').each(function () {
            var val = parseFloat($(this).val().replace(/,/g, ''));
            if (!isNaN(val) && val >= 0) {
                sum += val;
            }
            else {
                $(this).val(0)
            }
        });
        var formattedAmount = formatAmountWithCommas(sum);
        //$('#TotalExpense').val(sum.toFixed(2));
        $('#TotalExpense').val(formattedAmount);
        CalculateTotalIncome();
    }
    function CalculateTotalMaintanceAmount() {
        var sum = 0;
        $('.Maintance_Price').each(function () {
            var val = parseFloat($(this).val().replace(/,/g, ''));
            if (!isNaN(val) && val >= 0) {
                sum += val;
            }
            else {
                $(this).val(0)
            }
        });
        var formattedAmount = formatAmountWithCommas(sum);
        $('#TotalMaintance').val(formattedAmount);
        CalculateTotalIncome();
    }

    $(document).on('input', '.Amount', function () {
        var inputAmount = $(this).val().replace(/[^0-9.]/g, '');
        var formattedAmount = formatAmount(inputAmount);
        $(this).val(formattedAmount);
        CalculateTotalExpenseTypeAmount();
    });
    $(document).on('input', '.Maintance_Price', function () {
        var inputAmount = $(this).val().replace(/[^0-9.]/g, '');
        var formattedAmount = formatAmount(inputAmount);
        $(this).val(formattedAmount);
        CalculateTotalMaintanceAmount();
    });

    function formatAmount(amount) {
        if (!amount) return '';
        amount = amount.replace(/[^\d.]/g, '');
        var parts = amount.split('.');
        var integerPart = parts[0];
        var decimalPart = parts[1] ? '.' + parts[1] : '';
        integerPart = integerPart.replace(/\B(?=(\d{3})+(?!\d))/g, ',');
        return integerPart + decimalPart;
    }

    $("#FromFare, #ToFare").on('change', function () {
        var sum = 0;
        var FromFare = parseFloat($("#FromFare").val().replace(/,/g, ''));
        if (isNaN(FromFare) || FromFare <= 0) {
            FromFare = 0;
        }
        sum += FromFare;
        var ToFare = parseFloat($("#ToFare").val().replace(/,/g, ''));
        if (isNaN(ToFare) || ToFare <= 0) {
            ToFare = 0;
        }
        sum += ToFare;
        var formattedAmount = formatAmountWithCommas(sum);
        // $('#TotalFare').val(sum.toFixed(2));
        $('#TotalFare').val(formattedAmount);
        CalculateTotalIncome();
    });

    $('#ExpenseTypes').on('click', '.DeleteExpenseOnRouteTypes', function () {
        $(this).closest('#ExpenseOnRouteTypes').remove();
        CalculateTotalExpenseTypeAmount();
        UpdateIdValuesDynamically();
    });

    $('#MaintanceVeh').on('click', '.DeleteVehicleMaintanceTypes', function () {
        $(this).closest('#VehicleMaintanceTypes').remove();
        CalculateTotalMaintanceAmount();
    });
    $("#Back").click(function () {
        window.location.href = '/Expense/Index';
    });
});