using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tourism_Management
{



  
        public partial class UploadPage : System.Web.UI.Page
        {
            protected void btnUpload_Click(object sender, EventArgs e)
            {
                if (FileUpload1.HasFile)
                {
                    try
                    {
                        // Folder path inside the project
                        string folderPath = Server.MapPath("~/Uploads/");
                        if (!Directory.Exists(folderPath))
                            Directory.CreateDirectory(folderPath);

                        // Save the file
                        string fileName = Path.GetFileName(FileUpload1.FileName);
                        string savePath = Path.Combine(folderPath, fileName);
                        FileUpload1.SaveAs(savePath);

                        lblMessage.Text = $"✅ File '{fileName}' uploaded successfully!";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = "❌ Error: " + ex.Message;
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    lblMessage.Text = "⚠️ Please select a file.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }


