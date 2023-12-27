
const cart = JSON.parse(localStorage.getItem("myCart"));
const localCart = getLocalCart(cart);

    // Render cart row: start
$(document).ready(function () {
     var user_id = $('#temp-user-id').text();
        
    if (user_id != null && user_id != "") {

        var rowCount = $('.product-list--body tr').length;
        if (rowCount === 0 || typeof rowCount === "undefined") {
            emptyCart();
        }
        else {
            var totalPrice = 0;
            $(".product").each(function () {
                // Get the quantity and price values
                var quantity = parseInt($(this).find(".amount-feld").val());
                var price = parseInt($(this).find("#book-price-" + $(this).attr("id").split("-")[1]).text());
                var total = quantity * price;

                // Update the product-total td with the calculated total
                $(this).find(".product-total").text(total);

                totalPrice += total;
            });

            $(".sub-total--amount").text(totalPrice.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") + " đ");
            $(".total-amount").text(totalPrice.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") + " đ");

        }    
    }
    else
        displayCart();
        

        // CHANGE PRODUCT'S AMOUNT-BTN: START------------------
        function changeAmountBtn(btnType, id) {
            // var id = $(btn).attr("id").slice(4);
            var value = Number($(`#${id}`).val()) + btnType;
            if (value >= 1 || btnType != -1) {
            // const input = $(`#${id}`);
            $(`#${id}`).val(value).trigger("change");
                }
        }

        // minus
        $(".product-list--body").on("click", ".minus", function () {
                var id = $(this).attr("id").slice(10);
                changeAmountBtn(-1, id);
        });

        // plus
        $(".product-list--body").on("click", ".plus", function () {
                var id = $(this).attr("id").slice(9);
                changeAmountBtn(1, id);
        });

        // CHANGE PRODUCT'S AMOUNT-BTN: END



        // CHANGE PRODUCT AMOUNT: START------------------
        $(".amount-feld").on("change", function () {
            var inputId = $(this).attr("id");
            var quantity = $(this).val();
            var price = $(`#book-price-${inputId}`).text(); 

            var total = Number(quantity) * Number(price);

            $(`#pro-total-${inputId}`).text(total.toString());

            var totalPrice = 0;

            // Loop through each row in the table body
            $('.product-list--body tr').each(function () {
                // Get the price from the "Tổng tiền" column in the current row
                var price = $(this).find('.product-total').text();

                // Add the price to the total
                totalPrice += Number(price);
            });

            $(".sub-total--amount").text(totalPrice.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") + " đ");
            $(".total-amount").text(totalPrice.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") + " đ");

            //update data
            if (user_id === null || user_id === "") {
                const cart = JSON.parse(localStorage.getItem("myCart"));
                changeAmountLocal(getLocalCart(cart), Number(quantity), inputId);
            } else {
                changeAmountLogin(inputId, quantity);
            }
        });
    function changeAmountLocal(cartData, productAmount, rowId) {
            cartData[rowId].QUANTITY = productAmount;
           
            var cartArray = [];
            cartData.forEach((row) => {
                let rowData = {};

                rowData.productName = row.PRODUCT_NAME;
                rowData.productPrice = `${row.PRICE} VND`;
                rowData.numberOfProduct = `${row.QUANTITY}`;
                //co doi FIRST_PICTURE THANH FRONT_COVER Khong
                rowData.productImage = `${row.FIRST_PICTURE}`;
                cartArray.push(rowData);
            });
            localStorage.setItem("myCart", JSON.stringify(cartArray));
        }

        function changeAmountLogin(inputId, inputVal) {
                $.ajax({
                    type: "POST",
                    url: "/Order/UpdateCartProduct",
                    data: { order_detail_id: inputId, quantity: inputVal },
                    success: function () {
                        // Handle success if needed
                        console.log("update quantity success");
                    },
                    error: function (error) {
                        console.log(error);
                    }
                });
            }
        // CHANGE PRODUCT AMOUNT: END


        // REMOVE PRODUCT: START------------------
        // delete product
        $(".remove-btn").on("click", function () {
            var buttonId = $(this).attr('id');
            var id = buttonId.replace('btn-remove-', '');

            const confirmResult = confirm("Xác nhận xóa sản phẩm?");
            if (confirmResult === true) {
                // giam tong don hang
                var subTotal = $(".sub-total--amount").text();
                var old_price = parseInt(subTotal.replace(/,/g, ''), 10);

                var deletedProduct = $(`#pro-total-${id}`).text();
                var totalPrice = Number(old_price) - Number(deletedProduct);

                $(".sub-total--amount").text(totalPrice.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") + " đ");
                $(".total-amount").text(totalPrice.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") + " đ");

                // giam so luong san pham trong gio tren giao dien
                var rowCount = $('.product-list--body tr').length;
                rowCount--;

                $(`#product-${id}`).remove();
                //$(`#product-${id}`).addClass("hidden");
                if (rowCount === 0 || typeof rowCount === "undefined") {
                    emptyCart();
                }

                if (user_id) 
                    removeProductLogin(id);
                else 
                    removeProductLocal(id);
                     
            }
        });
        function removeProductLocal(id) {
            const updatedCart = JSON.parse(localStorage.getItem("myCart"));
            updatedCart.splice(id, 1);
            localStorage.setItem("myCart", JSON.stringify(updatedCart));
        }

        function removeProductLogin(orderDetailId) {
            $.ajax({
                type: "POST",
                url: "/Order/DeleteOrderDetail",
                data: { orderDetailId: orderDetailId },
                success: function (response) {
                    if (response.success) {
                        console.log(response.message);

                    } else {
                        console.log(response.message);
                    }
                },
                error: function () {
                    // Handle the AJAX error
                    console.log('error');
                }
            });
        }
        // REMOVE PRODUCT: END
    
        

   
});

function emptyCart() {
    document.querySelector(".empty-cart").classList.toggle("hidden");
    document.querySelector(".cart-body").classList.toggle("hidden");
}
        // LAY GIO HANG LUU O CLIENT NEU KHONG DANG NHAP: START
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
                    PRICE: Number(product.productPrice.slice(0, -4).replaceAll(",", "")),
                    QUANTITY: Number(product.numberOfProduct),
                    FIRST_PICTURE: product.productImage.replace(/^data:image\/\w+;base64,/, '')
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

        //DISPLAY CART IF USER NOT LOGIN: START
        function displayCart() {
            var total = 0;
            var data = getLocalCart(cart);
            
            // kt xem gio hang co trong khong
            if (data.length === 0) {
                emptyCart();
                return 0;
            }
            const tbCart = $(".product-list--body");
            data.forEach(function (row) {
                getRowAmount(data.length);

            var imageUrl = "data:image/png;base64," + row.FIRST_PICTURE;
            tbCart.append(`
            <tr
                class="product"
                id="product-${row.ORDER_DETAIL_ID}">
                <!-- infor -->
                <td class="product-infor" style="max-width:420px;">
                    <img
                        src="${imageUrl}"
                        class="product-img"
                        alt="${row.PRODUCT_NAME}" />
                    <div class="product-descr">
                        <a href="#">${row.PRODUCT_NAME}</a>
                    </div>
                </td>

                <!-- price -->
                <td id="book-price-${row.ORDER_DETAIL_ID}" class="price">${row.PRICE * 1}</td>

                <!-- amount -->
                <td class="product-amount">
                    <div class="flex amount">
                        <button
                            class="amount-btn pointer minus"
                            id="btn-minus-${row.ORDER_DETAIL_ID}">
                            -</button>
                        <input
                            class="amount-feld"
                            type="number"
                            min="1"
                            value="${row.QUANTITY}"
                            id="${row.ORDER_DETAIL_ID}">
                            <button
                                class="amount-btn pointer plus"
                                id="btn-plus-${row.ORDER_DETAIL_ID}">
                                +</button>
                    </div>
                </td>

                <!-- total -->
                <td
                    id="pro-total-${row.ORDER_DETAIL_ID}"
                    class="product-total">
                    ${row.PRICE * row.QUANTITY}
                </td>

                <!-- remove from cart -->
                <td>
                    <button
                        class="remove-btn pointer"
                        id="btn-remove-${row.ORDER_DETAIL_ID}"
                        type="button">
                        <span class="material-symbols-sharp">
                            delete
                        </span>
                    </button>
                </td>
            </tr>`);
        total += row.QUANTITY * row.PRICE;
                });

        // display sub-total
        document.querySelector(".sub-total--amount").textContent = total.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") + " đ";

        // display total
        document.querySelector(".total-amount").textContent = total.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") + " đ";
            }
            //DISPLAY CART IF USER NOT LOGIN: END



        