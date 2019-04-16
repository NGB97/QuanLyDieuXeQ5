using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DanhMuc_DanhMucNguoiDung : System.Web.UI.Page
{
    string sHoTen = "";
    string sMaQuyen = "";
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
    int PageSize = 5;

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
            LoadQuyen();
            try
            {
                if (Request.QueryString["HoTen"].Trim() != "")
                {
                    sHoTen = Request.QueryString["HoTen"].Trim();
                    txtHoTen.Value = sHoTen;
                }
            }
            catch { }
            try
            {
                if (Request.QueryString["MaQUyen"].Trim() != "")
                {
                    sMaQuyen = Request.QueryString["MaQUyen"].Trim();
                    slQuyen.Value = sMaQuyen;
                }
            }
            catch { }

            LoadNguoiDung();
        }
    }
    private void LoadQuyen()
    {
        string strSql = "select * from tb_Quyen";
        slQuyen.DataSource = Connect.GetTable(strSql);
        slQuyen.DataTextField = "TenQuyen";
        slQuyen.DataValueField = "MaQuyen";
        slQuyen.DataBind();
        slQuyen.Items.Add(new ListItem("-- Tất cả --", "0"));
        slQuyen.Items.FindByText("-- Tất cả --").Selected = true;
    }
    #region paging
    private void SetPage()
    {
        string sql = "select count(idNguoiDung) from tb_NguoiDung where '1'='1'";
        if (sHoTen != "")
            sql += " and HoTen like N'%" + sHoTen + "%'";
        if (sMaQuyen != "" && sMaQuyen != "0")
            sql += " and MaQuyen like '%" + sMaQuyen + "%'";
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
    private void LoadNguoiDung()
    {
        string sql = "";
        sql += @"select * from
            (
	            SELECT ROW_NUMBER() OVER
                  (
                        ORDER BY idNguoiDung desc
                  )AS RowNumber
	              ,*
                  FROM tb_NguoiDung where 1 = 1
            ";
        if (sHoTen != "")
            sql += " and HoTen like N'%" + sHoTen + "%'";
        if (sMaQuyen != "" && sMaQuyen != "0")
            sql += " and MaQuyen like '%" + sMaQuyen + "%'";
        sql += ") as tb1 WHERE RowNumber BETWEEN (" + Page + " - 1) * " + PageSize + " + 1 AND (((" + Page + " - 1) * " + PageSize + " + 1) + " + PageSize + ") - 1";


        DataTable table = Connect.GetTable(sql);
        //txtNoiDung.InnerHtml = table.Rows[0]["NoiDung"].ToString();
        SetPage();
        string html = @"<center><table class='table table-bordered table-hover dataTable'>
                            <tr>
                                <th class='th'>
                                    STT
                                </th>
                                <th class='th'>
                                    Họ tên
                                </th>
                                <th class='th'>
                                    Số điện thoại
                                </th>
                                <th class='th'>
                                    Email
                                </th>
                                <th class='th'>
                                    Địa chỉ
                                </th>
                                <th class='th'>
                                    Quyền
                                </th>
                                <th class='th'>
                                    Tên đăng nhập
                                </th>
                                <th class='th'>
                                    Mật khẩu
                                </th>
                                <th class='th'>
                                    Văn Phòng
                                </th>
                                <th class='th'></th>
                            </tr>";
        for (int i = 0; i < table.Rows.Count; i++)
        {
            html += "       <tr>";
            html += "       <td style='text-align:center;vertical-align: inherit;'>" + (((Page - 1) * PageSize) + i + 1).ToString() + "</td>";
            html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["HoTen"].ToString() + "</td>";
            html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["SoDienThoai"].ToString() + "</td>";
            html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["Email"].ToString() + "</td>";
            html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["DiaChi"].ToString() + "</td>";
            html += "       <td style='text-align:center;vertical-align: inherit;'>" + StaticData.getField("tb_Quyen", "TenQuyen", "MaQuyen", table.Rows[i]["MaQuyen"].ToString()) + "</td>";
            html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["TenDangNhap"].ToString() + "</td>";
            html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["MatKhau"].ToString() + "</td>";

            html += "       <td style='text-align:center;vertical-align: inherit;'>" + StaticData.getField("tb_ChiNhanh", "TenChiNhanh", "idChiNhanh", table.Rows[i]["ChiNhanh"].ToString()) + "</td>";
            html += "       <td style='text-align:center;vertical-align: inherit;font-size:20px'>";
            html += "           <a href='#' onclick='window.location=\"DanhMucNguoiDung-CapNhat.aspx?Page=" + Page.ToString() + "&idNguoiDung=" + table.Rows[i]["idNguoiDung"].ToString() + "\"'><i class='fa fa-edit'></i></a>";
            html += "           <a href='#' onclick='DeleteNguoiDung(\"" + table.Rows[i]["idNguoiDung"].ToString() + "\")'> <i class='fa fa-trash'></i></a>";
            html += "       </td>";

            //html += "       <td style='text-align:center;vertical-align: inherit;'>";

            //html += "  <table BORDERCOLOR='#ffffff' border='1' style='background-color:white;border-color:white;width:70%;'><tr><td style='background-color:white;border-color:white;'>      <a href='#' onclick='window.location=\"DanhMucNguoiDung-CapNhat.aspx?Page=" + Page.ToString() + "&idNguoiDung=" + table.Rows[i]["idNguoiDung"].ToString() + "\"'><img class='imgedit' src='../images/edit.png'/></a>";
            //html += "   </td><td style='background-color:white;' >    <a href='#'  onclick='DeleteNguoiDung(\"" + table.Rows[i]["idNguoiDung"].ToString() + "\")'> <img class='imgedit' src='../images/delete.png' /></a></td></tr></table></td>";
           // html += "       </td>";
            html += "       </tr>";

        }


        html += "  </table><table >   <tr>";
        html += "       <td colspan='17' class='footertable'>";
        string url = "DanhMucNguoiDung.aspx?";
        if (sHoTen != "")
            url += "HoTen=" + sHoTen + "&";
        if (sMaQuyen != "")
            url += "MaQuyen=" + sMaQuyen + "&";
        url += "Page=";
        html += StaticData.PhanTrang(url, txtFistPage, txtPage1, txtPage2, txtPage3, txtPage4, txtPage5, txtLastPage, Page);
        html += "    </td></tr><tr><td colspan='17'>&nbsp;</td></tr>";
        html += "     </table></center>";
        dvDanhSachNguoiDung.InnerHtml = html;
    }
    protected void btTimKiem_Click(object sender, EventArgs e)
    {
        string HoTen = txtHoTen.Value.Trim();
        string MaQuyen = slQuyen.Value.Trim();
        string url = "DanhMucNguoiDung.aspx?";
        if (HoTen != "")
            url += "HoTen=" + HoTen + "&";
        if (MaQuyen != "0")
            url += "MaQuyen=" + MaQuyen + "&";
        Response.Redirect(url);

         
    }

    protected void btXemTatCa_Click(object sender, EventArgs e)
    {
        string url = "DanhMucNguoiDung.aspx";
        Response.Redirect(url);
    }

    
}