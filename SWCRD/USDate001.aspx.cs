using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SWCRD_USDate001 : System.Web.UI.Page
{
    string SaveDate = "";
    string ID;

    protected void Page_Load(object sender, EventArgs e)
    {
        ID = Session["ID"] + "";
        show(ID);
        // Label1.Text = Session["ID"]+"";
    }
    protected void show(string ID)
    {


        try
        {

            ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
            using (SqlConnection UserConn = new SqlConnection(connectionString.ConnectionString))
            {
                UserConn.Open();

                // string sqlSelectStr = "INSERT INTO [dbo].[dataupdate]   ([dname] ,[IDnumber] ,[Cell_phone] ,[email] ,[address])  VALUES  " + ADD[0] + " , " + ADD[1] + " , " + ADD[2] + " , " + ADD[3] + " , " + ADD[4];
                string sqlSelectStr = "  select SWC013, SWC013ID, SWC013TEL,  UI004 , SWC014, savedate from( ";
                sqlSelectStr += "   SELECT          SWCCASE.SWC013, isnull(SWCCASE.SWC013ID,'') as SWC013ID, isnull(SWCCASE.SWC013TEL,'') as SWC013TEL,isnull( SWCCASE.SWC014,'') as SWC014, isnull(UserInfo.UI004,'') as UI004, SWCCASE.savedate,ROW_NUMBER() over(partition by SWCCASE.SWC013ID order by SWCCASE.savedate  desc) as r ";
                sqlSelectStr += " FROM              SWCCASE LEFT OUTER JOIN          UserInfo ON SWCCASE.SWC013ID = UserInfo.UI002	)	 as W 		 ";
                sqlSelectStr += " where r = '1'  and  SWC013ID = '" + ID + "'";
                sqlSelectStr += " order by savedate DESC";

                SqlDataReader readerUsers;
                SqlCommand objCmdUser = new SqlCommand(sqlSelectStr, UserConn);
                readerUsers = objCmdUser.ExecuteReader();

                while (readerUsers.Read())
                {
                    SWC013.Text = readerUsers["SWC013"] + " ";
                    SWC013ID.Text = readerUsers["SWC013ID"] + " ";
                    SWC013TEL.Text = readerUsers["SWC013TEL"] + " ";
                    SWC014.Text = readerUsers["SWC014"] + " ";
                    email.Text = readerUsers["UI004"] + " ";
                    SaveDate = readerUsers["savedate"] + "";


                }

                objCmdUser.Dispose();
                readerUsers.Close();
                UserConn.Close();
                UserConn.Dispose();
            }
        }
        catch (Exception ex)
        {


            Label1.Text = ex + "";


        }










    }


    protected void ADD(string[] ADD)
    {


        try
        {

            ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
            using (SqlConnection UserConn = new SqlConnection(connectionString.ConnectionString))
            {
                UserConn.Open();
                //帳號 姓名 身份證號 手機 信箱 地址 更新日 存檔日
                string sqlSelectStr = "  INSERT INTO [dbo].[UserInfo]   ([UI000],[UI001],[UI002] ,[UI003] ,[UI004] ,[UI005] ,[UpdDate] ,[SaveDate]) ";
                sqlSelectStr += "VALUES ('" + ID + "' ,'" + ADD[0] + "','" + ADD[1] + "','" + ADD[2] + "','" + ADD[3] + "','" + ADD[4] + "', GetDate(),'" + ADD[5] + "')";


                SqlDataReader readerUsers;
                SqlCommand objCmdUser = new SqlCommand(sqlSelectStr, UserConn);
                readerUsers = objCmdUser.ExecuteReader();

                while (readerUsers.Read())
                {

                }

                objCmdUser.Dispose();
                readerUsers.Close();
                UserConn.Close();
                UserConn.Dispose();
            }
            Label1.Text = "上傳成功";

        }
        catch (Exception ex)
        {
            Label1.Text = ex + "";
        }



    }








    protected void judgment(string[] Obligor)
    {
        int a = 0;

        try//更新原資料
        {

            ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
            using (SqlConnection UserConn = new SqlConnection(connectionString.ConnectionString))
            {
                UserConn.Open();
                string sqlSelectStr = "UPDATE SWCCASE  ";
                sqlSelectStr += " set SWC014 = '" + Obligor[4] + "' ";
                sqlSelectStr += " WHERE SWC013ID  ='" + ID + "'   ";


                SqlDataReader readerUsers;
                SqlCommand objCmdUser = new SqlCommand(sqlSelectStr, UserConn);
                readerUsers = objCmdUser.ExecuteReader();

                while (readerUsers.Read())
                {

                }
                objCmdUser.Dispose();
                readerUsers.Close();
                UserConn.Close();
                UserConn.Dispose();
            }
            Label1.Text = "上傳成功";
        }
        catch (Exception ex)
        {
            //   Response.Write("< script > alert('" + sqlSelectStr + "') </ script >");
        }




        try//判斷是否有這筆資料
        {

            ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
            using (SqlConnection UserConn = new SqlConnection(connectionString.ConnectionString))
            {
                UserConn.Open();

                string sqlSelectStr = " SELECT  [UI002]  FROM [TCGESWC].[dbo].[UserInfo]  ";
                sqlSelectStr += "  where [UI002] = '" + ID + "'";


                SqlDataReader readerUsers;
                SqlCommand objCmdUser = new SqlCommand(sqlSelectStr, UserConn);
                readerUsers = objCmdUser.ExecuteReader();

                if (readerUsers.HasRows)
                {
                    a = 1;
                }

                objCmdUser.Dispose();
                readerUsers.Close();
                UserConn.Close();
                UserConn.Dispose();
            }

        }
        catch (Exception ex)
        {
            Label1.Text = ex + "";
        }
        if (a == 0)
        {

            ADD(Obligor);

        }
        else
        {


            Update(Obligor);

        }





    }

    private void Update(string[] obligor)
    {
        DateTime dt = DateTime.Now; // 取得現在時間
        String str = dt.ToString();
        string sqlSelectStr = "UPDATE UserInfo  ";
        sqlSelectStr += " SET UI004='" + obligor[3] + "',UI005 ='" + obligor[4] + "', UpdDate = GetDate()";
        sqlSelectStr += " WHERE UI002 ='" + obligor[1] + "'   ";
        try//判斷是否有這筆資料
        {

            ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
            using (SqlConnection UserConn = new SqlConnection(connectionString.ConnectionString))
            {
                UserConn.Open();



                SqlDataReader readerUsers;
                SqlCommand objCmdUser = new SqlCommand(sqlSelectStr, UserConn);
                readerUsers = objCmdUser.ExecuteReader();

                while (readerUsers.Read())
                {

                }
                objCmdUser.Dispose();
                readerUsers.Close();
                UserConn.Close();
                UserConn.Dispose();
            }
            Label1.Text = "上傳成功";
        }
        catch (Exception ex)
        {
            Response.Write("< script > alert('" + sqlSelectStr + "') </ script >");
        }



    }

    protected void Button1_Click(object sender, EventArgs e)
    {      //  Label1.Text = "承接js傳過來的參數是 " + Request.Params["Semail"];


        apr();

        Response.Write("<script>alert('資料已存檔'); </script>");
        show(ID);
        Label1.Text = Session["PW"] + "";
    }

    public void apr()
    {


        string[] Obligor = new string[7];

        Obligor[0] = SWC013.Text + "";//義務人名稱

        Obligor[1] = SWC013ID.Text + "";//義務人身分證

        Obligor[2] = SWC013TEL.Text + "";//義務人手機

        //    Obligor[4] = SWC014.Text + "";//義務人地址
        Obligor[4] = Request.Params["SSWC014"] + "";
        // Obligor[3] = email.Text + " ";//義務人信箱
        Obligor[3] = Request.Params["Semail"] + "";
        DateTime dt = DateTime.Now;
        String str = dt.ToString("yyyy-MM-dd hh:mm:ss");
        if (SaveDate == "")
        {

            SaveDate = str;
        }


        DateTime dt1 = Convert.ToDateTime(SaveDate);
        Obligor[5] = dt1.ToString("yyyy-MM-dd hh:mm:ss");//存檔時間
                                                         // 取得現在時間


        Obligor[6] = str;//更新時間




        judgment(Obligor);


    }




}