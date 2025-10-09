using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;



namespace ITProjectTrackerClassLibrary
{
    public class ProjectModel : BindableBase
    {
        public int ID { get; set; }
        public string Status { get; set; }
        public string DepartmentPriority { get; set; }
        public string Site { get; set; }
        public double ProjectROI { get; set; }
        public string Department { get; set; }
        //private string _project;

        //public string Project
        //{
        //    get { return _project; }
        //    set { _project = value; OnPropertyChanged(); }
        //}
        public string Project { get; set; }
        public bool IsProjectHyperlinkEnabled
        {
            get
            {
                if (ProjectFolderPath.Length > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public string ProjectType { get; set; }
        public string ProjectOwner { get; set; }
        public string PrincipalIT { get; set; }
        public string BusinessOwner { get; set; }
        public int TicketNumber { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? ProjectedEndDate { get; set; }
        public string Stakeholders { get; set; }
        public string Scope { get; set; }
        public string ProjectStage { get; set; }
        public double PercentComplete { get; set; }
        public string Notes { get; set; } = "";
        public string ProjectFolderPath { get; set; } = "";
        public string ProjectROIFilePath { get; set; } = "";
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
