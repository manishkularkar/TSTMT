using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace TSTMT.Models
{
    public class DepartModel
    {
        public int Department_id { get; set; }
        public string Department_name { get; set; }


        // Save
        public string SaveDepart(DepartModel model)
        {
            string msg = "Data Save Successfully";

            string constring = ConfigurationManager.ConnectionStrings["dbCon"].ToString();
            SqlConnection cn = new SqlConnection(constring);
            SqlCommand cmd = new SqlCommand("Department_Save", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Department_id", model.Department_id);
            cmd.Parameters.AddWithValue("@Department_name", model.Department_name);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
            return msg;
            //if (i >= 1)
            //    return true;
            //else
            //    return false;
        }

        // list
        public List<DepartModel> DepartmentList()
        {
            List<DepartModel> lstDemo = new List<DepartModel>();

            string constring = ConfigurationManager.ConnectionStrings["dbCon"].ToString();
            SqlConnection cn = new SqlConnection(constring);
            SqlCommand cm = new SqlCommand("Department_Listt", cn);
            cm.CommandType = CommandType.StoredProcedure;
            //cm.Parameters.AddWithValue("@Search", search);
            cn.Open();

            SqlDataReader dr = cm.ExecuteReader();

            while (dr.Read())
            {
                lstDemo.Add(new DepartModel()
                {

                    Department_id = Convert.ToInt32(dr["Department_id"]),
                    Department_name = Convert.ToString(dr["Department_name"])
                });
            }
            return lstDemo;
        }

        // Delete

        public string DeleteDepartment(int Department_id)
        {
            string constring = ConfigurationManager.ConnectionStrings["dbCon"].ToString();
            SqlConnection cn = new SqlConnection(constring);
            SqlCommand cm = new SqlCommand("Department_Delete",cn);
            cm.CommandType = CommandType.StoredProcedure;

            cm.Parameters.AddWithValue("@Department_id", Department_id);
            cn.Open();
            string msg = "Delete Successfully";
            cm.ExecuteNonQuery();

            return msg;
        }

        // Edit
        public DepartModel EditDepartment(int Department_id)
        {
            string constring = ConfigurationManager.ConnectionStrings["dbCon"].ToString();
            SqlConnection cn = new SqlConnection();
            cn = new SqlConnection(constring);
            SqlCommand cm = new SqlCommand("Department_Edit",cn);
            cm.CommandType = CommandType.StoredProcedure;

            cm.Parameters.AddWithValue("@Department_id", Department_id);
            cn.Open();

            SqlDataReader dr = cm.ExecuteReader();
            DepartModel model = new DepartModel();
            while (dr.Read())
            {
                model.Department_id = Convert.ToInt32(dr["Department_id"]);
                model.Department_name = Convert.ToString(dr["Department_name"]);
            }
            return model;
        }

        // Details
        public DepartModel DetailDepartment(int Department_id)
        {
            string constring = ConfigurationManager.ConnectionStrings["dbCon"].ToString();
            SqlConnection cn = new SqlConnection(constring);
            SqlCommand cm =new SqlCommand("Department_Detail", cn);
            cm.CommandType = CommandType.StoredProcedure;

            cm.Parameters.AddWithValue("@Department_id", Department_id);
            cn.Open();

            SqlDataReader dr = cm.ExecuteReader();
            DepartModel model = new DepartModel();
            while (dr.Read())
            {
                model.Department_id = Convert.ToInt32(dr["Department_id"]);
                model.Department_name = Convert.ToString(dr["Department_name"]);
            }
            return model;
        }
    }
}





