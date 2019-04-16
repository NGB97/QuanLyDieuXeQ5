using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Net.NetworkInformation;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Net.Mail;

public static class StaticData
{

    public static string ConVertstringtodouble(string So)
    {
        string kq = "";
        try
        {
            kq = double.Parse(So).ToString("N0").Replace(",", ".");
        }
        catch { }
        return kq;

    }

    public static string PhanTrang(string url, string txtFistPage, string txtPage1, string txtPage2, string txtPage3, string txtPage4, string txtPage5, string txtLastPage, int Page)
    {
        string html = "";
        html += "           <a class='notepaging' id='page_fist' href='" + url + txtFistPage + "' /><i class='fa fa-step-backward'></i></a>";
        //Page 1
        if (txtPage1 != "")
        {
            if (Page.ToString() == txtPage1)
                html += "           <a id='page_1' class='notepagingactive' href='" + url + txtPage1 + "' />" + txtPage1 + "</a>";
            else
                html += "           <a id='page_1' class='notepaging' href='" + url + txtPage1 + "' />" + txtPage1 + "</a>";
        }
        else
        {
            html += "           <a id='page_1' class='notepagingnone' href='" + url + txtPage1 + "' />" + txtPage1 + "</a>";
        }
        //Page 2
        if (txtPage2 != "")
        {
            if (Page.ToString() == txtPage2)
                html += "           <a id='page_2' class='notepagingactive' href='" + url + txtPage2 + "' />" + txtPage2 + "</a>";
            else
                html += "           <a id='page_2' class='notepaging' href='" + url + txtPage2 + "' />" + txtPage2 + "</a>";
        }
        else
        {
            html += "           <a id='page_2' class='notepagingnone' href='" + url + txtPage2 + "' />" + txtPage2 + "</a>";
        }
        //Page 3
        if (txtPage3 != "")
        {
            if (Page.ToString() == txtPage3)
                html += "           <a id='page_3' class='notepagingactive' href='" + url + txtPage3 + "' />" + txtPage3 + "</a>";
            else
                html += "           <a id='page_3' class='notepaging' href='" + url + txtPage3 + "' />" + txtPage3 + "</a>";
        }
        else
        {
            html += "           <a id='page_3' class='notepagingnone' href='" + url + txtPage3 + "' />" + txtPage3 + "</a>";
        }
        //Page 4
        if (txtPage4 != "")
        {
            if (Page.ToString() == txtPage4)
                html += "           <a id='page_4' class='notepagingactive' href='" + url + txtPage4 + "' />" + txtPage4 + "</a>";
            else
                html += "           <a id='page_4' class='notepaging' href='" + url + txtPage4 + "' />" + txtPage4 + "</a>";
        }
        else
        {
            html += "           <a id='page_4' class='notepagingnone' href='" + url + txtPage4 + "' />" + txtPage4 + "</a>";
        }
        //Page 5
        if (txtPage5 != "")
        {
            if (Page.ToString() == txtPage5)
                html += "           <a id='page_5' class='notepagingactive' href='" + url + txtPage5 + "' />" + txtPage5 + "</a>";
            else
                html += "           <a id='page_5' class='notepaging' href='" + url + txtPage5 + "' />" + txtPage5 + "</a>";
        }
        else
        {
            html += "           <a id='page_5' class='notepagingnone' href='" + url + txtPage5 + "' />" + txtPage5 + "</a>";
        }

        html += "           <a id='page_last' class='notepaging' href='" + url + txtLastPage + "' /><i class='fa fa-step-forward'></i></a>";
        return html;
    }
    public static string GetDate()
    {
        string Ngay = DateTime.Now.Day.ToString();
        string Thang = DateTime.Now.Month.ToString();
        string Nam = DateTime.Now.Year.ToString();
        string Gio = DateTime.Now.Hour.ToString();
        string Phut = DateTime.Now.Minute.ToString();
        string Giay = DateTime.Now.Second.ToString();
        return Thang + "/" + Ngay + "/" + Nam + " " + Gio + ":" + Phut + ":" + Giay;
    }
    #region convert date
    public static string ConvertDDMMtoMMDD(string ngay)
    {
        if (ngay.Equals(""))
        {
            return "";
        }
        else
        {
            string ngayC = ngay.Substring(0, 2);
            string thangC = ngay.Substring(3, 2);
            string namC = ngay.Substring(6, 4);
            return thangC + "/" + ngayC + "/" + namC;
        }
    }
    public static string ConvertDDMMtoMMDD1(string ngay)
    {
        try
        {
            string[] arrNgay = ngay.Split(new string[] { "/" }, StringSplitOptions.None);
            return arrNgay[1] + "/" + arrNgay[0] + "/" + arrNgay[2];
        }
        catch
        {
            return "";
        }
    }
    public static string ConvertMMDDYYtoDDMMYY(string ngay)
    {
        if (ngay.Trim() == "")
        {
            return "";
        }
        else
        {
            int ngayC = 0;
            int thangC = 0;
            int namC = 0;
            try
            {
                thangC = int.Parse(ngay.Substring(0, 2));
                try
                {
                    ngayC = int.Parse(ngay.Substring(3, 2));
                    namC = int.Parse(ngay.Substring(6, 4));
                }
                catch
                {
                    ngayC = int.Parse(ngay.Substring(3, 1));
                    namC = int.Parse(ngay.Substring(5, 4));
                }
            }
            catch
            {
                thangC = int.Parse(ngay.Substring(0, 1));
                try
                {
                    ngayC = int.Parse(ngay.Substring(2, 2));
                    namC = int.Parse(ngay.Substring(5, 4));
                }
                catch
                {
                    ngayC = int.Parse(ngay.Substring(2, 1));
                    namC = int.Parse(ngay.Substring(4, 4));
                }
            }
            string ngaytrave = "";
            if (ngayC < 10 && thangC < 10)
                ngaytrave = "0" + ngayC.ToString() + "/0" + thangC.ToString() + "/" + namC.ToString();
            if (ngayC < 10 && thangC >= 10)
                ngaytrave = "0" + ngayC.ToString() + "/" + thangC.ToString() + "/" + namC.ToString();
            if (ngayC >= 10 && thangC < 10)
                ngaytrave = ngayC.ToString() + "/0" + thangC.ToString() + "/" + namC.ToString();
            if (ngayC >= 10 && thangC >= 10)
                ngaytrave = ngayC.ToString() + "/" + thangC.ToString() + "/" + namC.ToString();

            return ngaytrave;
        }
    }
    #endregion
    public static string ConvertDecimalToString(decimal number)
    {
        string s = number.ToString("#");
        string[] so = new string[] { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
        string[] hang = new string[] { "", "nghìn", "triệu", "tỷ" };
        int i, j, donvi, chuc, tram;
        string str = " ";
        bool booAm = false;
        decimal decS = 0;
        //Tung addnew
        try
        {
            decS = Convert.ToDecimal(s.ToString());
        }
        catch
        {
        }
        if (decS < 0)
        {
            decS = -decS;
            s = decS.ToString();
            booAm = true;
        }
        i = s.Length;
        if (i == 0)
            str = so[0] + str;
        else
        {
            j = 0;
            while (i > 0)
            {
                donvi = Convert.ToInt32(s.Substring(i - 1, 1));
                i--;
                if (i > 0)
                    chuc = Convert.ToInt32(s.Substring(i - 1, 1));
                else
                    chuc = -1;
                i--;
                if (i > 0)
                    tram = Convert.ToInt32(s.Substring(i - 1, 1));
                else
                    tram = -1;
                i--;
                if ((donvi > 0) || (chuc > 0) || (tram > 0) || (j == 3))
                    str = hang[j] + str;
                j++;
                if (j > 3) j = 1;
                if ((donvi == 1) && (chuc > 1))
                    str = "một " + str;
                else
                {
                    if ((donvi == 5) && (chuc > 0))
                        str = "lăm " + str;
                    else if (donvi > 0)
                        str = so[donvi] + " " + str;
                }
                if (chuc < 0)
                    break;
                else
                {
                    if ((chuc == 0) && (donvi > 0)) str = "lẻ " + str;
                    if (chuc == 1) str = "mười " + str;
                    if (chuc > 1) str = so[chuc] + " mươi " + str;
                }
                if (tram < 0) break;
                else
                {
                    if ((tram > 0) || (chuc > 0) || (donvi > 0)) str = so[tram] + " trăm " + str;
                }
                str = " " + str;
            }
        }
        if (booAm) str = "Âm " + str;
        return str + "đồng chẵn.";
    }
    public static bool IsValidEmail(string email)
    {
        try
        {
            int nFirt = int.Parse(email.Substring(0, 1));
            return false;
        }
        catch
        {
            string pattern = @"^[_a-zA-Z0-9][_.a-zA-Z0-9]*@[-.a-zA-Z0-9]+(\.[_.a-zA-Z0-9]+)*\.
                                    (com|edu|info|gov|int|mil|net|org|biz|name|museum|coop|aero|pro|tv|vn|[a-zA-Z]{2})$";
            //Regular expression object
            Regex check = new Regex(pattern, RegexOptions.IgnorePatternWhitespace);
            //boolean variable to return to calling method
            bool valid = false;

            //make sure an email address was provided
            if (string.IsNullOrEmpty(email))
            {
                valid = false;
            }
            else
            {
                //use IsMatch to validate the address
                valid = check.IsMatch(email.ToLower());
            }
            //return the value to the calling method
            return valid;
        }
    }
    public static string Change_AV(string ip_str_change)
    {
        Regex v_reg_regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
        string v_str_FormD = ip_str_change.Normalize(NormalizationForm.FormD);
        return v_reg_regex.Replace(v_str_FormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
    }
    #region Lisence
    public static string GetMD5(string chuoi)
    {
        string str_md5 = "";
        byte[] mang = System.Text.Encoding.UTF8.GetBytes(chuoi);

        MD5CryptoServiceProvider my_md5 = new MD5CryptoServiceProvider();
        mang = my_md5.ComputeHash(mang);

        foreach (byte b in mang)
        {
            str_md5 += b.ToString("X2");
        }

        return str_md5;
    }
    public static string GetMACAddress()
    {
        NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
        String sMacAddress = string.Empty;
        foreach (NetworkInterface adapter in nics)
        {
            if (sMacAddress == String.Empty)// only return MAC Address from first card
            {
                IPInterfaceProperties properties = adapter.GetIPProperties();
                sMacAddress = adapter.GetPhysicalAddress().ToString();
            }
        } return sMacAddress;
    }
    private static void HoanVi(string a, string b)
    {
        string x = "";
        x = a;
        a = b;
        b = x;
    }
    public static string getKey(string physicalAdress)
    {
        string result = "";
        string[] arrS = new string[GetMD5(physicalAdress).Length];
        for (int i = 0; i < GetMD5(physicalAdress).Length; i++)
        {
            arrS[i] = GetMD5(physicalAdress).Substring(i, 1);
        }
        //Hoán vị
        for (int i = 0; i < GetMD5(physicalAdress).Length; i++)
        {
            for (int j = 0; j < (GetMD5(physicalAdress).Length - 1); j++)
            {
                HoanVi(arrS[j], arrS[j + 1]);
            }
            for (int j = 0; j < (GetMD5(physicalAdress).Length - 3); j++)
            {
                HoanVi(arrS[j], arrS[j + 3]);
            }
            for (int j = 0; j < (GetMD5(physicalAdress).Length - 5); j++)
            {
                HoanVi(arrS[j], arrS[j + 5]);
            }
            for (int j = 0; j < (GetMD5(physicalAdress).Length - 2); j++)
            {
                HoanVi(arrS[j], arrS[j + 2]);

            }
            result += arrS[i];
        }
        //MessageBox.Show(result.Substring(0, 5) + "-" + result.Substring(5, 5) +"-"+ result.Substring(10, 5));
        return (result.Substring(0, 5) + "-" + result.Substring(5, 5) + "-" + result.Substring(10, 5));
    }

    //Đọc và ghi dữ liệu vào registry
    public static string ReadRegKey(string path, string getName)
    {
        try
        {
            RegistryKey regkey;
            regkey = Registry.LocalMachine.OpenSubKey(path);
            //return regkey.GetSubKeyNames(); 
            return regkey.GetValue(getName).ToString();
        }
        catch
        {
            return "";
        }
    }

    public static bool WriteRegKey(string keyName, object value, string path)
    {
        RegistryKey rk = Registry.LocalMachine;
        RegistryKey sk1 = rk.CreateSubKey(path);
        sk1.SetValue(keyName, value);
        return true;
    }
    public static void EnabledTCTSQLServer()
    {
        #region Enabeld TCP
        try
        {
            RegistryKey regkey = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Microsoft SQL Server\MSSQL.1\MSSQLServer\SuperSocketNetLib\Tcp", true);
            regkey.SetValue("Enabled", 1);
            RegistryKey regkey1 = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Microsoft SQL Server\MSSQL.1\MSSQLServer", true);
            regkey1.SetValue("LoginMode", 2);
        }
        catch
        { }
        try
        {
            RegistryKey regkey = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Microsoft SQL Server\MSSQL.2\MSSQLServer\SuperSocketNetLib\Tcp", true);
            regkey.SetValue("Enabled", 1);
            RegistryKey regkey1 = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Microsoft SQL Server\MSSQL.2\MSSQLServer", true);
            regkey1.SetValue("LoginMode", 2);
        }
        catch
        { }
        try
        {
            RegistryKey regkey = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Microsoft SQL Server\MSSQL.3\MSSQLServer\SuperSocketNetLib\Tcp", true);
            regkey.SetValue("Enabled", 1);
            RegistryKey regkey1 = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Microsoft SQL Server\MSSQL.3\MSSQLServer", true);
            regkey1.SetValue("LoginMode", 2);
        }
        catch
        { }
        try
        {
            RegistryKey regkey = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Microsoft SQL Server\MSSQL.4\MSSQLServer\SuperSocketNetLib\Tcp", true);
            regkey.SetValue("Enabled", 1);
            RegistryKey regkey1 = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Microsoft SQL Server\MSSQL.4\MSSQLServer", true);
            regkey1.SetValue("LoginMode", 2);
        }
        catch
        { }
        try
        {
            RegistryKey regkey = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Microsoft SQL Server\MSSQL.5\MSSQLServer\SuperSocketNetLib\Tcp", true);
            regkey.SetValue("Enabled", 1);
            RegistryKey regkey1 = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Microsoft SQL Server\MSSQL.5\MSSQLServer", true);
            regkey1.SetValue("LoginMode", 2);
        }
        catch
        { }
        try
        {
            RegistryKey regkey = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Microsoft SQL Server\MSSQL.6\MSSQLServer\SuperSocketNetLib\Tcp", true);
            regkey.SetValue("Enabled", 1);
            RegistryKey regkey1 = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Microsoft SQL Server\MSSQL.6\MSSQLServer", true);
            regkey1.SetValue("LoginMode", 2);
        }
        catch
        { }
        try
        {
            RegistryKey regkey = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Microsoft SQL Server\MSSQL.7\MSSQLServer\SuperSocketNetLib\Tcp", true);
            regkey.SetValue("Enabled", 1);
            RegistryKey regkey1 = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Microsoft SQL Server\MSSQL.7\MSSQLServer", true);
            regkey1.SetValue("LoginMode", 2);
        }
        catch
        { }
        try
        {
            RegistryKey regkey = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Microsoft SQL Server\MSSQL.8\MSSQLServer\SuperSocketNetLib\Tcp", true);
            regkey.SetValue("Enabled", 1);
            RegistryKey regkey1 = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Microsoft SQL Server\MSSQL.8\MSSQLServer", true);
            regkey1.SetValue("LoginMode", 2);
        }
        catch
        { }
        try
        {
            RegistryKey regkey = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Microsoft SQL Server\MSSQL.9\MSSQLServer\SuperSocketNetLib\Tcp", true);
            regkey.SetValue("Enabled", 1);
            RegistryKey regkey1 = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Microsoft SQL Server\MSSQL.9\MSSQLServer", true);
            regkey1.SetValue("LoginMode", 2);
        }
        catch
        { }
        try
        {
            RegistryKey regkey = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Microsoft SQL Server\MSSQL.10\MSSQLServer\SuperSocketNetLib\Tcp", true);
            regkey.SetValue("Enabled", 1);
            RegistryKey regkey1 = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Microsoft SQL Server\MSSQL.10\MSSQLServer", true);
            regkey1.SetValue("LoginMode", 2);
        }
        catch
        { }
        try
        {
            RegistryKey regkey = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Microsoft SQL Server\MSSQL.11\MSSQLServer\SuperSocketNetLib\Tcp", true);
            regkey.SetValue("Enabled", 1);
            RegistryKey regkey1 = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Microsoft SQL Server\MSSQL.11\MSSQLServer", true);
            regkey1.SetValue("LoginMode", 2);
        }
        catch
        { }
        try
        {
            RegistryKey regkey = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Microsoft SQL Server\MSSQL.12\MSSQLServer\SuperSocketNetLib\Tcp", true);
            regkey.SetValue("Enabled", 1);
            RegistryKey regkey1 = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Microsoft SQL Server\MSSQL.12\MSSQLServer", true);
            regkey1.SetValue("LoginMode", 2);
        }
        catch
        { }
        try
        {
            RegistryKey regkey = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Microsoft SQL Server\MSSQL.13\MSSQLServer\SuperSocketNetLib\Tcp", true);
            regkey.SetValue("Enabled", 1);
            RegistryKey regkey1 = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Microsoft SQL Server\MSSQL.13\MSSQLServer", true);
            regkey1.SetValue("LoginMode", 2);
        }
        catch
        { }
        try
        {
            RegistryKey regkey = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Microsoft SQL Server\MSSQL.14\MSSQLServer\SuperSocketNetLib\Tcp", true);
            regkey.SetValue("Enabled", 1);
            RegistryKey regkey1 = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Microsoft SQL Server\MSSQL.14\MSSQLServer", true);
            regkey1.SetValue("LoginMode", 2);
        }
        catch
        { }
        try
        {
            RegistryKey regkey = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Microsoft SQL Server\MSSQL.15\MSSQLServer\SuperSocketNetLib\Tcp", true);
            regkey.SetValue("Enabled", 1);
            RegistryKey regkey1 = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Microsoft SQL Server\MSSQL.15\MSSQLServer", true);
            regkey1.SetValue("LoginMode", 2);
        }
        catch
        { }
        #endregion
    }
    #endregion
    public static bool CoppyFile(string sourceFile, string destinationFile)
    {
        //try
        //{
        SqlConnection.ClearAllPools();
        if (System.IO.File.Exists(destinationFile))
        {
            System.IO.File.Delete(destinationFile);
            //if (System.IO.File.Exists(Application.StartupPath + @"\Database\db_CDSoft_Printer_log.ldf"))
            //    System.IO.File.Delete(Application.StartupPath+@"\Database\db_CDSoft_Printer_log.ldf");
        }
        //destinationFile = System.IO.Path.Combine(destinationFile, System.IO.Path.GetFileName(sourceFile));
        System.IO.File.Copy(sourceFile, destinationFile);
        return true;
        //}
        //catch
        //{
        //    return false;
        //}

    }

    public static string getField(string table, string getField, string paraField, string valueParaField)
    {
        string result = "";
        string sql = "select " + getField + " from " + table + " where " + paraField + "='" + valueParaField + "'";
        try
        {
            DataTable tb = Connect.GetTable(sql);
            if (tb.Rows.Count > 0)
                result = tb.Rows[0][0].ToString();
        }
        catch
        { }
        return result;
    }

    public static string getField1(string table, string getField, string paraField, string valueParaField)
    {
        string result = "";
        string sql = "select " + getField + " from " + table + " where " + paraField + "=N'" + valueParaField + "'";
        try
        {
            DataTable tb = Connect.GetTable(sql);
            if (tb.Rows.Count > 0)
                result = tb.Rows[0][0].ToString();
        }
        catch
        { }
        return result;
    }

    public static bool SendMail(string Subject, string Body, string FromMail, string FromMailPass, string ToMail)
    {
        try
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(ToMail);
            mail.From = new MailAddress(ToMail);
            mail.Subject = Subject;
            mail.Body = Body;

            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient("localhost", 25);
            smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address  
            smtp.Credentials = new System.Net.NetworkCredential
                 (FromMail, FromMailPass);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = true;
            smtp.Send(mail);
            return true;
        }
        catch
        {
            return false;
        }
    }
    public static string ValidParameter(string s)
    {
        s = s.Replace("\"", "").Replace("'", "").Replace("update", "").Replace("select", "").Replace("drop", "").Replace(";", "").Replace("--", "").Replace("insert", "").Replace("delete", "").Replace("xp_", "");
        return s;
    }
    private static readonly string[] VietnameseSigns = new string[]

    {

        "aAeEoOuUiIdDyY",

        "áàạảãâấầậẩẫăắằặẳẵ",

        "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",

        "éèẹẻẽêếềệểễ",

        "ÉÈẸẺẼÊẾỀỆỂỄ",

        "óòọỏõôốồộổỗơớờợởỡ",

        "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",

        "úùụủũưứừựửữ",

        "ÚÙỤỦŨƯỨỪỰỬỮ",

        "íìịỉĩ",

        "ÍÌỊỈĨ",

        "đ",

        "Đ",

        "ýỳỵỷỹ",

        "ÝỲỴỶỸ"

    };


    public static string BoDauTiengViet(string str)
    {

        //Tiến hành thay thế , lọc bỏ dấu cho chuỗi

        for (int i = 1; i < VietnameseSigns.Length; i++)
        {

            for (int j = 0; j < VietnameseSigns[i].Length; j++)

                str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);

        }

        return str;

    }
    public static string ReplaceTieuDe(string TieuDe)
    {
        string sTieuDe = TieuDe.Replace("?", "").Replace("[=1]", "").Replace("[=2]", "").Replace("[=3]", "").Replace("[=4]", "").Replace("[=5]", "").Replace("[=6]", "").Replace("[=7]", "").Replace("[=8]", "").Replace("[=9]", "").Replace("[=10]", "").Replace("[=11]", "").Replace("[=12]", "").Replace("[=13]", "").Replace("[=14]", "").Replace("[=15]", "").ToLower().Trim().Replace("/", " ").Replace("|", " ").Replace("–", "").Replace(" ", "-").Replace(".", "").Replace("%", "").Replace(":", "").Replace("!", "").Replace("\"", "").Replace(",", "").Replace(".", "").Replace("'", "").Replace("“", "").Replace("”", "").Replace("(", "").Replace(")", "").Replace(" ", "-").Replace(" ", "-").Replace(" ", "-").Replace(" ", "-").Replace(" ", "-").Replace("--", "-").Replace("--", "-").Replace("--", "-").Replace("---", "-").Replace("----", "-").Replace("-----", "-").Replace("&","");
        if (sTieuDe.Length <= 62)
            return sTieuDe;
        else
            return sTieuDe.Substring(0, 62);
    }
    public static string ReplaceTieuDePage(string TieuDe)
    {
        string sTieuDe = TieuDe.Replace("?", "").Replace("[=1]", "").Replace("[=2]", "").Replace("[=3]", "").Replace("[=4]", "").Replace("[=5]", "").Replace("[=6]", "").Replace("[=7]", "").Replace("[=8]", "").Replace("[=9]", "").Replace("[=10]", "").Replace("[=11]", "").Replace("[=12]", "").Replace("[=13]", "").Replace("[=14]", "").Replace("[=15]", "").Trim().Replace(".", "").Replace("%", "").Replace(":", ""); ;
        if (sTieuDe.Length <= 62)
            return sTieuDe;
        else
            return sTieuDe.Substring(0, 62);
    }
    public static string RemoveHtmlTagsUsingCharArray(this string htmlString)
    {
        var array = new char[htmlString.Length];
        var arrayIndex = 0;
        var inside = false;

        foreach (var @let in htmlString)
        {
            if (let == '<')
            {
                inside = true;
                continue;
            }
            if (let == '>')
            {
                inside = false;
                continue;
            }
            if (inside) continue;
            array[arrayIndex] = let;
            arrayIndex++;
        }
        return new string(array, 0, arrayIndex);
    }
    public static string DecodeFromUtf8(this string utf8String)
    {
        // read the string as UTF-8 bytes.
        byte[] encodedBytes = Encoding.UTF8.GetBytes(utf8String);

        // convert them into unicode bytes.
        byte[] unicodeBytes = Encoding.Convert(Encoding.UTF8, Encoding.Unicode, encodedBytes);

        // builds the converted string.
        return Encoding.Unicode.GetString(encodedBytes);
    }
}
