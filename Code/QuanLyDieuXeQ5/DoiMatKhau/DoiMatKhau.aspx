<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/MasterPage.master" AutoEventWireup="true" CodeFile="DoiMatKhau.aspx.cs" Inherits="DoiMatKhau_DoiMatKhau" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> <form runat="server">
    <div class="content-wrapper">
    <!-- Main content -->

    <section class="content">
         <div id="content-header">
       <div id="breadcrumb"> <a href="index.html" class="tip-bottom" data-original-title="" title=""><i class="icon-user"></i> ĐỔI MẬT KHẨU</a></div>
        </div>
    <div class="container-fluid">
        <div class="quick-actions_homepage">
    
                              <input placeholder="Mật khẩu cũ" class="form-control" data-val="true" data-val-required="" id="txtMatKhauCu" runat="server" name="Content.ContentName" type="password" value="" />
                         
                              <input placeholder="Mật khẩu mới" class="form-control" data-val="true" data-val-required="" id="txtMatKhauMoi" runat="server" name="Content.ContentName" type="password" value="" />
                         
                              <input placeholder="Nhập lại" class="form-control" data-val="true" data-val-required="" id="txtNhapLai" runat="server" name="Content.ContentName" type="password" value="" />
                          </div>

                <div class="quick-actions_homepage">
                    <asp:Button ID="btLuu" runat="server" Text="LƯU" class="btn btn-primary btn-flat" OnClick="btLuu_Click" />
                    <asp:Button ID="btHuy" runat="server" Text="HỦY" class="btn btn-primary btn-flat" OnClick="btHuy_Click" />
                </div>
           
        </div>
  
    </section>
    <!-- /.content -->
  </div> </form>
</asp:Content>

