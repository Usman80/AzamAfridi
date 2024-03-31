$(document).ready(function () {
    $('#loadTypeReportBtn').click(function () {
        var truckNo = $('#truckNoInput').val();
        var expenseType = $('.ExpenseTypeId').val();
        var startDate = $("#Start_Date").val();
        var endDate = $("#End_Date").val();
        if (truckNo == "" || expenseType == "" || startDate == "" || endDate == "")
        {
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
                    expenseTypeId: expenseType,
                    StartDate: startDate,
                    EndDate: endDate
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