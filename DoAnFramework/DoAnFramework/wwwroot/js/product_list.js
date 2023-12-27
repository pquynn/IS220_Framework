/** @format */

// Fetch data using AJAX
$(document).ready(function () {

    //$("#clearFilter").click(function () {
    //    // Xóa các lựa chọn phân loại.
    //    $("#panelsStayOpen-collapseOne a").removeClass("active");
    //    $("#panelsStayOpen-collapseTwo a").removeClass("active");
    //    // Xóa danh sách sản phẩm được hiện thị.
    //    $(".product-list").empty();
    //    optionGender = null;
    //    optionCategory = 0;
    //    InsertData(data.tableProduct, optionGender, optionCategory);
    //});
    $("#sort").change(function () {
        var selectedOption = $(this).val();
        sortProducts(selectedOption);
    });

    function sortProducts(sortOption) {
        var productContainer = $(".product-list");
        var products = productContainer.children(".product-detail");

        products.sort(function (a, b) {
            var aValue = parseInt($(a).data("price"));
            var bValue = parseInt($(b).data("price"));

            if (sortOption === "expensive-to-cheap") {
                return aValue - bValue;
            } else if (sortOption === "cheap-to-expensive") {
                return bValue - aValue;
            } else {
                // Default to sorting by price if the option is not recognized
                return aValue - bValue;
            }
        });

        // Xóa tất cả các sản phẩm hiện tại
        productContainer.empty();

        // Thêm lại các sản phẩm đã được sắp xếp
        products.each(function (index, product) {
            productContainer.append(product);
        });
    }
});

function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return "";
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}
