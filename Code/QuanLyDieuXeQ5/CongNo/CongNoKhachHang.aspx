﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/MasterPage.master" AutoEventWireup="true" CodeFile="CongNoKhachHang.aspx.cs" Inherits="QuanLyXuatKho_QuanLyXuatKho" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../dist/jquery-ui-1.11.3/jquery-ui.css" rel="stylesheet" />
    <script src="../dist/jquery-ui-1.11.3/jquery-ui.js"></script>

    <link href="../cssdatepicker/pikaday.css" rel="stylesheet" />
    <script src="../cssdatepicker/pikaday.js"></script>
    <script>
        window.onload = function () {
            KhachHangAutocomplete();
        }
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
            //XemChiTietDonHang(idKhachHang);
        }


        function LoadPopupLichSuNo(idKhachHang) {
            //alert(idKhachHang);
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
            xmlhttp.open("GET", "../Ajax.aspx?Action=LoadPopupLichSuNo&idKhachHang=" + idKhachHang, true);
            xmlhttp.send();
          
            MoXemChiTietDonHang();
        }
        //function LoadLichSuLenPopup(idKhachHang) {
        //    $.ajax({
        //        type: "POST",
        //        url: "CongNoKhachHang.aspx/LoadLichSuLenPopup",
        //        data: "{idKhachHang: '" + idKhachHang + "'}",
        //        contentType: "application/json; charset=utf-8",
        //        dataType: "json",
        //        success: function (datas) {
        //            if (datas.d != "") {
        //                //var arrTemp = datas.d.split("|~|");

        //                var html = datas.d;


        //                $("#tbLichSuDuAn").html(html);
        //                $('#LichSuLo').modal('show');
        //                //$("#btLichSuLo").attr("onclick", "CapNhatLichSuLo(\"" + idChiTietDuAn + "\")")
        //            }
        //            else {

        //            }
        //        },
        //        error: function () {
        //        }
        //    });
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
                        //  alert(xmlhttp.responseText);
                        var txt = xmlhttp.responseText
                               .replace(/[\"]/g, '\\"')
                              .replace(/[\\]/g, '\\\\')
                              .replace(/[\/]/g, '\\/')
                              .replace(/[\b]/g, '\\b')
                              .replace(/[\f]/g, '\\f')
                              .replace(/[\n]/g, '\\n')
                              .replace(/[\r]/g, '\\r')
                              .replace(/[\t]/g, '\\t');
                        //  alert(txt);



                        var listKhachHangAutocomplete = eval("(" + xmlhttp.responseText + ")");

                        $("#ContentPlaceHolder1_txtTenKhachHang").autocomplete({

                            minLength: 0,
                            source: listKhachHangAutocomplete,
                            focus: function (event, ui) {
                                $("#ContentPlaceHolder1_txtTenKhachHang").val(ui.item.label);
                                return false;
                            },
                            select: function (event, ui) {
                                $("#ContentPlaceHolder1_txtTenKhachHang").val(ui.item.label);
                                $("#ContentPlaceHolder1_hdIdKhachHang").val(ui.item.id);
                                return false;
                            }
                        })
                    }
                    else {
                        alert("Lỗi get tên nhân viên !")
                    }

                }
            }
            xmlhttp.open("GET", "../Ajax.aspx?Action=KhachHangAutocomplete1", true);
            xmlhttp.send();
        }

        function PrinfHoaDon(idKhachHang) {
            alert(idKhachHang);
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
                        var print = window.open('', '_blank');

                        var shtml = "<html>";
                        shtml += "<body onload=\"window.print(); window.close();\">";
                        shtml += xmlhttp.responseText;
                        shtml += "</body>";
                        shtml += "</html>";

                        print.document.write(shtml);
                        print.document.close();
                    }
                }
            }
            xmlhttp.open("GET", "../Ajax.aspx?Action=LoadPrintKhachHangTraNo&idKhachHang=" + idKhachHang, true);
            xmlhttp.send();
        }
        //
        function DeleteDonHang(idDonHang) {
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
                xmlhttp.open("GET", "../Ajax.aspx?Action=DeleteDonHang&idDonHang=" + idDonHang, true);
                xmlhttp.send();
            }
        }
        function MoXemChiTietDonHang() {
            document.getElementById('lightXemChiTietDonHang').style.display = 'block';
            document.getElementById('fadeXemChiTietDonHang').style.display = 'block';
        }
        function DongXemChiTietDonHang() {
            document.getElementById('lightXemChiTietDonHang').style.display = 'none';
            document.getElementById('fadeXemChiTietDonHang').style.display = 'none';
        }
        function XemChiTietDonHang(idDonHang) {
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
                        document.getElementById("dvChiTiet").innerHTML = xmlhttp.responseText;
                        //MoXemChiTietDonHang();
                    }
                }
            }
            xmlhttp.open("GET", "../Ajax.aspx?Action=XemChiTietDonHang&idDonHang=" + idDonHang, true);
            xmlhttp.send();
        }

        function CheckDaNhanTien(idDonHang) {
            if (confirm("Bạn muốn cấp nhật đơn hàng " + idDonHang + " phải không?")) {
                var sChecked = document.getElementById("ckbDaNhanTien_" + idDonHang).checked;
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
                            location.reload();
                        }
                        else {
                            alert("Lỗi cập nhật");
                        }
                    }
                }
                xmlhttp.open("GET", "../Ajax.aspx?Action=CheckDaNhanTien&idDonHang=" + idDonHang + "&Checked=" + sChecked, true);
                xmlhttp.send();
            }
        }

        function SuaNhanhDonHang(idDonHang) {
            var isDaNhanTien = document.getElementById("ckbDaNhanTien_" + idDonHang);
            var MaTinhTrang = document.getElementById("slTinhTrang_" + idDonHang);
            var GhiChu = document.getElementById("txtGhiChu_" + idDonHang);
            //alert(Duyet.value);
            if (isDaNhanTien.disabled == true && MaTinhTrang.disabled == true && GhiChu.disabled == true) {
                document.getElementById("ckbDaNhanTien_" + idDonHang).disabled = false;
                document.getElementById("slTinhTrang_" + idDonHang).disabled = false;
                document.getElementById("txtGhiChu_" + idDonHang).disabled = false;
                document.getElementById("btSuaNhanhDonHang_" + idDonHang).innerHTML = "Lưu";
            }
            else {
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
                xmlhttp.open("GET", "../Ajax.aspx?Action=SuaNhanhDonHang&idDonHang=" + idDonHang + "&isDaNhanTien=" + isDaNhanTien.checked + "&MaTinhTrang=" + MaTinhTrang.value + "&GhiChu=" + GhiChu.value.replace("&", ""), true);
                xmlhttp.send();
            }
        }
        function LoadPopupCTHG(s) {
            //var xmlhttp;
            //if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
            //    xmlhttp = new XMLHttpRequest();
            //}
            //else {// code for IE6, IE5
            //    xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
            //}
            //xmlhttp.onreadystatechange = function () {
            //    if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
            //        if (xmlhttp.responseText != "") {
            //            //alert(xmlhttp.responseText);
            //            var txt = xmlhttp.responseText;

            //            $("#dvXemSanPham").html(txt); MoXemQuyCach();
            //            // window.location.hash = IDPhieuXuat;

            //        }
            //        else {
            //            alert("Không có chi tiết để xem!")
            //        }

            //    }
            //}
            //xmlhttp.open("GET", "../Ajax.aspx?Action=LoadChiTietTonKHo&IDLoaiHangHoa=" + IDLoaiHangHoa, true);
            //xmlhttp.send();
            var txt;
            txt = "<div style='font-size: 20px' > LỊCH SỬ TRẢ NỢ: <span style='color:red'> " + "</span> </div>";
                txt += "<table class='table table-bordered' style='width:100%;'><tr>";
                txt += "<tr><th class='th'>Mã Phiếu Trả</th><th class='th'> Ngày Trả</th><th class='th'>Số Tiền </th><th class='th'></th></tr>";
                txt += "<tr><td>";
           
            //txt += " </table>";
            $("#dvXemChiTietDonHang").html(txt); MoXemChiTietDonHang();
        }
        function PrinfHoaDon(idKhachHang) {
            //alert(idKhachHang)
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
                        var print = window.open('', '_blank');

                        var shtml = "<html>";
                        shtml += "<body onload=\"window.print(); window.close();\">";
                        shtml += xmlhttp.responseText;
                        shtml += "</body>";
                        shtml += "</html>";

                        print.document.write(shtml);
                        print.document.close();
                    }
                }
            }
            xmlhttp.open("GET", "../Ajax.aspx?Action=LoadPrintKhachHangTraNo&idKhachHang=" + idKhachHang, true);
            xmlhttp.send();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form runat="server">
        <div class="container-fluid">
            <div class="form-group row-fluid">
                <div class="span4 title1">
                    <b>QUẢN LÝ CÔNG NỢ</b>
                </div>
                <div class="span8 CanhPhai">
                    <asp:LinkButton class="btn btn-primary" runat="server" OnClick="btTimKiem_Click"><i class="fa fa-search"></i><span>Tìm kiếm</span></asp:LinkButton>
                                        <asp:LinkButton class="btn btn-primary" runat="server" OnClick="btTimKiem_Click2"><i class="fa fa-search"></i><span> Đã hoàn thành </span></asp:LinkButton>
                  <asp:LinkButton class="btn btn-primary" runat="server" OnClick="btTimKiem_Click1"><i class="fa fa-search"></i><span> Còn nợ</span></asp:LinkButton>
                       <asp:LinkButton class="btn btn-primary" runat="server" OnClick="btXemTatCa_Click"><i class="fa fa-search"></i><span> Xem Tất Cả</span></asp:LinkButton>
                    <%--<a id="btImportExcel" runat="server" class="btn btn-primary" href="QuanLyDonHang-ImportExcel.aspx"><i class="fa fa-cloud-download"></i><span style="margin-left: 5px;">Import excel</span></a>--%>
                </div>
            </div>
            <div class="form-group row-fluid">
                <div class="col-md-4">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="fa fa-user"></i></span>
                        <input type="text" class="form-control" placeholder="Từ ngày" id="txtTuNgay" runat="server" />
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="fa fa-user"></i></span>
                        <input type="text" class="form-control" placeholder="Đến ngày" id="txtDenNgay" runat="server" />
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="fa fa-user"></i></span>
                        <input type="text" class="form-control" placeholder="Tên KH, Mã KH" id="txtKhachHang" runat="server" />
                        <input type="hidden" id="hdIdKhachHang" runat="server" />
                    </div>
                </div>
            </div>
            <%--<div class="form-group row-fluid">

            </div>--%>
            <div class="row-fluid">
                <div class="span12">
                    <div id="dvDanhSachDonHang" style="overflow: auto; margin-top: 10px" runat="server"></div>
                </div>
            </div>
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

