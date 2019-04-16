using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class QuanLyDonHang_QuanLyDonHang_CapNhat : System.Web.UI.Page
{
    string sIdPhanHangLenXe = "";
    string Page = "";
    string mTenDangNhap = "";
    string mQuyen = "";
    string mIdNguoiDung = "";
    string mIdKhachHang = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["QuanLyCongNoAnhKiet_Login"] != null && Request.Cookies["QuanLyCongNoAnhKiet_Login"].Value.Trim() != "")
        {
            mTenDangNhap = Request.Cookies["QuanLyCongNoAnhKiet_Login"].Value.Trim();
            mIdNguoiDung = StaticData.getField("tb_NguoiDung", "idNguoiDung", "TenDangNhap", mTenDangNhap);
            mIdKhachHang = StaticData.getField("tb_KhachHang", "idKhachHang", "TenDangNhap", mTenDangNhap);
            mQuyen = MyStaticData.GetMaQuyen(mTenDangNhap);
            //if (mQuyen.ToUpper() != "ADMIN" && mQuyen.ToUpper() != "NVGN" && mQuyen.ToUpper() != "NVVP" && mQuyen.ToUpper() != "KH" )
            //{
            //    Response.Redirect("../Home/DangNhap.aspx");
            //}
        }
        try
        {
            sIdPhanHangLenXe = StaticData.ValidParameter(Request.QueryString["idHangVanPhong"].Trim());
        }
        catch { }
        try
        {
            Page = StaticData.ValidParameter(Request.QueryString["Page"].Trim());
        }
        catch { }
        if (!IsPostBack)
        {
            slLoaiCuoc.Disabled = true;
            LoadTinh();
            
            txtNgayLap.Value = DateTime.Now.ToString("dd/MM/yyyy");
            txtNgayLap.Disabled = true;
            LoadLoaiCuoc();
            LoadLoaiSanPham1();
            //LoadKho();
            LoadNhanVienGiao();
            LoadTinhTrang();
            if (mQuyen.ToUpper() == "KH")
            {
              ///  hdIdKhachHang.Value = mIdKhachHang;
                txtChonXe.Value = StaticData.getField("tb_KhachHang", "TenKhachHang", "idKhachHang", mIdKhachHang);
                txtChonXe.Disabled = true;
                txtMaDonHang.Value = mIdKhachHang + MyStaticData.TaoMaDonHang();

                slNhanVienGiao.Disabled = true;
                txtNgayDuKienGiao.Disabled = true;
                txtGioDuKienGiao.Disabled = true;
                txtPhutDuKienGiao.Disabled = true;
                //slKho.Disabled = true;
                //slLoaiCuoc.Disabled = true;
                slTinhTrangDonHang.Disabled = true;
                slTinhTrangDonHang.Value = "CXL";
            }
            txtMaDonHang.Value = MyStaticData.TaoMaDonHang();
            LoadThongTinDonHang();

        }
    }
    private void LoadTinh()
    {
        string strSql = "select * from tb_Tinh";
        slTinh.DataSource = Connect.GetTable(strSql);
        slTinh.DataTextField = "TenTinh";
        slTinh.DataValueField = "idTinh";
        slTinh.DataBind();
        slTinh.Items.Add(new ListItem("Chọn Tỉnh/TP", "0"));
        slTinh.Items.FindByText("Chọn Tỉnh/TP").Selected = true;
    }
    private void LoadHuyen(string idTinh)
    {
        string strSql = "select * from tb_Huyen where idTinh='" + idTinh + "'";
        slHuyen.DataSource = Connect.GetTable(strSql);
        slHuyen.DataTextField = "TenHuyen";
        slHuyen.DataValueField = "idHuyen";
        slHuyen.DataBind();
        slHuyen.Items.Add(new ListItem("Chọn Quận/Huyện", "0"));
        slHuyen.Items.FindByText("Chọn Quận/Huyện").Selected = true;
    }
    private void LoadLoaiSanPham1()
    {
        string strSql = "select * from tb_LoaiSanPham";
        slLoaiSanPham1.DataSource = Connect.GetTable(strSql);
        slLoaiSanPham1.DataTextField = "TenLoaiSanPham";
        slLoaiSanPham1.DataValueField = "idLoaiSanPham";
        slLoaiSanPham1.DataBind();
        slLoaiSanPham1.Items.Add(new ListItem("-- Chọn --", "0"));
        slLoaiSanPham1.Items.FindByText("-- Chọn --").Selected = true;
    }
    private void LoadLoaiCuoc()
    {
        string strSql = "select * from tb_LoaiCuoc";
        slLoaiCuoc.DataSource = Connect.GetTable(strSql);
        slLoaiCuoc.DataTextField = "TenLoaiCuoc";
        slLoaiCuoc.DataValueField = "MaLoaiCuoc";
        slLoaiCuoc.DataBind();
        slLoaiCuoc.Items.Add(new ListItem("Loại cước", "0"));
        slLoaiCuoc.Items.FindByText("Loại cước").Selected = true;
    }
    private void LoadTinhTrang()
    {
        string strSql = "select * from tb_TinhTrang";
        slTinhTrangDonHang.DataSource = Connect.GetTable(strSql);
        slTinhTrangDonHang.DataTextField = "TenTinhTrang";
        slTinhTrangDonHang.DataValueField = "MaTinhTrang";
        slTinhTrangDonHang.DataBind();
        slTinhTrangDonHang.Items.Add(new ListItem("-- Chọn --", "0"));
        slTinhTrangDonHang.Items.FindByText("-- Chọn --").Selected = true;
    }

    private void LoadNhanVienGiao()
    {
        string strSql = "select * from tb_NguoiDung where MaQuyen='NVGN'";
        slNhanVienGiao.DataSource = Connect.GetTable(strSql);
        slNhanVienGiao.DataTextField = "HoTen";
        slNhanVienGiao.DataValueField = "idNguoiDung";
        slNhanVienGiao.DataBind();
        slNhanVienGiao.Items.Add(new ListItem("-- Chọn --", "0"));
        slNhanVienGiao.Items.FindByText("-- Chọn --").Selected = true;
    }

    //private void LoadKho()
    //{
    //    string strSql = "select * from tb_Kho";
    //    slKho.DataSource = Connect.GetTable(strSql);
    //    slKho.DataTextField = "TenKho";
    //    slKho.DataValueField = "idKho";
    //    slKho.DataBind();
    //    slKho.Items.Add(new ListItem("-- Chọn --", "0"));
    //    slKho.Items.FindByText("-- Chọn --").Selected = true;
    //}
    private void LoadThongTinDonHang()
    {
        if (mQuyen.ToUpper() != "ADMIN" && mQuyen.ToUpper() != "NVVP")
        {
            ckbDaNhanTien.Disabled = true;
            ckbDaNhanTien.Visible = false;
        }
        if (sIdPhanHangLenXe != "")
        {
            string sql = "select * from tb_PhanHangLenXe ph, tb_Xe  where IdPhanHangLenXe='" + sIdPhanHangLenXe + "' and ph.idXe = tb_Xe.idXe";
            DataTable table = Connect.GetTable(sql);
            if (table.Rows.Count > 0)
            {
                if (mQuyen.ToUpper() == "KH" && mIdKhachHang != table.Rows[0]["idKhachHang"].ToString())
                {
                    Response.Redirect("../Home/DangNhap.aspx");
                }
                dvTitle.InnerHtml = "SỬA THÔNG TIN ĐƠN HÀNG";
                btLuu.Text = "SỬA";
                txtMaDonHang.Value = table.Rows[0]["MaTuyen"].ToString();
                txtNgayLap.Value = DateTime.Parse(table.Rows[0]["NgayLap"].ToString()).ToString("dd/MM/yyyy");


                hdIdLoaiXe.Value = table.Rows[0]["IDXe"].ToString();
                string TenKH =  table.Rows[0]["BienSoXe"].ToString();
                txtChonXe.Value = TenKH;
                txtTaiXe.Value = StaticData.getField("tb_NguoiDung", "HoTen", "idNguoiDung", table.Rows[0]["idNguoiDung"].ToString());

                string sql1 = @"select ct.idChiTietDonHang,dh.MaDonHang, hh.TenHangHoa, ct.SoLuong, MaTinhTrang  from tb_ChiTietDonHang ct, tb_DonHang dh, tb_HangHoa hh where 
                ct.idDonHang = dh.idDonHang and ct.idHangHoa = hh.idHangHoa and IdPhanHangLenXe ='" + sIdPhanHangLenXe + "'";

                DataTable data = Connect.GetTable(sql1);
                //SetPage();
                loadSP(data);
                //// hdIdKhachHang.Value = table.Rows[0]["idKhachHang"].ToString();
               // //btHuy.Text = "DSĐH CỦA " + TenKH.ToUpper();
               // //slKho.Value = table.Rows[0]["idKho"].ToString();
               // if (mQuyen.ToUpper() == "NVGN")
               // {
               //     if(mIdNguoiDung != table.Rows[0]["idNguoiDung"].ToString())
               //     {
               //         Response.Redirect("../Home/DangNhap.aspx");
               //     }
               //     slNhanVienGiao.Value = mIdNguoiDung;
               //     slNhanVienGiao.Disabled = true;
               // }
               // else
               // {
               //     slNhanVienGiao.Value = table.Rows[0]["idNguoiDung"].ToString();
               // }
               // if (table.Rows[0]["ThoiDiemDuKienGiao"].ToString() != "")
               // {
               //     txtNgayDuKienGiao.Value = DateTime.Parse(table.Rows[0]["ThoiDiemDuKienGiao"].ToString()).ToString("dd/MM/yyyy");
               //     txtGioDuKienGiao.Value = DateTime.Parse(table.Rows[0]["ThoiDiemDuKienGiao"].ToString()).Hour.ToString();
               //     txtPhutDuKienGiao.Value = DateTime.Parse(table.Rows[0]["ThoiDiemDuKienGiao"].ToString()).Minute.ToString();
               // }
               // slTinhTrangDonHang.Value = table.Rows[0]["MaTinhTrang"].ToString();
               // txtGhiChu.Value = table.Rows[0]["GhiChu"].ToString();
               // txtNguoiNhan.Value = table.Rows[0]["NguoiNhan"].ToString();
               // txtDiaChiNguoiNhan.Value = table.Rows[0]["DiaChiNguoiNhan"].ToString();
               // txtSoDienThoaiNguoiNhan.Value = table.Rows[0]["SoDienThoaiNguoiNhan"].ToString();
               // slLoaiCuoc.Value = table.Rows[0]["MaLoaiCuoc"].ToString();
               // slTinh.SelectedValue = table.Rows[0]["idTinh"].ToString();
               // LoadHuyen(table.Rows[0]["idTinh"].ToString());
               // slHuyen.SelectedValue = table.Rows[0]["idHuyen"].ToString();
               // double TienHang = table.Rows[0]["TienHang"].ToString() == "" ? 0 : double.Parse(table.Rows[0]["TienHang"].ToString());
               // txtTienHang.Value = TienHang.ToString("N0").Replace(",",".");
               // double TienCuoc = table.Rows[0]["TienCuoc"].ToString() == "" ? 0 : double.Parse(table.Rows[0]["TienCuoc"].ToString());
               // txtTienCuoc.Value = TienCuoc.ToString("N0").Replace(",", ".");
               // double PhuPhi = table.Rows[0]["PhuPhi"].ToString() == "" ? 0 : double.Parse(table.Rows[0]["PhuPhi"].ToString());
               // txtPhuPhi.Value = PhuPhi.ToString("N0").Replace(",", ".");
               // txtTongTien.Value = (TienHang + TienCuoc + PhuPhi).ToString("N0").Replace(",", ".");
               // slGoiDichVu.Value = table.Rows[0]["GoiDichVu"].ToString();
               // txtThongTinBuuGui.Value = table.Rows[0]["ThongTinBuuGui"].ToString().Trim();
               // if (table.Rows[0]["isDaNhanTien"].ToString() == "True")
               //     ckbDaNhanTien.Checked = true;
               // dvButtonChiTietDonHang.InnerHtml = "<input id='btCapNhatChiTietDonHang' type='button' value='Thêm' onclick='CapNhatChiTietDonHang(\"THÊM\", \"" + table.Rows[0]["idDonHang"].ToString() + "\",\"\")' class='btn btn-primary btn-flat' />";
               // //dvChiTietDonHang.Style.Add("display", "block");
               // txtChonXe.Disabled = true;
               // if (table.Rows[0]["NguoiNhanTra"].ToString() == "True")
               //     radiNguoiNhanTra.Checked = true;
               // if (mQuyen.ToUpper() == "NVGN")
               // {
               //     txtMaDonHang.Disabled = true;
               //     //slKho.Disabled = true;
               //     txtNguoiNhan.Disabled = true;
               //     txtDiaChiNguoiNhan.Disabled = true;
               //     txtSoDienThoaiNguoiNhan.Disabled = true;
               //     slLoaiCuoc.Disabled = true;
               //     slTinh.Enabled = false;
               //     slHuyen.Enabled = false;
               //     slNhanVienGiao.Disabled = true;
               //     txtNgayDuKienGiao.Disabled = true;
               //     txtGioDuKienGiao.Disabled = true;
               //     txtPhutDuKienGiao.Disabled = true;
               //     txtGhiChu.Disabled = true;
               // }
                
            }
        }
        else
        {
            if (mQuyen.ToUpper() == "NVGN")
            {
                //slNhanVienGiao.Value = mIdNguoiDung;
                //slNhanVienGiao.Disabled = true;
                Response.Redirect("../Home/DangNhap.aspx");
            }
        }
    }

    protected void loadSP(DataTable data)
    {
        if (data.Rows.Count > 0)
        {
            string slistSP = "";

            string html = "";
            //double TongTien = 0;
            for (int i = 0; i < data.Rows.Count; i++)
            {
                string DonGia = "";
                string TenHH = "";


                slistSP += data.Rows[i]["idChiTietDonHang"] + "-" + data.Rows[i]["MaDonHang"] + "-" + data.Rows[i]["TenHangHoa"] + "-" + data.Rows[i]["SoLuong"] + "-" + data.Rows[i]["MaTinhTrang"];
                html += "<tr id='tr_" + data.Rows[i]["idChiTietDonHang"] + "'>";
                html += "<td>" + (i + 1) + "</td>";
                html += "     <td style='text-align:center;vertical-align: inherit;'>" + data.Rows[i]["MaDonHang"] + "</td>";
                html += "     <td style='text-align:center;vertical-align: inherit;'>" + data.Rows[i]["TenHangHoa"] + "</td>";
                html += "     <td style='text-align:center;vertical-align: inherit;'>" + data.Rows[i]["SoLuong"] + "</td>";
                html += "     <td style='text-align:center;vertical-align: inherit;'>" + StaticData.getField("tb_TinhTrang", "TenTinhTrang", "MaTinhTrang", data.Rows[i]["MaTinhTrang"].ToString()) + "</td>";
            //    html += "     <td style='text-align:center;vertical-align: inherit;'>" + StaticData.getField("tb_TinhTrang", "TenTinhTrang", "MaTinhTrang", data.Rows[i]["MaTinhTrang"].ToString()) + "</td>";
                //html += "       <td style='text-align:center'>" + data.Rows[i]["DonViTinh"] + "</td>";

                //html += "<td style='text-align:center'><a onclick=''><img class='imgedit' id='DeleteSP_" + data.Rows[i]["Id"] + "' src='../Images/delete.png'/></a></td>";
                html += "   <td style='text-align: center'><a style='cursor:pointer' onclick='XoaSanPham(\"" + data.Rows[i]["idChiTietDonHang"] + "\")'><i class='fa fa-trash'></i></a></td>";
                //html += "       <td style='text-align:center'>" + data.Rows[i]["HangGoi"] + "</td>";
                if (i < data.Rows.Count - 1)
                    slistSP += ",";


                html += "</tr>";
                //double ThanhTien = double.Parse(tb.Rows[0]["GiaCuoc"].ToString());
                //TongTien += ThanhTien;

            }
            // txtTongTien.Value = TongTien.ToString();
          //  txtTongTien.Value = TongTien.ToString("##,0").Replace(",", ".");

            //txtCKTien.Value = ((TongTien * ChietKhau) / 100).ToString();
            listSanPham.Value = slistSP;
            danhSachSPChon.InnerHtml = html;
        }
    }
    protected void btLuu_Click(object sender, EventArgs e)
    {
        string MaTuyen = "";
        string NgayLap = "";
        string idXe = "";
        //string idKho = "";
        string idNguoiDung = "";
        string NgayDuKienGiao = "";
        string GioDuKienGiao = "0";
        string PhutDuKienGiao = "0";
        string MaTinhTrang = "";
        string GhiChu = "";
        string NguoiNhan = "";
        string DiaChiNguoiNhan = "";
        string SoDienThoaiNguoiNhan = "";
        string MaLoaiCuoc = "";
        string idTinh = "";
        string idHuyen = "";
        string isDaNhanTien = ckbDaNhanTien.Checked.ToString();
        bool NguoiNhanTra = radiNguoiNhanTra.Checked;
        string TienHang = "";
        string TienCuoc = "";
        string PhuPhi = "";
        string GoiDichVu = "";
        string ThongTinBuuGui = "";
        //Mã đơn hàng
        //if (txtMaDonHang.Value.Trim() != "")
        //{
        //    /*if (sIdDonHang == "")
        //    {
        //        string sqlCheckDH = "select top 1 idDonHang from tb_DonHang where MaDonHang='" + txtMaDonHang.Value.Trim() + "'";
        //        DataTable tbCheckDH = Connect.GetTable(sqlCheckDH);
        //        if (tbCheckDH.Rows.Count > 0)
        //        {
        //            Response.Write("<script>alert('Mã đơn hàng đã tồn tại!')</script>");
        //            return;
        //        }
        //        else
        //        {
        //            MaDonHang = StaticData.ValidParameter(txtMaDonHang.Value.Trim());
        //        }
        //    }
        //    else
        //    {
        //        string sqlCheckDH = "select top 1 idDonHang from tb_DonHang where idDonHang!='" + sIdDonHang + "' and MaDonHang='" + txtMaDonHang.Value.Trim() + "'";
        //        DataTable tbCheckDH = Connect.GetTable(sqlCheckDH);
        //        if (tbCheckDH.Rows.Count > 0)
        //        {
        //            Response.Write("<script>alert('Mã đơn hàng đã tồn tại!')</script>");
        //            return;
        //        }
        //        else
        //        {
        //            MaDonHang = StaticData.ValidParameter(txtMaDonHang.Value.Trim());
        //        }
        //    }*/
        //}
        //else
        //{
        //    Response.Write("<script>alert('Bạn chưa nhập mã đơn hàng!')</script>");
        //    return;
        //}
        //Ngày lập
        MaTuyen = txtMaDonHang.Value.Trim();
      
            NgayLap = StaticData.ConvertDDMMtoMMDD(txtNgayLap.Value.Trim());
            idXe = hdIdLoaiXe.Value.Trim();
            string[] arraySPList = null;
            if (listSanPham.Value.Trim() == "" || listSanPham.Value.Trim() == null)
            {
                Response.Write("<script>alert('Bạn chưa nhập sản phẩm nào!')</script>");
               // temp = 1;
            }
            else
            {
                arraySPList = listSanPham.Value.Trim().Split(',');
                //MessageSanPham.Style.Add("display", "none");
            }
       
        //Khách hàng
      //  idKhachHang = hdIdKhachHang.Value.Trim();
        //if(idXe == "")
        //{
        //    Response.Write("<script>alert('Bạn chưa nhập số điện thoại người gửi!')</script>");
        //    return;
        //}
        ////Gói dịch vụ
      //  GoiDichVu = slGoiDichVu.Value.Trim();
      
        //////////
        if (sIdPhanHangLenXe == "")
        {
            string MaDonHang1 = MyStaticData.TaoMaDonHang();
            string sqlInsertDH = "insert into tb_HangVanPhong(MaTuyen,NgayLap,idXe,idNguoiDung)";
            sqlInsertDH += " values('" + MaTuyen + "','" + NgayLap + "','" + idXe + "','" + mIdNguoiDung + "'";


            sqlInsertDH += ")";
            bool ktInsertKH = Connect.Exec(sqlInsertDH);
           
            if (ktInsertKH)
            {
                string idChinhanhxuong = StaticData.getField("tb_NguoiDung", "ChiNhanh", "idNguoiDung", mIdNguoiDung);
                string idPhanHangLenXe = StaticData.getField("tb_HangVanPhong", "idHangVanPhong", "MaTuyen", MaTuyen);

                for (int i = 0; i < arraySPList.Length; i++)
                {

                    string[] arr = arraySPList[i].Split('-');
                                        string sqlcinhnhanh = @"select idChiNhanhNhan from tb_ChiTietDonHang ct, tb_DonHang dh where ct.idDonHang = dh.idDonHang and idChiTietDonHang = '" + arr[0] + "'";
                                       string idChiNhanhNhan = Connect.GetTable(sqlcinhnhanh).Rows[0]["idChiNhanhNhan"].ToString();
                                       string matingtrang = (idChinhanhxuong == idChiNhanhNhan ? arr[4] : "DGX");
                    string sqlUpdateKhachHang = "";
                sqlUpdateKhachHang += "update tb_ChiTietDonHang set";
               sqlUpdateKhachHang += " idHangVanPhong = '" + idPhanHangLenXe + "'";
               sqlUpdateKhachHang += " ,MaTinhTrang = N'" + matingtrang + "'";
               sqlUpdateKhachHang += " ,idNguoiDung = N'" + mIdNguoiDung + "'";
                sqlUpdateKhachHang += " where idChiTietDonHang ='" + arr[0] + "'";
                bool ktUpdateNguoiDung = Connect.Exec(sqlUpdateKhachHang);

                }
                



                //Response.Redirect("QuanLyDonHang-CapNhat.aspx?idDonHang=" + idDonHang);
                Response.Redirect("QuanLyHangTaiVanPhong.aspx" + (Page == "" ? "" : "?Page=" + Page));
            }
            else
            {
                Response.Write("<script>alert('Lỗi thêm đơn hàng, bạn vui lòng kiểm tra lại dữ liệu nhập!')</script>");
            }

        }
        else
        {
            MaTuyen = txtMaDonHang.Value;
            if (MaTuyen != "")
            {
                string sqlCheckDH = "select top 1 idDonHang from tb_DonHang where idDonHang!='" + sIdPhanHangLenXe + "' and MaDonHang='" + txtMaDonHang.Value.Trim() + "'";
                DataTable tbCheckDH = Connect.GetTable(sqlCheckDH);
                if (tbCheckDH.Rows.Count > 0)
                {
                    Response.Write("<script>alert('Mã đơn hàng đã tồn tại!')</script>");
                    return;
                }
                else
                {
                    MaTuyen = StaticData.ValidParameter(txtMaDonHang.Value.Trim());
                }
            }
            else
            {
                Response.Write("<script>alert('Mã đơn hàng không được trống')</script>");
                return;
            }
            string sqlUpdateDH = "";
            sqlUpdateDH += "update tb_PhanHangLenXe set";
            sqlUpdateDH += " NgayLap = '" + NgayLap + "'";
            sqlUpdateDH += " ,idNguoiDung = '" + mIdNguoiDung + "'";
            sqlUpdateDH += " ,idXe = '" + idXe + "'";
     
       //     sqlUpdateDH += " ,ThongTinBuuGui = N'" + ThongTinBuuGui + "'";
            sqlUpdateDH += " where idPhanHangLenXe ='" + sIdPhanHangLenXe + "'";
            bool ktUpdateKH = Connect.Exec(sqlUpdateDH);
            if (ktUpdateKH)
            {
                for (int i = 0; i < arraySPList.Length; i++)
                {
                    string[] arr = arraySPList[i].Split('-');
                    string sqlUpdateKhachHang = "";
                    sqlUpdateKhachHang += "update tb_ChiTietDonHang set";
                    sqlUpdateKhachHang += " IDPhanHangLenXe = '" + sIdPhanHangLenXe + "'";
                    sqlUpdateKhachHang += " ,MaTinhTrang = N'" + "DVC" + "'";
                    sqlUpdateKhachHang += " where idChiTietDonHang ='" + arr[0] + "'";
                    bool ktUpdateNguoiDung = Connect.Exec(sqlUpdateKhachHang);

                }




                //Response.Redirect("QuanLyDonHang-CapNhat.aspx?idDonHang=" + idDonHang);
                Response.Redirect("PhanHangLenXe.aspx" + (Page == "" ? "" : "?Page=" + Page));
            }
            else
            {
                Response.Write("<script>alert('Lỗi !')</script>");
            }
        }
    }
    protected void btHuy_Click(object sender, EventArgs e)
    {
        string idKhachHang = "";
        //if (hdIdKhachHang.Value.Trim() != "")
        //    idKhachHang = hdIdKhachHang.Value.Trim();
        if(idKhachHang != "")
            Response.Redirect("QuanLyHangTaiVanPhong.aspx?idKhachHang="+idKhachHang);
        else
            Response.Redirect("QuanLyHangTaiVanPhong.aspx");
    }
