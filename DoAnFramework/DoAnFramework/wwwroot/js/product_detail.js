/** @format */
// console.log(user_id);
var user_id=null;

var myCart = [];

var productName;
var productPrice;
var numberOfProduct;
var productID;
var soLuongTonKho;

if (window.location.href.includes("BookDetail")) {
    if (localStorage.getItem("myCart") !== null) {
        myCart = JSON.parse(localStorage.getItem("myCart"));
    }

    $(".btn-cancel").click(function () {
        productName = $(".product-name").text();
        productID = Number($(".product-name").data('value'));
        productPrice = convertCurrencyToNumber($(".product-price").text());
        numberOfProduct = Number($(".number").text());
        soLuongTonKho = Number($(".product-price").data('value'));


        if (productName && productPrice && numberOfProduct) {
            let tempProduct = {
                productID: productID,
                productName: productName,
                productPrice: productPrice,
                numberOfProduct: numberOfProduct
            };

            // Trường hợp không đăng nhập
            if (user_id === null) {
                alert("Chưa đăng nhập");

                // Đưa sản phẩm được chọn vào giỏ hàng.
                myCart.push(tempProduct);

                localStorage.setItem("myCart", JSON.stringify(myCart));
                $('.product-price').data('value', soLuongTonKho - numberOfProduct);
                alert("Sản phẩm đã được thêm vào giỏ hàng");
                console.log(myCart);
            }
            // Trường hợp có đăng nhập.
            else {
                alert("Đã đăng nhập");
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
                            alert("Không có dữ liệu trả về");
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
            alert("Thieu thong tin!");
        }
    });

    $('.btn-confirm').click(function () {
        alert("Mua hang");
    });
}
//$(".btn-confirm").click(function () {
//    productName = $(".product-name").text();
//    productPrice = $(".product-price").text();
//    numberOfProduct = $(".number").text();
//    productImage = $(".product-main-img img").attr("src");

//    if (productName && productPrice && numberOfProduct && productImage) {
//        let tempProduct = {
//            productName: productName,
//            productPrice: productPrice,
//            productSize: productSize,
//            numberOfProduct: numberOfProduct,
//            productImage: productImage,
//        };

//        // Trường hợp không đăng nhập
//        if (user_id === null) {
//            // Đưa sản phẩm được chọn vào giỏ hàng.
//            myCart.push(tempProduct);
//            console.log(myCart);

//            // Lưu dữ liệu vào localStorage.
//            localStorage.setItem("myCart", JSON.stringify(myCart));
//            alert("Sản phẩm đã được thêm vào giỏ hàng");
//        }
//        // Trường hợp có đăng nhập.
//        else {
//            console.log("Chua login");
//        }
//    }
//}


function convertCurrencyToNumber(currencyString) {
    // Loại bỏ ký tự không phải số và dấu phẩy
    var numericString = currencyString.replace(/[^0-9,]/g, '');

    // Loại bỏ dấu phẩy và chuyển đổi thành số nguyên
    var integerValue = parseInt(numericString.replace(/,/g, ''), 10);

    return integerValue;
}
//}
//} else if (window.location.href.includes("cart.php")) {
//    // Chuyển giỏ hàng qua các file có dạng "cart.php"
//    myCart = JSON.parse(localStorage.getItem("myCart"));
//}

// Hàm lấy số sản phẩm có trong giỏ hàng.
//function countProductInCart(myCart) {
//    if (myCart !== null) return myCart.length;
//    return 0;
//}
