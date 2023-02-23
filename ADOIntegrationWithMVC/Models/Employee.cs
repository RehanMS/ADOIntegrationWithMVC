using System.ComponentModel.DataAnnotations;

namespace ADOIntegrationWithMVC.Models
{
    public class Employee
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public string Job { get; set; }
        public double Salary { get; set; }
        public string Dname { get; set; }
    }
}