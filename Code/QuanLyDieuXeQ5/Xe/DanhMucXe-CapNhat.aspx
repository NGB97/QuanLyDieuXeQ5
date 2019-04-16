<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/MasterPage.master" AutoEventWireup="true" CodeFile="DanhMucXe-CapNhat.aspx.cs" Inherits="DanhMuc_DanhMucTinh_CapNhat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../dist/jquery-ui-1.11.3/jquery-ui.js"></script>
    <link href="../dist/jquery-ui-1.11.3/jquery-ui.css" rel="stylesheet" />
    <link href="../cssdatepicker/pikaday.css" rel="stylesheet" />
    <script src="../cssdatepicker/pikaday.js"></script>
    <script>
        window.onload = function () {
            TaiXeAutocomplete();
            
         }
                function TaiXeAutocomplete() {
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
                            $("#ContentPlaceHolder1_txtTenTaiXe").autocomplete({

                                minLength: 0,
                                source: listKhachHangAutocomplete,
                                focus: function (event, ui) {
                                    $("#ContentPlaceHolder1_txtTenTaiXe").val(ui.item.value);
                                    return false;
                                },
                                select: function (event, ui) {
                                    //GetMaDonHang(ui.item.id);
                                    $("#ContentPlaceHolder1_txtTenTaiXe").val(ui.item.value);
                                    $("#ContentPlaceHolder1_txtSDTTaiXe").val(ui.item.ten);
                                    $("#ContentPlaceHolder1_hdIdTaiXe").val(ui.item.id);
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
                xmlhttp.open("GET", "../Ajax.aspx?Action=TaiXeAutocomplete", true);
                xmlhttp.send();
                }
         </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <form  runat="server">
    <div class="content-wrapper">
    <!-- Main content -->
   
   <%-- <div class="title1"><a href="DanhMucTinh.aspx"><i class="fa fa-step-backward"></i> Danh mục tỉnh</a></div>--%>
    <section class="content">


        <div class="container-fluid">
                    <div style="padding-left: 2%">
                    <div class="row-fluid paddingbuttom30">
                            <div class="span12 title1">
                                <div class="span6">
                                     <a href="../DanhMuc/DanhMucXe.aspx" runat="server"> <i class="fa fa-arrow-left" style="padding-right:5px"></i>DANH SÁCH XE </a>                              
                                    </div>
                                <div class="span6">
                                 <b id="dvTitle" runat="server">THÊM THÔNG TIN LOẠI XE</b>
                                    </div>
                            </div>
                        </div>


                          <div hidden ="hidden" style ="display:none" class="col-md-12">
                                <div class="col-md-12">
                                <div class="col-md-2">
                                    <label class="control-label">Tên tỉnh / Thành phố (*): </label>
                                </div>
                                <div class="col-md-9">
                                     <input placeholder="Tên tỉnh (*)" class="form-control" data-val="true" data-val-required="" id="txtTenTinh" runat="server" name="Content.ContentName" type="text" value="" />
                                </div>
                            </div>
                                              
                                               
                       </div>
                         <div class="col-md-6">
                        <div class="col-md-12" style="text-align: center; padding-bottom: 10px">
                            <h4><b><u>Thông tin Xe</u></b></h4>
                        </div>
                        <div hidden ="hidden" style ="display:none" class="col-md-12">
                            <div class="col-md-3">
                                <p>Mã Xe: </p>
                            </div>
                            <div class="col-md-7">
                                <input placeholder="Mã Xe (*)"  disabled="" class="form-control" data-val="true" data-val-required="" id="txtMaXe" runat="server" name="Content.ContentName" type="text" value="" />
                            </div>
                            <div class="col-md-1" style="text-align: center">
                                <i class="fa fa-star"></i>
                            </div>
                        </div>
                        <div  hidden ="hidden" style ="display:none" class="col-md-12">
                            <div class="col-md-3">
                                <p>Tên Xe: </p>
                            </div>
                            <div class="col-md-7">
                                <input placeholder="Tên Xe" class="form-control" data-val="true" data-val-required="" id="txtTenXe" runat="server" name="Content.ContentName" type="text" value="" />
                               
                            </div>
                      
                        </div>
                        <div hidden ="hidden" style ="display:none"  class="col-md-12">
                            <div class="col-md-3">
                                <p>Loại Xe: </p>
                            </div>
                            <div class="col-md-7">
                                <select class="form-control" id="slLoaiXe" runat="server">
                                    <option value="3 chân">3 chân (15 tấn)</option>
                                    <option value="4 chân">4 chân (20 tấn) </option>
                                    <option value="Đầu kéo container">Đầu kéo container</option>
                                </select>
                            </div>
                            <div class="col-md-1" style="text-align: center">
                                <i class="fa fa-star"></i>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="col-md-3">
                                <p>Biển Số Xe: </p>
                            </div>
                            <div class="col-md-7">
                                <input placeholder="Biển số xe(*)" class="form-control" data-val="true" data-val-required="" id="txtBienSoXe" runat="server" name="Content.ContentName" type="text" value="" />
                            </div>
                            <div class="col-md-1" style="text-align: center">
                                <i class="fa fa-star"></i>
                            </div>
                        </div>


                       
                        <div hidden ="hidden" style ="display:none" class="col-md-12">
                            <div class="col-md-3">
                                <p>Người gửi: </p>
                            </div>
                            <div class="col-md-7">
                                <input placeholder="Người gửi" disabled="" class="form-control" data-val="true" data-val-required="" id="txtNguoiGui" runat="server" name="Content.ContentName" type="text" value="" />
                            </div>
                            <div class="col-md-1" style="text-align: center">
                                <i class="fa fa-star"></i>
                            </div>
                        </div>

                       


                       


                      
                      
                    </div>

                    <div class="col-md-6">
                        <div class="col-md-12" style="text-align: center; padding-bottom: 10px">
                            <h4><b><u>Thông tin tài xế</u></b></h4>
                        </div>

                        
                          <div class="col-md-12">
                            <div class="col-md-3">
                                <p>Tên Tài Xế: </p>
                            </div>
                            <div class="col-md-8">
                                <input placeholder="Tên Tài Xe" class="form-control" data-val="true" data-val-required="" id="txtTenTaiXe" runat="server" name="Content.ContentName" type="text" value="" />
                                <input type="hidden" id="hdIdTaiXe" runat="server" />
                            </div>
                            <div class="col-md-1" style="text-align: center">
                                <i class="fa fa-star"></i>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="col-md-3">
                                <p>Điện thoại: </p>
                            </div>
                            <div class="col-md-8">
                                <input placeholder="Số điện thoại tài xế" class="form-control" data-val="true" data-val-required="" id="txtSDTTaiXe" runat="server" name="Content.ContentName" type="text" value="" />
                            </div>
                            <div class="col-md-1" style="text-align: center">
                                <i class="fa fa-star"></i>
                            </div>
                        </div>



                        <div hidden ="hidden" style ="display:none"  class="col-md-12">
                            <div class="col-md-3">
                                <p>Địa chỉ: </p>
                            </div>
                            <div class="col-md-8">
                                <input placeholder="Địa chỉ " class="form-control" data-val="true" data-val-required="" id="txtDiaChiTaiXe" runat="server" name="Content.ContentName" type="text" value="" />
                            </div>
                            <div class="col-md-1" style="text-align: center">
                                <i class="fa fa-star"></i>
                            </div>
                        </div>
                        <div hidden ="hidden" style ="display:none"  class="col-md-12">
                            <div class="col-md-3">
                                <p>Số CMND: </p>
                            </div>
                            <div class="col-md-8">
                                <input placeholder="Số CMND" class="form-control" data-val="true" data-val-required="" id="txtSoCMND" runat="server" name="Content.ContentName" type="text" value="" />
                            </div>
                            <div class="col-md-1" style="text-align: center">
                                <i class="fa fa-star"></i>
                            </div>
                        </div>

                    
                        

                

                        
                        
                       
                    </div>


                                    
                <div class="col-md-12" style="text-align:center">
                  <asp:Button ID="btLuu" runat="server" Text="LƯU" class="btn btn-primary btn-flat" OnClick="btLuu_Click" />
                    <asp:Button ID="btHuy" runat="server" Text="HỦY" class="btn btn-primary btn-flat" OnClick="btHuy_Click" />
                </div>

                        </div>
         </div>

    </section>
    <!-- /.content -->
  </div></form>
</asp:Content>

