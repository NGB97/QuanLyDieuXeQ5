using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DanhMuc_LoaiCuoc : System.Web.UI.Page
{
    string sTenLoai = "";
    string sMaLoai = "";

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
    int PageSize = 10;
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
                if (mQuyen.ToUpper() != "ADMIN")
                {
                    Response.Redirect("../Home/DangNhap.aspx");
                }
            }
            try
            {
                if (Request.QueryString["TenHangHoa"].Trim() != "")
                {
                    sTenLoai = Request.QueryString["TenHangHoa"].Trim();
                    txtTen.Value = sTenLoai;
                }
            }
            catch { }
            try
            {
                if (Request.QueryString["MaHangHoa"].Trim() != "")
                {
                    sMaLoai = Request.QueryString["MaHangHoa"].Trim();
                    txtMa.Value = sMaLoai;
                }
            }
            catch { }
            LoadLoaiSanPham();
        }
    }
    #region paging
    private void SetPage()
    {
        string sql = "select count(idHangHoa) from tb_HangHoa where '1'='1'";
        if (sTenLoai != "")
            sql += " and TenHangHoa like N'%" + sTenLoai + "%'";
        if (sMaLoai != "")
            sql += " and MaHangHoa like N'%" + sMaLoai + "%'";
        DataTable tbTotalRows = Connect.GetTable(sql);
        int TotalRows = int.Parse(tbTotalRows.Rows[0][0].ToString());
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
    private void LoadLoaiSanPham()
    {
        string sql = "";
        sql += @"select * from
            (
	            SELECT ROW_NUMBER() OVER
                  (
                        ORDER BY idHangHoa
                  )AS RowNumber
	              ,*
                  FROM tb_HangHoa where 1 = 1
            ";
        if (sTenLoai != "")
            sql += " and TenHangHoa like N'%" + sTenLoai + "%'";
        if (sMaLoai != "")
            sql += " and MaHangHoa like N'%" + sMaLoai + "%'";
        sql += ") as tb1 WHERE RowNumber BETWEEN (" + Page + " - 1) * " + PageSize + " + 1 AND (((" + Page + " - 1) * " + PageSize + " + 1) + " + PageSize + ") - 1";


        DataTable table = Connect.GetTable(sql);
        //txtNoiDung.InnerHtml = table.Rows[0]["NoiDung"].ToString();
        SetPage();
        string html = @"<center><table class='table table-bordered table-striped' style='width:100%;radius:2px;'>
                            <tr>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    STT
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;text-align:left'>
                                    Mã Hàng Hóa
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;text-align:left'>
                                    Tên Hàng Hóa
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Giá cước
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;' >
                                    Chức năng
                                </th>
                            </tr>";
        for (int i = 0; i < table.Rows.Count; i++)
        {
            html += "       <tr>";
            html += "       <td style='text-align:center;vertical-align: inherit;'>" + (((Page - 1) * PageSize) + i + 1).ToString() + "</td>";
            html += "       <td>" + table.Rows[i]["MaHangHoa"].ToString() + "</td>";
            html += "       <td>" + table.Rows[i]["TenHangHoa"].ToString() + "</td>";
            html += "       <td style='text-align:center;vertical-align: inherit;'>" + (table.Rows[i]["GiaCuoc"].ToString() == "" ? "" : double.Parse(table.Rows[i]["GiaCuoc"].ToString()).ToString("N0").Replace(",", ".")) + "</td>";
            
            html += "       <td style='text-align:center;vertical-align: inherit;font-size:20px;white-space: nowrap;'>";
            html += "           <a href='#' onclick='window.location=\"DanhMucHangHoa-CapNhat.aspx?Page=" + Page.ToString() + "&idHangHoa=" + table.Rows[i]["idHangHoa"].ToString() + "\"'><i class='fa fa-edit'></i></a>";
            html += "           <a href='#' onclick='DeleteKho(\"" + table.Rows[i]["idHangHoa"].ToString() + "\")'> <i class='fa fa-trash'></i></a>";
            html += "       </td>";

            html += "       </tr>";

        }
        html += "  </table><table >   <tr>";
        html += "       <td colspan='17' class='footertable'>";
        string url = "DanhMucHangHoa.aspx?";
        if (sTenLoai != "")
            url += "TenHangHoa=" + sTenLoai + "&";
        if (sMaLoai != "")
            url += "MaHangHoa=" + sMaLoai + "&";
        url += "Page=";
        html += StaticData.PhanTrang(url, txtFistPage, txtPage1, txtPage2, txtPage3, txtPage4, txtPage5, txtLastPage, Page);
        html += "    </td></tr><tr><td colspan='17'>&nbsp;</td></tr>";
        html += "     </table></center>";
        dvDanhSachKho.InnerHtml = html;
    }
    protected void btTimKiem_Click(object sender, EventArgs e)
    {
        string MaLoai = txtMa.Value.Trim();
        string TenLoai = txtTen.Value.Trim();
        string url = "DanhMucHangHoa.aspx?";
        if (MaLoai != "")
            url += "MaHangHoa=" + MaLoai + "&";
        if (TenLoai != "")
            url += "TenHangHoa=" + TenLoai + "&";
        Response.Redirect(url);
    }
    protected void btXemTatCa_Click(object sender, EventArgs e)
    {
        string url = "DanhMucHangHoa.aspx";
        Response.Redirect(url);
    }
}