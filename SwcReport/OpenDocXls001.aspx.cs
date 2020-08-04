/*  Soil and Water Conservation Platform Project is a web applicant tracking system which allows citizen can search, view and manage their SWC applicant case.
    Copyright (C) <2020>  <Geotechnical Engineering Office, Public Works Department, Taipei City Government>

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License as
    published by the Free Software Foundation, either version 3 of the
    License, or any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Affero General Public License for more details.

    You should have received a copy of the GNU Affero General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

using Ionic.Zip;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Xml;

public partial class CsFolder_OpenDocXls01 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GenerateOds();

        //string tempFilseName = "匯出施工檢查檢核半年報表_1061029233037.ods";
        //Response.Redirect("..\\excelfile\\" + tempFilseName);
        //Response.Write("<script language='javascript'>window.close();</" + "script>");
    }

    private void GenerateOds()
    {
        GBClass001 SBApp = new GBClass001();

        string tempFilseName = "匯出施工檢查檢核半年報表_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".ods";
        string outputFilePath = ConfigurationManager.AppSettings["SwcFileTemp"] + tempFilseName;

        Int32 nI = 0;
        string PrintSQLStr = "";
        string tWhere = Session["TextSwcquerey"] + "";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            PrintSQLStr = " SELECT SWC.SWC000,SWC.SWC002,SWC.SWC005,SWC.SWC023,D3.DTLC002,D3.DATALOCK,D3.SAVEDATE FROM SWCCASE SWC LEFT JOIN SWCDTL03 D3 ON SWC.SWC000 = D3.SWC000 ";
            PrintSQLStr = PrintSQLStr + " WHERE D3.DTLC002 > DATEADD(MONTH, -6, GETDATE()) ";
            PrintSQLStr = PrintSQLStr + " ORDER BY D3.SWC000 ";
            
            SqlDataReader readerItem;
            SqlCommand objCmdItem = new SqlCommand(PrintSQLStr, SwcConn);
            readerItem = objCmdItem.ExecuteReader();

            while (readerItem.Read())
            {
                string dSWC000  = readerItem["SWC000"] + "";
                string dSWC002  = readerItem["SWC002"] + "";
                string dSWC005  = readerItem["SWC005"] + "";
                string dSWC023  = readerItem["SWC023"] + "";
                string dDTLC002 = readerItem["DTLC002"] + "";
                string dDATALOCK = readerItem["DATALOCK"] + "";
                string dSAVEDATE = readerItem["SAVEDATE"] + "";

                DataTable OBJ_GV = (DataTable)ViewState["GV"];
                DataTable DTGV = new DataTable();

                if (OBJ_GV == null)
                {
                    DTGV.Columns.Add(new DataColumn("SWCINI", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("SWC002", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("SWC005", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("SWC023", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTLC002", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("SENDDATE", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("SENDDAY", typeof(string)));

                    ViewState["GV"] = DTGV;
                    OBJ_GV = DTGV;

                    DataRow drTitle = OBJ_GV.NewRow();

                    drTitle["SWCINI"] = "NO.";
                    drTitle["SWC002"] = "行政審查案件編號";
                    drTitle["SWC005"] = "計劃名稱";
                    drTitle["SWC023"] = "面積/ha";
                    drTitle["DTLC002"] = "檢查日期";
                    drTitle["SENDDATE"] = "函送紀錄日期";
                    drTitle["SENDDAY"] = "送達天數";

                    OBJ_GV.Rows.Add(drTitle);

                    ViewState["GV"] = OBJ_GV;
                }

                string tREP01 = "";
                string tREP02 = "";

                DataRow drDATA = OBJ_GV.NewRow();

                nI = nI + 1;

                if (dDATALOCK == "Y")
                {
                    tREP01 = SBApp.DateView(dSAVEDATE, "00");
                    TimeSpan ts1 = Convert.ToDateTime(dSAVEDATE) - Convert.ToDateTime(dDTLC002);
                    tREP02 = ts1.Days.ToString();
                    tREP01 = SBApp.DateView(tREP01, "02");
                }

                drDATA["SWCINI"] = nI;
                drDATA["SWC002"] = dSWC002;
                drDATA["SWC005"] = dSWC005;
                drDATA["SWC023"] = dSWC023;
                drDATA["DTLC002"] = SBApp.DateView(dDTLC002, "02");
                drDATA["SENDDATE"] = tREP01;
                drDATA["SENDDAY"] = tREP02;
                
                OBJ_GV.Rows.Add(drDATA);

                ViewState["GV"] = OBJ_GV;
            }

            DataTable OBJ_GVSWC = (DataTable)ViewState["GV"];

            DataSet ds = new DataSet();

            //DataTable 加入 dataSet裡面
            ds.Tables.Add(OBJ_GVSWC);


            WriteOdsFile(ds, outputFilePath);

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
    private XmlNodeList GetTableNodes(XmlDocument contentXmlDocument, XmlNamespaceManager nmsManager)
    {
        return contentXmlDocument.SelectNodes("/office:document-content/office:body/office:spreadsheet/table:table", nmsManager);
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

    private void SaveContentXml(ZipFile templateFile, XmlDocument contentXml)
    {
        templateFile.RemoveEntry("content.xml");

        MemoryStream memStream = new MemoryStream();
        contentXml.Save(memStream);
        memStream.Seek(0, SeekOrigin.Begin);

        templateFile.AddEntry("content.xml", memStream);
    }
    private void SaveColumnDefinition(DataTable sheet, XmlNode sheetNode, XmlDocument ownerDocument)
    {
        XmlNode columnDefinition = ownerDocument.CreateElement("table:table-column", this.GetNamespaceUri("table"));

        XmlAttribute columnsCount = ownerDocument.CreateAttribute("table:number-columns-repeated", this.GetNamespaceUri("table"));
        columnsCount.Value = sheet.Columns.Count.ToString(CultureInfo.InvariantCulture);
        columnDefinition.Attributes.Append(columnsCount);

        sheetNode.AppendChild(columnDefinition);
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