using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class QuanLyDonHang_QuanLyDonHang_ImportExcel : System.Web.UI.Page
{
    string mTenDangNhap = "";
    string mQuyen = "";
    string mIdNguoiDung = "";
    string mIdKhachHang = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            LoadKho();
            LoadKhachHang();
            if (Request.Cookies["QuanLyCongNoAnhKiet_Login"] != null && Request.Cookies["QuanLyCongNoAnhKiet_Login"].Value.Trim() != "")
            {
                mTenDangNhap = Request.Cookies["QuanLyCongNoAnhKiet_Login"].Value.Trim();
                mIdNguoiDung = StaticData.getField("tb_NguoiDung", "idNguoiDung", "TenDangNhap", mTenDangNhap);
                mIdKhachHang = StaticData.getField("tb_KhachHang", "idKhachHang", "TenDangNhap", mTenDangNhap);
                mQuyen = MyStaticData.GetMaQuyen(mTenDangNhap);
                if (mQuyen.ToUpper() != "ADMIN" && mQuyen.ToUpper() != "NVVP" && mQuyen.ToUpper() != "KH")
                {
                    Response.Redirect("../Home/DangNhap.aspx");
                }
                if(mQuyen.ToUpper() == "KH")
                {
                    string isImportExcel = StaticData.getField("tb_KhachHang", "isImportExcel", "idKhachHang", mIdKhachHang);
                    if (isImportExcel != "True")
                    {
                        Response.Redirect("../Home/DangNhap.aspx");
                    }
                    slKhachHang.Value = mIdKhachHang;
                    slKhachHang.Disabled = true;
                }
            }
            
        }
    }
    private void LoadKho()
    {
        string strSql = "select * from tb_Kho";
        slKho.DataSource = Connect.GetTable(strSql);
        slKho.DataTextField = "TenKho";
        slKho.DataValueField = "idKho";
        slKho.DataBind();
        slKho.Items.Add(new ListItem("-- Chọn --", "0"));
        slKho.Items.FindByText("-- Chọn --").Selected = true;
    }
    private void LoadKhachHang()
    {
        string strSql = "select * from tb_KhachHang";
        slKhachHang.DataSource = Connect.GetTable(strSql);
        slKhachHang.DataTextField = "TenKhachHang";
        slKhachHang.DataValueField = "idKhachHang";
        slKhachHang.DataBind();
        slKhachHang.Items.Add(new ListItem("-- Chọn --", "0"));
        slKhachHang.Items.FindByText("-- Chọn --").Selected = true;
    }
    public static DataTable ImportToDataTable(string file)
    {
        // Create new dataset
        DataSet ds = new DataSet();

        // -- Start of Constructing OLEDB connection string to Excel file
        Dictionary<string, string> props = new Dictionary<string, string>();

        // For Excel 2007/2010
        if (file.EndsWith(".xlsx"))
        {
            props["Provider"] = "Microsoft.ACE.OLEDB.12.0;";
            props["Extended Properties"] = "Excel 12.0 XML";
        }
        // For Excel 2003 and older
        else if (file.EndsWith(".xls"))
        {
            props["Provider"] = "Microsoft.Jet.OLEDB.4.0";
            props["Extended Properties"] = "Excel 8.0";
        }
        else
            return null;

        props["Data Source"] = file;

        StringBuilder sb = new StringBuilder();

        foreach (KeyValuePair<string, string> prop in props)
        {
            sb.Append(prop.Key);
            sb.Append('=');
            sb.Append(prop.Value);
            sb.Append(';');
        }

        string connectionString = sb.ToString();
        // -- End of Constructing OLEDB connection string to Excel file

        // Connecting to Excel File
        using (OleDbConnection conn = new OleDbConnection(connectionString))
        {
            conn.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;

            DataTable dtSheet = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            foreach (DataRow dr in dtSheet.Rows)
            {
                string sheetName = dr["TABLE_NAME"].ToString();

                // you can choose the colums you want.
                cmd.CommandText = "SELECT * FROM [" + sheetName + "]";

                DataTable dt = new DataTable();
                dt.TableName = sheetName.Replace("$", string.Empty);

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                // Add table into DataSet
                ds.Tables.Add(dt);
            }

            cmd = null;
            conn.Close();
        }

        return ds.Tables[0];
    }
    protected void btImportExcel_Click1(object sender, EventArgs e)
    {
        string ListError = "";
        string filePath = "~/Files/" + Path.GetFileName(FileUpload1.PostedFile.FileName);
        try
        {
            FileUpload1.SaveAs(Server.MapPath(filePath));
        }
        catch
        {
            Response.Write("<script>alert('Bạn chưa chọn file excel!')</script>");
            return;
        }
        if(slKho.Value.Trim() == ""||slKho.Value.Trim() == "0")
        {
            Response.Write("<script>alert('Bạn chưa chọn kho!')</script>");
            return;
        }
        if (slKhachHang.Value.Trim() == "" || slKhachHang.Value.Trim() == "0")
        {
            Response.Write("<script>alert('Bạn chưa chọn khách hàng!')</script>");
            return;
        }
        //string filePath=FileUpload1.PostedFile
        DataTable table = ImportToDataTable(Server.MapPath(filePath));
        for (int i = 0; i < table.Rows.Count; i++)
        {
            try
            {
                string MaDonHang = "";
                string NgayLap = "";
                string idKhachHang = "";
                string idKho = "";
                string idNguoiDung = "";
                string MaTinhTrang = "CXL";
                string NguoiNhan = "";
                string SoDienThoaiNguoiNhan="";
                string DiaChiNguoiNhan = "";

                string idLoaiSanPham = "";
                string SoLuong="";
                string TienHang="";
                string TienCuoc="";
                //Loại sản phẩm
                if(table.Rows[i]["Loại sản phẩm"].ToString().Trim() != "")
                {
                    string sqlCheckLoaiSanPham="select * from tb_LoaiSanPham where TenLoaiSanPham=N'"+ table.Rows[i]["Loại sản phẩm"].ToString().Trim() +"'";
                    DataTable tbCheckLoaiSanPham=Connect.GetTable(sqlCheckLoaiSanPham);
                    if(tbCheckLoaiSanPham.Rows.Count>0)
                    {
                        idLoaiSanPham=tbCheckLoaiSanPham.Rows[0]["idLoaiSanPham"].ToString();
                    }
                    else
                    {
                        ListError += "Dòng " + (i + 1).ToString() + ",";
                        break;
                    }
                }
                else
                {
                    ListError += "Dòng " + (i + 1).ToString() + ",";
                    break;
                }
                //Số lượng
                SoLuong=table.Rows[i]["Số lượng"].ToString().Trim();
                //Tiền hàng
                TienHang=table.Rows[i]["Tiền hàng"].ToString().Trim().Replace(",","").Replace(".","");
                //Tiền cước
                TienCuoc=table.Rows[i]["Tiền cước"].ToString().Trim().Replace(",","").Replace(".","");

                //Mã đơn hàng
                if(table.Rows[i]["Mã"].ToString().Trim() != "")
                {
                    string sqlCheckDH = "select * from tb_DonHang where MaDonHang='" + table.Rows[i]["Mã"].ToString().Trim() + "'";
                    DataTable tbCheckDH = Connect.GetTable(sqlCheckDH);
                    if (tbCheckDH.Rows.Count == 0)
                    {
                        MaDonHang = table.Rows[i]["Mã"].ToString().Trim();
                    }
                }
                else
                {
                    ListError += "Dòng " + (i + 1).ToString() + ",";
                    break;
                }
                //Ngày lập
                if (table.Rows[i]["Ngày"].ToString().Trim() != "")
                {
                    //NgayLap = StaticData.ConvertDDMMtoMMDD(table.Rows[i]["Ngày"].ToString().Trim());
                    NgayLap = table.Rows[i]["Ngày"].ToString().Trim();
                }
                //Khách hàng
                idKhachHang = slKhachHang.Value.Trim();
                //Kho
                idKho = slKho.Value.Trim();
                //Nhân viên giao
                string sqlNguoiDung = "select * from tb_NguoiDung where HoTen=N'" + table.Rows[i]["Nhân viên giao"].ToString().Trim() + "'";
                DataTable tbNguoiDung = Connect.GetTable(sqlNguoiDung);
                if (tbNguoiDung.Rows.Count > 0)
                    idNguoiDung = tbNguoiDung.Rows[0]["idNguoiDung"].ToString();
                //Người nhận
                NguoiNhan = table.Rows[0]["NGƯỜI LẤY HÀNG"].ToString();
                //Số điện thoại người nhận
                SoDienThoaiNguoiNhan = table.Rows[0]["SĐT"].ToString();
                //Địa chỉ người nhận
                DiaChiNguoiNhan = table.Rows[0]["Địa chỉ giao hàng"].ToString();
                if(MaDonHang != "" && NgayLap != "" && idKhachHang != "")
                {
                    //Insert đơn hàng
                    string sqlInsertDH = "insert into tb_DonHang(MaDonHang,NgayLap,idKhachHang,idKho,idNguoiDung,MaTinhTrang,NguoiNhan,DiaChiNguoiNhan,SoDienThoaiNguoiNhan)";
                    sqlInsertDH += " values('" + MaDonHang + "','" + NgayLap + "','" + idKhachHang + "','" + idKho + "','" + idNguoiDung + "','" + MaTinhTrang + "',N'"+ NguoiNhan +"',N'"+ DiaChiNguoiNhan +"',N'"+ SoDienThoaiNguoiNhan +"')";
                    bool ktInsertDH = Connect.Exec(sqlInsertDH);
                    if(ktInsertDH)
                    {
                        //Insert chi tiết đơn hàng
                        string idDonHang = StaticData.getField("tb_DonHang", "idDonHang", "MaDonHang", MaDonHang);
                        string sqlInsertCTDH = "insert into tb_ChiTietDonHang(idDonHang,idLoaiSanPham,SoLuong,TienHang,TienCuoc)";
                        sqlInsertCTDH += " values('" + idDonHang + "','" + idLoaiSanPham + "','" + SoLuong + "','" + TienHang + "','" + TienCuoc + "')";
                        bool ktInsertCTDH = Connect.Exec(sqlInsertCTDH);
                    }
                }
                else
                {
                    ListError += "Dòng " + (i + 1).ToString() + ",";
                }
            }
            catch {
                ListError += "Dòng " + (i+1).ToString() + ",";
            }
        }
        if(ListError != "")
        {
            Response.Write("<script>alert('"+ ListError +" chưa import được!')</script>");
            return;
        }
        else
        {
            Response.Write("<script>alert('Import thành công!')</script>");
            return;
        }
    }



    protected void btHuy_Click(object sender, EventArgs e)
    {
        Response.Redirect("QuanLyDonHang.aspx");
    }
}