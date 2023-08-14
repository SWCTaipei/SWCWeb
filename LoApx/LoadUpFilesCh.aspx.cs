using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

public partial class ExcAspx_LoadUpFiles : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {   
        LoadUpFileSwcCh();
    }

    private void LoadUpFileSwcCh()
    {
        string NOFILE = "";
        Boolean folderExists;

        ConnectionStringSettings connectionString01 = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection SwcConn01 = new SqlConnection(connectionString01.ConnectionString))
        {
            string Str01 = " select swc.swc00,ch.水保案件編號,ch.案件編號,ch.簽名檔資料夾,ch.檢查項目照片1, ch.檢查項目照片2, ch.檢查項目照片3, ch.檢查項目照片4, ch.檢查項目照片5, ch.檢查項目照片6,ch.圖說檔案資料夾,ch.附件檔案資料夾,ch.其他附件檔案資料夾 from swcch ch ";
            Str01 = Str01 + " left join swcswc swc on ch.水保案件編號=swc.SWC02 ";

            //string Str01 = " select chg.施工檢查案件編號,chg.行政管理案件編號,swc.swc00, chg.人員照片,chg.相片1,chg.相片2,chg.相片3,chg.相片4,chg.相片5,chg.相片6,chg.附件1 from swcch ch ";
            //Str01 = Str01 + " left join swcswc swc on chg.行政管理案件編號=swc.SWC02 ";
            //Str01 = Str01 + " where ch.案件編號 = 'SWCCH20171015083741510405' ";  //單筆用

            SwcConn01.Open();
            SqlDataReader readeCase;
            SqlCommand objCmdCase = new SqlCommand(Str01, SwcConn01);
            readeCase = objCmdCase.ExecuteReader();

            while (readeCase.Read())
            {
                string sDoc1 = readeCase["案件編號"] +"";           //舊
                string sDoc2 = readeCase["swc00"]+"";               //新

                string sMOVF1  = (readeCase["簽名檔資料夾"] + "").Trim();
                string sMOVF2  = (readeCase["檢查項目照片1"] + "").Trim();
                string sMOVF3  = (readeCase["檢查項目照片2"] + "").Trim();
                string sMOVF4  = (readeCase["檢查項目照片3"] + "").Trim();
                string sMOVF5  = (readeCase["檢查項目照片4"] + "").Trim();
                string sMOVF6  = (readeCase["檢查項目照片5"] + "").Trim();
                string sMOVF7  = (readeCase["檢查項目照片6"] + "").Trim();
                string sMOVF8  = (readeCase["圖說檔案資料夾"] + "").Trim();
                string sMOVF9  = (readeCase["附件檔案資料夾"] + "").Trim();
                string sMOVF10 = (readeCase["其他附件檔案資料夾"] + "").Trim();

                string sPathYear = sDoc1.Substring(5,4);

                //搬搬搬…
                string TempFolderPath = "D:\\GeoVector\\tslmservice\\Uploads\\"+ sPathYear+"\\";        //舊
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
                    string sourceFile01 = @TempFolderPath + sDoc1 + "\\sign\\" + sMOVF1;
                    string destinationFile01 = @"D:\Web\SWCWeb\UpLoadFiles\SwcCaseFile\" + sDoc2 + "\\" + sMOVF1;

                    if (System.IO.File.Exists(sourceFile01))
                    {
                        if (System.IO.File.Exists(destinationFile01)) { } else
                        {
                        //// To move a file or folder to a new location:
                        System.IO.File.Copy(sourceFile01, destinationFile01);
						}
                    }
                    else
                    {
                        NOFILE = NOFILE + sDoc1 + "：人員簽名 資料庫有值，但資料夾無檔案。";
                    }
                }

                if (sMOVF2 != "")
                {
                    string sourceFile02 = @TempFolderPath + sDoc1 + "\\photo\\" + sMOVF2;
                    string destinationFile02 = @"D:\Web\SWCWeb\UpLoadFiles\SwcCaseFile\" + sDoc2 + "\\" + sMOVF2;

                    if (System.IO.File.Exists(sourceFile02))
                    {
                        if (System.IO.File.Exists(destinationFile02)) { } else
                        {
                            // To move a file or folder to a new location:
                            System.IO.File.Copy(sourceFile02, destinationFile02);
                        }
                    }
                    else
                    {
                        NOFILE = NOFILE + sDoc1 + "：照片1 資料庫有值，但資料夾無檔案。" + sourceFile02;
                    }
                }

                if (sMOVF3 != "")
                {
                    string sourceFile03 = @TempFolderPath+ sDoc1 + "\\photo\\" + sMOVF3;
                    string destinationFile03 = @"D:\Web\SWCWeb\UpLoadFiles\SwcCaseFile\" + sDoc2 + "\\" + sMOVF3;

                    if (System.IO.File.Exists(sourceFile03))
                    {
                        if (System.IO.File.Exists(destinationFile03)) { } else
                        {
							// To move a file or folder to a new location:
							System.IO.File.Copy(sourceFile03, destinationFile03);
						}
                    }
                    else
                    {
                        NOFILE = NOFILE + sDoc1 + "：照片2 資料庫有值，但資料夾無檔案。";
                    }
                }

                if (sMOVF4 != "")
                {
                    string sourceFile04 = @TempFolderPath + sDoc1 + "\\photo\\" + sMOVF4;
                    string destinationFile04 = @"D:\Web\SWCWeb\UpLoadFiles\SwcCaseFile\" + sDoc2 + "\\" + sMOVF4;

                    if (System.IO.File.Exists(sourceFile04))
                    {
                        if (System.IO.File.Exists(destinationFile04)) { } else
                        {
							// To move a file or folder to a new location:
							System.IO.File.Copy(sourceFile04, destinationFile04);
						}
                    }
                    else
                    {
                        NOFILE = NOFILE + sDoc1 + "：照片3 資料庫有值，但資料夾無檔案。";
                    }
                }

                if (sMOVF5 != "")
                {
                    string sourceFile05 = @TempFolderPath + sDoc1 + "\\photo\\" + sMOVF5;
                    string destinationFile05 = @"D:\Web\SWCWeb\UpLoadFiles\SwcCaseFile\" + sDoc2 + "\\" + sMOVF5;

                    if (System.IO.File.Exists(sourceFile05))
                    {
                        if (System.IO.File.Exists(destinationFile05)) { } else
                        {
							// To move a file or folder to a new location:
							System.IO.File.Copy(sourceFile05, destinationFile05);
						}
                    }
                    else
                    {
                        NOFILE = NOFILE + sDoc1 + "：照片4 資料庫有值，但資料夾無檔案。";
                    }
                }

                if (sMOVF6 != "")
                {
                    string sourceFile06 = @TempFolderPath + sDoc1 + "\\photo\\" + sMOVF6;
                    string destinationFile06 = @"D:\Web\SWCWeb\UpLoadFiles\SwcCaseFile\" + sDoc2 + "\\" + sMOVF6;

                    if (System.IO.File.Exists(sourceFile06))
                    {
                        if (System.IO.File.Exists(destinationFile06)) { } else
                        {
							// To move a file or folder to a new location:
							System.IO.File.Copy(sourceFile06, destinationFile06);
						}
                    }
                    else
                    {
                        NOFILE = NOFILE + sDoc1 + "：照片5 資料庫有值，但資料夾無檔案。";
                    }
                }

                if (sMOVF7 != "")
                {
                    string sourceFile07 = @TempFolderPath + sDoc1 + "\\photo\\" + sMOVF7;
                    string destinationFile07 = @"D:\Web\SWCWeb\UpLoadFiles\SwcCaseFile\" + sDoc2 + "\\" + sMOVF7;

                    if (System.IO.File.Exists(sourceFile07))
                    {
                        if (System.IO.File.Exists(destinationFile07)) { } else
                        {
                        // To move a file or folder to a new location:
                        System.IO.File.Copy(sourceFile07, destinationFile07);
						}
                    }
                    else
                    {
                        NOFILE = NOFILE + sDoc1 + "：照片6 資料庫有值，但資料夾無檔案。";
                    }
                }

                if (sMOVF8 != "")
                {
                    string sourceFile08 = @TempFolderPath + sDoc1 + "\\fig\\" + sMOVF8;
                    string destinationFile08 = @"D:\Web\SWCWeb\UpLoadFiles\SwcCaseFile\" + sDoc2 + "\\" + sMOVF8;

                    if (System.IO.File.Exists(sourceFile08))
                    {
                        if (System.IO.File.Exists(destinationFile08)) { } else
                        {
                        // To move a file or folder to a new location:
                        System.IO.File.Copy(sourceFile08, destinationFile08);
						}
                    }
                    else
                    {
                        NOFILE = NOFILE + sDoc1 + "：附件8 資料庫有值，但資料夾無檔案。";
                    }
                }
                if (sMOVF9 != "")
                {
                    string sourceFile09 = @TempFolderPath + sDoc1 + "\\fig\\" + sMOVF9;
                    string destinationFile09 = @"D:\Web\SWCWeb\UpLoadFiles\SwcCaseFile\" + sDoc2 + "\\" + sMOVF9;

                    if (System.IO.File.Exists(sourceFile09))
                    {
                        if (System.IO.File.Exists(destinationFile09)) { }
                        else
                        {
                            // To move a file or folder to a new location:
                            System.IO.File.Copy(sourceFile09, destinationFile09);
                        }
                    }
                    else
                    {
                        NOFILE = NOFILE + sDoc1 + "：附件9 資料庫有值，但資料夾無檔案。";
                    }
                }
                if (sMOVF10 != "")
                {
                    string sourceFile10 = @TempFolderPath + sDoc1 + "\\fig\\" + sMOVF10;
                    string destinationFile10 = @"D:\Web\SWCWeb\UpLoadFiles\SwcCaseFile\" + sDoc2 + "\\" + sMOVF10;

                    if (System.IO.File.Exists(sourceFile10))
                    {
                        if (System.IO.File.Exists(destinationFile10)) { }
                        else
                        {
                            // To move a file or folder to a new location:
                            System.IO.File.Copy(sourceFile10, destinationFile10);
                        }
                    }
                    else
                    {
                        NOFILE = NOFILE + sDoc1 + "：附件10 資料庫有值，但資料夾無檔案。";
                    }
                }

            }
        }
        Response.Write(NOFILE);
    }
}