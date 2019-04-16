<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/MasterPage.master" AutoEventWireup="true" CodeFile="QuanLyDonHang-ImportExcel.aspx.cs" Inherits="QuanLyDonHang_QuanLyDonHang_ImportExcel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <form runat="server">
    <div class="content-wrapper">
    <!-- Main content -->
    
   
    <section class="content">


          <div class="container-fluid">
                    <div style="padding-left: 2%">
                        <div class="row-fluid paddingbuttom30">
                            <div class="span12 title1">
                                  <b id="dvTitle" runat="server">IMPORT EXCEL ĐƠN HÀNG</b>
                            </div>
                        </div>


                          <div class="col-md-6">
                                <div class="col-md-12">
                                <div class="col-md-3">
                                    <label class="control-label">Chọn kho (*): </label>
                                </div>
                                <div class="col-md-8">
                                       <select id="slKho" class="form-control" runat="server"></select>
                                </div>
                            </div>
                       
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <label class="control-label">Chọn khách hàng (*): </label>
                                </div>
                                <div class="col-md-8">
                                        <select id="slKhachHang" class="form-control" runat="server"></select>
                                </div>
                            </div>

                                                 

                       </div>


                          <div class="col-md-6">
                             
                       
                        
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <label class="control-label">Chọn file Excel(*): </label>
                                </div>
                                <div class="col-md-8">
                                      <asp:FileUpload class="form-control" ID="FileUpload1" runat="server" />
                                      
                                </div>
                            </div>
                             



                       </div>

                        
                <div class="col-md-12" style="text-align:center">
                    <asp:Button ID="btImportExcel" runat="server" Text="Import excel" class="btn btn-primary btn-flat" OnClick="btImportExcel_Click1" />
                    <asp:Button ID="btHuy" runat="server" Text="HỦY" class="btn btn-primary btn-flat" OnClick="btHuy_Click" />
                </div>

                        </div>


         </div>

    </section>
    <!-- /.content -->
  </div></form>
</asp:Content>

