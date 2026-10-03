using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.Entity;
using System.Data.Sql;
using System.Configuration; //For DBase connection in the Web.Config
using System.Windows.Forms;
using ZAFMC.Models;
using System.Data;

namespace ZAFMC
{
    public partial class EventsCentre : System.Web.UI.Page
    {
        //Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            //Clear PageCache
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            //Resets Controls
            ResetControls(Page);
        }

        //Delete (Event)
        protected void DeleteEvents_Click(object sender, EventArgs e)
        {
            //Using SQL Stored Procedure

            EventsDelete();
            ResetControls(Page);//resets Controls

        }

        //Delete (Method)
        private void EventsDelete()
        {
            //Using SQL Stored Procedure

            string EvtDel = ConfigurationManager.ConnectionStrings["ZionEvents"].ConnectionString; // ZionEvents From Web.Config under ConnectionString Settings
            SqlConnection EvCon = new SqlConnection(EvtDel);
            SqlCommand RUDO = new SqlCommand("DeleteEvent", EvCon);

            try
            {
                RUDO.Parameters.AddWithValue("@DelEVT", EventName.Text); //@DelEVT StoredProcedure actual parameter passed in the DB
                RUDO.CommandType = System.Data.CommandType.StoredProcedure;
                EvCon.Open();
                RUDO.ExecuteNonQuery();
                EventsConnection(); // Refresh the Database

            }
            catch
            {
                throw new Exception("An Error occured, contact IT Department");
            }
            finally
            {
                RUDO.Dispose(); //Clean up memory
                EvCon.Close(); //Close DBase  connection
            }







        }

