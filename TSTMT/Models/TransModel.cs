using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;

namespace TSTMT.Models
{
    public class TransModel
    {
        public int Transaction_id { get; set; }
        public int Item_id { get; set; }
        public int Balance_quantity { get; set; }
        public string Transaction_date { get; set; }
        public string TransType { get; set; }
        public string Item_name { get; set; }
        public int Vendor_id { get; set; }
        public int Quantity { get; set; }
        public int Department_id { get; set; }
        public string Department_name { get; set; }
        public string Vendor_name { get; set; }
        public string ItemQtyTotal { get; set; }


        // department Dropdown Fetch

        public List<TransModel> ddlDepartment()
        {
            List<TransModel> lstDepartment = new List<TransModel>();

            string constring = ConfigurationManager.ConnectionStrings["dbCon"].ToString();
            SqlConnection cn = new SqlConnection(constring);
            cn = new SqlConnection(constring);
            SqlCommand cm = new SqlCommand("ddl_depart",cn);
            cm.CommandType = CommandType.StoredProcedure;
            cn.Open();

            SqlDataReader dr = cm.ExecuteReader();

            while (dr.Read())
            {
                lstDepartment.Add(new TransModel()
                {
                    Department_id = Convert.ToInt32(dr["Department_id"]),
                    Department_name = Convert.ToString(dr["Department_name"])
                });
            }
            return lstDepartment;
        }


        // Vendor Dropdown Fetch
        public List<TransModel> ddlVendor()
        {
            List<TransModel> lstVendor = new List<TransModel>();

            string constring = ConfigurationManager.ConnectionStrings["dbCon"].ToString();
            SqlConnection cn = new SqlConnection(constring);
            cn = new SqlConnection(constring);
            SqlCommand cm = new SqlCommand("ddl_Vendor",cn);
          
            cm.CommandType = CommandType.StoredProcedure;
            cn.Open();

            SqlDataReader dr = cm.ExecuteReader();

            while (dr.Read())
            {
                lstVendor.Add(new TransModel()
                {
                    Vendor_id = Convert.ToInt32(dr["Vendor_id"]),
                    Vendor_name = Convert.ToString(dr["Vendor_name"])
                });
            }
            return lstVendor;
        }


        // Ddl Item Name fetch

        public List<TransModel > ddlItemId()
        {

            List<TransModel> lstItemId = new List<TransModel>();

            string constring = ConfigurationManager.ConnectionStrings["dbCon"].ToString();
            SqlConnection cn = new SqlConnection(constring);
            SqlCommand cm = new SqlCommand("ItemQty_Namef", cn);

            cm.CommandType = CommandType.StoredProcedure;
            cn.Open();

            SqlDataReader dr = cm.ExecuteReader();

            while (dr.Read())
            {
                lstItemId.Add(new TransModel()
                {
                    Item_id = Convert.ToInt32(dr["Item_id"]),
                    Item_name = Convert.ToString(dr["Item_name"])

                });
            }

            return lstItemId;
        }


        // ddl item qty fhetch
        public List<TransModel> ddlItemQty(int Item_id)
        {

            List<TransModel> lstItemQty = new List<TransModel>();

            string constring = ConfigurationManager.ConnectionStrings["dbCon"].ToString();
            SqlConnection cn = new SqlConnection(constring);
            SqlCommand cm = new SqlCommand("ddlItemQty_f", cn);

            cm.CommandType = CommandType.StoredProcedure;
            cm.Parameters.AddWithValue("@Item_id", Item_id);
            cn.Open();

            SqlDataReader dr = cm.ExecuteReader();

            while (dr.Read())
            {

                lstItemQty.Add(new TransModel()
                {
                    Item_id = Convert.ToInt32(dr["Item_id"]),
                    Balance_quantity = Convert.ToInt32(dr["Balance_quantity"])

                });
            }

            return lstItemQty;
        }



        public string SaveTrans(TransModel model)
        {
            string msg = "Data Save Successfully";

            string constring = ConfigurationManager.ConnectionStrings["dbCon"].ToString();
            SqlConnection cn = new SqlConnection(constring);
            SqlCommand cm = new SqlCommand("Trans_Save",cn);
            cm.CommandType = CommandType.StoredProcedure;

            cm.Parameters.AddWithValue("@Transaction_id", model.Transaction_id);
            cm.Parameters.AddWithValue("@Item_id", model.Item_id);
            if(model.Department_id == 0)
                cm.Parameters.AddWithValue("@Department_id", DBNull.Value);
            else
                cm.Parameters.AddWithValue("@Department_id", model.Department_id);
            if (model.Vendor_id == 0)
                cm.Parameters.AddWithValue("@Vendor_id", DBNull.Value);
            else
                cm.Parameters.AddWithValue("@Vendor_id", model.Vendor_id);
            cm.Parameters.AddWithValue("@Quantity", model.Quantity);
            cm.Parameters.AddWithValue("@TransType", model.TransType);
            cm.Parameters.AddWithValue("@ItemQtyTotal", model.ItemQtyTotal);
            cm.Parameters.AddWithValue("@Balance_quantity", model.Balance_quantity);
            cm.Parameters.AddWithValue("@Transaction_date", Convert.ToDateTime(DateTime.Now));

            cn.Open();
            cm.ExecuteNonQuery();
            cn.Close();

            return msg;
        }

