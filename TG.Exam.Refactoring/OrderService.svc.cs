using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using log4net;
using log4net.Config;

namespace TG.Exam.Refactoring
{
   public class OrderService : IOrderService
   {
      private static readonly ILog logger = LogManager.GetLogger(typeof(OrderService));

      readonly string connectionString =
         ConfigurationManager.ConnectionStrings["OrdersDBConnectionString"].ConnectionString;

      //1. There is a access level issue, cache should be private
      public IDictionary<string, Order> cache = new Dictionary<string, Order>();
      private readonly ConcurrentDictionary<string, Order> _cache = new ConcurrentDictionary<string, Order>();

      public OrderService()
      {
         BasicConfigurator.Configure();
      }

      public Order LoadOrderLegacy(string orderId)
      {
         try
         {
            Debug.Assert(null != orderId && orderId != "");
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            //2. There is synchronization issue. It's better to use double check pattern if (cache.ContainsKey(orderId)) => lock =>  check again if (cache.ContainsKey(orderId))
            // But ConcurrentDictionary is even more preferable 
            lock (cache) //lock on dictionary doesn't prevent all the operations
            {
               if (cache.ContainsKey(orderId))
               {
                  stopWatch.Stop();
                  logger.InfoFormat("Elapsed - {0}", stopWatch.Elapsed);
                  return cache[orderId];
               }
            }

            //code instead of previous block with lock
            //            if (cache2.ContainsKey(orderId))
            //            {
            //               stopWatch.Stop();
            //               logger.InfoFormat("Elapsed - {0}", stopWatch.Elapsed);               
            //               return cache2.TryGetValue(orderId, out var orderOut) ? orderOut : null;
            //            }
            //3. Security issue with string. Should be changed to parameterized query
            string queryTemplate =
               "SELECT OrderId, OrderCustomerId, OrderDate" +
               "  FROM dbo.Orders where OrderId='{0}'";
            string query = string.Format(queryTemplate, orderId);
            //4. Dispose issue. using should be applied
            SqlConnection connection =
               new SqlConnection(this.connectionString);
            //5. Dispose issue. using should be applied
            SqlCommand command =
               new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
               Order order = new Order
               {
                  //6. Type conversion issue. Type conversion could be reduced. it's better to use reader.GetInt32(0), reader.GetString(1), GetDateTime(3)
                  //it will reduce the amount of type conversion
                  OrderId = (int) reader[0],
                  OrderCustomerId = (int) reader[1],
                  OrderDate = (DateTime) reader[2]
               };
               //7. Sync issue. It's better to use concurrent dictionary we use cache2.TryAdd(orderId, order);
               lock (cache)
               {
                  if (!cache.ContainsKey(orderId))
                     cache[orderId] = order;
               }
               //cache2.TryAdd(orderId, order);

               stopWatch.Stop();
               logger.InfoFormat("Elapsed - {0}", stopWatch.Elapsed);
               //reader and connection should be closed before return
               //reader.Close();
               //connection.Close();
               return order;
            }

            stopWatch.Stop();
            logger.InfoFormat("Elapsed - {0}", stopWatch.Elapsed);
            return null;
         }
         catch (SqlException ex)
         {
            logger.Error(ex.Message);
            throw new ApplicationException("Error");
            //8. Exception information propagation loosing issue. It's better to use throw ex;
         }
      }

      public Order LoadOrder(string orderId)
      {
         try
         {
            Debug.Assert(null != orderId && orderId != "");
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            if (_cache.ContainsKey(orderId))
            {
               stopWatch.Stop();
               logger.InfoFormat("Elapsed - {0}", stopWatch.Elapsed);
               return _cache.TryGetValue(orderId, out var orderOut) ? orderOut : null;
            }

            string query = "SELECT OrderId, OrderCustomerId, OrderDate  FROM dbo.Orders where OrderId= @orderId";
            var orderIdParameter = new SqlParameter("orderId", SqlDbType.VarChar) {Value = orderId};

            //3. Dispose issue. using should be applied
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
               //4. Dispose issue. using should be applied
               using (SqlCommand command = new SqlCommand(query, connection))
               {
                  command.Parameters.Add(orderIdParameter);
                  connection.Open();
                  SqlDataReader reader = command.ExecuteReader();
                  if (reader.Read())
                  {
                     Order order = new Order
                     {
                        //5. Type conversion issue. Type conversion could be reduced. it's better to use reader.GetInt32(0), reader.GetString(1), GetDateTime(3)
                        //it will reduce the amount of type conversion
                        OrderId = reader.GetInt32(0),
                        OrderCustomerId = reader.GetInt32(1),
                        OrderDate = reader.GetDateTime(2)
                     };
                     //6. Sync issue. It's better to use concurrent dictionary we use cache2.TryAdd(orderId, order);
                     _cache.TryAdd(orderId, order);
                     stopWatch.Stop();
                     logger.InfoFormat("Elapsed - {0}", stopWatch.Elapsed);

                     return order;
                  }
               }
            }

            stopWatch.Stop();
            logger.InfoFormat("Elapsed - {0}", stopWatch.Elapsed);
            return null;
         }
         catch (SqlException ex)
         {
            logger.Error(ex.Message);
            throw;
            //7. Exception information propagation loosing issue. It's better to use throw;
         }
      }
   }
}