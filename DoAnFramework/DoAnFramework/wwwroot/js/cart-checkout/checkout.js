
    //const cart = JSON.parse(localStorage.getItem("myCart"));
    //const localCart = getLocalCart(cart);

    $(document).ready(function () {
        var user_id = $('#temp-user-id').text();
        displayCheckout(user_id);

        $("#buy-form").submit(function (e) {
            e.preventDefault();
            console.log("buy-form is submitted");
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
            console.log(address);

             // thoi gian
             const now = new Date();
             const date = `${now.getFullYear()}-${now.getMonth()}-${now.getDate()} ${now.getHours()}:${now.getMinutes()}:${now.getSeconds()}`;

             // phuong thuc thanh toan
             const paymentMethod = $(`input[name="payment"]:checked`).val();
             
             //buy(name, phone, address, user_id, date, paymentMethod, localCart);
            buy(name, phone, address, user_id, date, paymentMethod, null);

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
        console.log(quantity);
        var price = $(this).find(".product-total").text();
        console.log(price);
            var total = Number(quantity) * Number(price);

            // Update the product-total td with the calculated total
            $(this).find(".product-total").text(total.toString());

            totalPrice += total;
        });

        $(".sub-total--amount").text(totalPrice.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") + " đ");
        $(".total-amount").text(totalPrice.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") + " đ");

    }


    function displayCheckout(user_id) {
        if (user_id) {
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
                    <small class="gray-text">Size ${row.SIZE}</small>
                    <small>X${row.QUANTITY}</small>
                </div>
            </td>

            <td>${(row.PRICE * row.QUANTITY).toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}</td>
        </tr>`);


        var totalPrice = 0;
        // Loop through each row in the table body
        $('.product-list--body tr').each(function () {
            var price = $(this).find('.product-total').text();
            totalPrice += Number(price);
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
    localCart) {
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
                console.log(response);
            },
            error: function () {
                console.log(0);
            },
        });
    }