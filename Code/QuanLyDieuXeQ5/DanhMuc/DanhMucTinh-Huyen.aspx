<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/MasterPage.master" AutoEventWireup="true" CodeFile="DanhMucTinh-Huyen.aspx.cs" Inherits="DanhMuc_DanhMucTinh_Huyen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script>
        function DeleteHuyen(idHuyen) {
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
                xmlhttp.open("GET", "../Ajax.aspx?Action=DeleteHuyen&idHuyen=" + idHuyen, true);
                xmlhttp.send();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <form runat="server">
    <div class="content-wrapper">
    <!-- Main content -->
    
  <%--  <div class="title1"><a href="DanhMucTinh.aspx"><i class="fa fa-step-backward"></i> Danh mục tỉnh</a></div>--%>
    <section class="content">

          <div class="container-fluid">
                    <div style="padding-left: 2%">
                       <div class="row-fluid paddingbuttom30">
                            <div class="span12 title1">
                                <div class="span6">
                                     <a href="../DanhMuc/DanhMucTinh.aspx" runat="server"> <i class="fa fa-arrow-left" style="padding-right:5px"></i>DANH SÁCH TỈNH</a>                              
                                    </div>
                                <div class="span6">
                                 <b id="dvTitle" runat="server">THÊM THÔNG TIN TỈNH</b>
                                    </div>
                            </div>
                        </div>


                          <div class="col-md-6">
                                <div class="col-md-12">
                                <div class="col-md-3">
                                    <label class="control-label">Tên tỉnh (*): </label>
                                </div>
                                <div class="col-md-8">
                                    <input placeholder="Tên tỉnh(*)" class="form-control" data-val="true" data-val-required="" id="txtTenTinh" runat="server" name="Content.ContentName" type="text" value="" />
                                </div>
                            </div>
                       
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <label class="control-label">Tên quận huyện (*): </label>
                                </div>
                                <div class="col-md-8">
                                      <input placeholder="Tên quận huyện(*)" class="form-control" data-val="true" data-val-required="" id="txtTenHuyen" runat="server" name="Content.ContentName" type="text" value="" />
                                </div>
                            </div>

                                                 

                       </div>


                          <div class="col-md-6">
                             
                       
                        
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <label class="control-label">Nhân viên giao (*): </label>
                                </div>
                                <div class="col-md-8">
                                          <select id="slNhanVienGiao" runat="server" class="form-control"></select>
                                </div>
                            </div>
                             
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <label class="control-label">Loại cước (*): </label>
                                </div>
                                <div class="col-md-8">
                                           <select id="slLoaiCuoc" runat="server" class="form-control"></select>
                                </div>
                            </div>


                       </div>

                        
                <div class="col-md-12" style="text-align:center">
                     <asp:Button ID="btLuu" runat="server" Text="LƯU" class="btn btn-primary btn-flat" OnClick="btLuu_Click" />
                    <asp:Button ID="btHuy" runat="server" Text="HỦY" class="btn btn-primary btn-flat" OnClick="btHuy_Click" />
                </div>

 
                        </div>

              <div class="col-md-12" style="padding-top:10px">
        <div id="dvDanhSachHuyen" runat="server"></div>
     </div>


         </div>


    </section>
    <!-- /.content -->
  </div>

    </form>
</asp:Content>

