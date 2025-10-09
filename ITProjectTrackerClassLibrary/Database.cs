using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITProjectTrackerClassLibrary
{
    public class Database
    {
        static readonly string ConnectionString = "ITProjectTrackerDB";

        public static void CreateProject(ProjectModel project)
        {
            using (IDbConnection connection = new SqlConnection(Helper.CnnValue(ConnectionString)))
            {
                string queryString = $"INSERT INTO Projects (Status, DepartmentPriority, Site, ProjectROI, Department, Project, ProjectType, ProjectOwner, PrincipalIT, BusinessOwner, TicketNumber, StartDate, ProjectedEndDate, Stakeholders, Scope, ProjectStage, PercentComplete, Notes, ProjectFolderPath)" +
                                                   $"VALUES (@Status, @DepartmentPriority, @Site, @ProjectROI, @Department, @Project, @ProjectType, @ProjectOwner, @PrincipalIT, @BusinessOwner, @TicketNumber, @StartDate, @ProjectedEndDate, @Stakeholders, @Scope, @ProjectStage, @PercentComplete, @Notes, @ProjectFolderPath)";

                connection.Open();  // For some reason this is needed in order to obtain the id of the newly created Project in the database.

                connection.Execute(queryString, project);

                project.ID = int.Parse(connection.ExecuteScalar("SELECT @@IDENTITY").ToString());
            }
        }
        public static List<ProjectModel> GetSampleProjects()
        {
            List<ProjectModel> projects = new List<ProjectModel>();

            projects.Add(new ProjectModel() { ID = 1, Site = "Drive", ProjectROI = 1000000, Department = "IT", Project = "Bedazzler", BusinessOwner = "Me", TicketNumber = 1003, StartDate = new DateTime(2022, 9, 1), ProjectedEndDate = new DateTime(2022, 9, 16), Stakeholders = "IT (Not the clown)", Scope = "Create a new Project Tracker.", ProjectStage = "Development", PercentComplete = .40, Notes = "Make like the spreadsheet version.", ProjectFolderPath = "https://smcltd.sharepoint.com/:f:/r/sites/SMCI.T.SponsoredProjects-TrackerandTemplates/Shared%20Documents/Tracker%20and%20Templates?csf=1&web=1&e=21xdhz" });
            projects.Add(new ProjectModel() { ID = 2, Site = "Amery", ProjectROI = 1000001, Department = "Automation", Project = "Wowzer", BusinessOwner = "Justin Curran", TicketNumber = 1004, StartDate = new DateTime(2022, 8, 22), ProjectedEndDate = new DateTime(2022, 10, 16), Stakeholders = "The Press floor.", Scope = "Create a new Automation Robot.", ProjectStage = "Building", PercentComplete = .60, Notes = "Make like a tree and get outta here.", ProjectFolderPath = "https://smcltd.sharepoint.com/:f:/r/sites/SMCI.T.SponsoredProjects-TrackerandTemplates/Shared%20Documents/Tracker%20and%20Templates?csf=1&web=1&e=21xdhz" });

            return projects;
        }
        public static List<ProjectModel> GetProjects()
        {
            string queryString = "SELECT * FROM Projects ORDER BY ID";

            using (IDbConnection connection = new SqlConnection(Helper.CnnValue(ConnectionString)))
            {
                return connection.Query<ProjectModel>(queryString).ToList();
            }
        }
        public static List<string> GetAdmins()
        {
            string queryString = "SELECT WindowsUserName FROM UserAccess";

            using (IDbConnection connection = new SqlConnection(Helper.CnnValue(ConnectionString)))
            {
                return connection.Query<string>(queryString).ToList();
            }
        }
        public static void UpdateProject(ProjectModel project, string fieldName)
        {
            using (IDbConnection connection = new SqlConnection(Helper.CnnValue(ConnectionString)))
            {
                string queryString = $"UPDATE Projects SET {fieldName} = @{fieldName}, DateModified = GETDATE() WHERE ID = @ID";

                connection.Execute(queryString, project);
            }
        }
        public static void UpdateProject(ProjectModel project, List<string> fields)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in fields)
            {
                sb.Append($" {item} = @{item},");
            }

            using (IDbConnection connection = new SqlConnection(Helper.CnnValue(ConnectionString)))
            {
                string queryString = $"UPDATE Projects SET{sb} DateModified = GETDATE() WHERE ID = @ID";

                connection.Execute(queryString, project);
            }
        }
        public static void DeleteProject(int projectID)
        {
            using (IDbConnection connection = new SqlConnection(Helper.CnnValue(ConnectionString)))
            {
                connection.Execute("DELETE FROM Projects WHERE ID = @ID", new { ID = projectID });
            }
        }

        public static void CreateProjectChange(int projectID, string userLogin, string fieldChanged, string newValue)
        {
            using (IDbConnection connection = new SqlConnection(Helper.CnnValue(ConnectionString)))
            {
                string queryString = "INSERT INTO ProjectChanges (ProjectID, UserLogin, FieldChanged, NewValue)" +
                                                        " VALUES (@ProjectID, @UserLogin, @FieldChanged, @NewValue)";

                connection.Execute(queryString, new { ProjectID = projectID, UserLogin = userLogin, FieldChanged = fieldChanged, NewValue = newValue });
            }
        }
    }
}
