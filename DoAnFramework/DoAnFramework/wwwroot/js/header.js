/** @format */
var user_id = null;

$(document).ready(function () {
  // hien tuy chon tai khoan doi voi nguoi dung: bat dau
    const account = $(".account");

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
    getRoleId(user_id);
  function getRoleId(user_id) {
    if (user_id === null) {
      account.append(`<li class="sub-nav--item hover-underline">
      <a href="#" id="login">ĐĂNG NHẬP</a>
    </li>`);

      // $(".order-not-login").text("ĐƠN HÀNG");

      $("#login").click(function () {
        // Tạo URL mới với tham số truyền vào là tên sản phẩm
        var url = "/Account/Login";

        // Chuyển hướng đến trang mới
        window.location.href = url;
      });
    }

      if (user_id != null) {
          //lấy session role id
          $.ajax({
              type: "POST",
              url: "/Account/getSession",
              data: { sessionName: "roleId" },
              success: function (response) {
                  if (response) {
                      console.log(response);
                      var role_id = response;
                      console.log(role_id);
                      var nav = '';
                      if (Number(role_id) == 1) {
                                      nav += `<li class="sub-nav--item hover-underline">
                            <a asp-area="Admin" asp-controller="Order" asp-action="Index">QUẢN LÝ</a>
                      </li>`
                                  }
                                  else if (Number(role_id) == 2) {
                                      nav += `<li class="sub-nav--item hover-underline">
                            <a asp-area="Admin" asp-controller="Order" asp-action="Index">QUẢN LÝ</a>
                      </li>`
                      }

                      // const account = $(".account");
                                      const accountHTML = `<a>
                        <span class="material-symbols-outlined"> account_circle </span>
                      </a>
                      <ul class="sub-nav">
                        <li class="sub-nav--item hover-underline">
                          <a asp-controller="Account" asp-action="Index">TÀI KHOẢN</a>
                        </li>
                        ${nav}
                        <li class="sub-nav--item hover-underline">
                          <a asp-controller="Account" asp-action="LogOut">LOG OUT</a>
                        </li>
                      </ul>`;

                      account.append(accountHTML);
                      // console.log($(".logout"));
                      
                  }
              },
              error: function (error) {
                  console.error("Đã xảy ra lỗi:", error);
              },
          });

    }
  }

  //// hien tuy chon tai khoan doi voi nguoi dung: bat dau

  //// Mở giỏ hàng
  //$("#cartIcon").click(function () {
  //  // Tạo URL mới với tham số truyền vào là tên sản phẩm
  //  var url = "../../store/cart-checkout/cart.php";

  //  // Chuyển hướng đến trang mới
  //  window.location.href = url;
  //});

  // Sự kiện khi tìm kiếm sản phẩm
  $("#search-product").keypress(function (event) {
    // Kiểm tra xem phím được nhấn có phải là phím Enter không (keyCode 13)
    if (event.which === 13) {
      // Lấy giá trị từ trường nhập liệu
      var searchProduct = $(this).val();

      if (searchProduct !== "") {
        // Tạo URL mới với tham số truyền vào là tên sản phẩm
        var url ="Book/searchProduct/" + encodeURIComponent(searchProduct);

        // Chuyển hướng đến trang mới
        window.location.href = url;
      }
    }
  });
});
