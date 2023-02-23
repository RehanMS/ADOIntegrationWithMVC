using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System;
using System.Data;

namespace ADOIntegrationWithMVC.Models
{
    public class RehanDBDataContext
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlCon"].ConnectionString);
        public bool CheckLogin(Login model)
        {
            string query = "select count(*) from Login where Username=@username and Password=@password";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@username", model.Username);
            cmd.Parameters.AddWithValue("@password", model.Password);
            con.Open();
            int value = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            if (value == 0)
                return false;
            else
                return true;
        }

        public List<Employee> GetEmployees()
        {
            List<Employee> emps = new List<Employee>();
            SqlCommand cmd = new SqlCommand("select * from Employee", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Employee emp = new Employee();
                    emp.EmpId = Convert.ToInt32(dr["EmpId"]);
                    emp.EmpName = Convert.ToString(dr["EmpName"]);
                    emp.Job = Convert.ToString(dr["Job"]);
                    emp.Salary = Convert.ToDouble(dr["Salary"]);
                    emp.Dname = Convert.ToString(dr["Dname"]);
                    emps.Add(emp);
                }
            }
            return emps;
        }
        public Employee Details(int id)
        {
            Employee emp = new Employee();
            SqlCommand cmd = new SqlCommand("select * from Employee where EmpId=" + id, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    emp.EmpId = Convert.ToInt32(dr["EmpId"]);
                    emp.EmpName = Convert.ToString(dr["EmpName"]);
                    emp.Job = Convert.ToString(dr["Job"]);
                    emp.Salary = Convert.ToDouble(dr["Salary"]);
                    emp.Dname = Convert.ToString(dr["Dname"]);
                }
                return emp;
            }
            return emp;
        }
        public bool AddEmployee(Employee emp)
        {
           // SqlCommand cmd = new SqlCommand("insert into Employee values(@EmpId,@EmpName,@Salary,@Job,@Dname)",con);
            SqlCommand cmd = new SqlCommand("insertRec", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmpId", emp.EmpId);
            cmd.Parameters.AddWithValue("@EmpName", emp.EmpName);
            cmd.Parameters.AddWithValue("@Salary", emp.Salary);
            cmd.Parameters.AddWithValue("@Job", emp.Job);                 
            cmd.Parameters.AddWithValue("@Dname", emp.Dname);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i != 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool UpdateEmployee(Employee emp)
        {
            // SqlCommand cmd = new SqlCommand("insert into Employee values(@EmpId,@EmpName,@Salary,@Job,@Dname)",con);
            SqlCommand cmd = new SqlCommand("UpdateRn", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmpId", emp.EmpId);
            cmd.Parameters.AddWithValue("@EmpName", emp.EmpName);
            cmd.Parameters.AddWithValue("@Salary", emp.Salary);
            cmd.Parameters.AddWithValue("@Job", emp.Job);
            cmd.Parameters.AddWithValue("@Dname", emp.Dname);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i != 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool DeleteEmployee(int id)
        {
            // SqlCommand cmd = new SqlCommand("insert into Employee values(@EmpId,@EmpName,@Salary,@Job,@Dname)",con);
            SqlCommand cmd = new SqlCommand("EmpDelete", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmpId", id);          
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i != 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
       
    }
}