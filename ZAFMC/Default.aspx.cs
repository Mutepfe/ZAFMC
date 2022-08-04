using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace ZAFMC
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Clear PageCache
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            
            //Resets Web Controls
            ResetControls(Page);
        }

        protected void CurDateTime_Load(object sender, EventArgs e)
        {
            CurDateTime.Text = DateTime.Now.ToString();
        }

        

        protected void RegUser_CheckedChanged(object sender, EventArgs e)
        {

        }

        // Resets all Controls on the Web Form
        private void ResetControls(System.Web.UI.Control JP)
        {
            foreach (System.Web.UI.Control ctr in JP.Controls)
            {
                //TexBoxes Control
                if (ctr is System.Web.UI.WebControls.TextBox)
                {
                    System.Web.UI.WebControls.TextBox TB = ctr as System.Web.UI.WebControls.TextBox;
                    if (TB != null)
                    {
                        TB.Text = string.Empty;
                    }
                }
                else
                {
                    if (ctr.Controls.Count > 0)
                    {
                        ResetControls(ctr);
                    }
                }

                //DropDownList Control
                if (ctr is System.Web.UI.WebControls.DropDownList)
                {
                    System.Web.UI.WebControls.DropDownList DDL = ctr as System.Web.UI.WebControls.DropDownList;

                    if (DDL != null)
                    {
                        DDL.SelectedValue = Convert.ToString(-1);
                    }
                    else
                        if (ctr.Controls.Count > 0)
                    {
                        ResetControls(ctr);
                    }
                }



            }


        }

        

    }
}