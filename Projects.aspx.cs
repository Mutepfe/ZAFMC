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
    public partial class Projects : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Clear PageCache
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
        }

        protected void CompletedDate_TextChanged(object sender, EventArgs e)
        {

        }
        //Database Connection (Refresh)
        public void ProjectConnection()
        {

            string ProCon = ConfigurationManager.ConnectionStrings["ZionProjects"].ConnectionString; // ZionProjects From Web.Config under ConnectionString Settings
            SqlConnection JM = new SqlConnection(ProCon);
            SqlCommand Rusape = new SqlCommand(@"SELECT [ProjectName] as [Name],(REPLACE(convert(nvarchar,ProjectStartDate,106),'','/')) as [Start Date],[ProjectDuration] as [Duration],(REPLACE(convert(nvarchar,ProjectExpecDate,106),'','/')) as [Expected Date],[ProjectLeader] as [Project Leader],[ProjectLeaderRank] as [Leader Position],[ProjectSite] as [Actual Site],('$'+REPLACE(CONVERT(varchar,convert(MONEY,ProjectEstimCost),1),'.00','.00c')) as [Estimated Cost($)],(REPLACE(convert(nvarchar,ProjectCompDate,106),'','/')) as [Completion Date] FROM [dbo].[Projects] ORDER BY [ProjectName]", JM);
            try
            {
                JM.Open();
                SqlDataReader MHOFU = Rusape.ExecuteReader();
                ProjectsList.DataSource = MHOFU;
                ProjectsList.DataBind();
            }
            catch
            {
                throw new Exception("Failed to connect to the database");
            }
            finally
            {
                Rusape.Dispose(); //Clean up memory
                JM.Close(); //Close DBase  connection
            }

        }
        //Add Record(Event)
        protected void SaveProject_Click(object sender, EventArgs e)
        {
            ProjectAddNew(); // Add or Save Record
            ProjectConnection(); // Refresh Database
            ResetControls(Page); // Resets all Web Controls
        }
        //Reset Controls (Event)
        protected void ResetPage_Click(object sender, EventArgs e)
        {
            ResetControls(Page);
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
        //AddNew Record (Method)
        private void ProjectAddNew()
        {

            string ProjInsert = ConfigurationManager.ConnectionStrings["ZionProjects"].ConnectionString; // ZionProjects From Web.Config under ConnectionString Settings
            SqlConnection PI = new SqlConnection(ProjInsert);
            SqlCommand Jacob = new SqlCommand("AddNewProject", PI);

            try
            {
                Jacob.CommandType = CommandType.StoredProcedure;
                Jacob.Parameters.AddWithValue("@ProjectID", DBNull.Value);
                Jacob.Parameters.AddWithValue("@ProjectName", ProjectName.Text);
                Jacob.Parameters.AddWithValue("@ProjectStartDate", ProjectStartDate.Text);
                Jacob.Parameters.AddWithValue("@ProjectDuration", ProjectDuration.Text);
                Jacob.Parameters.AddWithValue("@ProjectExpecDate", ExpectedDate.Text);
                Jacob.Parameters.AddWithValue("@ProjectLeader", ProjectLeader.Text);
                Jacob.Parameters.AddWithValue("@ProjectLeaderRank", LeaderPosition.Text);
                Jacob.Parameters.AddWithValue("@ProjectSite", ProjectSite.Text);
                Jacob.Parameters.AddWithValue("@ProjectEstimCost", EstimatedCost.Text);
                Jacob.Parameters.AddWithValue("@ProjectCompDate", CompletedDate.Text);
                PI.Open();
                Jacob.ExecuteNonQuery();

            }
            catch (Exception JP)
            {
                TopMostDialogs.ShowTopMost(JP.Message, "ZAFMC-Insert Error", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            finally
            {
                Jacob.Dispose(); //Clean up memory
                PI.Close(); //Close DBase  connection
                ResetControls(Page); // Resets all Web Controls
            }
        }
        protected void ViewProject_Click(object sender, EventArgs e)
        {
            ProjectsView();
        }
        //View Record (Method)
        private void ProjectsView()
        {

            string PV = ConfigurationManager.ConnectionStrings["ZionProjects"].ConnectionString; // ZionProjects From Web.Config under ConnectionString Settings
            SqlConnection ProjView = new SqlConnection(PV);
            SqlCommand TM = new SqlCommand(@"SELECT [ProjectName] as [Name],(REPLACE(convert(nvarchar,ProjectStartDate,106),'','/')) as [Start Date],[ProjectDuration] as [Duration],(REPLACE(convert(nvarchar,ProjectExpecDate,106),'','/')) as [Expected Date],[ProjectLeader] as [Project Leader],[ProjectLeaderRank] as [Leader Position],[ProjectSite] as [Actual Site],('$'+REPLACE(CONVERT(varchar,convert(MONEY,ProjectEstimCost),1),'.00','.00c')) as [Estimated Cost($)],(REPLACE(convert(nvarchar,ProjectCompDate,106),'','/')) as [Completion Date] FROM [dbo].[Projects] WHERE [ProjectName] LIKE @FIND ", ProjView);

            try
            {
                TM.Parameters.AddWithValue("@FIND", ProjectName.Text + "%");
                ProjView.Open();
                SqlDataReader AL = TM.ExecuteReader();
                ProjectsList.DataSource = AL; // Attach searched data to DataGridView
                ProjectsList.DataBind();  // Bind data to DataGridView
            }
            catch (Exception FORD)
            {
                TopMostDialogs.ShowTopMost(FORD.ToString(), "ZAFMC-Viewing Projects", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            finally
            {
                TM.Dispose(); //Clean up memory
                ProjView.Close(); //Close DBase  connection
            }
        }

        //Edit or Update Project(Method)
        private void UpdateEditedProject()
        {
            string M = ConfigurationManager.ConnectionStrings["ZionProjects"].ConnectionString; // ZionProjects From Web.Config under ConnectionString Settings
            SqlConnection ProjUpdate = new SqlConnection(M);
            SqlCommand PC = new SqlCommand("UpdateProject", ProjUpdate);

            try
            {
                PC.CommandType = System.Data.CommandType.StoredProcedure;
                PC.Parameters.AddWithValue("@ProjectID", DBNull.Value);
                PC.Parameters.AddWithValue("@ProjectName", ProjectName.Text);
				PC.Parameters.AddWithValue("@ProjectStartDate", ProjectStartDate.Text);
                PC.Parameters.AddWithValue("@ProjectDuration", ProjectDuration.Text);
                PC.Parameters.AddWithValue("@ProjectExpecDate", ExpectedDate.Text);
                PC.Parameters.AddWithValue("@ProjectLeader",ProjectLeader.Text );
				PC.Parameters.AddWithValue("@ProjectLeaderRank",LeaderPosition.Text );
				PC.Parameters.AddWithValue("@ProjectSite",ProjectSite.Text );
				PC.Parameters.AddWithValue("@ProjectEstimCost",EstimatedCost.Text );
				PC.Parameters.AddWithValue("@ProjectCompDate",CompletedDate.Text );  
                
                ProjUpdate.Open();
                PC.ExecuteNonQuery();

            }
            catch (Exception Mhofu)
            {
                TopMostDialogs.ShowTopMost(Mhofu.Message, "ZAFMC- Update Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                PC.Dispose(); //Clean up memory
                ProjUpdate.Close(); //Close DBase  connection

            }
		}
        //Edit or Update Project (Event)
        protected void EditProject_Click(object sender, EventArgs e)
        {
            DialogResult Proj = TopMostDialogs.ShowTopMost("Do you want to Update this Record..!!", "ZAFMC - Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (Proj == DialogResult.Yes)
            {
                UpdateEditedProject(); // Update the Record
                ProjectConnection();  //Refresh the DBase
                ResetControls(Page); // Resets all Web Controls
            }
            else
               if (Proj == DialogResult.No)
            {
                ProjectConnection();
                ResetControls(Page); // Resets all Web Controls
            }
        }
        protected void RefreshProject_Click(object sender, EventArgs e)
        {
            ProjectConnection();
            ResetControls(Page); // Resets all Web Controls
        }
        //DELETE Project(Event)
        protected void DeleteProject_Click(object sender, EventArgs e)
        {
            ProjectDelete();
            
            ResetControls(Page); // Resets all Web Controls
        }
        //Delete Project(Method)
        private void ProjectDelete()
        {
            //Using SQL Stored Procedure

            string PrDel = ConfigurationManager.ConnectionStrings["ZionProjects"].ConnectionString; // ZionProjects From Web.Config under ConnectionString Settings
            SqlConnection DP = new SqlConnection(PrDel);
            SqlCommand Theo = new SqlCommand("DeleteProject", DP);

            try
            {
                Theo.Parameters.AddWithValue("@DelPROJ", ProjectName.Text); //DelPROJ StoredProcedure actual parameter passed in the DB
                Theo.CommandType = System.Data.CommandType.StoredProcedure;
                DP.Open();
                Theo.ExecuteNonQuery();
                ProjectConnection(); // Refresh the Database

            }
            catch
            {
                throw new Exception("An Error occured, contact IT Department");
            }
            finally
            {
                Theo.Dispose(); //Clean up memory
                DP.Close(); //Close DBase  connection
                ResetControls(Page); // Resets all Web Controls
            }





        }






    }
}