using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web;

public partial class SwcReport_PdfSwcTimes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		string rCaseId = Request.QueryString["SWCNO"] + "";
        GenPdf(rCaseId);
        //GenPdfo();
    }

    private void GenPdf(string v)
    {
        string SwcUpLoadFilePath = ConfigurationManager.AppSettings["SwcFilePath20"].Trim();

        string rSWC005 = "";
		string rSWC013 = "";
		string rSWC021 = "";
		string rSWC087 = "";
		string rSWC034 = "";
		string rSWC109 = "";
		string rSWC088 = "";
		string rSWC125 = "";
		int count = 0;
		string sqlstr = "";
		string savedate_1 = "";
		string savedate_2 = "";
		string savedate_3 = "";
		string savedate_4 = "";
		string no = "";
		double delay = 0;
        string temp = "";
		TimeSpan ts = new TimeSpan();
        GBClass001 SBApp = new GBClass001();
		ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();
			sqlstr = " SELECT SWC005,SWC013,SWC021,SWC087,dateadd(DAY,20,SWC034) SWC034,SWC109,SWC088,SWC125 FROM SWCCASE where SWC000=@SWC000;";
			using (var cmd = SwcConn.CreateCommand())
            {
                cmd.CommandText = sqlstr;
                cmd.Parameters.Add(new SqlParameter("@SWC000", v));
                cmd.ExecuteNonQuery();

                using (SqlDataReader readerSWC = cmd.ExecuteReader())
                {
                    if (readerSWC.HasRows)
					{
                        while (readerSWC.Read())
                        {
                            rSWC005 = readerSWC["SWC005"].ToString();
							rSWC013 = readerSWC["SWC013"].ToString();
							rSWC021 = readerSWC["SWC021"].ToString();
							rSWC087 = readerSWC["SWC087"].ToString();
							rSWC034 = readerSWC["SWC034"].ToString();
							rSWC109 = readerSWC["SWC109"].ToString();
							rSWC088 = readerSWC["SWC088"].ToString();
							rSWC125 = readerSWC["SWC125"].ToString();
						}
					}
                    readerSWC.Close();
                }
                cmd.Cancel();
            }
            sqlstr = " SELECT ISNULL(MAX(DTLA006),'0') DTLA006 FROM SWCDTL01 where SWC000=@SWC000;";
			using (var cmd = SwcConn.CreateCommand())
            {
                cmd.CommandText = sqlstr;
                cmd.Parameters.Add(new SqlParameter("@SWC000", v));
                cmd.ExecuteNonQuery();

                using (SqlDataReader readerSWC = cmd.ExecuteReader())
                {
                    if (readerSWC.HasRows)
					{
                        while (readerSWC.Read())
                        {
                            count = Convert.ToInt32(readerSWC["DTLA006"].ToString());
						}
					}
                    readerSWC.Close();
                }
                cmd.Cancel();
            }

        }
		string tLBSAOID = "";
		string UserName = "";
		int ii = 0;
		string exeSqlStr = " select E.ETName,E.ETID,ISNULL(RGSID,'0') AS RGSID from GuildGroup G Left Join ETUsers E on G.ETID=E.ETID where G.swc000='" + v + "' order by convert(float,RGSID) ";
		connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
		using (SqlConnection DDL01Conn = new SqlConnection(connectionString.ConnectionString))
		{
			DDL01Conn.Open();
			SqlDataReader readerItemGG;
			SqlCommand objCmdItemGG = new SqlCommand(exeSqlStr, DDL01Conn);
			readerItemGG = objCmdItemGG.ExecuteReader();

			while (readerItemGG.Read())
			{
				string tmpUserName = readerItemGG["ETName"] + "";
				string tmpUserID = readerItemGG["ETID"] + "";
				string tmpRGSID = readerItemGG["RGSID"] + "";
				int aaa = Convert.ToInt32(tmpRGSID);
				if (aaa > 0 && aaa < 11) //1~10
				{
					UserName += tmpUserName.Trim()+ " ";
				}
			}
		}

        var doc1 = new Document(PageSize.A4);
        MemoryStream Memory = new MemoryStream();
        PdfWriter PdfWriter = PdfWriter.GetInstance(doc1, Memory);

        BaseFont bfChinese = BaseFont.CreateFont(@"C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
        Font ChFont = new Font(bfChinese, 12);
        Font ChFont_red = new Font(bfChinese, 12);
        ChFont_red.SetColor(255, 0, 0);
        Font ChFont_bold = new Font(bfChinese, 12, Font.BOLD);
        Font ChFont_title = new Font(bfChinese, 22, Font.BOLD);

        doc1.Open();

        PdfPTable table1 = new PdfPTable(new float[] { 10, 40 });  //2欄
        table1.TotalWidth = 540f;
        table1.LockedWidth = true;

        PdfPCell header_1 = new PdfPCell(new Phrase("管制時程表", ChFont_title));
        header_1.Colspan = 2;   //跨欄
        header_1.MinimumHeight = 80;//設定行高
        header_1.HorizontalAlignment = Element.ALIGN_CENTER;
        header_1.VerticalAlignment = Element.ALIGN_MIDDLE;
        header_1.Border = PdfPCell.NO_BORDER;
        table1.AddCell(header_1);

        PdfPCell cell = new PdfPCell();

        //案名
        cell = new PdfPCell(new Phrase("案名", ChFont_bold));
        cell.MinimumHeight = 30f;//設定行高
        cell.HorizontalAlignment = Element.ALIGN_CENTER;
        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        cell.Border = Rectangle.BOX;
        table1.AddCell(cell);

        cell = new PdfPCell(new Phrase(rSWC005, ChFont));
        cell.MinimumHeight = strlength(rSWC005);//設定行高
        cell.HorizontalAlignment = Element.ALIGN_LEFT;
        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        cell.Border = Rectangle.BOX;
        table1.AddCell(cell);


        //水土保持義務人
        cell = new PdfPCell(new Phrase("水土保持義務人", ChFont_bold));
        cell.MinimumHeight = 30f;//設定行高
        cell.HorizontalAlignment = Element.ALIGN_CENTER;
        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        cell.Border = Rectangle.BOX;
        table1.AddCell(cell);

        cell = new PdfPCell(new Phrase(rSWC013, ChFont));
        cell.MinimumHeight = strlength(rSWC013);//設定行高
        cell.HorizontalAlignment = Element.ALIGN_LEFT;
        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        cell.Border = Rectangle.BOX;
        table1.AddCell(cell);

        //承辦技師
        cell = new PdfPCell(new Phrase("承辦技師", ChFont_bold));
        cell.MinimumHeight = 30f;//設定行高
        cell.HorizontalAlignment = Element.ALIGN_CENTER;
        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        cell.Border = Rectangle.BOX;
        table1.AddCell(cell);

        cell = new PdfPCell(new Phrase(rSWC021, ChFont));
        cell.MinimumHeight = strlength(rSWC021);//設定行高
        cell.HorizontalAlignment = Element.ALIGN_LEFT;
        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        cell.Border = Rectangle.BOX;
        table1.AddCell(cell);

        //審查委員
        cell = new PdfPCell(new Phrase("審查委員", ChFont_bold));
        cell.MinimumHeight = 30f;//設定行高
        cell.HorizontalAlignment = Element.ALIGN_CENTER;
        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        cell.Border = Rectangle.BOX;
        table1.AddCell(cell);

        cell = new PdfPCell(new Phrase(UserName, ChFont));
        cell.MinimumHeight = strlength(UserName);//設定行高
        cell.HorizontalAlignment = Element.ALIGN_LEFT;
        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        cell.Border = Rectangle.BOX;
        table1.AddCell(cell);

        cell = new PdfPCell();
        cell.Border = PdfPCell.NO_BORDER;
        table1.AddCell(cell);
        table1.AddCell(cell);
        table1.AddCell(cell);
        table1.AddCell(cell);
        table1.AddCell(cell);
        table1.AddCell(cell);
        doc1.Add(table1);

        //*************************************************************

        table1 = new PdfPTable(new float[] { 10, 15, 15, 10 });  //4欄
        table1.TotalWidth = 540f;
        table1.LockedWidth = true;

        //審查次別
        cell = new PdfPCell(new Phrase("審查次別", ChFont_bold));
        cell.MinimumHeight = 30f;//設定行高
        cell.HorizontalAlignment = Element.ALIGN_CENTER;
        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        cell.Border = Rectangle.BOX;
        table1.AddCell(cell);

        //時間
        cell = new PdfPCell(new Phrase("時間", ChFont_bold));
        cell.Colspan = 2;   //跨欄
        cell.MinimumHeight = 30f;//設定行高
        cell.HorizontalAlignment = Element.ALIGN_CENTER;
        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        cell.Border = Rectangle.BOX;
        table1.AddCell(cell);


        //逾期天數
        cell = new PdfPCell(new Phrase("逾期天數", ChFont_bold));
        cell.MinimumHeight = 30f;//設定行高
        cell.HorizontalAlignment = Element.ALIGN_CENTER;
        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        cell.Border = Rectangle.BOX;
        table1.AddCell(cell);

        //第幾次審查
        for (int i = 1; i <= count; i++)
        {
            switch (i)
            {
                case 2:
					no="001";
					break;
				case 3:
					no="002";
					break;
				case 4:
					no="003";
					break;
				case 5:
					no="004";
					break;
				case 6:
					no="005";
					break;
				case 7:
					no="006";
					break;
				case 8:
					no="007";
					break;
				case 9:
					no="008";
					break;
				case 10:
					no="009";
					break;
            }
            using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
            {
                SWCConn.Open();
                sqlstr = " SELECT MIN(savedate) savedate FROM SWCDTL01 where SWC000=@SWC000 and DTLA006=@DTLA006;";
                using (var cmd = SWCConn.CreateCommand())
                {
                    cmd.CommandText = sqlstr;
                    cmd.Parameters.Add(new SqlParameter("@SWC000", v));
                    cmd.Parameters.Add(new SqlParameter("@DTLA006", i.ToString()));
                    cmd.ExecuteNonQuery();

                    using (SqlDataReader readerSWC = cmd.ExecuteReader())
                    {
                        if (readerSWC.HasRows)
                        {
                            while (readerSWC.Read())
                            {
                                savedate_1 = readerSWC["savedate"].ToString();
                            }
                        }
                        readerSWC.Close();
                    }
                    cmd.Cancel();
                }
            }
            if (i != 1)
            {
                using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
                {
                    SWCConn.Open();
                    sqlstr = " SELECT dateadd(day,20,savedate) savedate FROM ShareFiles where SWC000=@SWC000 and SFTYPE=@SFTYPE;";
                    using (var cmd = SWCConn.CreateCommand())
                    {
                        cmd.CommandText = sqlstr;
                        cmd.Parameters.Add(new SqlParameter("@SWC000", v));
                        cmd.Parameters.Add(new SqlParameter("@SFTYPE", no));
                        cmd.ExecuteNonQuery();

                        using (SqlDataReader readerSWC = cmd.ExecuteReader())
                        {
                            if (readerSWC.HasRows)
                            {
                                while (readerSWC.Read())
                                {
                                    savedate_2 = readerSWC["savedate"].ToString();
                                }
                            }
                            readerSWC.Close();
                        }
                        cmd.Cancel();
                    }
                }

            }
            cell = new PdfPCell(new Phrase("第" + i + "次審查", ChFont));
            cell.MinimumHeight = 30f;//設定行高
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.Border = Rectangle.BOX;
            table1.AddCell(cell);

            if (i == 1) {
				if(SBApp.DateView(rSWC034, "03")=="")
					temp = "-";
				else
					temp = "發文期限：" + SBApp.DateView(rSWC034, "03");
            } else {
				if(SBApp.DateView(savedate_2, "03")=="")
					temp = "-";
				else
					temp = "發文期限：" + SBApp.DateView(savedate_2, "03"); 
            }
            cell = new PdfPCell(new Phrase(temp, ChFont));
            cell.MinimumHeight = 30f;//設定行高
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.Border = Rectangle.BOX;
            table1.AddCell(cell);

            cell = new PdfPCell(new Phrase("發文日期：" + SBApp.DateView(savedate_1, "03"), ChFont));
            cell.MinimumHeight = 30f;//設定行高
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.Border = Rectangle.BOX;
            table1.AddCell(cell);


            temp = "";
            ts = new TimeSpan();
            if (i == 1)
            {
                if (SBApp.DateView(savedate_1,"00") != "" && SBApp.DateView(rSWC034,"00") != "")
                    ts = new TimeSpan(Convert.ToDateTime(savedate_1).Ticks - Convert.ToDateTime(rSWC034).Ticks);
            }
            else
            {
                if (SBApp.DateView(savedate_1,"00") != "" && SBApp.DateView(savedate_2,"00") != "")
                    ts = new TimeSpan(Convert.ToDateTime(savedate_1).Ticks - Convert.ToDateTime(savedate_2).Ticks);
            }
            if (ts.Days > 0) { delay += ts.Days; temp = ts.Days.ToString() + "天"; }
            else { temp = ""; }
            cell = new PdfPCell(new Phrase(temp, ChFont));
            cell.MinimumHeight = 30f;//設定行高
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.Border = Rectangle.BOX;
            table1.AddCell(cell);
        }


        /*
		switch(count){
			case 1:
				no="001";
				break;
			case 2:
				no="002";
				break;
			case 3:
				no="003";
				break;
			case 4:
				no="006";
				break;
			case 5:
				no="007";
				break;
		}
		using (SqlConnection SWCConn = new SqlConnection(connectionStringSWC.ConnectionString))
		{
			SWCConn.Open();
			sqlstr = " SELECT dateadd(day,20,savedate) savedate FROM ShareFiles where SWC000=@SWC000 and SFTYPE=@SFTYPE;";
			using (var cmd = SWCConn.CreateCommand())
			{
				cmd.CommandText = sqlstr;
				cmd.Parameters.Add(new SqlParameter("@SWC000", v));
				cmd.Parameters.Add(new SqlParameter("@SFTYPE", no));
				cmd.ExecuteNonQuery();
			
				using (SqlDataReader readerSWC = cmd.ExecuteReader())
				{
					if (readerSWC.HasRows)
					{
						while (readerSWC.Read())
						{
							savedate_3 = readerSWC["savedate"].ToString();
						}
					}
					readerSWC.Close();
				}
				cmd.Cancel();
			}
		}
		*/
		
		ConnectionStringSettings connectionStringTSLM = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
		using (SqlConnection SWCConn = new SqlConnection(connectionStringTSLM.ConnectionString))
		{
			SWCConn.Open();
			sqlstr = " SELECT dateadd(day,20,MIN(SAA002)) AS DATE FROM SwcApply2001 where SWC000=@SWC000 ;";
			using (var cmd = SWCConn.CreateCommand())
			{
				cmd.CommandText = sqlstr;
				cmd.Parameters.Add(new SqlParameter("@SWC000", v));
				cmd.ExecuteNonQuery();
			
				using (SqlDataReader readerSWC = cmd.ExecuteReader())
				{
					if (readerSWC.HasRows)
					{
						while (readerSWC.Read())
						{
							savedate_3 = readerSWC["DATE"].ToString();
						}
					}
					readerSWC.Close();
				}
				cmd.Cancel();
			}
		}

        //核定查核定稿本最小值 rSWC109

        cell = new PdfPCell(new Phrase("定稿本製作", ChFont));
        cell.MinimumHeight = 30f;//設定行高
        cell.HorizontalAlignment = Element.ALIGN_CENTER;
        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        cell.Border = Rectangle.BOX;
        table1.AddCell(cell);

		if(SBApp.DateView(savedate_3, "03")=="")
			cell = new PdfPCell(new Phrase("-", ChFont));
		else
			cell = new PdfPCell(new Phrase("發文期限：" + SBApp.DateView(savedate_3, "03"), ChFont));
        cell.MinimumHeight = 30f;//設定行高
        cell.HorizontalAlignment = Element.ALIGN_CENTER;
        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        cell.Border = Rectangle.BOX;
        table1.AddCell(cell);

		if(SBApp.DateView(rSWC109, "03")=="")
			cell = new PdfPCell(new Phrase("-", ChFont));
		else
			cell = new PdfPCell(new Phrase("發文日期：" + SBApp.DateView(rSWC109, "03"), ChFont));
        cell.MinimumHeight = 30f;//設定行高
        cell.HorizontalAlignment = Element.ALIGN_CENTER;
        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        cell.Border = Rectangle.BOX;
        table1.AddCell(cell);

        temp = "";
        ts = new TimeSpan();
        if (SBApp.DateView(rSWC109,"00") != "" && SBApp.DateView(savedate_3,"00") != "")
            ts = new TimeSpan(Convert.ToDateTime(rSWC109).Ticks - Convert.ToDateTime(savedate_3).Ticks);
        if (ts.Days > 0) { delay += ts.Days; temp = ts.Days.ToString() + "天"; }
        else { temp = ""; }
        cell = new PdfPCell(new Phrase(temp, ChFont));
        cell.MinimumHeight = 30f;//設定行高
        cell.HorizontalAlignment = Element.ALIGN_CENTER;
        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        cell.Border = Rectangle.BOX;
        table1.AddCell(cell);


        //rSWC088 rSWC109
        cell = new PdfPCell(new Phrase("總審查期限", ChFont));
        cell.MinimumHeight = 30f;//設定行高
        cell.HorizontalAlignment = Element.ALIGN_CENTER;
        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        cell.Border = Rectangle.BOX;
        table1.AddCell(cell);

		if(SBApp.DateView(rSWC088, "03")=="")
			cell = new PdfPCell(new Phrase("-", ChFont));
		else
			cell = new PdfPCell(new Phrase("發文期限：" + SBApp.DateView(rSWC088, "03"), ChFont));
        cell.MinimumHeight = 30f;//設定行高
        cell.HorizontalAlignment = Element.ALIGN_CENTER;
        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        cell.Border = Rectangle.BOX;
        table1.AddCell(cell);

		if(SBApp.DateView(rSWC109, "03")=="")
			cell = new PdfPCell(new Phrase("-", ChFont));
		else
			cell = new PdfPCell(new Phrase("發文日期：" + SBApp.DateView(rSWC109, "03"), ChFont));
        cell.MinimumHeight = 30f;//設定行高
        cell.HorizontalAlignment = Element.ALIGN_CENTER;
        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        cell.Border = Rectangle.BOX;
        table1.AddCell(cell);

        temp = "";
        ts = new TimeSpan();
        if (SBApp.DateView(rSWC109, "03") != "" && SBApp.DateView(rSWC088, "03") != "")
            ts = new TimeSpan(Convert.ToDateTime(rSWC109).Ticks - Convert.ToDateTime(rSWC088).Ticks);
        if (ts.Days > 0) { delay += ts.Days; temp = ts.Days.ToString() + "天"; }
        else { temp = ""; }
        cell = new PdfPCell(new Phrase(temp, ChFont));
        cell.MinimumHeight = 30f;//設定行高
        cell.HorizontalAlignment = Element.ALIGN_CENTER;
        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        cell.Border = Rectangle.BOX;
        table1.AddCell(cell);
		
        //rSWC125 SwcApply2001多筆撈最大
        using (SqlConnection TSLMConn = new SqlConnection(connectionStringTSLM.ConnectionString))
        {
            TSLMConn.Open();
            sqlstr = " SELECT MAX(savedate) savedate FROM SwcApply2001 where SWC000=@SWC000;";
            using (var cmd = TSLMConn.CreateCommand())
            {
                cmd.CommandText = sqlstr;
                cmd.Parameters.Add(new SqlParameter("@SWC000", v));
                cmd.ExecuteNonQuery();

                using (SqlDataReader readerTSLM = cmd.ExecuteReader())
                {
                    if (readerTSLM.HasRows)
                    {
                        while (readerTSLM.Read())
                        {
                            savedate_4 = readerTSLM["savedate"].ToString();
                        }
                    }
                    readerTSLM.Close();
                }
                cmd.Cancel();
            }
        }

		if(SBApp.DateView(rSWC125, "03")==""){}
		else{
			cell = new PdfPCell(new Phrase("建議核定/不予核定補正", ChFont));
			cell.MinimumHeight = 30f;//設定行高
			cell.HorizontalAlignment = Element.ALIGN_CENTER;
			cell.VerticalAlignment = Element.ALIGN_MIDDLE;
			cell.Border = Rectangle.BOX;
			table1.AddCell(cell);
	
			if(SBApp.DateView(rSWC125, "03")=="")
				cell = new PdfPCell(new Phrase("-", ChFont));
			else
				cell = new PdfPCell(new Phrase("發文期限：" + SBApp.DateView(rSWC125, "03"), ChFont));
			cell.MinimumHeight = 30f;//設定行高
			cell.HorizontalAlignment = Element.ALIGN_CENTER;
			cell.VerticalAlignment = Element.ALIGN_MIDDLE;
			cell.Border = Rectangle.BOX;
			table1.AddCell(cell);
	
			if(SBApp.DateView(savedate_4, "03")=="")
				cell = new PdfPCell(new Phrase("-", ChFont));
			else
				cell = new PdfPCell(new Phrase("發文日期：" + SBApp.DateView(savedate_4, "03"), ChFont));
			cell.MinimumHeight = 30f;//設定行高
			cell.HorizontalAlignment = Element.ALIGN_CENTER;
			cell.VerticalAlignment = Element.ALIGN_MIDDLE;
			cell.Border = Rectangle.BOX;
			table1.AddCell(cell);
	
			temp = "";
			ts = new TimeSpan();
			if (SBApp.DateView(rSWC125, "03") != "" && SBApp.DateView(savedate_4, "03") != "")
				ts = new TimeSpan(Convert.ToDateTime(savedate_4).Ticks - Convert.ToDateTime(rSWC125).Ticks);
			if (ts.Days > 0) { delay += ts.Days; temp = ts.Days.ToString() + "天"; }
			else { temp = ""; }
			cell = new PdfPCell(new Phrase(temp, ChFont));
			cell.MinimumHeight = 30f;//設定行高
			cell.HorizontalAlignment = Element.ALIGN_CENTER;
			cell.VerticalAlignment = Element.ALIGN_MIDDLE;
			cell.Border = Rectangle.BOX;
			table1.AddCell(cell);
		}
        


        cell = new PdfPCell(new Phrase("總逾期天數：" + delay.ToString() + " 天", ChFont_bold));
        cell.Colspan = 4;
        cell.MinimumHeight = 30f;//設定行高
        cell.HorizontalAlignment = Element.ALIGN_CENTER;
        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        cell.Border = Rectangle.BOX;
        table1.AddCell(cell);
		

        temp = "※歷次審查階段發文日期逾收文日期次日起20日者，每日扣罰委託服務費千分之三" + "\n" + "※建議核定日期逾審查期限者，每日扣罰委託服務費千分之三" + "\n" + "※補正日期逾補正期限者，每日扣罰委託服務費千分之三並暫停輪值一次";
		cell = new PdfPCell(new Phrase(temp, ChFont_red));
        cell.Colspan = 4;
        cell.MinimumHeight = 70f;//設定行高
        cell.HorizontalAlignment = Element.ALIGN_LEFT;
        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        cell.Border = Rectangle.BOX;
        table1.AddCell(cell);

		


        doc1.Add(table1);


        doc1.Close();



        //使用者下載，另存新檔 \r\n
        Response.Clear();
        //Response.AddHeader("content-disposition", "attachment;filename=管制時程表_" + (DateTime.Now.Year - 1911) + (DateTime.Now.ToString("MMddmmss")) + ".pdf");
        Response.AddHeader("content-disposition", "attachment;filename=管制時程表_" + v + ".pdf");
        Response.ContentType = "application/octet-stream";
        Response.OutputStream.Write(Memory.GetBuffer(), 0, Memory.GetBuffer().Length);
        Response.OutputStream.Flush();
        Response.OutputStream.Close();
        Response.Flush();
        Response.End();
        //ooooooooooooooooooooooooooooooooooooooooooooooo

    }

    private float strlength(string str)
    {
        float op = 30f;
        if (str.Length <= 37) op = 30f;
        else if (str.Length <= 37*2) op = 40f;
        else if (str.Length <= 37*3) op = 50f;
        return op;
    }
}