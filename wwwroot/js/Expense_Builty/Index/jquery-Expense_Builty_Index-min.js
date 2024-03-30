$(document).ready(function () {
    $("#CreateBuilty").click(function () {
        window.location.href = '/Expense/Create';
    });

    $(function () {
        $.noConflict();
        $("#RouteDetails").DataTable({
            "responsive": true, "lengthChange": false, "autoWidth": false
        }).buttons().container().appendTo('#RouteDetails_wrapper .col-md-6:eq(0)');
    });

    $('#RouteDetails').on('click', '.delete-btn', function () {
        var builtyRow = $(this).closest('tr');
        var builtyNo = builtyRow.data('builty-no');
        if (!confirm('Are you sure you want to delete this record?')) {
            return false;
        }
        else {
            deleteExpense(builtyNo, builtyRow);
        }
    });
    function deleteExpense(builtyNo,builtyRow) {
        $.ajax({
            url: "/Expense/DeleteExpense",
            type: "Post",
            data: { builtyNo: builtyNo },
            success: function (response) {
                if (response.isSaved) {
                    Swal.fire({
                        title: "Success!",
                        text: "Expense Deleted Successfully!",
                        icon: "success"
                    });
                    builtyRow.addClass('deleted-row');
                    $('.delete-btn[data-builty-no="' + builtyNo + '"]').remove();
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
        return false;
    }
});