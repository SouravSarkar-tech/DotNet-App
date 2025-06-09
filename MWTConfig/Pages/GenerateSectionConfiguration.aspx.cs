using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.IO;
using System.Configuration;
public partial class Pages_GenerateSectionConfiguration : BasePage
{
    string ConfigDir = ConfigurationManager.AppSettings["ConfigDir"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        try
        {
            List<SectionMaster> sectionList = MWTEntities.SectionMasters.Where(x => x.Active == true).ToList();
            char[] charsToTrim = { ' ', '\t','.' };

            foreach (var section in sectionList)
            {
                //List<SectionFieldMaster> sectionFields = MWTEntities.SectionFieldMasters.Where(x => x.SectionID == section.ID && x.Active == true).ToList();
                //if (sectionFields != null)
                //    if (sectionFields.Count > 0)
                //    {
                        string sectionName = section.Name.Trim(charsToTrim).Replace(' ', '_');
                        string fileName = (sectionName + ".cs");
                        string fullFilePath = ConfigDir + "/" + fileName;

                        if (File.Exists(fullFilePath))
                            File.Delete(fullFilePath);

                        using (FileStream fs = new FileStream(fullFilePath, FileMode.OpenOrCreate))
                        {
                            using (StreamWriter sw = new StreamWriter(fs))
                            {
                                


                                sw.WriteLine("using System;");
                                sw.WriteLine("using System.Collections.Generic;");
                                sw.WriteLine("using System.Linq;");
                                sw.WriteLine("using System.Text;");
                                sw.WriteLine("\n");

                                System.Data.Objects.ObjectResult<GenerateClassPropertyC_Result> objstr = MWTEntities.GenerateClassPropertyC(Convert.ToInt32(section.ID));
                                //List<GenerateClassPropertyC_Result> objstr = MWTEntities.GenerateClassPropertyC(Convert.ToInt32(section.ID)).ToList<GenerateClassPropertyC_Result>();

                                foreach (GenerateClassPropertyC_Result stri in objstr)
                                {

                                    //str = objstr.ElementAt(0);
                                    //lblMessage.Text = str;
                                    sw.WriteLine(stri.ProperyScript);
                                }
                                //sw.WriteLine("namespace SectionConfiguration");
                                //sw.WriteLine("{");
                                //sw.WriteLine("public class " + sectionName);
                                //sw.WriteLine("{");
                                //...Writing Propperty.....................
                                //foreach (var field in sectionFields)
                                //{
                                //    sw.WriteLine("\n");
                                //    sw.WriteLine(" public int " + field.FieldName);
                                //    sw.WriteLine("{");
                                //    sw.WriteLine("get");
                                //    sw.WriteLine("{");
                                //    sw.WriteLine("return " + field.FieldStatus + " ;");
                                //    sw.WriteLine("}");
                                //    sw.WriteLine("}");


                                //}
                                //....End property.................
                                //sw.WriteLine("\n");
                                //sw.WriteLine("}");
                                //sw.WriteLine("}");

                            }
                        }
                    //}
            }
            lblMessage.Text = "Section Configuration Class file Geneterated please complie the project 'SectionConfiguration'\n" + "Location is " + ConfigDir;

        }
        catch (Exception E)
        {
            lblMessage.Text =lblMessage.Text + E.Message;
        }

    }
}