$(document).ready(function () {
    const orderId = $('#order-id-head').text();
    var status = $('.user-info.status').text();
    // Reset all elements to their initial state
    $("#progressbar li").removeClass("active");

    // Add 'active' class based on the status
    if (status === 'pending') {
        $("#step1").addClass("active");
        $("#step2").addClass("active");
        $(".review-order").css("display", "none");
    } else if (status === 'out for delivery') {
        $("#step1").addClass("active");
        $("#step2").addClass("active");
        $("#step3").addClass("active");
        $(".review-order").css("display", "none");
    } else if (status === 'done') {
        $("#step1").addClass("active");
        $("#step2").addClass("active");
        $("#step3").addClass("active");
        $("#step4").addClass("active");
        $(".review-order").css("display", "normal");
        $(".cancel-order").css("display", "none");
    } else if (status === 'cancelled') {
        $("#step1").addClass("active");
        $("#step2").addClass("active");
        $("#step4").addClass("active");
        $("#step3").css("display", "none");
        $(".review-order").css("display", "none");
        $(".cancel-order").css("display", "none");
        $("#step4 span").text("Đã hủy");
    }

    //hủy đơn hàng
    $('.cancel-order').on('click', function (event) {
        //event.preventDefault();
        if (confirm("Xác nhận hủy đơn hàng?")) {
            if (status != 'pending') {
                //showToastr("error", "Không thể hủy đơn hàng!");
                alert("Không thể hủy đơn hàng!");
            } else {
                cancelOrder(orderId);
            }
        }
    });

    //$('.review-order').on('click', function (event) {
    //    window.location.href = "/Order/OrderFeedback/" + orderId;
    //}

});

function cancelOrder(orderId) {
        $.ajax({
            url: "/Order/CancelOrder", // Make sure the URL is correct
            type: 'POST',
            data: { orderId: orderId },
            dataType: 'json',
            success: function (result) {
                if (result) {
                    alert("Hủy đơn hàng thành công!");
                    var url = '/Order/OrderDetail/' + orderId; // Redirect to your orders page
                    window.location.href = url;
                } else {
                    alert("Hủy đơn hàng không thành công");
                }
            },
            error: function () {
                console.error("Lỗi kết nối: hủy đơn hàng");
            }
        });
    }

