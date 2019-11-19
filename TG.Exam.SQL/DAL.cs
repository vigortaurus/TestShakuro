using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TG.Exam.SQL
{
   public class DAL
   {
      private SqlConnection GetConnection()
      {
         var connectionString = ConfigurationManager.AppSettings["ConnectionString"];

         var con = new SqlConnection(connectionString);

         con.Open();

         return con;
      }

      private DataSet GetData(string sql)
      {
         var ds = new DataSet();

         using (var con = GetConnection())
         {
            using (var cmd = new SqlCommand(sql, con))
            {
               using (var adp = new SqlDataAdapter(cmd))
               {
                  adp.Fill(ds);
               }
            }
         }

         return ds;
      }

      private void Execute(string sql)
      {
         using (var con = GetConnection())
         {
            using (var cmd = new SqlCommand(sql, con))
            {
               cmd.ExecuteNonQuery();
            }
         }
      }

      public DataTable GetAllOrders()
      {
         var sql = "SELECT OrderId, OrderCustomerId, OrderDate FROM Orders";

         var ds = GetData(sql);

         var result = ds.Tables.OfType<DataTable>().FirstOrDefault();

         return result;
      }

      public DataTable GetAllOrdersWithCustomers()
      {
         var sql = $@"SELECT OrderId, OrderCustomerId ,OrderDate FROM [Orders]
            where OrderCustomerId in (Select CustomerId from Customers)";

         var ds = GetData(sql);

         var result = ds.Tables.OfType<DataTable>().FirstOrDefault();

         return result;
      }

      public DataTable GetAllOrdersWithPriceUnder(int price)
      {
         var sql = $@"SELECT ORD.OrderId,ORD.OrderCustomerId, ORD.OrderDate, SUM(OI.[Count]) AS SumAmount,
            SUM(OI.[Count] * IT.ItemPrice) as Total
            FROM Orders AS ORD
            inner join OrdersItems AS OI
            ON ORD.OrderId = OI.OrderId
            inner join Items as IT
            ON OI.ItemId = IT.ItemId
            Group by ORD.OrderId,ORD.OrderCustomerId, ORD.OrderDate
            HAVING SUM(OI.[Count] * IT.ItemPrice) > {price}";

         var ds = GetData(sql);

         var result = ds.Tables.OfType<DataTable>().FirstOrDefault();

         return result;
      }

      public void DeleteCustomer(int orderId)
      {
         var sql = $@"Delete From Customers where CustomerId in
            (Select OrderCustomerId from Orders where OrderId = {orderId})";

         Execute(sql);
      }

      internal DataTable GetAllItemsAndTheirOrdersCountIncludingTheItemsWithoutOrders()
      {
         var sql = $@"SELECT it.ItemId, it.ItemName, it.ItemPrice, Isnull(oit.[Count],0) as [Count]
                        FROM Items as it
                        left join OrdersItems as oit
                        on it.ItemId = oit.ItemId";

         var ds = GetData(sql);

         var result = ds.Tables.OfType<DataTable>().FirstOrDefault();

         return result;
      }
   }
}