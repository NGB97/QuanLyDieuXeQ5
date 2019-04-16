<%@ Page Title="" enableEventValidation="false" Language="C#" MasterPageFile="~/Layout/MasterPage.master" AutoEventWireup="true" CodeFile="PhanHangLenXe-CapNhat.aspx.cs" Inherits="QuanLyDonHang_QuanLyDonHang_CapNhat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../dist/jquery-ui-1.11.3/jquery-ui.js"></script>
    <link href="../dist/jquery-ui-1.11.3/jquery-ui.css" rel="stylesheet"/>
    <link href="../cssdatepicker/pikaday.css" rel="stylesheet" />
    <script src="../cssdatepicker/pikaday.js"></script>
    <script>
        window.onload = function () {
            PhanXeAutocomplete();
            ChonDonHangAutocomplete();
            ChiNhanhGuiAutocomplete();
            LoadDSChiTietDonHang('<%= Request.QueryString["idDonHang"] %>');
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
                        $("#ContentPlaceHolder1_txtChiNhanh").autocomplete({

                            minLength: 0,
                            source: listKhachHangAutocomplete,
                            focus: function (event, ui) {
                                $("#ContentPlaceHolder1_txtChiNhanh").val(ui.item.value);
                                return false;
                            },
                            select: function (event, ui) {
                                // GetMaDonHang(ui.item.id);
                                $("#ContentPlaceHolder1_txtChiNhanh").val(ui.item.value);
                                //  $("#ContentPlaceHolder1_txtDonGia").val((ui.item.ten).replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1."));
                                $("#ContentPlaceHolder1_hdIDChiNhanhGui").val(ui.item.id);
                                LoadSanPham(ui.item.id);
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
            function PhanXeAutocomplete() {
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
                            $("#ContentPlaceHolder1_txtChonXe").autocomplete({

                                minLength: 0,
                                source: listKhachHangAutocomplete,
                                focus: function (event, ui) {
                                    $("#ContentPlaceHolder1_txtChonXe").val(ui.item.value);
                                    return false;
                                },
                                select: function (event, ui) {
                                  //  GetMaDonHang(ui.item.id);
                                    $("#ContentPlaceHolder1_txtChonXe").val(ui.item.ten);
                                    $("#ContentPlaceHolder1_txtTaiXe").val(ui.item.value);
                                    $("#ContentPlaceHolder1_hdIdLoaiXe").val(ui.item.id);
                                    //$( "#results").text($("#topicID").val());    
                                    //alert($("#hdIdLoaiXe").val());
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
                xmlhttp.open("GET", "../Ajax.aspx?Action=PhanXeAutocomplete", true);
                xmlhttp.send();
            }

            function ChonDonHangAutocomplete() {
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
                            $("#ContentPlaceHolder1_txtChonDonHang").autocomplete({

                                minLength: 0,
                                source: listKhachHangAutocomplete,
                                focus: function (event, ui) {
                                    $("#ContentPlaceHolder1_txtChonDonHang").val(ui.item.value);
                                    return false;
                                },
                                select: function (event, ui) {
                                   // GetMaDonHang(ui.item.id);
                                    $("#ContentPlaceHolder1_txtChonDonHang").val(ui.item.ten);
                                   // $("#ContentPlaceHolder1_txtTaiXe").val(ui.item.value);
                                    $("#ContentPlaceHolder1_hdIdChonDonHang").val(ui.item.id);
                                    //XemSoDo(ui.item.id);
                                    //$( "#results").text($("#topicID").val());    
                                    //alert($("#hdIdLoaiXe").val());
                                  //  listsanpham = [];
                                    LoadSanPham(ui.item.id);
                                    //ChonSanPhamAutocomplete(ui.item.id);
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
                xmlhttp.open("GET", "../Ajax.aspx?Action=ChonDonHangAutocomplete", true);
                xmlhttp.send();
            }

            function LoadChiNhanh(Ngay) {
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
                            var sp = xmlhttp.responseText.split('\r\n');
                            var html = "";
                            for (var i = 0; i < sp.length ; i++) {
                                var ctsp = sp[i].split('|~|');
                                html += "<div class='col-md-4'>";
                                html += "<input type='checkbox' onchange='DSSanPham(this.checked,\"" + ctsp[0] + "-" + ctsp[1] + "-" + ctsp[2] + "-" + ctsp[3] + "\")' id='chk" + ctsp[0] + "' name='tra' class='flat-red' value =" + ctsp[0] + "/>";
                                html += "<a /> SP: " + ctsp[2];
                                html += "<br />Số lượng :" + ctsp[3];
                                html += "</div>";



                            }
                            $('#dvsanpham').html(html);
                        }
                        else {
                            alert("Lỗi get tên nhân viên !")
                        }

                    }
                }
                xmlhttp.open("GET", "../Ajax.aspx?Action=LoadChiNhanh&Ngay=" + Ngay, true);
                xmlhttp.send();
            }

            function LoadSanPham(idChiNhanh)
            {
                //var idKhachHang = document.getElementById("ContentPlaceHolder1_hdIdKhachHang").value;
                //alert(idChiNhanh);
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
                            var sp = xmlhttp.responseText.split('\r\n');
                            listsanpham = xmlhttp.responseText.split('\r\n');
                            //ThemSanPham();

                            var html = "";
                            $('#ContentPlaceHolder1_danhSachSPChon').html(html);
                            for(var i = 0; i < sp.length ; i ++)
                            {
                                var mangcon = listsanpham[i].split('|~|');
                                //var IDChiTietDonHang = mangcon[0];
                                //var MaDonHang = mangcon[1];
                                //var TenSanPham = mangcon[2];
                                //var SoLuong = mangcon[3];
                                var IDChiTietDonHang = mangcon[0];
                                var MaDonHang = mangcon[1];
                                var TenKhachHang = mangcon[2];
                                var TenChiNhanh = mangcon[3];
                                var DiaChi = mangcon[4];
                                var SoLuong = mangcon[5];
                                var TenHangHoa = mangcon[6];
                                var NguoiNhanTra = mangcon[7];
                                if (NguoiNhanTra == 0) {
                                    var NguoiNhan = "Người Gửi Trả";
                                }
                                else {
                                    var NguoiNhan = "Người Nhận Trả";
                                }
                              //  listSanPham1.push(IDChiTietDonHang + "-" + MaDonHang + "-" + TenSanPham + "-" + SoLuong);
                               // $("#ContentPlaceHolder1_listSanPham").val(listSanPham1);
                                //alert($("#ContentPlaceHolder1_listSanPham").val())
                                html += "<tr id='tr_" + (i + 1) + "'>";
                                html += "<td>" + (i + 1) + "</td>";
                                html += "<td style='text-align: center'>" + MaDonHang + "</td>";
                                html += "<td style='text-align: center'>" + TenKhachHang + "</td>";
                                html += "<td style='text-align: center'>" + TenChiNhanh + "</td>";
                                html += "<td style='text-align: center'>" + DiaChi + "</td>";
                                html += "<td style='text-align: center'>" + SoLuong + "</td>";
                                html += "<td style='text-align: center'>" + TenHangHoa + "</td>";
                                html += "<td style='text-align: center'>" + NguoiNhan + "</td>";

                                html += "<td style='text-align: center' > <input type='checkbox' onchange='DSSanPham(this.checked,\"" + mangcon[0] + "-" + mangcon[1] + "-" + mangcon[2] + "-" + mangcon[3] + "\")' id='chk" + mangcon[0] + "' name='tra' class='flat-red' value =" + mangcon[0] + "/> </td>";
                                //html += "<td style='text-align: center'>" + ThanhTien.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1.") + "</td>";
                                //  html += "<td style='text-align: center'><a style='cursor:pointer' onclick='XoaSanPham(\"" + IDChiTietDonHang + "\")'><i class='fa fa-trash'></i></a></td>"
                                //html += "<td style='text-align: center' > <input type='checkbox' onchange='DSSanPham(this.checked,\"VP\",\"" + mangcon[0] + "-" + mangcon[1] + "-" + mangcon[6] + "-" + mangcon[5] + "-" + "XDVP" + "\")' id='chk" + mangcon[0] + "' name='tra' class='flat-red' value =" + mangcon[0] + "/> </td>";
                                //html += "<td style='text-align: center' > <input type='checkbox' onchange='DSSanPham(this.checked,\"TT\",\"" + mangcon[0] + "-" + mangcon[1] + "-" + mangcon[6] + "-" + mangcon[5] + "-" + "DHT" + "\")' id='chk" + mangcon[0] + "' name='tra' class='flat-red' value =" + mangcon[0] + "/> </td>";

                              //  html += "<tr id='tr_" + (i + 1) + "'>";
                              //  html += "<td>" + (i + 1) + "</td>";
                              //  html += "<td style='text-align: center'>" + MaDonHang + "</td>";
                              //  html += "<td style='text-align: center'>" + TenSanPham + "</td>";
                              //  html += "<td style='text-align: center'>" + SoLuong + "</td>";
                              //  //html += "<td style='text-align: center'>" + TinhTrangDonHang + "</td>";
                              //  // html += "<td style='text-align: center'>" + SoKhoi + "</td>";

                              //  //html += "<td style='text-align: center'>" + ThanhTien.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1.") + "</td>";
                              ////  html += "<td style='text-align: center'><a style='cursor:pointer' onclick='XoaSanPham(\"" + IDChiTietDonHang + "\")'><i class='fa fa-trash'></i></a></td>"
                              //  html += "<td style='text-align: center' > <input type='checkbox' onchange='DSSanPham(this.checked,\"" + mangcon[0] + "-" + mangcon[1] + "-" + mangcon[2] + "-" + mangcon[3] + "\")' id='chk" + mangcon[0] + "' name='tra' class='flat-red' value =" + mangcon[0] + "/> </td>";
                              //  // html += "<td style='text-align: center'></td>";
                                html += "</tr>";
                                //$("#tr_SanPham").after(html);
                                //  $("#ContentPlaceHolder1_txtTongTien").val(TongTien.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1.") + " đ");
                                //      $("#ContentPlaceHolder1_txtTongTien").val(TongTien.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1.") + " đ");

                                $("#ContentPlaceHolder1_txtChonDonHang").val("");


                                $("#ContentPlaceHolder1_txtChonSanPham").val("");
                                $("#ContentPlaceHolder1_hdSoLuong").val("");
                                // $("#ContentPlaceHolder1_slTinhTrangDon").val("");
                                //   $("#ContentPlaceHolder1_txtSoKhoi").val("");
                                //  $("#ContentPlaceHolder1_txt_ThanhTien").val("");
                                //   $("#ContentPlaceHolder1_txtGiaNhap").val("");
                                // TinhTongTien();
                                sortTable();

                                //var ctsp = sp[i].split('|~|');
                                //html += "<div class='col-md-4'>";
                                //html += "<input type='checkbox' onchange='DSSanPham(this.checked,\"" + ctsp[0] + "-" + ctsp[1] + "-" + ctsp[2] + "-" + ctsp[3] + "\")' id='chk" + ctsp[0] + "' name='tra' class='flat-red' value =" + ctsp[0] + "/>";
                                //html += "<a /> SP: " + ctsp[2];
                                //html += "<br />Số lượng :" + ctsp[3];
                                //html += "</div>";


                                
                            }

                            $('#ContentPlaceHolder1_danhSachSPChon').html(html);
                        }
                        else {
                            alert("Lỗi! Không có đơn hàng nào.")
                        }

                    }
                }
                xmlhttp.open("GET", "../Ajax.aspx?Action=ChonSanPhamAutocomplete&idChiNhanh=" + idChiNhanh, true);
                xmlhttp.send();
            }
            var listsanpham2 = [];
            function DSSanPham(value,idChiTietDonHang) {
                if(value)
                {
                    listsanpham2.push(idChiTietDonHang);
                }
                else
                {

                    var index = listsanpham2.lastIndexOf(idChiTietDonHang);
                    listsanpham2.splice(index, 1);
                }

                $("#ContentPlaceHolder1_listSanPham").val(listsanpham2);
               // alert(listsanpham2)
            }
    

            function ChonSanPhamAutocomplete(idDonHang) {
              // alert(idDonHang);
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
                            $("#ContentPlaceHolder1_txtChonSanPham").autocomplete({

                                minLength: 0,
                                source: listKhachHangAutocomplete,
                                focus: function (event, ui) {
                                    $("#ContentPlaceHolder1_txtChonSanPham").val(ui.item.value);
                                    return false;
                                },
                                select: function (event, ui) {
                                  //  GetMaDonHang(ui.item.id);
                                    $("#ContentPlaceHolder1_txtChonSanPham").val(ui.item.ten);
                                    $("#ContentPlaceHolder1_hdSoLuong").val(ui.item.value);
                                    $("#ContentPlaceHolder1_hdIDChonSanPham").val(ui.item.id);
                                    //$( "#results").text($("#topicID").val());    
                                    //alert($("#hdIdLoaiXe").val());
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
                xmlhttp.open("GET", "../Ajax.aspx?Action=ChonSanPhamAutocomplete&idDonHang=" + idDonHang, true);
                xmlhttp.send();
            }

            var listSanPham1 = [];
            function ThemSanPham() {
                for (var i = 0; i < listsanpham.length; i++)
                {
                    //alert(listsanpham);
                    var mangcon = listsanpham[i].split('-');
                    var IDChiTietDonHang = mangcon[0];
                    var MaDonHang = mangcon[1];
                    var TenSanPham = mangcon[2];
                    var SoLuong = mangcon[3];

                    if (TenSanPham == "" || MaDonHang == "" || SoLuong == "") {
                        alert("Dũ liệu nhập chưa đủ hoặc không chính xác. Vui lòng nhập lại!");
                        return;
                    }
                    else {
                        for (var j = 0 ; j < mangcon ; j++) {
                            if (mangcon[2] == TenSanPham) {
                                alert("Bạn đã nhập sản phẩm " + TenSanPham + ". Vui lòng chọn sản phẩm khác!");
                                return;
                            }

                        }


                        // TongTien += Number(DonGia.replace(/\./g, ""));

                        //tới đây.

                        listSanPham1.push(IDChiTietDonHang + "-" + MaDonHang + "-" + TenSanPham + "-" + SoLuong);

                        //else {
                        //    listSanPham.push(idHangHoa + "-" + MaHangHoa + "-" + TenHangHoa + "-" + idNhapMua + "-" + SoLuong + "-" + DonGia + "-" + ThanhTien);
                        //}
                       // alert(listSanPham1);
                        $("#ContentPlaceHolder1_listSanPham").val(listSanPham1);
                        //alert($("#ContentPlaceHolder1_listSanPham").val())
                        var html = "<tr id='tr_" + (i + 1) + "'>";
                        html += "<td>" + (i + 1) + "</td>";
                        html += "<td style='text-align: center'>" + MaDonHang + "</td>";





                        html += "<td style='text-align: center'>" + TenSanPham + "</td>";
                        html += "<td style='text-align: center'>" + SoLuong + "</td>";
                        //html += "<td style='text-align: center'>" + TinhTrangDonHang + "</td>";
                        // html += "<td style='text-align: center'>" + SoKhoi + "</td>";

                        //html += "<td style='text-align: center'>" + ThanhTien.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1.") + "</td>";
                        html += "<td style='text-align: center'><a style='cursor:pointer' onclick='XoaSanPham(\"" + IDChiTietDonHang + "\")'><i class='fa fa-trash'></i></a></td>"

                        // html += "<td style='text-align: center'></td>";
                        html += "</tr>";
                        $("#tr_SanPham").after(html);
                        //  $("#ContentPlaceHolder1_txtTongTien").val(TongTien.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1.") + " đ");
                        //      $("#ContentPlaceHolder1_txtTongTien").val(TongTien.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1.") + " đ");

                        $("#ContentPlaceHolder1_txtChonDonHang").val("");


                        $("#ContentPlaceHolder1_txtChonSanPham").val("");
                        $("#ContentPlaceHolder1_hdSoLuong").val("");
                        // $("#ContentPlaceHolder1_slTinhTrangDon").val("");
                        //   $("#ContentPlaceHolder1_txtSoKhoi").val("");
                        //  $("#ContentPlaceHolder1_txt_ThanhTien").val("");
                        //   $("#ContentPlaceHolder1_txtGiaNhap").val("");
                        // TinhTongTien();
                        sortTable();
                    
                }
                   
                
                }
            }

            function sortTable() {
             //   alert(listSanPham1);
                var table, rows, switching, i, x, y, shouldSwitch;
                table = document.getElementById("tb-danhsachSP");
                rows = table.getElementsByTagName("TR");

                for (i = 1; i < rows.length ; i++) {
                    x = rows[i].getElementsByTagName("TD")[0];
                    x.innerHTML = (i);
                }
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
                                <a href="../PhanHangLenXe/PhanHangLenXe.aspx" runat="server"><i class="fa fa-arrow-left" style="padding-right: 5px"></i>Quay lại</a>
                            </div>
                            <div class="span6">
                                <b id="dvTitle" runat="server">THÊM THÔNG TIN TUYẾN ĐI</b>
                            </div>
                        </div>
                    </div>
                      <div class="col-md-12" style="text-align: center; padding-bottom: 10px">
                            <h4><b><u>Thông tin Tuyến</u></b></h4>
                        </div>
                    <div class="col-md-6">
                      
                        <div class="col-md-12">
                            <div class="col-md-3">
                                <p>Mã tuyến: </p>
                            </div>
                            <div class="col-md-7">
                                <input placeholder="Mã đơn hàng tạo tự động" class="form-control" data-val="true" data-val-required="" id="txtMaDonHang" runat="server" name="Content.ContentName" readonly="" type="text" value="" />
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="col-md-3">
                                <p>Ngày lập: </p>
                            </div>
                            <div class="col-md-7">
                                <input placeholder="Ngày lập(*)" class="form-control" data-val="true" data-val-required="" id="txtNgayLap" runat="server" name="Content.ContentName" type="text" value="" />
                            </div>
                            <div class="col-md-1" style="text-align: center">
                                <i class="fa fa-star"></i>
                            </div>
                        </div>


                        <div class="col-md-12">
                            <div class="col-md-3">
                                <p>Chọn xe: </p>
                            </div>
                            <div class="col-md-7">
                                <input placeholder="Biển số xe và tài xế xe" class="form-control" data-val="true" data-val-required="" id="txtChonXe" runat="server" name="Content.ContentName" type="text" value="" />
                                <input  type="hidden" id="hdIdLoaiXe" runat="server" />
                            </div>
                            <div class="col-md-1" style="text-align: center">
                                <i class="fa fa-star"></i>
                            </div>
                        </div>
                       

                     

                        <div hidden ="hidden" style ="display:none"  class="col-md-12">
                            <div class="col-md-3">
                                <p>Gói dịch vụ: </p>
                            </div>
                            <div class="col-md-7">
                                <select class="form-control" id="slGoiDichVu" runat="server">
                                    <option value="Giao hàng nhanh">Giao hàng nhanh</option>
                                    <option value="Giao hàng tiết kiệm">Giao hàng qua ngày</option>
                                    <option value="Giao hàng siêu tốc">Giao hàng siêu tốc</option>
                                </select>
                            </div>
                            <div class="col-md-1" style="text-align: center">
                                <i class="fa fa-star"></i>
                            </div>
                        </div>

                        <div hidden ="hidden" style ="display:none"  class="col-md-12">
                            <div class="col-md-3">
                                <p>Thời gian giao: </p>
                            </div>
                            <div class="col-md-3">
                                <input placeholder="Thời gian giao" class="form-control" data-val="true" data-val-required="" id="txtNgayDuKienGiao" runat="server" name="Content.ContentName" type="text" value="" />
                            </div>
                            <div class="col-md-2" style="padding-right: 1px">
                                <input placeholder="Giờ..." class="form-control" data-val="true" data-val-required="" id="txtGioDuKienGiao" runat="server" name="Content.ContentName" type="text" value="" />
                            </div>
                            <div class="col-md-2">
                                <input placeholder="Phút..." class="form-control" data-val="true" data-val-required="" id="txtPhutDuKienGiao" runat="server" name="Content.ContentName" type="text" value="" />
                            </div>
                        </div>

                        <div hidden ="hidden" style ="display:none"  class="col-md-12">
                            <div class="col-md-3">
                                <p>Thông tin bưu gửi: </p>
                            </div>
                            <div class="col-md-7">
                                <input placeholder="" class="form-control" data-val="true" data-val-required="" id="txtThongTinBuuGui" runat="server" name="Content.ContentName" type="text" value="" />
                            </div>
                        </div>

                        <div hidden ="hidden" style ="display:none"  class="col-md-12" style="text-align: center">
                            <div class="col-md-5">
                                <input type="radio" id="Checkbox1" checked="" name="thanhtoan" class='flat-red' runat="server" />
                                <br />
                                Chưa thanh toán:
                            </div>
                            <div class="col-md-5">
                                <input type="radio" id="ckbDaNhanTien" name="thanhtoan" class='flat-red' runat="server" />
                                <br />
                                Đã thanh toán:
                            </div>
                        </div>

                        <div hidden ="hidden" style ="display:none" class="col-md-12" style="text-align: center">
                            <div class="col-md-5">
                                <input type="radio" checked="" name="tra" class='flat-red' runat="server" />
                                <br />
                                Người gửi trả:
                            </div>
                            <div class="col-md-5">
                                <input type="radio" id="radiNguoiNhanTra" name="tra" class='flat-red' runat="server" />
                                <br />
                                Người nhận trả:
                            </div>
                        </div>
       
                    </div>
                    <div  class="col-md-6">

                         <div class="col-md-12">
                            <div class="col-md-3">
                                <p>Tài xế: </p>
                            </div>
                            <div class="col-md-7">
                                <input placeholder="Tài xế" disabled="" class="form-control" data-val="true" data-val-required="" id="txtTaiXe" runat="server" name="Content.ContentName" type="text" value="" />
                            </div>
                            <div class="col-md-1" style="text-align: center">
                                <i class="fa fa-star"></i>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="col-md-3">
                                <p>Chọn Chi Nhánh: </p>
                            </div>
                            <div class="col-md-7">
                                <input placeholder="Chi Nhánh(*)"  onchange="" class="form-control" data-val="true" data-val-required="" id="txtChiNhanh" runat="server" name="Content.ContentName" type="text" value="" />
                            <input type="hidden" id="hdIDChiNhanhGui" runat="server" /> 
                            </div>
                            <div class="col-md-1" style="text-align: center">
                                <i class="fa fa-star"></i>
                            </div>
                        </div>

                        <div hidden="hidden" class="col-md-12">
                            <div class="col-md-3">
                                <p>Chọn Đơn Hàng: </p>
                            </div>
                            <div class="col-md-7">
                                <input placeholder="Chọn đơn hàng:" class="form-control" data-val="true" data-val-required="" id="txtChonDonHang" runat="server" name="Content.ContentName" type="text" value="" />
                                <input  type="hidden" id="hdIDChonDonHang" runat="server" />
                            </div>
                            <div class="col-md-1" style="text-align: center">
                                <i class="fa fa-star"></i>
                            </div>
                        </div>
                            
                         <%--<div  class="col-md-12" style="text-align: center" id ="dvsanpham">
                            <div class="col-md-3">
                                <input type="radio" checked="" name="tra" class='flat-red' runat="server" />
                               <%-- <a />
                                SP1: <br />
                                Soluong :
                            </div>--%>
                            <%--<div class="col-md-3">
                                <input type="radio" id="Radio1" name="tra1" class='flat-red' runat="server" />
                                <br />
                                SP2 :
                            </div>
                                <div class="col-md-3">
                                <input type="radio" id="Radio2" name="tra2" class='flat-red' runat="server" />
                                <br />
                                SP2 :
                            </div>
                                <div class="col-md-3">
                                <input type="radio" id="Radio3" name="tra3" class='flat-red' runat="server" />
                                <br />
                                SP2 :--%>
                            </div>
                        </div>










                         <div  hidden="hidden" class="col-md-12">
                            <div class="col-md-3">
                                <p>Chọn Sản Phẩm: </p>
                            </div>
                            <div class="col-md-7">
                                <input placeholder="Chọn sản phẩm" class="form-control" data-val="true" data-val-required="" id="txtChonSanPham" runat="server" name="Content.ContentName" type="text" value="" />
                                <input   type="hidden" id="hdIDChonSanPham" runat="server" />
                                  <input  type="hidden" id="hdSoLuong" runat="server" />
                            </div>
                            <div class="col-md-1" style="text-align: center">
                                <i class="fa fa-star"></i>
                            </div>
                        </div>

                       
                         
                          
                         <input id="hdidDonHang"  runat="server" hidden="hidden" style ="display:none" />
                       
                         <input id="hdIDHangHoa"  runat="server" hidden="hidden" style ="display:none" />
                        <div class="col-md-12" style="text-align: center;">
                           <%-- <asp:Button ID="btThem" runat="server" Text="Thêm" class="btn btn-primary btn-flat" OnClick="btThem_Click" />
                           <a id="btThem" class="btn btn-primary btn-flat">Thêm</a>--%>
                            <%-- <a class="btn btn-primary" onclick="ThemSanPham()" >Thêm</a>--%>
                        </div>

                    </div>

                    
                        <div class="col-md-12" style="text-align: center">
                        <table id="tb-danhsachSP" class='table table-bordered table-hover dataTable'>
                            <thead style='white-space: nowrap;'>
                                <tr id="tr_SanPham">
                                     <th class='th' style='background-color: #1c9c13; font-size: 15px; color: white;'>STT
                                    </th>
                                     <th class='th' style='background-color: #1c9c13; font-size: 15px; color: white;'>Mã Hàng Hóa
                                    </th>
                                    <th class='th' style='background-color: #1c9c13; font-size: 15px; color: white;'>Tên Người Nhận
                                    </th>
                                     <th class='th' style='background-color: #1c9c13; font-size: 15px; color: white;'>Chi Nhánh Nhận
                                    </th>
                                    <th class='th' style='background-color: #1c9c13; font-size: 15px; color: white;'>Đ/C Người Nhận
                                    </th>
                                     <th class='th' style='background-color: #1c9c13; font-size: 15px; color: white;'>Số Lượng
                                    </th>
                                    <th class='th' style='background-color: #1c9c13; font-size: 15px; color: white;'>Tên Hàng Hóa
                                    </th>
                                     <th class='th' style='background-color: #1c9c13; font-size: 15px; color: white;'>Tình Trạng
                                    </th>
                                    
                           
                                    <th class='th' style='background-color: #1c9c13; font-size: 15px; color: white;'>Chọn
                                    </th>
                                </tr>
                            </thead >

                            <tbody id="danhSachSPChon" runat="server">

                            </tbody>

                        </table>
                            <%--<input id="listSanPham2"  runat="server"  style ="display:none" />--%>
                         <input id="listSanPham"  runat="server" hidden="hidden" style ="display:none" />
                     <%--   <input id="hdidHangHoa"  runat="server" hidden="hidden" style ="display:none" />--%>
                        <%--<input id="idNhaCungCap"  runat="server" hidden="hidden" style ="display:none" />--%>
                    </div>


                    <div hidden ="hidden" style ="display:none" class="col-md-6">
                        <div class="col-md-12" style="text-align: center; padding-bottom: 10px">
                            <h4><b><u>Phí vận chuyển</u></b></h4>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-3">
                                <p>Phí phát sinh: </p>
                            </div>
                            <div class="col-md-8">
                                <input placeholder="Phí phát sinh" class="form-control" data-val="true" data-val-required="" id="txtSoDienThoaiNguoiNhan" runat="server" name="Content.ContentName" type="text" value="" />
                            </div>
                          <%--  <div class="col-md-1" style="text-align: center">
                                <i class="fa fa-star"></i>
                            </div>--%>
                        </div>

                        <div class="col-md-12">
                            <div class="col-md-3">
                                <p>Trung chuyển: </p>
                            </div>
                            <div class="col-md-8">
                                <input placeholder="Trung chuyển" class="form-control" data-val="true" data-val-required="" id="txtNguoiNhan" runat="server" name="Content.ContentName" type="text" value="" />
                            </div>
                       <%--     <div class="col-md-1" style="text-align: center">
                                <i class="fa fa-star"></i>
                            </div>--%>
                        </div>


                        <div class="col-md-12">
                            <div class="col-md-3">
                                <p>Xe cẩu: </p>
                            </div>
                            <div class="col-md-8">
                                <input placeholder="Xe cẩu" class="form-control" data-val="true" data-val-required="" id="txtDiaChiNguoiNhan" runat="server" name="Content.ContentName" type="text" value="" />
                            </div>
                         <%--   <div class="col-md-1" style="text-align: center">
                                <i class="fa fa-star"></i>
                            </div>--%>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-3">
                                <p>Bóc vác: </p>
                            </div>
                            <div class="col-md-8">
                                <input placeholder="Bóc vác" class="form-control" data-val="true" data-val-required="" id="txtCongTyNguoiNhan" runat="server" name="Content.ContentName" type="text" value="" />
                            </div>
                        <%--    <div class="col-md-1" style="text-align: center">
                                <i class="fa fa-star"></i>
                            </div>--%>
                        </div>
                           <div class="col-md-12">
                            <div class="col-md-3">
                                <p>Nâng hạ Container: </p>
                            </div>
                            <div class="col-md-8">
                                <input placeholder="Nâng hạ Container:" class="form-control" data-val="true" data-val-required="" id="Text1" runat="server" name="Content.ContentName" type="text" value="" />
                            </div>
                           <%-- <div class="col-md-1" style="text-align: center">
                                <i class="fa fa-star"></i>
                            </div>--%>
                        </div>
                        <div class="col-md-12">
                                    <div class="col-md-3">
                                        <p>Phí qua phà: </p>
                                    </div>
                                
                              
                                     
                                    <div class="col-md-8">
                                        <input placeholder="Phí phà"  class="form-control" data-val="true" data-val-required="" id="txtPhiPha" runat="server" name="Content.ContentName" type="text" value="" />
                                    
                                    </div>
                              
                                </div>
                        <div hidden ="hidden" style ="display:none"  class="col-md-12">
                            <div class="col-md-3">
                                <p>Khu vực: </p>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="slTinh" AutoPostBack="true" runat="server" class="form-control" OnSelectedIndexChanged="slTinh_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="slHuyen" AutoPostBack="true" runat="server" class="form-control" OnSelectedIndexChanged="slHuyen_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Chọn Quận/Huyện</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <select id="slLoaiCuoc" runat="server" class="form-control"></select>
                            </div>
                        </div>

                        

                        <div  class="col-md-12">
                            <div class="col-md-3">
                                <p>Lưu xe (neo xe): </p>
                            </div>
                            <div class="col-md-8">
                                <input placeholder="Tiền cước" oninput="LoadTongTien(this)" class="form-control" data-val="true" data-val-required="" id="txtTienCuoc" runat="server" name="Content.ContentName" type="text" value="" />
                            </div>
                         <%--   <div class="col-md-1" style="text-align: center">
                                <i class="fa fa-star"></i>
                            </div>--%>
                        </div>

                        <div  class="col-md-12">
                            <div class="col-md-3">
                                <p>Cò xe: </p>
                            </div>
                            <div class="col-md-8">
                                <input placeholder="Phụ phí" oninput="LoadTongTien(this)" class="form-control" data-val="true" data-val-required="" id="txtPhuPhi" runat="server" name="Content.ContentName" type="text" value="" />
                            </div>
                        </div>

                        <div  class="col-md-12" style="padding-top: 10px">
                            <div class="col-md-3">
                                <p>Tổng phí: </p>
                            </div>
                            <div class="col-md-8">
                                <input placeholder="Tổng tiền" disabled="" class="form-control" data-val="true" data-val-required="" id="txtTongTien" runat="server" name="Content.ContentName" type="text" value="0" />
                            </div>
                          <%--  <div class="col-md-1" style="text-align: center">
                                <i class="fa fa-star"></i>
                            </div>--%>
                        </div>

                        <div hidden ="hidden" style ="display:none" class="col-md-12" style="padding-top: 10px">
                            <div class="col-md-3">
                                <p>Nhân viên giao: </p>
                            </div>
                            <div class="col-md-8">
                                <select id="slNhanVienGiao" runat="server" class="form-control">
                                    <option>--Chọn--</option>
                                </select>
                            </div>
                            <%--<div class="col-md-1" style="text-align: center">
                                <i class="fa fa-star"></i>
                            </div>--%>
                        </div>
                        
                        <div hidden ="hidden" style ="display:none" class="col-md-12" style="padding-top: 10px">
                            <div class="col-md-3">
                                <p>Tình trạng đơn: </p>
                            </div>
                            <div class="col-md-8">
                                <select id="slTinhTrangDonHang" runat="server" class="form-control">
                                    <option value="0">--Chọn--</option>
                                </select>
                            </div>
                            <div class="col-md-1" style="text-align: center">
                                <i class="fa fa-star"></i>
                            </div>
                        </div>
                    </div>
              <%--       <div class="row-fluid paddingbuttom30">--%>
                        <div  hidden ="hidden" style ="display:none"  class="span12 title1">
                            <div class="col-md-12" style="text-align: center; padding-bottom: 10px">
                                <h4><b><u>Thông tin hàng hóa</u></b></h4>
                            </div>
                           
                        </div>
                   <%-- </div>--%>
                       <div hidden ="hidden" style ="display:none"  class="col-md-6">
                                <div  class="col-md-12">
                                    <div class="col-md-3">
                                        <p>Tên hàng: </p>
                                    </div>
                                    <div class="col-md-7">
                                        <input placeholder="Tên hàng"  class="form-control" data-val="true" data-val-required="" id="txtTenHang" runat="server" name="Content.ContentName" type="text" value="" />
                                    </div>
                                    <div class="col-md-1" style="text-align: center">
                                        <i class="fa fa-star"></i>
                                    </div>
                                </div>
                                 <div  class="col-md-12">
                                    <div class="col-md-3">
                                        <p>Đơn Vị Tính: </p>
                                    </div>
                                    <div class="col-md-7">
                                        <input placeholder="DonViTinh"  class="form-control" data-val="true" data-val-required="" id="txtDonViTinh" runat="server" name="Content.ContentName" type="text" value="" />
                                    </div>
                                    <div class="col-md-1" style="text-align: center">
                                        <i class="fa fa-star"></i>
                                    </div>
                                </div>
                                <div  class="col-md-12">
                                    <div class="col-md-3">
                                        <p>Số lượng: </p>
                                    </div>
                                    <div class="col-md-7">
                                        <input placeholder="Số lượng"  class="form-control" data-val="true" data-val-required="" id="txtSoLuong" runat="server" name="Content.ContentName" type="text" value="" />
                                    </div>
                                    <div class="col-md-1" style="text-align: center">
                                        <i class="fa fa-star"></i>
                                    </div>
                                </div>
                                <div  class="col-md-12">
                                    <div class="col-md-3">
                                        <p>Trọng lượng: </p>
                                    </div>
                                    <div class="col-md-7">
                                        <input placeholder="Trọng lượng"  class="form-control" data-val="true" data-val-required="" id="txtTrongLuong" runat="server" name="Content.ContentName" type="text" value="" />
                                    </div>
                                    <div class="col-md-1" style="text-align: center">
                                        <i class="fa fa-star"></i>
                                    </div>
                                </div>
                                <div  class="col-md-12">
                                    <div class="col-md-3">
                                        <p>Số Khối: </p>
                                    </div>
                                    <div class="col-md-7">
                                        <input placeholder="Số khối"  class="form-control" data-val="true" data-val-required="" id="txtSoKhoi" runat="server" name="Content.ContentName" type="text" value="" />
                                    </div>
                                    <div class="col-md-1" style="text-align: center">
                                        <i class="fa fa-star"></i>
                                    </div>
                                </div>

                            <div class="col-md-10" style="padding-top: 10px">
                                <textarea id="txtGhiChu" style="width: 94.2%;" placeholder="Ghi chú" runat="server" class="form-control"></textarea>
                            </div>

                         </div>
                     <div hidden ="hidden" style ="display:none"  class="col-md-6">
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
                          <div class="col-md-12">
                            <div class="col-md-3">
                                <p>Loại thanh toán: </p>
                            </div>
                            <div class="col-md-8">
                                <select class="form-control" id="slLoaiThanhToan" runat="server">
                                    <option value="Lái xe">Lái Xe</option>
                                    <option value="Kế toán 1">Kế Toán 1 </option>
                                    <option value="Kế toán 2">Kế Toán 2 </option>
                                    <option value="Chuyển Khoản">Chuyển Khoản</option>
                                </select>
                            </div>
                            <div class="col-md-1" style="text-align: center">
                                <i class="fa fa-star"></i>
                            </div>
                        </div>
                            <div class="col-md-12">
                                    <div class="col-md-3">
                                        <p>Tiền Thanh Toán: </p>
                                    </div>
                                    <div class="col-md-4">
                                         <input placeholder="Tiền thanh toán"  class="form-control" data-val="true" data-val-required="" id="Text4" runat="server" name="Content.ContentName" type="text" value="" />
                                    </div>
                                 
                                     <div class="col-md-1" style="text-align: center">
                                        <i class="fa fa-star"></i>
                                    </div>
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
        field: document.getElementById('ContentPlaceHolder1_txtNgayNhanHang'),
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

