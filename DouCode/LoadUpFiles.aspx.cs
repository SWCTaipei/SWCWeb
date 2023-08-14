using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

public partial class ExcAspx_LoadUpFiles : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {   
        LoadUpFileSwcChg();
    }

    private void LoadUpFileSwcChg()
    {
        string NOFILE = "";
        Boolean folderExists;

        ConnectionStringSettings connectionString01 = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection SwcConn01 = new SqlConnection(connectionString01.ConnectionString))
        {
            string Str01 = " select chg.施工檢查案件編號,chg.行政管理案件編號,swc.swc00, chg.人員照片,chg.相片1,chg.相片2,chg.相片3,chg.相片4,chg.相片5,chg.相片6,chg.附件1 from swcchg chg ";
            Str01 = Str01 + " left join swcswc swc on chg.行政管理案件編號=swc.SWC02 ";
            Str01 = Str01 + " Where chg.施工檢查案件編號 = 'SWCCHG20150524083104' ";  //單筆用

            SwcConn01.Open();
            SqlDataReader readeCase;
            SqlCommand objCmdCase = new SqlCommand(Str01, SwcConn01);
            readeCase = objCmdCase.ExecuteReader();

            while (readeCase.Read())
            {
                string sDoc1 = readeCase["施工檢查案件編號"]+"";    //舊
                string sDoc2 = readeCase["swc00"]+"";               //新

                string sMOVF1 = (readeCase["人員照片"] + "").Trim();
                string sMOVF2 = (readeCase["相片1"] + "").Trim();
                string sMOVF3 = (readeCase["相片2"] + "").Trim();
                string sMOVF4 = (readeCase["相片3"] + "").Trim();
                string sMOVF5 = (readeCase["相片4"] + "").Trim();
                string sMOVF6 = (readeCase["相片5"] + "").Trim();
                string sMOVF7 = (readeCase["相片6"] + "").Trim();
                string sMOVF8 = (readeCase["附件1"] + "").Trim();

                //搬搬搬…
                string TempFolderPath = "D:\\GeoVector\\tslmservice\\swcchgimg\\";        //舊
                string SwcCaseFolderPath = "D:\\Web\\SWCWeb\\UpLoadFiles\\SwcCaseFile\\";   //新

                //建立資料夾
                folderExists = Directory.Exists(SwcCaseFolderPath);
                if (folderExists == false)
                {
                    Directory.CreateDirectory(SwcCaseFolderPath);
                }

                //建立資料夾
                folderExists = Directory.Exists(SwcCaseFolderPath + sDoc2);
                if (folderExists == false)
                {
                    Directory.CreateDirectory(SwcCaseFolderPath + sDoc2);
                }

                if (sMOVF1 != "")
                {
                    string sourceFile01 = @"D:\Web\SWCWeb\UpLoadFiles\swcchgimg\" + sDoc1 + "\\" + sMOVF1;
                    string destinationFile01 = @"D:\Web\SWCWeb\UpLoadFiles\SwcCaseFile\" + sDoc2 + "\\" + sMOVF1;

                    if (System.IO.File.Exists(sourceFile01))
                    {
                        //// To move a file or folder to a new location:
                        System.IO.File.Move(sourceFile01, destinationFile01);
                    }
                    else
                    {
                        NOFILE = NOFILE + sDoc1 + "：人員簽名 資料庫有值，但資料夾無檔案。";
                    }
                }

                if (sMOVF2 != "")
                {
                    string sourceFile02 = @"D:\Web\SWCWeb\UpLoadFiles\swcchgimg\" + sDoc1 + "\\" + sMOVF2;
                    string destinationFile02 = @"D:\Web\SWCWeb\UpLoadFiles\SwcCaseFile\" + sDoc2 + "\\" + sMOVF2;

                    if (System.IO.File.Exists(sourceFile02))
                    {
                        // To move a file or folder to a new location:
                        System.IO.File.Move(sourceFile02, destinationFile02);
                    }
                    else
                    {
                        NOFILE = NOFILE + sDoc1 + "：照片1 資料庫有值，但資料夾無檔案。";
                    }
                }

                if (sMOVF3 != "")
                {
                    string sourceFile03 = @"D:\Web\SWCWeb\UpLoadFiles\swcchgimg\" + sDoc1 + "\\" + sMOVF3;
                    string destinationFile03 = @"D:\Web\SWCWeb\UpLoadFiles\SwcCaseFile\" + sDoc2 + "\\" + sMOVF3;

                    if (System.IO.File.Exists(sourceFile03))
                    {
                        // To move a file or folder to a new location:
                        System.IO.File.Move(sourceFile03, destinationFile03);
                    }
                    else
                    {
                        NOFILE = NOFILE + sDoc1 + "：照片2 資料庫有值，但資料夾無檔案。";
                    }
                }

                if (sMOVF4 != "")
                {
                    string sourceFile04 = @"D:\Web\SWCWeb\UpLoadFiles\swcchgimg\" + sDoc1 + "\\" + sMOVF4;
                    string destinationFile04 = @"D:\Web\SWCWeb\UpLoadFiles\SwcCaseFile\" + sDoc2 + "\\" + sMOVF4;

                    if (System.IO.File.Exists(sourceFile04))
                    {
                        // To move a file or folder to a new location:
                        System.IO.File.Move(sourceFile04, destinationFile04);
                    }
                    else
                    {
                        NOFILE = NOFILE + sDoc1 + "：照片3 資料庫有值，但資料夾無檔案。";
                    }
                }

                if (sMOVF5 != "")
                {
                    string sourceFile05 = @"D:\Web\SWCWeb\UpLoadFiles\swcchgimg\" + sDoc1 + "\\" + sMOVF5;
                    string destinationFile05 = @"D:\Web\SWCWeb\UpLoadFiles\SwcCaseFile\" + sDoc2 + "\\" + sMOVF5;

                    if (System.IO.File.Exists(sourceFile05))
                    {
                        // To move a file or folder to a new location:
                        System.IO.File.Move(sourceFile05, destinationFile05);
                    }
                    else
                    {
                        NOFILE = NOFILE + sDoc1 + "：照片4 資料庫有值，但資料夾無檔案。";
                    }
                }

                if (sMOVF6 != "")
                {
                    string sourceFile06 = @"D:\Web\SWCWeb\UpLoadFiles\swcchgimg\" + sDoc1 + "\\" + sMOVF6;
                    string destinationFile06 = @"D:\Web\SWCWeb\UpLoadFiles\SwcCaseFile\" + sDoc2 + "\\" + sMOVF6;

                    if (System.IO.File.Exists(sourceFile06))
                    {
                        // To move a file or folder to a new location:
                        System.IO.File.Move(sourceFile06, destinationFile06);
                    }
                    else
                    {
                        NOFILE = NOFILE + sDoc1 + "：照片5 資料庫有值，但資料夾無檔案。";
                    }
                }

                if (sMOVF7 != "")
                {
                    string sourceFile07 = @"D:\Web\SWCWeb\UpLoadFiles\swcchgimg\" + sDoc1 + "\\" + sMOVF7;
                    string destinationFile07 = @"D:\Web\SWCWeb\UpLoadFiles\SwcCaseFile\" + sDoc2 + "\\" + sMOVF7;

                    if (System.IO.File.Exists(sourceFile07))
                    {
                        // To move a file or folder to a new location:
                        System.IO.File.Move(sourceFile07, destinationFile07);
                    }
                    else
                    {
                        NOFILE = NOFILE + sDoc1 + "：照片6 資料庫有值，但資料夾無檔案。";
                    }
                }

                if (sMOVF8 != "")
                {
                    string sourceFile08 = @"D:\Web\SWCWeb\UpLoadFiles\swcchgimg\" + sDoc1 + "\\" + sMOVF8;
                    string destinationFile08 = @"D:\Web\SWCWeb\UpLoadFiles\SwcCaseFile\" + sDoc2 + "\\" + sMOVF8;

                    if (System.IO.File.Exists(sourceFile08))
                    {
                        // To move a file or folder to a new location:
                        System.IO.File.Move(sourceFile08, destinationFile08);
                    }
                    else
                    {
                        NOFILE = NOFILE + sDoc1 + "：附件1 資料庫有值，但資料夾無檔案。";
                    }
                }
                
            }
        }
        Response.Write(NOFILE);
    }
}