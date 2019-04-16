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
    string Ngay = "";
    string sTuNgay = "";
    string sDenNgay = "";
    string sIdNhanVien = "";
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
            LoadNhanVien();
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
    
    #region paging
    private void SetPage()
    {

        string sql = @"select count(idXe) from  tb_Xe";
        //if (sTuNgay == "" | sDenNgay == "")
        //{
        //    Ngay = DateTime.Now.ToString("MM-dd-yyyy");
        //    sql += " and NgayLap = '" + Ngay + "'";
        //}
        //else
        //{
        //    if (sTuNgay != "")
        //        sql += " and NgayLap >= '" + StaticData.ConvertDDMMtoMMDD(sTuNgay) + " 00:00:00'";
        //    if (sDenNgay != "")
        //        sql += " and NgayLap <= '" + StaticData.ConvertDDMMtoMMDD(sDenNgay) + " 23:59:59'";
        //}
       // sql += " group by dh.IDDonHang,dh.NgayLap, dh.TongCuoc) as T";
        DataTable tbTotalRows = Connect.GetTable(sql);
        int TotalRows = int.Parse(tbTotalRows.Rows[0][0].ToString());
      //  TongTienHang = double.Parse(tbTotalRows.Rows[0][1].ToString());
      //  TongTienCuoc = double.Parse(tbTotalRows.Rows[0][1].ToString());
      //  TongPhuPhi = double.Parse(tbTotalRows.Rows[0][3].ToString());
    //    SoHoaDon = TotalRows;
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
        string Ngay = "";
        string sql = "";
        sql += @"select * from 
            (
	            SELECT ROW_NUMBER() OVER
                  (
                        ORDER BY idXe desc
                  )AS RowNumber
	              ,*
                  FROM (select * from tb_Xe
            ";
       
        //if (sTuNgay != "" | sDenNgay != "")
        //{
        //    if (sTuNgay != "")
        //        sql += " and NgayLap >= '" + StaticData.ConvertDDMMtoMMDD(sTuNgay) + " 00:00:00'";
        //    if (sDenNgay != "")
        //        sql += " and NgayLap <= '" + StaticData.ConvertDDMMtoMMDD(sDenNgay) + " 23:59:59'";
           
        //}
        //else
        //{
        //    Ngay = DateTime.Now.ToString("MM-dd-yyyy");
        //    sql += " and NgayLap = '" + Ngay + "'";
        //}


        sql += " ) as T) as tb1 WHERE RowNumber BETWEEN (" + Page + " - 1) * " + PageSize + " + 1 AND (((" + Page + " - 1) * " + PageSize + " + 1) + " + PageSize + ") - 1";


        DataTable table = Connect.GetTable(sql);
        //txtNoiDung.InnerHtml = table.Rows[0]["NoiDung"].ToString();
        SetPage();
        string html = @"<center><table class='table table-bordered table-hover dataTable'>
                            <tr>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    STT
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Biển Số Xe
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Tài Xế 
                                </th>
  
                          
                            </tr>";
        html += "           <tr style='background: #ff7423;'>";
     //   html += "               <td colspan='3' style='text-align:center;vertical-align: inherit;'><b>Tổng cộng (" + SoHoaDon + " đơn)</b></td>";
       // html += "               <td style='text-align:center;vertical-align: inherit;'><b>" + TongTienHang.ToString("N0").Replace(",", ".") + "</b></td>";
    //    html += "               <td style='text-align:center;vertical-align: inherit;'><b>" + TongTienCuoc.ToString("N0").Replace(",", ".") + "</b></td>";
     //   html += "               <td style='text-align:center;vertical-align: inherit;'><b>" + TongPhuPhi.ToString("N0").Replace(",", ".") + "</b></td>";
     //   html += "               <td style='text-align:center;vertical-align: inherit;'><b>" + (TongTienHang + TongTienCuoc + TongPhuPhi).ToString("N0").Replace(",", ".") + "</b></td>";
        html += "           </tr>";
        if(table.Rows.Count > 0)
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                string idDonHang = table.Rows[i]["idXe"].ToString();
                //string sqlCTDH = "select TongSoLuong=sum(isnull(SoLuong,0)),TongTienHang=sum(isnull(TienHang,0)),TongTienCuoc=sum(isnull(TienCuoc,0)) from tb_ChiTietDonHang where idDonHang='" + table.Rows[i]["idDonHang"].ToString() + "'";
                //DataTable tbCTDH = Connect.GetTable(sqlCTDH);
                html += "       <tr onclick=XemChiTiet(\"" + idDonHang + "\") style='cursor:pointer'>";
                html += "       <td  style='text-align:center;vertical-align: inherit;font-size:20px;white-space: nowrap;'>" + (((Page - 1) * PageSize) + i + 1).ToString() + "</td>";
                //   html += "       <td>" + StaticData.getField("tb_NguoiDung", "HoTen", "idNguoiDung", table.Rows[i]["idNguoiDung"].ToString()) + "</td>";
                html += "       <td  style='text-align:center;vertical-align: inherit;font-size:20px;white-space: nowrap;'>" + table.Rows[i]["BienSoXe"].ToString() + "</td>";
                html += "       <td  style='text-align:center;vertical-align: inherit;font-size:20px;white-space: nowrap;'>" + StaticData.getField("tb_NguoiDung", "HoTen", "idNguoiDung", table.Rows[i]["idNguoiDung"].ToString()) + "</td>";
                
                html += "       </tr>";
                
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
                html += "                       <table id='tb-danhsachSP' class='table table-bordered table-hover dataTable'>";
                html += "                           <thead style='white-space: nowrap;'>";
                html += "                               <tr id='tr_SanPham'>";
                html += "                               <th class='th' style='background-color: #1c9c13; font-size: 15px; color: white;'>STT</th>";
                html += "                              <th class='th' style='background-color: #1c9c13; font-size: 15px; color: white;'>Tên Hàng Hóa</th>";
                html += "                              <th class='th' style='background-color: #1c9c13; font-size: 15px; color: white;'>Mã Hàng Hóa</th>";
                html += "                              <th class='th' style='background-color: #1c9c13; font-size: 15px; color: white;'>Số Lượng</th>";
                html += "                              <th class='th' style='background-color: #1c9c13; font-size: 15px; color: white;'>Tình Trạng</th>";
                //   html += "                              <th class='th' style='background-color: #1c9c13; font-size: 15px; color: white;'>Chức Năng</th>";
                html += "                           </tr>";
                html += "                       </thead >";
                html += "                       <tbody id='danhSachSPChon' runat='server'>";
                string sqlCTDH = "select * from tb_PhanHangLenXe ph, tb_ChiTietDonHang ct where ph.idPhanHangLenXe = ct.idPhanHangLenXe and MaTinhTrang = 'DVC' and idXe ='" + idDonHang + "'";
                DataTable tbCTDH = Connect.GetTable(sqlCTDH);
                for (int j = 0; j < tbCTDH.Rows.Count; j++)
                {
                    html += "<tr>";
                    html += "       <td style='text-align:center;vertical-align: inherit;'>" + (j + 1) + "</td>";

                    html += "<td style='text-align:center;vertical-align: inherit;'> " + StaticData.getField("tb_HangHoa", "TenHangHoa", "IDHangHoa", tbCTDH.Rows[j]["idHangHoa"].ToString()) + "</td>";
                      html += "<td style='text-align:center;vertical-align: inherit;'> " +  tbCTDH.Rows[j]["MaHangHoa"].ToString() + "</td>";
                    html += "<td style='text-align:center;vertical-align: inherit;'> " + tbCTDH.Rows[j]["SoLuong"].ToString() + "</td>";

                    html += "<td style='text-align:center;vertical-align: inherit;'> " + StaticData.getField("tb_TinhTrang", "TenTinhTrang", "MaTinhTrang", tbCTDH.Rows[j]["MaTinhTrang"].ToString()) + "</td>";
                    //     html += "<td style='text-align:center;vertical-align: inherit;'> " + tbCTDH.Rows[j]["SDTNguoiNhan"].ToString() + "</td>";
                    html += "</tr>";
                }
                html += "</tbody>";
                // html += "                       <tbody id='danhSachSPChon' runat='server'></tbody>";
                html += "                        </table>";
             
                html += "                       <div class='container-fluid' >";
                html += "                           <div class='span12 CanhPhai' style='padding-top: 10px;'>";
                html += "           <a class='btn btn-default' title='In đơn hàng' onclick='PrinfBienNhanXeCho(\"" + table.Rows[i]["idXe"].ToString() + "\")'><i class='fa fa-print'></i><sapn> In đơn hàng</span></a>";
                if (mQuyen.ToUpper() != "NVVP" && mQuyen.ToUpper() != "KH" && mQuyen.ToUpper() != "NVGN")
                {
                    //html += "           <a class='btn btn-primary' title='Sửa đơn hàng' onclick='window.location=\"PhanHangLenXe-CapNhat.aspx?Page=" + Page.ToString() + "&idPhanHangLenXe=" + table.Rows[i]["idPhanHangLenXe"].ToString() + "\"'><i class='fa fa-edit'></i><span> Cập nhật</span></a>";
                    //html += "           <a class='btn btn-danger' title='Xóa đơn hàng' onclick='DeleteDonHang(\"" + table.Rows[i]["idPhanHangLenXe"].ToString() + "\")'><i class='fa fa-trash'></i><span> Xóa</span></a>";

                }
                else
                {
                    //if (mQuyen.ToUpper() == "NVVP" || mQuyen.ToUpper() == "NVGN" || mQuyen.ToUpper() == "KH")
                    //{
                    //    html += "       <a style='cursor:pointer' title='Sửa đơn hàng' onclick='window.location=\"QuanLyVanChuyen-CapNhat.aspx?Page=" + Page.ToString() + "&idDonHang=" + table.Rows[i]["idDonHang"].ToString() + "\"'><i class='fa fa-edit'></i><span> Cập nhật</span></a>";
                    //}
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
        }
       
        html += "  </table><table >   <tr>";
        html += "       <td colspan='21' class='footertable'>";
        string url = "ThongKeDonHangNhanTrongNgay.aspx?";
        if (sIdNhanVien != "")
            url += "idNhanVien=" + sIdNhanVien + "&";
        if (sNguoiGui != "")
            url += "NguoiGui=" + sNguoiGui + "&";
        if (sTuNgay != "")
            url += "TuNgay=" + sTuNgay + "&";
        if (sDenNgay != "")
            url += "DenNgay=" + sDenNgay + "&";
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
        string url = "ThongKeDonHangGui.aspx?";
        if (idNhanVien != "" && idNhanVien != "0")
            url += "idNhanVien=" + idNhanVien + "&";
        if (NguoiGui != "")
            url += "NguoiGui=" + NguoiGui + "&";
        if (TuNgay != "")
            url += "TuNgay=" + TuNgay + "&";
        if (DenNgay != "")
            url += "DenNgay=" + DenNgay + "&";

        Response.Redirect(url);
    }
    protected void btXemTatCa_Click(object sender, EventArgs e)
    {
        string url = "ThongKeDonHangGui.aspx";
        Response.Redirect(url);
    }
    protected void btXuatExcel_Click(object sender, EventArgs e)
    {
        string sql = @"select * from
            (
	            SELECT ROW_NUMBER() OVER
                  (
                        ORDER BY idDonHang desc
                  )AS 'STT',nd.HoTen 'Nhân viên giao'
	              ,dh.MaDonHang as 'Mã đơn hàng',convert(varchar,dh.NgayLap,103) as 'Ngày lập',kh.TenKhachHang as 'Người gửi',tt.TenTinhTrang as 'Tình trạng',isnull(dh.TienHang,0) as 'Tiền hàng', isnull(dh.TienCuoc,0) as 'Tiền cước', isnull(dh.PhuPhi,0) as 'Phụ phí', isnull(isnull(dh.TienHang,0)+isnull(dh.TienCuoc,0)+isnull(dh.PhuPhi,0),0) as 'Tổng tiền'
                  FROM tb_DonHang dh left join tb_NguoiDung nd on dh.idNguoiDung = nd.idNguoiDung,tb_KhachHang kh,tb_TinhTrang tt where dh.isDaNhanTien='1' and dh.idKhachHang = kh.idKhachHang and dh.MaTinhTrang like tt.MaTinhTrang) as tb ";
        ExportToExcel(Connect.GetTable(sql));
    }
    private void ExportToExcel(DataTable dt)
    {
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(dt, "ThongKeDonHangNhanTrongNgay");

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=ThongKeDonHangDaNhanTien.xlsx");
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