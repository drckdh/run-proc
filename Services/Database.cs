// using Microsoft.Data.SqlClient;

// namespace RunProcApi.Services
// {
//     public class SqlService
//     {
//         public static SqlConnection GetSqlConnection () {
//             SqlConnection cnn = new SqlConnection
//             {
//                 ConnectionString = ConfigurationManager.ConnectionStrings["LOCKBOX_EXCEPTIONS"].ConnectionString
//             };
//             cnn.Open();
//             return cnn;
//         }
//         public static SqlCommand GetSqlCommand () {
//             SqlCommand cmd = new SqlCommand
//             {
//                 Connection = GetSqlConnection(),
//                 CommandTimeout = 30,
//                 CommandType = System.Data.CommandType.StoredProcedure
//             };
//             return cmd;
//         }
//     }
// }