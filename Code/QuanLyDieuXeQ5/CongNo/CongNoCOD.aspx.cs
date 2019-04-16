using System;
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

    string sCongNo = "";
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
            try
            {
                if (Request.QueryString["CongNo"].Trim() != "")
                {
                    sCongNo = Request.QueryString["CongNo"].Trim();
                    // txtMaDonHang.Value = sMaDonHang;
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
                    txtKhachHang.Value = StaticData.getField("tb_KhachHang", "TenKhachHang", "idKhachHang", sIdKhachHang);
                }
            }
            catch { }
//            dvDanhSachDonHang.InnerHtml = @"<center><table class='table table-bordered table-hover dataTable'>
//                            <tr style='white-space: nowrap;'>
//                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
//                                    STT
//                                </th>
//                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
//                                    Mã nhà cung cấp
//                                </th>
//                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
//                                    Tên nhà cung cấp
//                                </th>
//                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
//                                    Điện thoại
//                                </th>
//                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
//                                    Đầu kỳ
//                                </th>
//                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
//                                    Thanh toán
//                                </th>
//                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
//                                    Còn lại
//                                </th>
//                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
//                                    Lịch sử giao dịch
//                                </th>
//                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
//                                    In phiếu
//                                </th>
//                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
//                                    
//                                </th>
//                            </tr>
//                            </tr>
//                            <tr>
//                                <td>
//                                    1
//                                </td>
//                                <td>
//                                    NCC00001
//                                </td>
//                                <td>
//                                    Nhà cung cấp A
//                                </td>
//                                <td style='text-align:center;'>
//                                    02839000000
//                                </td>
//                                <td style='text-align:center;'>
//                                    29.000.000
//                                </td>
//                                <td style='text-align:center;'>
//                                    28.000.000
//                                </td>
//                                <td style='text-align:center;'>
//                                    1.000.000
//                                </td>
//                                <td style='text-align:center;'>
//                                    <a href='#' >Xem</a>
//                                </td>
//                                <td style='text-align:center;vertical-align: inherit;font-size:20px;white-space: nowrap;'>
//                                    <a href='#'><i class='fa fa-print'></i></a>
//                                </td>
//                                <td style='text-align:center;vertical-align: inherit;white-space: nowrap;'>
//                                    <a href='../CongNo/CongNoNhaCungCap-CapNhat.aspx'><i style='font-size:20px;' class='fa fa-edit'></i> Trả Nợ </a>
//                                </td>
//                            </tr>
//                        </table>
//                    </center>";
            LoadDonHang();
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

        string sql = "select count(idDonHang) from tb_DonHang where PhiCOD > 0";
        //if (sHoTen != "")
        //    sql += " and HoTen like N'%" + sHoTen + "%'";
        //if (sMaQuyen != "" && sMaQuyen != "0")
        //    sql += " and MaQuyen like '%" + sMaQuyen + "%'";
        if (sTuNgay != "")
            sql += " and NgayTra >= '" + StaticData.ConvertDDMMtoMMDD(sTuNgay) + " 00:00:00'";
        if (sDenNgay != "")
            sql += " and NgayTra <= '" + StaticData.ConvertDDMMtoMMDD(sDenNgay) + " 00:00:00'";
        if (sIdKhachHang != "")

            sql += " AND MaNhaCungCap in ( select a.MaNhaCungCap from tb_NhaCungCap a where  a.MaNhaCungCap = N'" + sIdKhachHang + "' )";
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
        // end trong if
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
                  FROM tb_DonHang where PhiCOD >0
            ";
        if (sTuNgay != "")
            sql += " and NgayTra >= '" + StaticData.ConvertDDMMtoMMDD(sTuNgay) + " 00:00:00'";
        if (sDenNgay != "")
            sql += " and NgayTra <= '" + StaticData.ConvertDDMMtoMMDD(sDenNgay) + " 00:00:00'";
        //if (sMaDonHang != "")
        //    sql += " and MaDonHang like N'%" + sMaDonHang + "%'";

        if (sIdKhachHang != "")
            sql += " and dh.IDNhaCungCap='" + sIdKhachHang + "'";

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
                                    Tên Người Gửi
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    SDT Người Gửi
                                </th>
                            
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Tổng COD
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Thanh Toán
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Còn Lại
                                </th>
                              
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Lịch Sử Giao Dịch
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    In Phiếu
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    
                                </th>
                            </tr>";
        //html += "           <tr style='background: #ff7423;'>";
        //html += "               <td colspan='7' style='text-align:center;vertical-align: inherit;'><b>Tổng cộng (" + SoHoaDon + " đơn)</b></td>";
        //html += "               <td style='text-align:center;vertical-align: inherit;'><b>" + TongTienHang.ToString("N0").Replace(",", ".") + "</b></td>";
        //html += "               <td style='text-align:center;vertical-align: inherit;'><b>" + TongTienCuoc.ToString("N0").Replace(",", ".") + "</b></td>";
        //html += "               <td style='text-align:center;vertical-align: inherit;'><b>" + TongPhuPhi.ToString("N0").Replace(",", ".") + "</b></td>";
        //html += "               <td style='text-align:center;vertical-align: inherit;'><b>" + (TongTienHang + TongTienCuoc + TongPhuPhi).ToString("N0").Replace(",", ".") + "</b></td>";
        //html += "           </tr>";
        for (int i = 0; i < table.Rows.Count; i++)
        {
            string idKhachHang = table.Rows[i]["IDDonHang"].ToString();

            string sqltientra = @"select * from tb_TraNoCOD where IDDonHang='" + idKhachHang + "'";
            DataTable tabletientra = Connect.GetTable(sqltientra);
            double Tientra = 0;
            if (tabletientra.Rows.Count > 0)
            {
                for (int j = 0; j < tabletientra.Rows.Count; j++)
                {
                    double Tien = double.Parse(tabletientra.Rows[j]["SoTien"].ToString());

                    Tientra += Tien;
                }
            }
            double NoKhachHang = (table.Rows[i]["PhiCOD"].ToString() == "" ? 0 : double.Parse(table.Rows[i]["PhiCOD"].ToString()));
            double ConLai = 0;
            ConLai = NoKhachHang - Tientra;


            // double TongNo = 0;
            if (sCongNo == "DaHoanThanh")
            {
                if (ConLai == 0)
                {
                    html += "       <tr>";
                    html += "       <td style='text-align:center;vertical-align: inherit;'>" + (((Page - 1) * PageSize) + i + 1).ToString() + "</td>";
                    html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["NguoiGui"].ToString() + "</td>";
                    html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["SDTNguoiGui"].ToString() + "</td>";
                    //  html += "       <td>" + table.Rows[i]["NoKhachHang"].ToString() + "</td>";

                    html += "       <td style='text-align:center;vertical-align: inherit;'>" + NoKhachHang.ToString("N0").Replace(",", ".") + "</td>";


                    html += "       <td style='text-align:center;vertical-align: inherit;'>" + Tientra.ToString("N0").Replace(",", ".") + "</td>";

                    html += "       <td style='text-align:center;vertical-align: inherit;'>" + ConLai.ToString("N0").Replace(",", ".") + "</td>";

                    html += "  <td style='text-align:center; vertical-align: inherit;font-size:20px'>";
                    html += " <a onclick='LoadPopupLichSuNoNhaCungCap(" + table.Rows[i]["idDonHang"] + @")' style='cursor:pointer;'><span style='font-size:20px;'><i class='fa fa-check-square-o'></i></span>Xem</a> </td>";
                    //html += "                       <a href='#' >Xem</a> </td>";
                    html += "       <td style='text-align:center;vertical-align: inherit;'>";
                    html += "          <a href='#'><i class='fa fa-print'></i></a>    ";
                    html += "       </td>";


                    html += "       <td style='text-align:center;vertical-align: inherit;font-size:20px'>";
                    html += "           <a href='#' onclick='window.location=\"CongNoCOD-CapNhat.aspx?&Page=" + Page.ToString() + "&idDonHang=" + table.Rows[i]["idDonHang"].ToString() + "\"'><i class='fa fa-edit'></i> Trả Nợ </a>";
                    //html += "          <a href='../CongNo/CongNoKhachHang-CapNhat.aspx'><i style='font-size:20px;' class='fa fa-edit'></i> Thu Nợ </a>
                    html += "       </td>";



                    html += "       </tr>";
                }

            }
            else
            {
                if (sCongNo == "ConNo")
                {
                    if (ConLai > 0)
                    {
                        html += "       <tr>";
                        html += "       <td style='text-align:center;vertical-align: inherit;'>" + (((Page - 1) * PageSize) + i + 1).ToString() + "</td>";
                        html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["NguoiGui"].ToString() + "</td>";
                        html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["SDTNguoiGui"].ToString() + "</td>";
                        //  html += "       <td>" + table.Rows[i]["NoKhachHang"].ToString() + "</td>";

                        html += "       <td style='text-align:center;vertical-align: inherit;'>" + NoKhachHang.ToString("N0").Replace(",", ".") + "</td>";


                        html += "       <td style='text-align:center;vertical-align: inherit;'>" + Tientra.ToString("N0").Replace(",", ".") + "</td>";

                        html += "       <td style='text-align:center;vertical-align: inherit;'>" + ConLai.ToString("N0").Replace(",", ".") + "</td>";

                        html += "  <td style='text-align:center; vertical-align: inherit;font-size:20px'>";
                        html += " <a onclick='LoadPopupLichSuNoNhaCungCap(" + table.Rows[i]["idDonHang"] + @")' style='cursor:pointer;'><span style='font-size:20px;'><i class='fa fa-check-square-o'></i></span>Xem</a> </td>";
                        //html += "                       <a href='#' >Xem</a> </td>";
                        html += "       <td style='text-align:center;vertical-align: inherit;'>";
                        html += "          <a href='#'><i class='fa fa-print'></i></a>    ";
                        html += "       </td>";


                        html += "       <td style='text-align:center;vertical-align: inherit;font-size:20px'>";
                        html += "           <a href='#' onclick='window.location=\"CongNoCOD-CapNhat.aspx?&Page=" + Page.ToString() + "&idDonHang=" + table.Rows[i]["idDonHang"].ToString() + "\"'><i class='fa fa-edit'></i> Trả Nợ </a>";
                        //html += "          <a href='../CongNo/CongNoKhachHang-CapNhat.aspx'><i style='font-size:20px;' class='fa fa-edit'></i> Thu Nợ </a>
                        html += "       </td>";



                        html += "       </tr>";
                    }

                }
                else
                {
                    html += "       <tr>";
                    html += "       <td style='text-align:center;vertical-align: inherit;'>" + (((Page - 1) * PageSize) + i + 1).ToString() + "</td>";
                    html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["NguoiGui"].ToString() + "</td>";
                    html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["SDTNguoiGui"].ToString() + "</td>";
                    //  html += "       <td>" + table.Rows[i]["NoKhachHang"].ToString() + "</td>";

                    html += "       <td style='text-align:center;vertical-align: inherit;'>" + NoKhachHang.ToString("N0").Replace(",", ".") + "</td>";


                    html += "       <td style='text-align:center;vertical-align: inherit;'>" + Tientra.ToString("N0").Replace(",", ".") + "</td>";

                    html += "       <td style='text-align:center;vertical-align: inherit;'>" + ConLai.ToString("N0").Replace(",", ".") + "</td>";

                    html += "  <td style='text-align:center; vertical-align: inherit;font-size:20px'>";
                    html += " <a onclick='LoadPopupLichSuNoNhaCungCap(" + table.Rows[i]["idDonHang"] + @")' style='cursor:pointer;'><span style='font-size:20px;'><i class='fa fa-check-square-o'></i></span>Xem</a> </td>";
                    //html += "                       <a href='#' >Xem</a> </td>";
                    html += "       <td style='text-align:center;vertical-align: inherit;'>";
                    html += "          <a href='#'><i class='fa fa-print'></i></a>    ";
                    html += "       </td>";


                    html += "       <td style='text-align:center;vertical-align: inherit;font-size:20px'>";
                    html += "           <a href='#' onclick='window.location=\"CongNoCOD-CapNhat.aspx?&Page=" + Page.ToString() + "&idDonHang=" + table.Rows[i]["idDonHang"].ToString() + "\"'><i class='fa fa-edit'></i> Trả Nợ </a>";
                    //html += "          <a href='../CongNo/CongNoKhachHang-CapNhat.aspx'><i style='font-size:20px;' class='fa fa-edit'></i> Thu Nợ </a>
                    html += "       </td>";



                    html += "       </tr>";
                }
            }


        }
       // {
       //     double TongNo = 0;
       //     html += "       <tr>";
       //     html += "       <td style='text-align:center;vertical-align: inherit;'>" + (((Page - 1) * PageSize) + i + 1).ToString() + "</td>";
        
        //     html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["SoDienThoai"].ToString() + "</td>";
       //     double NoKhachHang = (table.Rows[i]["PhiCOD"].ToString() == "" ? 0 : double.Parse(table.Rows[i]["PhiCOD"].ToString()));
       //     html += "       <td style='text-align:center;vertical-align: inherit;'>" + NoKhachHang.ToString("N0").Replace(",", ".") + "</td>";

       //     string idKhachHang = table.Rows[i]["idDonHang"].ToString();

       //     string sqltientra = @"select * from tb_TraNoCOD where IDDonHang='" + idKhachHang + "'";
       //     DataTable tabletientra = Connect.GetTable(sqltientra);
       //     double Tientra = 0;
       //     if (tabletientra.Rows.Count > 0)
       //     {
       //         for (int j = 0; j < tabletientra.Rows.Count; j++)
       //         {
       //             double Tien = double.Parse(tabletientra.Rows[j]["SoTien"].ToString());

       //             Tientra += Tien;
       //         }
       //     }
       //     html += "       <td style='text-align:center;vertical-align: inherit;'>" + Tientra.ToString("N0").Replace(",", ".") + "</td>";
       //     double ConLai = 0;
       //     ConLai = NoKhachHang - Tientra;
       //     html += "       <td style='text-align:center;vertical-align: inherit;'>" + ConLai.ToString("N0").Replace(",", ".") + "</td>";
      
       //     html += "  <td style='text-align:center; vertical-align: inherit;font-size:20px'>";
       //     html += " <a onclick='LoadPopupLichSuNoNhaCungCap(" + table.Rows[i]["idDonHang"] + @")' style='cursor:pointer;'><span style='font-size:20px;'><i class='fa fa-check-square-o'></i></span>Xem</a> </td>";
       //     //html += "                       <a href='#' >Xem</a> </td>";
       //     html += "       <td style='text-align:center;vertical-align: inherit;'>";
       //     html += "          <a href='#'><i class='fa fa-print'></i></a>    ";
       //     html += "       </td>";


       //     html += "       <td style='text-align:center;vertical-align: inherit;font-size:20px'>";
       //     html += "           <a href='#' onclick='window.location=\"CongNoCOD-CapNhat.aspx?&Page=" + Page.ToString() + "&idDonHang=" + table.Rows[i]["idDonHang"].ToString() + "\"'><i class='fa fa-edit'></i> Trả Nợ </a>";
       //     //html += "          <a href='../CongNo/CongNoKhachHang-CapNhat.aspx'><i style='font-size:20px;' class='fa fa-edit'></i> Thu Nợ </a>
       //     html += "       </td>";



       //     html += "       </tr>";
        

       // }
        html += "  </table><table >   <tr>";
        html += "       <td colspan='21' class='footertable'>";
        string url = "CongNoNhaCungCap.aspx?";
        if (sTuNgay != "")
            url += "TuNgay=" + sTuNgay + "&";
        if (sDenNgay != "")
            url += "DenNgay=" + sDenNgay + "&";
        //if (sMaDonHang != "")
        //    url += "MaDonHang=" + sMaDonHang + "&";

        if (sIdKhachHang != "" && sIdKhachHang != "0")
            url += "idNhaCungCap=" + sIdKhachHang + "&";

        url += "Page=";
        html += StaticData.PhanTrang(url, txtFistPage, txtPage1, txtPage2, txtPage3, txtPage4, txtPage5, txtLastPage, Page);
        html += "    </td></tr>";
        html += "     </table></center>";


        dvDanhSachDonHang.InnerHtml = html;
    }
    protected void btTimKiem_Click2(object sender, EventArgs e)
    {
        string TuNgay = txtTuNgay.Value.Trim();
        string DenNgay = txtDenNgay.Value.Trim();
        //string MaDonHang = txtMaDonHang.Value.Trim();
        // string idNguoiDung = slNhanVienGiao.Value.Trim();
        string idKhachHang = hdIdKhachHang.Value.Trim();
        //  string Ten = txtTenKhachHang.Value.Trim();
        string DaHoanThanh = "DaHoanThanh";


        //string DiaChiNguoiNhan = txtDiaChiNguoiNhan.Value.ToString();
        //  string ChiNhanhNhan = txtChiNhanhNhan.Value.Trim();
        string url = "CongNoCOD.aspx?";
        if (TuNgay != "")
            url += "TuNgay=" + TuNgay + "&";
        if (DenNgay != "")
            url += "DenNgay=" + DenNgay + "&";
        //if (MaDonHang != "")
        //    url += "MaDonHang=" + MaDonHang + "&";


        if (DaHoanThanh != "" && DaHoanThanh != "0")
            url += "CongNo=" + DaHoanThanh + "&";

        //if (DiaChiNguoiNhan != "")
        //    url += "DiaChiNguoiNhan=" + DiaChiNguoiNhan + "&";

        Response.Redirect(url);
    }

    protected void btTimKiem_Click1(object sender, EventArgs e)
    {
        string TuNgay = txtTuNgay.Value.Trim();
        string DenNgay = txtDenNgay.Value.Trim();
        //string MaDonHang = txtMaDonHang.Value.Trim();
        // string idNguoiDung = slNhanVienGiao.Value.Trim();
        string idKhachHang = hdIdKhachHang.Value.Trim();
        //  string Ten = txtTenKhachHang.Value.Trim();
        string DaHoanThanh = "ConNo";


        //string DiaChiNguoiNhan = txtDiaChiNguoiNhan.Value.ToString();
        //  string ChiNhanhNhan = txtChiNhanhNhan.Value.Trim();
        string url = "CongNoCOD.aspx?";
        if (TuNgay != "")
            url += "TuNgay=" + TuNgay + "&";
        if (DenNgay != "")
            url += "DenNgay=" + DenNgay + "&";
        //if (MaDonHang != "")
        //    url += "MaDonHang=" + MaDonHang + "&";


        if (DaHoanThanh != "" && DaHoanThanh != "0")
            url += "CongNo=" + DaHoanThanh + "&";

        //if (DiaChiNguoiNhan != "")
        //    url += "DiaChiNguoiNhan=" + DiaChiNguoiNhan + "&";

        Response.Redirect(url);
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
        string url = "CongNoCOD.aspx";
        Response.Redirect(url);
    }

    
}