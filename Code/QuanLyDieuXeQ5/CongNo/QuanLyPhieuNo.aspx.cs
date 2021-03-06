﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class QuanLyXuatKho_QuanLyXuatKho : System.Web.UI.Page
{
    int SoHoaDon = 0;
    double TongSoLuong = 0;
    double TongTienHang = 0;
    double TongTienCuoc = 0;
    double TongPhuPhi = 0;

    string sTuNgay = "";
    string sDenNgay = "";
    string sTen = "";
    //string sMaDonHang = "";
    //string sIdKho = "";
    string sIdKhachHang = "";
    string sMaTinhTrang = "";
    string sIdTinh = "";
    string sIdHuyen = "";
    //string sDiaChiNguoiNhan = "";
    string sIdNguoiDung = "";
    string sNgayDuKienGiao = "";
    string sIsDaHoanThanh = "";

    string mTenDangNhap = "";
    string mQuyen = "";
    string mIdNguoiDung = "";
    string mIdKhachHang = "";

    string txtFistPage = "1";
    string txtPage1 = "";
    string txtPage2 = "";
    string txtPage3 = "";
    string txtPage4 = "";
    string txtPage5 = "";
    string txtLastPage = "";
    int Page = 0;
    int MaxPage = 0;
    int PageSize = 20;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Page = int.Parse(Request.QueryString["Page"].ToString());
        }
        catch
        {
            Page = 1;
        }

        if (!IsPostBack)
        {
            try
            {
                if (Request.QueryString["TuNgay"].Trim() != "")
                {
                    sTuNgay = Request.QueryString["TuNgay"].Trim();
                    txtTuNgay.Value = sTuNgay;
                }
            }
            catch { }
            try
            {
                if (Request.QueryString["DenNgay"].Trim() != "")
                {
                    sDenNgay = Request.QueryString["DenNgay"].Trim();
                    txtDenNgay.Value = sDenNgay;
                }
            }
            catch { }
            //try
            //{
            //    if (Request.QueryString["DiaChiNguoiNhan"].Trim() != "")
            //    {
            //        sDiaChiNguoiNhan = Request.QueryString["DiaChiNguoiNhan"].Trim();
            //        txtDiaChiNguoiNhan.Value = sDiaChiNguoiNhan;
            //    }
            //}
            //catch { }
            try
            {
                if (Request.QueryString["NgayDuKienGiao"].Trim() != "")
                {
                    sNgayDuKienGiao = Request.QueryString["NgayDuKienGiao"].Trim();
                    //txtKhachHang.Value = sNgayDuKienGiao;
                }
            }
            catch { }
            //if (Request.Cookies["QuanLyCongNoAnhKiet_Login"] != null && Request.Cookies["QuanLyCongNoAnhKiet_Login"].Value.Trim() != "")
            //{
            //    mTenDangNhap = Request.Cookies["QuanLyCongNoAnhKiet_Login"].Value.Trim();
            //    mIdNguoiDung = StaticData.getField("tb_NguoiDung", "idNguoiDung", "TenDangNhap", mTenDangNhap);
            //    mIdKhachHang = StaticData.getField("tb_KhachHang", "idKhachHang", "TenDangNhap", mTenDangNhap);
            //    mQuyen = MyStaticData.GetMaQuyen(mTenDangNhap);
            //    //if (mQuyen.ToUpper() != "ADMIN" && mQuyen.ToUpper() != "NVGN" && mQuyen.ToUpper() != "NVVP" && mQuyen.ToUpper() != "KH")
            //    //{
            //    //    Response.Redirect("../Home/DangNhap.aspx");
            //    //}
            //    if(mQuyen.ToUpper() == "KH")
            //    {
            //        //string isImportExcel = StaticData.getField("tb_KhachHang", "isImportExcel", "idKhachHang", mIdKhachHang);
            //        //if(isImportExcel != "True")
            //        //{
            //        //    btImportExcel.Style.Add("display", "none");
            //        //}
            //        txtTenKhachHang.Disabled = true;
            //    }
            //    if (mQuyen.ToUpper() == "NVGN")
            //    {
            //        btThemMoi.Style.Add("display", "none");
            //        //btImportExcel.Style.Add("display", "none");
            //    }
            //}
            //LoadKho();
            //try
            //{
            //    if (Request.QueryString["MaDonHang"].Trim() != "")
            //    {
            //        sMaDonHang = Request.QueryString["MaDonHang"].Trim();
            //        txtMaDonHang.Value = sMaDonHang;
            //    }
            //}
            //catch { }
            /*try
            {
                if (Request.QueryString["idKho"].Trim() != "")
                {
                    sIdKho = Request.QueryString["idKho"].Trim();
                    slKho.Value = sIdKho;
                }
            }
            catch { }*/
            try
            {
                if (Request.QueryString["idKhachHang"].Trim() != "")
                {
                    sIdKhachHang = Request.QueryString["idKhachHang"].Trim();
                    hdIdKhachHang.Value = sIdKhachHang;
                    txtTenKhachHang.Value = StaticData.getField("tb_KhachHang", "TenKhachHang", "idKhachHang", sIdKhachHang);
                }
            }
            catch { }
            try
            {
                if (Request.QueryString["Ten"].Trim() != "")
                {
                    sTen = Request.QueryString["Ten"].Trim();
                    txtTenKhachHang.Value = sTen;
                }
            }
            catch { }
            dvDanhSachDonHang.InnerHtml = @"<center><table class='table table-bordered table-hover dataTable'>
                            <tr style='white-space: nowrap;'>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    STT
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Mã phiếu trả
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Ngày lập
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Khách hàng
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Số tiền
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Ghi chú
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    
                                </th>
                            </tr>
                            </tr>
                            <tr>
                                <td>
                                    1
                                </td>
                                <td>
                                    PTN-0000001
                                </td>
                                <td>
                                    29/05/2018
                                </td>
                                <td>
                                Nguyễn Văn A
                                </td>
                                <td style='text-align:center;'>
                                    20.000
                                </td>
                                <td style='text-align:center;'>
                                    
                                </td>
                                <td style='text-align:center;vertical-align: inherit;font-size:20px;white-space: nowrap;'>
                                    <a href='#'><i class='fa fa-edit'></i></a>
                                    <a href='#'><i class='fa fa-trash'></i></a>
                                </td>
                            </tr>
                        </table>
                    </center>";
            //LoadDonHang();
        }
    }
    /*private void LoadKho()
    {
        string strSql = "select * from tb_Kho";
        slKho.DataSource = Connect.GetTable(strSql);
        slKho.DataTextField = "TenKho";
        slKho.DataValueField = "idKho";
        slKho.DataBind();
        slKho.Items.Add(new ListItem("-- Tất cả --", "0"));
        slKho.Items.FindByText("-- Tất cả --").Selected = true;
    }*/
    #region paging
    private void SetPage()
    {
        string sql = "select count(idDonHang), isNull(Sum(TienHang),0), isNull(Sum(TienCuoc),0), isNull(Sum(PhuPhi),0) from tb_DonHang where '1'='1'";
        if (sTuNgay != "")
            sql += " and NgayLap >= '" + StaticData.ConvertDDMMtoMMDD(sTuNgay) + " 00:00:00'";
        if (sDenNgay != "")
            sql += " and NgayLap <= '" + StaticData.ConvertDDMMtoMMDD(sDenNgay) + " 00:00:00'";
        //if (sMaDonHang != "")
        //    sql += " and MaDonHang like N'%" + sMaDonHang + "%'";
        if (sIdNguoiDung != "" && sIdNguoiDung != "0")
            sql += " and idNguoiDung = '" + sIdNguoiDung + "'";
        if (mQuyen.ToUpper() == "NVGN")
            sql += " and idNguoiDung='" + mIdNguoiDung + "'";
        if (sIdKhachHang != "")
            sql += " and idKhachHang='" + sIdKhachHang + "'";
        if (sTen != "")
            sql += " and (MaDonHang like N'%" + sTen + "%' or DiaChiNguoiNhan like N'%" + sTen + "%' or SoDienThoaiNguoiNhan like N'%" + sTen + "%' or NguoiNhan like N'%" + sTen + "%')";
        if (mQuyen.ToUpper() == "KH")
            sql += " and idKhachHang='" + mIdKhachHang + "'";
        if (sMaTinhTrang != "")
        {
            if (sMaTinhTrang == "True" || sMaTinhTrang == "False")
                sql += " and isDaNhanTien='" + sMaTinhTrang + "'";
            else
                sql += " and MaTinhTrang='" + sMaTinhTrang + "'";
        }
        if (sIdTinh != "" && sIdTinh != "0")
            sql += " and idTinh='" + sIdTinh + "'";
        if (sIdHuyen != "" && sIdTinh != "0")
            sql += " and idHuyen='" + sIdHuyen + "'";
        if (sNgayDuKienGiao != "")
            sql += " and convert(varchar(10),ThoiDiemDuKienGiao,103) = '" + sNgayDuKienGiao + "'";
        if (sIsDaHoanThanh != "")
            sql += " and isnull(isDaNhanTien,'False') = '" + sIsDaHoanThanh + "'";
        DataTable tbTotalRows = Connect.GetTable(sql);
        int TotalRows = int.Parse(tbTotalRows.Rows[0][0].ToString());
        TongTienHang = double.Parse(tbTotalRows.Rows[0][1].ToString());
        TongTienCuoc = double.Parse(tbTotalRows.Rows[0][2].ToString());
        TongPhuPhi = double.Parse(tbTotalRows.Rows[0][3].ToString());
        SoHoaDon = TotalRows;
        if (TotalRows % PageSize == 0)
            MaxPage = TotalRows / PageSize;
        else
            MaxPage = TotalRows / PageSize + 1;
        txtLastPage = MaxPage.ToString();
        if (Page == 1)
        {
            for (int i = 1; i <= MaxPage; i++)
            {
                if (i <= 5)
                {
                    switch (i)
                    {
                        case 1: txtPage1 = i.ToString(); break;
                        case 2: txtPage2 = i.ToString(); break;
                        case 3: txtPage3 = i.ToString(); break;
                        case 4: txtPage4 = i.ToString(); break;
                        case 5: txtPage5 = i.ToString(); break;
                    }
                }
                else
                    return;
            }
        }
        else
        {
            if (Page == 2)
            {
                for (int i = 1; i <= MaxPage; i++)
                {
                    if (i == 1)
                        txtPage1 = "1";
                    if (i <= 5)
                    {
                        switch (i)
                        {
                            case 2: txtPage2 = i.ToString(); break;
                            case 3: txtPage3 = i.ToString(); break;
                            case 4: txtPage4 = i.ToString(); break;
                            case 5: txtPage5 = i.ToString(); break;
                        }
                    }
                    else
                        return;
                }
            }
            else
            {
                int Cout = 1;
                if (Page <= MaxPage)
                {
                    for (int i = Page; i <= MaxPage; i++)
                    {
                        if (i == Page)
                        {
                            txtPage1 = (Page - 2).ToString();
                            txtPage2 = (Page - 1).ToString();
                        }
                        if (Cout <= 3)
                        {
                            if (i == Page)
                                txtPage3 = i.ToString();
                            if (i == (Page + 1))
                                txtPage4 = i.ToString();
                            if (i == (Page + 2))
                                txtPage5 = i.ToString();
                            Cout++;
                        }
                        else
                            return;
                    }
                }
                else
                {
                    //Page = MaxPage;
                    SetPage();
                }
            }
        }
    }
    #endregion
    private void LoadDonHang()
    {
        string sql = "";
        sql += @"select * from
            (
	            SELECT ROW_NUMBER() OVER
                  (
                        ORDER BY idDonHang desc
                  )AS RowNumber
	              ,dh.*,kh.TenKhachHang,kh.SoDienThoai
                  FROM tb_DonHang dh, tb_KhachHang kh where kh.idKhachHang = dh.idKhachHang
            ";
        if (sTuNgay != "")
            sql += " and NgayLap >= '" + StaticData.ConvertDDMMtoMMDD(sTuNgay) + " 00:00:00'";
        if (sDenNgay != "")
            sql += " and NgayLap <= '" + StaticData.ConvertDDMMtoMMDD(sDenNgay) + " 00:00:00'";
        //if (sMaDonHang != "")
        //    sql += " and MaDonHang like N'%" + sMaDonHang + "%'";
        if (sIdNguoiDung != "" && sIdNguoiDung != "0")
            sql += " and dh.idNguoiDung = '" + sIdNguoiDung + "'";
        if (mQuyen.ToUpper() == "NVGN")
            sql += " and dh.idNguoiDung='" + mIdNguoiDung + "'";
        if (sIdKhachHang != "")
            sql += " and dh.idKhachHang='" + sIdKhachHang + "'";
        if (sTen != "")
            sql += " and (MaDonHang like N'%" + sTen + "%' or DiaChiNguoiNhan like N'%" + sTen + "%' or SoDienThoaiNguoiNhan like N'%" + sTen + "%' or NguoiNhan like N'%" + sTen + "%')";
        if (mQuyen.ToUpper() == "KH")
            sql += " and dh.idKhachHang='" + mIdKhachHang + "'";
        if (sMaTinhTrang != "")
        {
            if(sMaTinhTrang == "True" || sMaTinhTrang == "False")
                sql += " and isDaNhanTien='" + sMaTinhTrang + "'";
            else
                sql += " and MaTinhTrang='" + sMaTinhTrang + "'";
        }
        if (sIdTinh != "" && sIdTinh != "0")
            sql += " and idTinh='" + sIdTinh + "'";
        if (sIdHuyen != "" && sIdTinh != "0")
            sql += " and idHuyen='" + sIdHuyen + "'";
        if (sNgayDuKienGiao != "")
            sql += " and convert(varchar(10),ThoiDiemDuKienGiao,103) = '" + sNgayDuKienGiao + "'";
        if (sIsDaHoanThanh != "")
            sql += " and isnull(isDaNhanTien,'False') = '" + sIsDaHoanThanh + "'";
        sql += ") as tb1 WHERE RowNumber BETWEEN (" + Page + " - 1) * " + PageSize + " + 1 AND (((" + Page + " - 1) * " + PageSize + " + 1) + " + PageSize + ") - 1";


        DataTable table = Connect.GetTable(sql);
        //txtNoiDung.InnerHtml = table.Rows[0]["NoiDung"].ToString();
        SetPage();
        string html = @"<center><table class='table table-bordered table-hover dataTable'>
                            <tr style='white-space: nowrap;'>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    STT
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Mã đơn hàng
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Ngày lập
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Người gửi
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Ngày dự kiến giao
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Tình trạng
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Thanh toán
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Tiền hàng
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Tiền cước
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Phụ phí
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Tổng tiền
                                </th>
                            </tr>";
        html += "           <tr style='background: #ff7423;'>";
        html += "               <td colspan='7' style='text-align:center;vertical-align: inherit;'><b>Tổng cộng (" + SoHoaDon + " đơn)</b></td>";
        html += "               <td style='text-align:center;vertical-align: inherit;'><b>" + TongTienHang.ToString("N0").Replace(",", ".") + "</b></td>";
        html += "               <td style='text-align:center;vertical-align: inherit;'><b>" + TongTienCuoc.ToString("N0").Replace(",", ".") + "</b></td>";
        html += "               <td style='text-align:center;vertical-align: inherit;'><b>" + TongPhuPhi.ToString("N0").Replace(",", ".") + "</b></td>";
        html += "               <td style='text-align:center;vertical-align: inherit;'><b>" + (TongTienHang + TongTienCuoc + TongPhuPhi).ToString("N0").Replace(",", ".") + "</b></td>";
        html += "           </tr>";
        for (int i = 0; i < table.Rows.Count; i++)
        {
            string sqlCTDH = "select TongSoLuong=sum(isnull(SoLuong,0)),TongTienHang=sum(isnull(TienHang,0)),TongTienCuoc=sum(isnull(TienCuoc,0)) from tb_ChiTietDonHang where idDonHang='" + table.Rows[i]["idDonHang"].ToString() + "'";
            DataTable tbCTDH = Connect.GetTable(sqlCTDH);
            string idDonHang = table.Rows[i]["idDonHang"].ToString();
            html += "   <tr onclick=XemChiTiet(\"" + idDonHang + "\") style='cursor:pointer'>";
            html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["RowNumber"].ToString() + "</td>";
            html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["MaDonHang"].ToString() + "</td>";
            html += "       <td style='text-align:center;vertical-align: inherit;'>" + DateTime.Parse(table.Rows[i]["NgayLap"].ToString()).ToString("dd/MM/yyyy") + "</td>";
            html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["TenKhachHang"].ToString() + "</td>";
            if (table.Rows[i]["ThoiDiemDuKienGiao"].ToString() != "")
                html += "<td style='text-align:center;vertical-align: inherit;'>" + DateTime.Parse(table.Rows[i]["ThoiDiemDuKienGiao"].ToString()).ToString("dd/MM/yyyy hh:mm") + "</td>";
            else
                html += "<td></td>";
            html += "       <td style='text-align:center;vertical-align: inherit;'>" + StaticData.getField("tb_TinhTrang", "TenTinhTrang", "MaTinhTrang", table.Rows[i]["MaTinhTrang"].ToString()) + "</td>";
            if (mQuyen.ToUpper() != "ADMIN" && mQuyen.ToUpper() != "NVVP")
            {
                html += "       <td></td>";
            }
            else
            {
                if (table.Rows[i]["isDaNhanTien"].ToString() == "True")
                    html += "       <td style='text-align: center;'><input type='checkbox' id='ckbDaNhanTien_" + table.Rows[i]["idDonHang"].ToString() + "' checked='true' disabled='disabled'/></td>";
                else
                    html += "       <td style='text-align: center;'><input type='checkbox' id='ckbDaNhanTien_" + table.Rows[i]["idDonHang"].ToString() + "' disabled='disabled'/></td>";
            }
            double TienHang = (table.Rows[i]["TienHang"].ToString() == "" ? 0 : double.Parse(table.Rows[i]["TienHang"].ToString()));
            html += "       <td style='text-align:center;vertical-align: inherit;'>" + TienHang.ToString("N0").Replace(",",".") + "</td>";
            double TienCuoc = (table.Rows[i]["TienCuoc"].ToString() == "" ? 0 : double.Parse(table.Rows[i]["TienCuoc"].ToString()));
            html += "       <td style='text-align:center;vertical-align: inherit;'>" + TienCuoc.ToString("N0").Replace(",", ".") + "</td>";
            double PhuPhi = (table.Rows[i]["PhuPhi"].ToString() == "" ? 0 : double.Parse(table.Rows[i]["PhuPhi"].ToString()));
            html += "       <td style='text-align:center;vertical-align: inherit;'>" + PhuPhi.ToString("N0").Replace(",", ".") + "</td>";
            html += "       <td style='text-align:center;vertical-align: inherit;'>" + (TienHang + TienCuoc + PhuPhi).ToString("N0").Replace(",", ".") + "</td>";
            //if (tbCTDH.Rows.Count > 0)
            //{
            //    if (tbCTDH.Rows[0]["TongSoLuong"].ToString() != "")
            //    {
            //        html += "<td style='text-align:center;vertical-align: inherit;'>" + tbCTDH.Rows[0]["TongSoLuong"].ToString() + "</td>";
            //        TongSoLuong += int.Parse(tbCTDH.Rows[0]["TongSoLuong"].ToString());
            //    }
            //    else
            //        html += "<td style='text-align:center;vertical-align: inherit;'>0</td>";
            //    if (tbCTDH.Rows[0]["TongTienHang"].ToString() != "")
            //    {
            //        html += "<td style='text-align:center;vertical-align: inherit;'>" + double.Parse(tbCTDH.Rows[0]["TongTienHang"].ToString()).ToString("#,##").Replace(",", ".") + "</td>";
            //        TongTienHang += double.Parse(tbCTDH.Rows[0]["TongTienHang"].ToString());
            //    }
            //    else
            //        html += "<td style='text-align:center;vertical-align: inherit;'>0</td>";
            //    if (tbCTDH.Rows[0]["TongTienCuoc"].ToString() != "")
            //    {
            //        html += "<td style='text-align:center;vertical-align: inherit;'>" + double.Parse(tbCTDH.Rows[0]["TongTienCuoc"].ToString()).ToString("#,##").Replace(",", ".") + "</td>";
            //        TongTienCuoc += double.Parse(tbCTDH.Rows[0]["TongTienCuoc"].ToString());
            //    }
            //    else
            //        html += "<td style='text-align:center;vertical-align: inherit;'>0</td>";
            //}
            //else
            //{
            //    html += "<td></td>";
            //    html += "<td></td>";
            //    html += "<td></td>";
            //}
            
            html += "   </tr>";

            html += "   <tr hidden id='tr_" + idDonHang + "' style='background: #eeeeee;'>";
            html += "       <td colspan='13' style='border: 1px solid;'>";
            html += "           <div>";
            html += "               <ul class='nav nav-tabs' style='margin:inherit; border:none'>";
            html += "                   <li class='active'><a data-toggle='tab' href='#HoaDon1_" + idDonHang + "'> <span style='padding-right:12px;font-weight:bold'> THÔNG TIN </span></a>    </li>";
            html += "               </ul>";
            html += "           </div>";
            html += "           <div class='tab-content'>";
            html += "               <div id='HoaDon1_" + idDonHang + "' class='tab-pane fade in active' style='background-color:white;padding: 10px;'>";
            html += "                   <div class='container-fluid'>";
            html += "                       <div class='span4'>";
            html += "                           <div class='container-fluid'>";
            html += "                               <div class='row-fluid FieldHienThi'><div class='span5'>Mã đơn hàng:</div><div class='span7'><b>" + table.Rows[i]["MaDonHang"].ToString() + "</b></div></div>";
            html += "                               <div class='row-fluid FieldHienThi'><div class='span5'>Ngày lập:</div><div class='span7'><b>" + DateTime.Parse(table.Rows[i]["NgayLap"].ToString()).ToString("dd/MM/yyyy") + "</b></div></div>";
            html += "                               <div class='row-fluid FieldHienThi'><div class='span5'>Tên người gửi:</div><div class='span7'><b>" + table.Rows[i]["TenKhachHang"].ToString() + "</b></div></div>";
            html += "                               <div class='row-fluid FieldHienThi'><div class='span5'>Số điện thoại:</div><div class='span7'><b>" + table.Rows[i]["SoDienThoai"].ToString() + "</b></div></div>";
            html += "                               <div class='row-fluid FieldHienThi'><div class='span5'>Ngày dự kiến giao:</div><div class='span7'><b>" + (table.Rows[i]["ThoiDiemDuKienGiao"].ToString() == "" ? "" : DateTime.Parse(table.Rows[i]["ThoiDiemDuKienGiao"].ToString()).ToString("dd/MM/yyyy HH:mm")) + "</b></div></div>";
            html += "                               <div class='row-fluid FieldHienThi'><div class='span5'>Gói dịch vụ:</div><div class='span7'><b>" + table.Rows[i]["GoiDichVu"].ToString() + "</b></div></div>";
            html += "                           </div>";
            html += "                       </div>";
            html += "                       <div class='span5'>";
            html += "                           <div class='container-fluid'>";
            html += "                               <div class='row-fluid FieldHienThi'><div class='span4'>Tên người nhận:</div><div class='span8'><b>" + table.Rows[i]["NguoiNhan"].ToString() + "</b></div></div>";
            html += "                               <div class='row-fluid FieldHienThi'><div class='span4'>SĐT người nhận:</div><div class='span8'><b>" + table.Rows[i]["SoDienThoaiNguoiNhan"].ToString() + "</b></div></div>";
            html += "                               <div class='row-fluid FieldHienThi'><div class='span4'>Địa chỉ:</div><div class='span8'><b>" + table.Rows[i]["DiaChiNguoiNhan"].ToString() + "</b></div></div>";
            html += "                               <div class='row-fluid FieldHienThi'><div class='span4'>Tỉnh/Huyện:</div><div class='span8'><b>" + StaticData.getField("tb_Huyen", "TenHuyen", "idHuyen", table.Rows[i]["idHuyen"].ToString()) + " - " +  StaticData.getField("tb_Tinh", "TenTinh", "idTinh", table.Rows[i]["idTinh"].ToString()) + "</b></div></div>";
            html += "                               <div class='row-fluid FieldHienThi'><div class='span4'>Người trả:</div><div class='span8'><b>" + (table.Rows[i]["NguoiNhanTra"].ToString() == "True" ? "Người nhận" : "Người gửi") + "</b></div></div>";
            html += "                               <div class='row-fluid FieldHienThi'><div class='span4'>Nhân viên giao:</div><div class='span8'><b>" + StaticData.getField("tb_NguoiDung", "HoTen", "idNguoiDung", table.Rows[i]["idNguoiDung"].ToString()) + " " + StaticData.getField("tb_NguoiDung", "SoDienThoai", "idNguoiDung", table.Rows[i]["idNguoiDung"].ToString()) + "</b></div></div>";
            html += "                           </div>";
            html += "                       </div>";
            html += "                       <div class='span3'>";
            html += "                           <div class='container-fluid'>";
            html += "                               <div class='row-fluid FieldHienThi'><div class='span12'>Ghi chú:</div></div>";
            html += "                               <div class='row-fluid FieldHienThi' style='border: solid 1px #ccd0d0;min-height: 153px;'><div class='span12'><p style='color:#818080;padding-left: 10px;'>" + table.Rows[i]["GhiChu"].ToString() + "</p></div></div>";
            html += "                           </div>";
            html += "                       </div>";

            //html += "                       <div class='col-md-12'>";
            //html += "                           <div class='container-fluid'>";
            //html += "                               <div class='row-fluid FieldHienThi'><div class='span12'>Danh sách sản phẩm:</div></div>";
            //html += "                               <div id='dvChiTiet'></div>";
            //html += "                           </div>";
            //html += "                       </div>";
            html += "                       <div class='container-fluid' >";
            html += "                           <div class='span12 CanhPhai' style='padding-top: 10px;'>";
            if (mQuyen.ToUpper() != "NVVP" && mQuyen.ToUpper() != "KH" && mQuyen.ToUpper() != "NVGN")
            {
                html += "           <a class='btn btn-default' title='In đơn hàng' onclick='PrinfHoaDon(\"" + table.Rows[i]["idDonHang"].ToString() + "\")'><i class='fa fa-print'></i><sapn> In đơn hàng</span></a>";
                html += "           <a class='btn btn-primary' title='Sửa đơn hàng' onclick='window.location=\"QuanLyDonhang-CapNhat.aspx?Page=" + Page.ToString() + "&idDonHang=" + table.Rows[i]["idDonHang"].ToString() + "\"'><i class='fa fa-edit'></i><span> Cập nhật</span></a>";
                html += "           <a class='btn btn-danger' title='Xóa đơn hàng' onclick='DeleteDonHang(\"" + table.Rows[i]["idDonHang"].ToString() + "\")'><i class='fa fa-trash'></i><span> Xóa</span></a>";

            }
            else
            {
                if (mQuyen.ToUpper() == "NVVP" || mQuyen.ToUpper() == "NVGN" || mQuyen.ToUpper() == "KH")
                {
                    html += "       <a style='cursor:pointer' title='Sửa đơn hàng' onclick='window.location=\"QuanLyDonhang-CapNhat.aspx?Page=" + Page.ToString() + "&idDonHang=" + table.Rows[i]["idDonHang"].ToString() + "\"'><i class='fa fa-edit'></i><span> Cập nhật</span></a>";
                }
            }
            //html += "                           <a class='btn btn-primary' href='DanhMucKhachHang-CapNhat.aspx?idKhachHang=" + idDonHang + "&Page=" + Page + "'><i class='fa fa-pencil-square-o'></i><span> Cập nhật</span></a>";
            //html += "                           <a class='btn btn-danger' onclick='DeleteKhachHang(\"" + table.Rows[i]["idDonHang"].ToString() + "\")'><i class='fa fa-trash'></i><span style='margin-left: 5px;'>Xóa</span></a>";
            html += "                           </div>";
            html += "                       </div>";
            html += "                   </div>";
            html += "               </div>";
            html += "           </div>";
            html += "       </td>";
            html += "   </tr>";

        }
        html += "  </table><table >   <tr>";
        html += "       <td colspan='21' class='footertable'>";
        string url = "QuanLyDonHang.aspx?";
        if (sTuNgay != "")
            url += "TuNgay=" + sTuNgay + "&";
        if (sDenNgay != "")
            url += "DenNgay=" + sDenNgay + "&";
        //if (sMaDonHang != "")
        //    url += "MaDonHang=" + sMaDonHang + "&";
        if (sIdNguoiDung != "" && sIdNguoiDung != "0")
            url += "idNguoiDung=" + sIdNguoiDung + "&";
        if (sIdKhachHang != "" && sIdKhachHang != "0")
            url += "idKhachHang=" + sIdKhachHang + "&";
        if (sTen != "")
            url += "Ten=" + sTen + "&";
        if (sMaTinhTrang != "" && sMaTinhTrang != "0")
            url += "MaTinhTrang=" + sMaTinhTrang + "&";
        if (sIdTinh != "" && sIdTinh != "0")
            url += "idTinh=" + sIdTinh + "&";
        if (sIdHuyen != "" && sIdTinh != "0")
            url += "idHuyen=" + sIdTinh + "&";
        //if (sDiaChiNguoiNhan != "")
        //    url += "DiaChiNguoiNhan=" + sDiaChiNguoiNhan + "&";
        if (sNgayDuKienGiao != "")
            url += "NgayDuKienGiao=" + sNgayDuKienGiao + "&";
        if (sIsDaHoanThanh != "")
            url += "isDaHoanThanh=" + sIsDaHoanThanh + "&";
        url += "Page=";
        html += StaticData.PhanTrang(url, txtFistPage, txtPage1, txtPage2, txtPage3, txtPage4, txtPage5, txtLastPage, Page);
        html += "    </td></tr>";
        html += "     </table></center>";

        //html += "                       <div class='col-md-12'>";
        //html += "                           <div class='form-wrapper'>";
        //html += "                               <div class='table-responsive'><table class='table table-borderless'>";
        //html += "                               <tr><td style='width: 83.7%;text-align:right'><b>Tổng số hóa đơn :</b></td><td style='text-align:center'><b> " + SoHoaDon.ToString() + "</b></td></tr>";
        //html += "                               <tr><td style='text-align:right'><b>Tổng tiền hàng :</b></td><td style='text-align:center'><b> " + TongTienHang.ToString("N0").Replace(",", ".") + "</b></td></tr>";
        //html += "                               <tr><td style='text-align:right'><b>Tổng tiền cước :</b></td><td style='text-align:center'><b> " + TongTienCuoc.ToString("N0").Replace(",", ".") + "</b></td></tr>";
        //html += "                               <tr><td style='text-align:right'><b>Tổng phụ phí :</b></td><td style='text-align:center'><b> " + TongPhuPhi.ToString("N0").Replace(",", ".") + "</b></td></tr>";
        //html += "                               <tr><td style='text-align:right'><b>Tổng tiền :</b></td><td style='text-align:center'><b> " + (TongTienHang + TongPhuPhi + TongTienCuoc).ToString("N0").Replace(",", ".") + "</b></td></tr>";
        //html += "                               </table></div>";
        //html += "                           </div>";
        //html += "                       </div>";

        //html += "<div style='text-align:right;font-size: 20px;'><b>Tổng số hóa đơn:</b> " + SoHoaDon.ToString() + "</div>";
        //html += "<div style='text-align:right;font-size: 20px;'><b>Tổng tiền hàng:</b> " + TongTienHang.ToString("#,##").Replace(",", ".") + "</div>";
        //html += "<div style='text-align:right;font-size: 20px;'><b>Tổng tiền cước:</b> " + TongTienCuoc.ToString("#,##").Replace(",", ".") + "</div>";
        dvDanhSachDonHang.InnerHtml = html;
    }
    protected void btTimKiem_Click(object sender, EventArgs e)
    {
        //string TuNgay = txtTuNgay.Value.Trim();
        //string DenNgay = txtDenNgay.Value.Trim();
        //string idKhachHang = hdIdKhachHang.Value.Trim();
        //string Ten = txtTenKhachHang.Value.Trim();
        ////string DiaChiNguoiNhan = txtDiaChiNguoiNhan.Value.ToString();
        //string NgayDuKienGiao = txtNgayDuKienGiao.Value.Trim();
        //string url = "QuanLyDonHang.aspx?";
        //if (TuNgay != "")
        //    url += "TuNgay=" + TuNgay + "&";
        //if (DenNgay != "")
        //    url += "DenNgay=" + DenNgay + "&";
        ////if (MaDonHang != "")
        ////    url += "MaDonHang=" + MaDonHang + "&";
        ////if (DiaChiNguoiNhan != "")
        ////    url += "DiaChiNguoiNhan=" + DiaChiNguoiNhan + "&";
        //if (NgayDuKienGiao != "")
        //    url += "NgayDuKienGiao=" + NgayDuKienGiao + "&";
        //Response.Redirect(url);
    }
    protected void btXemTatCa_Click(object sender, EventArgs e)
    {
        string url = "QuanLyDonHang.aspx";
        Response.Redirect(url);
    }
    
}