using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

namespace SwitchData
{
    public partial class FrmUpload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string FilePath = string.Concat(Server.MapPath("~/UploadFile/" + FileUpload1.FileName));
            if (FileUpload1.FileName.Contains("SwitchData"))
            {

                FileUpload1.SaveAs(FilePath);
                GLSpliter objMRGLdt = new GLSpliter();
                DAConnection objImportDt = new DAConnection();
                DataTable dtMSGL = new DataTable();
                string extension = Path.GetExtension(FilePath);
                dtMSGL = objMRGLdt.SwitchData(FilePath, extension);
                string ImportStatus = string.Empty;
                if (dtMSGL.Rows.Count > 0)
                {
                    ImportStatus = objImportDt.ImportSwitchData(dtMSGL);
                }
            }
        }
            public string FilePath { get; set; }
        }
    }

