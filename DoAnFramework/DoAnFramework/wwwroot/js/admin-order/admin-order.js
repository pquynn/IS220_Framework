$(document).ready(function () {
    
    //xóa đơn hàng
    $('.admin-table').on('click', '.admin-delete', function (e) {
        e.preventDefault();
        if (confirm('Xác nhận xóa ?')) {
            // Get the employee_id from the first column of the row
            closest_row = $(this).closest('tr');
            var orderId = closest_row.find('td:first').text();
            deleteOrder(orderId);
        }
    });
    

});

function deleteOrder(orderId) {
    $.ajax({
        url: "/Admin/Order/DeleteOrder", 
        type: 'POST',
        data: { orderId: orderId},
        dataType: 'json',
        success: function (result) {
            if (result) {
                alert("Xóa đơn hàng thành công!");
                window.location.href = window.location.href;
            } else {
                alert("Xóa đơn hàng không thành công");
            }
        },
        error: function () {
            console.error("Lỗi kết nối");
        }
    });
}

