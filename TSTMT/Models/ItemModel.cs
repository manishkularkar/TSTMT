using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace TSTMT.Models
{
    public class ItemModel
    {
        public int Item_id { get; set; }
        public string Item_name { get; set; }
        public string Category { get; set; }
        public decimal Rate { get; set; }
        public int Balance_quantity { get; set; }

        public int SerialNo { get; set; }


        // Category
        public int id { get; set; }
        public string Category_Name { get; set; }

        public string SaveItem(ItemModel model)
        {
            string msg = "Data Save successfully";
            string constring = ConfigurationManager.ConnectionStrings["dbCon"].ToString();
            SqlConnection cn = new SqlConnection(constring);
            SqlCommand cmd = new SqlCommand("SP_SaveItem", cn);
            cmd.CommandType =CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Item_id", model.Item_id);
            cmd.Parameters.AddWithValue("@Item_name", model.Item_name);
            cmd.Parameters.AddWithValue("@Category", model.Category);
            cmd.Parameters.AddWithValue("@Rate", model.Rate);
            cmd.Parameters.AddWithValue("@Balance_quantity", model.Balance_quantity);


            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();

            return msg;
        }
        public ItemModel EditItem(int Item_id)
        {
            string constring = ConfigurationManager.ConnectionStrings["dbCon"].ToString();
            SqlConnection cn = new SqlConnection();
            cn = new SqlConnection(constring);
            SqlCommand cm = new SqlCommand("SP_EditItem", cn);
            cm.CommandType = CommandType.StoredProcedure;

            cm.Parameters.AddWithValue("@Item_id", Item_id);
            cn.Open();

            SqlDataReader dr = cm.ExecuteReader();
            ItemModel model = new ItemModel();
            while (dr.Read())
            {
                model.Item_id = Convert.ToInt32(dr["Item_id"]);
                model.Item_name = Convert.ToString(dr["Item_name"]);
                model.Category = Convert.ToString(dr["Category"]);
                model.Rate = Convert.ToDecimal(dr["Rate"]);
                model.Balance_quantity = Convert.ToInt32(dr["Balance_quantity"]);

            }
            return model;
        }

        public List<ItemModel> ItemList()
        {
            List<ItemModel> lstItem = new List<ItemModel>();

            string constring = ConfigurationManager.ConnectionStrings["dbCon"].ToString();
            SqlConnection cn = new SqlConnection(constring);
            SqlCommand cm = new SqlCommand("SP_Item_List", cn);
            cm.CommandType = CommandType.StoredProcedure;

            //cm.Parameters.AddWithValue("@Search", search);
            cn.Open();

            SqlDataReader dr = cm.ExecuteReader();

            while (dr.Read())
            {
                lstItem.Add(new ItemModel()
                {
                    Item_id = Convert.ToInt32(dr["Item_id"]),
                    Item_name = Convert.ToString(dr["Item_name"]),
                    Category = Convert.ToString(dr["Category"]),
                    Rate = Convert.ToDecimal(dr["Rate"]),
                    Balance_quantity = Convert.ToInt32(dr["Balance_quantity"])
                });
            }


            return lstItem;
        }

        // Delete

        public string DeleteItem(int Item_id)
        {
            string constring = ConfigurationManager.ConnectionStrings["dbCon"].ToString();
           
                SqlConnection cn = new SqlConnection(constring);
                SqlCommand cm = new SqlCommand("SP_DeleteItem", cn);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@Item_id", Item_id);
                cn.Open();
                string msg = "Delete Successfully";
                cm.ExecuteNonQuery();
                cn.Close();
           
            return msg;
        }


        // Details
        public ItemModel DetailItem(int Item_id)
        {
            string constring = ConfigurationManager.ConnectionStrings["dbCon"].ToString();
             SqlConnection cn = new SqlConnection();
            cn = new SqlConnection(constring);
            SqlCommand cm =new SqlCommand("Item_Details", cn);
            cm.CommandType = CommandType.StoredProcedure;

            cm.Parameters.AddWithValue("@Item_id", Item_id);

            cn.Open();
            SqlDataReader dr = cm.ExecuteReader();
            ItemModel model = new ItemModel();

            while (dr.Read())
            {
                model.Item_id = Convert.ToInt32(dr["Item_id"]);
                model.Item_name = Convert.ToString(dr["Item_name"]);
                model.Category = Convert.ToString(dr["Category"]);
                model.Rate = Convert.ToInt32(dr["Rate"]);
                model.Balance_quantity = Convert.ToInt32(dr["Balance_quantity"]);
            }
            return model;
        }

        // Category DropDown Fetch

        public List<ItemModel> ddlCategoryList()
        {
            List<ItemModel> lstCategory = new List<ItemModel>();

            string constring = ConfigurationManager.ConnectionStrings["dbCon"].ToString();
            SqlConnection cn = new SqlConnection();
            cn = new SqlConnection(constring);
            SqlCommand cm = cn.CreateCommand();
            cm.CommandText = "Ddl_Category";

            cm.CommandType = CommandType.StoredProcedure;
            cn.Open();

            SqlDataReader dr = cm.ExecuteReader();

            while (dr.Read())
            {
                lstCategory.Add(new ItemModel()
                {
                    id = Convert.ToInt32(dr["id"]),
                    Category_Name = Convert.ToString(dr["Category_Name"])
                });
            }

            return lstCategory;
        }

    }
}






