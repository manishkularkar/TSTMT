using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace TSTMT.Models
{
    public class VendorModel
    {
        public int Vendor_id { get; set; }
        public string Vendor_name { get; set; }

        public string SaveVendor(VendorModel model)
        {
            string msg = "Save data successfully";
            string constring = ConfigurationManager.ConnectionStrings["dbCon"].ToString();
            SqlConnection cn = new SqlConnection(constring);
            SqlCommand cm = new SqlCommand("Vendor_Save",cn);
            cm.CommandType = CommandType.StoredProcedure;

            cm.Parameters.AddWithValue("@Vendor_id",model.Vendor_id);
            cm.Parameters.AddWithValue("@Vendor_name",model.Vendor_name);

            cn.Open();
            cm.ExecuteNonQuery();
            cn.Close();
            return msg;
        }

        // list
        public List<VendorModel> VendorList()
        {
            List<VendorModel> lstDemo = new List<VendorModel>();

            string constring = ConfigurationManager.ConnectionStrings["dbCon"].ToString();
            SqlConnection cn = new SqlConnection(constring);
            SqlCommand cm = new SqlCommand("Vendor_List",cn);
            cm.CommandType= CommandType.StoredProcedure;
            
            cn.Open();

            SqlDataReader dr = cm.ExecuteReader();

            while (dr.Read())
            {
                lstDemo.Add(new VendorModel()
                {

                    Vendor_id = Convert.ToInt32(dr["Vendor_id"]),
                    Vendor_name = Convert.ToString(dr["Vendor_name"])
                });
            }
            return lstDemo;
        }

        // Delete
        public string DeleteVendor(int Vendor_id)
        {
            string constring = ConfigurationManager.ConnectionStrings["dbCon"].ToString();
            SqlConnection cn = new SqlConnection(constring);
            SqlCommand cm = new SqlCommand("Vendor_Delete", cn);

            cm.CommandType = CommandType.StoredProcedure;
            cm.Parameters.AddWithValue("@Vendor_id", Vendor_id);
            cn.Open();
            string msg = "Delete Successfully";
            cm.ExecuteNonQuery();
            return msg;
        }

        // Details
        public VendorModel DetailVendor(int Vendor_id)
        {
            string constring = ConfigurationManager.ConnectionStrings["dbCon"].ToString();
            SqlConnection cn = new SqlConnection(constring);
            SqlCommand cm = new SqlCommand("Vendor_Details", cn);
            cm.CommandType = CommandType.StoredProcedure;
            cm.Parameters.AddWithValue("@Vendor_id", Vendor_id);
            cn.Open();

            SqlDataReader dr = cm.ExecuteReader();
            VendorModel model = new VendorModel();

            while (dr.Read())
            {
                model.Vendor_id = Convert.ToInt32(dr["Vendor_id"]);
                model.Vendor_name = Convert.ToString(dr["Vendor_name"]);
            }
            return model;
        }

        // Edit
        public VendorModel EditVendor(int Vendor_id)
        {
            string constring = ConfigurationManager.ConnectionStrings["dbCon"].ToString();
            SqlConnection cn = new SqlConnection(constring);
            SqlCommand cm =new SqlCommand("Vendor_Edit",cn);
            cm.CommandType = CommandType.StoredProcedure;
            cm.Parameters.AddWithValue("@Vendor_id", Vendor_id);
            cn.Open();

            SqlDataReader dr = cm.ExecuteReader();
            VendorModel model = new VendorModel();
            while (dr.Read())
            {
                model.Vendor_id = Convert.ToInt32(dr["Vendor_id"]);
                model.Vendor_name = Convert.ToString(dr["Vendor_name"]);
            }
            return model;
        }
    }
}