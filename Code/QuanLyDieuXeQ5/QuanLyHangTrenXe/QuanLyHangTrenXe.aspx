﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/MasterPage.master" AutoEventWireup="true" CodeFile="QuanLyHangTrenXe.aspx.cs" Inherits="QuanLyChiTieu_QuanLyChiTieu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <link href="../cssdatepicker/pikaday.css" rel="stylesheet" />
    <script src="../cssdatepicker/pikaday.js"></script>
    <script>
        function DeleteChiTieu(idChiTieu) {
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
                xmlhttp.open("GET", "../Ajax.aspx?Action=DeleteChiTieu&idChiTieu=" + idChiTieu, true);
                xmlhttp.send();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <form runat="server">
        <div class="content-wrapper">
            <!-- Main content -->
            <div class="container-fluid">
                <div class="form-group row-fluid">
                    <div class="span4 title1">
                        <b>QUẢN LÝ CHI TIÊU</b>
                    </div>
                    <div class="span8 CanhPhai">
                        <asp:LinkButton class="btn btn-primary" runat="server" OnClick="btTimKiem_Click"><i class="fa fa-search"></i><span>Tìm kiếm</span></asp:LinkButton>
                        <a class="btn btn-primary" href="QuanLyHangTrenXe-CapNhat.aspx"><i class="fa fa-plus"></i><span>Thêm mới</span></a>
                        <%--<a class="btn btn-primary" href="DanhMucHangHoa-CapNhat.aspx"><i class="fa fa-cloud-download"></i><span style="margin-left: 5px;">Import</span></a>
                                        <a class="btn btn-primary" href="DanhMucHangHoa-CapNhat.aspx"><i class="fa fa-cloud-upload"></i><span style="margin-left: 5px;">Export</span></a>--%>
                    </div>

                </div>
                <div class="form-group row-fluid">
                    <%--<div class="col-lg-3">
                                        <div class="input-group">
                                            <span class="input-group-addon"><i class="fa fa-cube"></i></span>
                                            <input type="text" class="form-control" placeholder="Tìm kiếm theo tên người dùng" id="txtMaPhieuNhap" runat="server" />
                                        </div>
                                    </div>--%>
                    <div class="col-lg-6">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                            <input type="text" class="form-control" placeholder="Tìm kiếm theo từ ngày" id="txtTuNgay" runat="server" />
                        </div>
                    </div>
                    <div class="col-lg-6">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                            <input type="text" class="form-control" placeholder="Tìm kiếm theo đến ngày" id="txtDenNgay" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="row-fluid">

                    <div class="span12">

                        <div id="dvDanhSachChiTieu" style="overflow: auto; margin-top: 10px" runat="server">
                        </div>
                    </div>

                </div>
            </div>
            <!-- /.content -->
        </div>

  
    <script type="text/javascript">
        var picker = new Pikaday(
 {
     field: document.getElementById('ContentPlaceHolder1_txtTuNgay'),
     firstDay: 1,
     format: 'DD/MM/YYYY',
     minDate: new Date(1900, 1, 1),
     maxDate: new Date(2020, 12, 31),
     yearRange: [2000, 2020]
 });
        var picker = new Pikaday(
    {
        field: document.getElementById('ContentPlaceHolder1_txtDenNgay'),
        firstDay: 1,
        format: 'DD/MM/YYYY',
        minDate: new Date(1900, 1, 1),
        maxDate: new Date(3000, 12, 31),
        yearRange: [2000, 3000]
    });
    </script>
        </form>
</asp:Content>

