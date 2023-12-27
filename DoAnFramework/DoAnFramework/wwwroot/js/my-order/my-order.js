var user_id = null;
$(document).ready(function () {
    //lấy thử session
    $.ajax({
        type: "POST",
        url: "/Account/getSession",
        data: {sessionName: "userId"}
        success: function (response) {
                alert(response);
                console.log(response);
                user_id = response;
        },
        error: function (error) {
            console.error("Đã xảy ra lỗi:", error);
        },
    });
    

    //SEARCH ORDER
    $('#search').on('keyup', function () {
        var searchTerm = $('#search').val();
        search(searchTerm, user_id);
    });
});


function search(searhchTerm, user_id) {

    $.ajax({
        url: '/Order/Search',
        type: 'POST',
        data: { searhchTerm: searhchTerm, user_id: user_id },
        success: function (result) {
            $('.store-table tbody').html(result);
        },
        error: function () {
            console.error('Error during search.');
        }
    });
}
