using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
//using Newtonsoft.Json;
using System.Net.Http.Headers;
using ISSHotel.config;

namespace ISSHotel
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ACCESSDB();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
            }

        }
        public static void ACCESSDB()
        {
            string connectionString = Connection.Default.accessDBPath;
            DataTable dtLedgerTable = new DataTable();
            DataTable dtTblGRCs = new DataTable();
            DataTable dtRoomStatus = new DataTable();

            dtLedgerTable.TableName = "LedgerTable";
            dtTblGRCs.TableName = "TblGRCs";
            dtRoomStatus.TableName = "RoomStatus";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                string strSQL = "SELECT * FROM LedgerTable where flag=0";
                OleDbCommand command = new OleDbCommand(strSQL, connection);
                try
                {
                    // Open connecton    
                    connection.Open();

                    //Execute command
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        dtLedgerTable.Load(reader);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("------------Getting Original data ----------------");
                    }

                    SQLUpdate(dtLedgerTable);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(" Ledger Talbe Completed....... \n -------------------------------------------------------------------------------");
                    Console.WriteLine(" Starting TblGRC Progress.................");
                    Console.WriteLine("Starting RoomStatu Progress...");

                    string query = "SELECT * FROM TblGRC";
                    OleDbCommand newCommend = new OleDbCommand(query, connection);
                    //Execute command
                    using (OleDbDataReader reader = newCommend.ExecuteReader())
                    {
                        dtTblGRCs.Load(reader);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("------------Getting TblGRCs data ----------------");
                    }
                    SQLUpdate(dtTblGRCs);
                    string roomsQuery = "SELECT * FROM RoomStatu";
                    OleDbCommand newcommend = new OleDbCommand(roomsQuery, connection);
                    //Execute command
                    using (OleDbDataReader reader = newCommend.ExecuteReader())
                    {
                        dtRoomStatus.Load(reader);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("-------------Getteing RoomStatus data---------");
                    }


                    SQLUpdate(dtRoomStatus);
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.ReadLine();
                // The connection is automatically closed becasuse of using block.    
            }

        }
        public static void SQLUpdate(DataTable dt)
        {

            var datasource = Connection.Default.datasource;//your server
            var database = Connection.Default.database; //your database name
            var username = Connection.Default.username; //username of server to connect
            var password = Connection.Default.password; //password

            //your connection string 
            string connString = @"Data Source=" + datasource + ";Initial Catalog="
                        + database + ";Persist Security Info=True;User ID=" + username + ";Password=" + password;
            try
            {
                Console.WriteLine("Openning Connection ...");
                using (SqlConnection sqlCon = new SqlConnection(connString))
                {
                    sqlCon.Open();
                    if (dt.TableName == "TblGRCs")
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            try
                            {
                                if (row["flag"].ToString() == "0")
                                {
                                    string strSQL = $"SELECT * FROM TblGRC where Status='{row["Status"]}' and tranNo={row["tranNo"]}";
                                    SqlCommand command = new SqlCommand(strSQL, sqlCon);
                                    SqlDataReader sqlDataReader = command.ExecuteReader();
                                    DataTable tempTable = new DataTable();
                                    tempTable.Load(sqlDataReader);

                                    if (tempTable.Rows.Count > 0)
                                    {
                                        string datet = row["date"].ToString();
                                        DateTime date = Convert.ToDateTime(datet);
                                        using (SqlCommand sqlCmd2 = new SqlCommand
                                        {
                                            CommandText = $"Update TblGRC set grcNo='{row["GrcNo"]}',income='{row["income"]}',expence='{row["expenses"]}',flag='{row["flag"]}'," +
                                            $"date='{date.ToString("yyyy-MM-dd")}',roomno='{row["roomno"]}',particular='{row["particular"]}',status='{row["status"]}',[user]='{row["user"]}',mode='{row["mode"]}',invno='{row["invno"]}' where Status='{row["Status"]}' and tranNo={row["tranNo"]}",

                                            Connection = sqlCon
                                        })
                                        {
                                            sqlDataReader.Close();
                                            sqlDataReader.Dispose();
                                            sqlCmd2.ExecuteNonQuery();
                                            int I = UpdateRecord(row);
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.WriteLine($" Transaction No {row["tranno"]} and Status {row["status"]} updated ");

                                        }


                                    }
                                    else
                                    {
                                        using (SqlCommand sqlCmd1 = new SqlCommand
                                        {
                                            CommandText = "INSERT INTO [Hotels] ([tranNo], [income], [expence], [flag], [date], [roomNo],[particular],[status],[user],mode,invNo) " +
                                   "VALUES (@tranNo, @income, @expence, @flag, @date, @roomNo,@particular,@status,@user,@mode,@invNo)",
                                            Connection = sqlCon
                                        })
                                        {
                                            sqlCmd1.Parameters.AddWithValue("@tranNo", row["tranNo"]);
                                            sqlCmd1.Parameters.AddWithValue("@income", row["income"]);
                                            sqlCmd1.Parameters.AddWithValue("@expence", row["Expenses"]);
                                            sqlCmd1.Parameters.AddWithValue("@flag", row["flag"]);
                                            sqlCmd1.Parameters.AddWithValue("@date", row["date"]);
                                            sqlCmd1.Parameters.AddWithValue("@roomNo", row["roomNo"]);
                                            sqlCmd1.Parameters.AddWithValue("@particular", row["particular"]);
                                            sqlCmd1.Parameters.AddWithValue("@status", row["status"]);
                                            sqlCmd1.Parameters.AddWithValue("@user", row["user"]);
                                            sqlCmd1.Parameters.AddWithValue("@mode", row["mode"]);
                                            sqlCmd1.Parameters.AddWithValue("@invNo", row["invNo"]);
                                            sqlDataReader.Close();
                                            sqlDataReader.Dispose();
                                            sqlCmd1.ExecuteNonQuery();
                                            int i = UpdateRecord(row);
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.WriteLine($" Transaction No {row["tranno"]} and Statu {row["status"]} Created as New ");
                                        }


                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($" Transaction No {row["tranno"]} and Statu {row["status"]} Got error : {ex.Message} ");
                                Console.ForegroundColor = ConsoleColor.Blue;
                            }
                        }
                    }
                    else if (dt.TableName == "RoomStatus")
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            try
                            {

                                using (SqlCommand sqlCmd1 = new SqlCommand
                                {
                                    CommandText = "truncate table RoomStatus  INSERT INTO RoomStatus ([id], [booking], [vacancy], [housKeeping], [date]) " +
                           "VALUES (@id, @booking, @vacancy, @housKeeping, @date)",
                                    Connection = sqlCon
                                })
                                {
                                    sqlCmd1.Parameters.AddWithValue("@id", row["Id"]);
                                    sqlCmd1.Parameters.AddWithValue("@booking", row["Booking"]);
                                    sqlCmd1.Parameters.AddWithValue("@vacancy", row["Vacancy"]);
                                    sqlCmd1.Parameters.AddWithValue("@housKeeping", row["HousKeeping"]);
                                    sqlCmd1.Parameters.AddWithValue("@date", row["Date"]);

                                    //sqlDataReader.Close();
                                    //sqlDataReader.Dispose();
                                    sqlCmd1.ExecuteNonQuery();
                                    int i = UpdateRecord(row);
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine($" Rooms No {row["Booking"]} and Statu {row["Vacancy"]} Created as New ");
                                }


                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($" Transaction No {row["tranno"]} and Statu {row["status"]} Got error : {ex.Message} ");
                                Console.ForegroundColor = ConsoleColor.Blue;
                            }
                        }
                    }
                    else if (dt.TableName == "LedgerTable")
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            try
                            {
                                if (row["flag"].ToString() == "0")
                                {

                                    string strSQL = $"SELECT * FROM Hotels where Status='{row["Status"]}' and tranNo={row["tranNo"]}";
                                    SqlCommand command = new SqlCommand(strSQL, sqlCon);
                                    SqlDataReader sqlDataReader = command.ExecuteReader();
                                    DataTable tempTable = new DataTable();
                                    tempTable.Load(sqlDataReader);

                                    if (tempTable.Rows.Count > 0)
                                    {
                                        string datet = row["date"].ToString();
                                        DateTime date = Convert.ToDateTime(datet);
                                        using (SqlCommand sqlCmd2 = new SqlCommand
                                        {
                                            CommandText = $"Update Hotels set tranNo='{row["tranno"]}',income='{row["income"]}',expence='{row["expenses"]}',flag='{row["flag"]}'," +
                                            $"date='{date.ToString("yyyy-MM-dd")}',roomno='{row["roomno"]}',particular='{row["particular"]}',status='{row["status"]}',[user]='{row["user"]}',mode='{row["mode"]}',invno='{row["invno"]}' where Status='{row["Status"]}' and tranNo={row["tranNo"]}",
                                            Connection = sqlCon
                                        })
                                        {
                                            sqlDataReader.Close();
                                            sqlDataReader.Dispose();
                                            sqlCmd2.ExecuteNonQuery();
                                            int I = UpdateRecord(row);
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.WriteLine($" Transaction No {row["tranno"]} and Status {row["status"]} updated ");

                                        }


                                    }
                                    else if (dt.TableName == "TblGRCs")
                                    {
                                        using (SqlCommand sqlCmd1 = new SqlCommand
                                        {
                                            CommandText = "INSERT INTO [Hotels] ([tranNo], [income], [expence], [flag], [date], [roomNo],[particular],[status],[user],mode,invNo) " +
                                   "VALUES (@tranNo, @income, @expence, @flag, @date, @roomNo,@particular,@status,@user,@mode,@invNo)",
                                            Connection = sqlCon
                                        })
                                        {
                                            sqlCmd1.Parameters.AddWithValue("@tranNo", row["tranNo"]);
                                            sqlCmd1.Parameters.AddWithValue("@income", row["income"]);
                                            sqlCmd1.Parameters.AddWithValue("@expence", row["Expenses"]);
                                            sqlCmd1.Parameters.AddWithValue("@flag", row["flag"]);
                                            sqlCmd1.Parameters.AddWithValue("@date", row["date"]);
                                            sqlCmd1.Parameters.AddWithValue("@roomNo", row["roomNo"]);
                                            sqlCmd1.Parameters.AddWithValue("@particular", row["particular"]);
                                            sqlCmd1.Parameters.AddWithValue("@status", row["status"]);
                                            sqlCmd1.Parameters.AddWithValue("@user", row["user"]);
                                            sqlCmd1.Parameters.AddWithValue("@mode", row["mode"]);
                                            sqlCmd1.Parameters.AddWithValue("@invNo", row["invNo"]);
                                            sqlDataReader.Close();
                                            sqlDataReader.Dispose();
                                            sqlCmd1.ExecuteNonQuery();
                                            int i = UpdateRecord(row);
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.WriteLine($" Transaction No {row["tranno"]} and Statu {row["status"]} Created as New ");
                                        }


                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($" Transaction No {row["tranno"]} and Statu {row["status"]} Got error : {ex.Message} ");
                                Console.ForegroundColor = ConsoleColor.Blue;
                            }
                        }
                    }


                    else if (dt.TableName == "RoomStatus")
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            try
                            {
                                if (row["flag"].ToString() == "0")
                                {

                                    string strSQL = $"SELECT * FROM Hotels where Status='{row["Status"]}' and tranNo={row["tranNo"]}";
                                    SqlCommand command = new SqlCommand(strSQL, sqlCon);
                                    SqlDataReader sqlDataReader = command.ExecuteReader();
                                    DataTable tempTable = new DataTable();
                                    tempTable.Load(sqlDataReader);

                                    if (tempTable.Rows.Count > 0)
                                    {
                                        string datet = row["date"].ToString();
                                        DateTime date = Convert.ToDateTime(datet);
                                        using (SqlCommand sqlCmd2 = new SqlCommand
                                        {
                                            CommandText = $"Update Hotels set id='{row["id"]}',booking='{row["booking"]}',vacancy='{row["vacancy"]}',housKeeping='{row["housKeeping"]}'," +
                                            $"date='{date.ToString("yyyy-MM-dd")}'",
                                            //CommandText = "Update Hotels set id='@id',booking='@booking',vacancy='@vacancy',housKeeping='@housKeeping',date='@date'",
                                            Connection = sqlCon
                                        })
                                        {
                                            sqlDataReader.Close();
                                            sqlDataReader.Dispose();
                                            sqlCmd2.ExecuteNonQuery();
                                            int I = UpdateRecord(row);
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.WriteLine($" Transaction No {row["tranno"]} and Status {row["status"]} updated ");

                                        }


                                    }
                                    else if (dt.TableName == "RoomStatus")
                                    {
                                        using (SqlCommand sqlCmd1 = new SqlCommand
                                        {
                                            CommandText = "INSERT INTO [Hotels] ([id], [booking], [vacancy], [housKeeping], [date]) " +
                                   "VALUES (@id, @booking, @vacancy, @housKeeping, @date)",
                                            Connection = sqlCon
                                        })
                                        {
                                            sqlCmd1.Parameters.AddWithValue("@id", row["id"]);
                                            sqlCmd1.Parameters.AddWithValue("@booking", row["booking"]);
                                            sqlCmd1.Parameters.AddWithValue("@vacancy", row["Vacancy"]);
                                            sqlCmd1.Parameters.AddWithValue("@housKeeping", row["housKeeping"]);
                                            sqlCmd1.Parameters.AddWithValue("@date", row["date"]);

                                            sqlDataReader.Close();
                                            sqlDataReader.Dispose();
                                            sqlCmd1.ExecuteNonQuery();
                                            int i = UpdateRecord(row);
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.WriteLine($" Transaction No {row["tranno"]} and Statu {row["status"]} Created as New ");
                                        }


                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($" Transaction No {row["tranno"]} and Statu {row["status"]} Got error : {ex.Message} ");
                                Console.ForegroundColor = ConsoleColor.Blue;
                            }
                        }
                    }
                    sqlCon.Close();
                }
                Console.WriteLine("Migrations successful!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

            //Console.Read();

        }
        public static int UpdateRecord(DataRow dr)
        {
            int res = 0;
            string connectionString = Connection.Default.accessDBPath;
            //  string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\USER\Downloads\Hotelmaster.mdb";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {

                string strSQL = $"update LedgerTable set flag =1 where Status='{dr["Status"]}' and tranNo={dr["tranNo"]}";
                OleDbCommand command = new OleDbCommand(strSQL, connection);
                try
                {
                    // Open connecton    
                    connection.Open();

                    //Execute command
                    command.ExecuteNonQuery();
                    res = 1;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                }
                // The connection is automatically closed becasuse of using block.    
            }
            return res;
        }
    }
}