/** @format */
// console.log(user_id);
//user_id = null;
var myCart = [];

var productName;
var productPrice;
var numberOfProduct;
var productImage;

if (window.location.href.includes("BookDetail")) {
    if (localStorage.getItem("myCart") !== null) {
        myCart = JSON.parse(localStorage.getItem("myCart"));
    }

    $(".btn-cancel").click(function () {
        productName = $(".product-name").text();
        productPrice = $(".product-price").text();
        numberOfProduct = $(".number").text();
        productImage = $(".product-main-img img").attr("src");

        if (productName && productPrice && numberOfProduct && productImage) {
            let tempProduct = {
                productName: productName,
                productPrice: productPrice,
                numberOfProduct: numberOfProduct,
                productImage: productImage,
            };

            // Trường hợp không đăng nhập
            //if (user_id === null) {
                // Đưa sản phẩm được chọn vào giỏ hàng.
                myCart.push(tempProduct);

                localStorage.setItem("myCart", JSON.stringify(myCart));
                alert("Sản phẩm đã được thêm vào giỏ hàng");
                console.log(myCart);
            //}
            // Trường hợp có đăng nhập.
            //else {
            //    alert("Chua Login");
            //}
        }
        else {
            alert("Thieu thong tin!");
        }
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


//    function formatNumber(input) {
//            let strNumber = String(input);

//            // Split the string into groups of 3 characters from the right
//            let chunks = [];
//            while (strNumber.length > 0) {
//                chunks.push(strNumber.slice(-3));
//                strNumber = strNumber.slice(0, -3);
//            }

//            // Reverse the chunks and join them with dots
//            let formattedStr = chunks.reverse().join(".");

//            return formattedStr;
//        }
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
