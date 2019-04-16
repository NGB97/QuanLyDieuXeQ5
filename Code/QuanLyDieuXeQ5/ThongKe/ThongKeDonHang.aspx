<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/MasterPage.master" AutoEventWireup="true" CodeFile="ThongKeDonHang.aspx.cs" Inherits="ThongKe_ThongKeDonHang" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../dist/jquery-ui-1.11.3/jquery-ui.js"></script>
    <link href="../dist/jquery-ui-1.11.3/jquery-ui.css" rel="stylesheet"/>
     <link href="../cssdatepicker/pikaday.css" rel="stylesheet" />
    <script src="../cssdatepicker/pikaday.js"></script>
    <script>
        window.onload = function () {
            KhachHangAutocomplete();
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
                                //$( "#results").text($("#topicID").val());    
                                //alert($("#hdIdKhuVuc").val());
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
                        document.getElementById("dvXemChiTietDonHang").innerHTML = xmlhttp.responseText;
                        MoXemChiTietDonHang();
                    }
                }
            }
            xmlhttp.open("GET", "../Ajax.aspx?Action=XemChiTietDonHang&idDonHang=" + idDonHang, true);
            xmlhttp.send();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <form runat="server">
        <div class="content-wrapper">
            <!-- Main content -->
            <section class="content">
                <div class="container-fluid">
                    <div class="row-fluid">
                        <div class="span12">
                            <div class="container-fluid">
                                <div class="form-group row-fluid">
                                    <div class="span4 title1">
                                        <b>THỐNG KÊ ĐƠN HÀNG</b>
                                    </div>
                                    <div class="span8 CanhPhai">
                                        <asp:LinkButton class="btn btn-primary" runat="server" OnClick="btTimKiem_Click"><i class="fa fa-search"></i><span>Tìm kiếm</span></asp:LinkButton>
                                        <%--<a id="btThemMoi" runat = "server" class="btn btn-primary" href="QuanLyDonHang-CapNhat.aspx"><i class="fa fa-plus"></i><span>Thêm mới</span></a>
                                        <a id="btImportExcel" runat="server" class="btn btn-primary" href="QuanLyDonHang-ImportExcel.aspx"><i class="fa fa-cloud-download"></i><span style="margin-left: 5px;">Import excel</span></a>--%>
                                        <%--<input id="btImportExcel" runat="server" type="button" class="btn btn-primary btn-flat" onclick="window.location = 'QuanLyDonHang-ImportExcel.aspx'" value="Import excel" />
                                        <a class="btn btn-primary" href="DanhMucHangHoa-CapNhat.aspx"><i class="fa fa-cloud-upload"></i><span style="margin-left: 5px;">Export</span></a>--%>
                                    </div>

                                </div>
                                <div class="form-group row-fluid">
                                     <div class="col-lg-2">
                                        <div class="input-group">
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            <input type="text" class="form-control" placeholder="Từ ngày" id="txtTuNgay" runat="server" style="width: 88%;"/>
                                        </div>
                                    </div>
                                     <div class="col-lg-2">
                                        <div class="input-group">
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            <input type="text" class="form-control" placeholder="Đến ngày" id="txtDenNgay" runat="server" style="width: 88%;"/>
                                        </div>
                                    </div>
                                     <div class="col-lg-2">
                                        <div class="input-group">
                                            <span class="input-group-addon"><i class="fa fa-barcode"></i></span>
                                            <input type="text" class="form-control" placeholder="Tìm kiếm theo mã đơn hàng" id="txtMaDonHang" runat="server" style="width: 88%;"/>
                                        </div>
                                    </div>
                                     <div class="col-lg-2">
                                        <div class="input-group">
                                            <span class="input-group-addon"><i class="fa fa-user"></i></span>
                                            <input type="text" class="form-control" placeholder="Tên khách hàng" id="txtTenKhachHang" runat="server" style="width: 88%;"/>
                                            <input type="hidden" id="hdIdKhachHang" runat="server" />
                                        </div>
                                    </div>


                                    <div class="col-lg-2" style="display:none">
                                        <div class="input-group">
                                            <span class="input-group-addon"><i class="fa fa-cubes"></i></span>
                                            <select id="slKho" runat="server" class="form-control">
                              </select>
                                        </div>
                                    </div>

                                    
                                    <div class="col-lg-2">
                                        <div class="input-group">
                                            <span class="input-group-addon"><i class="fa fa-flag"></i></span>
                                            <select id="slNhanVienGiao" runat="server" class="form-control">
                              </select>
                                        </div>
                                    </div>




                                    <div class="col-lg-2">
                                        <div class="input-group">
                                            <span class="input-group-addon"><i class="fa fa-flag"></i></span>
                                            <select id="slTinhTrang" runat="server" class="form-control">
                              </select>
                                        </div>
                                    </div>


                                </div>
                                
                                <div class="row-fluid">


                                    <div class="span12">




                                        <div id="dvDanhSachDonHang" style="overflow: auto; margin-top: 10px" runat="server">
                                        </div>


                                    </div>


                                </div>


                            </div>



                        </div>
                    </div>
                </div>
            </section>
            <!-- /.content -->
        </div>

    
<!--Popup xem chi tiết đơn hàng-->
    <div id="lightXemChiTietDonHang" class="white_content" style=" top: 10%; width: 70%; left: 15%; height: 80%;">
        <div class="box">
            <div id="dvXemChiTietDonHang" class="box-body">
                
            </div>
        </div>
    </div>
    <div id="fadeXemChiTietDonHang" onclick="DongXemChiTietDonHang()" class="black_overlay"></div>
    <!--End popup--->

    <!-- /.content -->
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

