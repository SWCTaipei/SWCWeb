using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class SWCAPPLY_SWCDT007L : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string ssUserType = Session["UserType"] + "";   //大地工程處人員
        string ssEdit4 = Session["Edit4"] + "";   //完工檢查公會

        //ssUserType = "3";
        if (ssEdit4 == "Y" || ssUserType=="3") {if(!IsPostBack) GridViewDate(); } else { Response.Redirect("../SWCDOC/SWC001.aspx"); }
    }

    private void GridViewDate()
    {
        string searchQ01 = TXTS001.Text;
        string searchQ02 = TXTS002.Text;
        string searchQ03 = TXTS003.Text;
        string searchQ04 = TXTS004.Text;
        string searchQ05 = TXTS005.Text;
        string searchDD1 = DropDownList1.SelectedValue;
        string searchDD2 = DropDownList2.SelectedValue;
        string searchDD3 = DropDownList3.SelectedValue;

        string pgaeGVData = " select D7.DTLG001 AS FD001,left(convert(char, D7.DTLG002, 120),10) AS FD002,SWC.SWC013 AS FD003,SWC.SWC002 AS FD004,SWC.SWC005 AS FD005,D7.DTLG004 AS FD006,D7.DTLG017 AS FD007,D7.DTLG019 AS FD008,D7.DTLG021 AS FD009,D7.DTLG027 AS FD010,D7.DTLG032 AS FD011,D7.DTLG037 AS FD012 from SWCDTL07 D7 Left join swccase SWC on SWC.SWC000=D7.SWC000 where D7.DATALOCK='Y' order by D7.id desc;";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString)) {
            SwcConn.Open();
            using (var cmd = SwcConn.CreateCommand())
            {
                cmd.CommandText = pgaeGVData;
                cmd.ExecuteNonQuery();

                using (SqlDataReader readerSwc = cmd.ExecuteReader())
                {
                    if (readerSwc.HasRows) {

                        DataTable dtD7GV = new DataTable();
                        DataTable dt = new DataTable();
                        dt.Columns.Add(new DataColumn("FD001", typeof(string)));
                        dt.Columns.Add(new DataColumn("FD002", typeof(string)));
                        dt.Columns.Add(new DataColumn("FD003", typeof(string)));
                        dt.Columns.Add(new DataColumn("FD004", typeof(string)));
                        dt.Columns.Add(new DataColumn("FD005", typeof(string)));
                        dt.Columns.Add(new DataColumn("FD006", typeof(string)));
                        ViewState["D7GV"] = dt;
                        dtD7GV = dt;

                        while (readerSwc.Read())
                        {
                            string qFD001 = readerSwc["FD001"] + "";
                            string qFD002 = readerSwc["FD002"] + "";
                            string qFD003 = readerSwc["FD003"] + "";
                            string qFD004 = readerSwc["FD004"] + "";
                            string qFD005 = readerSwc["FD005"] + "";
                            string qFD006 = readerSwc["FD006"] + "";
                            string qFD007 = readerSwc["FD007"] + "";
                            string qFD008 = readerSwc["FD008"] + "";
                            string qFD009 = readerSwc["FD009"] + "";
                            string qFD010 = readerSwc["FD010"] + "";
                            string qFD011 = readerSwc["FD011"] + "";
                            string qFD012 = readerSwc["FD012"] + "";

                            bool dvIns = true;
                            if (searchQ01.Trim() != "") { if (qFD001 != searchQ01) { dvIns = false; } }
                            if (searchQ02.Trim() != "") { if (qFD004 != searchQ02) { dvIns = false; } }
                            if (searchQ03.Trim() != "") { if (qFD005.IndexOf(searchQ03) < 0) { dvIns = false; } }
                            if (searchQ04.Trim() != "") { if (qFD003.IndexOf(searchQ04) < 0) { dvIns = false; } }
                            if (searchQ05.Trim() != "") { if (qFD006.IndexOf(searchQ05) < 0) { dvIns = false; } }
                            if (CheckBox1.Checked || CheckBox2.Checked || CheckBox3.Checked) {
                                bool tempCK = false;
                                if (CheckBox1.Checked) { tempCK = qFD007 == "1" ? true : tempCK; }
                                if (CheckBox2.Checked) { tempCK = qFD008 == "1" ? true : tempCK; }
                                if (CheckBox3.Checked) { tempCK = qFD009 == "1" ? true : tempCK; }
                                if (!tempCK) dvIns = false;
                            }
                            if (searchDD1.Trim() != "" && qFD010 != searchDD1) { dvIns = false; }
                            if (searchDD2.Trim() != "" && qFD011 != searchDD2) { dvIns = false; }
                            if (searchDD3.Trim() != "" && qFD012 != searchDD3) { dvIns = false; }

                            if (dvIns) {
                            DataRow trD7GV = dtD7GV.NewRow();
                            trD7GV["FD001"] = qFD001;
                            trD7GV["FD002"] = qFD002;
                            trD7GV["FD003"] = qFD003;
                            trD7GV["FD004"] = qFD004;
                            trD7GV["FD005"] = qFD005;
                            trD7GV["FD006"] = qFD006;
                            dtD7GV.Rows.Add(trD7GV);
                            ViewState["D7GV"] = dtD7GV; }
                        }
                        CaseCount.Text = dtD7GV.Rows.Count.ToString();
                        GridView1.DataSource = dtD7GV;
                        GridView1.DataBind();
                    }
                    readerSwc.Close();
                }
                cmd.Cancel();
            }
        }
    }
    protected void GVSWCList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Class1 C1 = new Class1();
        string jAct = e.CommandName;
        switch (jAct)
        {
            case "detail":
                int aa = GridView1.Rows[Convert.ToInt32(e.CommandArgument)].RowIndex;
                string jSWC002 = GridView1.Rows[aa].Cells[1].Text;
                string jKeyValue = GridView1.Rows[aa].Cells[0].Text;
                string goUrl = "../SWCDOC/SWCDT007.aspx?SWCNO=" + C1.getSWC000(jSWC002) + "&DTLNO=" + jKeyValue;
                //Response.Redirect("../SWCDOC/SWCDT007.aspx?SWCNO=" + C1.getSWC000(jSWC002) + "&DTLNO=" + jKeyValue);
                Response.Write("<script>window.open('"+ goUrl + "');</script>");
                break;
        }

    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
    }

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {

    }

    protected void GridView1_PageIndexChanged(object sender, EventArgs e)
    {
        GridView1.DataSource = ViewState["D7GV"];
        GridView1.DataBind();
    }

    protected void ExeQSel_Click(object sender, EventArgs e)
    {
        GridViewDate();
    }

    protected void RemoveSel_Click(object sender, EventArgs e)
    {
        TXTS001.Text = "";
        TXTS002.Text = "";
        TXTS003.Text = "";
        TXTS004.Text = "";
        CheckBox1.Checked = false;
        CheckBox2.Checked = false;
        CheckBox3.Checked = false;
        DropDownList1.SelectedValue = "";
        DropDownList2.SelectedValue = "";
        DropDownList3.SelectedValue = "";
        GridViewDate();
    }

    protected void WriteExcel_Click(object sender, ImageClickEventArgs e)
    {
        string ExcelFileName = AppDomain.CurrentDomain.BaseDirectory + "/OutputFile/" + DateTime.Now.ToString("yyyy-MM-dd_hhmmss") + "臺北市山坡地水土保持設施維護檢查及輔導紀錄表.xls";

        Response.Clear();
        Response.Buffer = true;
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");

        FileStream Excelfilestream = new FileStream(ExcelFileName, FileMode.Create, FileAccess.Write);
        StreamWriter Excelstreamwrite = new StreamWriter(Excelfilestream, System.Text.Encoding.GetEncoding("unicode"));

        string[] arrayTitle = new string[] {"設施維護檢表編號","水保局編號","水土保持申請書件名稱","檢查日期","義務人" };
        String Excelcol = "";
        //建立標題行各欄位
        for (int i=0;i<arrayTitle.Length;i++) { Excelcol += arrayTitle[i]+"\t"; }
        //把第一行的標題行寫入串流
        Excelstreamwrite.WriteLine(Excelcol.ToString());

        //開始填入裡面的值
        DataTable DTGV = (DataTable)ViewState["D7GV"];
        int totalRow = DTGV.Rows.Count;
        for (int i = 0; i < totalRow; i++)
        {
            string dt001 = DTGV.Rows[i][0].ToString();
            string dt002 = DTGV.Rows[i][3].ToString();
            string dt003 = DTGV.Rows[i][4].ToString().Replace("\n", "").Replace("\r", "");
            string dt004 = DTGV.Rows[i][1].ToString();
            string dt005 = DTGV.Rows[i][2].ToString();

            Excelcol = "";
            string[] arrayData = new string[] { dt001, dt002, dt003, dt004, dt005 };
            for (int j = 0; j < arrayData.Length; j++) { Excelcol += arrayData[j] + "\t"; }
            Excelstreamwrite.WriteLine(Excelcol.ToString());
        }
        Excelstreamwrite.Close();

        Response.AddHeader("content-disposition", "attachment;filename=" + Server.UrlEncode(DateTime.Now.ToString("yyyy-MM-dd_hhmmss") + "臺北市山坡地水土保持設施維護檢查及輔導紀錄表.xls"));
        Response.ContentType = "application/ms-excel";

        //指定返回的是一個不能被用戶端讀取的流，必須被下載
        Response.WriteFile(ExcelFileName);

        //把檔流發送到用戶端
        Response.End();

    }
    protected void WriteOds_Click(object sender, ImageClickEventArgs e)
    {
        string tempFilseName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".ods";
        string outputFilePath = ConfigurationManager.AppSettings["SwcFileTemp"] + tempFilseName;

        DataTable OBJ_GV1 = (DataTable)ViewState["D7GV"];
        DataTable OBJ_GV;
        DataTable DTGV = new DataTable();

        DTGV.Columns.Add(new DataColumn("DTSWC000", typeof(string)));
        DTGV.Columns.Add(new DataColumn("DTSWC001", typeof(string)));
        DTGV.Columns.Add(new DataColumn("DTSWC002", typeof(string)));
        DTGV.Columns.Add(new DataColumn("DTSWC003", typeof(string)));
        DTGV.Columns.Add(new DataColumn("DTSWC004", typeof(string)));
        OBJ_GV = DTGV;

        DataRow drTitle = OBJ_GV.NewRow();
        drTitle["DTSWC000"] = "設施維護檢表編號";
        drTitle["DTSWC001"] = "水保局編號";
        drTitle["DTSWC002"] = "水土保持申請書件名稱";
        drTitle["DTSWC003"] = "檢查日期";
        drTitle["DTSWC004"] = "義務人";
        OBJ_GV.Rows.Add(drTitle);

        int totalRow = OBJ_GV1.Rows.Count;
        for (int i = 0; i < totalRow; i++)
        {
            DataRow drDATA = OBJ_GV.NewRow();

            string dt001 = OBJ_GV1.Rows[i][0].ToString();
            string dt002 = OBJ_GV1.Rows[i][3].ToString();
            string dt003 = OBJ_GV1.Rows[i][4].ToString().Replace("\n", "").Replace("\r", "");
            string dt004 = OBJ_GV1.Rows[i][1].ToString();
            string dt005 = OBJ_GV1.Rows[i][2].ToString();

            drDATA["DTSWC000"] = dt001;
            drDATA["DTSWC001"] = dt002;
            drDATA["DTSWC002"] = dt003;
            drDATA["DTSWC003"] = dt004;
            drDATA["DTSWC004"] = dt005;
            OBJ_GV.Rows.Add(drDATA);
        }
        DataSet ds = new DataSet();

        if (OBJ_GV != null)
        {
            //DataTable 加入 dataSet裡面
            ds.Tables.Add(OBJ_GV);

            WriteOdsFile(ds, outputFilePath);
            Response.Redirect("..\\UpLoadFiles\\temp\\" + tempFilseName);
        }
        else
        {
            tempFilseName = "臺北市山坡地水土保持設施維護檢查及輔導紀錄表.ods";
            Response.Redirect("..\\UpLoadFiles\\temp\\" + tempFilseName);
        }
    }
    public void WriteOdsFile(DataSet odsFile, string outputFilePath)
    {
        string tFilePath = ConfigurationManager.AppSettings["SwcSysFilePath"] + "template.ods";

        ZipFile templateFile = this.GetZipFile(tFilePath);

        XmlDocument contentXml = this.GetContentXmlFile(templateFile);

        XmlNamespaceManager nmsManager = this.InitializeXmlNamespaceManager(contentXml);

        XmlNode sheetsRootNode = this.GetSheetsRootNodeAndRemoveChildrens(contentXml, nmsManager);


        foreach (DataTable sheet in odsFile.Tables)
            this.SaveSheet(sheet, sheetsRootNode);

        this.SaveContentXml(templateFile, contentXml);

        templateFile.Save(outputFilePath);
    }
    private void SaveContentXml(ZipFile templateFile, XmlDocument contentXml)
    {
        templateFile.RemoveEntry("content.xml");

        MemoryStream memStream = new MemoryStream();
        contentXml.Save(memStream);
        memStream.Seek(0, SeekOrigin.Begin);

        templateFile.AddEntry("content.xml", memStream);
    }

    // Read zip file (.ods file is zip file).
    private ZipFile GetZipFile(string inputFilePath)
    {
        return ZipFile.Read(inputFilePath);
    }
    private XmlDocument GetContentXmlFile(ZipFile zipFile)
    {
        // Get file(in zip archive) that contains data ("content.xml").
        ZipEntry contentZipEntry = zipFile["content.xml"];

        // Extract that file to MemoryStream.
        Stream contentStream = new MemoryStream();
        contentZipEntry.Extract(contentStream);
        contentStream.Seek(0, SeekOrigin.Begin);

        // Create XmlDocument from MemoryStream (MemoryStream contains content.xml).
        XmlDocument contentXml = new XmlDocument();
        contentXml.Load(contentStream);

        return contentXml;
    }
    private XmlNamespaceManager InitializeXmlNamespaceManager(XmlDocument xmlDocument)
    {
        XmlNamespaceManager nmsManager = new XmlNamespaceManager(xmlDocument.NameTable);

        for (int i = 0; i < namespaces.GetLength(0); i++)
            nmsManager.AddNamespace(namespaces[i, 0], namespaces[i, 1]);

        return nmsManager;
    }
    private XmlNode GetSheetsRootNodeAndRemoveChildrens(XmlDocument contentXml, XmlNamespaceManager nmsManager)
    {
        XmlNodeList tableNodes = this.GetTableNodes(contentXml, nmsManager);

        XmlNode sheetsRootNode = tableNodes.Item(0).ParentNode;
        // remove sheets from template file
        foreach (XmlNode tableNode in tableNodes)
            sheetsRootNode.RemoveChild(tableNode);

        return sheetsRootNode;
    }
    // In ODF sheet is stored in table:table node
    private XmlNodeList GetTableNodes(XmlDocument contentXmlDocument, XmlNamespaceManager nmsManager)
    {
        return contentXmlDocument.SelectNodes("/office:document-content/office:body/office:spreadsheet/table:table", nmsManager);
    }
    private string GetNamespaceUri(string prefix)
    {
        for (int i = 0; i < namespaces.GetLength(0); i++)
        {
            if (namespaces[i, 0] == prefix)
                return namespaces[i, 1];
        }

        throw new InvalidOperationException("Can't find that namespace URI");
    }
    private void SaveSheet(DataTable sheet, XmlNode sheetsRootNode)
    {
        XmlDocument ownerDocument = sheetsRootNode.OwnerDocument;

        XmlNode sheetNode = ownerDocument.CreateElement("table:table", this.GetNamespaceUri("table"));

        XmlAttribute sheetName = ownerDocument.CreateAttribute("table:name", this.GetNamespaceUri("table"));
        sheetName.Value = sheet.TableName;
        sheetNode.Attributes.Append(sheetName);

        this.SaveColumnDefinition(sheet, sheetNode, ownerDocument);

        this.SaveRows(sheet, sheetNode, ownerDocument);

        sheetsRootNode.AppendChild(sheetNode);
    }
    private void SaveColumnDefinition(DataTable sheet, XmlNode sheetNode, XmlDocument ownerDocument)
    {
        XmlNode columnDefinition = ownerDocument.CreateElement("table:table-column", this.GetNamespaceUri("table"));

        XmlAttribute columnsCount = ownerDocument.CreateAttribute("table:number-columns-repeated", this.GetNamespaceUri("table"));
        columnsCount.Value = sheet.Columns.Count.ToString(CultureInfo.InvariantCulture);
        columnDefinition.Attributes.Append(columnsCount);

        sheetNode.AppendChild(columnDefinition);
    }
    private void SaveRows(DataTable sheet, XmlNode sheetNode, XmlDocument ownerDocument)
    {
        DataRowCollection rows = sheet.Rows;
        for (int i = 0; i < rows.Count; i++)
        {
            XmlNode rowNode = ownerDocument.CreateElement("table:table-row", this.GetNamespaceUri("table"));

            this.SaveCell(rows[i], rowNode, ownerDocument);

            sheetNode.AppendChild(rowNode);
        }
    }

    private void SaveCell(DataRow row, XmlNode rowNode, XmlDocument ownerDocument)
    {
        object[] cells = row.ItemArray;

        for (int i = 0; i < cells.Length; i++)
        {
            XmlElement cellNode = ownerDocument.CreateElement("table:table-cell", this.GetNamespaceUri("table"));

            if (row[i] != DBNull.Value)
            {
                // We save values as text (string)
                XmlAttribute valueType = ownerDocument.CreateAttribute("office:value-type", this.GetNamespaceUri("office"));
                valueType.Value = "string";
                cellNode.Attributes.Append(valueType);

                XmlElement cellValue = ownerDocument.CreateElement("text:p", this.GetNamespaceUri("text"));
                cellValue.InnerText = row[i].ToString();
                cellNode.AppendChild(cellValue);
            }

            rowNode.AppendChild(cellNode);
        }
    }
    private static string[,] namespaces = new string[,]
     {
            {"table", "urn:oasis:names:tc:opendocument:xmlns:table:1.0"},
            {"office", "urn:oasis:names:tc:opendocument:xmlns:office:1.0"},
            {"style", "urn:oasis:names:tc:opendocument:xmlns:style:1.0"},
            {"text", "urn:oasis:names:tc:opendocument:xmlns:text:1.0"},
            {"draw", "urn:oasis:names:tc:opendocument:xmlns:drawing:1.0"},
            {"fo", "urn:oasis:names:tc:opendocument:xmlns:xsl-fo-compatible:1.0"},
            {"dc", "http://purl.org/dc/elements/1.1/"},
            {"meta", "urn:oasis:names:tc:opendocument:xmlns:meta:1.0"},
            {"number", "urn:oasis:names:tc:opendocument:xmlns:datastyle:1.0"},
            {"presentation", "urn:oasis:names:tc:opendocument:xmlns:presentation:1.0"},
            {"svg", "urn:oasis:names:tc:opendocument:xmlns:svg-compatible:1.0"},
            {"chart", "urn:oasis:names:tc:opendocument:xmlns:chart:1.0"},
            {"dr3d", "urn:oasis:names:tc:opendocument:xmlns:dr3d:1.0"},
            {"math", "http://www.w3.org/1998/Math/MathML"},
            {"form", "urn:oasis:names:tc:opendocument:xmlns:form:1.0"},
            {"script", "urn:oasis:names:tc:opendocument:xmlns:script:1.0"},
            {"ooo", "http://openoffice.org/2004/office"},
            {"ooow", "http://openoffice.org/2004/writer"},
            {"oooc", "http://openoffice.org/2004/calc"},
            {"dom", "http://www.w3.org/2001/xml-events"},
            {"xforms", "http://www.w3.org/2002/xforms"},
            {"xsd", "http://www.w3.org/2001/XMLSchema"},
            {"xsi", "http://www.w3.org/2001/XMLSchema-instance"},
            {"rpt", "http://openoffice.org/2005/report"},
            {"of", "urn:oasis:names:tc:opendocument:xmlns:of:1.2"},
            {"rdfa", "http://docs.oasis-open.org/opendocument/meta/rdfa#"},
            {"config", "urn:oasis:names:tc:opendocument:xmlns:config:1.0"}
     };
}