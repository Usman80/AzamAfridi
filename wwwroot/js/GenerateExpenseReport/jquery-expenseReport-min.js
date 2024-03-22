$(document).ready(function () {
    $('#loadReportBtn').click(function () {
        var truckNo = $('#truckNoInput').val();
        if (truckNo == "" || truckNo == null) {
            Swal.fire({
                icon: "error",
                title: "Error...",
                text: "Enter Truck No!"
            });
        }
        else {
            $.ajax({
                url: "/ExpenseReport/GenerateExpenseReport",
                type: 'GET',
                data: { truckNo: truckNo },
                success: function (data) {
                    $('#reportContainer').html(data);
                },
                error: function () {
                    alert('Error loading report.');
                }
            });
        }
    });
});