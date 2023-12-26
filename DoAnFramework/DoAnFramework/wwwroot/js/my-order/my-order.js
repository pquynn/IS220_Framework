
$(document).ready(function () {

     user_id='KH009'; //giả sử
    

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
