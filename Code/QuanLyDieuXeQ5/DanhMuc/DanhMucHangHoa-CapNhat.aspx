<%@ Page Language="C#" MasterPageFile="~/Layout/MasterPage.master" AutoEventWireup="true" CodeFile="DanhMucHangHoa-CapNhat.aspx.cs" Inherits="DanhMuc_DanhMucLoaiCuoc_CapNhat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <form runat="server">
        <div class="content-wrapper">
            <!-- Main content -->

            <%-- <div class="title1"><a href="DanhMucKho.aspx"><i class="fa fa-step-backward"></i> Danh mục kho</a></div>--%>
            <section class="content">
                <div class="container-fluid">
                    <div style="padding-left: 2%">
                        <div class="row-fluid paddingbuttom30">
                            <div class="span12 title1">
                                <div class="span6">
                                    <a href="../DanhMuc/DanhMucPha.aspx" runat="server"><i class="fa fa-arrow-left" style="padding-right: 5px"></i>Quay lại</a>
                                </div>
                                <div class="span6">
                                    <b id="dvTitle" runat="server">THÊM HÀNG HÓA</b>
                                </div>
                            </div>
                        </div>

                        <div hidden ="hidden" style ="display:none" class="col-md-6">
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <label class="control-label">Mã hàng hóa: </label>
                                </div>
                                <div class="col-md-7">
                                    <input placeholder="Mã loại cước" disabled=""  class="form-control" data-val="true" data-val-required="" id="txtMaLoaiCuoc" runat="server" name="Content.ContentName" type="text" value="" />
                                </div>
                                <div class="col-md-1" style="text-align:center">
                                    <i class="fa fa-star"></i>
                                </div>
                            </div>
                            
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <label class="control-label">Giá cước: </label>
                                </div>
                                <div class="col-md-7">
                                    <input placeholder="Giá cước" id="tt" oninput="format_curency(this)" runat="server" class="form-control"/>
                                </div>
                            </div>
                        </div>

                        <div hidden ="hidden" style ="display:none" class="col-md-6">
                             <div class="col-md-12">
                                <div class="col-md-3">
                                    <label class="control-label">Tên hàng hóa: </label>
                                </div>
                                <div class="col-md-7">
                                    <input placeholder="Tên loại cước" class="form-control" data-val="true" data-val-required="" id="txtTenLoaiCuoc" runat="server" name="Content.ContentName" type="text" value="" />
                                </div>
                                <div class="col-md-1" style="text-align: center">
                                    <i class="fa fa-star"></i>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <label class="control-label">Mã hàng hóa : </label>
                                </div>
                                <div class="col-md-7">
                                    <input placeholder="Mã hàng hóa" disabled=""  class="form-control" data-val="true" data-val-required="" id="txtMaHangHoa" runat="server" name="Content.ContentName" type="text" value="" />
                                </div>
                                <div class="col-md-1" style="text-align:center">
                                    <i class="fa fa-star"></i>
                                </div>
                            </div>
                            
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <label class="control-label">Giá cước: </label>
                                </div>
                                <div class="col-md-7">
                                    <input placeholder="Giá cước" id="txtGiaCuoc" oninput="format_curency(this)" runat="server" class="form-control"/>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-6">
                             <div class="col-md-12">
                                <div class="col-md-3">
                                    <label class="control-label">Tên hàng hóa: </label>
                                </div>
                                <div class="col-md-7">
                                    <input placeholder="Tên hàng hóa" class="form-control" data-val="true" data-val-required="" id="txtTenHangHoa" runat="server" name="Content.ContentName" type="text" value="" />
                                </div>
                                <div class="col-md-1" style="text-align: center">
                                    <i class="fa fa-star"></i>
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
</asp:Content>
