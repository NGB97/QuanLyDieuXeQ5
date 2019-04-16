using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DanhMuc_DanhMucKhachHang : System.Web.UI.Page
{
    string sTenKhachHang = "";
    string sSoDienThoai = "";
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
                if (mQuyen.ToUpper() != "ADMIN")
                {
                    Response.Redirect("../Home/DangNhap.aspx");
                }
            }
            try
            {
                if (Request.QueryString["TenChiNhanh"].Trim() != "")
                {
                    sTenKhachHang = Request.QueryString["TenChiNhanh"].Trim();
                    txtTenKhachHang.Value = sTenKhachHang;
                }
            }
            catch { }
            try
            {
                if (Request.QueryString["SoDienThoai"].Trim() != "")
                {
                    sSoDienThoai = Request.QueryString["SoDienThoai"].Trim();
                    txtSoDienThoai.Value = sSoDienThoai;
                }
            }
            catch { }

            LoadNguoiDung();
        }
    }
    #region paging
    private void SetPage()
    {
        string sql = "select count(idKhachHang) from tb_KhachHang where '1'='1'";
        if (sTenKhachHang != "")
            sql += " and TenKhachHang like N'%" + sTenKhachHang + "%'";
        if (sSoDienThoai != "")
            sql += " and SoDienThoai like '%" + sSoDienThoai + "%'";
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
                        ORDER BY idChiNhanh desc
                  )AS RowNumber
	              ,*
                  FROM tb_ChiNhanh where 1 = 1
            ";
        if (sTenKhachHang != "")
            sql += " and TenChiNhanh like N'%" + sTenKhachHang + "%'";
        if (sSoDienThoai != "")
            sql += " and SoDienThoai like '%" + sSoDienThoai + "%'";
        sql += ") as tb1 WHERE RowNumber BETWEEN (" + Page + " - 1) * " + PageSize + " + 1 AND (((" + Page + " - 1) * " + PageSize + " + 1) + " + PageSize + ") - 1";


        DataTable table = Connect.GetTable(sql);
        //txtNoiDung.InnerHtml = table.Rows[0]["NoiDung"].ToString();
        SetPage();
        string html = @"<center><table class='table table-bordered table-striped' style='width:100%;radius:2px;'>
                            <tr>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    STT
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Mã chi nhánh
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Số điện thoại
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Tên Chi Nhánh
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Địa chỉ
                                </th>
                             
                   
                               
                             
                            </tr>";
        for (int i = 0; i < table.Rows.Count; i++)
        {
            string idChiNhanh = table.Rows[i]["idChiNhanh"].ToString();
            html += "   <tr onclick=XemChiTiet(\"" + idChiNhanh + "\") style='cursor:pointer'>";
            html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["RowNumber"].ToString() + "</td>";
            html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["MaChiNhanh"].ToString() + "</td>";
            html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["SoDienThoai"].ToString() + "</td>";
            html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["TenChiNhanh"].ToString() + "</td>";

            html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["DiaChi"].ToString() + "</td>";
            //html += "       <td style='text-align:center;vertical-align: inherit;'>" + (table.Rows[i]["GiaCuocNgoaiThanh"].ToString() != "" ? double.Parse(table.Rows[i]["GiaCuocNgoaiThanh"].ToString()).ToString("#,##").Replace(",", ".") : "") + "</td>";
            //html += "       <td style='text-align:center;vertical-align: inherit;'>" + (table.Rows[i]["GiaCuocDiTinh"].ToString() != "" ? double.Parse(table.Rows[i]["GiaCuocDiTinh"].ToString()).ToString("#,##").Replace(",", ".") : "") + "</td>";
            //html += "       <td style='text-align:center;vertical-align: inherit;'>" + (table.Rows[i]["GiaCuocHuyen"].ToString() != "" ? double.Parse(table.Rows[i]["GiaCuocHuyen"].ToString()).ToString("#,##").Replace(",", ".") : "") + "</td>";
            html += "   </tr>";

            html += "   <tr hidden id='tr_" + idChiNhanh + "' style='background: #eeeeee;'>";
            html += "       <td colspan='13' style='border: 1px solid;'>";
            html += "           <div>";
            html += "               <ul class='nav nav-tabs' style='margin:inherit; border:none'>";
            html += "                   <li class='active'><a data-toggle='tab' href='#HoaDon1_" + idChiNhanh + "'> <span style='padding-right:12px;font-weight:bold'> THÔNG TIN </span></a>    </li>";
            html += "               </ul>";
            html += "           </div>";
            html += "           <div class='tab-content'>";
            html += "               <div id='HoaDon1_" + idChiNhanh + "' class='tab-pane fade in active' style='background-color:white;padding: 10px;'>";
            html += "                   <div class='container-fluid'>";
            html += "                       <div class='row-fluid'>";
            html += "                       <div class='span4'>";
            html += "                           <div class='container-fluid'>";
            html += "                               <div class='row-fluid FieldHienThi'><div class='span4'>Tên chi nhánh:</div><div class='span8'><b>" + table.Rows[i]["TenChiNhanh"].ToString() + "</b></div></div>";
            html += "                               <div class='row-fluid FieldHienThi'><div class='span4'>Mã chi nhánh:</div><div class='span8'><b>" + table.Rows[i]["MaChiNhanh"].ToString() + "</b></div></div>";
            html += "                              </div>";
            html += "                       </div>";
            html += "                       <div class='span4'>";
            html += "                           <div class='container-fluid'>";

            html += "        <div class='row-fluid FieldHienThi'><div class='span4'>Số Điện Thoại:</div><div class='span8'><b>" + table.Rows[i]["SoDienThoai"].ToString() + "</b></div></div>";
            html += "                               <div class='row-fluid FieldHienThi'><div class='span4'>Địa chỉ:</div><div class='span8'><b>" + table.Rows[i]["DiaChi"].ToString() + "</b></div></div>";
                    
            html += "                           </div>";
            html += "                       </div>";
            html += "                   </div>";
            html += "                   <div class='row-fluid' style='padding-top: 10px;'>";
            html += "                       <div class='span12 CanhPhai'>";
            html += "                           <a class='btn btn-primary' href='DanhMucChiNhanh-CapNhat.aspx?idChiNhanh=" + idChiNhanh + "&Page=" + Page + "'><i class='fa fa-pencil-square-o'></i><span> Cập nhật</span></a>";
            html += "                           <a class='btn btn-danger' onclick='DeleteKhachHang(\"" + table.Rows[i]["idChiNhanh"].ToString() + "\")'><i class='fa fa-trash'></i><span style='margin-left: 5px;'>Xóa</span></a>";
            html += "                       </div>";
            html += "                   </div>";
            html += "               </div>";
            html += "           </div>";
            html += "       </td>";
            html += "   <tr>";
            //html += "       <tr>";
            //html += "       <td>" + (((Page - 1) * PageSize) + i + 1).ToString() + "</td>";
            //html += "       <td><a href='../QuanLyDonHang/QuanLyDonHang.aspx?idKhachHang=" + table.Rows[i]["idKhachHang"].ToString() + "'>" + table.Rows[i]["TenKhachHang"].ToString() + "</a></td>";
            //html += "       <td>" + table.Rows[i]["SoDienThoai"].ToString() + "</td>";
            //html += "       <td>" + table.Rows[i]["Email"].ToString() + "</td>";
            //html += "       <td>" + table.Rows[i]["DiaChi"].ToString() + "</td>";
            //if (table.Rows[i]["GiaCuocNoiThanh"].ToString() != "")
            //    html += "   <td>" + double.Parse(table.Rows[i]["GiaCuocNoiThanh"].ToString()).ToString("#,##").Replace(",", ".") + "</td>";
            //else
            //    html += "   <td></td>";
            //if (table.Rows[i]["GiaCuocNgoaiThanh"].ToString() != "")
            //    html += "   <td>" + double.Parse(table.Rows[i]["GiaCuocNgoaiThanh"].ToString()).ToString("#,##").Replace(",", ".") + "</td>";
            //else
            //    html += "   <td></td>";
            //if (table.Rows[i]["GiaCuocDiTinh"].ToString() != "")
            //    html += "   <td>" + double.Parse(table.Rows[i]["GiaCuocDiTinh"].ToString()).ToString("#,##").Replace(",", ".") + "</td>";
            //else
            //    html += "   <td></td>";
            //if (table.Rows[i]["GiaCuocHuyen"].ToString() != "")
            //    html += "   <td>" + double.Parse(table.Rows[i]["GiaCuocHuyen"].ToString()).ToString("#,##").Replace(",", ".") + "</td>";
            //else
            //    html += "   <td></td>";
            //html += "       <td>" + table.Rows[i]["isImportExcel"].ToString() + "</td>";
            //html += "       <td>" + table.Rows[i]["TenDangNhap"].ToString() + "</td>";
            //html += "       <td>" + table.Rows[i]["MatKhau"].ToString() + "</td>";

            //html += "       <td style='text-align:center;vertical-align: inherit;font-size:20px;white-space: nowrap;'>";
            //html += "           <a href='#' onclick='window.location=\"DanhMucKhachHang-CapNhat.aspx?Page=" + Page.ToString() + "&idKhachHang=" + table.Rows[i]["idKhachHang"].ToString() + "\"'><i class='fa fa-edit'></i></a>";
            //html += "           <a href='#' onclick='DeleteKhachHang(\"" + table.Rows[i]["idKhachHang"].ToString() + "\")'> <i class='fa fa-trash'></i></a>";
            //html += "       </td>";

            //html += "       </tr>";

        }
        html += "  </table><table >   <tr>";
        html += "       <td colspan='17' class='footertable'>";
        string url = "DanhMucChiNhanh.aspx?";
        if (sTenKhachHang != "")
            url += "TenChiNhanh=" + sTenKhachHang + "&";
        if (sSoDienThoai != "")
            url += "SoDienThoai=" + sSoDienThoai + "&";
        url += "Page=";
        html += StaticData.PhanTrang(url, txtFistPage, txtPage1, txtPage2, txtPage3, txtPage4, txtPage5, txtLastPage, Page);
        html += "    </td></tr><tr><td colspan='17'>&nbsp;</td></tr>";
        html += "     </table></center>";
        dvDanhSachKhachHang.InnerHtml = html;
    }
    protected void btTimKiem_Click(object sender, EventArgs e)
    {
        string TenKhachHang = txtTenKhachHang.Value.Trim();
        string SoDienThoai = txtSoDienThoai.Value.Trim();
        string url = "DanhMucChiNhanh.aspx?";
        if (TenKhachHang != "")
            url += "TenChiNhanh=" + TenKhachHang + "&";
        if (SoDienThoai != "")
            url += "SoDienThoai=" + SoDienThoai + "&";
        Response.Redirect(url);
    }
    protected void btXemTatCa_Click(object sender, EventArgs e)
    {
        string url = "DanhMucChiNhanh.aspx";
        Response.Redirect(url);
    }
}