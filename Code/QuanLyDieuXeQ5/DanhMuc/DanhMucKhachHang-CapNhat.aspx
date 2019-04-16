<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/MasterPage.master" AutoEventWireup="true" CodeFile="DanhMucKhachHang-CapNhat.aspx.cs" Inherits="DanhMuc_DanhMucKho_CapNhat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <form  runat="server">
    <div class="content-wrapper">
    <!-- Main content -->
   
   <%-- <div class="title1"><a href="DanhMucKho.aspx"><i class="fa fa-step-backward"></i> Danh mục kho</a></div>--%>
    <section class="content">


             <div class="container-fluid">
                    <div style="padding-left: 2%">
                       <div class="row-fluid paddingbuttom30">
                            <div class="span12 title1">
                                <div class="span6">
                                     <a href="../DanhMuc/DanhMucKhachHang.aspx" runat="server"> <i class="fa fa-arrow-left" style="padding-right:5px"></i>DANH SÁCH KHÁCH HÀNG</a>                              
                                    </div>
                                <div class="span6">
                                 <b id="dvTitle" runat="server">THÊM THÔNG TIN KHÁCH HÀNG</b>
                                    </div>
                            </div>
                        </div>


                          <div class="col-md-6">
                               <div class="col-md-12">
                                    <div class="col-md-3">
                                        <label class="control-label">Mã khách hàng (*): </label>
                                    </div>
                                    <div class="col-md-8">
                                         <input placeholder="Mã khách hàng(*)" disabled="" class="form-control" data-val="true" data-val-required="" id="txtMaKhachHang" runat="server" name="Content.ContentName" type="text" value="" />
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <label class="control-label">Tên khách hàng (*): </label>
                                    </div>
                                    <div class="col-md-8">
                                         <input placeholder="Tên khách hàng(*)" class="form-control" data-val="true" data-val-required="" id="txtTenKhachHang" runat="server" name="Content.ContentName" type="text" value="" />
                                    </div>
                                 </div>
                       
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <label class="control-label">Số Điện thoại: </label>
                                    </div>
                                    <div class="col-md-8">
                                        <input placeholder="số điện thoại" class="form-control" data-val="true" data-val-required="" id="txtSoDienThoai" runat="server" name="Content.ContentName" type="text" value="" />
                                    </div>
                                </div>

                                                 

                       </div>


                         <div class="col-md-6">
                             
                       
                        
                                <div class="col-md-12">
                                        <div class="col-md-3">
                                            <label class="control-label">Email(*): </label>
                                        </div>
                                        <div class="col-md-8">
                                            <input placeholder="Email(*)" class="form-control" data-val="true" data-val-required="" id="txtEmail" runat="server" name="Content.ContentName" type="text" value="" />
                                        </div>
                                 </div>
                                <div class="col-md-12">
                                        <div class="col-md-3">
                                            <label class="control-label">Địa chỉ (*): </label>
                                        </div>
                                        <div class="col-md-8">
                                             <input placeholder="Địa chỉ(*)" class="form-control" data-val="true" data-val-required="" id="txtDiaChi" runat="server" name="Content.ContentName" type="text" value="" />
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
  </div> </form>
</asp:Content>

