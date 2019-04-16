<%@ Page Language="C#" Title="" EnableEventValidation="false" AutoEventWireup="true"  MasterPageFile="~/Layout/MasterPage.master" CodeFile="QuanLyVeXe-CapNhat.aspx.cs" Inherits="QuanLyVeXe_QuanLyVeXe_CapNhat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script src="../dist/jquery-ui-1.11.3/jquery-ui.js"></script>
    <link href="../dist/jquery-ui-1.11.3/jquery-ui.css" rel="stylesheet"/>
    <link href="../cssdatepicker/pikaday.css" rel="stylesheet" />
    <script src="../cssdatepicker/pikaday.js"></script>
     <script>
         window.onload = function () {
             
           //  ChiNhanhGuiAutocomplete();
            <%-- ChiNhanhNhanAutocomplete();
             LoadDSChiTietDonHang('<%= Request.QueryString["idDonHang"] %>');--%>
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

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content1">
        <form runat="server">
            <div id="content-header">
                <div id="breadcrumb">
                    <a href="DanhMucNguoiDung.aspx" title="" class="tip-bottom"><i class="icon-home"></i>Người dùng</a>
                    <a class="current">Thêm mới</a>
                </div>
                <h1 class="title1" id="dvTitle" runat="server">Nhập Thông Tin Vé</h1>
            </div>
            <div class="container-fluid">
                <div class="row-fluid">
                    <div class="col-md-6">
                        <div class="col-md-12">
                            <div class="col-md-3">
                                <b>Mã Vé: </b>
                            </div>
                            <div class="col-md-8">
                                <input class="form-control" data-val="true" data-val-required="" id="txtHoTen" runat="server" name="Content.ContentName" type="text" value="" placeholder="Mã tạo tự động" />
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="col-md-3">
                                <b>Ngày Xuất Bến: </b>
                            </div>
                            <div class="col-md-8">
                                <input class="form-control" data-val="true" data-val-required="" id="txtSoDienThoai" runat="server" name="Content.ContentName" type="text" value="" placeholder="Ngày Khởi Hành" />
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="col-md-3">
                                <b>Giờ Khởi Hành: </b>
                            </div>
                            <div class="col-md-8">
                                <select >
                                    <option>2 giờ</option>
                                  <option>8 giờ</option>
                                    <option>12 giờ</option>
                                    <option>15 giờ</option>
                               
                                </select>
                               <%-- <input class="form-control" data-val="true" data-val-required="" id="txtEmail" runat="server" type="text" value="" placeholder="Chọn Giờ Khởi Hành" />--%>
                            </div>
                        </div>


                        

                         <div class="col-md-12">
                            <div class="col-md-3">
                                <b>Thời Gian Mua Vé: </b>
                            </div>
                            <div class="col-md-8">
                                <input class="form-control" data-val="true" data-val-required="" id="txtTenDangNhap" runat="server" name="Content.ContentName" type="text" value="" placeholder="Ngày giờ" />
                            </div>
                        </div>
                        
                        <div class="col-md-12">
                            <div class="col-md-3">
                                <b>Tên ghế </b>
                            </div>
                            <div class="col-md-8">
                                <input placeholder="" class="form-control" data-val="true" data-val-required="" id="txtChiNhanhGui" runat="server" name="Content.ContentName" type="text" value="" />
                                <input type="hidden" id="hdIDChiNhanhGui" runat="server" /> 
                            </div>
                            
                        </div>
                       

                    </div>


                    <div class="col-md-6">
                        <div class="col-md-12">
                            <div class="col-md-3">
                                <b>Tên Hành Khách: </b>
                            </div>
                            <div class="col-md-8">

                                <input class="form-control" data-val="true" data-val-required="" id="txtDiaChi" runat="server" type="text" value="" placeholder="Họ và Tên" />
                            </div>
                        </div>
                       

                        <div class="col-md-12">
                            <div class="col-md-3">
                                <b>Tiền Vé: </b>
                            </div>
                            <div class="col-md-8">
                                <input class="form-control" data-val="true" data-val-required="" id="txtMatKhau" runat="server" name="Content.ContentName" type="text" value="" placeholder="" />
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="col-md-3">
                                <b>Tình Trạng Thanh Toán </b>
                            </div>
                            <div class="col-md-8">
                                <select >
                                    <option>Đã Thanh Toán</option>
                                  <option>Chưa Thanh Toán</option>
                               
                                </select>
                            </div>
                        </div>
                         <div class="col-md-12">
                            <div class="col-md-3">
                                <b>Số Lượng Vé: </b>
                            </div>
                            <div class="col-md-8">

                                <input class="form-control" data-val="true" data-val-required="" id="Text1" runat="server" type="text" value="" placeholder="" />
                            </div>
                        </div>


                          
                        <div class="col-md-12">
                            <div class="col-md-3">
                                <b>Tổng Tiền: </b>
                            </div>
                            <div class="col-md-8">

                                <input class="form-control" data-val="true" data-val-required="" id="Text2" runat="server" type="text" value="" placeholder="" />
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12" style="text-align: center; padding-top: 30px;">
                        <asp:Button ID="btLuu" runat="server" Text="LƯU VÀ ĐÓNG" class="btn btn-primary btn-flat" OnClick="btLuu_Click" />
                        <asp:Button ID="btHuy" runat="server" Text="HỦY" class="btn btn-primary btn-flat" OnClick="btHuy_Click" />
                    </div>

                </div>
            </div>
             <script type="text/javascript">

            var picker = new Pikaday(
      {
          field: document.getElementById('ContentPlaceHolder1_txtSoDienThoai'),
          firstDay: 1,
          format: 'DD/MM/YYYY',
          minDate: new Date(1900, 1, 1),
          maxDate: new Date(2020, 12, 31),
          yearRange: [2000, 2020]
      });
            var picker = new Pikaday(
        {
            field: document.getElementById('ContentPlaceHolder1_txtTenDangNhap'),
            firstDay: 1,
            format: 'DD/MM/YYYY',
            minDate: new Date(1900, 1, 1),
            maxDate: new Date(3000, 12, 31),
            yearRange: [2000, 3000]
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



        </script>
        </form>
    </div>
</asp:Content>




