using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InspectIT
{
    public partial class logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["IIT_UID"] = null;
            Session["IIT_UName"] = null;
            Session["IIT_EmailAddress"] = null;
            Session["IIT_Role"] = null;

            Response.Redirect("Default.aspx");
        }
    }
}