var role_id = null;
$(document).ready(function () {
    
    const account = $(".account");
    
    //lấy session
    $.ajax({
        type: "POST",
        url: "/Account/getSession",
        data: { sessionName: "roleId" },
        success: function (response) {
            if (response)
                role_id = response;
            else
                role_id = null;
                console.log("role" + role_id);
                if (role_id == null || role_id == "") {
                    account.append(`<li class="sub-nav--item hover-underline">
      <a href="#" id="login">ĐĂNG NHẬP</a>
    </li>`);

                    $("#login").click(function () {
                        // Tạo URL mới với tham số truyền vào là tên sản phẩm
                        var url = "/Account/Login";

                        // Chuyển hướng đến trang mới
                        window.location.href = url;
                    });
                }

                if (role_id != null) {
                    var nav = '';
                    if (Number(role_id) == 1) {
                        nav += `<li class="sub-nav--item hover-underline">
                            <a href="/Admin/Order">QUẢN LÝ</a>
                      </li>`
                    }
                    else if (Number(role_id) == 2) {
                        nav += `<li class="sub-nav--item hover-underline">
                            <a href="/Admin/Order">QUẢN LÝ</a>
                      </li>`
                    }

                    // const account = $(".account");
                    const accountHTML = `<a>
                        <span class="material-symbols-outlined"> account_circle </span>
                      </a>
                      <ul class="sub-nav">
                        <li class="sub-nav--item hover-underline">
                          <a href="/Account">TÀI KHOẢN</a>
                        </li>
                        ${nav}
                        <li class="sub-nav--item hover-underline">
                          <a href="/Account/LogOut">LOG OUT</a>
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
    






});
