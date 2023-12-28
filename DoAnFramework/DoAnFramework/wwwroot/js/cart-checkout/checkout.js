var user_id = null;
const cart = JSON.parse(localStorage.getItem("myCart"));
const localCart = getLocalCart(cart);
var sendPrice = 0;
$(document).ready(function () {
    //lấy session
    $.ajax({
        type: "POST",
        url: "/Account/getSession",
        data: { sessionName: "userId" },
        success: function (response) {
            if (response) {
                console.log(response);
                user_id = response;
            }
        },
        error: function (error) {
            console.error("Đã xảy ra lỗi:", error);
        },
    });
        

        
    displayCheckout(user_id);
        

    $("#buy-form").submit(function (e) {
        e.preventDefault();
    // Ten, sdt
            const name = $(".customer-name").val();
            const phone = $(".customer-phone").val();
         
            // Dia chi
         
        const address = {
            tinhThanh: $("#city").val(),
            quanHuyen: $("#district").val(),
            xaPhuong: $("#ward").val(),
            duongAp: $("#street").val(),
        };

            // thoi gian
            const now = new Date();
            const date = `${now.getFullYear()}-${now.getMonth()}-${now.getDate()} ${now.getHours()}:${now.getMinutes()}:${now.getSeconds()}`;

            // phuong thuc thanh toan
        const paymentMethod = $(`input[name="payment"]:checked`).val();

        // tong tien
        console.log(sendPrice);
            buy(name, phone, address, user_id, date, paymentMethod, localCart, Number(sendPrice));
            
            if (paymentMethod == "cod") {
                alert("Đặt hàng thành công!");
                window.location.href = "/Order/Cart";
            }
             

            // xoa localstorage chua gio hàng khi ko đăng nhập
            localStorage.clear();
    });
            
});

function displayData() {
        var addressString = $('#temp-address').text();
            
            var myString = addressString.split(",");
        for (var i = 0; i < myString.length; i++) {
            myString[i] = myString[i].trim();
                }

        var address = {
            tinhThanh: "",
            quanHuyen: "",
            xaPhuong: "",
            duongAp: "",
        };
        address.duongAp = myString[0];
        if (myString.length == 4) {
            address.xaPhuong = myString[1];
        address.quanHuyen = myString[2];
        address.tinhThanh = myString[3];
                } else {
            address.quanHuyen = myString[1];
        address.tinhThanh = myString[2];
                }
        if (address.tinhThanh == "TP.HCM") {
            address.tinhThanh = "Thành phố Hồ Chí Minh";
                }
        // display address
        $("#city").val(address.tinhThanh);
        $("#district").val(address.quanHuyen);
        $("#ward").val(address.xaPhuong);
        $("#street").val(address.duongAp);


        var totalPrice = 0;
    $(".product-checkout").each(function () {
            // Get the quantity and price values
        var quantity_text = ($(this).find(".amount-feld").text()).slice('X ',);
        var quantity = quantity_text.replace(/^X\s/, '');
        var price = $(this).find(".product-total").text();
            var total = Number(quantity) * Number(price);

            // Update the product-total td with the calculated total
        $(this).find(".product-total").text(total.toString());
        sendPrice += total;
            totalPrice += total;
        });

        $(".sub-total--amount").text(totalPrice.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") + " đ");
        $(".total-amount").text(totalPrice.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") + " đ");

    }


    function displayCheckout(user_id) {
        if (user_id != null && user_id != "") {
            displayData();
        }
        else
            displayProductData(localCart);
    }
    
    
    function displayProductData(productData) {
            // DISPLAY PRODUCT: START
            var subTotal = 0;
        const tbProduct = $(".product-list--checkout tbody");
        productData.forEach(function (row) {
            subTotal += row.PRICE * row.QUANTITY;
        var imageUrl = "data:image/png;base64," + row.FIRST_PICTURE;
        tbProduct.append(`<tr class="product-checkout">
            <td class="product-infor">
                <img
                    alt="${row.PRODUCT_NAME}"
                    class="product-img"
                    src="${imageUrl}" />
                <div class="product-descr">
                    <a href="#">${row.PRODUCT_NAME}</a>
                    <small>X ${row.QUANTITY}</small>
                </div>
            </td>

            <td class="product-total">${(row.PRICE * row.QUANTITY).toString()}</td>
        </tr>`);


        var totalPrice = 0;
        // Loop through each row in the table body
        $('.product-list--body tr').each(function () {
            var price = $(this).find('.product-total').text();
            totalPrice += Number(price);
            sendPrice = totalPrice;
                    });

        $(".sub-total--amount").text(totalPrice.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") + " đ");
        $(".total-amount").text(totalPrice.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") + " đ");
                });
            // DISPLAY PRODUCT: END
        }


    function buy(
    name,
    phone,
    address,
    user_id,
    date,
    paymentMethod,
        localCart,
    id) {

        $.ajax({
            url: "/Checkout/Buy",
            type: "POST",
            // dataType: "json",
            data: {
                user_id: user_id,
                name: name,
                phone: phone,
                tinhThanh: address.tinhThanh,
                quanHuyen: address.quanHuyen,
                xaPhuong: address.xaPhuong,
                duongAp: address.duongAp,
                date: date,
                paymentMethod: paymentMethod,
                localCart: localCart,
            },
            success: function (response) {
                if (response.success) {
                    if (paymentMethod == 'momo-wallet')
                        window.location.href = "/Checkout/Payment/" + id;
                }
                else
                    console.log('failed');
            },
            error: function () {
                console.log(0);
            },
        });
}


function getLocalCart(cart) {
    if (cart === null) {
        return [];
    }
    // get cart form client
    var mergedCart = [];
    // LAY DATA NEU NGUOI DUNG KHONG DANG NHAP: START
    // DINH DANG JSON CHO GIO HANG && GOP SAN PHAM TRUNG
    // kiem tra xem gio hang da co san pham chua
    function isInclude(arrayObj, value) {
        for (let i = 0; i < arrayObj.length; i++) {
            if (
                arrayObj[i].PRODUCT_NAME == value.PRODUCT_NAME
            ) {
                return i;
            }
        }
        return false;
    }

    // dinh dang san pham
    cart.forEach((product, i) => {
        // chuyen thanh object
        var row = {
            ORDER_DETAIL_ID: i,
            PRODUCT_NAME: product.productName,
            PRICE: Number(product.productPrice),
            QUANTITY: Number(product.numberOfProduct),
            FIRST_PICTURE: product.imageOfProduct.replace(/^data:image\/\w+;base64,/, '')
        };

        // dua vao day san pham (cart)
        var varIsInclude = isInclude(mergedCart, row);
        if (varIsInclude === false) {
            mergedCart.push(row);
        } else {
            mergedCart[varIsInclude].QUANTITY += row.QUANTITY;
        }
    });
    // LAY DATA NEU NHUOI DUNG KHONG DANG NHAP: END
    return mergedCart;
}
// LAY GIO HANG LUU O CLIENT NEU KHONG DANG NHAP: END

var rowAmount;
function getRowAmount(amount) {
    rowAmount = amount;
}