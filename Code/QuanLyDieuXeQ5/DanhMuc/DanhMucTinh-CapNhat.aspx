<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/MasterPage.master" AutoEventWireup="true" CodeFile="DanhMucTinh-CapNhat.aspx.cs" Inherits="DanhMuc_DanhMucTinh_CapNhat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
                                     <a href="../DanhMuc/DanhMucTinh.aspx" runat="server"> <i class="fa fa-arrow-left" style="padding-right:5px"></i>DANH SÁCH TỈNH </a>                              
                                    </div>
                                <div class="span6">
                                 <b id="dvTitle" runat="server">THÊM THÔNG TIN TỈNH / TP</b>
                                    </div>
                            </div>
                        </div>


                          <div class="col-md-12">
                                <div class="col-md-12">
                                <div class="col-md-2">
                                    <label class="control-label">Tên tỉnh / Thành phố (*): </label>
                                </div>
                                <div class="col-md-9">
                                     <input placeholder="Tên tỉnh (*)" class="form-control" data-val="true" data-val-required="" id="txtTenTinh" runat="server" name="Content.ContentName" type="text" value="" />
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

