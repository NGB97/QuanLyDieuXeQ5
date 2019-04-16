<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/MasterPage.master" AutoEventWireup="true" CodeFile="DanhMucChiNhanh-CapNhat.aspx.cs" Inherits="DanhMuc_DanhMucKhachHang_CapNhat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <form runat="server">
            <div class="container-fluid">
                <!-- Main content -->

                <%--  <div class="title1"><a href="DanhMucKhachHang.aspx"><i class="fa fa-step-backward"></i> Danh mục khách hàng</a></div>--%>
                <section class="content">
                    <div class="container-fluid">
                        <div style="padding-left: 2%">
                            <div class="row-fluid paddingbuttom30">
                                <div class="span12 title1">
                                    <div class="span6">
                                        <a href="../DanhMuc/DanhMucKhachHang.aspx" runat="server"><i class="fa fa-arrow-left" style="padding-right: 5px"></i>DANH SÁCH KHÁCH HÀNG</a>
                                    </div>
                                    <div class="span6">
                                        <b id="dvTitle" runat="server">THÊM THÔNG TIN CHI NHÁNH</b>
                                    </div>
                                </div>
                            </div>


                            <div class="col-md-6">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <label class="control-label">Mã chi nhánh (*): </label>
                                    </div>
                                    <div class="col-md-8">
                                        <input disabled="" class="form-control" data-val="true" data-val-required="" id="txtMaChiNhanh" runat="server" name="Content.ContentName" type="text" value="" placeholder="Tên khách hàng(*)" />
                                    </div>
                                </div>

                                 <div class="col-md-12">
                                    <div class="col-md-3">
                                        <label class="control-label">Tên chi nhánh: </label>
                                    </div>
                                    <div class="col-md-8">
                                        <input class="form-control" data-val="true" data-val-required="" id="txtTenChiNhanh" runat="server" name="Content.ContentName" type="text" value="" placeholder="TenChiNhanh" />
                                    </div>
                                </div>

                  

                               

                                 <div hidden ="hidden" style ="display:none" class="col-md-12" style="text-align: center">
                                    <div class="col-md-3">
                                        <label class="control-label">Loại Khách: </label>
                                    </div>
                                    <div class="col-md-4">
                                        <input type="radio" id="ckbKhachSi" checked="" name="thanhtoan" class='flat-red' runat="server" />
                                        <br />
                                        Khách sỉ:
                                    </div>
                                    <div class="col-md-4">
                                        <input type="radio" id="ckbKhachLe" name="thanhtoan" class='flat-red' runat="server" />
                                        <br />
                                        Khách lẻ:
                                    </div>
                                </div>

                            
                              



                            </div>


                            <div class="col-md-6">
                                 <div class="col-md-12">
                                    <div class="col-md-3">
                                        <label class="control-label">Số điện thoại: </label>
                                    </div>
                                    <div class="col-md-8">
                                        <input class="form-control" data-val="true" data-val-required="" id="txtSoDienThoai" runat="server" name="Content.ContentName" type="text" value="" placeholder="Số điện thoại" />
                                    </div>
                                </div>


                                  <div class="col-md-12">
                                    <div class="col-md-3">
                                        <label class="control-label">Địa Chỉ: </label>
                                    </div>
                                    <div class="col-md-8">
                                        <input class="form-control" data-val="true" data-val-required="" id="txtDiaChi" runat="server" name="Content.ContentName" type="text" value="" placeholder="Giá cước nội thành" />
                                    </div>
                                </div>
                                
                                 
                            </div>


                            <div class="col-md-12" style="text-align: center">
                                <asp:Button ID="btLuu" runat="server" Text="LƯU" class="btn btn-primary btn-flat" OnClick="btLuu_Click" />
                                <asp:Button ID="btHuy" runat="server" Text="HỦY" class="btn btn-primary btn-flat" OnClick="btHuy_Click" />
                            </div>

                        </div>


                    </div>

                </section>
                <!-- /.content -->
            </div>
        </form>
    </div>
</asp:Content>

