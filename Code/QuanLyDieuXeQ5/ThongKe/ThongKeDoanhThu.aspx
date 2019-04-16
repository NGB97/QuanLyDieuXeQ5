<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/MasterPage.master" AutoEventWireup="true" CodeFile="ThongKeDoanhThu.aspx.cs" Inherits="ThongKe_ThongKeNhanVienThuTien" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      <link href="../dist/jquery-ui-1.11.3/jquery-ui.css" rel="stylesheet" />
    <script src="../dist/jquery-ui-1.11.3/jquery-ui.js"></script>

    <link href="../cssdatepicker/pikaday.css" rel="stylesheet" />
    <script src="../cssdatepicker/pikaday.js"></script>
    <script>
        window.onload = function () {
         //   KhachHangAutocomplete();
         //   NguoiThuTienAutocomplete();
        }

        function LoadPopupHangGuiXem(idDonHang,NgayLap) {
    //        alert(idDonHang);
            var xmlhttp;
            if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
                xmlhttp = new XMLHttpRequest();
            }
            else {// code for IE6, IE5
                xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
            }
            xmlhttp.onreadystatechange = function () {
                if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                    if (xmlhttp.responseText != "") {
                        //alert(xmlhttp.responseText);
                        var txt = xmlhttp.responseText;

                        // $("#dvXemSanPham").html(txt); MoXemQuyCach();
                        // window.location.hash = IDPhieuXuat;
                        $("#dvXemChiTietDonHang").html(txt);
                    }
                    else {
                        alert("Không có chi tiết để xem!")
                    }

                }
            }
            xmlhttp.open("GET", "../Ajax.aspx?Action=LoadPopupHangGuiXem&idNguoiDung=" + idDonHang + "&NgayLap=" + NgayLap, true);
            xmlhttp.send();

            MoXemChiTietDonHang();
        }
        function LoadPopupHangNhanXem(idDonHang, NgayLap) {
            //        alert(idDonHang);
            var xmlhttp;
            if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
                xmlhttp = new XMLHttpRequest();
            }
            else {// code for IE6, IE5
                xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
            }
            xmlhttp.onreadystatechange = function () {
                if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                    if (xmlhttp.responseText != "") {
                        //alert(xmlhttp.responseText);
                        var txt = xmlhttp.responseText;

                        // $("#dvXemSanPham").html(txt); MoXemQuyCach();
                        // window.location.hash = IDPhieuXuat;
                        $("#dvXemChiTietDonHang").html(txt);
                    }
                    else {
                        alert("Không có chi tiết để xem!")
                    }

                }
            }
            xmlhttp.open("GET", "../Ajax.aspx?Action=LoadPopupHangNhanXem&idNguoiDung=" + idDonHang + "&NgayLap=" + NgayLap, true);
            xmlhttp.send();

            MoXemChiTietDonHang();
        }

        function LoadPopupThuNoXem(idDonHang, NgayLap) {
            //        alert(idDonHang);
            var xmlhttp;
            if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
                xmlhttp = new XMLHttpRequest();
            }
            else {// code for IE6, IE5
                xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
            }
            xmlhttp.onreadystatechange = function () {
                if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                    if (xmlhttp.responseText != "") {
                        //alert(xmlhttp.responseText);
                        var txt = xmlhttp.responseText;

                        // $("#dvXemSanPham").html(txt); MoXemQuyCach();
                        // window.location.hash = IDPhieuXuat;
                        $("#dvXemChiTietDonHang").html(txt);
                    }
                    else {
                        alert("Không có chi tiết để xem!")
                    }

                }
            }
            xmlhttp.open("GET", "../Ajax.aspx?Action=LoadPopupThuNoXem&idNguoiDung=" + idDonHang + "&NgayLap=" + NgayLap, true);
            xmlhttp.send();

            MoXemChiTietDonHang();
        }
        function LoadPopupCPNTraXem(idDonHang, NgayLap) {
            //        alert(idDonHang);
            var xmlhttp;
            if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
                xmlhttp = new XMLHttpRequest();
            }
            else {// code for IE6, IE5
                xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
            }
            xmlhttp.onreadystatechange = function () {
                if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                    if (xmlhttp.responseText != "") {
                        //alert(xmlhttp.responseText);
                        var txt = xmlhttp.responseText;

                        // $("#dvXemSanPham").html(txt); MoXemQuyCach();
                        // window.location.hash = IDPhieuXuat;
                        $("#dvXemChiTietDonHang").html(txt);
                    }
                    else {
                        alert("Không có chi tiết để xem!")
                    }

                }
            }
            xmlhttp.open("GET", "../Ajax.aspx?Action=LoadPopupCPNTraXem&idNguoiDung=" + idDonHang + "&NgayLap=" + NgayLap, true);
            xmlhttp.send();

            MoXemChiTietDonHang();
        }
        function LoadPopupCPNNhanXem(idDonHang, NgayLap) {
                //        alert(idDonHang);
                var xmlhttp;
                if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
                    xmlhttp = new XMLHttpRequest();
                }
                else {// code for IE6, IE5
                    xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
                }
                xmlhttp.onreadystatechange = function () {
                    if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                        if (xmlhttp.responseText != "") {
                            //alert(xmlhttp.responseText);
                            var txt = xmlhttp.responseText;

                            // $("#dvXemSanPham").html(txt); MoXemQuyCach();
                            // window.location.hash = IDPhieuXuat;
                            $("#dvXemChiTietDonHang").html(txt);
                        }
                        else {
                            alert("Không có chi tiết để xem!")
                        }

                    }
                }
                xmlhttp.open("GET", "../Ajax.aspx?Action=LoadPopupCPNNhanXem&idNguoiDung=" + idDonHang + "&NgayLap=" + NgayLap, true);
                xmlhttp.send();

                MoXemChiTietDonHang();
            }
        function LoadPopupCODNhanXem(idDonHang, NgayLap) {
                //        alert(idDonHang);
                var xmlhttp;
                if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
                    xmlhttp = new XMLHttpRequest();
                }
                else {// code for IE6, IE5
                    xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
                }
                xmlhttp.onreadystatechange = function () {
                    if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                        if (xmlhttp.responseText != "") {
                            //alert(xmlhttp.responseText);
                            var txt = xmlhttp.responseText;

                            // $("#dvXemSanPham").html(txt); MoXemQuyCach();
                            // window.location.hash = IDPhieuXuat;
                            $("#dvXemChiTietDonHang").html(txt);
                        }
                        else {
                            alert("Không có chi tiết để xem!")
                        }

                    }
                }
                xmlhttp.open("GET", "../Ajax.aspx?Action=LoadPopupCODNhanXem&idNguoiDung=" + idDonHang + "&NgayLap=" + NgayLap, true);
                xmlhttp.send();

                MoXemChiTietDonHang();
            }
        function LoadPopupCODTraGuiXem(idDonHang, NgayLap) {
                //        alert(idDonHang);
                var xmlhttp;
                if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
                    xmlhttp = new XMLHttpRequest();
                }
                else {// code for IE6, IE5
                    xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
                }
                xmlhttp.onreadystatechange = function () {
                    if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                        if (xmlhttp.responseText != "") {
                            //alert(xmlhttp.responseText);
                            var txt = xmlhttp.responseText;

                            // $("#dvXemSanPham").html(txt); MoXemQuyCach();
                            // window.location.hash = IDPhieuXuat;
                            $("#dvXemChiTietDonHang").html(txt);
                        }
                        else {
                            alert("Không có chi tiết để xem!")
                        }

                    }
                }
                xmlhttp.open("GET", "../Ajax.aspx?Action=LoadPopupCODTraGuiXem&idNguoiDung=" + idDonHang + "&NgayLap=" + NgayLap, true);
                xmlhttp.send();

                MoXemChiTietDonHang();
            }
        function LoadPopupChiKhacXem(idDonHang, NgayLap) {
                //        alert(idDonHang);
                var xmlhttp;
                if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
                    xmlhttp = new XMLHttpRequest();
                }
                else {// code for IE6, IE5
                    xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
                }
                xmlhttp.onreadystatechange = function () {
                    if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                        if (xmlhttp.responseText != "") {
                            //alert(xmlhttp.responseText);
                            var txt = xmlhttp.responseText;

                            // $("#dvXemSanPham").html(txt); MoXemQuyCach();
                            // window.location.hash = IDPhieuXuat;
                            $("#dvXemChiTietDonHang").html(txt);
                        }
                        else {
                            alert("Không có chi tiết để xem!")
                        }

                    }
                }
                xmlhttp.open("GET", "../Ajax.aspx?Action=LoadPopupChiKhacXem&idNguoiDung=" + idDonHang + "&NgayLap=" + NgayLap, true);
                xmlhttp.send();

                MoXemChiTietDonHang();
            }
        
        function MoXemChiTietDonHang() {
            document.getElementById('lightXemChiTietDonHang').style.display = 'block';
            document.getElementById('fadeXemChiTietDonHang').style.display = 'block';
        }
        function DongXemChiTietDonHang() {
            document.getElementById('lightXemChiTietDonHang').style.display = 'none';
            document.getElementById('fadeXemChiTietDonHang').style.display = 'none';
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <link href="../cssdatepicker/pikaday.css" rel="stylesheet" />
    <script src="../cssdatepicker/pikaday.js"></script>
    <form runat="server">
    <div class="content-wrapper">
            <!-- Main content -->
                <div class="container-fluid">
                                <div class="form-group row-fluid">
                                    <div class="span4 title1">
                                        <b>THÔNG KÊ DOANH THU THEO NGÀY</b>
                                    </div>
                                    <div class="span8 CanhPhai">
                                       
                                        <%--<a class="btn btn-primary" href="QuanLyChiTieu-CapNhat.aspx"><i class="fa fa-plus"></i><span>Thêm mới</span></a>--%>
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
                                  
                  <div class="col-md-4">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                            <input type="text" class="form-control" placeholder="Tìm kiếm theo từ ngày" id="txtTuNgay" runat="server"  />
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                            <input type="text" class="form-control" placeholder="Tìm kiếm theo đến ngày" id="txtDenNgay" runat="server"  />
                        </div>
                    </div>
                                    <div hidden="hidden" class="col-lg-4">
                                        <div class="input-group">
                                            <span class="input-group-addon"><i class="fa fa-child"></i></span>
                                            <input type="text" class="form-control" placeholder="Tìm kiếm theo người gửi" id="txtNguoiGui" runat="server" />
                                        </div>
                                    </div>


                                    <div class="col-lg-4">
                                        <div style="padding-left:10px">
                                        <asp:LinkButton class="btn btn-primary" runat="server" OnClick="btTimKiem_Click"><i class="fa fa-search"></i><span> Tìm kiếm </span></asp:LinkButton>
                                             <asp:LinkButton class="btn btn-primary" runat="server" OnClick="Reload_Click"><i class="fa fa-refresh"></i><span> Refresh </span></asp:LinkButton>
                                            <asp:LinkButton class="btn btn-primary" runat="server" OnClick="btXuatExcel_Click"><i class="fa fa-file-excel-o"></i><span> Xuất Excel</span></asp:LinkButton>
                                            </div>
                                    </div>

                                   
                                  
                                </div>
                                <div class="row-fluid">


                                    <div class="span12">




                                        <div id="dvThongKeNguoiGui" style="overflow: auto; margin-top: 10px" runat="server">
                                        </div>


                                    </div>


                                </div>
                </div>
            <!-- /.content -->
        </div>
         <!--Popup xem chi tiết đơn hàng-->
        <div id="lightXemChiTietDonHang" class="white_content" style="top: 10%; width: 70%; left: 15%; height: 80%;">
            <div class="quick-actions_homepage">
                <div id="dvXemChiTietDonHang" class="quick-actions_homepage">
                </div>
            </div>
        </div>
        <div id="fadeXemChiTietDonHang" onclick="DongXemChiTietDonHang()" class="black_overlay"></div>
        <!--End popup--->

        <script src="../plugins/datetimePicker1/build/jquery.datetimepicker.full.js"></script>
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

