<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/MasterPage.master" AutoEventWireup="true" CodeFile="DanhMucNguoiDung.aspx.cs" Inherits="DanhMuc_DanhMucNguoiDung" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script>
        function DeleteNguoiDung(idNguoiDung) {
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
                xmlhttp.open("GET", "../Ajax.aspx?Action=DeleteNguoiDung&idNguoiDung=" + idNguoiDung, true);
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
                        <b>NGƯỜI DÙNG</b>
                    </div>
                    <div class="span8 CanhPhai">
                        <asp:LinkButton class="btn btn-primary" runat="server" OnClick="btTimKiem_Click"><i class="fa fa-search"></i><span>Tìm kiếm</span></asp:LinkButton>
                                        <a class="btn btn-primary" href="DanhMucNguoiDung-CapNhat.aspx"><i class="fa fa-plus"></i><span>Thêm mới</span></a>
                        <%--<a class="btn btn-primary" href="DanhMucHangHoa-CapNhat.aspx"><i class="fa fa-cloud-download"></i><span style="margin-left: 5px;">Import</span></a>
                                            <a class="btn btn-primary" href="DanhMucHangHoa-CapNhat.aspx"><i class="fa fa-cloud-upload"></i><span style="margin-left: 5px;">Export</span></a>--%>
                    </div>

                </div>
                <div class="form-group row-fluid">
                    <div class="col-md-6">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="fa fa-connectdevelop"></i></span>
                            <input type="text" class="form-control" placeholder="Tìm kiếm người dùng theo Họ Tên" id="txtHoTen" runat="server" />
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="fa fa-cog"></i></span>
                            <select type="text" id="slQuyen" runat="server">
                            </select>
                        </div>
                    </div>
                </div>
                <div class="row-fluid">
                    <div class="span12">
                        <div id="dvDanhSachNguoiDung" style="overflow: auto; margin-top: 10px" runat="server">
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div>
    <link href="../plugins/iCheck/all.css" rel="stylesheet" />
    <script src="../plugins/iCheck/icheck.min.js"></script>

    <script>

        //Flat red color scheme for iCheck

        $("#ContentPlaceHolder1_slQuyen").select2();

    </script>

</asp:Content>

