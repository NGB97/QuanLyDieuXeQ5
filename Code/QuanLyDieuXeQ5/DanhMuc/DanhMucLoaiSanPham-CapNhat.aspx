<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/MasterPage.master" AutoEventWireup="true" CodeFile="DanhMucLoaiSanPham-CapNhat.aspx.cs" Inherits="DanhMuc_DanhMucLoaiSanPham_CapNhat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <form  runat="server">
    <div class="content-wrapper">
    <!-- Main content -->
   
    <%--<div class="title1"><a href="DanhMucLoaiSanPham.aspx"><i class="fa fa-step-backward"></i> Danh mục loại sản phẩm</a></div>--%>
    <section class="content">


          <div class="container-fluid">
                    <div style="padding-left: 2%">
                       <div class="row-fluid paddingbuttom30">
                            <div class="span12 title1">
                                <div class="span6">
                                     <a href="../DanhMuc/DanhMucLoaiSanPham.aspx" runat="server"> <i class="fa fa-arrow-left" style="padding-right:5px"></i>DANH SÁCH LOẠI SẢN PHẨM</a>                              
                                    </div>
                                <div class="span6">
                                 <b id="dvTitle" runat="server">THÊM THÔNG TIN LOẠI SẢN PHẨM</b>
                                    </div>
                            </div>
                        </div>


                          <div class="col-md-6">
                                <div class="col-md-12">
                                <div class="col-md-3">
                                    <label class="control-label">Tên loại sản phẩm (*): </label>
                                </div>
                                <div class="col-md-8">
                                    <input class="form-control" data-val="true" data-val-required="" id="txtTenLoaiSanPham" runat="server" name="Content.ContentName" type="text" value="" placeholder="Tên loại sản phẩm(*)" />
                                </div>
                            </div>
                       
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <label class="control-label">Đơn vị tính: </label>
                                </div>
                                <div class="col-md-8">
                                      <input class="form-control" data-val="true" data-val-required="" id="txtDonViTinh" runat="server" name="Content.ContentName" type="text" value="" placeholder="Đơn vị tính" />
                                </div>
                            </div>

                                                 

                       </div>


                          <div class="col-md-6">
                             
                       
                        
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <label class="control-label">Ghi chú: </label>
                                </div>
                                <div class="col-md-8">
                                        <textarea id="txtGhiChu" runat="server" class="form-control" style="width:95%; height:65px" placeholder="Ghi chú"></textarea>
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
  </div>
           </form>
</asp:Content>

