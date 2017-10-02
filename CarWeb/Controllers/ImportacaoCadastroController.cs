using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace CarWeb.Controllers
{
    public class ImportacaoCadastroController : Controller
    {
        // GET: ImportacaoCadastro
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            DataSet ds = new DataSet();
            if (Request.Files["file"].ContentLength > 0)
            {
                string fileExtension = System.IO.Path.GetExtension(Request.Files["file"].FileName);

                if (fileExtension == ".xls" || fileExtension == ".xlsx")
                {
                    string fileLocation = Server.MapPath("~/Content/") + Request.Files["file"].FileName;
                    if (System.IO.File.Exists(fileLocation))
                    {

                        System.IO.File.Delete(fileLocation);
                    }
                    Request.Files["file"].SaveAs(fileLocation);
                    string excelConnectionString = string.Empty;
                    excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    //connection String for xls file format.
                    if (fileExtension == ".xls")
                    {
                        excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    }
                    //connection String for xlsx file format.
                    else if (fileExtension == ".xlsx")
                    {

                        excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    }
                    //Create Connection to Excel work book and add oledb namespace
                    OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                    excelConnection.Open();
                    DataTable dt = new DataTable();

                    dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    if (dt == null)
                    {
                        return null;
                    }

                    String[] excelSheets = new String[dt.Rows.Count];
                    int t = 0;
                    //excel data saves in temp file here.
                    foreach (DataRow row in dt.Rows)
                    {
                        excelSheets[t] = row["TABLE_NAME"].ToString();
                        t++;
                    }
                    OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);


                    string query = string.Format("Select * from [{0}]", excelSheets[0]);
                    using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                    {
                        dataAdapter.Fill(ds);
                    }
                }
                if (fileExtension.ToString().ToLower().Equals(".xml"))
                {
                    string fileLocation = Server.MapPath("~/Content/") + Request.Files["FileUpload"].FileName;
                    if (System.IO.File.Exists(fileLocation))
                    {
                        System.IO.File.Delete(fileLocation);
                    }

                    Request.Files["FileUpload"].SaveAs(fileLocation);
                    XmlTextReader xmlreader = new XmlTextReader(fileLocation);
                    // DataSet ds = new DataSet();
                    ds.ReadXml(xmlreader);
                    xmlreader.Close();
                }

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ModelCarWeb"].ConnectionString);
                    string query = ("Insert into Cadastros values(@ncadastro,@idlote,@idmunicipio,@propietario,@cpf,@fonecontato,@propriedade,@comunidade," +
                        "@area,@statuscoodenada,@statusgoogle,@statusservico,@observacao,@cadastrador,@dtcadastro,@statusemissao,@numerocarba,@statusimpressao,@dtentrega)");
                    SqlCommand cmd = new SqlCommand(query, con);

                    if (ds.Tables[0].Rows[i][1].ToString() != "")
                    {
                       

                        cmd.Parameters.AddWithValue("@ncadastro", ds.Tables[0].Rows[i][0].ToString());
                        cmd.Parameters.AddWithValue("@idlote", ds.Tables[0].Rows[i][1].ToString());
                        cmd.Parameters.AddWithValue("@idmunicipio", ds.Tables[0].Rows[i][2].ToString());
                        string cpf = ds.Tables[0].Rows[i][3].ToString();

                        if (cpf == "")
                        {
                            cmd.Parameters.AddWithValue("@cpf", "");

                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@cpf", cpf);
                        }
                        string proprietario = ds.Tables[0].Rows[i][4].ToString();

                        if (proprietario == "")
                        {
                            cmd.Parameters.AddWithValue("@propietario", "");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@propietario", proprietario);
                        }

                       
                        string fonecontato = ds.Tables[0].Rows[i][5].ToString();
                        if (fonecontato == "")
                        {
                            cmd.Parameters.AddWithValue("@fonecontato", "");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@fonecontato", fonecontato);
                        }

                        string propriedade = ds.Tables[0].Rows[i][6].ToString();
                        if (propriedade == "")
                        {
                            cmd.Parameters.AddWithValue("@propriedade", "");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@propriedade", propriedade);
                        }
                       
                        string municipio = ds.Tables[0].Rows[i][7].ToString();
                        if (municipio != "")
                        {
                            cmd.Parameters.AddWithValue("@comunidade", municipio);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@comunidade", municipio);
                        }
                        string area = ds.Tables[0].Rows[i][8].ToString();
                        if (area != "")
                        {
                            cmd.Parameters.AddWithValue("@area", Convert.ToDecimal(area));
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@area", Convert.ToDecimal(0));
                        }
                        string coordenacao = ds.Tables[0].Rows[i][9].ToString();
                        if (coordenacao == "X")
                        {
                            cmd.Parameters.AddWithValue("@statuscoodenada", "COM COORDENADAS");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@statuscoodenada", "SEM COORDENADAS");
                        }
                        string google = ds.Tables[0].Rows[i][10].ToString();
                        if (google == "X")
                        {
                            cmd.Parameters.AddWithValue("@statusgoogle", "COM COORDENADAS");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@statusgoogle", "SEM COORDENADAS");
                        }
                        string servico = ds.Tables[0].Rows[i][11].ToString();
                        if (servico != "")
                        {
                            cmd.Parameters.AddWithValue("@statusservico", servico);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@statusservico", "");
                        }

                        string obs = ds.Tables[0].Rows[i][12].ToString();
                        if (obs == "")
                        {
                            cmd.Parameters.AddWithValue("@observacao", "");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@observacao", obs);
                        }
                        string cadastrador = ds.Tables[0].Rows[i][13].ToString();

                        if (cadastrador == "")
                        {
                            cmd.Parameters.AddWithValue("@cadastrador", "");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@cadastrador", cadastrador);
                        }
                  

                        string dtcadastro = ds.Tables[0].Rows[i][14].ToString();
                        if (dtcadastro == "")
                        {
                            cmd.Parameters.AddWithValue("@dtcadastro", SqlDbType.Date).Value = dtcadastro = "01/01/1900";

                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@dtcadastro", SqlDbType.Date).Value = Convert.ToDateTime(ds.Tables[0].Rows[i][14]).ToString("yyyy/MM/dd");
                        }
                        string emissao = ds.Tables[0].Rows[i][15].ToString();
                        if (emissao == "OK")
                        {
                            cmd.Parameters.AddWithValue("@statusemissao", "SIM");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@statusemissao", "NAO");
                        }
                        string numerocarba = ds.Tables[0].Rows[i][16].ToString();
                        if (numerocarba == "")
                        {
                            cmd.Parameters.AddWithValue("@numerocarba", "");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@numerocarba", numerocarba);
                        }
                        string statusimp = ds.Tables[0].Rows[i][17].ToString();
                        if (statusimp == "")
                        {
                            cmd.Parameters.AddWithValue("@statusimpressao","");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@statusimpressao", statusimp);
                        }

                        string dtentrega = ds.Tables[0].Rows[i][18].ToString();
                        if (dtentrega == "")
                        {
                            cmd.Parameters.AddWithValue("@dtentrega", SqlDbType.Date).Value = dtcadastro = "01/01/1900";
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@dtentrega", SqlDbType.Date).Value = Convert.ToDateTime(ds.Tables[0].Rows[i][18]).ToString("yyyy/MM/dd");
                        }
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                    }
                }

            }
            return View();
        }

    }
}