$(document).ready(function () {
    $('#loadTypeReportBtn').click(function () {
        var truckNo = $('#truckNoInput').val();
        var expenseType = $('.ExpenseTypeId').val();
        alert(expenseType);
        if (truckNo == "" || truckNo == null || expenseType == "" || expenseType == null) {
            Swal.fire({
                icon: "error",
                title: "Error...",
                text: "Enter Data For Search!"
            });
        }
        else {
            $.ajax({
                url: "/ExpenseTypeReport/ExpenseTypeReport",
                type: 'GET',
                data: {
                    truckNumber: truckNo,
                    expenseTypeId: expenseType
                },
                success: function (data) {
                    $('#ExpenseTypeReportContainer').html(data);
                },
                error: function () {
                    alert('Error loading report.');
                }
            });
        }
    });
});