//    [WebMethod]
//    public static string XemSoDo(string idDonHang)
//    {
//        string html = @"  <div style='overflow: auto; margin-bottom: 12px;'> <table id='tbKhuMo' border='1' style='width:100%'>";

//        string _Hang = StaticData.getField("tb_Khu", "SoHang", "idKhu", idDonHang);
//        string _Cot = "4";
//        string TenKhuMo = StaticData.getField("tb_DonHang", "TenKhu", "idKhu", idDonHang);

//        int Hang = int.Parse(_Hang.Trim());
//        int Cot = int.Parse(_Cot.Trim());

//        for (int i = 1; i <= Hang; i++)
//        {
//            html += @"<tr>";
//            for (int j = 1; j <= Cot; j++)
//            {
//                string Mo = "-";
//                string idHuyet = "";

//                string sql = @"SELECT TOP 1 * FROM [tb_Huyet] WHERE idKhu = '" + idDonHang + "' AND Hang = '" + i + "' AND Huyet = '" + j + "'";
//                DataTable tb = Connect.GetTable(sql);
//                if (tb.Rows.Count > 0)
//                {
//                    Mo = tb.Rows[0]["TenHuyet"].ToString();
//                    idHuyet = tb.Rows[0]["idHuyet"].ToString();
//                }



