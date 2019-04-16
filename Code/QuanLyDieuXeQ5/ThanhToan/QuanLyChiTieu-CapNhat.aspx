<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/MasterPage.master" AutoEventWireup="true" CodeFile="QuanLyChiTieu-CapNhat.aspx.cs" Inherits="QuanLyChiTieu_QuanLyChiTieu_CapNhat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <link href="../cssdatepicker/pikaday.css" rel="stylesheet" />
    <script src="../cssdatepicker/pikaday.js"></script>
    
    <form runat="server">
         <div class="content-wrapper">



    
    <!-- Main content -->
    
<%--    <div class="title1"><a href="QuanLyChiTieu.aspx"><i class="fa fa-step-backward"></i> Danh sách chi tiêu</a></div>--%>
    <section class="content">
      <div class="container-fluid">
                <div style="padding-left:2%" >

                    <div class="row-fluid paddingbuttom30">
                            <div class="span12 title1">
                                <div class="span6">
                                     <a href="../QuanLyChiTieu/QuanLyChiTieu.aspx" runat="server"> <i class="fa fa-arrow-left" style="padding-right:5px"></i>Quay lại</a>                              
                                    </div>
                                <div class="span6">
                                 <b id="dvTitle" runat="server">THÊM THÔNG TIN CHI TIÊU</b>
                                    </div>
                            </div>
                        </div>

                          <div class="col-md-6">
                                <div class="col-md-12">
                                <div class="col-md-3">
                                    <label class="control-label">Ngày chi (*): </label>
                                </div>
                                <div class="col-md-8">
                                   <input placeholder="Ngày chi" class="form-control" data-val="true" data-val-required="" id="txtNgayChi" runat="server" name="Content.ContentName" type="text" value="" />
                                </div>
                            </div>
                       
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <label class="control-label">Số tiền (*): </label>
                                </div>
                                <div class="col-md-8">
                                     <input placeholder="Số tiền(*)" oninput="format_curency(this)"  class="form-control" data-val="true" data-val-required="" id="txtSoTien" runat="server" name="Content.ContentName" type="text" value="" />
                                </div>
                            </div>
                             

                       </div>

                    <div class="col-md-6">
                                <div class="col-md-12">
                                <div class="col-md-3">
                                    <label class="control-label">Nội dung chi: </label>
                                </div>
                                <div class="col-md-8">
                                   <textarea id="txtNoiDung" class="form-control" style="width:95%;" runat="server"></textarea>
                                </div>
                            </div>
                                                                    
                       </div>

                </div>
          

                <div class="col-md-12" style="text-align:center">
                    <asp:Button ID="btLuu" runat="server" Text="LƯU" class="btn btn-primary btn-flat" OnClick="btLuu_Click" />
                    <asp:Button ID="btHuy" runat="server" Text="HỦY" class="btn btn-primary btn-flat" OnClick="btHuy_Click" />
                </div>

    </div>
    </section>
    <!-- /.content -->
  </div>
    <script src="../plugins/datetimePicker1/build/jquery.datetimepicker.full.js"></script>
    <script type="text/javascript">
        var picker = new Pikaday(
 {
     field: document.getElementById('ContentPlaceHolder1_txtNgayChi'),
     firstDay: 1,
     format: 'DD/MM/YYYY',
     minDate: new Date(1900, 1, 1),
     maxDate: new Date(2020, 12, 31),
     yearRange: [2000, 2020]
 });
        var picker = new Pikaday(
    {
        field: document.getElementById('ContentPlaceHolder1_txtDenNgay'),
        firstDay: 1,
        format: 'DD/MM/YYYY',
        minDate: new Date(1900, 1, 1),
        maxDate: new Date(3000, 12, 31),
        yearRange: [2000, 3000]
    });
    </script>
             </div>
             </form>
</asp:Content>

