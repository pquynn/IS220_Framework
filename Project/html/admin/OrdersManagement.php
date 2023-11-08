<!-- start: admin navigation -->
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <script src="https://kit.fontawesome.com/f7fcb1a9ac.js"crossorigin="anonymous"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-T3c6CoIi6uLrA9TneNEoa7RxnatzjcDSCmG1MXxSR1GAsXEV/Dwwykc2MPK8M2HN" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-C6RzsynM9kWDrMNeT87bh95OGNyZPhcTNXj1NW7RuBCsyN/o0jlpcV8Qyq46cDfL" crossorigin="anonymous"></script>
    
    <link rel="stylesheet" href="../../css/style-components/base.css">
    <link rel="stylesheet" href="../../css/admin/AdminNavigation.css">
    <link rel="stylesheet" href="../../css/style-components/table.css">
    <link rel="stylesheet" href="../../css/style-components/pagination.css">
    <link rel="stylesheet" href="../../css/admin/ProductsManagement.css">
    <link rel="stylesheet" href="../../css/admin/ModalAddNewProduct.css">
    <link rel="stylesheet" href="../../css/admin/ModalAlert.css">
    <link rel="stylesheet" href="../../css/admin/admin-orders.css"/>
    <title>Danh sách đơn hàng</title>

</head>
<body>
    <!-- start: admin navigation -->
    <div class="nav-container">
        <div class="nav-admin">
            <a href="#" class="nav-admin-logo">
                <!-- change logo color into white. modified 10/18/2023 by Quyen -->
                <img src="../../img/logo-white.png" alt="logo"> 
                <h1>LOGO</h1>
            </a>
    
            <ul class="nav-admin-menu">
                <li><a href="#">Về Cửa Hàng</a></li>
                <li><a href="Dashboard.php">Thống Kê</a></li>
                <li><a href="CategoriesManagement.php">Danh Mục</a></li>
                <li><a href="ProductsManagement.php" class="active">Sản Phẩm</a></li>
                <li><a href="OrdersManagement.php">Đơn Hàng</a></li>
                <li><a href="CustomersManagement.php">Khách Hàng</a></li>
                <li><a href="EmployeesManagement.php">Nhân Viên</a></li>
                <li><a href="BlogManagement.php">Blog</a></li>
            </ul>
    
            <div class="nav-admin-account">
                <a href="#" id="account">
                        <img src="../../img/user.png" alt="Your account">
                        <p>Nguyễn Văn A</p>
                </a>
                <a href="#" id="logout">Đăng Xuất</a>
            </div>
        </div>  
    </div>
    <!-- end: admin navigation -->
    
<!--MAIN SECTION----START-->
<div class="admin-orders">
    <h2 class="section_heading">Danh sách đơn hàng</h2>

    <ol class="breadcrumb">
        <li class= "breadcrumb-item active" aria-current="page">Danh sách đơn hàng</li>
    </ol>
    <!--Orders' date-->
    <div class="date-orders">
        <i class="fa-solid fa-calendar"></i>
        <span>dd/mm/yyyy - dd/mm/yyyy</span>
    </div>

    <!--Dropdown choose status for orders-->
    <div class="choose-status">
            <span>Trạng thái đơn hàng</span>                
            <i class="fa-solid fa-angle-down"></i>

            <ul class="list-status">
                <li>Đang xử lý</li>
                <li>Giao hàng thành công</li>
                <li>Đã hủy</li>  
            </ul>                      
    </div>
    
    <!--Search bar-->
    <div class="admin-search-container">
        <div class="search-bar-box">
            <input type="text" id="search" placeholder="Tìm kiếm..." class="form-control ">
            <a href="#" class="btn-search"><i class="fa-solid fa-magnifying-glass"></i></a>
        </div>
    </div>

    <!--Table list orders-->
    <div class="admin-table">
        <table class="list-orders">
            <tr>
                <th><input type="checkbox"></th>   
                <th>Mã đơn hàng</th>
                <th>Ngày hóa đơn</th>   
                <th>SĐT</th>   
                <th>Tên khách hàng</th>                  
                <th>Địa chỉ</th>
                <th>PTTT</th>   
                <th>Trạng thái</th>   
                <th>Tổng sản phẩm</th>   
                <th>Tổng tiền</th>   
                <th>Thao tác</th>
            </tr>

            <tr>
                <td><input type="checkbox"></td>
                <td>A123</td>
                <td>dd/mm/yyy</td>   
                <td>0912345678</td>   
                <td>ABC DEF GHIJK</td>                  
                <td>123 ABCD</td>
                <td>Momo</td>   
                <td>Giao hàng thành công</td>   
                <td>3</td>   
                <td>$9999</td> 
                <td class="action">
                    <a href="OrderDetail.php"><i class="fa-solid fa-pen"></i></a>
                    <a href="#"><i class="fa-solid fa-trash"></i></a>
                </td> 
            </tr>
        </table>
    </div>
    

    <!--Pagination-->
    <div class="pagination admin">
        <a href="#">&laquo;</a>
        <a class="active" href="#">1</a>
        <a href="#">2</a>
        <a href="#">3</a>
        <a href="#">4</a>
        <a href="#">5</a>
        <a href="#">6</a>
        <a href="#">&raquo;</a>
    </div>
</div>
<!--MAIN SECTION----END-->