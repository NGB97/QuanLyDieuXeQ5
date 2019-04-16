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
    string sIdDonHang = "";
    string Page = "";
    string midChiNhanh = "";
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
            midChiNhanh = StaticData.getField("tb_NguoiDung", "ChiNhanh", "TenDangNhap", mTenDangNhap);
            txtChiNhanhGui.Value = StaticData.getField("tb_ChiNhanh", "TenChiNhanh", "IDChiNhanh", midChiNhanh);
            hdIDChiNhanhGui.Value = midChiNhanh;
            //if (mQuyen.ToUpper() != "ADMIN" && mQuyen.ToUpper() != "NVGN" && mQuyen.ToUpper() != "NVVP" && mQuyen.ToUpper() != "KH" )
            //{
            //    Response.Redirect("../Home/DangNhap.aspx");
            //}
        }
        try
        {
            sIdDonHang = StaticData.ValidParameter(Request.QueryString["idDonHang"].Trim());
        }
        catch { }
        try
        {
            Page = StaticData.ValidParameter(Request.QueryString["Page"].Trim());
        }
        catch { }

        if (listSanPham.Value != "")
        {
            
                DataTable data = new DataTable();
                data.Columns.Add("idHangHoa");
              //  data.Columns.Add("MaHangHoa");
            //    data.Columns.Add("TenHangHoa");

                data.Columns.Add("SoLuong");
                data.Columns.Add("TinhTrang");
                data.Columns.Add("DonGia");
                data.Columns.Add("ThanhTien");
             
                string[] arr = listSanPham.Value.Split(',');
                for (int i = 0; i < arr.Length; i++)
                {
                    string[] arrSP = arr[i].Split('-');
                    data.Rows.Add(arrSP[0], arrSP[1], arrSP[2], arrSP[3], arrSP[4]);
                }
                loadSPLucBan(data);
            
        }

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
            txt_ChuyenPhatNhanh.Disabled = true;
            LoadTinhTrang();
            if (mQuyen.ToUpper() == "KH")
            {
                hdIdKhachHang.Value = mIdKhachHang;
                txtNguoiNhan.Value = StaticData.getField("tb_KhachHang", "TenKhachHang", "idKhachHang", mIdKhachHang);
                txtNguoiNhan.Disabled = true;
             //   txtMaDonHang.Value = mIdKhachHang + MyStaticData.TaoMaDonHang();

                slNhanVienGiao.Disabled = true;
                txtNgayDuKienGiao.Disabled = true;
                txtGioDuKienGiao.Disabled = true;
                txtPhutDuKienGiao.Disabled = true;
               
                //slKho.Disabled = true;
                //slLoaiCuoc.Disabled = true;
                slTinhTrangDonHang.Disabled = true;
                slTinhTrangDonHang.Value = "CXL";
            }
            LoadMaDonHang();
           // txtMaDonHang.Value = MyStaticData.TaoMaDonHang();
            LoadThongTinDonHang();

        }
    }

   private void LoadTenHangHoa()
    {

    }

   protected void loadSPLucBan(DataTable data)
   {
       if (data.Rows.Count > 0)
       {
           string slistSP = "";

           string html = "";
           double TongTien = 0;
           for (int i = 0; i < data.Rows.Count; i++)
           {
               string MaHH = "";
               string TenHH = "";
               string IDHangHoa = "";
               //string idChiTietNhapMua = data.Rows[i]["IDChiTietNhapMua"].ToString();
               //string sqlid = "select * from tb_ChiTietNhapMua where IDChiTietNhapMua= '" + idChiTietNhapMua + "'";
               //DataTable tbid = Connect.GetTable(sqlid);
               //if (tbid.Rows.Count > 0)
               //{
               // IDHangHoa = data.Rows[0]["IDHangHoa"].ToString();
               //}
               //string sql = "select * from tb_HangHoa where IDHangHoa = '" + MaHH + "'";
               //DataTable tb = Connect.GetTable(sql);
               //if (tb.Rows.Count > 0)
               //{
               IDHangHoa = data.Rows[i]["idHangHoa"].ToString();
           //    MaHH = data.Rows[i]["MaHangHoa"].ToString();
               string sql = "select * from tb_HangHoa where idHangHoa = N'" + IDHangHoa + "'";
               DataTable tb = Connect.GetTable(sql);
               if (tb.Rows.Count > 0)
               {
                   TenHH = tb.Rows[0]["TenHangHoa"].ToString();
               }

               slistSP += IDHangHoa + "-" + data.Rows[i]["SoLuong"] + "-" + data.Rows[i]["TinhTrang"] + "-" + data.Rows[i]["DonGia"] + "-" + data.Rows[i]["ThanhTien"];
               html += "<tr id='tr_" + IDHangHoa + "'>";
               html += "<td>" + (i + 1) + "</td>";
           //    html += "     <td style='text-align:center;vertical-align: inherit;'>" + MaHH + "</td>";
               html += "     <td style='text-align:center;vertical-align: inherit;'>" + TenHH + "</td>";
               //html += "       <td style='text-align:center'>" + data.Rows[i]["DonViTinh"] + "</td>";
               //string MaNhapMua = "";
               //string sqlma = "SELECT  tb_NhapMua.MaNhapMua FROM tb_ChiTietNhapMua, tb_NhapMua WHERE tb_ChiTietNhapMua.IDNhapMua=tb_NhapMua.IDNhapMua and IDChiTietNhapMua = '" + idChiTietNhapMua + "'";
               //DataTable tbma = Connect.GetTable(sqlma);
               //if (tbma.Rows.Count > 0)
               //{
               //    MaNhapMua = tbma.Rows[0]["MaNhapMua"].ToString();
               //}
               //html += "       <td style='text-align:center'>" + MaNhapMua + "</td>";
               html += "       <td style='text-align:center'>" + data.Rows[i]["SoLuong"] + "</td>";


               html += "       <td style='text-align:center'>" + double.Parse(data.Rows[i]["DonGia"].ToString()).ToString("N0").Replace(",", ".") + "</td>";
               //html += "<td style='text-align:center'><input class='idSLSP' style='width: 40px' type='number' data-id = '" + data.Rows[i]["Id"] + "' value='" + data.Rows[i]["SoLuong"] + "' runat='server'/></i></td>";


               html += "   <td style='text-align:center'>" + data.Rows[i]["TinhTrang"].ToString() + "</td>";


               //html += "<td style='text-align:center'><a onclick=''><img class='imgedit' id='DeleteSP_" + data.Rows[i]["Id"] + "' src='../Images/delete.png'/></a></td>";
               html += "   <td style='text-align: center'><a style='cursor:pointer' onclick='XoaSanPham(\"" + IDHangHoa + "\")'><i class='fa fa-trash'></i></a></td>";
             //  html += "       <td style='text-align:center'>" + data.Rows[i]["HangGui"] + "</td>";
               if (i < data.Rows.Count - 1)
                   slistSP += ",";


               html += "</tr>";
               double ThanhTien = double.Parse(data.Rows[i]["ThanhTien"].ToString());
               double SoLuong = double.Parse(data.Rows[i]["SoLuong"].ToString());
               TongTien += (ThanhTien);

           }
           txtTongTien.Value = TongTien.ToString();
           txt_ThanhToan.Value = TongTien.ToString();
           //txtCKTien.Value = ((TongTien * ChietKhau) / 100).ToString();
           listSanPham.Value = slistSP;
           danhSachSPChon.InnerHtml = html;
       }
   }
    private void LoadMaDonHang()
    {
        string MaKhachHang = "";
        // string _Ngay = "";
        string sql = "select isnull(max(IDDonHang),0)+1 as 'MaKhachHang' from tb_DonHang";
        DataTable table = Connect.GetTable(sql);
        MaKhachHang = table.Rows[0]["MaKhachHang"].ToString();
        //_Ngay = DateTime.Now.ToString("yyyy-MM-dd");
        string[] ngay = DateTime.Now.ToString("MM/dd/yyyy").Split('/');
        string MaDonHang = ngay[0] + ngay[1] + ngay[2];
        // txtMaDonHang.DataSource = Connect.GetTable(sql);
        txtMaDonHang.Value = MaDonHang + "00" + MaKhachHang + "";
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
        if (sIdDonHang != "")
        {
            string sql = "select * from tb_DonHang where idDonHang='" + sIdDonHang + "'";
            DataTable table = Connect.GetTable(sql);
            if (table.Rows.Count > 0)
            {
                if (mQuyen.ToUpper() == "KH" && mIdKhachHang != table.Rows[0]["idKhachHang"].ToString())
                {
                    Response.Redirect("../Home/DangNhap.aspx");
                }
                dvTitle.InnerHtml = "SỬA THÔNG TIN ĐƠN HÀNG";
                btLuu.Text = "SỬA";

                txtMaDonHang.Value = table.Rows[0]["MaDonHang"].ToString();
                txtNgayLap.Value = DateTime.Parse(table.Rows[0]["NgayLap"].ToString()).ToString("dd/MM/yyyy");

                string TenKH = StaticData.getField("tb_KhachHang", "TenKhachHang", "idKhachHang", table.Rows[0]["idKhachHang"].ToString());
              
                //btHuy.Text = "DSĐH CỦA " + TenKH.ToUpper();
                //slKho.Value = table.Rows[0]["idKho"].ToString();
                txtDienThoaiNguoiGui.Value = table.Rows[0]["SDTNguoiGui"].ToString();
                txtNguoiGui.Value = table.Rows[0]["NguoiGui"].ToString();
                txtChiNhanhGui.Value = StaticData.getField("tb_ChiNhanh", "TenChiNhanh", "IDChiNhanh", table.Rows[0]["idChiNhanhGui"].ToString());
                hdIDChiNhanhGui.Value = table.Rows[0]["idChiNhanhGui"].ToString();
               // txtPhiCOD.Value = table.Rows[0]["PhiCOD"].ToString();

                double PhiCOD = table.Rows[0]["PhiCOD"].ToString() == "" ? 0 : double.Parse(table.Rows[0]["PhiCOD"].ToString());
                txtPhiCOD.Value = PhiCOD.ToString("N0").Replace(",", "."); 
           
              //  txtDiaChiNguoiNhan.Value = table.Rows[0]["DiaChiNguoiNhan"].ToString();
                txtNguoiNhan.Value = TenKH;
                txtSoDienThoaiNguoiNhan.Value = StaticData.getField("tb_KhachHang", "SoDienThoai", "idKhachHang", table.Rows[0]["idKhachHang"].ToString());
                txtDiaChiNguoiNhan.Value = StaticData.getField("tb_KhachHang", "DiaChi", "idKhachHang", table.Rows[0]["idKhachHang"].ToString());
                hdIdKhachHang.Value = table.Rows[0]["idKhachHang"].ToString();

                txtChiNhanhNhan.Value = StaticData.getField("tb_ChiNhanh", "TenChiNhanh", "IDChiNhanh", table.Rows[0]["idChiNhanhNhan"].ToString());
                hdIDChiNhanhNhan.Value = table.Rows[0]["idChiNhanhNhan"].ToString();


                double ThanhToan = table.Rows[0]["ThanhToan"].ToString() == "" ? 0 : double.Parse(table.Rows[0]["ThanhToan"].ToString());
                double TienCuoc = table.Rows[0]["TongCuoc"].ToString() == "" ? 0 : double.Parse(table.Rows[0]["TongCuoc"].ToString());
                txtTongTien.Value = TienCuoc.ToString("N0").Replace(",", ".");
                txt_ThanhToan.Value = ThanhToan.ToString("N0").Replace(",", ".");
                string sql1 = "select * from tb_ChiTietDonHang where IDDonHang ='" + sIdDonHang + "'";

                DataTable data = Connect.GetTable(sql1);
                //SetPage();
                loadSP(data);
                
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
            double TongTien = 0;
            for (int i = 0; i < data.Rows.Count; i++)
            {
                double DonGia = 0;
                string TenHH = "";

                string IDHangHoa = data.Rows[i]["IDHangHoa"].ToString();
              
                string sql = "select * from tb_HangHoa where IDHangHoa = '" + IDHangHoa + "'";
                DataTable tb = Connect.GetTable(sql);
                if (tb.Rows.Count > 0)
                {
                    TenHH = tb.Rows[0]["TenHangHoa"].ToString();
                    DonGia = double.Parse(tb.Rows[0]["GiaCuoc"].ToString());
                }
                double SL = double.Parse(data.Rows[i]["SoLuong"].ToString());
                slistSP += IDHangHoa + "-" + data.Rows[i]["SoLuong"] + "-" + data.Rows[i]["MaTinhTrang"] + "-" + DonGia + "-" + (SL* DonGia);
                html += "<tr id='tr_" + IDHangHoa + "'>";
                html += "<td>" + (i + 1) + "</td>";
                html += "     <td style='text-align:center;vertical-align: inherit;'>" + TenHH + "</td>";
                html += "     <td style='text-align:center;vertical-align: inherit;'>" + SL.ToString("N0").Replace(",", ".") + "</td>";
                html += "     <td style='text-align:center;vertical-align: inherit;'>" + DonGia.ToString("N0").Replace(",", ".") + "</td>";
                html += "     <td style='text-align:center;vertical-align: inherit;'>" + StaticData.getField("tb_TinhTrang", "TenTinhTrang", "MaTinhTrang", data.Rows[i]["MaTinhTrang"].ToString()) + "</td>"; 
                //html += "       <td style='text-align:center'>" + data.Rows[i]["DonViTinh"] + "</td>";
              
                //html += "<td style='text-align:center'><a onclick=''><img class='imgedit' id='DeleteSP_" + data.Rows[i]["Id"] + "' src='../Images/delete.png'/></a></td>";
                html += "   <td style='text-align: center'><a style='cursor:pointer' onclick='XoaSanPham(\"" + IDHangHoa + "\")'><i class='fa fa-trash'></i></a></td>";
                //html += "       <td style='text-align:center'>" + data.Rows[i]["HangGoi"] + "</td>";
                if (i < data.Rows.Count - 1)
                    slistSP += ",";


                html += "</tr>";
                double ThanhTien = double.Parse(tb.Rows[0]["GiaCuoc"].ToString());
                TongTien += ThanhTien;

            }
            // txtTongTien.Value = TongTien.ToString();
            txtTongTien.Value = TongTien.ToString("##,0").Replace(",", ".");

            //txtCKTien.Value = ((TongTien * ChietKhau) / 100).ToString();
            listSanPham.Value = slistSP;
            danhSachSPChon.InnerHtml = html;
        }
    }

    protected void btLuu_Click(object sender, EventArgs e)
    {
        string MaDonHang = "";
        string NgayLap = "";

        string SDTNguoiGui = "";
        string Nguoigui = "";
        //string idKho = "";
        string idChiNhanhGui = "";

        string ThanhToan = "";
       // string NguoiNhan = "";
       // string DiaChiNguoiNhan = "";
       // string SoDienThoaiNguoiNhan = "";
        string idChiNhanhNhan = "";
        string idKhachHang = "";
        string TongCuoc = "";
        string PhiCOD = "";

        string ChuyenPhatNhanh = "";
        string MaTinhTrang = "";
        string GhiChu = "";

        string isDaNhanTien = ckbDaNhanTien.Checked.ToString();
        bool NguoiNhanTra = radiNguoiNhanTra.Checked;


        //Mã đơn hàng
        if (txtMaDonHang.Value.Trim() != "")
        {
            if (sIdDonHang == "")
            {
                string sqlCheckDH = "select top 1 idDonHang from tb_DonHang where MaDonHang='" + txtMaDonHang.Value.Trim() + "'";
                DataTable tbCheckDH = Connect.GetTable(sqlCheckDH);
                if (tbCheckDH.Rows.Count > 0)
                {
                    Response.Write("<script>alert('Mã đơn hàng đã tồn tại!')</script>");
                    return;
                }
                else
                {
                    MaDonHang = StaticData.ValidParameter(txtMaDonHang.Value.Trim());
                }
            }
            else
            {
                string sqlCheckDH = "select top 1 idDonHang from tb_DonHang where idDonHang!='" + sIdDonHang + "' and MaDonHang='" + txtMaDonHang.Value.Trim() + "'";
                DataTable tbCheckDH = Connect.GetTable(sqlCheckDH);
                if (tbCheckDH.Rows.Count > 0)
                {
                    Response.Write("<script>alert('Mã đơn hàng đã tồn tại!')</script>");
                    return;
                }
                else
                {
                    MaDonHang = StaticData.ValidParameter(txtMaDonHang.Value.Trim());
                }
            }
        }
        else
        {
            Response.Write("<script>alert('Bạn chưa nhập mã đơn hàng!')</script>");
            return;
        }
        //Ngày lập
     //   MaDonHang = txtMaDonHang.Value.Trim();
  
            NgayLap = StaticData.ConvertDDMMtoMMDD(txtNgayLap.Value.Trim());
            // DiaChiNguoiNhan = txtDiaChiNguoiNhan.Value.Trim();
            idChiNhanhNhan = hdIDChiNhanhNhan.Value.Trim();
            if (idChiNhanhNhan == "")
            {
                Response.Write("<script>alert('Bạn chưa nhập chi nhánh nhận!')</script>");
                return;
            }


     
        idChiNhanhGui = hdIDChiNhanhGui.Value.Trim();
        if(idChiNhanhGui == "")
        {
            Response.Write("<script>alert('Bạn chưa nhập chi nhánh gởi!')</script>");
            return;
        }

        SDTNguoiGui = txtDienThoaiNguoiGui.Value.Trim();
        Nguoigui = txtNguoiGui.Value.Trim();
        PhiCOD = txtPhiCOD.Value.Trim().Replace(".", "");
        if (PhiCOD != "")
        {
            double PhiCOD1 = double.Parse(PhiCOD.ToString());
            if (SDTNguoiGui == "" || Nguoigui == "" )
            {


                Response.Write("<script>alert('Bạn chưa nhập số điện thoại và người gửi!')</script>");
                return;

            }
        }
        
     

        //Khách hàng
        idKhachHang = hdIdKhachHang.Value.Trim();
   
        if (idKhachHang == "")
        {
            string DTKH = txtSoDienThoaiNguoiNhan.Value.Trim();
            if (DTKH == "")
            {
                Response.Write("<script>alert('Bạn chưa nhập số điện thoại người nhận !')</script>");
                return;
            }
            string KH = txtNguoiNhan.Value.Trim();
            string DiaChi = txtDiaChiNguoiNhan.Value.Trim();
            string MaKhachHang = "";
            string sMa = "";
            // string _Ngay = "";
            string sql = "select isnull(max(IDKhachHang),0)+1 as 'MaKhachHang' from tb_KhachHang";
            DataTable table = Connect.GetTable(sql);
            MaKhachHang = table.Rows[0]["MaKhachHang"].ToString();
            //_Ngay = DateTime.Now.ToString("yyyy-MM-dd");
            string[] ngay = DateTime.Now.ToString("MM/dd/yyyy").Split('/');
            string MaDonHang1 = ngay[0] + ngay[1] + ngay[2];
            // txtMaDonHang.DataSource = Connect.GetTable(sql);
            sMa = MaDonHang1 + "00" + MaKhachHang + "";

            string sqlkt = "select * from tb_KhachHang where SoDienThoai = N'" + DTKH + "'";
            DataTable table1 = Connect.GetTable(sqlkt);
            if (table1.Rows.Count > 0)
            {
                idKhachHang = table1.Rows[0]["idKhachHang"].ToString();
            }
            else
            {
                string sqlInsertDH1 = "insert into tb_KhachHang(MaKhachHang,TenKhachHang,SoDienThoai,DiaChi)";
                sqlInsertDH1 += " values('" + sMa + "',N'" + KH + "','" + DTKH + "',N'" + DiaChi + "'";


                sqlInsertDH1 += ")";
                bool ktInsertKH1 = Connect.Exec(sqlInsertDH1);
                if (ktInsertKH1)
                {
                    idKhachHang = StaticData.getField("tb_KhachHang", "idKhachHang", "SoDienThoai", DTKH);
                }
            }

        }
        TongCuoc = txtTongTien.Value.Trim().Replace(".", "");

       // PhiCOD = txtPhiCOD.Value.Trim().Replace(".", "");
        
        //Gói dịch vụ
        ChuyenPhatNhanh = txt_ChuyenPhatNhanh.Value.Trim().Replace(".", "");
        string PhiChuyenPhatNhanh = txt_CuocChuyenPhatNhanh.Value.Trim().Replace(".", "");
        //Ghi chú
        GhiChu = txtGhiChu.Value.Trim();
        double TongTien1 = 0;
        string[] arraySPList = null;
        if (ChuyenPhatNhanh == "" || ChuyenPhatNhanh == null)
        {
            if (listSanPham.Value.Trim() == "" || listSanPham.Value.Trim() == null)
            {
                Response.Write("<script>alert('Bạn chưa nhập sản phẩm nào!')</script>");
                //temp = 1;
            }
            else
            {
                arraySPList = listSanPham.Value.Trim().Split(',');
                if (arraySPList != null)
                {
                    for (int i = 0; i < arraySPList.Length; i++)
                    {
                        //  double DonGia = double.Parse(arraySPList[i].Split('-')[3]);
                        double DonGia = arraySPList[i].Split('-')[4].ToString() == "" ? 0 : double.Parse(arraySPList[i].Split('-')[4].ToString());
                        TongTien1 += DonGia;
                    }
                }
                //MessageSanPham.Style.Add("display", "none");
            }
        }

        ThanhToan = txt_ThanhToan.Value.Trim().Replace(",", ".");

        if (NguoiNhanTra == false)
        {
            if (ThanhToan == "")
                Response.Write("<script>alert('Bạn chưa nhập thanh toán !')</script>");
        }
        else
            ThanhToan = "0";
        
        double Tien = 0;
      //  Tien = TongTien1.Replace(",", ".");
        if (ChuyenPhatNhanh != "")
        {
            TongTien1 = double.Parse(PhiChuyenPhatNhanh);
        }
        //////////
        if (sIdDonHang == "")
        {
         
           // string MaDonHang1 = MyStaticData.TaoMaDonHang();
            string sqlInsertDH = "insert into tb_DonHang(MaDonHang,NgayLap,SDTNguoiGui,NguoiGui,idChiNhanhGui,idKhachHang,NguoiNhanTra,idChiNhanhNhan,PhiCOD,TongCuoc,ChuyenPhatNhanh,CuocChuyenPhatNhanh,ThanhToan,idNguoiDung,MaTinhTrangCPN)";
            sqlInsertDH += " values('" + MaDonHang + "','" + NgayLap + "','" + SDTNguoiGui + "',N'" + Nguoigui + "',N'" + idChiNhanhGui + "',N'" + idKhachHang + "','" + NguoiNhanTra + "',N'" + idChiNhanhNhan + "',N'" + PhiCOD + "',N'" + TongTien1 + "',N'" + ChuyenPhatNhanh + "',N'" + PhiChuyenPhatNhanh + "',N'" + ThanhToan + "',N'" + mIdNguoiDung + "'";
            if (ChuyenPhatNhanh != "")
            {
                sqlInsertDH += ",N'XDVP'";
            }
            else
                sqlInsertDH += ",N''";
            
            sqlInsertDH += ")";
            bool ktInsertKH = Connect.Exec(sqlInsertDH);
            if (ktInsertKH)
            {
                string idDonHang = StaticData.getField("tb_DonHang", "idDonHang", "MaDonHang", MaDonHang);
                //Response.Redirect("QuanLyDonHang-CapNhat.aspx?idDonHang=" + idDonHang);
               // Response.Redirect("QuanLyDonHang.aspx" + (Page == "" ? "" : "?Page=" + Page));
                if (ChuyenPhatNhanh == "")
                {
                    SaveOrderDetail(arraySPList, idDonHang);
                }
                Response.Redirect("QuanLyDonHang.aspx?Page=" + Page);
            }
            else
            {
                Response.Write("<script>alert('Lỗi thêm đơn hàng, bạn vui lòng kiểm tra lại dữ liệu nhập!')</script>");
            }

        }
        else
        {
            
            string sqlUpdateDH = "";
            sqlUpdateDH += "update tb_DonHang set";
            sqlUpdateDH += " NgayLap = '" + NgayLap + "'";
            sqlUpdateDH += " ,MaDonHang = '" + MaDonHang + "'";
         
            sqlUpdateDH += " ,SDTNguoiGui = '" + SDTNguoiGui + "'";
            sqlUpdateDH += " ,NguoiGui = N'" + Nguoigui + "'";
            sqlUpdateDH += " ,idChiNhanhGui = '" + idChiNhanhGui + "'";
       
           
       
           
            //sqlUpdateDH += " ,DiaChiNguoiNhan = N'" + DiaChiNguoiNhan + "'";
            //
            sqlUpdateDH += " ,idKhachHang = '" + idKhachHang + "'";
            sqlUpdateDH += " ,idChiNhanhNhan = '" + idChiNhanhNhan + "'";
            sqlUpdateDH += " ,PhiCOD = '" + PhiCOD + "'";
            sqlUpdateDH += " ,NguoiNhanTra = '" + NguoiNhanTra + "'";

            sqlUpdateDH += " ,TongCuoc = '" + TongTien1 + "'";
            sqlUpdateDH += " ,ChuyenPhatNhanh = '" + ChuyenPhatNhanh + "'";
            sqlUpdateDH += " ,CuocChuyenPhatNhanh = '" + PhiChuyenPhatNhanh + "'";
            sqlUpdateDH += " ,ThanhToan = '" + ThanhToan + "'";
            sqlUpdateDH += " where idDonHang ='" + sIdDonHang + "'";
            bool ktUpdateKH = Connect.Exec(sqlUpdateDH);
            if (ktUpdateKH)
            {
                //if (Page != "")
                //    Response.Redirect("QuanLyDonHang.aspx?Page=" + Page);
                //else
                //    Response.Redirect("QuanLyDonHang.aspx");
                //SaveOrderDetail(arraySPList, sIdDonHang);
                //Response.Write("<script>alert('Sửa thành công!')</script>");

                string sqlDeleteCTSupervisor = @"DELETE FROM tb_ChiTietDonHang
                                            WHERE idDonHang = '" + sIdDonHang + "'";

                bool ExsqlDelete = Connect.Exec(sqlDeleteCTSupervisor);
                if (ExsqlDelete)
                {
                    //SaveOrderDetail(arraySPList, sIdDonHang);
                    if (ChuyenPhatNhanh == "")
                    {
                        SaveOrderDetail(arraySPList, sIdDonHang);
                    }
                }

                Response.Redirect("QuanLyDonHang.aspx?Page=" + Page);
            }
            else
            {
                Response.Write("<script>alert('Lỗi !')</script>");
            }
        }
     
    }

    protected void SaveOrderDetail(string[] arraySPList, string idDonHang)
    {

        for (int i = 0; i < arraySPList.Length; i++)
        {
            string MaHangHoa = "";
            string[] arr = arraySPList[i].Split('-');
            string idHangHoa = arr[0];
            string Mahh = "";
            string sql = "select isnull(max(idChiTietDonHang),0)+1 as 'MaKhachHang' from tb_ChiTietDonHang";
            DataTable table = Connect.GetTable(sql);
            Mahh = table.Rows[0]["MaKhachHang"].ToString();
            string[] ngay = DateTime.Now.ToString("MM/dd/yy").Split('/');
            string MaDonHang = ngay[0] + ngay[1] + ngay[2];
            // txtMaDonHang.DataSource = Connect.GetTable(sql);
            MaHangHoa = Mahh  + MaDonHang + i +"";
            string sqlorderdetail = @"INSERT INTO tb_ChiTietDonHang(idDonHang,idHangHoa,SoLuong,MaTinhTrang,MaHangHoa,DonGia,idNguoiDung)
                                              VALUES ('" + idDonHang + "','" + idHangHoa + "','" + arr[1] + "',N'" + arr[2] + "',N'" + MaHangHoa + "',N'" + arr[3] + "',N'" + mIdNguoiDung + "')";
            bool Exsql = Connect.Exec(sqlorderdetail);


        }
        Response.Redirect("QuanLyDonHang.aspx?Page=" + Page);
    }

    protected void btHuy_Click(object sender, EventArgs e)
    {
        string idKhachHang = "";
        if (hdIdKhachHang.Value.Trim() != "")
            idKhachHang = hdIdKhachHang.Value.Trim();
        if(idKhachHang != "")
            Response.Redirect("QuanLyDonHang.aspx?idKhachHang="+idKhachHang);
        else
            Response.Redirect("QuanLyDonHang.aspx");
    }

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


    //[WebMethod]
    //public static string LoadThongTinNguoiNhan(string SoDT)
    //{
    //    string kq = "|~~|";
    //    string sql = @"SELECT TOP 1 * FROM [tb_DonHang] WHERE SoDienThoaiNguoiNhan = '" +SoDT + "'";
    //    DataTable tb = Connect.GetTable(sql);
    //    if(tb.Rows.Count > 0)
    //    {
    //        kq = tb.Rows[0]["NguoiNhan"].ToString() + "|~~|" + tb.Rows[0]["DiaChiNguoiNhan"].ToString();
    //    }

    //    return kq;
       
    //}


}