//                if (tb.Rows[0]["idDangKyDichVuMaiTang"].ToString() != "")
//                {

//                    html += @" <td>
//                           <button id='btHuyet_" + idHuyet + @"' class='ui icon red button'> " + Mo + @"
//                        </button>
//                       </td> ";
//                }
////                else
////                {
////                    var Mau = "";
////                    if (tb.Rows[0]["LoaiMo"].ToString() == "1")
////                        Mau = "green";
////                    if (tb.Rows[0]["LoaiMo"].ToString() == "2")
////                        Mau = "black";

////                    html += @" <td id='O_" + idHuyet + @"'>
////                           <button onclick='Thu(" + idHuyet + ")' id='btHuyet_" + idHuyet + @"' class='ui " + Mau + " icon button'> " + Mo + @"
////                        </button>
////                       </td> ";
////                }
//            }
//            html += @"</tr>";
//        }

//        html += @"</table> 
//</div>";

//        return html + "@" + "Xem sơ đồ khu mộ " + TenKhuMo;
//    }
    protected void slTinh_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadHuyen(slTinh.SelectedValue.Trim());
    }
    protected void slHuyen_SelectedIndexChanged(object sender, EventArgs e)
    {
        string sqlHuyen = "select * from tb_Huyen where idHuyen='" + slHuyen.SelectedValue.Trim() + "'";
        DataTable tbHuyen = Connect.GetTable(sqlHuyen);
        if(tbHuyen.Rows.Count>0)
        {
            slNhanVienGiao.Value = tbHuyen.Rows[0]["idNguoiDung"].ToString();
            if (tbHuyen.Rows[0]["MaLoaiCuoc"].ToString() != "")
            {
                slLoaiCuoc.Value = tbHuyen.Rows[0]["MaLoaiCuoc"].ToString();
                string GiaCuoc = StaticData.getField("tb_LoaiCuoc","GiaCuoc","MaLoaiCuoc",tbHuyen.Rows[0]["MaLoaiCuoc"].ToString());
                txtTienCuoc.Value = (GiaCuoc == "" ? "" : double.Parse(GiaCuoc).ToString("N0").Replace(",", "."));
            }
            //else
            //    slLoaiCuoc.Value = "DiTinh";
            double TienHang = (txtTienHang.Value == "" ? 0 : double.Parse(txtTienHang.Value.Replace(".", "")));
            double TienCuoc = (txtTienCuoc.Value == "" ? 0 : double.Parse(txtTienCuoc.Value.Replace(".", "")));
            double PhuPhi = (txtPhuPhi.Value == "" ? 0 : double.Parse(txtPhuPhi.Value.Replace(".", "")));
            txtTongTien.Value = (TienHang + TienCuoc + PhuPhi).ToString("N0").Replace(",", ".");
        }
    }


    [WebMethod]
    public static string LoadThongTinNguoiNhan(string SoDT)
    {
        string kq = "|~~|";
        string sql = @"SELECT TOP 1 * FROM [tb_DonHang] WHERE SoDienThoaiNguoiNhan = '" +SoDT + "'";
        DataTable tb = Connect.GetTable(sql);
        if(tb.Rows.Count > 0)
        {
            kq = tb.Rows[0]["NguoiNhan"].ToString() + "|~~|" + tb.Rows[0]["DiaChiNguoiNhan"].ToString();
        }

        return kq;
       
    }


}