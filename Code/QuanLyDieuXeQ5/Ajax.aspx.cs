using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.IO;
using Zen.Barcode;

public partial class Ajax : System.Web.UI.Page
{
    string sTenDangNhap = "";
    string mQuyen = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["QuanLyHoaDongDifoco_Login"] != null && Session["QuanLyHoaDongDifoco_Login"].ToString() != "")
        //{
        //    sTenDangNhap = Session["QuanLyHoaDongDifoco_Login"].ToString();
        //    mQuyen = StaticData.getField("tb_Admin", "MaNhomAdmin", "TenDangNhap", sTenDangNhap);
        //}
        //else
        //{
        //    //Response.Redirect("Home/DangNhap.aspx");
        //}
        try
        {
            string action = Request.QueryString["Action"].Trim();
            switch (action)
            {
                case "DangXuat":
                    DangXuat(); break;
                case "DeleteNguoiDung":
                    DeleteNguoiDung(); break;
                case "DeleteKhachHang":
                    DeleteKhachHang(); break;
                case "DeleteLoaiSanPham":
                    DeleteLoaiSanPham(); break;
                case "DeleteKho":
                    DeleteKho(); break;
                case "DeleteDonHang":
                    DeleteDonHang(); break;
                case "TaiXeAutocomplete":
                    TaiXeAutocomplete(); break;
                case "KhachHangAutocomplete":
                    KhachHangAutocomplete(); break;

                case "LoadPopupLichSuNo":
                    LoadPopupLichSuNo(); break;
                case "LoadPopupLichSuNoNhaCungCap":
                    LoadPopupLichSuNoNhaCungCap(); break;
                case "PrinfHoaDon":
                    PrinfHoaDon(); break;
                case "PrinfHoaDonTem":
                    PrinfHoaDonTem(); break;
                //case "ChiNhanhAutocomplete":
                //    ChiNhanhAutocomplete(); break;
                case "ChonDonHangAutocomplete":
                    ChonDonHangAutocomplete();break;
                case "SanPhamAutocomplete":
                   SanPhamAutocomplete(); break;
                case "ChonSanPhamAutocomplete":
                    ChonSanPhamAutocomplete(); break;
                case "PhanXeAutocomplete":
                    PhanXeAutocomplete(); break;
                case "TinhTrangAutocomplete":
                    TinhTrangAutocomplete(); break;

                case "ChiNhanhNhanAutocomplete":
                    ChiNhanhNhanAutocomplete(); break;
                case "ChiNhanhGuiAutocomplete":
                    ChiNhanhGuiAutocomplete(); break;
                case "KhachHangAutocomplete1":
                    KhachHangAutocomplete1(); break;
                //Chi tiết đơn hàng
                case "DeleteChiTietDonHang":
                    DeleteChiTietDonHang(); break;
                case "LoadChiTietDonHang":
                    LoadChiTietDonHang(); break;
                case "LoadDSChiTietDonHang":
                    LoadDSChiTietDonHang(); break;
                case "ThemChiTietDonHang":
                    ThemChiTietDonHang(); break;
                case "SuaChiTietDonHang":
                    SuaChiTietDonHang(); break;
                case "XemChiTietDonHang":
                    XemChiTietDonHang(); break;

                case "LoadGiaCuoc":
                    LoadGiaCuoc(); break;
                case "TinhTienCuoc":
                    TinhTienCuoc(); break;
                case "DeleteChiTieu":
                    DeleteChiTieu(); break;
                case "TenHangAutocomplete":
                    TenHangAutocomplete(); break;

                case "GetMaDonHang":
                    GetMaDonHang(); break;
                case "DeleteTinh":
                    DeleteTinh(); break;
                case "DeleteHuyen":
                    DeleteHuyen(); break;

                case "CheckDaNhanTien":
                    CheckDaNhanTien(); break;
                case "SuaNhanhDonHang":
                    SuaNhanhDonHang(); break;


                case "LoadPopupHangNhanXem":
                    LoadPopupHangNhanXem(); break;


                case "LoadPopupHangGuiXem":
                    LoadPopupHangGuiXem(); break;


                case "LoadPopupThuNoXem":
                    LoadPopupThuNoXem(); break;

                case "LoadPopupCPNTraXem":
                    LoadPopupCPNTraXem(); break;

                case "LoadPopupCPNNhanXem":
                    LoadPopupCPNNhanXem(); break;

                case "LoadPopupCODNhanXem":
                    LoadPopupCODNhanXem(); break;

                case "LoadPopupCODTraGuiXem":
                    LoadPopupCODTraGuiXem(); break;

                case "LoadPopupChiKhacXem":
                    LoadPopupChiKhacXem(); break;

                case "PrinfBienNhanXeCho":
                    PrinfBienNhanXeCho(); break;
                case "PrinfPhanHangLenXe":
                    PrinfPhanHangLenXe(); break;
                case "PrinfBienNhanXuongVP":
                    PrinfBienNhanXuongVP(); break;
                case "DeleteLoaiCuoc":
                    DeleteLoaiCuoc();break;
            }
        }
        catch { }
    }

    private void LoadPopupHangGuiXem()
    {
        string idNguoiDung = StaticData.ValidParameter(Request.QueryString["idNguoiDung"].Trim());
        string NgayLap = StaticData.ValidParameter(Request.QueryString["NgayLap"].Trim());

        string sql = @" select MaDonHang,NguoiGui,TenKhachHang ,ThanhToan from tb_DonHang dh, tb_KhachHang kh
            where dh.idKhachHang = kh.idKhachHang and NguoiNhanTra = 0 and idNguoiDung = " + idNguoiDung + " and NgayLap = '" + NgayLap + "'";
        string html = @"<table class='table table-bordered table-hover dataTable'>
                            <tr style='white-space: nowrap;'>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    STT
                                </th>
                                 <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Mã Đơn Hàng
                                </th>
                                 <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Tên Người Gửi
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Tên Người Nhận
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Tiền Cước
                                </th>
                               
                                
                            </tr>";
        DataTable table = Connect.GetTable(sql);
        for (int i = 0; i < table.Rows.Count; i++)
        {


            html += "<tr>";
            html += "<td>" + (i + 1) + "</td>";

            html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["MaDonHang"].ToString() + "</td>";
            html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["NguoiGui"].ToString() + "</td>";
            html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["TenKhachHang"].ToString() + "</td>";
            double PhiThuHo = (table.Rows[i]["ThanhToan"].ToString() == "" ? 0 : double.Parse(table.Rows[i]["ThanhToan"].ToString()));
            html += "       <td style='text-align:center;vertical-align: inherit;'>" + PhiThuHo.ToString("N0").Replace(",", ".") + "</td>";


            html += "</tr>";
        }

        Response.Write(html);
    }

    private void LoadPopupHangNhanXem()
    {
        string idNguoiDung = StaticData.ValidParameter(Request.QueryString["idNguoiDung"].Trim());
        string NgayLap = StaticData.ValidParameter(Request.QueryString["NgayLap"].Trim());

        string sql = @" select TenKhachHang ,TienTraHang from tb_TraNoKhachHang dh, tb_KhachHang kh
   where dh.idKhachHang = kh.idKhachHang  and idNguoiDung =  " + idNguoiDung + " and NgayTra  = '" + NgayLap + "'";
        string html = @"<table class='table table-bordered table-hover dataTable'>
                            <tr style='white-space: nowrap;'>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    STT
                                </th>
                          
                        
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Tên Khách Hàng
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Tiền Cước
                                </th>
                               
                                
                            </tr>";
        DataTable table = Connect.GetTable(sql);
        for (int i = 0; i < table.Rows.Count; i++)
        {


            html += "<tr>";
            html += "<td>" + (i + 1) + "</td>";

       //     html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["MaDonHang"].ToString() + "</td>";
         //   html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["NguoiGui"].ToString() + "</td>";
            html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["TenKhachHang"].ToString() + "</td>";
            double PhiThuHo = (table.Rows[i]["TienTraHang"].ToString() == "" ? 0 : double.Parse(table.Rows[i]["TienTraHang"].ToString()));
            html += "       <td style='text-align:center;vertical-align: inherit;'>" + PhiThuHo.ToString("N0").Replace(",", ".") + "</td>";


            html += "</tr>";
        }

        Response.Write(html);
    }

    private void LoadPopupThuNoXem()
    {
        string idNguoiDung = StaticData.ValidParameter(Request.QueryString["idNguoiDung"].Trim());
        string NgayLap = StaticData.ValidParameter(Request.QueryString["NgayLap"].Trim());

        string sql = @" select TenKhachHang ,SoTien from tb_TraNoKhachHang dh, tb_KhachHang kh
   where dh.idKhachHang = kh.idKhachHang  and idNguoiDung =  " + idNguoiDung + " and NgayTra = '" + NgayLap + "'"; string html = @"<table class='table table-bordered table-hover dataTable'>
                            <tr style='white-space: nowrap;'>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    STT
                                </th>
                         
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Tên Người Nhận
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Tiền Cước
                                </th>
                               
                                
                            </tr>";
        DataTable table = Connect.GetTable(sql);
        for (int i = 0; i < table.Rows.Count; i++)
        {


            html += "<tr>";
            html += "<td>" + (i + 1) + "</td>";

         //   html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["MaDonHang"].ToString() + "</td>";
          //  html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["NguoiGui"].ToString() + "</td>";
            html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["TenKhachHang"].ToString() + "</td>";
            double PhiThuHo = (table.Rows[i]["SoTien"].ToString() == "" ? 0 : double.Parse(table.Rows[i]["SoTien"].ToString()));
            html += "       <td style='text-align:center;vertical-align: inherit;'>" + PhiThuHo.ToString("N0").Replace(",", ".") + "</td>";


            html += "</tr>";
        }

        Response.Write(html);
    }
    private void LoadPopupCPNTraXem()
    {
        string idNguoiDung = StaticData.ValidParameter(Request.QueryString["idNguoiDung"].Trim());
        string NgayLap = StaticData.ValidParameter(Request.QueryString["NgayLap"].Trim());

        string sql = @" select TenKhachHang ,ChuyenPhatNhanh from tb_TraNoKhachHang dh, tb_KhachHang kh
   where dh.idKhachHang = kh.idKhachHang  and idNguoiDung =  " + idNguoiDung + " and NgayTra  = '" + NgayLap + "'";
        string html = @"<table class='table table-bordered table-hover dataTable'>
                            <tr style='white-space: nowrap;'>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    STT
                                </th>
                              
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Tên Người Nhận
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Tiền Cước
                                </th>
                               
                                
                            </tr>";
        DataTable table = Connect.GetTable(sql);
        for (int i = 0; i < table.Rows.Count; i++)
        {


            html += "<tr>";
            html += "<td>" + (i + 1) + "</td>";

         //   html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["MaDonHang"].ToString() + "</td>";
         //   html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["NguoiGui"].ToString() + "</td>";
            html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["TenKhachHang"].ToString() + "</td>";
            double PhiThuHo = (table.Rows[i]["ChuyenPhatNhanh"].ToString() == "" ? 0 : double.Parse(table.Rows[i]["ChuyenPhatNhanh"].ToString()));
            html += "       <td style='text-align:center;vertical-align: inherit;'>" + PhiThuHo.ToString("N0").Replace(",", ".") + "</td>";


            html += "</tr>";
        }

        Response.Write(html);
    }
    private void LoadPopupCPNNhanXem()
    {
        string idNguoiDung = StaticData.ValidParameter(Request.QueryString["idNguoiDung"].Trim());
        string NgayLap = StaticData.ValidParameter(Request.QueryString["NgayLap"].Trim());

        string sql = @" select MaDonHang,NguoiGui,TenKhachHang ,ChuyenPhatNhanh from tb_DonHang dh, tb_KhachHang kh
            where dh.idKhachHang = kh.idKhachHang and NguoiNhanTra = 0 and idNguoiDung = " + idNguoiDung + " and NgayLap = '" + NgayLap + "'";
        string html = @"<table class='table table-bordered table-hover dataTable'>
                            <tr style='white-space: nowrap;'>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    STT
                                </th>
                                 <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Mã Đơn Hàng
                                </th>
                                 <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Tên Người Gửi
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Tên Người Nhận
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Tiền Cước
                                </th>
                               
                                
                            </tr>";
        DataTable table = Connect.GetTable(sql);
        for (int i = 0; i < table.Rows.Count; i++)
        {


            html += "<tr>";
            html += "<td>" + (i + 1) + "</td>";

            html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["MaDonHang"].ToString() + "</td>";
            html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["NguoiGui"].ToString() + "</td>";
            html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["TenKhachHang"].ToString() + "</td>";
            double PhiThuHo = (table.Rows[i]["ChuyenPhatNhanh"].ToString() == "" ? 0 : double.Parse(table.Rows[i]["ChuyenPhatNhanh"].ToString()));
            html += "       <td style='text-align:center;vertical-align: inherit;'>" + PhiThuHo.ToString("N0").Replace(",", ".") + "</td>";


            html += "</tr>";
        }

        Response.Write(html);
    }
    private void LoadPopupCODNhanXem()
    {
        string idNguoiDung = StaticData.ValidParameter(Request.QueryString["idNguoiDung"].Trim());
        string NgayLap = StaticData.ValidParameter(Request.QueryString["NgayLap"].Trim());

        string sql = @" select TenKhachHang ,ThanhToanCOD from tb_TraNoKhachHang dh, tb_KhachHang kh
   where dh.idKhachHang = kh.idKhachHang and idNguoiDung = " + idNguoiDung + "   and NgayTra = '" + NgayLap + "'";
        string html = @"<table class='table table-bordered table-hover dataTable'>
                            <tr style='white-space: nowrap;'>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    STT
                                </th>
                         
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Tên Người Nhận
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Tiền Cước
                                </th>
                               
                                
                            </tr>";
        DataTable table = Connect.GetTable(sql);
        for (int i = 0; i < table.Rows.Count; i++)
        {


            html += "<tr>";
            html += "<td>" + (i + 1) + "</td>";

           // html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["MaDonHang"].ToString() + "</td>";
          //  html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["NguoiGui"].ToString() + "</td>";
            html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["TenKhachHang"].ToString() + "</td>";
            double PhiThuHo = (table.Rows[i]["ThanhToanCOD"].ToString() == "" ? 0 : double.Parse(table.Rows[i]["ThanhToanCOD"].ToString()));
            html += "       <td style='text-align:center;vertical-align: inherit;'>" + PhiThuHo.ToString("N0").Replace(",", ".") + "</td>";


            html += "</tr>";
        }

        Response.Write(html);
    }
    private void LoadPopupCODTraGuiXem()
    {
        string idNguoiDung = StaticData.ValidParameter(Request.QueryString["idNguoiDung"].Trim());
        string NgayLap = StaticData.ValidParameter(Request.QueryString["NgayLap"].Trim());

        string sql = @"select MaDonHang,NguoiGui,TenKhachHang ,cod.SoTien from tb_DonHang dh, tb_KhachHang kh,tb_TraNoCOD cod
   where dh.idKhachHang = kh.idKhachHang and dh.idDonHang = cod.idDonHang and cod.idNguoiDung = " + idNguoiDung + " and NgayTra = '" + NgayLap + "'";
        string html = @"<table class='table table-bordered table-hover dataTable'>
                            <tr style='white-space: nowrap;'>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    STT
                                </th>
                                 <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Mã Đơn Hàng
                                </th>
                                 <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Tên Người Gửi
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Tên Người Nhận
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Tiền Cước
                                </th>
                               
                                
                            </tr>";
        DataTable table = Connect.GetTable(sql);
        for (int i = 0; i < table.Rows.Count; i++)
        {


            html += "<tr>";
            html += "<td>" + (i + 1) + "</td>";

            html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["MaDonHang"].ToString() + "</td>";
            html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["NguoiGui"].ToString() + "</td>";
            html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["TenKhachHang"].ToString() + "</td>";
            double PhiThuHo = (table.Rows[i]["SoTien"].ToString() == "" ? 0 : double.Parse(table.Rows[i]["SoTien"].ToString()));
            html += "       <td style='text-align:center;vertical-align: inherit;'>" + PhiThuHo.ToString("N0").Replace(",", ".") + "</td>";


            html += "</tr>";
        }

        Response.Write(html);
    }
    private void LoadPopupChiKhacXem()
    {
        string idNguoiDung = StaticData.ValidParameter(Request.QueryString["idNguoiDung"].Trim());
        string NgayLap = StaticData.ValidParameter(Request.QueryString["NgayLap"].Trim());

        string sql = @"  select NoiDung ,SoTien from tb_ChiKhac dh
   where idNguoiDung = " + idNguoiDung + " and NgayChi = '" + NgayLap + "'";
        string html = @"<table class='table table-bordered table-hover dataTable'>
                            <tr style='white-space: nowrap;'>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    STT
                                </th>
                                 <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Nội Dung Chi
                                </th>
                        
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Tiền Chi
                                </th>
                               
                                
                            </tr>";
        DataTable table = Connect.GetTable(sql);
        for (int i = 0; i < table.Rows.Count; i++)
        {


            html += "<tr>";
            html += "<td>" + (i + 1) + "</td>";

          //  html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["MaDonHang"].ToString() + "</td>";
          //  html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["NguoiGui"].ToString() + "</td>";
            html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["NoiDung"].ToString() + "</td>";
            double PhiThuHo = (table.Rows[i]["SoTien"].ToString() == "" ? 0 : double.Parse(table.Rows[i]["SoTien"].ToString()));
            html += "       <td style='text-align:center;vertical-align: inherit;'>" + PhiThuHo.ToString("N0").Replace(",", ".") + "</td>";


            html += "</tr>";
        }

        Response.Write(html);
    }
    
    private void DangXuat()
    {
       /* Session["QuanLyCongNoAnhKiet_Login"] = null;*/
        HttpCookie cookie_AdminWebsiteLuyenThi_Login = new HttpCookie("QuanLyCongNoAnhKiet_Login", "");
        cookie_AdminWebsiteLuyenThi_Login.Expires = DateTime.Now;
        Response.Cookies.Add(cookie_AdminWebsiteLuyenThi_Login);
        Response.Write("True");
    }

    private void LoadPopupLichSuNoNhaCungCap()
    {
        string idNhaCungCap = StaticData.ValidParameter(Request.QueryString["idNhaCungCap"].Trim());

        string sql = @"select tn.*, HoTen  from tb_TraNoCOD  tn, tb_NguoiDung nd where  idDonHang = '" + idNhaCungCap + "' and tn.idNguoiDung = nd.idNguoiDung";
        string html = @"<table class='table table-bordered table-hover dataTable'>
                            <tr style='white-space: nowrap;'>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    STT
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                   Mã Đơn Hàng
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                   Mã Phiếu Trã
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Số Tiền
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Ngày Trả
                                </th>
                              
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Người Thu
                                </th>
                              
                                
                            </tr>";
        DataTable table = Connect.GetTable(sql);
        for (int i = 0; i < table.Rows.Count; i++)
        {


            html += "<tr>";
            html += "<td>" + (i + 1) + "</td>";

            html += "<td style='text-align:center;vertical-align: inherit;'>" + StaticData.getField("tb_DonHang", "MaDonHang", "idDonHang", table.Rows[i]["idDonHang"].ToString().Trim()) + "</td>";
            html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["MaPhieuTra"].ToString() + "</td>";
            html += "       <td style='text-align:center;vertical-align: inherit;'>" + (double.Parse(table.Rows[i]["SoTien"].ToString())).ToString("N0").Replace(",", ".") + "</td>";
            DateTime NgayThanhToan = DateTime.Parse(table.Rows[i]["NgayTra"].ToString());
            html += "       <td style='text-align:center;vertical-align: inherit;'>" + NgayThanhToan.ToString("dd/MM/yyyy") + "</td>";
            //   html += "       <td style='text-align:center;vertical-align: inherit;white-space: nowrap;' id='tdtest'> <a onclick='ontest()' style='cursor:pointer;'><span style='font-size:20px;'><i class='fa fa-check-circle'></i></span> Nhận</a> </td>";

            html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["HoTen"].ToString() + "</td>";

            html += "</tr>";
        }

        Response.Write(html);
    }

    private void SuaNhanhDonHang()
    {
        string idDonHang = Request.QueryString["idDonHang"].Trim();
        string isDaNhanTien = Request.QueryString["isDaNhanTien"].Trim();
        string MaTinhTrang = Request.QueryString["MaTinhTrang"].Trim();
        string GhiChu = Request.QueryString["GhiChu"].Trim();

        string sqlUpdateDH = "update tb_DonHang set GhiChu=N'" + GhiChu + "'";
        if (isDaNhanTien != "")
            sqlUpdateDH += ",isDaNhanTien='" + isDaNhanTien + "'";
        else
            sqlUpdateDH += ",isDaNhanTien=null";
        if (MaTinhTrang != "")
            sqlUpdateDH += ",MaTinhTrang='" + MaTinhTrang + "'";
        else
            sqlUpdateDH += ",MaTinhTrang=null";
        sqlUpdateDH += " where idDonHang='" + idDonHang + "'";
        bool ktUpdateDH = Connect.Exec(sqlUpdateDH);
        if (ktUpdateDH)
            Response.Write("True");
        else
            Response.Write("False");
    }

       private void PrinfHoaDon()
    {
           //in hóa đơn đơn h
        string idDonHang = StaticData.ValidParameter(Request.QueryString["idDonHang"].Trim());
        string sqlOrderById = @"select * from tb_DonHang xt, tb_ChiTietDonHang ct, tb_KhachHang kh where xt.idDonHang = ct.idDonHang
	    and xt.idDonHang ='" + idDonHang + "'and kh.idKhachHang = xt.idKhachHang";
        DataTable data = Connect.GetTable(sqlOrderById);
        string MaXuatTra = data.Rows[0]["MaDonHang"].ToString();
        if (data.Rows.Count > 0)
        {
            BarcodeSymbology s = BarcodeSymbology.Code39C;
            BarcodeDraw drawObject = BarcodeDrawFactory.GetSymbology(s);
            var metrics = drawObject.GetDefaultMetrics(60);
            metrics.Scale = 2;
            var barcodeImage = drawObject.Draw(idDonHang, metrics);

            string barCode = "";

            using (MemoryStream ms = new MemoryStream())
            {
                barcodeImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] imageBytes = ms.ToArray();

                barCode = Convert.ToBase64String(imageBytes);
            }

            double SoTien = 0;
            string html = @"
            <div style='width:100%'>
            <div style='font-family: 'Times New Roman', Times, serif; font-size: 13px; text-align: left; width: 800px; margin-top: -10px; margin-left: -20px;'>
        <div style='margin-top: 0; margin-left: 20px'>";

           // html += MyStaticData.GetTieuDeIn(barCode, MaXuatTra);
            html += @"<p style='font-size: 18px; text-align: left; font-weight: bold; margin: 0; padding: 10px 0 5px 0;'>
                NHÀ XE CÔ HAI
                <br> 319 Trần Phú - ĐT: 0839.242.264
            </p>";
        html += @"<p style='font-size: 16px; text-align: center;  margin: 0; padding: 10px 0 5px 0;'>
                BIÊN NHẬN HÀNG HÓA
               
            </p>
            <p style='text-align: center; font-style: italic; margin: 0; padding: 0 0 10px 0;'> ";

        //    html += "     Ngày " + DateTime.Parse(data.Rows[0]["NgayLap"].ToString()).ToString("dd") + " Tháng " + DateTime.Parse(data.Rows[0]["NgayLap"].ToString()).ToString("MM") + " Năm " + DateTime.Parse(data.Rows[0]["NgayLap"].ToString()).ToString("yyyy");
            html += @" </p>

            <table width='100%' border='0' style='text-align: left; margin: 0; padding: 0 0 10px 0; ' cellpadding='1'>
                <!--=======================THÔNG TIN NGƯỜI TRẢ===============================--> 
                

                <tr>
                 
                     <td colspan='3'  style='font-size: 12px; '  width='20%' align='left'> Người nhận:</td> 
                     <td  style='font-size: 12px;text-align: left;' width='30%' > " + data.Rows[0]["TenKhachHang"].ToString() + @"
                     <td colspan='3' style='font-size: 10px;' width='50%' align='left'> Lưu ý:</td>  
                </tr>";
               html += @" <tr>
                 
                     <td colspan='3'  style='font-size: 12px; '  width='20%' align='left'> Nơi nhận:</td>
                     <td  style='font-size: 12px;text-align: left;' width='30%' > " + data.Rows[0]["DiaChi"].ToString() + @"
                     <td colspan='3' style='font-size: 10px;' width='50%' align='left'>Nếu mất chỉ đền gấp 2 giá cước.</td>  
                </tr>";
               html += @" <tr>
                 
                     <td colspan='3'  style='font-size: 12px; '  width='30%' align='left'> SDT:</td>
                     <td  style='font-size: 12px;text-align: left;' width='40%' > " + data.Rows[0]["SoDienThoai"].ToString() + @"
                     <td colspan='3' style='font-size: 10px;' width='40%' align='left'>Không chịu trách nhiệm hàng bên trong.</td>  
                </tr>";
               html += @" <tr>
                 
                     <td colspan='3'  style='font-size: 12px; '  width='30%' align='left'> COD:</td>
                     <td  style='font-size: 12px;text-align: left;' width='40%' > " + data.Rows[0]["PhiCOD"].ToString() + @"
                     <td colspan='3' style='font-size: 10px;' width='40%' align='left'>Không vận tải hàng cấm.</td>  
                </tr>";
               html += @" <tr>
                 
                     <td colspan='3'  style='font-size: 12px; '  width='30%' align='left'> Mã tra cứu:</td>
                     <td  style='font-size: 12px;text-align: left;' width='40%' > " + data.Rows[0]["MaDonHang"].ToString() + @"
                     <td colspan='3' style='font-size: 10px;' width='40%' align='left'>Biên nhận này có giá trị 7 ngày từ ngày tạo.</td>  
                </tr>";
               html += @" <tr>
                 
                     <td colspan='3'  style='font-size: 12px; '  width='30%' align='left'> Ngày tạo:</td>
                     <td  style='font-size: 12px;text-align: left;' width='40%' > " + DateTime.Parse(data.Rows[0]["NgayLap"].ToString()).ToString("dd//MM/yyyy") + @"
                    
                </tr>";
               html += @" <tr>
                 
                     <td colspan='3'  style='font-size: 12px; '  width='30%' align='left'> Tổng cước:</td>
                     <td  style='font-size: 12px;text-align: left;' width='40%' > " + double.Parse(data.Rows[0]["TongCuoc"].ToString()).ToString("#,##").Replace(",", ".") + @"
                    
                </tr>";
               html += @" <tr>
                 
                     <td colspan='3'  style='font-size: 12px; '  width='30%' align='left'> Cước đã trả:</td>
                     <td  style='font-size: 12px;text-align: left;' width='40%' > "  + @"
                    
                </tr>";
               html += @" <tr>
                 
                     <td colspan='3'  style='font-size: 12px; '  width='30%' align='left'> Cước còn nợ:</td>
                     <td  style='font-size: 12px;text-align: left;' width='40%' > " + @"
                    
                </tr>";
                //</tr>
                // <tr>
            
            html += @"
            </table>
            <!--=======================DANH SÁCH CHI TIÊT  XUẤT TRẢ===============================-->
            <table width='100%' border='1' cellpadding='3' cellspacing='0' style='margin-top: 10px; font-size: 14px'>
                <tr>
                    <td style='font-size: 12px; background: #ccc; text-align: center; font-weight: bold; width: 40px;'>STT
                    </td>
               
              
                    <td style='font-size: 12px; background: #ccc; text-align: center; font-weight: bold; width: 90px;'>Tên Hàng Hóa
                    </td>
                        <td style='font-size: 12px; background: #ccc; text-align: center; font-weight: bold; width: 90px;'>Số Lượng
                    </td>
                     <td style='font-size: 12px; background: #ccc; text-align: center; font-weight: bold; width: 90px;'>Đơn Giá
                       <td style='font-size: 12px; background: #ccc; text-align: center; font-weight: bold; width: 90px;'>Số Tiền
                    </td>

                 
                </tr> ";

            for (int i = 0; i < data.Rows.Count; i++)
            {
                html += "<tr>";
                html += "<td style='text-align: center;'>" + (i + 1).ToString() + " </td> ";
                string IDHangHoa = "";
                string idChiTietDonHang = data.Rows[i]["idChiTietDonHang"].ToString();
                //string sqlid = "select * from tb_ChiTietDonHang where idChiTietDonHang= '" + idChiTietDonHang + "'";
                //DataTable tbid = Connect.GetTable(sqlid);
                //if (tbid.Rows.Count > 0)
                //{
                IDHangHoa = data.Rows[i]["IDHangHoa"].ToString();
                //}
              
                double DonGia = 0;
               double ThanhTien = 0;
               double SL = 0;
                DonGia = double.Parse(data.Rows[i]["DonGia"].ToString());
                //ThanhTien = double.Parse(data.Rows[i]["ThanhTien"].ToString());
                html += "<td style='text-align: center;'>" + StaticData.getField("tb_HangHoa", "TenHangHoa", "IDHangHoa", IDHangHoa) + " </td> ";
             //   string DVT = StaticData.getField("tb_HangHoa", "IDDonViTinh", "IDHangHoa", IDHangHoa);
           //     html += "<td style='text-align: center;'>" + StaticData.getField("tb_DonViTinh", "DonViTinh", "IDDonViTinh", DVT) + " </td> ";
                SL = double.Parse(data.Rows[i]["SoLuong"].ToString());
                ThanhTien = SL * DonGia;
                html += "<td style='text-align: center;'>" + SL + " </td> ";
                //html += "<td style='text-align: center;'>" + data.Rows[i]["DonGia"].ToString() + " </td> ";
                html += "<td style='text-align: center;'>" + DonGia.ToString("#,##").Replace(",", ".") + " </td> ";
                html += "<td style='text-align: center;'>" + ThanhTien.ToString("#,##").Replace(",", ".") + " </td> ";
                html += "</tr>";
                //SoTien += ThanhTien;
               
            }

       //     html += @"";
       //     //html += "   <tr>";
       //     html += "<tr style='background: #3dec3a; font-weight:bold'>";
       //     html += "   <td colspan='2' style='vertical-align: inherit;font-size:12px; white-space: nowrap;'>";
       //     html += "       <table width='100%'><tr><td colspan='2' style='text-align:right;font-size:14px'>Ngày " + DateTime.Parse(data.Rows[0]["NgayLap"].ToString()).ToString("dd") + " Tháng " + DateTime.Parse(data.Rows[0]["NgayLap"].ToString()).ToString("MM") + " Năm " + DateTime.Parse(data.Rows[0]["NgayLap"].ToString()).ToString("yyyy") + "</td></tr>";
       //     html += "       <tr><td style='text-align:center;font-size:12px'>KHÁCH HÀNG</td><td style='text-align:center;font-size:12px'>LẬP PHIẾU</td></tr>";
       //     html += "       <tr><td style='text-align:center;font-size:10px'>(Ký/Họ Tên)</td><td style='text-align:center;font-size:10px'>(Ký/Họ Tên)</td></tr>";
       //     html += "       <tr><td style='text-align:center'>  </td><td style='text-align:center'>  </td></tr>";
       //     html += "       <tr><td style='text-align:center'>  </td><td style='text-align:center'>  </td></tr>";
       //     html += "       <tr><td style='text-align:center'>  </td><td style='text-align:center'>  </td></tr>";
       //     html += "       <tr><td style='text-align:center'>  </td><td style='text-align:center'>  </td></tr>";
       //     html += "       <tr><td style='text-align:center'>  </td><td style='text-align:center'>  </td></tr>";
       //     html += "       <tr><td style='text-align:center'>  </td><td style='text-align:center'>  </td></tr></table>";
       //     html += "   </td>";
       //     html += "   <td colspan='1' style='vertical-align: inherit;font-size:14px;'>";
       //     html += "       <span style='text-align:left'>Tổng Cộng :</span><span style='padding-left:65px'>" + (String.Format("{0:0,0}", data.Rows[0]["TongCuoc"])).Replace(',', '.') + " đ </span>";
       //   //  html += "     <br><span style='text-align:left'>Nợ Cũ :</span><span style='padding-left:92.5px'>" + SoTien.ToString("#,##").Replace(",", ".") + " đ </span>";
       //  //   html += "      <br><span style='text-align:left'>Thanh Toán :</span><span style='padding-left:58px'>" + (String.Format("{0:0,0}", data.Rows[0]["ThanhToan"])).Replace(',', '.') + " đ </span>";
       ////     html += "     <br><span style='text-align:left'> :</span><span style='padding-left:83px'>" + SoTien.ToString("#,##").Replace(",", ".") + " đ </span>";

       //     html += "   </td>";
       //     html += "</tr>";
            // //html += "  <table width='100%' border='0' cellpadding='2' style='margin-top: 0;'>";
         
            html += @"";
            // html += " <tr> ";
            //   html += "         <td colspan='6' style='text-align: left; height: 25px;'>";
            // html += "               <b>Bằng chữ : </b>";
            // html += "               <i> ";
            // html += StaticData.ConvertDecimalToString(decimal.Parse(data.Rows[0]["TongCuoc"].ToString()));
            //                html += @" </i>";
            //html += "            </td>";

            //html += "        </tr>";
            html += @"</table>



          
       
                        
                 <div style='text-align: center; margin-top: 5px;font-size: 12px'>
                    <div>
                        
                    </div>
                    </br>
                    <div style='text-align: center; font-size: 10px'>
                        Nhà xe Cô Hai hân hạnh phục vụ quý khách!
                    </div>
                </div>
        </div>
    </div>
</div>
<input id='txtidHoaDon' hidden='hidden' value='" + idDonHang + "'/>";
            Response.Write(html);
        }
    }

       private void PrinfHoaDonTem()
       {
           string idHangHoa = StaticData.ValidParameter(Request.QueryString["idHangHoa"].Trim());
           string idDonHang = StaticData.ValidParameter(Request.QueryString["idDonHang"].Trim());
           string sqlOrderById = @"select * from tb_DonHang dh, tb_ChiTietDonHang ct, tb_KhachHang kh where dh.idDonHang = ct.idDonHang and idHangHoa = '" + idHangHoa + "' and dh.idDonHang ='" + idDonHang + "' and kh.idKhachHang = dh.idKhachHang";
           DataTable data = Connect.GetTable(sqlOrderById);
           string MaXuatTra = data.Rows[0]["MaHangHoa"].ToString();
           if (data.Rows.Count > 0)
           {
               BarcodeSymbology s = BarcodeSymbology.Code39C;
               BarcodeDraw drawObject = BarcodeDrawFactory.GetSymbology(s);
               var metrics = drawObject.GetDefaultMetrics(60);
               metrics.Scale = 2;
               var barcodeImage = drawObject.Draw(idDonHang, metrics);

               string barCode = "";

               using (MemoryStream ms = new MemoryStream())
               {
                   barcodeImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                   byte[] imageBytes = ms.ToArray();

                   barCode = Convert.ToBase64String(imageBytes);
               }

               double SoTien = 0;
               string html = @"
            <div style='width:100%'>
            <div style='font-family: 'Times New Roman', Times, serif; font-size: 15px; text-align: left; width: 800px; margin-top: -10px; margin-left: -20px;'>
        <div style='margin-top: 0; margin-left: 20px'>";

            //   html += MyStaticData.GetTieuDeIn(barCode, MaXuatTra);
               html += @"<p style='font-size: 20px; text-align: left; font-weight: bold; margin: 0; padding: 10px 0 5px 0;'>
                NHÀ XE CÔ HAI
                <br> 319 Trần Phú - ĐT: 0839242264
            </p>";
  html += @"<p style='font-size: 20px; text-align: center; font-weight: bold; margin: 0; padding: 10px 0 5px 0;'>
                Phiếu Thông Tin
             
            </p>
            <p style='text-align: center; font-style: italic; margin: 0; padding: 0 0 10px 0;'> ";

               html += "     Ngày " + DateTime.Parse(data.Rows[0]["NgayLap"].ToString()).ToString("dd") + " Tháng " + DateTime.Parse(data.Rows[0]["NgayLap"].ToString()).ToString("MM") + " Năm " + DateTime.Parse(data.Rows[0]["NgayLap"].ToString()).ToString("yyyy");
               html += @" </p>

            <table width='100%' border='0' cellpadding='1'>
                <!--=======================THÔNG TIN NGƯỜI TRẢ===============================--> 
                
                <tr>
                    <td width='10%' align='left'> Người Nhận :</td>
                    <td width='60%' align='left'> ";
               html +=  data.Rows[0]["TenKhachHang"].ToString();
               html += @"</tr>";
               //</tr>
               html += "<tr><td width='20%' align='left'> Nơi Nhận:</td>";

               html += "<td colspan='3' align='left'> " +  data.Rows[0]["DiaChi"].ToString() + "</td></tr>";
               // <tr>
               html += "<tr><td width='20%' align='left'> Điện  thoại: </td>";
               html += "<td colspan='3' align='left'> " +  data.Rows[0]["SoDienThoai"].ToString() + "</td></tr>";
               html += "<tr><td width='20%' align='left'> Ghi Chú: </td>";

               html += "<tr><td width='20%' align='left'> Biểu Phí: </td>";
               html += "<td colspan='3' align='left'> " + StaticData.getField("tb_HangHoa", "TenHangHoa", "IDHangHoa", idHangHoa) + "</td></tr>";
               html += "<tr><td width='20%' align='left'> Cước Gói Hàng: </td>";
               html += "<td colspan='3' align='left'> " + data.Rows[0]["DonGia"].ToString() + "</td></tr>";

               html += "<tr><td width='20%' align='left'> STT: </td>";
               html += "<td colspan='3' align='left'> " + data.Rows[0]["SoDienThoai"].ToString() + "</td></tr>";

               html += "<tr><td width='20%' align='center'> Mã Gói Hàng: </td></tr>";
               //html += "<td colspan='3' align='left'> " + barCode + "</td></tr>";
               html += @"                            <td style='text-align: center'>
                                       
                                        
                                        <img id='Imgcode' src='data:image/png;base64," + barCode + "' style='height:20px;width:180px;' /> </br> ";
        html += @"                  </td>
              
            </table>
            <!--=======================DANH SÁCH CHI TIÊT  XUẤT TRẢ===============================-->
           

          
       
                        
                
        </div>
    </div>
</div>
<input id='txtidHoaDon' hidden='hidden' value='" + idDonHang + "'/>";
               Response.Write(html);
           }
       }

    private void CheckDaNhanTien()
    {
        string idDonHang = Request.QueryString["idDonHang"].Trim();
        string Checked = Request.QueryString["Checked"].Trim();
        string sqlUpdateDH = "update tb_DonHang set isDaNhanTien='" + Checked + "'";
        sqlUpdateDH += " where idDonHang='" + idDonHang + "'";
        bool ktUpdateDH = Connect.Exec(sqlUpdateDH);
        if (ktUpdateDH)
            Response.Write("True");
        else
            Response.Write("False");
    }
    private void GetMaDonHang()
    {
        string idKhachHang = Request.QueryString["idKhachHang"].Trim();
        string MaDonHang = idKhachHang + MyStaticData.TaoMaDonHang();
        Response.Write(MaDonHang);
    }
    private void DeleteHuyen()
    {
        string idHuyen = StaticData.ValidParameter(Request.QueryString["idHuyen"].Trim());
        string sql = "delete from tb_Huyen where idHuyen='" + idHuyen + "'";
        bool ktDelete = Connect.Exec(sql);
        if (ktDelete)
            Response.Write("True");
        else
            Response.Write("False");
    }
    private void DeleteTinh()
    {
        string idTinh = StaticData.ValidParameter(Request.QueryString["idTinh"].Trim());
        string sql = "delete from tb_Tinh where idTinh='" + idTinh + "'";
        bool ktDelete = Connect.Exec(sql);
        if (ktDelete)
            Response.Write("True");
        else
            Response.Write("False");
    }
    private void DeleteChiTieu()
    {
        string idChiTieu = StaticData.ValidParameter(Request.QueryString["idChiTieu"].Trim());
        string sql = "delete from tb_ChiKhac where idChiKhac='" + idChiTieu + "'";
        bool ktDelete = Connect.Exec(sql);
        if (ktDelete)
            Response.Write("True");
        else
            Response.Write("False");
    }
    private void TinhTienCuoc()
    {
        string idKhachHang = Request.QueryString["idKhachHang"].Trim();
        string MaLoaiCuoc = Request.QueryString["MaLoaiCuoc"].Trim();
        string SoLuong = Request.QueryString["SoLuong"].Trim();
        double TienCuoc = 0;
        string sqlKhachHang = "select * from tb_KhachHang where idKhachHang='" + idKhachHang + "'";
        DataTable tbKhachHang = Connect.GetTable(sqlKhachHang);
        if (tbKhachHang.Rows.Count>0)
        {
            double GiaCuoc = 0;
            if(MaLoaiCuoc.ToUpper()=="NOITHANH")
            {
                if(tbKhachHang.Rows[0]["GiaCuocNoiThanh"].ToString().Trim() != "")
                {
                    GiaCuoc = double.Parse(tbKhachHang.Rows[0]["GiaCuocNoiThanh"].ToString().Trim());
                }
            }
            if (MaLoaiCuoc.ToUpper() == "NGOAITHANH")
            {
                if (tbKhachHang.Rows[0]["GiaCuocNgoaiThanh"].ToString().Trim() != "")
                {
                    GiaCuoc = double.Parse(tbKhachHang.Rows[0]["GiaCuocNgoaiThanh"].ToString().Trim());
                }
            }
            if (MaLoaiCuoc.ToUpper() == "DITINH")
            {
                if (tbKhachHang.Rows[0]["GiaCuocDiTinh"].ToString().Trim() != "")
                {
                    GiaCuoc = double.Parse(tbKhachHang.Rows[0]["GiaCuocDiTinh"].ToString().Trim());
                }
            }
            if (MaLoaiCuoc.ToUpper() == "HUYEN")
            {
                if (tbKhachHang.Rows[0]["GiaCuocHuyen"].ToString().Trim() != "")
                {
                    GiaCuoc = double.Parse(tbKhachHang.Rows[0]["GiaCuocHuyen"].ToString().Trim());
                }
            }
            try
            {
                TienCuoc = double.Parse(SoLuong) * GiaCuoc;
            }
            catch { }
        }
        Response.Write(TienCuoc.ToString("#,##").Replace(",", "."));
    }
    private void LoadGiaCuoc()
    {
        string idKhachHang = Request.QueryString["idKhachHang"].Trim();
        string GiaCuoc = StaticData.getField("tb_KhachHang", "GiaCuoc", "idKhachHang", idKhachHang);
        if (GiaCuoc != "")
            Response.Write(double.Parse(GiaCuoc).ToString("#,##").Replace(",", "."));
        else
            Response.Write("");
    }
    private void PrinfPhanHangLenXe()
    {
        // in hóa đơn phân hàng lên xe
        string idHD = StaticData.ValidParameter(Request.QueryString["idHD"].Trim());

        string sqlOrderById = @"select idChiTietDonHang,(SoLuong*DonGia) as ThanhTien, MaTuyen,ct.MaHangHoa,MaDonHang,TenKhachHang,dh.PhiCOD,TenChiNhanh,kh.DiaChi,kh.SoDienThoai,SoLuong , TenHangHoa, NguoiNhanTra
                            , 'TenChiNhanhGui' = (select TenChiNhanh from tb_ChiNhanh where idChiNhanh = dh.idChiNhanhGui), BienSoXe,HoTen,
'TenNguoiLap' = (select Hoten from tb_NguoiDung where idNguoiDung = ph.idNguoiDung)
                            from tb_ChiTietDonHang ct, tb_HangHoa hh, tb_DonHang dh, tb_PhanHangLenXe ph , tb_KhachHang kh,tb_ChiNhanh cn,tb_Xe x, tb_NguoiDung nd
                             where  ct.idPhanHangLenXe = " + idHD +@"  and ct.idHangHoa = hh.idHangHoa and dh.idDonHang = ct.idDonHang and ph.idPhanHangLenXe = ct.idPhanHangLenXe
                             and dh.idKhachHang = kh.idKhachHang and dh.idChiNhanhNhan = cn.idChiNhanh and ph.idXe = x.idXe and x.idNguoiDung = nd.idNguoiDung
                            ";
        DataTable data = Connect.GetTable(sqlOrderById);
        if (data.Rows.Count > 0)
        {
            BarcodeSymbology s = BarcodeSymbology.Code39C;
            BarcodeDraw drawObject = BarcodeDrawFactory.GetSymbology(s);
            var metrics = drawObject.GetDefaultMetrics(60);
            metrics.Scale = 2;
            var barcodeImage = drawObject.Draw(idHD, metrics);

            string barCode = "";

            using (MemoryStream ms = new MemoryStream())
            {
                barcodeImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] imageBytes = ms.ToArray();

                barCode = Convert.ToBase64String(imageBytes);
            }

            double SoTien = 0;
            string html = @"
            <div style='width:100%'>
            <div style='font-family: 'Times New Roman', Times, serif; font-size: 13px; text-align: left; width: 800px; margin-top: -10px; margin-left: -20px;'>
        <div style='margin-top: 0; margin-left: 20px'>";

            // html += MyStaticData.GetTieuDeIn(barCode, MaXuatTra);
            html += @"<p style='font-size: 18px; text-align: left; font-weight: bold; margin: 0; padding: 10px 0 5px 0;'>
                NHÀ XE CÔ HAI
                <br> 319 Trần Phú - ĐT: 0839.242.264
            </p>";
            html += @"<p colspan='2' style='font-size: 24px; text-align: center;  margin: 0; padding: 10px 0 5px 0;'>
                BIÊN NHẬN PHÂN HÀNG LÊN XE
                
            </p>

            <p style='text-align: right; font-size: 14px; margin: 0; padding: 0 0 10px 0;' > Số: " + data.Rows[0]["MaTuyen"].ToString() + @"";

            //    html += "     Ngày " + DateTime.Parse(data.Rows[0]["NgayLap"].ToString()).ToString("dd") + " Tháng " + DateTime.Parse(data.Rows[0]["NgayLap"].ToString()).ToString("MM") + " Năm " + DateTime.Parse(data.Rows[0]["NgayLap"].ToString()).ToString("yyyy");
            html += @" </p>

            <table width='100%' border='0' style='text-align: left; margin: 0; padding: 0 0 10px 0; ' cellpadding='1'>
                <!--=======================THÔNG TIN NGƯỜI TRẢ===============================--> 
                

                <tr>
                 
                     <td colspan='2'  style='font-size: 18px; '  width='20%' align='left'> Bên Giao:</td> 
                     <td  style='font-size: 18px;text-align: left;' width='30%' > " + data.Rows[0]["TenChiNhanhGui"].ToString() + @"
                     
                </tr>";
            html += @" <tr>
                 
                     <td colspan='2'  style='font-size: 18px; '  width='20%' align='left'> Bên nhận:</td>
                     <td  style='font-size: 18px;text-align: left;' width='30%'> Xe:  " + data.Rows[0]["BienSoXe"].ToString() + " - Tài Xế: "+ data.Rows[0]["HoTen"].ToString() + @"
                   
                </tr>";
            string Ngay = DateTime.Now.ToString("HH:mm:ss");
            html += @" <tr>
                 
                     <td colspan='2'  style='font-size: 18px; '  width='30%' align='left'> Thời Điểm:</td>
                     <td  style='font-size: 18px;text-align: left;' width='40%' > " + Ngay + " Ngày " + DateTime.Now.ToString("dd/MM/yyyy") + @"
                   
                </tr>";
            html += @" <tr>
                 
                     <td colspan='2'  style='font-size: 18px; '  width='30%' align='left'> Người Lập Phiếu:</td>
                     <td  style='font-size: 18px;text-align: left;' width='40%' > " + data.Rows[0]["TenNguoiLap"].ToString() + @"
                
                </tr>";
            
            
//            html += @" <tr>
//                 
//                     <td colspan='3'  style='font-size: 12px; '  width='30%' align='left'> Ngày tạo:</td>
//                     <td  style='font-size: 12px;text-align: left;' width='40%' > " + DateTime.Parse(data.Rows[0]["NgayLap"].ToString()).ToString("dd//MM/yyyy") + @"
//                    
//                </tr>";
           
           
           
            //</tr>
            // <tr>

            html += @"
            </table>
            <!--=======================DANH SÁCH CHI TIÊT  XUẤT TRẢ===============================-->
            <table width='100%' border='1' cellpadding='3' cellspacing='0' style='margin-top: 10px; font-size: 14px'>
                <tr>
                    <td style='font-size: 12px; background: #ccc; text-align: center; font-weight: bold; width: 40px;'>STT
                    </td>
               
              
                    <td style='font-size: 12px; background: #ccc; text-align: center; font-weight: bold; width: 90px;'>Mã Hàng Hóa
                    </td>
                        <td style='font-size: 12px; background: #ccc; text-align: center; font-weight: bold; width: 90px;'>người Nhận
                    </td>
                     <td style='font-size: 12px; background: #ccc; text-align: center; font-weight: bold; width: 90px;'>Chi Nhánh Nhận
                       <td style='font-size: 12px; background: #ccc; text-align: center; font-weight: bold; width: 90px;'>Địa Chỉ 
                    </td>
                    <td style='font-size: 12px; background: #ccc; text-align: center; font-weight: bold; width: 90px;'>SĐT
                    </td>
                    <td style='font-size: 12px; background: #ccc; text-align: center; font-weight: bold; width: 90px;'>Số Lượng
                    </td>
                    <td style='font-size: 12px; background: #ccc; text-align: center; font-weight: bold; width: 90px;'>Tên Hàng Hóa 
                    </td>
                    <td style='font-size: 12px; background: #ccc; text-align: center; font-weight: bold; width: 90px;'>Tình Trạng
                    </td>
  <td style='font-size: 12px; background: #ccc; text-align: center; font-weight: bold; width: 90px;'>COD
                    </td>
                    <td style='font-size: 12px; background: #ccc; text-align: center; font-weight: bold; width: 90px;'>Tổng Cước
                    </td>
                    <td style='font-size: 12px; background: #ccc; text-align: center; font-weight: bold; width: 90px;'>Ký Nhận 
                    </td>
                    <td style='font-size: 12px; background: #ccc; text-align: center; font-weight: bold; width: 90px;'>Ghi Chú
                    </td>

                 
                </tr> ";

            for (int i = 0; i < data.Rows.Count; i++)
            {
                html += "<tr>";
                html += "<td style='text-align: center;'>" + (i + 1).ToString() + " </td> ";

                html += "<td style='text-align: center;'>" + data.Rows[i]["MaHangHoa"].ToString() + "<br> (" + data.Rows[i]["MaDonHang"].ToString() + ") </td> ";
                html += "<td style='text-align: center;'>" + data.Rows[i]["TenKhachHang"].ToString() + " </td> ";
                html += "<td style='text-align: center;'>" + data.Rows[i]["TenChiNhanh"].ToString() + " </td> ";
                html += "<td style='text-align: center;'>" + data.Rows[i]["DiaChi"].ToString() + " </td> ";
                html += "<td style='text-align: center;'>" + data.Rows[i]["SoDienThoai"].ToString() + " </td> ";
                html += "<td style='text-align: center;'>" + data.Rows[i]["SoLuong"].ToString() + " </td> ";
                html += "<td style='text-align: center;'>" + data.Rows[i]["TenHangHoa"].ToString() + " </td> ";
                string NguoiNhan = data.Rows[i]["NguoiNhanTra"].ToString();
                if(NguoiNhan == "0")
                {
                    html += "<td style='text-align: center;'> Người Gửi Trả </td> ";
               
                }
                else
                {
                    html += "<td style='text-align: center;'> Người Nhận Trả </td> ";
               
                }
                double COD = (data.Rows[i]["PhiCOD"].ToString() == "" ? 0 : double.Parse(data.Rows[i]["PhiCOD"].ToString()));
                html += "       <td style='text-align:center;vertical-align: inherit;'>" + COD.ToString("N0").Replace(",", ".") + "</td>";
  
                double CuocHangGui = (data.Rows[i]["ThanhTien"].ToString() == "" ? 0 : double.Parse(data.Rows[i]["ThanhTien"].ToString()));
                html += "       <td style='text-align:center;vertical-align: inherit;'>" + CuocHangGui.ToString("N0").Replace(",", ".") + "</td>";

                //html += "<td style='text-align: center;'>" + data.Rows[i]["DonGia"].ToString() + " </td> ";
                html += "<td style='text-align: center;'>"  + " </td> ";
                html += "<td style='text-align: center;'>"  + " </td> ";
                html += "</tr>";
                //SoTien += ThanhTien;

            }

       
            html += @"";
      
            html += @"</table>



          
       
                        
                 <div style='text-align: center; margin-top: 5px;font-size: 12px'>
                    <div>
                        
                    </div>
                    </br>
                    <div style='text-align: center; font-size: 10px'>
                        
                    </div>
                </div>
        </div>
    </div>
</div>
<input id='txtidHoaDon' hidden='hidden' value='" + idHD + "'/>";
            Response.Write(html);
        }
    }


    private void PrinfBienNhanXeCho()
    {
        // in hóa đơn phân hàng lên xe
        string idHD = StaticData.ValidParameter(Request.QueryString["idHD"].Trim());

        string sqlOrderById = @"  select idChiTietDonHang, ct.MaHangHoa,TenKhachHang,dh.PhiCOD,TenChiNhanh,MaDonHang,kh.DiaChi,kh.SoDienThoai,SoLuong,(SoLuong*DonGia) as ThanhTien , TenHangHoa, NguoiNhanTra
                            , 'TenChiNhanhGui' = (select TenChiNhanh from tb_ChiNhanh where idChiNhanh = dh.idChiNhanhGui), BienSoXe,HoTen,
'TenNguoiLap' = (select Hoten from tb_NguoiDung where idNguoiDung = ph.idNguoiDung)
 from tb_PhanHangLenXe ph, tb_ChiTietDonHang ct , tb_HangHoa hh, tb_DonHang dh, tb_KhachHang kh,tb_ChiNhanh cn,tb_Xe x, tb_NguoiDung nd
 where ph.idPhanHangLenXe = ct.idPhanHangLenXe and MaTinhTrang = 'DVC' and PH.idXe = " + idHD + @"
 and ct.idHangHoa = hh.idHangHoa and dh.idDonHang = ct.idDonHang 
 and dh.idKhachHang = kh.idKhachHang and dh.idChiNhanhNhan = cn.idChiNhanh and ph.idXe = x.idXe and x.idNguoiDung = nd.idNguoiDung
                            ";
        DataTable data = Connect.GetTable(sqlOrderById);
        if (data.Rows.Count > 0)
        {
            BarcodeSymbology s = BarcodeSymbology.Code39C;
            BarcodeDraw drawObject = BarcodeDrawFactory.GetSymbology(s);
            var metrics = drawObject.GetDefaultMetrics(60);
            metrics.Scale = 2;
            var barcodeImage = drawObject.Draw(idHD, metrics);

            string barCode = "";

            using (MemoryStream ms = new MemoryStream())
            {
                barcodeImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] imageBytes = ms.ToArray();

                barCode = Convert.ToBase64String(imageBytes);
            }

            double SoTien = 0;
            string html = @"
            <div style='width:100%'>
            <div style='font-family: 'Times New Roman', Times, serif; font-size: 13px; text-align: left; width: 800px; margin-top: -10px; margin-left: -20px;'>
        <div style='margin-top: 0; margin-left: 20px'>";

            // html += MyStaticData.GetTieuDeIn(barCode, MaXuatTra);
            html += @"<p style='font-size: 18px; text-align: left; font-weight: bold; margin: 0; padding: 10px 0 5px 0;'>
                NHÀ XE CÔ HAI
                <br> 319 Trần Phú - ĐT: 0839.242.264
            </p>";
            html += @"<p style='font-size: 24px; text-align: center;  margin: 0; padding: 10px 0 5px 0;'>
                BIÊN NHẬN XE CHỞ
               
            </p>
            <p style='text-align: center; font-style: italic; margin: 0; padding: 0 0 10px 0;'> ";

            //    html += "     Ngày " + DateTime.Parse(data.Rows[0]["NgayLap"].ToString()).ToString("dd") + " Tháng " + DateTime.Parse(data.Rows[0]["NgayLap"].ToString()).ToString("MM") + " Năm " + DateTime.Parse(data.Rows[0]["NgayLap"].ToString()).ToString("yyyy");
            html += @" </p>

            <table width='100%' border='0' style='text-align: left; margin: 0; padding: 0 0 10px 0; ' cellpadding='1'>
                <!--=======================THÔNG TIN NGƯỜI TRẢ===============================--> 
                

                <tr>
                 
                     <td colspan='2'  style='font-size: 18px; '  width='20%' align='left'> Xe Chở:</td> 
  <td  style='font-size: 18px;text-align: left;' width='30%'> Xe:  " + data.Rows[0]["BienSoXe"].ToString() + " - Tài Xế: " + data.Rows[0]["HoTen"].ToString() + @"
                   
                  
                </tr>";
//            html += @" <tr>
//                 
//                     <td colspan='2'  style='font-size: 18px; '  width='20%' align='left'> Bên Nhận:</td>
//                      <td  style='font-size: 18px;text-align: left;' width='30%' > " + data.Rows[0]["TenChiNhanh    "].ToString() + @"
//                     
//                </tr>";
            string Ngay = DateTime.Now.ToString("HH:mm:ss");
            html += @" <tr>
                 
                     <td colspan='2'  style='font-size: 18px; '  width='30%' align='left'> Thời Điểm:</td>
                     <td  style='font-size: 18px;text-align: left;' width='40%' > " + Ngay + " Ngày " + DateTime.Now.ToString("dd/MM/yyyy") + @"
                   
                </tr>";
            html += @" <tr>
                 
                     <td colspan='2'  style='font-size: 18px; '  width='30%' align='left'> Người Lập Phiếu:</td>
                     <td  style='font-size: 18px;text-align: left;' width='40%' > " + data.Rows[0]["TenNguoiLap"].ToString() + @"
                
                </tr>";


            //            html += @" <tr>
            //                 
            //                     <td colspan='3'  style='font-size: 12px; '  width='30%' align='left'> Ngày tạo:</td>
            //                     <td  style='font-size: 12px;text-align: left;' width='40%' > " + DateTime.Parse(data.Rows[0]["NgayLap"].ToString()).ToString("dd//MM/yyyy") + @"
            //                    
            //                </tr>";



            //</tr>
            // <tr>

            html += @"
            </table>
            <!--=======================DANH SÁCH CHI TIÊT  XUẤT TRẢ===============================-->
            <table width='100%' border='1' cellpadding='3' cellspacing='0' style='margin-top: 10px; font-size: 14px'>
                <tr>
                    <td style='font-size: 12px; background: #ccc; text-align: center; font-weight: bold; width: 40px;'>STT
                    </td>
               
              
                    <td style='font-size: 12px; background: #ccc; text-align: center; font-weight: bold; width: 90px;'>Mã Hàng Hóa
                    </td>
                        <td style='font-size: 12px; background: #ccc; text-align: center; font-weight: bold; width: 90px;'>Người Nhận
                    </td>
                     <td style='font-size: 12px; background: #ccc; text-align: center; font-weight: bold; width: 90px;'>Chi Nhánh Nhận
                       <td style='font-size: 12px; background: #ccc; text-align: center; font-weight: bold; width: 90px;'>Địa Chỉ 
                    </td>
                    <td style='font-size: 12px; background: #ccc; text-align: center; font-weight: bold; width: 90px;'>SĐT
                    </td>
                    <td style='font-size: 12px; background: #ccc; text-align: center; font-weight: bold; width: 90px;'>Số Lượng
                    </td>
                    <td style='font-size: 12px; background: #ccc; text-align: center; font-weight: bold; width: 90px;'>Tên Hàng Hóa 
                    </td>
                    <td style='font-size: 12px; background: #ccc; text-align: center; font-weight: bold; width: 90px;'>Tình Trạng
                    </td>
                    <td style='font-size: 12px; background: #ccc; text-align: center; font-weight: bold; width: 90px;'>COD
                    </td>
                    <td style='font-size: 12px; background: #ccc; text-align: center; font-weight: bold; width: 90px;'>Tổng Cước
                    </td>
                    <td style='font-size: 12px; background: #ccc; text-align: center; font-weight: bold; width: 90px;'>Ký Nhận 
                    </td>
                    <td style='font-size: 12px; background: #ccc; text-align: center; font-weight: bold; width: 90px;'>Ghi Chú
                    </td>

                 
                </tr> ";

            for (int i = 0; i < data.Rows.Count; i++)
            {
                html += "<tr>";
                html += "<td style='text-align: center;'>" + (i + 1).ToString() + " </td> ";
                html += "<td style='text-align: center;'>" + data.Rows[i]["MaHangHoa"].ToString() + "<br> (" + data.Rows[i]["MaDonHang"].ToString() + ") </td> ";
               // html += "<td style='text-align: center;'>" + data.Rows[i]["MaHangHoa"].ToString() + " </td> ";
                html += "<td style='text-align: center;'>" + data.Rows[i]["TenKhachHang"].ToString() + " </td> ";
                html += "<td style='text-align: center;'>" + data.Rows[i]["TenChiNhanh"].ToString() + " </td> ";
                html += "<td style='text-align: center;'>" + data.Rows[i]["DiaChi"].ToString() + " </td> ";
                html += "<td style='text-align: center;'>" + data.Rows[i]["SoDienThoai"].ToString() + " </td> ";
                html += "<td style='text-align: center;'>" + data.Rows[i]["SoLuong"].ToString() + " </td> ";
                html += "<td style='text-align: center;'>" + data.Rows[i]["TenHangHoa"].ToString() + " </td> ";
                string NguoiNhan = data.Rows[i]["NguoiNhanTra"].ToString();
                if (NguoiNhan == "0")
                {
                    html += "<td style='text-align: center;'> Người Gửi Trả </td> ";

                }
                else
                {
                    html += "<td style='text-align: center;'> Người Nhận Trả </td> ";

                }
                double COD = (data.Rows[i]["PhiCOD"].ToString() == "" ? 0 : double.Parse(data.Rows[i]["PhiCOD"].ToString()));
                html += "       <td style='text-align:center;vertical-align: inherit;'>" + COD.ToString("N0").Replace(",", ".") + "</td>";

                double CuocHangGui = (data.Rows[i]["ThanhTien"].ToString() == "" ? 0 : double.Parse(data.Rows[i]["ThanhTien"].ToString()));
                html += "       <td style='text-align:center;vertical-align: inherit;'>" + CuocHangGui.ToString("N0").Replace(",", ".") + "</td>";


                //html += "<td style='text-align: center;'>" + data.Rows[i]["DonGia"].ToString() + " </td> ";
                html += "<td style='text-align: center;'>" + " </td> ";
                html += "<td style='text-align: center;'>" + " </td> ";
                html += "</tr>";
                //SoTien += ThanhTien;

            }


            html += @"";

            html += @"</table>



          
       
                        
                 <div style='text-align: center; margin-top: 5px;font-size: 12px'>
                    <div>
                        
                    </div>
                    </br>
                    <div style='text-align: center; font-size: 10px'>
                        
                    </div>
                </div>
        </div>
    </div>
</div>
<input id='txtidHoaDon' hidden='hidden' value='" + idHD + "'/>";
            Response.Write(html);
        }
    }


    private void PrinfBienNhanXuongVP()
    {
        // in hóa đơn phân hàng lên xe
        string idHD = StaticData.ValidParameter(Request.QueryString["idHD"].Trim());

        string sqlOrderById = @" select idChiTietDonHang,ct.MaTinhTrang,ph.idNguoiDung,ct.idDonHang,MaTuyen, ct.MaHangHoa,TenKhachHang,dh.PhiCOD,MaDonHang,TenChiNhanh,kh.DiaChi,kh.SoDienThoai,SoLuong , TenHangHoa, NguoiNhanTra,(SoLuong*DonGia) as ThanhTien
                            , 'TenChiNhanhGui' = (select TenChiNhanh from tb_ChiNhanh where idChiNhanh = dh.idChiNhanhGui), BienSoXe,HoTen,
'TenNguoiLap' = (select Hoten from tb_NguoiDung where idNguoiDung = ph.idNguoiDung)
  from 
tb_ChiTietDonHang ct, tb_HangHoa hh, tb_DonHang dh, tb_HangVanPhong ph , tb_KhachHang kh,tb_ChiNhanh cn,tb_Xe x, tb_NguoiDung nd
  where ct.idHangVanPhong= " + idHD + @"  and ct.idHangHoa = hh.idHangHoa and dh.idDonHang = ct.idDonHang and ph.idHangVanPhong = ct.idHangVanPhong
 and dh.idKhachHang = kh.idKhachHang and dh.idChiNhanhNhan = cn.idChiNhanh and ph.idXe = x.idXe and x.idNguoiDung = nd.idNguoiDung
                            ";
        DataTable data = Connect.GetTable(sqlOrderById);
        if (data.Rows.Count > 0)
        {
            BarcodeSymbology s = BarcodeSymbology.Code39C;
            BarcodeDraw drawObject = BarcodeDrawFactory.GetSymbology(s);
            var metrics = drawObject.GetDefaultMetrics(60);
            metrics.Scale = 2;
            var barcodeImage = drawObject.Draw(idHD, metrics);

            string barCode = "";

            using (MemoryStream ms = new MemoryStream())
            {
                barcodeImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] imageBytes = ms.ToArray();

                barCode = Convert.ToBase64String(imageBytes);
            }

            double SoTien = 0;
            string html = @"
            <div style='width:100%'>
            <div style='font-family: 'Times New Roman', Times, serif; font-size: 13px; text-align: left; width: 800px; margin-top: -10px; margin-left: -20px;'>
        <div style='margin-top: 0; margin-left: 20px'>";

            // html += MyStaticData.GetTieuDeIn(barCode, MaXuatTra);
            html += @"<p style='font-size: 18px; text-align: left; font-weight: bold; margin: 0; padding: 10px 0 5px 0;'>
                NHÀ XE CÔ HAI
                <br> 319 Trần Phú - ĐT: 0839.242.264
            </p>";
            html += @"<p style='font-size: 24px; text-align: center;  margin: 0; padding: 10px 0 5px 0;'>
                BIÊN NHẬN PHÂN HÀNG XUỐNG VĂN PHÒNG
               
            </p>
 <p style='text-align: right; font-size: 14px; margin: 0; padding: 0 0 10px 0;' > Số: " + data.Rows[0]["MaTuyen"].ToString() + @"
            <p style='text-align: center; font-style: italic; margin: 0; padding: 0 0 10px 0;'> ";

            //    html += "     Ngày " + DateTime.Parse(data.Rows[0]["NgayLap"].ToString()).ToString("dd") + " Tháng " + DateTime.Parse(data.Rows[0]["NgayLap"].ToString()).ToString("MM") + " Năm " + DateTime.Parse(data.Rows[0]["NgayLap"].ToString()).ToString("yyyy");
            html += @" </p>

            <table width='100%' border='0' style='text-align: left; margin: 0; padding: 0 0 10px 0; ' cellpadding='1'>
                <!--=======================THÔNG TIN NGƯỜI TRẢ===============================--> 
                

                <tr>
                 
                     <td colspan='2'  style='font-size: 18px; '  width='20%' align='left'> Bên Giao:</td> 
  <td  style='font-size: 18px;text-align: left;' width='30%'> Xe:  " + data.Rows[0]["BienSoXe"].ToString() + " - Tài Xế: " + data.Rows[0]["HoTen"].ToString() + @"
                   
                  
                </tr>";
            html += @" <tr>
                 
                     <td colspan='2'  style='font-size: 18px; '  width='20%' align='left'> Bên Nhận:</td>
                      <td  style='font-size: 18px;text-align: left;' width='30%' > " + data.Rows[0]["TenChiNhanh"].ToString() + @"
                     
                </tr>";
            string Ngay = DateTime.Now.ToString("HH:mm:ss");
            html += @" <tr>
                 
                     <td colspan='2'  style='font-size: 18px; '  width='30%' align='left'> Thời Điểm:</td>
                     <td  style='font-size: 18px;text-align: left;' width='40%' > " + Ngay + " Ngày " + DateTime.Now.ToString("dd/MM/yyyy") + @"
                   
                </tr>";
            html += @" <tr>
                 
                     <td colspan='2'  style='font-size: 18px; '  width='30%' align='left'> Người Lập Phiếu:</td>
                     <td  style='font-size: 18px;text-align: left;' width='40%' > " + data.Rows[0]["TenNguoiLap"].ToString() + @"
                
                </tr>";


            //            html += @" <tr>
            //                 
            //                     <td colspan='3'  style='font-size: 12px; '  width='30%' align='left'> Ngày tạo:</td>
            //                     <td  style='font-size: 12px;text-align: left;' width='40%' > " + DateTime.Parse(data.Rows[0]["NgayLap"].ToString()).ToString("dd//MM/yyyy") + @"
            //                    
            //                </tr>";



            //</tr>
            // <tr>

            html += @"
            </table>
            <!--=======================DANH SÁCH CHI TIÊT  XUẤT TRẢ===============================-->
            <table width='100%' border='1' cellpadding='3' cellspacing='0' style='margin-top: 10px; font-size: 14px'>
                <tr>
                    <td style='font-size: 12px; background: #ccc; text-align: center; font-weight: bold; width: 40px;'>STT
                    </td>
               
              
                    <td style='font-size: 12px; background: #ccc; text-align: center; font-weight: bold; width: 90px;'>Mã Hàng Hóa
                    </td>
                        <td style='font-size: 12px; background: #ccc; text-align: center; font-weight: bold; width: 90px;'>Người Nhận
                    </td>
                     <td style='font-size: 12px; background: #ccc; text-align: center; font-weight: bold; width: 90px;'>Chi Nhánh Nhận
                       <td style='font-size: 12px; background: #ccc; text-align: center; font-weight: bold; width: 90px;'>Địa Chỉ 
                    </td>
                    <td style='font-size: 12px; background: #ccc; text-align: center; font-weight: bold; width: 90px;'>SĐT
                    </td>
                    <td style='font-size: 12px; background: #ccc; text-align: center; font-weight: bold; width: 90px;'>Số Lượng
                    </td>
                    <td style='font-size: 12px; background: #ccc; text-align: center; font-weight: bold; width: 90px;'>Tên Hàng Hóa 
                    </td>
                    <td style='font-size: 12px; background: #ccc; text-align: center; font-weight: bold; width: 90px;'>Tình Trạng
                    </td>
  <td style='font-size: 12px; background: #ccc; text-align: center; font-weight: bold; width: 90px;'>COD
                    </td>
                    <td style='font-size: 12px; background: #ccc; text-align: center; font-weight: bold; width: 90px;'>Tổng Cước
                    </td>
                    <td style='font-size: 12px; background: #ccc; text-align: center; font-weight: bold; width: 90px;'>Ký Nhận 
                    </td>
                    <td style='font-size: 12px; background: #ccc; text-align: center; font-weight: bold; width: 90px;'>Ghi Chú
                    </td>

                 
                </tr> ";

            for (int i = 0; i < data.Rows.Count; i++)
            {
                html += "<tr>";
                html += "<td style='text-align: center;'>" + (i + 1).ToString() + " </td> ";
                html += "<td style='text-align: center;'>" + data.Rows[i]["MaHangHoa"].ToString() + "<br> (" + data.Rows[i]["MaDonHang"].ToString() + ") </td> ";
               // html += "<td style='text-align: center;'>" + data.Rows[i]["MaHangHoa"].ToString() + " </td> ";
                html += "<td style='text-align: center;'>" + data.Rows[i]["TenKhachHang"].ToString() + " </td> ";
                if (data.Rows[i]["MaTinhTrang"].ToString() == "DHT")
                {
                    html += "<td style='text-align: center;'>" + data.Rows[i]["TenChiNhanh"].ToString() + " </td> ";
                }
                else
                {
                    string sq = @"select TenChiNhanh from  tb_NguoiDung nd , tb_ChiNhanh cn where 
                    idNguoiDung = '" + data.Rows[i]["idNguoiDung"].ToString() + "' and cn.idChiNhanh =nd.ChiNhanh";
                    DataTable kt = Connect.GetTable(sq);
                    if (kt.Rows.Count > 0)
                    {
                        string idChinhanh = (kt.Rows[0]["TenChiNhanh"].ToString());
                        html += "<td style='text-align: center;'>" + idChinhanh + " </td> ";

                    }
                    else
                        html += "<td style='text-align: center;'>" + data.Rows[i]["TenChiNhanh"].ToString() + " </td> ";
                }
                html += "<td style='text-align: center;'>" + data.Rows[i]["DiaChi"].ToString() + " </td> ";
                html += "<td style='text-align: center;'>" + data.Rows[i]["SoDienThoai"].ToString() + " </td> ";
                html += "<td style='text-align: center;'>" + data.Rows[i]["SoLuong"].ToString() + " </td> ";
                html += "<td style='text-align: center;'>" + data.Rows[i]["TenHangHoa"].ToString() + " </td> ";
                string NguoiNhan = data.Rows[i]["NguoiNhanTra"].ToString();
                if (NguoiNhan == "0")
                {
                    html += "<td style='text-align: center;'> Người Gửi Trả </td> ";

                }
                else
                {
                    html += "<td style='text-align: center;'> Người Nhận Trả </td> ";

                }

                double COD = (data.Rows[i]["PhiCOD"].ToString() == "" ? 0 : double.Parse(data.Rows[i]["PhiCOD"].ToString()));
                html += "       <td style='text-align:center;vertical-align: inherit;'>" + COD.ToString("N0").Replace(",", ".") + "</td>";
  
                double CuocHangGui = (data.Rows[i]["ThanhTien"].ToString() == "" ? 0 : double.Parse(data.Rows[i]["ThanhTien"].ToString()));
                html += "       <td style='text-align:center;vertical-align: inherit;'>" + CuocHangGui.ToString("N0").Replace(",", ".") + "</td>";

                //html += "<td style='text-align: center;'>" + data.Rows[i]["DonGia"].ToString() + " </td> ";
                html += "<td style='text-align: center;'>" + " </td> ";
                html += "<td style='text-align: center;'>" + " </td> ";
                html += "</tr>";
                //SoTien += ThanhTien;

            }


            html += @"";

            html += @"</table>



          
       
                        
                 <div style='text-align: center; margin-top: 5px;font-size: 12px'>
                    <div>
                        
                    </div>
                    </br>
                    <div style='text-align: center; font-size: 10px'>
                        
                    </div>
                </div>
        </div>
    </div>
</div>
<input id='txtidHoaDon' hidden='hidden' value='" + idHD + "'/>";
            Response.Write(html);
        }
    }
    private void XemChiTietDonHang()
    {
        string idDonHang = Request.QueryString["idDonHang"].Trim();
        string sqlChiTietDonHang = "select ctdh.*,lsp.TenLoaiSanPham,lsp.DonViTinh from tb_ChiTietDonHang ctdh inner join tb_LoaiSanPham lsp on ctdh.idLoaiSanPham=lsp.idLoaiSanPham";
        sqlChiTietDonHang += " where ctdh.idDonHang='" + idDonHang + "'";
        DataTable tbChiTietDonHang = Connect.GetTable(sqlChiTietDonHang);

        string html = "<table class='table table-bordered table-hover dataTable'>";
        html += @"<tr>
                    <th class='th' style='background-color:#dcf4fc;font-size:15px;color:#393131;'>
                        STT
                    </th>
                    <th class='th' style='background-color:#dcf4fc;font-size:15px;color:#393131;'>
                        Loại sản phẩm
                    </th>
                    <th class='th' style='background-color:#dcf4fc;font-size:15px;color:#393131;'>
                        Đơn vị tính
                    </th>
                    <th class='th' style='background-color:#dcf4fc;font-size:15px;color:#393131;'>
                        Số lượng
                    </th>
                    <th class='th' style='background-color:#dcf4fc;font-size:15px;color:#393131;'>
                        Tiền hàng
                    </th>
                </tr>";
        double TongSL = 0;
        double TongTH = 0;
        double TongTC = 0;
        for (int i = 0; i < tbChiTietDonHang.Rows.Count; i++)
        {
            html += "<tr>";
            html += "<td style='text-align:center;vertical-align: inherit;'>" + (i + 1).ToString() + "</td>";
            html += "<td>" + tbChiTietDonHang.Rows[i]["TenLoaiSanPham"].ToString() + "</td>";
            html += "<td>" + tbChiTietDonHang.Rows[i]["DonViTinh"].ToString() + "</td>";
            html += "<td style='text-align:center;vertical-align: inherit;'>" + tbChiTietDonHang.Rows[i]["SoLuong"].ToString() + "</td>";
            if (tbChiTietDonHang.Rows[i]["TienHang"].ToString() != "")
            {
                html += "<td style='text-align:center;vertical-align: inherit;'>" + double.Parse(tbChiTietDonHang.Rows[i]["TienHang"].ToString()).ToString("#,##").Replace(",", ".") + "</td>";
                TongTH += double.Parse(tbChiTietDonHang.Rows[i]["TienHang"].ToString());
            }
            else
                html += "<td></td>";
            //if (tbChiTietDonHang.Rows[i]["TienCuoc"].ToString() != "")
            //{
            //    html += "<td style='text-align:center;vertical-align: inherit;'>" + double.Parse(tbChiTietDonHang.Rows[i]["TienCuoc"].ToString()).ToString("#,##").Replace(",", ".") + "</td>";
            //    TongTC += double.Parse(tbChiTietDonHang.Rows[i]["TienCuoc"].ToString());
            //}
            //else
            //    html += "<td></td>";
            html += "</tr>";
            TongSL += (tbChiTietDonHang.Rows[i]["SoLuong"].ToString() == "" ? 0 : double.Parse(tbChiTietDonHang.Rows[i]["SoLuong"].ToString()));
        }
        html += "</table>";

        //html += "                       <div class='col-md-12'>";
        //html += "                           <div class='form-wrapper'>";
        //html += "                               <div class='table-responsive'><table class='table table-borderless' style='margin-bottom: 0;'>";
        //html += "                                   <tr><td style='width: 83.7%;text-align:right'>Tổng số mặt hàng :</td><td style='text-align:center'><b> " + tbChiTietDonHang.Rows.Count + "</b></td></tr>";
        //html += "                                   <tr><td style='width: 83.7%;text-align:right'>Tổng số lượng :</td><td style='text-align:center'><b> " + TongSL + "</b></td></tr>";
        //html += "                                   <tr><td style='text-align:right'>Tổng tiền hàng :</td><td style='text-align:center'><b> " + TongTH.ToString("N0").Replace(",", ".") + "</b></td></tr>";
        //html += "                                   <tr><td style='text-align:right'>Tổng tiền cước :</td><td style='text-align:center'><b> " + TongTC.ToString("N0").Replace(",", ".") + "</b></td></tr>";
        //html += "                               </table></div>";
        //html += "                           </div>";
        //html += "                       </div>";

        Response.Write(html);
    }
    private void ThemChiTietDonHang()
    {
        string idDonHang = Request.QueryString["idDonHang"].Trim();
        string idLoaiSanPham = Request.QueryString["idLoaiSanPham"].Trim();
        string SoLuong = Request.QueryString["SoLuong"].Trim();
        string TienHang = Request.QueryString["TienHang"].Trim().Replace(",", "").Replace(".", "");
        string TienCuoc = Request.QueryString["TienCuoc"].Trim().Replace(",", "").Replace(".", "");
        string sqlInsertCTDH = "insert into tb_ChiTietDonHang(idDonHang,idLoaiSanPham,SoLuong,TienHang,TienCuoc)";
        sqlInsertCTDH += " values('" + idDonHang + "','" + idLoaiSanPham + "','" + SoLuong + "','" + TienHang + "','" + TienCuoc + "')";
        bool ktInsertCTDH = Connect.Exec(sqlInsertCTDH);
        if (ktInsertCTDH)
            Response.Write("True");
        else
            Response.Write("False");
    }
    private void SuaChiTietDonHang()
    {
        string idChiTietDonHang = Request.QueryString["idChiTietDonHang"].Trim();
        string idLoaiSanPham = Request.QueryString["idLoaiSanPham"].Trim();
        string SoLuong = Request.QueryString["SoLuong"].Trim();
        string TienHang = Request.QueryString["TienHang"].Trim().Replace(",", "").Replace(".", "");
        string TienCuoc = Request.QueryString["TienCuoc"].Trim().Replace(",", "").Replace(".", "");
        string sqlUpdateCTDH = "update tb_ChiTietDonHang set";
        sqlUpdateCTDH += " idLoaiSanPham='" + idLoaiSanPham + "'";
        sqlUpdateCTDH += ",SoLuong='" + SoLuong + "'";
        sqlUpdateCTDH += ",TienHang='" + TienHang + "'";
        sqlUpdateCTDH += ",TienCuoc='" + TienCuoc + "'";
        sqlUpdateCTDH += " where idChiTietDonHang='" + idChiTietDonHang + "'";
        bool ktUpdateCTDH = Connect.Exec(sqlUpdateCTDH);
        if (ktUpdateCTDH)
            Response.Write("True");
        else
            Response.Write("False");
    }
    private void LoadDSChiTietDonHang()
    {
        string idDonHang = Request.QueryString["idDonHang"].Trim();
        string sqlChiTietDonHang = "select ctdh.*,lsp.TenLoaiSanPham,lsp.DonViTinh from tb_ChiTietDonHang ctdh inner join tb_LoaiSanPham lsp on ctdh.idLoaiSanPham=lsp.idLoaiSanPham";
        sqlChiTietDonHang += " where ctdh.idDonHang='" + idDonHang + "' order by idChiTietDonHang asc";
        DataTable tbChiTietDonHang = Connect.GetTable(sqlChiTietDonHang);
        string html = "";
        html += @"<table class='table table-bordered table-hover dataTable'>
                                <tr>
                                    <th class='th'>
                                        STT
                                    </th>
                                    <th class='th'>
                                        Tên loại sản phẩm
                                    </th>
                                    <th class='th'>
                                        Đơn vị tính
                                    </th>
                                    <th class='th'>
                                        Số lượng
                                    </th>
                                    <th class='th'>
                                        Tiền hàng
                                    </th>
                                    <th id='command' class='th'></th>
                                </tr>";
        for (int i = 0; i < tbChiTietDonHang.Rows.Count; i++)
        {
            html += "<tr>";
            html += "<td>" + (i + 1).ToString() + "</td>";
            html += "<td>" + tbChiTietDonHang.Rows[i]["TenLoaiSanPham"].ToString() + "</td>";
            html += "<td>" + tbChiTietDonHang.Rows[i]["DonViTinh"].ToString() + "</td>";
            if (tbChiTietDonHang.Rows[i]["SoLuong"].ToString() != "")
                html += "<td>" + tbChiTietDonHang.Rows[i]["SoLuong"].ToString() + "</td>";
            else
                html += "<td></td>";
            if (tbChiTietDonHang.Rows[i]["TienHang"].ToString() != "")
                html += "<td>" + double.Parse(tbChiTietDonHang.Rows[i]["TienHang"].ToString()).ToString("#,##").Replace(",", ".") + "</td>";
            else
                html += "<td></td>";
            //if (tbChiTietDonHang.Rows[i]["TienCuoc"].ToString() != "")
            //    html += "<td>" + double.Parse(tbChiTietDonHang.Rows[i]["TienCuoc"].ToString()).ToString("#,##").Replace(",", ".") + "</td>";
            //else
            //    html += "<td></td>";
            html += "       <td style='text-align:center'><img title='Sửa' class='imgCommand' src='../Images/edit.png' onclick='LoadChiTietDonHang(\"" + tbChiTietDonHang.Rows[i]["idChiTietDonHang"].ToString() + "\")' />";
            html += "       &nbsp;&nbsp;&nbsp;<img title='Xóa' class='imgCommand' src='../Images/delete.png' onclick='DeleteChiTietDonHang(\"" + tbChiTietDonHang.Rows[i]["idChiTietDonHang"].ToString() + "\")' /></td>";
            html += "</tr>";
        }
        html += "</table>";
        Response.Write(html);
    }
    private void LoadChiTietDonHang()
    {
        string idChiTietDonHang = Request.QueryString["idChiTietDonHang"].Trim();
        string sqlChiTietDonHang = "select * from tb_ChiTietDonHang where idChiTietDonHang='" + idChiTietDonHang + "'";
        DataTable tbChiTietDonHang = Connect.GetTable(sqlChiTietDonHang);
        string s = "";
        if (tbChiTietDonHang.Rows.Count > 0)
        {
            s += tbChiTietDonHang.Rows[0]["idLoaiSanPham"].ToString() + "|~~|";
            if (tbChiTietDonHang.Rows[0]["SoLuong"].ToString() != "")
                s += tbChiTietDonHang.Rows[0]["SoLuong"].ToString() + "|~~|";
            else
                s += "|~~|";
            if (tbChiTietDonHang.Rows[0]["TienHang"].ToString() != "")
                s += double.Parse(tbChiTietDonHang.Rows[0]["TienHang"].ToString()).ToString("#,##").Replace(",", ".") + "|~~|";
            else
                s += "|~~|";
            if (tbChiTietDonHang.Rows[0]["TienCuoc"].ToString() != "")
                s += double.Parse(tbChiTietDonHang.Rows[0]["TienCuoc"].ToString()).ToString("#,##").Replace(",", ".") + "|~~|";
            else
                s += "|~~|";
        }
        Response.Write(s);
    }
    private void DeleteChiTietDonHang()
    {
        string idChiTietDonHang = StaticData.ValidParameter(Request.QueryString["idChiTietDonHang"].Trim());
        string sql = "delete from tb_ChiTietDonHang where idChiTietDonHang='" + idChiTietDonHang + "'";
        bool ktDelete = Connect.Exec(sql);
        if (ktDelete)
            Response.Write("True");
        else
            Response.Write("False");
    }


    private void ChiNhanhGuiAutocomplete()
    {
        string sql = "select * from tb_ChiNhanh  ";
        DataTable table = Connect.GetTable(sql);
        string listgAutocomplete = "[";
        listgAutocomplete += "{";
        listgAutocomplete += "value: 'Chọn',";
        listgAutocomplete += "label: '--Chọn--',";
        listgAutocomplete += "id: '0'";
        listgAutocomplete += "},";
        for (int i = 0; i < table.Rows.Count; i++)
        {
            listgAutocomplete += "{";
            listgAutocomplete += "value: '" + table.Rows[i]["TenChiNhanh"].ToString() + "',";
            listgAutocomplete += "label: '" + table.Rows[i]["MaChiNhanh"].ToString() + " - " + table.Rows[i]["TenChiNhanh"].ToString() + "',";
            listgAutocomplete += "ten: '" + table.Rows[i]["MaChiNhanh"].ToString() + "',";
               listgAutocomplete += "id: '" + table.Rows[i]["idChiNhanh"].ToString() + "'";
            if (i == table.Rows.Count - 1)
                listgAutocomplete += "}";
            else
                listgAutocomplete += "},";
        }
        listgAutocomplete += " ]";
        Response.Write(listgAutocomplete);
    }

    private void ChiNhanhNhanAutocomplete()
    {
        string sql = "select * from tb_ChiNhanh  ";
        DataTable table = Connect.GetTable(sql);
        string listgAutocomplete = "[";
        listgAutocomplete += "{";
        listgAutocomplete += "value: 'Chọn',";
        listgAutocomplete += "label: '--Chọn--',";
        listgAutocomplete += "id: '0'";
        listgAutocomplete += "},";
        for (int i = 0; i < table.Rows.Count; i++)
        {
            listgAutocomplete += "{";
            listgAutocomplete += "value: '" + table.Rows[i]["TenChiNhanh"].ToString() + "',";
            listgAutocomplete += "label: '" + table.Rows[i]["MaChiNhanh"].ToString() + " - " + table.Rows[i]["TenChiNhanh"].ToString() + "',";
            listgAutocomplete += "ten: '" + table.Rows[i]["MaChiNhanh"].ToString() + "',";
            listgAutocomplete += "id: '" + table.Rows[i]["idChiNhanh"].ToString() + "'";
            if (i == table.Rows.Count - 1)
                listgAutocomplete += "}";
            else
                listgAutocomplete += "},";
        }
        listgAutocomplete += " ]";
        Response.Write(listgAutocomplete);
    }
    private void TinhTrangAutocomplete()
    {
        string sql = "select * from tb_TinhTrang  ";
        DataTable table = Connect.GetTable(sql);
        string listgAutocomplete = "[";
        listgAutocomplete += "{";
        listgAutocomplete += "value: 'Chọn',";
        listgAutocomplete += "label: '--Chọn--',";
        listgAutocomplete += "id: '0'";
        listgAutocomplete += "},";
        for (int i = 0; i < table.Rows.Count; i++)
        {
            listgAutocomplete += "{";
            listgAutocomplete += "value: '" + table.Rows[i]["TenTinhTrang"].ToString() + "',";
            listgAutocomplete += "label: '" + table.Rows[i]["MaTinhTrang"].ToString() + " - " + table.Rows[i]["TenTinhTrang"].ToString() + "',";
            listgAutocomplete += "ten: '" + table.Rows[i]["MaTinhTrang"].ToString() + "',";
         //   listgAutocomplete += "id: '" + table.Rows[i]["idHangHoa"].ToString() + "'";
            if (i == table.Rows.Count - 1)
                listgAutocomplete += "}";
            else
                listgAutocomplete += "},";
        }
        listgAutocomplete += " ]";
        Response.Write(listgAutocomplete);
    }
    private void TenHangAutocomplete()
    {

        string sql = "select * from tb_HangHoa  ";
        DataTable table = Connect.GetTable(sql);
        string listgAutocomplete = "[";
        listgAutocomplete += "{";
        listgAutocomplete += "value: 'Chọn',";
        listgAutocomplete += "label: '--Chọn--',";
        listgAutocomplete += "id: '0'";
        listgAutocomplete += "},";
        for (int i = 0; i < table.Rows.Count; i++)
        {
            listgAutocomplete += "{";
            listgAutocomplete += "value: '" + table.Rows[i]["TenHangHoa"].ToString() + "',";
            listgAutocomplete += "label: '" + table.Rows[i]["GiaCuoc"].ToString() + " - " + table.Rows[i]["TenHangHoa"].ToString() + "',";
            listgAutocomplete += "ten: '" + table.Rows[i]["GiaCuoc"].ToString() + "',";
            listgAutocomplete += "id: '" + table.Rows[i]["idHangHoa"].ToString() + "'";
            if (i == table.Rows.Count - 1)
                listgAutocomplete += "}";
            else
                listgAutocomplete += "},";
        }
        listgAutocomplete += " ]";
        Response.Write(listgAutocomplete);
    }
    private void LoadChiNhanh()
    {

        //string idDonHang = StaticData.ValidParameter(Request.QueryString["idDonHang"].Trim());
        //string idDonHang = StaticData.ValidParameter(Request.QueryString["idDonHang"].Trim());
        string Ngay = Request.QueryString["Ngay"].Trim();
        string sql = "select idChiTietDonHang, MaDonHang, TenHangHoa, SoLuong from tb_ChiTietDonHang ct, tb_HangHoa hh, tb_DonHang dh where NgayLap = '" + Ngay + "' and IDPhanHangLenXe IS Null and ct.idHangHoa = hh.idHangHoa and dh.idDonHang = ct.idDonHang";
        DataTable tb = Connect.GetTable(sql);
        string listgAutocomplete = string.Join(Environment.NewLine, tb.Rows.OfType<DataRow>().Select(x => string.Join("|~|", x.ItemArray)));
        Response.Write(listgAutocomplete);
    }
    private void ChonSanPhamAutocomplete()
    {

        //string idDonHang = StaticData.ValidParameter(Request.QueryString["idDonHang"].Trim());
        //string idDonHang = StaticData.ValidParameter(Request.QueryString["idDonHang"].Trim());
        string idChiNhanh = Request.QueryString["idChiNhanh"].Trim();
        string sql = @"select idChiTietDonHang, ct.MaHangHoa,TenKhachHang,TenChiNhanh,kh.DiaChi,SoLuong , TenHangHoa, NguoiNhanTra
                from tb_ChiTietDonHang ct, tb_HangHoa hh, tb_DonHang dh , tb_KhachHang kh,tb_ChiNhanh cn
                 where  (idChiNhanhGui = '" + idChiNhanh + @"' or '" + idChiNhanh + @"' in  (select ChiNhanh from tb_HangVanPhong hvp, tb_NguoiDung ng 
				 where hvp.idNguoiDung = ng.idNguoiDung and  idHangVanPhong = ct.idHangVanPhong) ) and ct.idHangHoa = hh.idHangHoa and dh.idDonHang = ct.idDonHang 
                 and dh.idKhachHang = kh.idKhachHang and dh.idChiNhanhNhan = cn.idChiNhanh and ct.MaTinhTrang = 'DGX'  "; //ct.IDPhanHangLenXe IS Null
        DataTable tb = Connect.GetTable(sql);
        string listgAutocomplete = string.Join(Environment.NewLine, tb.Rows.OfType<DataRow>().Select(x => string.Join("|~|", x.ItemArray)));
        Response.Write(listgAutocomplete);
    }


    private void LoadPopupLichSuNo()
    {
        string idKhachHang = StaticData.ValidParameter(Request.QueryString["idKhachHang"].Trim());

        string sql = @"select tn.*, HoTen  from tb_TraNoKhachHang tn, tb_NguoiDung nd where  idKhachHang = '" + idKhachHang + "' and tn.idNguoiDung = nd.idNguoiDung and SoTien >0";
        string html = @"<table class='table table-bordered table-hover dataTable'>
                            <tr style='white-space: nowrap;'>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    STT
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                   Tên Khách Hàng
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                   Mã Phiếu Trã
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Số Tiền
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Ngày Trả
                                </th>
                                 <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Người Thu
                                </th>
                                                           
                             
                              
                                
                            </tr>";
        DataTable table = Connect.GetTable(sql);
        for (int i = 0; i < table.Rows.Count; i++)
        {


            html += "<tr>";
            html += "<td>" + (i + 1) + "</td>";

            html += "<td style='text-align:center;vertical-align: inherit;'>" + StaticData.getField("tb_KhachHang", "TenKhachHang", "IDKhachHang", table.Rows[i]["IDKhachHang"].ToString().Trim()) + "</td>";
            html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["MaPhieuTra"].ToString() + "</td>";
            html += "       <td style='text-align:center;vertical-align: inherit;'>" + (table.Rows[i]["SoTien"].ToString() == "" ? 0 : double.Parse(table.Rows[i]["SoTien"].ToString())).ToString("N0").Replace(",", ".") + "</td>";
            DateTime NgayThanhToan = DateTime.Parse(table.Rows[i]["NgayTra"].ToString());
            html += "       <td style='text-align:center;vertical-align: inherit;'>" + NgayThanhToan.ToString("dd/MM/yyyy") + "</td>";
            //   html += "       <td style='text-align:center;vertical-align: inherit;white-space: nowrap;' id='tdtest'> <a onclick='ontest()' style='cursor:pointer;'><span style='font-size:20px;'><i class='fa fa-check-circle'></i></span> Nhận</a> </td>";

            html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["HoTen"].ToString() + "</td>";
       

            html += "</tr>";
        }

        Response.Write(html);
    }
    private void SanPhamAutocomplete()
    {

        //string idDonHang = StaticData.ValidParameter(Request.QueryString["idDonHang"].Trim());
        //string idDonHang = StaticData.ValidParameter(Request.QueryString["idDonHang"].Trim());
        string idChiNhanh = Request.QueryString["idChiNhanh"].Trim();
        string sql = @"select idChiTietDonHang, ct.MaHangHoa,TenKhachHang,TenChiNhanh,kh.DiaChi,SoLuong , TenHangHoa, NguoiNhanTra
                from tb_ChiTietDonHang ct, tb_HangHoa hh, tb_DonHang dh, tb_PhanHangLenXe ph , tb_KhachHang kh,tb_ChiNhanh cn
                 where  IDXe = '" + idChiNhanh + @"' and ct.idHangHoa = hh.idHangHoa and dh.idDonHang = ct.idDonHang and ph.idPhanHangLenXe = ct.idPhanHangLenXe
                 and dh.idKhachHang = kh.idKhachHang and dh.idChiNhanhNhan = cn.idChiNhanh and  MaTinhTrang != 'XDVP' and  MaTinhTrang != 'DHT'";
        DataTable tb = Connect.GetTable(sql);
        string listgAutocomplete = string.Join(Environment.NewLine, tb.Rows.OfType<DataRow>().Select(x => string.Join("|~|", x.ItemArray)));
        Response.Write(listgAutocomplete);
    }

   
    private void ChonDonHangAutocomplete()
    {
        string sql = "select MaDonHang, NgayLap, dh.IDDonHang from tb_DonHang dh, tb_ChiTietDonHang ct where dh.idDonHang = ct.idDonHang and ct.IDPhanHangLenXe IS Null group by dh.IDDonHang, NgayLap, MaDonHang ";
        DataTable table = Connect.GetTable(sql);
        string listgAutocomplete = "[";
        listgAutocomplete += "{";
        listgAutocomplete += "value: 'Chọn',";
        listgAutocomplete += "label: '--Chọn--',";
        listgAutocomplete += "id: '0'";
        listgAutocomplete += "},";
        for (int i = 0; i < table.Rows.Count; i++)
        {
            listgAutocomplete += "{";
            listgAutocomplete += "value: '" + DateTime.Parse(table.Rows[i]["NgayLap"].ToString()).ToString("dd/MM/yyyy") + "',";
            listgAutocomplete += "label: '" + table.Rows[i]["MaDonHang"].ToString() + " - " + DateTime.Parse(table.Rows[i]["NgayLap"].ToString()).ToString("dd/MM/yyyy") + "',";
            listgAutocomplete += "ten: '" + table.Rows[i]["MaDonHang"].ToString() + "',";
            listgAutocomplete += "id: '" + table.Rows[i]["IDDonHang"].ToString() + "'";
            if (i == table.Rows.Count - 1)
                listgAutocomplete += "}";
            else
                listgAutocomplete += "},";
        }
        listgAutocomplete += " ]";
        Response.Write(listgAutocomplete);
    }
    private void PhanXeAutocomplete()
    {
        string sql = "select *,HoTen   from tb_Xe, tb_NguoiDUng nd where nd.idNguoiDung = tb_Xe.idnguoiDung ";
        DataTable table = Connect.GetTable(sql);
        string listgAutocomplete = "[";
        listgAutocomplete += "{";
        listgAutocomplete += "value: 'Chọn',";
        listgAutocomplete += "label: '--Chọn--',";
        listgAutocomplete += "id: '0'";
        listgAutocomplete += "},";
        for (int i = 0; i < table.Rows.Count; i++)
        {
            listgAutocomplete += "{";
            listgAutocomplete += "value: '" + table.Rows[i]["HoTen"].ToString() + "',";
            listgAutocomplete += "label: '" + table.Rows[i]["BienSoXe"].ToString() + " - " + table.Rows[i]["HoTen"].ToString() + "',";
            listgAutocomplete += "ten: '" + table.Rows[i]["BienSoXe"].ToString() + "',";
            listgAutocomplete += "id: '" + table.Rows[i]["idXe"].ToString() + "'";
            if (i == table.Rows.Count - 1)
                listgAutocomplete += "}";
            else
                listgAutocomplete += "},";
        }
        listgAutocomplete += " ]";
        Response.Write(listgAutocomplete);
    }
    private void TaiXeAutocomplete()
    {
        string sql = "select * from tb_NguoiDung where MaQuyen = 'TaiXe' ";
        DataTable table = Connect.GetTable(sql);
        string listgAutocomplete = "[";
        listgAutocomplete += "{";
        listgAutocomplete += "value: 'Chọn',";
        listgAutocomplete += "label: '--Chọn--',";
        listgAutocomplete += "id: '0'";
        listgAutocomplete += "},";
        for (int i = 0; i < table.Rows.Count; i++)
        {
            listgAutocomplete += "{";
            listgAutocomplete += "value: '" + table.Rows[i]["HoTen"].ToString() + "',";
            listgAutocomplete += "label: '" + table.Rows[i]["SoDienThoai"].ToString() + " - " + table.Rows[i]["HoTen"].ToString() + "',";
            listgAutocomplete += "ten: '" + table.Rows[i]["SoDienThoai"].ToString() + "',";
            listgAutocomplete += "id: '" + table.Rows[i]["idNguoiDung"].ToString() + "'";
            if (i == table.Rows.Count - 1)
                listgAutocomplete += "}";
            else
                listgAutocomplete += "},";
        }
        listgAutocomplete += " ]";
        Response.Write(listgAutocomplete);
    }
    private void KhachHangAutocomplete()
    {
        string sql = "select * from tb_KhachHang";
        DataTable table = Connect.GetTable(sql);
        string listgAutocomplete = "[";
        listgAutocomplete += "{";
        listgAutocomplete += "value: 'Chọn',";
        listgAutocomplete += "label: '--Chọn--',";
        listgAutocomplete += "id: '0'";
        listgAutocomplete += "},";
        for (int i = 0; i < table.Rows.Count; i++)
        {
            listgAutocomplete += "{";
            listgAutocomplete += "value: '" + table.Rows[i]["SoDienThoai"].ToString() + "',";
            listgAutocomplete += "label: '" + table.Rows[i]["TenKhachHang"].ToString() + " - " + table.Rows[i]["SoDienThoai"].ToString() + "',";
            listgAutocomplete += "ten: '" + table.Rows[i]["TenKhachHang"].ToString() + "',";
            listgAutocomplete += "diachi: '" + table.Rows[i]["DiaChi"].ToString() + "',";
            listgAutocomplete += "id: '" + table.Rows[i]["idKhachHang"].ToString() + "'";
            if (i == table.Rows.Count - 1)
                listgAutocomplete += "}";
            else
                listgAutocomplete += "},";
        }
        listgAutocomplete += " ]";
        Response.Write(listgAutocomplete);
    }
    private void KhachHangAutocomplete1()
    {
        string sql = "select * from tb_KhachHang";
        DataTable table = Connect.GetTable(sql);
        string listgAutocomplete = "[";
        listgAutocomplete += "{";
        listgAutocomplete += "value: 'Tất cả',";
        listgAutocomplete += "label: '--Tất cả--',";
        listgAutocomplete += "id: '0'";
        listgAutocomplete += "},";
        for (int i = 0; i < table.Rows.Count; i++)
        {
            listgAutocomplete += "{";
            listgAutocomplete += "value: '" + table.Rows[i]["TenKhachHang"].ToString() + " - " + table.Rows[i]["idKhachHang"].ToString() + "',";
            listgAutocomplete += "label: '" + table.Rows[i]["TenKhachHang"].ToString() + " - " + table.Rows[i]["idKhachHang"].ToString() + "',";
            listgAutocomplete += "id: '" + table.Rows[i]["idKhachHang"].ToString() + "'";
            if (i == table.Rows.Count - 1)
                listgAutocomplete += "}";
            else
                listgAutocomplete += "},";
        }
        listgAutocomplete += " ]";
        Response.Write(listgAutocomplete);
    }
    private void DeleteDonHang()
    {
        string idDonHang = StaticData.ValidParameter(Request.QueryString["idDonHang"].Trim());
        string sql1 = "delete from tb_ChiTietDonHang where idDonHang='" + idDonHang + "'";
        bool ktDelete1 = Connect.Exec(sql1);

        string sql = "delete from tb_DonHang where idDonHang='" + idDonHang + "'";
        bool ktDelete = Connect.Exec(sql);
        if (ktDelete)
            Response.Write("True");
        else
            Response.Write("False");
    }
    private void DeleteKho()
    {
        string idKho = StaticData.ValidParameter(Request.QueryString["idKho"].Trim());
        string sql = "delete from tb_Kho where idKho='" + idKho + "'";
        bool ktDelete = Connect.Exec(sql);
        if (ktDelete)
            Response.Write("True");
        else
            Response.Write("False");
    }
    private void DeleteLoaiCuoc()
    {
        string MaLoaiCuoc = StaticData.ValidParameter(Request.QueryString["MaLoaiCuoc"].Trim());
        string sql = "delete from tb_LoaiCuoc where MaLoaiCuoc='" + MaLoaiCuoc + "'";
        bool ktDelete = Connect.Exec(sql);
        if (ktDelete)
            Response.Write("True");
        else
            Response.Write("False");
    }
    private void DeleteNguoiDung()
    {
        string idNguoiDung = StaticData.ValidParameter(Request.QueryString["idNguoiDung"].Trim());
        string sql = "delete from tb_NguoiDung where idNguoiDung='" + idNguoiDung + "'";
        bool ktDelete = Connect.Exec(sql);
        if (ktDelete)
            Response.Write("True");
        else
            Response.Write("False");
    }
    private void DeleteKhachHang()
    {
        string idKhachHang = StaticData.ValidParameter(Request.QueryString["idKhachHang"].Trim());
        string sql = "delete from tb_KhachHang where idKhachHang='" + idKhachHang + "'";
        bool ktDelete = Connect.Exec(sql);
        if (ktDelete)
            Response.Write("True");
        else
            Response.Write("False");
    }
    private void DeleteLoaiSanPham()
    {
        string idLoaiSanPham = StaticData.ValidParameter(Request.QueryString["idLoaiSanPham"].Trim());
        string sql = "delete from tb_LoaiSanPham where idLoaiSanPham='" + idLoaiSanPham + "'";
        bool ktDelete = Connect.Exec(sql);
        if (ktDelete)
            Response.Write("True");
        else
            Response.Write("False");
    }
}