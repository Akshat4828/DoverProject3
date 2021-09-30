using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Phase3.Models
{
    public class CustomerBO
    {
        
        SqlConnection cn = null;
        public SqlConnection ConnectToSqlServer()
        {
            string cs = string.Empty;
            cs = "server=H5CG1220K2P;integrated security=true;database=Phase_3DB";
            cn = new SqlConnection(cs);
            cn.Open();
            return cn;
        }
      
        public List<CustomerModel> GetAllCustomers()
        {
            cn = ConnectToSqlServer();
            List<CustomerModel> customers = new List<CustomerModel>();
            string query = "exec spGetAllCustomers";
            SqlCommand cmd = new SqlCommand(query, cn);
            SqlDataReader dr = cmd.ExecuteReader();
            
            while(dr.Read())
            {
                CustomerModel c = new CustomerModel();
                c.Id = Convert.ToInt32(dr[0]);
                c.CName = dr[1].ToString();
                c.Email = dr[2].ToString();
                c.City = dr[3].ToString();
                c.Mobile_no = dr[4].ToString();
                customers.Add(c);
            }
            return customers;
        }
        public CustomerModel GetAllCustomersById(int id)
        {
            cn = ConnectToSqlServer();
            CustomerModel c = new CustomerModel();
            string query = "exec spGetCustomerById  @id";
            SqlCommand cmd = new SqlCommand(query, cn);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader dr = cmd.ExecuteReader();
            
            if (dr.Read())
            {
                
                c.Id = Convert.ToInt32(dr[0]);
                c.CName = dr[1].ToString();
                c.Email = dr[2].ToString();
                c.City = dr[3].ToString();
                c.Mobile_no = dr[4].ToString();
                
            }
            return c;
        }

        public int AddCustomer(CustomerModel c)
        {
            cn = ConnectToSqlServer();
            string query = "exec speAddCustomer @cname,@email,@city,@mobile_no,@password ";
            SqlCommand cmd = new SqlCommand(query, cn);
            
            cmd.Parameters.AddWithValue("@cname", c.CName);
            cmd.Parameters.AddWithValue("@email", c.Email);
            cmd.Parameters.AddWithValue("@city", c.City);
            cmd.Parameters.AddWithValue("@mobile_no", c.Mobile_no);
            cmd.Parameters.AddWithValue("@password", c.Password);

             return cmd.ExecuteNonQuery();
            
        }
        public int EditCustomer(CustomerModel c)
        {
            cn = ConnectToSqlServer();
            string query = "exec spEditCustomer @id,@cname,@email,@city,@mobile_no  ";
            SqlCommand cmd = new SqlCommand(query, cn);
            cmd.Parameters.AddWithValue("@id", c.Id);
            cmd.Parameters.AddWithValue("@cname", c.CName);
            cmd.Parameters.AddWithValue("@email", c.Email);
            cmd.Parameters.AddWithValue("@city", c.City);
            cmd.Parameters.AddWithValue("@mobile_no", c.Mobile_no);
           
            return cmd.ExecuteNonQuery();

        }
        public int DeleteCustomer(CustomerModel c)
        {
            cn = ConnectToSqlServer();
            string query = "exec spDeleteCustomer  @id ";
            SqlCommand cmd = new SqlCommand(query, cn);
            cmd.Parameters.AddWithValue("@id", c.Id);
            return cmd.ExecuteNonQuery();

        }
    }
}
