$(document).ready(function () {
    $('#loadMaintanceReportBtn').click(function () {
        var truckNo = $('#truckNoInput').val();
        var Start_Date = $("#Start_Date").val();
        var End_Date = $("#End_Date").val();
        if (truckNo == "" || Start_Date == "" || End_Date == "") {
            Swal.fire({
                icon: "error",
                title: "Error...",
                text: "Enter Information!"
            });
        }
        else {
            $.ajax({
                url: "/VchMaintReport/ExpenseTypeReport",
                type: 'GET',
                data: {
                    truckNumber: truckNo, StartDate: Start_Date, EndDate: End_Date
                },
                success: function (data) {
                    $('#reportMaintanceContainer').html(data);
                },
                error: function () {
                    alert('Error loading report.');
                }
            });
        }
    });
});