<%@ Page Title="" enableEventValidation="false" Language="C#" MasterPageFile="~/Layout/MasterPage.master" AutoEventWireup="true" CodeFile="CongNoCOD-CapNhat.aspx.cs" Inherits="QuanLyDonHang_QuanLyDonHang_CapNhat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../dist/jquery-ui-1.11.3/jquery-ui.js"></script>
    <link href="../dist/jquery-ui-1.11.3/jquery-ui.css" rel="stylesheet"/>
    <link href="../cssdatepicker/pikaday.css" rel="stylesheet" />
    <script src="../cssdatepicker/pikaday.js"></script>
    <script>
        window.onload = function () {
            KhachHangAutocomplete();
            LoadTienTraNo();
            LoadDSChiTietDonHang('<%= Request.QueryString["idDonHang"] %>');
        }
        function LoadTienTraNo(a) {
            var SoTienNo = Number($("#ContentPlaceHolder1_txtSoTienNo").val().replace(/\./g, ""));
            var SoTienTra = Number($("#ContentPlaceHolder1_txtSoTienTra1").val().replace(/\./g, ""));
            var SoTienConLai = "";
            if (!isNaN(SoTienNo) && !isNaN(SoTienTra))
                SoTienConLai = (SoTienNo - SoTienTra);
            $("#ContentPlaceHolder1_txtConLai").val(SoTienConLai.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1."));
            format_curency(a);


        }
        function TinhTienCuoc() {
            var idKhachHang = document.getElementById("ContentPlaceHolder1_hdIdKhachHang").value;
            var MaLoaiCuoc = document.getElementById("ContentPlaceHolder1_slLoaiCuoc").value;
            var SoLuong = document.getElementById("ContentPlaceHolder1_txtSoLuong1").value;
            //alert(SoLuong);
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
                        document.getElementById("ContentPlaceHolder1_txtTienCuoc1").value = xmlhttp.responseText;
                    }
                }
            }
            xmlhttp.open("GET", "../Ajax.aspx?Action=TinhTienCuoc&idKhachHang=" + idKhachHang + "&MaLoaiCuoc=" + MaLoaiCuoc + "&SoLuong=" + SoLuong, true);
            xmlhttp.send();
        }
        function DeleteChiTietDonHang(idChiTietDonHang) {
            if (confirm("Bạn có muốn xóa chi tiết đơn hàng này không ?")) {
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
                            LoadDSChiTietDonHang('<%= Request.QueryString["idDonHang"] %>');
                        else
                            alert("Lỗi !")
                    }
                }
                xmlhttp.open("GET", "../Ajax.aspx?Action=DeleteChiTietDonHang&idChiTietDonHang=" + idChiTietDonHang, true);
                xmlhttp.send();
            }
        }
            function LoadChiTietDonHang(idChiTietDonHang) {
                //alert(idSanPham);
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
                            var arrChiTietDonHang = xmlhttp.responseText.split("|~~|");
                            document.getElementById("ContentPlaceHolder1_slLoaiSanPham1").value = arrChiTietDonHang[0];
                            document.getElementById("ContentPlaceHolder1_txtSoLuong1").value = arrChiTietDonHang[1];
                            document.getElementById("ContentPlaceHolder1_txtTienHang1").value = arrChiTietDonHang[2];
                            //document.getElementById("ContentPlaceHolder1_txtTienCuoc1").value = arrChiTietDonHang[3];
                            document.getElementById("ContentPlaceHolder1_dvButtonChiTietDonHang").innerHTML = "<input id='btCapNhatChiTietDonHang' type='button' value='Sửa' onclick='CapNhatChiTietDonHang(\"SỬA\", \"\",\"" + idChiTietDonHang + "\")' class='btn btn-primary btn-flat' />";
                            MoThemChiTietDonHang();
                        }
                    }
                }
                xmlhttp.open("GET", "../Ajax.aspx?Action=LoadChiTietDonHang&idChiTietDonhang=" + idChiTietDonHang, true);
                xmlhttp.send();
            }
            function LoadDSChiTietDonHang(idDonHang) {
                //alert(idSanPham);
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
                            DongThemChiTietDonHang();
                            document.getElementById("dvDanhSachChiTietDonHang").innerHTML = xmlhttp.responseText;
                        }
                    }
                }
                xmlhttp.open("GET", "../Ajax.aspx?Action=LoadDSChiTietDonHang&idDonHang=" + idDonHang, true);
                xmlhttp.send();
            }
            
            function CapNhatChiTietDonHang(Loai, idDonHang, idChiTietDonHang) {
                var idLoaiSanPham = document.getElementById("ContentPlaceHolder1_slLoaiSanPham1").value.replace("&", "Và").replace("'", "").replace("|~~|", "").replace("|~~~~|", "");
                var SoLuong = document.getElementById("ContentPlaceHolder1_txtSoLuong1").value.replace("&", "Và").replace("'", "").replace("|~~|", "").replace("|~~~~|", "");
                var TienHang = document.getElementById("ContentPlaceHolder1_txtTienHang1").value.replace("&", "Và").replace("'", "").replace("|~~|", "").replace("|~~~~|", "");
                //var TienCuoc = document.getElementById("ContentPlaceHolder1_txtTienCuoc1").value.replace("&", "Và").replace("'", "").replace("|~~|", "").replace("|~~~~|", "");
                var TienCuoc = 0;
                if (idLoaiSanPham != "") {
                    var xmlhttp;
                    if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
                        xmlhttp = new XMLHttpRequest();
                    }
                    else {// code for IE6, IE5
                        xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
                    }
                    xmlhttp.onreadystatechange = function () {
                        if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                            if (xmlhttp.responseText == "True") {
                                //Load quy cách
                                LoadDSChiTietDonHang('<%= Request.QueryString["idDonHang"] %>');
                                //Đóng
                            }
                            if (xmlhttp.responseText == "False") {
                                alert("Lỗi, bạn vui lòng kiểm tra lại dữ liệu!");
                            }
                            if (xmlhttp.responseText == "DaTonTai") {
                                alert("Loại sản phẩm đã tồn tại!");
                            }
                        }
                    }
                    if (Loai == "THÊM")
                        xmlhttp.open("GET", "../Ajax.aspx?Action=ThemChiTietDonHang&idDonHang=" + idDonHang + "&idLoaiSanPham=" + idLoaiSanPham + "&SoLuong=" + SoLuong + "&TienHang=" + TienHang + "&TienCuoc=" + TienCuoc, true);
                    else
                        xmlhttp.open("GET", "../Ajax.aspx?Action=SuaChiTietDonHang&idChiTietDonHang=" + idChiTietDonHang + "&idDonHang=" + idDonHang + "&idLoaiSanPham=" + idLoaiSanPham + "&SoLuong=" + SoLuong + "&TienHang=" + TienHang + "&TienCuoc=" + TienCuoc, true);
                    xmlhttp.send();
                }
                else {
                    alert("Bạn chưa chọn loại sản phẩm!");
                }
            }
            function ThemMoiChiTietDonHang() {
                ResetChiTietDonHang();
                document.getElementById("ContentPlaceHolder1_dvButtonChiTietDonHang").innerHTML = "<input id='btCapNhatChiTietDonHang' type='button' value='Thêm' onclick='CapNhatChiTietDonHang(\"THÊM\", \"" + '<%= Request.QueryString["idDonHang"] %>' + "\",\"\")' class='btn btn-primary btn-flat' />";
                MoThemChiTietDonHang();
                //Load giá cước
                /*var idKhachHang = document.getElementById("ContentPlaceHolder1_hdIdKhachHang").value;
                var xmlhttp;
                if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
                    xmlhttp = new XMLHttpRequest();
                }
                else {// code for IE6, IE5
                    xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
                }
                xmlhttp.onreadystatechange = function () {
                    if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                        if (xmlhttp.responseText != "")
                            document.getElementById("ContentPlaceHolder1_txtTienCuoc1").value = xmlhttp.responseText;
                        }
                    }
                    xmlhttp.open("GET", "../Ajax.aspx?Action=LoadGiaCuoc&idKhachHang=" + idKhachHang, true);
                    xmlhttp.send();*/
            }
            function ResetChiTietDonHang() {
                document.getElementById("ContentPlaceHolder1_slLoaiSanPham1").value = "0";
                document.getElementById("ContentPlaceHolder1_txtSoLuong1").value = "";
                document.getElementById("ContentPlaceHolder1_txtTienHang1").value = "";
                //document.getElementById("ContentPlaceHolder1_txtTienCuoc1").value = "";
            }
            function MoThemChiTietDonHang() {
                document.getElementById('lightThemChiTietDonHang').style.display = 'block';
                document.getElementById('fadeThemChiTietDonHang').style.display = 'block';
            }
            function DongThemChiTietDonHang() {
                document.getElementById('lightThemChiTietDonHang').style.display = 'none';
                document.getElementById('fadeThemChiTietDonHang').style.display = 'none';
            }
            function GetMaDonHang(idKhachHang) {
                //alert(idSanPham);
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
                            document.getElementById("ContentPlaceHolder1_txtMaDonHang").value = xmlhttp.responseText;
                        }
                    }
                }
                xmlhttp.open("GET", "../Ajax.aspx?Action=GetMaDonHang&idKhachHang="+idKhachHang, true);
                xmlhttp.send();
            }
            function KhachHangAutocomplete() {
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
                            var txt = xmlhttp.responseText
                                   .replace(/[\"]/g, '\\"')
                                  .replace(/[\\]/g, '\\\\')
                                  .replace(/[\/]/g, '\\/')
                                  .replace(/[\b]/g, '\\b')
                                  .replace(/[\f]/g, '\\f')
                                  .replace(/[\n]/g, '\\n')
                                  .replace(/[\r]/g, '\\r')
                                  .replace(/[\t]/g, '\\t');

                            var listKhachHangAutocomplete = eval("(" + xmlhttp.responseText + ")");
                            //alert(listKhuVucAutocomplete.toString());
                            //listKhuVucAutocomplete = [{ value: 'Công ty CP CT VIỆTTRONICS', label: 'Công ty CP CT VIỆTTRONICS', id: '2' }];
                            $("#ContentPlaceHolder1_txtMaKhachHang").autocomplete({

                                minLength: 0,
                                source: listKhachHangAutocomplete,
                                focus: function (event, ui) {
                                    $("#ContentPlaceHolder1_txtMaKhachHang").val(ui.item.value);
                                    return false;
                                },
                                select: function (event, ui) {
                                    GetMaDonHang(ui.item.id);
                                    $("#ContentPlaceHolder1_txtMaKhachHang").val(ui.item.value);
                                    $("#ContentPlaceHolder1_txtNguoiGui").val(ui.item.ten);
                                    $("#ContentPlaceHolder1_hdIdKhachHang").val(ui.item.id);
                                    //$( "#results").text($("#topicID").val());    
                                    //alert($("#hdIdKhuVuc").val());
                                    return false;
                                }
                            }).focus(function () {
                                $(this).autocomplete("search", "");
                            })
                        }
                        else {
                            alert("Lỗi get tên nhân viên !")
                        }

                    }
                }
                xmlhttp.open("GET", "../Ajax.aspx?Action=KhachHangAutocomplete", true);
                xmlhttp.send();
            }
            function LoadTongTien(a) {
                var money = a.value.replace(/\./g, "");
                if (isNaN(money) == true) {
                    money = money.substring(0, money.length - 1);
                }
                a.value = money.replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1.");
                var TienHang = $("#ContentPlaceHolder1_txtTienHang").val().replace(/\./g,"");
                var TienCuoc = $("#ContentPlaceHolder1_txtTienCuoc").val().replace(/\./g, "");
                var PhuPhi = $("#ContentPlaceHolder1_txtPhuPhi").val().replace(/\./g, "");
                $("#ContentPlaceHolder1_txtTongTien").val((Number(TienHang) + Number(TienCuoc) + Number(PhuPhi)).toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1."));
            }
            function slLoaichange() {
                if ($("#ContentPlaceHolder1_slLoai").val() == "2")
                    $("#pLoaiNCCKH").html("Nhà cung cấp(*):");
                else
                    $("#pLoaiNCCKH").html("Khách hàng(*) :");
            }
            function loadtien() {
                var s = Number($("ContentPlaceHolder1_txtSoTienTra").val()).toString();
                if (s != "NaN")
                    $("#ContentPlaceHolder1_txtConLai").val(s);
            }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">  
    
    <form runat="server">
 
  <!-- Main content -->
 
<%--    <div class="title1"><a href="QuanLyDonHang.aspx"><i class="fa fa-step-backward"></i> Quản lý đơn hàng</a></div>--%>

        <div class="content-wrapper">
            <div class="container-fluid">
                <div style="padding-left: 2%">
                    <div class="row-fluid paddingbuttom30">
                        <div class="span12 title1">
                            <div class="span6">
                                <a href="../QuanLyXuatKho/QuanLyXuatKho.aspx" runat="server"><i class="fa fa-arrow-left" style="padding-right: 5px"></i>Quay lại</a>
                            </div>
                            <div class="span6">
                                <b id="dvTitle" runat="server">CẬP NHẬT CÔNG NỢ</b>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-6">
                        <div class="col-md-12">
                            <div class="col-md-3">
                                <p>Mã phiếu trả (*) : </p>
                            </div>
                            <div class="col-md-7">
                                <input placeholder="Mã phiếu trả" disabled="" class="form-control" data-val="true" data-val-required="" id="txtMaPhieuTra" runat="server" name="Content.ContentName" type="text" value="" />
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-3">
                                <p>Mã đơn hàng: </p>
                            </div>
                            <div class="col-md-7">
                                <input placeholder="" disabled="" class="form-control" data-val="true" data-val-required="" id="txtTenKhachHang" runat="server" name="Content.ContentName" type="text" value="" />
                            </div>
                        </div>
                        

                          <div class="col-md-12">
                            <div class="col-md-3">
                                <p>Ngày Trả  (*) : </p>
                            </div>
                            <div class="col-md-7">
                                <input placeholder="Ngày trả" class="form-control" data-val="true" data-val-required="" id="txtNgayLap" runat="server" name="Content.ContentName" type="text" value="" />
                            </div>
                        </div>
                    </div>
                    
                    <div class="col-md-6">
                        <div class="col-md-12">
                            <div class="col-md-3">
                                <p>Số tiền nợ  (*) : </p>
                            </div>
                            <div class="col-md-7">
                                <input placeholder="Số tiền nợ" disabled="" class="form-control" data-val="true" data-val-required="" id="txtSoTienNo" runat="server" name="Content.ContentName" type="text" value="" />
                            </div>
                        </div>
                        
                        <div class="col-md-12">
                            <div class="col-md-3">
                                <p>Số tiền trả: </p>
                            </div>
                            <div class="col-md-7">
                                <input placeholder="Số tiền trả" oninput="LoadTienTraNo(this)" class="form-control" data-val="true" data-val-required="" id="txtSoTienTra1" runat="server" name="Content.ContentName" type="text" value="" />
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="col-md-3">
                                <p>Còn lại: </p>
                            </div>
                            <div class="col-md-7">
                                <input disabled="" class="form-control" data-val="true" data-val-required="" id="txtConLai" runat="server" name="Content.ContentName" type="text" value="" />
                            </div>
                        </div>

                    </div>
                    <div class="col-md-12" style="text-align: center">
                        <asp:Button ID="btLuu" runat="server" Text="LƯU" class="btn btn-primary btn-flat" OnClick="btLuu_Click" />
                        <asp:Button ID="btHuy" runat="server" Text="HỦY" class="btn btn-primary btn-flat" OnClick="btHuy_Click" />
                    </div>
                    <%--<div class="col-md-12" style="text-align: center">
                        <p style="font-size:20px;color:#868989;text-align:left;padding-top:6px;font-family:Arial,Helvetica,Times New Roman,sans-serif;"><b> Lịch sử trả nợ </b> </p>
                        <table class='table table-bordered table-hover dataTable'>
                            <thead style='white-space: nowrap;'>
                                <tr>
                                    <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                        STT
                                    </th>
                                    <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                        Mã phiếu trả
                                    </th>
                                    <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                        Ngày lập
                                    </th>
                                    <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                        Khách hàng
                                    </th>
                                    <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                        Số tiền
                                    </th>
                                    <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                        Ghi chú
                                    </th>
                                    <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    
                                    </th>
                                </tr>
                            </thead>
                        <tbody>
                            <tr>
                                <td>
                                    1
                                </td>
                                <td>
                                    PTNCC-00001
                                </td>
                                <td>
                                    29/05/2018
                                </td>
                                <td>
                                Nguyễn Văn A
                                </td>
                                <td style='text-align:center;'>
                                    20.000
                                </td>
                                <td style='text-align:center;'>
                                    
                                </td>
                                <td style='text-align:center;vertical-align: inherit;font-size:20px;white-space: nowrap;'>
                                    <a href='#'><i class='fa fa-edit'></i></a>
                                    <a href='#'><i class='fa fa-trash'></i></a>
                                </td>
                            </tr>
                        </tbody>
                                                                      </table>
                    </div>--%>

                    <div id="dvChiTietDonHang" runat="server" class="form-group" style="margin-bottom: 0px; display: none">
                        <label class="col-md-2 control-label" for="Content_ContentName">Loại sản phẩm:</label>
                        <div class="col-md-10">
                            <div style="padding-bottom: 10px">
                                <input class="btn btn-primary btn-flat" style="width: 140px;" value="Thêm chi tiết ĐH" type="button" onclick="ThemMoiChiTietDonHang()" />
                            </div>
                            <div id="dvDanhSachChiTietDonHang">
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
   
   
    <div id="lightThemChiTietDonHang" class="white_content" style="top: 10%; width: 70%; left: 15%; height: 80%;">
        <div class="box">
            <div class="box-body">
                <div style="text-align: center; font-weight: bold; padding: 10px">THÊM LOẠI SẢN PHẨM</div>
                <div id="dvThemChiTietDonHang" style="padding: 10px; text-align: center">
                    <div class="container-fluid">
                        <div style="padding-left: 2%">
                            <div class="col-md-6">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <label class="control-label">Loại sản phẩm(*): </label>
                                    </div>
                                    <div class="col-md-8">
                                        <select class="form-control" id="slLoaiSanPham1" runat="server">
                                            <%--<option>--Chọn--</option>--%>
                                        </select>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <label class="control-label">Số lượng (*): </label>
                                    </div>
                                    <div class="col-md-8">
                                        <input id="txtSoLuong1" runat="server" oninput="TinhTienCuoc()" class="form-control" data-val="true" data-val-required="" name="Content.ContentName" type="text" value="" />
                                    </div>

                                </div>

                            </div>
                            <div class="col-md-6">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <label class="control-label">Tiền hàng: </label>
                                    </div>
                                    <div class="col-md-8">
                                        <input id="txtTienHang1" oninput="format_curency(this)" runat="server" class="form-control" data-val="true" data-val-required="" name="Content.ContentName" type="text" value="" />
                                    </div>
                                </div>
                                <%--<div class="col-md-12">
                                    <div class="col-md-3">
                                        <label class="control-label">Tiền cước: </label>
                                    </div>
                                    <div class="col-md-8">
                                        <input id="txtTienCuoc1" oninput="format_curency(this)" runat="server" class="form-control" data-val="true" data-val-required="" name="Content.ContentName" type="text" value="" />
                                    </div>
                                </div>--%>


                            </div>
                        </div>
                    </div>
                    <div class="box-footer" style="padding-top: 10px;" id="dvButtonChiTietDonHang" runat="server">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="fadeThemChiTietDonHang" onclick="DongThemChiTietDonHang()" class="black_overlay"></div>
    <!--End popup--->

        <link href="../plugins/iCheck/all.css" rel="stylesheet" />
    <script src="../plugins/iCheck/icheck.min.js"></script>
    <script src="../plugins/datetimePicker1/build/jquery.datetimepicker.full.js"></script>
    <script type="text/javascript">
        var picker = new Pikaday(
{
    field: document.getElementById('ContentPlaceHolder1_txtNgayLap'),
    firstDay: 1,
    format: 'DD/MM/YYYY',
    minDate: new Date(1900, 1, 1),
    maxDate: new Date(2030, 12, 31),
    yearRange: [2000, 2030]
});
        var picker = new Pikaday(
    {
        field: document.getElementById('ContentPlaceHolder1_txtNgayDuKienGiao'),
        firstDay: 1,
        format: 'DD/MM/YYYY',
        minDate: new Date(1900, 1, 1),
        maxDate: new Date(3000, 12, 31),
        yearRange: [2000, 3000]
    });
        $('input[type="checkbox"].flat-red, input[type="radio"].flat-red').iCheck({
            checkboxClass: 'icheckbox_flat-green',
            radioClass: 'iradio_flat-green'
        });

        $('#ContentPlaceHolder1_txtSoDienThoaiNguoiNhan').on("input", function () {

            var SoDT = $('#ContentPlaceHolder1_txtSoDienThoaiNguoiNhan').val().trim();

            if ($.trim(SoDT) != "") {
                $.ajax({
                    type: "POST",
                    url: "QuanLyDonHang-CapNhat.aspx/LoadThongTinNguoiNhan",
                    data: "{SoDT: '" + SoDT + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (datas) {
                        if ($.trim(datas.d) != "") {
                            $("#ContentPlaceHolder1_txtNguoiNhan").val(datas.d.split("|~~|")[0]);
                            $("#ContentPlaceHolder1_txtDiaChiNguoiNhan").val(datas.d.split("|~~|")[1]);
                        }
                    },
                    error: function () { }
                });
            }

        })
    </script>

      </form>
</asp:Content>

