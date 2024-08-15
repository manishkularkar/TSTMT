using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TSTMT.Models
{
    
        public class ReportsModel
        {
            public int Transaction_id { get; set; }//
            public int Item_Transaction { get; set; }//
            public string Item_name { get; set; }//
                                                 //public string Category { get; set; }
            public DateTime Transaction_date { get; set; }//
            public string TransType { get; set; }
            public string Department_name { get; set; }//
            public string Vendor_name { get; set; }//
            public int Quantity { get; set; }//
            public int Balance_quantity { get; set; }//


            //select A.Transaction_id, A.Transaction_date, A.Quantity, D.Balance_quantity, A.TransType, B.Department_name, C.Vendor_name, D.Item_name from Item_Transaction A
            //left join Department_mast B on A.Department_id= B.Department_id
            //left join Vendor_mast C on A.Vendor_id= C.Vendor_id
            //left join Item_master D on A.Item_id= D.Item_id

            public List<ReportsModel> GetAllData()
            {
                List<ReportsModel> combine = new List<ReportsModel>();
                string constring = ConfigurationManager.ConnectionStrings["dbCon"].ToString();
                SqlConnection cn = new SqlConnection(constring);
                SqlCommand cm = new SqlCommand("Sp_Report", cn);
                cn.Open();

                SqlDataReader dr = cm.ExecuteReader();
                {
                    while (dr.Read())
                    {
                        ReportsModel data = new ReportsModel
                        {
                            Transaction_id = Convert.ToInt32(dr["Transaction_id"]),
                            Item_name = dr["Item_name"].ToString(),
                            Transaction_date = Convert.ToDateTime(dr["Transaction_date"]),
                            Department_name = dr["Department_name"]?.ToString(),
                            Vendor_name = dr["Vendor_name"]?.ToString(),
                            Quantity = Convert.ToInt32(dr["Quantity"]),
                            //Rate = Convert.ToDecimal(dr["Rate"]),
                            Balance_quantity = Convert.ToInt32(dr["Balance_quantity"])
                        };
                        combine.Add(data);

                    }
                }
                return combine; // Return the list of combined data
            }
        }
        
}