        //View Record(Event)
        protected void ViewEvents_Click(object sender, EventArgs e)
        {
            EventsView();


        }
        //View Record (Method)
        private void EventsView()
        {

            string EV = ConfigurationManager.ConnectionStrings["ZionEvents"].ConnectionString; // ZionEvents From Web.Config under ConnectionString Settings
            SqlConnection EvtView = new SqlConnection(EV);
            SqlCommand PM = new SqlCommand(@"SELECT [EventName] as [Event],(REPLACE(convert(nvarchar,[EventDate],106),'','/')) as [Date],[EventVenue] as Venue,[EventPerson] as [Contact Person],[EventPersonCell] as [Mobile Number],[EventPersonEmail] as [Email Address],[EventDuration] as Duration,[EventStatus] as [Status] FROM [dbo].[Events] WHERE [EventName] LIKE @FIND ", EvtView);

            try
            {
                PM.Parameters.AddWithValue("@FIND", EventName.Text + "%");
                EvtView.Open();
                SqlDataReader Jac = PM.ExecuteReader();
                EventsList.DataSource = Jac; // Attach searched data to DataGridView
                EventsList.DataBind();  // Bind data to DataGridView
            }
            catch (Exception FORD)
            {
                TopMostDialogs.ShowTopMost(FORD.ToString(), "ZAFMC-Viewing Events", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            finally
            {
                PM.Dispose(); //Clean up memory
                EvtView.Close(); //Close DBase  connection
            }
        }
        //AddNew Record (Method)
        protected void SaveEvents_Click(object sender, EventArgs e)
        {
            EventsAddNew(); // Add or Save Record
            EventsConnection(); // Refresh Database
            ResetControls(Page); // Resets all Web Controls


        }
        //AddNew Record (Method)
        private void EventsAddNew()
        {

            string EventsInsert = ConfigurationManager.ConnectionStrings["ZionEvents"].ConnectionString; // ZionEvents From Web.Config under ConnectionString Settings
            SqlConnection EI = new SqlConnection(EventsInsert);
            SqlCommand JM = new SqlCommand("AddNewEvent", EI);

            try
            {
                JM.CommandType = CommandType.StoredProcedure;
                JM.Parameters.AddWithValue("@EventID", DBNull.Value);
                JM.Parameters.AddWithValue("@EventName", EventName.Text);
                JM.Parameters.AddWithValue("@EventDate", EventDate.Text);
                JM.Parameters.AddWithValue("@EventVenue", EventVenue.Text);
                JM.Parameters.AddWithValue("@EventPerson", EventPerson.Text);
                JM.Parameters.AddWithValue("@EventPersonCell", EventCell.Text);
                JM.Parameters.AddWithValue("@EventPersonEmail", EventEmail.Text);
                JM.Parameters.AddWithValue("@EventDuration", EventDuration.Text);
                JM.Parameters.AddWithValue("@EventStatus", EventStatus.Text);

                EI.Open();
                JM.ExecuteNonQuery();

            }
            catch (Exception JP)
            {
                TopMostDialogs.ShowTopMost(JP.Message, "ZAFMC-Insert Error", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            finally
            {
                JM.Dispose(); //Clean up memory
                EI.Close(); //Close DBase  connection

            }
        }

        //Refresh the DBase(Method)
        public void EventsConnection()
        {

            string EvtCon = ConfigurationManager.ConnectionStrings["ZionEvents"].ConnectionString; // ZionEvents From Web.Config under ConnectionString Settings
            SqlConnection JPM = new SqlConnection(EvtCon);
            SqlCommand Rusape = new SqlCommand(@"SELECT [EventName] as [Event],(REPLACE(convert(nvarchar,[EventDate],106),'','/')) as [Date],[EventVenue] as Venue,[EventPerson] as [Contact Person],[EventPersonCell] as [Mobile Number],[EventPersonEmail] as [Email Address],[EventDuration] as Duration,[EventStatus] as [Status] FROM [dbo].[Events] ORDER BY [EventName]", JPM);
            try
            {
                JPM.Open();
                SqlDataReader MHOFU = Rusape.ExecuteReader();
                EventsList.DataSource = MHOFU;
                EventsList.DataBind();
            }
            catch
            {
                throw new Exception("Failed to connect to the database");
            }
            finally
            {
                Rusape.Dispose(); //Clean up memory
                JPM.Close(); //Close DBase  connection
            }

        }

        //Update Record (Event) 
        protected void EditEvents_Click(object sender, EventArgs e)
        {

            DialogResult EVT = TopMostDialogs.ShowTopMost("Do you want to Update this Record..!!", "ZAFMC - Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (EVT == DialogResult.Yes)
            {
                UpdateEditedEvents(); // Update the Record
                EventsConnection();  //Refresh the DBase
            }
            else
               if (EVT == DialogResult.No)
            {
                EventsConnection();
                ResetControls(Page);//Resets Controls
            }

        }

        //Update (Method) 
        private void UpdateEditedEvents()
        {
            string MJ = ConfigurationManager.ConnectionStrings["ZionEvents"].ConnectionString; // ZionEvents From Web.Config under ConnectionString Settings
            SqlConnection EvtUpdate = new SqlConnection(MJ);
            SqlCommand Jethro = new SqlCommand("UpdateEvents", EvtUpdate);

            try
            {
                Jethro.CommandType = System.Data.CommandType.StoredProcedure;
                Jethro.Parameters.AddWithValue("@EventID", DBNull.Value);
                Jethro.Parameters.AddWithValue("@EventName", EventName.Text);
                Jethro.Parameters.AddWithValue("@EventDate", EventDate.Text);
                Jethro.Parameters.AddWithValue("@EventVenue", EventVenue.Text);
                Jethro.Parameters.AddWithValue("@EventPerson", EventPerson.Text);
                Jethro.Parameters.AddWithValue("@EventPersonCell", EventCell.Text);
                Jethro.Parameters.AddWithValue("@EventPersonEmail", EventEmail.Text);
                Jethro.Parameters.AddWithValue("@EventDuration", EventDuration.Text);
                Jethro.Parameters.AddWithValue("@EventStatus", EventStatus.Text);

                EvtUpdate.Open();
                Jethro.ExecuteNonQuery();

            }
            catch (Exception Mhofu)
            {
                TopMostDialogs.ShowTopMost(Mhofu.Message, "ZAFMC- Update Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                Jethro.Dispose(); //Clean up memory
                EvtUpdate.Close(); //Close DBase  connection

            }
        }

        protected void RefreshEvents_Click(object sender, EventArgs e)
        {
            EventsConnection();
            ResetControls(Page);// Resets Controls
        }


        //Reset Controls (Event)
        protected void ResetPage_Click(object sender, EventArgs e)
        {
            ResetControls(Page);
        }
        // Resets all Controls on the Web Form (Method)
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