using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace InspectIT
{
    public partial class PF_returnURL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string orderId = Request.Form["m_payment_id"];

            Global DLdb = new Global();
            Response.Redirect("ViewCOCStatement.aspx?msg=" + DLdb.Encrypt("Your purchase was successful. " + orderId));
        }
    }
}