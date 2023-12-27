$(document).ready(function () {
    const orderId = $('#order-id-head').text();
    var status = $('.user-info.status').text();
    // Reset all elements to their initial state
    $("#progressbar li").removeClass("active");

    // Add 'active' class based on the status
    if (status === 'pending') {
        $("#step1").addClass("active");
        $("#step2").addClass("active");
    } else if (status === 'out for delivery') {
        $("#step1").addClass("active");
        $("#step2").addClass("active");
        $("#step3").addClass("active");
        $("#out-for-delivery").css("display", "none");
    } else if (status === 'done') {
        $("#step1").addClass("active");
        $("#step2").addClass("active");
        $("#step3").addClass("active");
        $("#step4").addClass("active");
        $("#out-for-delivery").css("display", "none");
        $("#done").css("display", "none");
    } else if (status === 'cancelled') {
        $("#step1").addClass("active");
        $("#step2").addClass("active");
        $("#step4").addClass("active");
        $("#step3").css("display", "none");
        $("#out-for-delivery").css("display", "none");
        $("#done").css("display", "none");
        $("#step4 span").text("Đã hủy");
    }

    //hủy đơn hàng
    $('#out-for-delivery').on('click', function (event) {
        //event.preventDefault();
        if (confirm("Bắt đầu giao hàng?")) {
            
            updateOrderStatus(orderId, 'out for delivery');
            
        }
    });

    $('#done').on('click', function (event) {
        //event.preventDefault();
        if (confirm("Hoàn thành đơn hàng?")) {
            if (status == 'pending') {
                alert("Hãy chuyển sang trạng thái đang giao trước khi hoàn thành");
            }
            else
            updateOrderStatus(orderId, 'done');

        }
    });

    //$('.review-order').on('click', function (event) {
    //    window.location.href = "/Order/OrderFeedback/" + orderId;
    //}

});

function updateOrderStatus(orderId, status) {
    $.ajax({
        url: "/Admin/Order/UpdateOrderStatus", 
        type: 'POST',
        data: { orderId: orderId, status: status },
        dataType: 'json',
        success: function (result) {
            if (result) {
                alert("Cập nhật trạng thái thành công!");
                window.location.href =  window.location.href;
            } else {
                alert("Cập nhật trạng thái không thành công");
            }
        },
        error: function () {
            console.error("Lỗi kết nối");
        }
    });
}

