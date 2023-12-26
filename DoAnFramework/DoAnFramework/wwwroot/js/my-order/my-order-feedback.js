$(document).ready(function () {
    $(".add-order-feedback").submit(function (event) {
        event.preventDefault();
        var orderId = $('#temp-order-id').text();
        var userId = $('#temp-user-id').text();
        var userName = $('#temp-user-name').text();
        var comments = [];
        

        $(".product-line").each(function () {
            var comtId = $(this).attr("id");
            var bookId = $(this).find(".book-name").attr("id");
            var bookName = $(this).find(".book-name").text();
            var score = $(this).find('input[name*="rating"]:checked').val();
            var content = $(this).find(".input-feedback").val();
            // Construct comment object
            var comment = {
                comtId: comtId,
                bookId: bookId,
                bookName: bookName,
                score: score,
                content: content
            };

            // Push comment object to comments array
            comments.push(comment);
        });

        addFeedback(userId, userName, comments);
    });

    $('.btn-cancel').on('click', function (event) {
        event.preventDefault();
        if (confirm("Những thay đổi sẽ không được lưu?"))
            window.location.href = "/Order/OrderDetail/" + orderId;
    });
});

function addFeedback(userId, userName, comments) {
    $.ajax({
        url: "/Order/AddFeedback", // Make sure the URL is correct
        type: 'POST',
        data: { userId: userId, userName: userName, comments: comments },
        dataType: 'json',
        success: function (result) {
            if (result) {
                alert("Đánh giá thành công!");
                var url = '/Order/OrderFeddback/' + orderId; // Redirect to your orders page
                window.location.href = url;
            } else {
                alert("Đánh giá không thành công");
            }
        },
        error: function () {
            console.error("Lỗi kết nối: Đánh giá");
        }
    });
}