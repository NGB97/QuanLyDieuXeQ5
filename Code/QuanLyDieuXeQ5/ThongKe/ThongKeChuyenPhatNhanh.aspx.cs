using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using ClosedXML.Excel;

public partial class ThongKe_ThongKeNhanVienGiaoHang : System.Web.UI.Page
{
    int SoHoaDon = 0;
    double TongSoLuong = 0;
    double TongTienHang = 0;
    double TongTienCuoc = 0;
    double TongPhuPhi = 0;

    string sTuNgay = "";
    string sDenNgay = "";
    string sIdNhanVien = "";
    string sNguoiGui = "";
    string _TinhTrang = "";

    string mTenDangNhap = "";
    string mQuyen = "";
    string mIdNguoiDung = "";

    string txtFistPage = "1";
    string txtPage1 = "";
    string txtPage2 = "";
    string txtPage3 = "";
    string txtPage4 = "";
    string txtPage5 = "";
    string txtLastPage = "";
    int Page = 0;
    int MaxPage = 0;
    int PageSize = 70;
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
            if (Request.Cookies["QuanLyCongNoAnhKiet_Login"] != null && Request.Cookies["QuanLyCongNoAnhKiet_Login"].Value.Trim() != "")
            {
                mTenDangNhap = Request.Cookies["QuanLyCongNoAnhKiet_Login"].Value.Trim();
                mIdNguoiDung = StaticData.getField("tb_NguoiDung", "idNguoiDung", "TenDangNhap", mTenDangNhap);
                mQuyen = MyStaticData.GetMaQuyen(mTenDangNhap);
                if (mQuyen.ToUpper() != "ADMIN" && mQuyen.ToUpper() != "QTWS")
                {
                    Response.Redirect("../Home/DangNhap.aspx");
                }
            }
            LoadNhanVien();
            LoadTinhTrang();
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
            try
            {
                if (Request.QueryString["idNhanVien"].Trim() != "")
                {
                    sIdNhanVien = Request.QueryString["idNhanVien"].Trim();
                    slNhanVien.Value = sIdNhanVien;
                }
            }
            catch { }
            try
            {
                if (Request.QueryString["NguoiGui"].Trim() != "")
                {
                    sNguoiGui = Request.QueryString["NguoiGui"].Trim();
                    txtNguoiGui.Value = sNguoiGui;
                }
            }
            catch { }
            try
            {
                if (Request.QueryString["TinhTrang"].Trim() != "")
                {
                    _TinhTrang = Request.QueryString["TinhTrang"].Trim();
                    slTinhTrang.Value = _TinhTrang;
                }
            }
            catch { }
            LoadDonHang();
        }
    }
    private void LoadNhanVien()
    {
        string strSql = "select * from tb_NguoiDung where MaQuyen='NVGN'";
        slNhanVien.DataSource = Connect.GetTable(strSql);
        slNhanVien.DataTextField = "HoTen";
        slNhanVien.DataValueField = "idNguoiDung";
        slNhanVien.DataBind();
        slNhanVien.Items.Add(new ListItem("-- Chọn nhân viên --", "0"));
        slNhanVien.Items.FindByText("-- Chọn nhân viên --").Selected = true;
    }
    private void LoadTinhTrang()
    {
        string strSql = "select * from tb_TinhTrang";
        slTinhTrang.DataSource = Connect.GetTable(strSql);
        slTinhTrang.DataTextField = "TenTinhTrang";
        slTinhTrang.DataValueField = "MaTinhTrang";
        slTinhTrang.DataBind();
        slTinhTrang.Items.Add(new ListItem("-- Chọn tình trạng --", "0"));
        slTinhTrang.Items.FindByText("-- Chọn tình trạng --").Selected = true;
    }
    
    #region paging
    private void SetPage()
    {
        string sql = @"select count(idDonHang), isNull(Sum(ChuyenPhatNhanh),0) from 
        (select * from tb_DonHang where ChuyenPhatNhanh is not null and ChuyenPhatNhanh !=0) as T where  '1'='1'  ";
        if (sIdNhanVien != "")
            sql += " and tb_NguoiDung.idNguoiDung = '" + sIdNhanVien + "'";
        if (sNguoiGui != "")
            sql += " and idKhachHang in (select idKhachHang from tb_KhachHang where TenKhachHang like N'%" + sNguoiGui + "%')";
        if (sTuNgay != "")
            sql += " and NgayLap >= '" + StaticData.ConvertDDMMtoMMDD(sTuNgay) + " 00:00:00'";
        if (sDenNgay != "")
            sql += " and NgayLap <= '" + StaticData.ConvertDDMMtoMMDD(sDenNgay) + " 23:59:59'";
        if (_TinhTrang != "")
            sql += " and MaTinhTrang like N'" + _TinhTrang + "'";
        DataTable tbTotalRows = Connect.GetTable(sql);
        int TotalRows = int.Parse(tbTotalRows.Rows[0][0].ToString());
        TongTienHang = double.Parse(tbTotalRows.Rows[0][1].ToString());
        //TongTienCuoc = double.Parse(tbTotalRows.Rows[0][2].ToString());
        //TongPhuPhi = double.Parse(tbTotalRows.Rows[0][3].ToString());
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
	              ,*
                  FROM (select * from tb_DonHang where ChuyenPhatNhanh is not null and ChuyenPhatNhanh !=0) as T where  1=1 
            ";
        if (sIdNhanVien != "")
            sql += " and tb_NguoiDung.idNguoiDung = '" + sIdNhanVien + "'";
        if (sNguoiGui != "")
            sql += " and idKhachHang in (select idKhachHang from tb_KhachHang where TenKhachHang like N'%" + sNguoiGui + "%')";
        if (sTuNgay != "")
            sql += " and NgayLap >= '" + StaticData.ConvertDDMMtoMMDD(sTuNgay) + " 00:00:00'";
        if (sDenNgay != "")
            sql += " and NgayLap <= '" + StaticData.ConvertDDMMtoMMDD(sDenNgay) + " 23:59:59'";
        if (_TinhTrang != "")
            sql += " and MaTinhTrang like N'" + _TinhTrang + "'";
        sql += ") as tb1 WHERE RowNumber BETWEEN (" + Page + " - 1) * " + PageSize + " + 1 AND (((" + Page + " - 1) * " + PageSize + " + 1) + " + PageSize + ") - 1";


        DataTable table = Connect.GetTable(sql);
        //txtNoiDung.InnerHtml = table.Rows[0]["NoiDung"].ToString();
        SetPage();
        string html = @"<center><table class='table table-bordered table-hover dataTable'>
                            <tr>
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
                                    SDT Người Gửi
                                </th>
                            <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Chi Nhánh Gửi
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Người Nhận
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    SDT Người Nhận
                                </th>
                            <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Chi Nhánh Nhận
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Tiền Chuyển Phát
                                </th>
                             
                            </tr>";
        html += "           <tr style='background: #ff7423;'>";
        html += "               <td colspan='9' style='text-align:center;vertical-align: inherit;'><b>Tổng cộng nhân viên giao hàng (" + SoHoaDon + " đơn)</b></td>";
        html += "               <td style='text-align:center;vertical-align: inherit;'><b>" + TongTienHang.ToString("N0").Replace(",", ".") + "</b></td>";
        //html += "               <td style='text-align:center;vertical-align: inherit;'><b>" + TongTienCuoc.ToString("N0").Replace(",", ".") + "</b></td>";
        //html += "               <td style='text-align:center;vertical-align: inherit;'><b>" + TongPhuPhi.ToString("N0").Replace(",", ".") + "</b></td>";
     //   html += "               <td style='text-align:center;vertical-align: inherit;'><b>" + (TongTienHang + TongTienCuoc + TongPhuPhi).ToString("N0").Replace(",", ".") + "</b></td>";
        html += "           </tr>";
        for (int i = 0; i < table.Rows.Count; i++)
        {
            string sqlCTDH = "select TongSoLuong=sum(isnull(SoLuong,0)),TongTienHang=sum(isnull(TienHang,0)),TongTienCuoc=sum(isnull(TienCuoc,0)) from tb_ChiTietDonHang where idDonHang='" + table.Rows[i]["idDonHang"].ToString() + "'";
            DataTable tbCTDH = Connect.GetTable(sqlCTDH);
            html += "       <tr>";
            html += "       <td>" + (((Page - 1) * PageSize) + i + 1).ToString() + "</td>";
            html += "       <td>" + table.Rows[i]["MaDonHang"].ToString() + "</td>";
            html += "       <td>" + DateTime.Parse(table.Rows[i]["NgayLap"].ToString()).ToString("dd/MM/yyyy") + "</td>";
            html += "       <td>" + table.Rows[i]["NguoiGui"].ToString() + "</td>";
            html += "       <td>" + table.Rows[i]["SDTNguoiGui"].ToString() + "</td>";
          //  html += "       <td>" + StaticData.getField("tb_ChiNhanh", "HoTen", "idNguoiDung", table.Rows[i]["idNguoiDung"].ToString()) + "</td>";
            html += "       <td style='text-align:center;vertical-align: inherit;'>" + StaticData.getField("tb_ChiNhanh", "TenChiNhanh", "IDChiNhanh", table.Rows[i]["idChiNhanhGui"].ToString()) + "</td>";
            html += "       <td>" + StaticData.getField("tb_KhachHang", "TenKhachHang", "idKhachHang", table.Rows[i]["idKhachHang"].ToString()) + "</td>";
            html += "       <td>" + StaticData.getField("tb_KhachHang", "SoDienThoai", "idKhachHang", table.Rows[i]["idKhachHang"].ToString()) + "</td>";
            html += "       <td style='text-align:center;vertical-align: inherit;'>" + StaticData.getField("tb_ChiNhanh", "TenChiNhanh", "IDChiNhanh", table.Rows[i]["idChiNhanhNhan"].ToString()) + "</td>";
          //  html += "       <td>" + StaticData.getField("tb_TinhTrang", "TenTinhTrang", "MaTinhTrang", table.Rows[i]["MaTinhTrang"].ToString()) + "</td>";
            double TienHang = (table.Rows[i]["ChuyenPhatNhanh"].ToString() == "" ? 0 : double.Parse(table.Rows[i]["ChuyenPhatNhanh"].ToString()));
            html += "       <td style='text-align:center;vertical-align: inherit;'>" + TienHang.ToString("N0").Replace(",", ".") + "</td>";
            //double TienCuoc = (table.Rows[i]["TienCuoc"].ToString() == "" ? 0 : double.Parse(table.Rows[i]["TienCuoc"].ToString()));
            //html += "       <td style='text-align:center;vertical-align: inherit;'>" + TienCuoc.ToString("N0").Replace(",", ".") + "</td>";
            //double PhuPhi = (table.Rows[i]["PhuPhi"].ToString() == "" ? 0 : double.Parse(table.Rows[i]["PhuPhi"].ToString()));
            //html += "       <td style='text-align:center;vertical-align: inherit;'>" + PhuPhi.ToString("N0").Replace(",", ".") + "</td>";
            //html += "       <td style='text-align:center;vertical-align: inherit;'>" + (TienHang + TienCuoc + PhuPhi).ToString("N0").Replace(",", ".") + "</td>";
            //html += "       <td>" + StaticData.getField("tb_Kho", "TenKho", "idKho", table.Rows[i]["idKho"].ToString()) + "</td>";
            //if (tbCTDH.Rows.Count > 0)
            //{
            //    if (tbCTDH.Rows[0]["TongSoLuong"].ToString() != "")
            //    {
            //        html += "<td>" + tbCTDH.Rows[0]["TongSoLuong"].ToString() + "</td>";
            //        TongSoLuong += float.Parse(tbCTDH.Rows[0]["TongSoLuong"].ToString());
            //    }
            //    else
            //        html += "<td>0</td>";
            //    if (tbCTDH.Rows[0]["TongTienHang"].ToString() != "")
            //    {
            //        html += "<td>" + double.Parse(tbCTDH.Rows[0]["TongTienHang"].ToString()).ToString("#,##").Replace(",", ".") + "</td>";
            //        TongTienHang += float.Parse(tbCTDH.Rows[0]["TongTienHang"].ToString());
            //    }
            //    else
            //        html += "<td>0</td>";
            //    if (tbCTDH.Rows[0]["TongTienCuoc"].ToString() != "")
            //    {
            //        html += "<td>" + double.Parse(tbCTDH.Rows[0]["TongTienCuoc"].ToString()).ToString("#,##").Replace(",", ".") + "</td>";
            //        TongTienCuoc += float.Parse(tbCTDH.Rows[0]["TongTienCuoc"].ToString());
            //    }
            //    else
            //        html += "<td>0</td>";
            //}
            //else
            //{
            //    html += "<td></td>";
            //    html += "<td></td>";
            //    html += "<td></td>";
            //}
            
            html += "       </tr>";
        }
        html += "  </table><table >   <tr>";
        html += "       <td colspan='21' class='footertable'>";
        string url = "ThongKeNhanVienGiaoHang.aspx?";
        if (sIdNhanVien != "")
            url += "idNhanVien=" + sIdNhanVien + "&";
        if (sNguoiGui != "")
            url += "NguoiGui=" + sNguoiGui + "&";
        if (sTuNgay != "")
            url += "TuNgay=" + sTuNgay + "&";
        if (sDenNgay != "")
            url += "DenNgay=" + sDenNgay + "&";
        if (_TinhTrang != "")
            url += "TinhTrang=" + _TinhTrang + "&";
        url += "Page=";
        html += StaticData.PhanTrang(url, txtFistPage, txtPage1, txtPage2, txtPage3, txtPage4, txtPage5, txtLastPage, Page);
        html += "    </td></tr><tr><td colspan='17'>&nbsp;</td></tr>";
        html += "     </table></center>";
        //html += "<div style='text-align:right;font-size: 20px;'><b>Tổng số hóa đơn:</b> " + SoHoaDon.ToString() + "</div>";
        //html += "<div style='text-align:right;font-size: 20px;'><b>Tổng tiền hàng:</b> " + TongTienHang.ToString("#,##").Replace(",", ".") + "</div>";
        //html += "<div style='text-align:right;font-size: 20px;'><b>Tổng tiền cước:</b> " + TongTienCuoc.ToString("#,##").Replace(",", ".") + "</div>";
        dvDanhSachNhanVienThuTien.InnerHtml = html;
    }
    protected void btTimKiem_Click(object sender, EventArgs e)
    {
        string NguoiGui = txtNguoiGui.Value.Trim();
        string idNhanVien = slNhanVien.Value.Trim();
        string TuNgay = txtTuNgay.Value.Trim();
        string DenNgay = txtDenNgay.Value.Trim();
        string TinhTrang = slTinhTrang.Value.Trim();
        string url = "ThongKeNhanVienGiaoHang.aspx?";
        if (idNhanVien != "" && idNhanVien != "0")
            url += "idNhanVien=" + idNhanVien + "&";
        if (NguoiGui != "")
            url += "NguoiGui=" + NguoiGui + "&";
        if (TuNgay != "")
            url += "TuNgay=" + TuNgay + "&";
        if (DenNgay != "")
            url += "DenNgay=" + DenNgay + "&";
        if (TinhTrang != "" && TinhTrang != "0")
            url += "TinhTrang=" + TinhTrang + "&";

        Response.Redirect(url);
    }
    protected void btXemTatCa_Click(object sender, EventArgs e)
    {
        string url = "ThongKeNhanVienGiaoHang.aspx";
        Response.Redirect(url);
    }
    protected void btXuatExcel_Click(object sender, EventArgs e)
    {
        string sql = @"select * from
            (
	            SELECT ROW_NUMBER() OVER
                  (
                        ORDER BY idDonHang desc
                  )AS 'STT',nd.HoTen
	              ,dh.MaDonHang as 'Mã đơn hàng',convert(varchar,dh.NgayLap,103) as 'Ngày lập',kh.TenKhachHang as 'Người gửi',tt.TenTinhTrang as 'Tình trạng',isnull(dh.TienHang,0) as 'Tiền hàng', isnull(dh.TienCuoc,0) as 'Tiền cước', isnull(dh.PhuPhi,0) as 'Phụ phí', isnull(isnull(dh.TienHang,0)+isnull(dh.TienCuoc,0)+isnull(dh.PhuPhi,0),0) as 'Tổng tiền'
                  FROM tb_DonHang dh,tb_KhachHang kh,tb_NguoiDung nd,tb_TinhTrang tt where dh.idKhachHang = kh.idKhachHang and nd.idNguoiDung = dh.idNguoiDung and dh.MaTinhTrang like tt.MaTinhTrang) as tb";
        ExportToExcel(Connect.GetTable(sql));
    }
    private void ExportToExcel(DataTable dt)
    {
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(dt, "ThongKeDonHangCoNhanVienGiao");

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=ThongKeDonHangCoNhanVienGiao.xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
    }
}