        //................................
      
        //...............................

        // list
        public List<TransModel> TransactionList()
        {
            List<TransModel> lstTrans = new List<TransModel>();

            string constring = ConfigurationManager.ConnectionStrings["dbCon"].ToString();
            using (SqlConnection cn = new SqlConnection(constring))
            {
                SqlCommand cm = new SqlCommand("Transaction_List", cn);
                cm.CommandType = CommandType.StoredProcedure;
                //cm.Parameters.AddWithValue("@Search", search);
                cn.Open();


                SqlDataReader dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    TransModel trans = new TransModel()
                    {
                        Transaction_id = Convert.ToInt32(dr["Transaction_id"]),
                        Item_name = Convert.ToString(dr["Item_name"]),
                        Transaction_date = Convert.ToDateTime(dr["Transaction_date"]).ToShortDateString(),
                        Department_name = Convert.ToString(dr["Department_name"]),
                        Vendor_name = Convert.ToString(dr["Vendor_name"]),
                        Balance_quantity = Convert.ToInt32(dr["Balance_quantity"]),
                        Quantity = Convert.ToInt32(dr["Quantity"]),
                        TransType = Convert.ToString(dr["TransType"])
                    };

                    // Check for DBNull for Vendor_id
                    //if (dr["Vendor_id"] != DBNull.Value)
                    //{
                    //    trans.Vendor_id = Convert.ToInt32(dr["Vendor_id"]);
                    //    //trans.Vendor_id = Convert.ToInt32(dr["Vendor_name"]);
                    //}
                    //else
                    //{
                    //    trans.Vendor_id = 0; // Default value, adjust as needed
                    //}

                    //// Check for DBNull for Department_id
                    //if (dr["Department_id"] != DBNull.Value)
                    //{
                    //    //trans.Department_id = Convert.ToInt32(dr["Department_id"]);
                    //    trans.Department_id = Convert.ToInt32(dr["Department_id"]);
                    //}
                    //else
                    //{
                    //    trans.Department_id = 0; // Default value, adjust as needed
                    //}

                    lstTrans.Add(trans);
                }

                //using (SqlDataAdapter da = new SqlDataAdapter(cm))
                //{
                //    DataSet ds = new DataSet();
                //    da.Fill(ds);

                //    foreach (DataRow row in ds.Tables[0].Rows)
                //    {
                //        TransModel trans = new TransModel()
                //        {
                //            Transaction_id = Convert.ToInt32(row["Transaction_id"]),
                //            Item_id = Convert.ToInt32(row["Item_name"]),
                //            Transaction_date = Convert.ToDateTime(row["Transaction_date"]).ToShortDateString(),
                //            Balance_quantity = Convert.ToInt32(row["Balance_quantity"]),
                //            Quantity = Convert.ToInt32(row["Quantity"]),
                //            TransType = Convert.ToString(row["TransType"])
                //        };

                //        if (row["Vendor_name"] != DBNull.Value)
                //        {
                //            trans.Vendor_id = Convert.ToInt32(row["Vendor_name"]);
                //        }
                //        else
                //        {
                //            trans.Vendor_id = 0; // Default value, adjust as needed
                //        }

                //        if (row["Department_name"] != DBNull.Value)
                //        {
                //            trans.Department_id = Convert.ToInt32(row["Department_name"]);
                //        }
                //        else
                //        {
                //            trans.Department_id = 0; // Default value, adjust as needed
                //        }

                //        lstTrans.Add(trans);
                //    }
                //}
            }

            return lstTrans;
        }
        // Delete
        public string DeleteTransaction(int Transaction_id)
        {
            string constring = ConfigurationManager.ConnectionStrings["dbCon"].ToString();
            SqlConnection cn = new SqlConnection(constring);
            //cn = new SqlConnection(constring);
            SqlCommand cm =  new SqlCommand("Delete_Transaction", cn);
            cm.CommandType = CommandType.StoredProcedure;
            cm.Parameters.AddWithValue("@Transaction_id", Transaction_id);
            cn.Open();
            string msg = "Delete Successfully";
            cm.ExecuteNonQuery();
            return msg;
        }

    }
}