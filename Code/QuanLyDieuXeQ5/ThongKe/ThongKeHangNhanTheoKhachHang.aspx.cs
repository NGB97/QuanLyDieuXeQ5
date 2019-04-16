using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using ClosedXML.Excel;

public partial class ThongKe_ThongKeNhanVienThuTien : System.Web.UI.Page
{
    int SoHoaDon = 0;
    double TongSoLuong = 0;
    double TongTienHang = 0;
    double TongTienCuoc = 0;
    double TongPhuPhi = 0;

    string sNguoiGui = "";

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
                if (mQuyen.ToUpper() != "ADMIN" && mQuyen.ToUpper() != "NVVP")
                {
                    Response.Redirect("../Home/DangNhap.aspx");
                }
            }

                 
            try
            {
                if (Request.QueryString["NguoiGui"].Trim() != "")
                {
                    sNguoiGui = Request.QueryString["NguoiGui"].Trim();
                    txtNguoiGui.Value = sNguoiGui;
                }
            }
            catch { }
            LoadDonHang();
        }
    }


    
    #region paging
    private void SetPage()
    {
        string sql = @" SELECT ROW_NUMBER() OVER
                  (
                        ORDER BY TongTien desc
                  )AS RowNumber, *
				  FROM (
				  SELECT
	              	COUNT(idKhachHang) AS SoDonHang,
				  ISNULL(SUM(TongCuoc),0) AS TongTien,		  
				  idKhachHang
                  FROM tb_DonHang
				  GROUP BY idKhachHang
					) AS T WHERE 1 = 1 ";

        if (sNguoiGui != "")
            sql += " and idKhachHang in (select idKhachHang from tb_KhachHang where TenKhachHang like N'%" + sNguoiGui + "%')";

        DataTable tbTotalRows = Connect.GetTable(sql);
        int TotalRows = tbTotalRows.Rows.Count;
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
                        ORDER BY TongTien desc
                  )AS RowNumber, *
				  FROM (
				  SELECT
	              	COUNT(idKhachHang) AS SoDonHang,
				  ISNULL(SUM(TongCuoc),0) AS TongTien,		  
				  idKhachHang
                  FROM tb_DonHang
				  GROUP BY idKhachHang
					) AS TB WHERE 1 = 1
            ";

        if (sNguoiGui != "")
            sql += " and idKhachHang in (select idKhachHang from tb_KhachHang where TenKhachHang like N'%" + sNguoiGui + "%')";

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
                                    NGƯỜI NHẬN / KHÁCH HÀNG
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    SỐ ĐƠN HÀNG
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    TỔNG TIỀN
                                </th>
                                
                            </tr>";
        for (int i = 0; i < table.Rows.Count; i++)
        {
   
            html += "       <tr>";
            html += "       <td style='text-align:center;vertical-align: inherit;' >" + (((Page - 1) * PageSize) + i + 1).ToString() + "</td>";

            html += "       <td style='text-align:center;vertical-align: inherit;' > <a href='../DanhMuc/DanhMucKhachHang-CapNhat.aspx?idKhachHang=" + table.Rows[i]["idKhachHang"].ToString() + "' >  " + StaticData.getField("tb_KhachHang", "TenKhachHang", "idKhachHang", table.Rows[i]["idKhachHang"].ToString()) + " </a></td>";
            double SoDonHang = (table.Rows[i]["SoDonHang"].ToString() == "" ? 0 : double.Parse(table.Rows[i]["SoDonHang"].ToString()));
            html += "       <td style='text-align:center;vertical-align: inherit;'>" + SoDonHang.ToString("N0").Replace(",", ".") + "</td>";
            double TongTien = (table.Rows[i]["TongTien"].ToString() == "" ? 0 : double.Parse(table.Rows[i]["TongTien"].ToString()));
            html += "       <td style='text-align:center;vertical-align: inherit;'>" + TongTien.ToString("N0").Replace(",", ".") + "</td>";
            
            html += "       </tr>";
        }
        html += "  </table><table >   <tr>";
        html += "       <td colspan='21' class='footertable'>";
        string url = "ThongKeNguoiGui.aspx?";

        if (sNguoiGui != "")
            url += "NguoiGui=" + sNguoiGui + "&";

        url += "Page=";
        html += StaticData.PhanTrang(url, txtFistPage, txtPage1, txtPage2, txtPage3, txtPage4, txtPage5, txtLastPage, Page);
        html += "    </td></tr><tr><td colspan='17'>&nbsp;</td></tr>";
        html += "     </table></center>";
        //html += "<div style='text-align:right;font-size: 20px;'><b>Tổng số hóa đơn:</b> " + SoHoaDon.ToString() + "</div>";
        //html += "<div style='text-align:right;font-size: 20px;'><b>Tổng tiền hàng:</b> " + TongTienHang.ToString("#,##").Replace(",", ".") + "</div>";
        //html += "<div style='text-align:right;font-size: 20px;'><b>Tổng tiền cước:</b> " + TongTienCuoc.ToString("#,##").Replace(",", ".") + "</div>";
        dvThongKeNguoiGui.InnerHtml = html;
    }
    protected void btTimKiem_Click(object sender, EventArgs e)
    {
        string NguoiGui = txtNguoiGui.Value.Trim();

        string url = "ThongKeNguoiGui.aspx?";

        if (NguoiGui != "")
            url += "NguoiGui=" + NguoiGui + "&";


        Response.Redirect(url);
    }
    protected void btXemTatCa_Click(object sender, EventArgs e)
    {
        string url = "ThongKeNguoiGui.aspx";
        Response.Redirect(url);
    }
    protected void Reload_Click(object sender, EventArgs e)
    {
        Response.Redirect("../ThongKe/ThongKeNguoiGui.aspx");
    }
    private void ExportToExcel(DataTable dt)
    {
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(dt, "ThongKeDonHang");

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=ThongKeDonHang.xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
    }
    protected void btXuatExcel_Click(object sender, EventArgs e)
    {
        string sql = @"select tb1.RowNumber 'STT', tb_KhachHang.TenKhachHang 'NGƯỜI GỬI / KHÁCH HÀNG',tb1.SoDonHang 'SỐ ĐƠN HÀNG',PARSENAME(convert(varchar,convert(money,isnull(tb1.TongTien,0)),1),2 ) 'TỔNG TIỀN' from
            (
	             SELECT ROW_NUMBER() OVER
                  (
                        ORDER BY TongTien desc
                  )AS RowNumber, *
				  FROM (
				  SELECT
	              	COUNT(idKhachHang) AS SoDonHang,
				  ISNULL(SUM(TienHang + TienCuoc + PhuPhi),0) AS TongTien,		  
				  idKhachHang
                  FROM tb_DonHang
				  GROUP BY idKhachHang
					) AS TB WHERE 1 = 1) as tb1,tb_KhachHang where tb1.idKhachHang = tb_KhachHang.idKhachHang";
        ExportToExcel(Connect.GetTable(sql));
    }
}