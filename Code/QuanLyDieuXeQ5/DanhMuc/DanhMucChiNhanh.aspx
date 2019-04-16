<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/MasterPage.master" AutoEventWireup="true" CodeFile="DanhMucChiNhanh.aspx.cs" Inherits="DanhMuc_DanhMucKhachHang" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script>
        var idKhachHangdachon = 0;
        function XemChiTiet(idKhachHang) {
            if ($("#tr_" + idKhachHang).attr("hidden") == "hidden") {
                $("#tr_" + idKhachHang).removeAttr("hidden");
                if (idKhachHang != idKhachHangdachon)
                    $("#tr_" + idKhachHangdachon).attr("hidden", "hidden");
            }
            else {
                $("#tr_" + idKhachHang).attr("hidden", "hidden");
            }
            idKhachHangdachon = idKhachHang;
        }
        function DeleteKhachHang(idKhachHang) {
            if (confirm("Bạn có muốn xóa không ?")) {
                //alert(idSlide);
                var xmlhttp;
                if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
                    xmlhttp = new XMLHttpRequest();
                }
                else {// code for IE6, IE5
                    xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
                }
                xmlhttp.onreadystatechange = function () {
                    if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                        if (xmlhttp.responseText == "True")
                            window.location.reload();
                        else
                            alert("Lỗi !")
                    }
                }
                xmlhttp.open("GET", "../Ajax.aspx?Action=DeleteKhachHang&idKhachHang=" + idKhachHang, true);
                xmlhttp.send();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content">
        <form runat="server">
            <div class="container-fluid">
                <div class="form-group row-fluid">
                    <div class="span4 title1">
                        <b>DANH MỤC CHI NHÁNH</b>
                    </div>
                    <div class="span8 CanhPhai">
                        <asp:LinkButton class="btn btn-primary" runat="server" OnClick="btTimKiem_Click"><i class="fa fa-search"></i><span>Tìm kiếm</span></asp:LinkButton>
                        <a class="btn btn-primary" href="DanhMucChiNhanh-CapNhat.aspx"><i class="fa fa-plus"></i><span>Thêm mới</span></a>
                    </div>
                </div>
                <div class="form-group row-fluid">
                    <div class="col-md-6">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="fa fa-user"></i></span>
                            <input type="text" class="form-control" placeholder="Tìm kiếm theo tên chi nhánh" id="txtTenKhachHang" runat="server" />
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="fa fa-phone"></i></span>
                            <input type="text" class="form-control" placeholder="Tìm kiếm theo số điện thoại chi nhánh" id="txtSoDienThoai" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="row-fluid">
                    <div class="span12">
                        <div id="dvDanhSachKhachHang" style="overflow: auto; margin-top: 10px" runat="server">
                        </div>
                    </div>
                </div>
                <!-- /.content -->
            </div>
        </form>
    </div>
    <link href="../plugins/iCheck/all.css" rel="stylesheet" />
    <script src="../plugins/iCheck/icheck.min.js"></script>

    <script>

        //Flat red color scheme for iCheck
        $('input[type="checkbox"].flat-red, input[type="radio"].flat-red').iCheck({
            checkboxClass: 'icheckbox_flat-green',
            radioClass: 'iradio_flat-green'
        });

    </script>
</asp:Content>

