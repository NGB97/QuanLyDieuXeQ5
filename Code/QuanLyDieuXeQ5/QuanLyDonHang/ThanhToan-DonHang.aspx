<%@ Page Title="" enableEventValidation="false" Language="C#" MasterPageFile="~/Layout/MasterPage.master" AutoEventWireup="true" CodeFile="ThanhToan-DonHang.aspx.cs" Inherits="QuanLyDonHang_QuanLyDonHang_CapNhat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../dist/jquery-ui-1.11.3/jquery-ui.js"></script>
    <link href="../dist/jquery-ui-1.11.3/jquery-ui.css" rel="stylesheet"/>
    <link href="../cssdatepicker/pikaday.css" rel="stylesheet" />
    <script src="../cssdatepicker/pikaday.js"></script>
    <script>
        window.onload = function () {
            KhachHangAutocomplete();
            TinhTrangAutocomplete();
            TenHangAutocomplete();
            ChiNhanhGuiAutocomplete();
            ChiNhanhNhanAutocomplete();
            LoadTienTraNo();
  
            LoadDSChiTietDonHang('<%= Request.QueryString["idDonHang"] %>');
        }

        function LoadTienTraNo(a) {
            var SoTienNo = Number($("#ContentPlaceHolder1_txtTongCuoc").val().replace(/\./g, ""));
            var SoTienTra = Number($("#ContentPlaceHolder1_txtSoTienTra").val().replace(/\./g, ""));
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
                            //document.getElementById("ContentPlaceHolder1_dvButtonChiTietDonHang").innerHTML = "<input id='btCapNhatChiTietDonHang' type='button' value='Sửa' onclick='CapNhatChiTietDonHang(\"SỬA\", \"\",\"" + idChiTietDonHang + "\")' class='btn btn-primary btn-flat' />";
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

        function DSSanPham() {
            var HangGui = $("#ContentPlaceHolder1_chkHangGui").prop("checked");
            
            if (HangGui == true) {
               
                disableTxt();
                undisableTxt();
       
            }
            else {

                document.getElementById("ContentPlaceHolder1_txtTenHang").disabled = false;
                document.getElementById("ContentPlaceHolder1_txtDonGia").disabled = false;
                document.getElementById("ContentPlaceHolder1_txtSoLuong").disabled = false;
                document.getElementById("ContentPlaceHolder1_txt_ChuyenPhatNhanh").disabled = true;
                document.getElementById("ContentPlaceHolder1_txt_ChuyenPhatNhanh").value = "";
                //  alert(ContentPlaceHolder1_txt_ChuyenPhatNhanh);
            }

    
        }
        function disableTxt() {
            document.getElementById("ContentPlaceHolder1_txtTenHang").disabled = true;
            document.getElementById("ContentPlaceHolder1_txtDonGia").disabled = true;
            document.getElementById("ContentPlaceHolder1_txtSoLuong").disabled = true;
        }
        function undisableTxt() {
            document.getElementById("ContentPlaceHolder1_txt_ChuyenPhatNhanh").disabled = false;
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
            //function GetMaDonHang(idKhachHang) {
            //    //alert(idSanPham);
            //    var xmlhttp;
            //    if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
            //        xmlhttp = new XMLHttpRequest();
            //    }
            //    else {// code for IE6, IE5
            //        xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
            //    }
            //    xmlhttp.onreadystatechange = function () {
            //        if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
            //            if (xmlhttp.responseText != "") {
            //                //alert(xmlhttp.responseText);
            //                document.getElementById("ContentPlaceHolder1_txtMaDonHang").value = xmlhttp.responseText;
            //            }
            //        }
            //    }
            //    xmlhttp.open("GET", "../Ajax.aspx?Action=GetMaDonHang&idKhachHang="+idKhachHang, true);
            //    xmlhttp.send();
            //}
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
                            $("#ContentPlaceHolder1_txtSoDienThoaiNguoiNhan").autocomplete({

                                minLength: 0,
                                source: listKhachHangAutocomplete,
                                focus: function (event, ui) {
                                    $("#ContentPlaceHolder1_txtSoDienThoaiNguoiNhan").val(ui.item.value);
                                    return false;
                                },
                                select: function (event, ui) {
                                 //   GetMaDonHang(ui.item.id);
                                    $("#ContentPlaceHolder1_txtSoDienThoaiNguoiNhan").val(ui.item.value);
                                    $("#ContentPlaceHolder1_txtNguoiNhan").val(ui.item.ten);
                                    $("#ContentPlaceHolder1_txtDiaChiNguoiNhan").val(ui.item.diachi);
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

            function TinhTrangAutocomplete() {
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
                            $("#ContentPlaceHolder1_txtTinhTrangDonHang").autocomplete({

                                minLength: 0,
                                source: listKhachHangAutocomplete,
                                focus: function (event, ui) {
                                    $("#ContentPlaceHolder1_txtTinhTrangDonHang").val(ui.item.value);
                                    return false;
                                },
                                select: function (event, ui) {
                                  //  GetMaDonHang(ui.item.id);
                                    $("#ContentPlaceHolder1_txtTinhTrangDonHang").val(ui.item.value);
                                  //  $("#ContentPlaceHolder1_txtDonGia").val(ui.item.ten);
                                    $("#ContentPlaceHolder1_hdTinhTrangDonHang").val(ui.item.ten);
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
                xmlhttp.open("GET", "../Ajax.aspx?Action=TinhTrangAutocomplete", true);
                xmlhttp.send();
            }

            function TenHangAutocomplete() {
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
                            $("#ContentPlaceHolder1_txtTenHang").autocomplete({

                                minLength: 0,
                                source: listKhachHangAutocomplete,
                                focus: function (event, ui) {
                                    $("#ContentPlaceHolder1_txtTenHang").val(ui.item.value);
                                    return false;
                                },
                                select: function (event, ui) {
                                  //  GetMaDonHang(ui.item.id);
                                    $("#ContentPlaceHolder1_txtTenHang").val(ui.item.value);
                                    $("#ContentPlaceHolder1_txtDonGia").val((ui.item.ten).replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1."));
                                    $("#ContentPlaceHolder1_hdIDTenHang").val(ui.item.id);
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
                xmlhttp.open("GET", "../Ajax.aspx?Action=TenHangAutocomplete", true);
                xmlhttp.send();
            }
            
            function ChiNhanhNhanAutocomplete() {
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
                            $("#ContentPlaceHolder1_txtChiNhanhNhan").autocomplete({

                                minLength: 0,
                                source: listKhachHangAutocomplete,
                                focus: function (event, ui) {
                                    $("#ContentPlaceHolder1_txtChiNhanhNhan").val(ui.item.value);
                                    return false;
                                },
                                select: function (event, ui) {
                                    // GetMaDonHang(ui.item.id);
                                    $("#ContentPlaceHolder1_txttxtChiNhanhNhan").val(ui.item.value);
                                    //  $("#ContentPlaceHolder1_txtDonGia").val((ui.item.ten).replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1."));
                                    $("#ContentPlaceHolder1_hdIDChiNhanhNhan").val(ui.item.id);
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
                xmlhttp.open("GET", "../Ajax.aspx?Action=ChiNhanhGuiAutocomplete", true);
                xmlhttp.send();
            }



            function ChiNhanhGuiAutocomplete() {
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
                            $("#ContentPlaceHolder1_txtChiNhanhGui").autocomplete({

                                minLength: 0,
                                source: listKhachHangAutocomplete,
                                focus: function (event, ui) {
                                    $("#ContentPlaceHolder1_txtChiNhanhGui").val(ui.item.value);
                                    return false;
                                },
                                select: function (event, ui) {
                                   // GetMaDonHang(ui.item.id);
                                    $("#ContentPlaceHolder1_txttxtChiNhanhGui").val(ui.item.value);
                                  //  $("#ContentPlaceHolder1_txtDonGia").val((ui.item.ten).replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1."));
                                    $("#ContentPlaceHolder1_hdIDChiNhanhGui").val(ui.item.id);
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
                xmlhttp.open("GET", "../Ajax.aspx?Action=ChiNhanhGuiAutocomplete", true);
                xmlhttp.send();
            }


            var listSanPham = [];
            function ThemSanPham() {
                if ($("#ContentPlaceHolder1_listSanPham").val() != "")
                    listSanPham = $("#ContentPlaceHolder1_listSanPham").val().split(',');
                TongTien = Number($("#ContentPlaceHolder1_txtTongTien").val().split(' ')[0].replace(/\./g, ''));
                //  var idHangHoa = $("#ContentPlaceHolder1_hdidHangHoa").val();
                //   var MaHangHoa = $("#ContentPlaceHolder1_txtSanPham").val();
                var TenHangHoa = $('#ContentPlaceHolder1_txtTenHang').val();
                var idHanghoa = $('#ContentPlaceHolder1_hdIDTenHang').val();
                //var TenHangHoa1 = $('#select TenHangHoa1 from tb_HangHoa where MaHangHoa = " + TenHangHoa + "');
                var DonGia = $("#ContentPlaceHolder1_txtDonGia").val();

                var SoLuong = $("#ContentPlaceHolder1_txtSoLuong").val();
                //var TinhTrangDonHang = $("#ContentPlaceHolder1_txtTinhTrangDonHang").val();
                var MaTinhTrang = $("#ContentPlaceHolder1_hdTinhTrangDonHang").val();

                // var TrongLuong = $("#ContentPlaceHolder1_txtTrongLuong").text();
                //var TenNhaCungCap = $('#ContentPlaceHolder1_slNhaCungCap').find(":selected").text();
                //alert(NhaCungCap)
               // var SoKhoi = $("#ContentPlaceHolder1_txtSoKhoi").val();
                //var DonGia = $("#ContentPlaceHolder1_txtDonGia").val().replace(/\./g, '').replace(/\,/g, '');
                //var ThanhTien = Number($("#ContentPlaceHolder1_txt_ThanhTien").val().replace(/\./g, '').replace(/\,/g, ''));

                //var HangGui = $("#ContentPlaceHolder1_chkHangGui").prop("checked");


                //var GiaNhap = $("#ContentPlaceHolder1_txtGiaNhap").val();
                //var ThanhTien = SoLuong * GiaNhap.replace(/\./g, '');

                if (TenHangHoa == "" || DonGia == "" || SoLuong == "") {
                    alert("Dũ liệu nhập chưa đủ hoặc không chính xác. Vui lòng nhập lại!");
                    return;
                }
                else {
                    for (var i = 0 ; i < listSanPham.length ; i++) {
                        if (listSanPham[i].split('-')[5] == TenHangHoa) {
                            alert("Bạn đã nhập sản phẩm " + TenHangHoa + ". Vui lòng chọn sản phẩm khác!");
                            return;
                        }

                    }


                    TongTien += Number(DonGia.replace(/\./g,""));
              


                    listSanPham.push(idHanghoa + "-" + SoLuong + "-" + 'DGX' + "-" + DonGia);

                    //else {
                    //    listSanPham.push(idHangHoa + "-" + MaHangHoa + "-" + TenHangHoa + "-" + idNhapMua + "-" + SoLuong + "-" + DonGia + "-" + ThanhTien);
                    //}
                   // alert(listSanPham);
                    $("#ContentPlaceHolder1_listSanPham").val(listSanPham);
                    //alert($("#ContentPlaceHolder1_listSanPham").val())
                    var html = "<tr id='tr_" + (i + 1) + "'>";
                    html += "<td>" + (i + 1) + "</td>";
                    html += "<td style='text-align: center'>" + TenHangHoa + "</td>";

                



                    html += "<td style='text-align: center'>" + SoLuong + "</td>";
                    html += "<td style='text-align: center'>" + DonGia + "</td>";
                    html += "<td style='text-align: center'>" + 'Đợi Giao Xe' + "</td>";
                   // html += "<td style='text-align: center'>" + SoKhoi + "</td>";

                    //html += "<td style='text-align: center'>" + ThanhTien.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1.") + "</td>";
                    html += "<td style='text-align: center'><a style='cursor:pointer' onclick='XoaSanPham(\"" + idHanghoa + "\")'><i class='fa fa-trash'></i></a></td>"

                    // html += "<td style='text-align: center'></td>";
                    html += "</tr>";
                    $("#tr_SanPham").after(html);
                    $("#ContentPlaceHolder1_txtTongTien").val(TongTien.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1.") + " đ");
                    //      $("#ContentPlaceHolder1_txtTongTien").val(TongTien.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1.") + " đ");

                    $("#ContentPlaceHolder1_txtTenHang").val("");


                    $("#ContentPlaceHolder1_txtDonGia").val("");
                    $("#ContentPlaceHolder1_txtSoLuong").val("");
                    $("#ContentPlaceHolder1_slTinhTrangDon").val("");
                 //   $("#ContentPlaceHolder1_txtSoKhoi").val("");
                    //  $("#ContentPlaceHolder1_txt_ThanhTien").val("");
                    //   $("#ContentPlaceHolder1_txtGiaNhap").val("");
                    // TinhTongTien();
                    sortTable();
                }
            }
            function sortTable() {
                var table, rows, switching, i, x, y, shouldSwitch;
                table = document.getElementById("tb-danhsachSP");
                rows = table.getElementsByTagName("TR");

                for (i = 1; i < rows.length ; i++) {
                    x = rows[i].getElementsByTagName("TD")[0];
                    x.innerHTML = (i);
                }
            }

            function XoaSanPham(idSanPham) {
                TongTien = Number($("#ContentPlaceHolder1_txtTongTien").val().split(' ')[0].replace(/\./g, ''));

                if ($("#ContentPlaceHolder1_listSanPham").val() != "")
                    listSanPham = $("#ContentPlaceHolder1_listSanPham").val().split(',');
                alert(idSanPham);
                for (var i = 0; i < listSanPham.length; i++) {
                    if (listSanPham[i].split('-')[0] == idSanPham) {

                        TongTien -= (listSanPham[i].split('-')[3].replace(/\./g, ''));

                        listSanPham.splice(i, 1);

                    }
                }
                $("#ContentPlaceHolder1_listSanPham").val(listSanPham);
                $("#tr_" + idSanPham).remove();

                $("#ContentPlaceHolder1_txtTongTien").val(TongTien.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1.") + " đ");
                sortTable();
                // $("#ContentPlaceHolder1_TongTien").html(TongTien.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1.") + " đ");
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
                                <a href="../QuanLyDonHang/QuanLyDonHang.aspx" runat="server"><i class="fa fa-arrow-left" style="padding-right: 5px"></i>Quay lại</a>
                            </div>
                            <div class="span6">
                                <b id="dvTitle" runat="server">THÊM THÔNG TIN ĐƠN HÀNG</b>
                            </div>
                        </div>
                    </div>

                     <div class="col-md-6">
                        <div class="col-md-12">
                            <div class="col-md-3">
                                <p>Mã phiếu trả (*) : </p>
                            </div>
                            <div class="col-md-7">
                                <input placeholder="Mã phiếu trả"  disabled="" class="form-control" data-val="true" data-val-required="" id="txtMaPhieuTra" runat="server" name="Content.ContentName" type="text" value="" />
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-3">
                                <p>Mã đơn hàng (*) : </p>
                            </div>
                            <div class="col-md-7">
                                <input placeholder="Mã đơn hàng"  disabled="" class="form-control" data-val="true" data-val-required="" id="txtMaDonHang1" runat="server" name="Content.ContentName" type="text" value="" />
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-3">
                                <p>Tên khách hàng: </p>
                            </div>
                            <div class="col-md-7">
                                <input placeholder="" disabled="" class="form-control" data-val="true" data-val-required="" id="txtTenKhachHang" runat="server" name="Content.ContentName" type="text" value="" />
                            <input type="hidden" id="hdIdKhachHang" runat="server" />
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-3">
                                <p>Ngày thanh toán  (*) : </p>
                            </div>
                            <div class="col-md-7">
                                <input placeholder="Ngày thanh toán" class="form-control" data-val="true" data-val-required="" id="txtNgayThanhToan" runat="server" name="Content.ContentName" type="text" value="" />
                            </div>
                        </div>

                    </div>

                     <div class="col-md-6">
                        <div class="col-md-12">
                            <div class="col-md-3">
                                <p>Tổng cước (*) : </p>
                            </div>
                            <div class="col-md-7">
                                <input placeholder="Tổng cước" disabled="" class="form-control" data-val="true" data-val-required="" id="txtTongCuoc" runat="server" name="Content.ContentName" type="text" value="" />
                            </div>
                        </div>
                        
                        <div class="col-md-12">
                            <div class="col-md-3">
                                <p>Thanh Toán: </p>
                            </div>
                            <div class="col-md-7">
                                <input placeholder="Thanh toán" oninput="LoadTienTraNo(this)" class="form-control" data-val="true" data-val-required="" id="txtSoTienTra" runat="server" name="Content.ContentName" type="text" value="" />
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
                        <div class="col-md-12">
                            <div class="col-md-3">
                                <p>Tiền chuyển phát nhanh: </p>
                            </div>
                            <div class="col-md-7">
                                <input disabled="" class="form-control" data-val="true" data-val-required="" id="txt_ChuyenPhatNhanh" runat="server" name="Content.ContentName" type="text" value="" />
                            </div>
                        </div>
                       <div class="col-md-12" id="TienCOD" runat="server">
                            <div class="col-md-3">
                                <p>Tiền COD: </p>
                            </div>
                            <div class="col-md-7">
                                <input  oninput="format_curency(this)" class="form-control" data-val="true" data-val-required="" id="txt_TienCOD" runat="server" name="Content.ContentName" type="text" value="" />
                            </div>
                        </div>
                    </div>

                    

                   <%--       <div class="col-md-12" style="text-align: center;">
                           <%-- <asp:Button ID="btThem" runat="server" Text="Thêm" class="btn btn-primary btn-flat" OnClick="btThem_Click" />
                           <a id="btThem" class="btn btn-primary btn-flat">Thêm</a>
                             <a class="btn btn-primary" onclick="ThemSanPham()" >Thêm</a>
                        </div>--%>
                 <%--           <div class="col-md-12" style="text-align: center">
              <%--          <table id="tb-danhsachSP" class='table table-bordered table-hover dataTable'>
                            <thead style='white-space: nowrap;'>
                                <tr id="tr_SanPham">
                                    <th class='th' style='background-color: #1c9c13; font-size: 15px; color: white;'>STT</th>
                                    <th class='th' style='background-color: #1c9c13; font-size: 15px; color: white;'>Tên Hàng
                                    </th>
                                  
                                      <th class='th' style='background-color: #1c9c13; font-size: 15px; color: white;'>Số Lượng
                                    </th>
                                      <th class='th' style='background-color: #1c9c13; font-size: 15px; color: white;'>Đơn Giá
                                    </th>
                                <%--      <th class='th' style='background-color: #1c9c13; font-size: 15px; color: white;'>Thành Tiền
                                    </th>

                                    <th class='th' style='background-color: #1c9c13; font-size: 15px; color: white;'>Tình Trạng
                                    </th>
                                    <th class='th' style='background-color: #1c9c13; font-size: 15px; color: white;'>Xóa
                                    </th>
                                </tr>
                            </thead >

                            <tbody id="danhSachSPChon" runat="server"></tbody>

                        </table>
                         <input id="listSanPham"  runat="server" hidden="hidden" style ="display:none" />
                        <input id="hdidHangHoa"  runat="server" hidden="hidden" style ="display:none" />
                        <%--<input id="idNhaCungCap"  runat="server" hidden="hidden" style ="display:none" />
                    </div>--%>










                     <div hidden ="hidden" style ="display:none" class="col-md-6">
                                  <div class="col-md-12">
                                    <div class="col-md-3">
                                        <p>Phí Lấy Hàng: </p>
                                    </div>
                                    <div class="col-md-4">
                                         <input placeholder="Phí xe cẩu lấy hàng"  class="form-control" data-val="true" data-val-required="" id="txtPhiXeCauLayHang" runat="server" name="Content.ContentName" type="text" value="" />
                                    </div>
                                    <div class="col-md-4">
                                        <input placeholder="Phí bốc xếp lấy hàng"  class="form-control" data-val="true" data-val-required="" id="txtPhiBocXepLayHang" runat="server" name="Content.ContentName" type="text" value="" />
                                     
                                    </div>
                                    <div class="col-md-1" style="text-align: center">
                                        <i class="fa fa-star"></i>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <p>Cước Vận Chuyển: </p>
                                    </div>
                                    <div class="col-md-4">
                                         <input placeholder="Cước Thực Thu"  class="form-control" data-val="true" data-val-required="" id="txtCuocThucThu" runat="server" name="Content.ContentName" type="text" value="" />
                                    </div>
                                    <div class="col-md-4">
                                        <input placeholder="Cước Phí Hoa Hồng"  class="form-control" data-val="true" data-val-required="" id="txtCuocPhiHoaHong" runat="server" name="Content.ContentName" type="text" value="" />
                                    
                                    </div>
                                    <div class="col-md-1" style="text-align: center">
                                        <i class="fa fa-star"></i>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <p>Phí Giao Hàng: </p>
                                    </div>
                                    <div class="col-md-4">
                                         <input placeholder="Phí xe cẩu giao hàng"  class="form-control" data-val="true" data-val-required="" id="txtPhiXeCauGiaoHang" runat="server" name="Content.ContentName" type="text" value="" />
                                    </div>
                                    <div class="col-md-4">
                                        <input placeholder="Phí bốc xếp giao hàng"  class="form-control" data-val="true" data-val-required="" id="txtPhiBocXepGiaoHang" runat="server" name="Content.ContentName" type="text" value="" />
                                    
                                    </div>
                                    <div class="col-md-1" style="text-align: center">
                                        <i class="fa fa-star"></i>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <p>Phí Container: </p>
                                    </div>
                                    <div class="col-md-4">
                                         <input placeholder="Phí cọc container"  class="form-control" data-val="true" data-val-required="" id="txtCocContainer" runat="server" name="Content.ContentName" type="text" value="" />
                                    </div>
                                    <div class="col-md-4">
                                        <input placeholder="Phí nâng hạ container"  class="form-control" data-val="true" data-val-required="" id="txtNangHaontainer" runat="server" name="Content.ContentName" type="text" value="" />
                                    
                                    </div>
                          
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <p>Phí Bảo Trì Container: </p>
                                    </div>
                                    <div class="col-md-4">
                                         <input placeholder="Phí vệ sinh container"  class="form-control" data-val="true" data-val-required="" id="txtVeSinhContainer" runat="server" name="Content.ContentName" type="text" value="" />
                                    </div>
                                    <div class="col-md-4">
                                        <input placeholder="Phí sữa chữa container"  class="form-control" data-val="true" data-val-required="" id="txtSuaChuaContainer" runat="server" name="Content.ContentName" type="text" value="" />
                                    
                                    </div>
                          
                                </div>
                                <div  class="col-md-12">
                                    <div class="col-md-3">
                                        <p>Phí Đóng Gói: </p>
                                    </div>
                                    <div class="col-md-8">
                                        <input placeholder="Phí đóng gói" oninput="LoadTongTien(this)" class="form-control" data-val="true" data-val-required="" id="txtTienHang" runat="server" name="Content.ContentName" type="text" value="" />
                                    </div>
                                    <div class="col-md-1" style="text-align: center">
                                        <i class="fa fa-star"></i>
                                    </div>
                                </div>
                                 <div class="col-md-12">
                                    <div class="col-md-3">
                                        <p>Tiền Thu Hộ: </p>
                                    </div>
                                    <div class="col-md-4">
                                         <input placeholder="Tiền Thu Hộ"  class="form-control" data-val="true" data-val-required="" id="txtTienThuHo" runat="server" name="Content.ContentName" type="text" value="" />
                                    </div>
                                      <div class="col-md-1">
                                        <p>VAT: </p>
                                    </div>
                                    <div class="col-md-3">
                                        <input placeholder="Phí VAT"  class="form-control" data-val="true" data-val-required="" id="txtVAT" runat="server" name="Content.ContentName" type="text" value="" />
                                    
                                    </div>
                                  <div class="col-md-1" style="text-align: center">
                                        <i class="fa fa-star"></i>
                                    </div>
                                </div>
                           <div  class="col-md-12" style="padding-top: 10px">
                            <div class="col-md-3">
                                <p>Tổng phí: </p>
                            </div>
                            <div class="col-md-8">
                                <input placeholder="Tổng tiền" disabled="" class="form-control" data-val="true" data-val-required="" id="Text1" runat="server" name="Content.ContentName" type="text" value="0" />
                            </div>
                          <%--  <div class="col-md-1" style="text-align: center">
                                <i class="fa fa-star"></i>
                            </div>--%>
                        </div>
                         <div class="col-md-11" style="padding-top: 10px">
                                <textarea id="txtGhiChu" style="width: 94.2%;" placeholder="Ghi chú" runat="server" class="form-control"></textarea>
                            </div>
                         </div>
                   



                    <div class="col-md-12" style="text-align: center">
                        <asp:Button ID="btLuu" runat="server" Text="LƯU" class="btn btn-primary btn-flat" OnClick="btLuu_Click" />
                        <asp:Button ID="btHuy" runat="server" Text="HỦY" class="btn btn-primary btn-flat" OnClick="btHuy_Click" />
                    </div>

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
    maxDate: new Date(2020, 12, 31),
    yearRange: [2000, 2020]
});
        var picker = new Pikaday(
    {
        field: document.getElementById('ContentPlaceHolder1_txtNgayThanhToan'),
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

