<!-- Start of edit address modal -->
    <!-- use modal of bootstrap to style edit profile. modified 10/27/2023 by Quyen -->
    <!-- Button trigger modal -->
    <!-- Modal -->
    <div class="modal fade" id="edit-profile" tabindex="-1" aria-labelledby="modal-profile" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">

                <div class="modal-header" style="border-bottom: none;">
                    <h1 class="modal-title fs-5" id="modal-profile">SỬA THÔNG TIN</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <form>
                        <div class="row row-gap-3">
                            <div class="form-floating">
                                <input type="text" class="form-control" id="name" placeholder="Họ và tên*" required>
                                <label for="name">Họ và tên*</label>
                            </div>

                            <div class="form-floating">
                                <input type="date" class="form-control" id="dateofbirth" required>
                                <label for="phonenumber">Ngày sinh*</label>
                            </div>

                            <div>
                                <input type="radio" id="nam" name="gioitinh">
                                <label for="radio1">Nam</label>

                                <input type="radio" id="nu" name="gioitinh">
                                <label for="radio2">Nữ</label>

                                <input type="radio" id="khac" name="gioitinh">
                                <label for="radio3">Khác</label>
                            </div>

                            <div class="btn-container">
                                <button class="btn btn-confirm" onclick="savechange()" style="width: 100%;">Lưu thay
                                    đổi</button>
                                <button class="btn btn-cancel" onclick="cancel()" data-bs-dismiss="modal"
                                    style="width: 100%;">Hủy</button>
                            </div>
                        </div>

                    </form>
                </div>
            </div>
        </div>
    </div>
    <!-- End of edit address modal -->