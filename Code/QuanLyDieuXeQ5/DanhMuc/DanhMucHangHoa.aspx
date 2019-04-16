<%@ Page Language="C#" MasterPageFile="~/Layout/MasterPage.master" AutoEventWireup="true" CodeFile="DanhMucHangHoa.aspx.cs" Inherits="DanhMuc_LoaiCuoc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script>
        function DeleteKho(MaLoaiCuoc) {
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
                            alert("Không thể xóa loại cước này !")
                    }
                }
                xmlhttp.open("GET", "../Ajax.aspx?Action=DeleteLoaiCuoc&MaLoaiCuoc=" + MaLoaiCuoc, true);
                xmlhttp.send();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <form runat="server">
        <div class="content-wrapper">
            <!-- Main content -->
            <div class="container-fluid">
                <div class="form-group row-fluid">
                    <div class="span4 title1">
                        <b>DANH MỤC HÀNG HÓA</b>
                    </div>
                    <div class="span8 CanhPhai">
                        <asp:LinkButton class="btn btn-primary" runat="server" OnClick="btTimKiem_Click"><i class="fa fa-search"></i><span>Tìm kiếm</span></asp:LinkButton>
                        <a class="btn btn-primary" href="DanhMucHangHoa-CapNhat.aspx"><i class="fa fa-plus"></i><span>Thêm mới</span></a>
                        <%--<a class="btn btn-primary" href="DanhMucHangHoa-CapNhat.aspx"><i class="fa fa-cloud-download"></i><span style="margin-left: 5px;">Import</span></a>
                                        <a class="btn btn-primary" href="DanhMucHangHoa-CapNhat.aspx"><i class="fa fa-cloud-upload"></i><span style="margin-left: 5px;">Export</span></a>--%>
                    </div>

                </div>
                <div class="form-group row-fluid">
                    <div class="col-md-6">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="fa fa-cube"></i></span>
                            <input type="text" class="form-control" placeholder="Tìm kiếm theo mã hàng hóa" id="txtMa" runat="server" />
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="fa fa-cubes"></i></span>
                            <input type="text" class="form-control" placeholder="Tìm kiếm theo tên hàng hóa" id="txtTen" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="row-fluid">
                    <div class="span12">
                        <div id="dvDanhSachKho" style="overflow: auto; margin-top: 10px" runat="server">
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.content -->
        </div>
    </form>
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