/** @format */

var user_id = null;

$.ajax({
    type: "POST",
    url: "/Account/getSession",
    data: { sessionName: "userId" },
    success: function (response) {
        if (response) {
            console.log(response);
            user_id = response;
        }
        else {
            alert("Không có dữ liệu trả về");
        }
    },
    error: function (error) {
        console.error("Đã xảy ra lỗi:", error);
    },
});

var myCart = [];

var productName;
var productPrice;
var numberOfProduct;
var imageOfProduct;
var productID;
var soLuongTonKho;

if (window.location.href.includes("BookDetail")) {
    if (localStorage.getItem("myCart") !== null) {
        myCart = JSON.parse(localStorage.getItem("myCart"));
    }

    function productToCart() {
        productName = $(".product-name").text();
        productID = Number($(".product-name").data('value'));
        imageOfProduct = $(".product-main-img img").attr("src");
        productPrice = convertCurrencyToNumber($(".product-price").text());
        numberOfProduct = Number($(".number").text());
        soLuongTonKho = Number($(".product-price").data('value'));


        if (productName && productPrice && numberOfProduct && imageOfProduct) {
            let tempProduct = {
                productID: productID,
                productName: productName,
                productPrice: productPrice,
                numberOfProduct: numberOfProduct,
                imageOfProduct: imageOfProduct
            };

            // Trường hợp không đăng nhập
            if (user_id === null) {
                // Đưa sản phẩm được chọn vào giỏ hàng.
                myCart.push(tempProduct);

                localStorage.setItem("myCart", JSON.stringify(myCart));
                $('.product-price').data('value', soLuongTonKho - numberOfProduct);
                alert("Sản phẩm đã được thêm vào giỏ hàng");
            }
            // Trường hợp có đăng nhập.
            else {
                $.ajax({
                    type: "POST",
                    url: "/Order/addToCart",
                    data: {
                        userID: user_id,
                        productData: tempProduct,
                    },
                    datatype: 'json',
                    success: function (response) {
                        if (response) {
                            console.log(response);
                            alert("Sản phẩm đã được thêm vào giỏ hàng");
                        }
                        else {
                            alert("Đã xảy ra lỗi!");
                        }
                    },
                    error: function (error) {
                        alert("Sản phẩm bị lỗi khi đưa vào giỏ hàng");
                        console.error("Đã xảy ra lỗi:", error);
                    },
                });
            }
        }
        else {
            alert("Thiếu thông tin !");
        }
    }
    $(".btn-cancel").click(function () {
        productToCart();
    });

    $('.btn-confirm').click(function () {
        productToCart();
        window.location.href = "/Order/Cart";
    });
}


function convertCurrencyToNumber(currencyString) {
    // Loại bỏ ký tự không phải số và dấu phẩy
    var numericString = currencyString.replace(/[^0-9,]/g, '');

    // Loại bỏ dấu phẩy và chuyển đổi thành số nguyên
    var integerValue = parseInt(numericString.replace(/,/g, ''), 10);

    return integerValue;
}