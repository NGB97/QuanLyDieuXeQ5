using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class QuanLyDonHang_QuanLyDonHang : System.Web.UI.Page
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
    string sChiNhanhNhan = "";
    string sidChiNhanhNhan = "";
    string sIsDaHoanThanh = "";

    string mTenDangNhap = "";
    string mQuyen = "";
    string mIdNguoiDung = "";
    string mIdKhachHang = "";

    string mIDChiNhanh = "";
    string txtFistPage = "1";
    string txtPage1 = "";
    string txtPage2 = "";
    string txtPage3 = "";
    string txtPage4 = "";
    string txtPage5 = "";
    string txtLastPage = "";
    string pMaTrangThaiDH = "";
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
            LoadTinh();
            LoadTinhTrang();
            LoadNhanVienGiao();
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
                if (Request.QueryString["MaTinhTrang"].Trim() != "")
                {
                    sMaTinhTrang = Request.QueryString["MaTinhTrang"].Trim();
                    slTinhTrang.Value = sMaTinhTrang;
                }
            }
            catch { }
            try
            {
                if (Request.QueryString["idTinh"].Trim() != "")
                {
                    sIdTinh = Request.QueryString["idTinh"].Trim();
                    slTinh.SelectedValue = sIdTinh;
                    LoadHuyen(sIdTinh);
                }
            }
            catch { }
            try
            {
                if (Request.QueryString["idHuyen"].Trim() != "")
                {
                    sIdHuyen = Request.QueryString["idHuyen"].Trim();
                    slHuyen.SelectedValue = sIdHuyen;
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
                if (Request.QueryString["idNguoiDung"].Trim() != "")
                {
                    sIdNguoiDung = Request.QueryString["idNguoiDung"].Trim();
                    slNhanVienGiao.Value = sIdNguoiDung;
                }
            }
            catch { }
            try
            {
                if (Request.QueryString["ChiNhanhNhan"].Trim() != "")
                {
                    sChiNhanhNhan = Request.QueryString["ChiNhanhNhan"].Trim();
                    txtChiNhanhNhan.Value = sChiNhanhNhan;
                    sidChiNhanhNhan = StaticData.getField1("tb_ChiNhanh", "idChiNhanh", "TenChiNhanh", sChiNhanhNhan);
                 //   hdidChiNhanhNhan.Value = sidChiNhanhNhan ;
                    
                }
            }
            catch { }
            if (Request.Cookies["QuanLyCongNoAnhKiet_Login"] != null && Request.Cookies["QuanLyCongNoAnhKiet_Login"].Value.Trim() != "")
            {
                mTenDangNhap = Request.Cookies["QuanLyCongNoAnhKiet_Login"].Value.Trim();
                mIdNguoiDung = StaticData.getField("tb_NguoiDung", "idNguoiDung", "TenDangNhap", mTenDangNhap);
                mIdKhachHang = StaticData.getField("tb_KhachHang", "idKhachHang", "TenDangNhap", mTenDangNhap);
                mQuyen = MyStaticData.GetMaQuyen(mTenDangNhap);
                mIDChiNhanh = StaticData.getField("tb_NguoiDung", "ChiNhanh", "TenDangNhap", mTenDangNhap);
                //if (mQuyen.ToUpper() != "ADMIN" && mQuyen.ToUpper() != "NVGN" && mQuyen.ToUpper() != "NVVP" && mQuyen.ToUpper() != "KH")
                //{
                //    Response.Redirect("../Home/DangNhap.aspx");
                //}
                if(mQuyen.ToUpper() == "KH")
                {
                    //string isImportExcel = StaticData.getField("tb_KhachHang", "isImportExcel", "idKhachHang", mIdKhachHang);
                    //if(isImportExcel != "True")
                    //{
                    //    btImportExcel.Style.Add("display", "none");
                    //}
                    txtTenKhachHang.Disabled = true;
                }
                if (mQuyen.ToUpper() == "NVGN")
                {
                    btThemMoi.Style.Add("display", "none");
                    //btImportExcel.Style.Add("display", "none");
                }
            }
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
                pMaTrangThaiDH = Request.QueryString["MaTrangThaiDH"].Trim();
                //txtTenGoiCuoc.Value = pTenGoiCuoc;
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
            LoadDonHang();
          //  LoadTrangThaiDH(pMaTrangThaiDH);
        }
    }
    
    private void LoadTinh()
    {
        string strSql = "select * from tb_Tinh";
        slTinh.DataSource = Connect.GetTable(strSql);
        slTinh.DataTextField = "TenTinh";
        slTinh.DataValueField = "idTinh";
        slTinh.DataBind();
        slTinh.Items.Add(new ListItem("-- Chọn địa chỉ KH theo tỉnh --", "0"));
        slTinh.Items.FindByText("-- Chọn địa chỉ KH theo tỉnh --").Selected = true;
    }

    private void LoadHuyen(string idTinh)
    {
        string strSql = "select * from tb_Huyen where idTinh='" + idTinh + "'";
        slHuyen.DataSource = Connect.GetTable(strSql);
        slHuyen.DataTextField = "TenHuyen";
        slHuyen.DataValueField = "idHuyen";
        slHuyen.DataBind();
        slHuyen.Items.Add(new ListItem("-- Chọn địa chỉ KH theo huyện --", "0"));
        slHuyen.Items.FindByText("-- Chọn địa chỉ KH theo huyện --").Selected = true;
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
    private void LoadNhanVienGiao()
    {
        string strSql = "select * from tb_NguoiDung where MaQuyen='NVGN'";
        slNhanVienGiao.DataSource = Connect.GetTable(strSql);
        slNhanVienGiao.DataTextField = "HoTen";
        slNhanVienGiao.DataValueField = "idNguoiDung";
        slNhanVienGiao.DataBind();
        slNhanVienGiao.Items.Add(new ListItem("-- Chọn nhân viên giao --", "0"));
        slNhanVienGiao.Items.FindByText("-- Chọn nhân viên giao --").Selected = true;
    }

    private void LoadTinhTrang()
    {
        string strSql = "select * from tb_TinhTrang where '1'='1'";
        slTinhTrang.DataSource = Connect.GetTable(strSql);
        slTinhTrang.DataTextField = "TenTinhTrang";
        slTinhTrang.DataValueField = "MaTinhTrang";
        slTinhTrang.DataBind();
        slTinhTrang.Items.Add(new ListItem("Đã thu tiền", "True"));
        slTinhTrang.Items.Add(new ListItem("Chưa thu tiền", "False"));
        slTinhTrang.Items.Add(new ListItem("-- Chọn tình trạng --", "0"));
        slTinhTrang.Items.FindByText("-- Chọn tình trạng --").Selected = true;
    }

    #region paging
    private void SetPage()
    {
        string sql = "select count(idDonHang), isNull(Sum(Tong),0) from (select *,'Tong'=(select isnull(sum(DonGia*SoLuong),0) from tb_ChiTietDonHang ct where ct.idDonHang = dh.idDonHang) from tb_DonHang dh) as T where '1'='1'";
        if (mQuyen.ToUpper() != "ADMIN")
            sql += " and ( idChiNhanhGui =  '" + mIDChiNhanh + "' or idChiNhanhNhan = '" + mIDChiNhanh + "')";



        if (sTuNgay != "")
            sql += " and NgayLap >= '" + StaticData.ConvertDDMMtoMMDD(sTuNgay) + " 00:00:00'";
        if (sDenNgay != "")
            sql += " and NgayLap <= '" + StaticData.ConvertDDMMtoMMDD(sDenNgay) + " 00:00:00'";
        //if (sMaDonHang != "")
        //    sql += " and MaDonHang like N'%" + sMaDonHang + "%'";
        if (sIdNguoiDung != "" && sIdNguoiDung != "0")
            sql += " and dh.idNguoiDung = '" + sIdNguoiDung + "'";

        if (sIdKhachHang != "")
            sql += " and dh.idKhachHang='" + sIdKhachHang + "'";
        if (sTen != "")
            sql += " and (MaDonHang like N'%" + sTen + "%' or DiaChiNguoiNhan like N'%" + sTen + "%' or SoDienThoaiNguoiNhan like N'%" + sTen + "%' or NguoiNhan like N'%" + sTen + "%')";
        if (mQuyen.ToUpper() == "KH")
            sql += " and dh.idKhachHang='" + mIdKhachHang + "'";
        if (sMaTinhTrang != "")
        {
            if (sMaTinhTrang == "XDVP")
            {
                sql += " and (idDonHang in (select idDonHang from tb_ChiTietDonHang where MaTinhTrang = '" + sMaTinhTrang + "') or ChuyenPhatNhanh != 0)";
                sql += "and idChiNhanhGui != '" + mIDChiNhanh + "'";
            }
            else
                sql += " and (idDonHang in (select idDonHang from tb_ChiTietDonHang where MaTinhTrang = '" + sMaTinhTrang + "') )";

            if (sMaTinhTrang == "DGX")
                sql += "and idChiNhanhNhan != '" + mIDChiNhanh + "'";
            if (sMaTinhTrang == "DVC")
                sql += "and idChiNhanhGui != '" + mIDChiNhanh + "'";
            //if (sMaTinhTrang == "XDVP")
            //    sql += " and (idDonHang in (select idDonHang from tb_ChiTietDonHang where MaTinhTrang = '" + sMaTinhTrang + "') or ChuyenPhatNhanh != 0)";
            //else
            //    sql += " and (idDonHang in (select idDonHang from tb_ChiTietDonHang where MaTinhTrang = '" + sMaTinhTrang + "') )";
        }
        if (sIdTinh != "" && sIdTinh != "0")
            sql += " and idTinh='" + sIdTinh + "'";
        if (sIdHuyen != "" && sIdTinh != "0")
            sql += " and idHuyen='" + sIdHuyen + "'";
        if (sChiNhanhNhan != "")
            sql += " and idChiNhanhNhan = '" + sidChiNhanhNhan + "'";
        if (sIsDaHoanThanh != "")
            sql += " and isnull(isDaNhanTien,'False') = '" + sIsDaHoanThanh + "'";
       
        DataTable tbTotalRows = Connect.GetTable(sql);
        int TotalRows = int.Parse(tbTotalRows.Rows[0][0].ToString());
        //TongTienHang = double.Parse(tbTotalRows.Rows[0][1].ToString());
        TongTienCuoc = double.Parse(tbTotalRows.Rows[0][1].ToString());
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
        sql += @"select *, 'TenCN' = (select TenChiNhanh from tb_ChiNhanh where idChiNhanh = ViTri) from
            (
	            SELECT DENSE_RANK() OVER
                  (
                        ORDER BY dh.idDonHang desc
                  )AS RowNumber
	              ,ct.*, dh.MaDonHang, dh.NgayLap,idChiNhanhNhan, idChiNhanhGui, dh.NguoiNhanTra,CHuyenPhatNhanh, PhiCOD
				  ,kh.*
,'BienSoXe' = (select BienSoXe from tb_PhanHangLenXe ph, tb_Xe x where idPhanHangLenXe = ct.idPhanHangLenXe  and ph.idXe = x.idXe )
,'TenHH' = (select TenHangHoa from tb_HangHoa where idHangHoa = ct.idHangHoa)
				  ,'MaHH' = (select MaHangHoa from tb_HangHoa where idHangHoa = ct.idHangHoa)
				  ,'CNNhan' = (select TenChiNhanh from tb_ChiNhanh where idChiNhanh = dh.idChiNhanhNhan)
				  ,'ViTri' = (case MaTinhTrang when 'DGX' then (case when IDHangVanPhong is NULL then IDChiNhanhGui else (select ChiNhanh from tb_HangVanPhong hvp, tb_NguoiDung ng where hvp.idNguoiDung = ng.idNguoiDung and idHangVanPhong = ct.idHangVanPhong) end)
				  else idCHiNhanhNhan end )
,'TenTT' = (select TenTinhTrang from tb_TinhTrang where MaTinhTrang = ct.MaTinhTrang)
from tb_DonHang dh, tb_ChiTietDonHang ct, tb_KhachHang kh
where dh.idDonHang = ct.idDonHang  and kh.idKhachHang = dh.idKhachHang";
        if (sTuNgay != "")
            sql += " and NgayLap >= '" + StaticData.ConvertDDMMtoMMDD(sTuNgay) + " 00:00:00'";
        if (sDenNgay != "")
            sql += " and NgayLap <= '" + StaticData.ConvertDDMMtoMMDD(sDenNgay) + " 00:00:00'";
        //if (sMaDonHang != "")
        //    sql += " and MaDonHang like N'%" + sMaDonHang + "%'";
        if (sIdNguoiDung != "" && sIdNguoiDung != "0")
            sql += " and dh.idNguoiDung = '" + sIdNguoiDung + "'";

        if (sIdKhachHang != "")
            sql += " and dh.idKhachHang='" + sIdKhachHang + "'";
        //if (sTen != "")
        //    sql += " and (MaDonHang like N'%" + sTen + "%' or DiaChiNguoiNhan like N'%" + sTen + "%' or SoDienThoaiNguoiNhan like N'%" + sTen + "%' or NguoiNhan like N'%" + sTen + "%')";
        if (mQuyen.ToUpper() == "ADMIN")
        {


            if (sMaTinhTrang != "")
            {
                if (sMaTinhTrang == "XDVP")
                {
                    sql += " and (ct.MaTinhTrang = '" + sMaTinhTrang + "' or ChuyenPhatNhanh != 0)";
                    sql += " and idChiNhanhGui != '" + mIDChiNhanh + "'";
                }
                else
                {
                    sql += " and ct.MaTinhTrang = '" + sMaTinhTrang + "'";
                    sql += " and idChiNhanhGui != '" + mIDChiNhanh + "'";
                }
                if (sMaTinhTrang == "DGX")
                    sql += "and idChiNhanhNhan != '" + mIDChiNhanh + "'";
                if (sMaTinhTrang == "DVC")
                {
                    sql += "and idChiNhanhGui != '" + mIDChiNhanh + "'";
                }
            }
 
            if (sChiNhanhNhan != "")
                sql += " and idChiNhanhNhan = '" + sidChiNhanhNhan + "'";
            if (sIsDaHoanThanh != "")
                sql += " and isnull(isDaNhanTien,'False') = '" + sIsDaHoanThanh + "'";
            sql += @") as tb1 where 1 = 1 ";
          
        }

    else
        {
            if (sMaTinhTrang != "")
            {
                if (sMaTinhTrang == "XDVP")
                {
                    sql += " and (ct.MaTinhTrang = '" + sMaTinhTrang + "' or ChuyenPhatNhanh != 0)";
                    sql += " and idChiNhanhGui != '" + mIDChiNhanh + "'";
                }
                if (sMaTinhTrang == "DHT")
                {
                    sql += " and ct.MaTinhTrang = '" + sMaTinhTrang + "'";
                    sql += " and idChiNhanhGui != '" + mIDChiNhanh + "'";
                }
                if (sMaTinhTrang == "DGX")
                {
                    sql += " and ct.MaTinhTrang = '" + sMaTinhTrang + "'";
                }
                if (sMaTinhTrang == "DVC")
                {
                    sql += " and (MaTinhTrang = '" + sMaTinhTrang + "' or  (idChiNhanhGui !=  '" + mIDChiNhanh + "' and MaTinhTrang = 'DGX' and  idChiNhanhNhan =  '" + mIDChiNhanh + "' ))";
                    //sql += "and idChiNhanhGui != '" + mIDChiNhanh + "'";
                }
            }
           
            if (sChiNhanhNhan != "")
                sql += " and idChiNhanhNhan = '" + sidChiNhanhNhan + "'";
            if (sIsDaHoanThanh != "")
                sql += " and isnull(isDaNhanTien,'False') = '" + sIsDaHoanThanh + "'";
            sql += @") as tb1 where 1 = 1 ";
     
                sql += " and ( idChiNhanhGui =  '" + mIDChiNhanh + "' or idChiNhanhNhan = '" + mIDChiNhanh + "' or ViTri = '" + mIDChiNhanh + "')";
        }
        
        sql += " and RowNumber BETWEEN (" + Page + " - 1) * " + PageSize + " + 1 AND (((" + Page + " - 1) * " + PageSize + " + 1) + " + PageSize + ") - 1";


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
                                    Tên người nhận
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                  Chi nhánh nhận
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    SDT người nhận
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                  Địa chỉ
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                   Thanh Toán
                                </th>
                                 <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                  Tình trạng đơn
                                </th>
                                
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                  Chuyển Phát Nhanh
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                   COD
                                </th>
                                 <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                  Tổng Cước
                                </th>
                               
                            </tr>";
        html += "           <tr style='background: #ff7423;'>";
        html += "               <td colspan='11' style='text-align:center;vertical-align: inherit;'><b>Tổng cộng (" + SoHoaDon + " đơn)</b></td>";
     //   html += "               <td style='text-align:center;vertical-align: inherit;'><b>" + TongTienHang.ToString("N0").Replace(",", ".") + "</b></td>";
        html += "               <td style='text-align:center;vertical-align: inherit;'><b>" + TongTienCuoc.ToString("N0").Replace(",", ".") + "</b></td>";
     //   html += "               <td style='text-align:center;vertical-align: inherit;'><b>" + TongPhuPhi.ToString("N0").Replace(",", ".") + "</b></td>";
     //   html += "               <td style='text-align:center;vertical-align: inherit;'><b>" + (TongTienHang + TongTienCuoc + TongPhuPhi).ToString("N0").Replace(",", ".") + "</b></td>";
        html += "           </tr>";
        if (table.Rows.Count > 0)
        {
            int dem = 1;
            string STT = table.Rows[0]["RowNumber"].ToString();
            string htmlct = "";
            for (int i = 0; i <= table.Rows.Count; i++)
            {
                
                //string idDonHang = table.Rows[i]["idDonHang"].ToString();
                
                if (STT != (i < table.Rows.Count ? table.Rows[i]["RowNumber"].ToString() : "-1") || i == table.Rows.Count)
                {
                    int j = table.Rows.Count == 1 ? 0 : i - 1;
                    string idChiNhanhNhan = table.Rows[j]["idChiNhanhNhan"].ToString();
                    string Mau = (idChiNhanhNhan != mIDChiNhanh ? "" : "style='background: #dfdcf5;'");

                    html += "   <tr " + Mau + " onclick=XemChiTiet(\"" + table.Rows[j]["idDonHang"] + "\") style='cursor:pointer'>";
                    html += "       <td style='text-align:center;vertical-align: inherit;'>" + dem + "</td>";
                    html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[j]["MaDonHang"].ToString() + "</td>";
                    html += "       <td style='text-align:center;vertical-align: inherit;'>" + DateTime.Parse(table.Rows[j]["NgayLap"].ToString()).ToString("dd/MM/yyyy") + "</td>";

                    html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[j]["TenKhachHang"].ToString() + "</td>";
                    html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[j]["CNNhan"].ToString() + "</td>";
                    html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[j]["SoDienThoai"].ToString() + "</td>";
                    html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[j]["DiaChi"].ToString() + "</td>";
                    html += "       <td style='text-align:center;vertical-align: inherit;'>" + (table.Rows[j]["NguoiNhanTra"].ToString() == "True" ? "Người nhận trả" : "Người gửi trả") + "</td>";

                    html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[j]["TenTT"].ToString() + "</td>";

                    double PhiCOD = double.Parse(table.Rows[j]["PhiCOD"].ToString());

                    if (double.Parse(table.Rows[j]["ChuyenPhatNhanh"].ToString()) > 0)
                    {
                        double Cuoc = (table.Rows[j]["CuocChuyenPhatNhanh"].ToString() == "" ? 0 : double.Parse(table.Rows[j]["CuocChuyenPhatNhanh"].ToString()));
                        double Tien = (table.Rows[j]["ChuyenPhatNhanh"].ToString() == "" ? 0 : double.Parse(table.Rows[j]["ChuyenPhatNhanh"].ToString()));
                        html += "       <td style='text-align: left;'><input type='checkbox' id='ckbDaNhanTien_" + table.Rows[j]["idDonHang"].ToString() + "' checked='true' disabled='disabled'/>  CPN " + Tien.ToString("N0").Replace(",", ".") + " đ <br> Tiền Cước : " + Cuoc.ToString("N0").Replace(",", ".") + " đ</td>";

                        html += "       <td style='text-align:center;vertical-align: inherit;'>" + PhiCOD.ToString("N0").Replace(",", ".") + "</td>";
                        html += "       <td style='text-align:center;vertical-align: inherit;'>" + Cuoc.ToString("N0").Replace(",", ".") + "</td>";
                    }
                    else
                    {
                        html += "       <td style='text-align: center;'><input type='checkbox' id='ckbDaNhanTien_" + table.Rows[j]["idDonHang"].ToString() + "' disabled='disabled'/></td>";
                        double TongCuoc = 0;
                        //double TongCuoc = (table.Rows[i]["Tong"].ToString() == "" ? 0 : double.Parse(table.Rows[i]["Tong"].ToString()));
                        html += "       <td style='text-align:center;vertical-align: inherit;'>" + PhiCOD.ToString("N0").Replace(",", ".") + "</td>";
                        html += "       <td style='text-align:center;vertical-align: inherit;'>" + TongCuoc.ToString("N0").Replace(",", ".") + "</td>";
                    }

                    html += "   </tr>";
                    html += "   <tr hidden id='tr_" + table.Rows[j]["idDonHang"] + "' style='background: #eeeeee;'>";
                    html += "       <td colspan='13' style='border: 1px solid;'>";
                    html += "           <div>";
                    html += "               <ul class='nav nav-tabs' style='margin:inherit; border:none'>";
                    html += "                   <li class='active'><a data-toggle='tab' href='#HoaDon1_" + table.Rows[j]["idDonHang"] + "'> <span style='padding-right:12px;font-weight:bold'> THÔNG TIN </span></a>    </li>";
                    html += "               </ul>";
                    html += "           </div>";
                    html += "           <div class='tab-content'>";
                    html += "               <div id='HoaDon1_" + table.Rows[j]["idDonHang"] + "' class='tab-pane fade in active' style='background-color:white;padding: 10px;'>";
                    html += "                   <div class='container-fluid'>";
                    html += "                       <table id='tb-danhsachSP' class='table table-bordered table-hover dataTable'>";
                    html += "                           <thead style='white-space: nowrap;'>";
                    html += "                               <tr id='tr_SanPham'>";
                    html += "                               <th class='th' style='background-color: #1c9c13; font-size: 15px; color: white;'>STT</th>";
                    html += "                              <th class='th' style='background-color: #1c9c13; font-size: 15px; color: white;'>Tên Hàng Hóa</th>";
                    html += "                              <th class='th' style='background-color: #1c9c13; font-size: 15px; color: white;'>Mã Hàng Hóa</th>";
                    html += "                              <th class='th' style='background-color: #1c9c13; font-size: 15px; color: white;'>Vị Trí</th>";
                    html += "                              <th class='th' style='background-color: #1c9c13; font-size: 15px; color: white;'>Số Lượng</th>";
                    html += "                              <th class='th' style='background-color: #1c9c13; font-size: 15px; color: white;'>Đơn Giá</th>";
                    html += "                              <th class='th' style='background-color: #1c9c13; font-size: 15px; color: white;'>Tình Trạng</th>";
                    html += "                              <th class='th' style='background-color: #1c9c13; font-size: 15px; color: white;'>In Tem</th>";

                    html += "                           </tr>";
                    html += "                       </thead >";
                    html += "                       <tbody id='danhSachSPChon' runat='server'>";

                    html += htmlct;
                    html += "</tbody>";
                    html += "                        </table>";
                    html += "                       <div class='container-fluid' >";
                    html += "                           <div class='span12 CanhPhai' style='padding-top: 10px;'>";

                    if (table.Rows[j]["MaTinhTrang"].ToString() != "DHT")
                    {
                        html += "           <a class='btn btn-primary' title='Sửa đơn hàng' onclick='window.location=\"ThanhToan-DonHang.aspx?Page=" + Page.ToString() + "&idDonHang=" + table.Rows[j]["idDonHang"].ToString() + "\"'><i class='fa fa-money'></i><span> Trả hàng</span></a>";
                    }
                    //    html += "          <a class='btn btn-primary' href='#'  onclick='window.location=\"ThanhToan-DonHang.aspx?Page=" + Page.ToString() + "&idDonHang=" + table.Rows[i]["idDonHang"].ToString() + "\")'><i class='fa fa-money'> Thanh Toán</i></a>    ";
                    //html += "       <td style='text-align:center;vertical-align: inherit;'>";
                    html += "          <a class='btn btn-primary' href='#' onclick='PrinfHoaDon1(\"" + table.Rows[j]["idDonHang"] + "\")'><i class='fa fa-print'> In đơn hàng</i></a>    ";
                    //html += "       </td>";
                    // html += "           <a class='btn btn-default' title='In đơn hàng' onclick='PrinfHoaDon1(\"" + table.Rows[i]["idDonHang"].ToString() + "\")'><i class='fa fa-print'></i><sapn> In đơn hàng</span></a>";
                    if (mQuyen.ToUpper() == "ADMIN" && mQuyen.ToUpper() != "KH" && mQuyen.ToUpper() != "NVGN")
                    {
                        //html += "           <a class='btn btn-primary' title='Sửa đơn hàng' onclick='window.location=\"QuanLyDonhang-CapNhat.aspx?Page=" + Page.ToString() + "&idDonHang=" + table.Rows[j]["idDonHang"].ToString() + "\"'><i class='fa fa-edit'></i><span> Cập nhật</span></a>";
                        //html += "           <a class='btn btn-danger' title='Xóa đơn hàng' onclick='DeleteDonHang(\"" + table.Rows[j]["idDonHang"].ToString() + "\")'><i class='fa fa-trash'></i><span> Xóa</span></a>";

                    }
                    else
                    {
                        if (mQuyen.ToUpper() == "NVVP" || mQuyen.ToUpper() == "NVGN" || mQuyen.ToUpper() == "KH")
                        {
                            // html += "       <a style='cursor:pointer' title='Sửa đơn hàng' onclick='window.location=\"QuanLyDonhang-CapNhat.aspx?Page=" + Page.ToString() + "&idDonHang=" + table.Rows[i]["idDonHang"].ToString() + "\"'><i class='fa fa-edit'></i><span> Cập nhật</span></a>";
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

                    htmlct = "";
                    dem++;
                    if (i < table.Rows.Count)
                    STT = table.Rows[i]["RowNumber"].ToString();
                }
                if (i < table.Rows.Count)
                {
                    htmlct += "<tr>";
                    htmlct += "       <td style='text-align:center;vertical-align: inherit;'>" + (i + 1) + "</td>";

                    htmlct += "<td style='text-align:center;vertical-align: inherit;'> " + table.Rows[i]["TenHH"].ToString() + "</td>";
                    htmlct += "<td style='text-align:center;vertical-align: inherit;'> " + table.Rows[i]["MaHH"].ToString() + "</td>";
                    htmlct += "<td style='text-align:center;vertical-align: inherit;'>" + (table.Rows[i]["MaTinhTrang"].ToString() == "DVC" ? table.Rows[i]["BienSoXe"].ToString() : table.Rows[i]["TenCN"].ToString()) + "</td>";
                    htmlct += "<td style='text-align:center;vertical-align: inherit;'> " + table.Rows[i]["SoLuong"].ToString() + "</td>";
                    double DonGia = 0;
                    DonGia = (table.Rows[i]["DonGia"].ToString() == "" ? 0 : double.Parse(table.Rows[i]["DonGia"].ToString()));
                    htmlct += "<td style='text-align:center;vertical-align: inherit;'> " + DonGia.ToString("N0").Replace(",", ".") + "</td>";
                    htmlct += "<td style='text-align:center;vertical-align: inherit;'> " + table.Rows[i]["TenTT"].ToString() + "</td>";
                    //     html += "<td style='text-align:center;vertical-align: inherit;'> " + tbCTDH.Rows[j]["SDTNguoiNhan"].ToString() + "</td>";
                    htmlct += "       <td style='text-align:center;vertical-align: inherit;'>";
                    htmlct += "          <a href='#'  onclick='PrinfHoaDonTem(" + table.Rows[i]["idHangHoa"] + "," + table.Rows[i]["idDonHang"] + ")'><i class='fa fa-print'> In Tem</i></a>    ";
                    htmlct += "       </td>";
                    htmlct += "</tr>";
                }
            }
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
        if (sChiNhanhNhan != "")
            url += "idChiNhanhNhan=" + hdidChiNhanhNhan + "&";
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
        string TuNgay = txtTuNgay.Value.Trim();
        string DenNgay = txtDenNgay.Value.Trim();
        //string MaDonHang = txtMaDonHang.Value.Trim();
        string idNguoiDung = slNhanVienGiao.Value.Trim();
        string idKhachHang = hdIdKhachHang.Value.Trim();
        string Ten = txtTenKhachHang.Value.Trim();
        string MaTinhTrang = slTinhTrang.Value.Trim();
        string idTinh = slTinh.SelectedValue.Trim();
        string idHuyen = slHuyen.SelectedValue.Trim();
        //string DiaChiNguoiNhan = txtDiaChiNguoiNhan.Value.ToString();
        string ChiNhanhNhan = txtChiNhanhNhan.Value.Trim();
        string url = "QuanLyDonHang.aspx?";
        if (TuNgay != "")
            url += "TuNgay=" + TuNgay + "&";
        if (DenNgay != "")
            url += "DenNgay=" + DenNgay + "&";
        //if (MaDonHang != "")
        //    url += "MaDonHang=" + MaDonHang + "&";
        if (idNguoiDung != "" && idNguoiDung != "0")
            url += "idNguoiDung=" + idNguoiDung + "&";
        if (idKhachHang != "" && idKhachHang != "0")
            url += "idKhachHang=" + idKhachHang + "&";
        else if(Ten != "")
            url += "Ten=" + Ten + "&";
        if (MaTinhTrang != "" && MaTinhTrang != "0")
            url += "MaTinhTrang=" + MaTinhTrang + "&";
        if (idTinh != "" && idTinh != "0")
            url += "idTinh=" + idTinh + "&";
        if (idHuyen != "" && idHuyen != "0")
            url += "idHuyen=" + idHuyen + "&";
        //if (DiaChiNguoiNhan != "")
        //    url += "DiaChiNguoiNhan=" + DiaChiNguoiNhan + "&";
        if (ChiNhanhNhan != "")
            url += "ChiNhanhNhan=" + ChiNhanhNhan + "&";
        Response.Redirect(url);
    }

    protected void btTimKiem_Click1(object sender, EventArgs e)
    {
        string TuNgay = txtTuNgay.Value.Trim();
        string DenNgay = txtDenNgay.Value.Trim();
        //string MaDonHang = txtMaDonHang.Value.Trim();
        string idNguoiDung = slNhanVienGiao.Value.Trim();
        string idKhachHang = hdIdKhachHang.Value.Trim();
        string Ten = txtTenKhachHang.Value.Trim();
        string MaTinhTrang = "DVC";
       
       
        //string DiaChiNguoiNhan = txtDiaChiNguoiNhan.Value.ToString();
        string ChiNhanhNhan = txtChiNhanhNhan.Value.Trim();
        string url = "QuanLyDonHang.aspx?";
        if (TuNgay != "")
            url += "TuNgay=" + TuNgay + "&";
        if (DenNgay != "")
            url += "DenNgay=" + DenNgay + "&";
        //if (MaDonHang != "")
        //    url += "MaDonHang=" + MaDonHang + "&";
        if (idNguoiDung != "" && idNguoiDung != "0")
            url += "idNguoiDung=" + idNguoiDung + "&";
        if (idKhachHang != "" && idKhachHang != "0")
            url += "idKhachHang=" + idKhachHang + "&";
        else if (Ten != "")
            url += "Ten=" + Ten + "&";
        if (MaTinhTrang != "" && MaTinhTrang != "0")
            url += "MaTinhTrang=" + MaTinhTrang + "&";
      
        //if (DiaChiNguoiNhan != "")
        //    url += "DiaChiNguoiNhan=" + DiaChiNguoiNhan + "&";
        if (ChiNhanhNhan != "")
            url += "ChiNhanhNhan=" + ChiNhanhNhan + "&";
        Response.Redirect(url);
    }

    protected void btTimKiem_Click2(object sender, EventArgs e)
    {
        string TuNgay = txtTuNgay.Value.Trim();
        string DenNgay = txtDenNgay.Value.Trim();
        //string MaDonHang = txtMaDonHang.Value.Trim();
        string idNguoiDung = slNhanVienGiao.Value.Trim();
        string idKhachHang = hdIdKhachHang.Value.Trim();
        string Ten = txtTenKhachHang.Value.Trim();
        string MaTinhTrang = "DGX";


        //string DiaChiNguoiNhan = txtDiaChiNguoiNhan.Value.ToString();
        string ChiNhanhNhan = txtChiNhanhNhan.Value.Trim();
        string url = "QuanLyDonHang.aspx?";
        if (TuNgay != "")
            url += "TuNgay=" + TuNgay + "&";
        if (DenNgay != "")
            url += "DenNgay=" + DenNgay + "&";
        //if (MaDonHang != "")
        //    url += "MaDonHang=" + MaDonHang + "&";
        if (idNguoiDung != "" && idNguoiDung != "0")
            url += "idNguoiDung=" + idNguoiDung + "&";
        if (idKhachHang != "" && idKhachHang != "0")
            url += "idKhachHang=" + idKhachHang + "&";
        else if (Ten != "")
            url += "Ten=" + Ten + "&";
        if (MaTinhTrang != "" && MaTinhTrang != "0")
            url += "MaTinhTrang=" + MaTinhTrang + "&";

        //if (DiaChiNguoiNhan != "")
        //    url += "DiaChiNguoiNhan=" + DiaChiNguoiNhan + "&";
        if (ChiNhanhNhan != "")
            url += "ChiNhanhNhan=" + ChiNhanhNhan + "&";
        Response.Redirect(url);
    }

    protected void btTimKiem_Click3(object sender, EventArgs e)
    {
        string TuNgay = txtTuNgay.Value.Trim();
        string DenNgay = txtDenNgay.Value.Trim();
        string idKhachHang = hdIdKhachHang.Value.Trim();
        string Ten = txtTenKhachHang.Value.Trim();
        string MaTinhTrang = "XDVP";


      
        string ChiNhanhNhan = txtChiNhanhNhan.Value.Trim();
        string url = "QuanLyDonHang.aspx?";
        if (TuNgay != "")
            url += "TuNgay=" + TuNgay + "&";
        if (DenNgay != "")
            url += "DenNgay=" + DenNgay + "&";
        if (idKhachHang != "" && idKhachHang != "0")
            url += "idKhachHang=" + idKhachHang + "&";
        else if (Ten != "")
            url += "Ten=" + Ten + "&";
        if (MaTinhTrang != "" && MaTinhTrang != "0")
            url += "MaTinhTrang=" + MaTinhTrang + "&";

        //if (DiaChiNguoiNhan != "")
        //    url += "DiaChiNguoiNhan=" + DiaChiNguoiNhan + "&";
        if (ChiNhanhNhan != "")
            url += "ChiNhanhNhan=" + ChiNhanhNhan + "&";
        Response.Redirect(url);
    }

    protected void btTimKiem_Click4(object sender, EventArgs e)
    {
        string TuNgay = txtTuNgay.Value.Trim();
        string DenNgay = txtDenNgay.Value.Trim();
        string idKhachHang = hdIdKhachHang.Value.Trim();
        string Ten = txtTenKhachHang.Value.Trim();
        string MaTinhTrang = "DHT";
        string ChiNhanhNhan = txtChiNhanhNhan.Value.Trim();
        string url = "QuanLyDonHang.aspx?";
        if (TuNgay != "")
            url += "TuNgay=" + TuNgay + "&";
        if (DenNgay != "")
            url += "DenNgay=" + DenNgay + "&";
       
        if (idKhachHang != "" && idKhachHang != "0")
            url += "idKhachHang=" + idKhachHang + "&";
        else if (Ten != "")
            url += "Ten=" + Ten + "&";
        if (MaTinhTrang != "" && MaTinhTrang != "0")
            url += "MaTinhTrang=" + MaTinhTrang + "&";

        //if (DiaChiNguoiNhan != "")
        //    url += "DiaChiNguoiNhan=" + DiaChiNguoiNhan + "&";
        if (ChiNhanhNhan != "")
            url += "ChiNhanhNhan=" + ChiNhanhNhan + "&";
        Response.Redirect(url);
    }

    protected void btXemTatCa_Click(object sender, EventArgs e)
    {
        string url = "QuanLyDonHang.aspx";
        Response.Redirect(url);
    }

    protected void slTinh_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadHuyen(slTinh.SelectedValue.Trim());
    }
    
}