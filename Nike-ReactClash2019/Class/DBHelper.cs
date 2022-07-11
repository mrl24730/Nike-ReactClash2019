using Kitchen;
using Nike_ReactClash2019.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Web;

namespace Nike_ReactClash2019.Class
{
    public class DBHelper
    {
        #region Constraints
        private static readonly string envir = (string)ConfigurationManager.AppSettings["Environment"];
        public static readonly string constr = (string)ConfigurationManager.ConnectionStrings["dB" + envir].ConnectionString;
        public static string defaultSKey = "nuOt=Glycs1";
        public static string fixedIV = "RyejratFec.os9";
        #endregion

        public static List<Timeslot> GetTimeslot()
        {
            List<Timeslot> timeslots = new List<Timeslot>();

            string sql = "SELECT * FROM tbl_reactclash_timeslot";

            using (SqlConnection conn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            timeslots.Add(new Timeslot
                            {
                                Id = (int)reader["tid"],
                                Name_tc = (string)reader["name_tc"],
                                Qty = (int)reader["qty"],
                                Slot_group = (string)reader["slot_group"]
                            });
                        }
                    }
                }
            }

            return timeslots;
        }

        public static int AddRecord(Record record)
        {
            int status = 1;
            DateTime Now = DateTime.Now;

            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    var transaction = conn.BeginTransaction();
                    cmd.Connection = conn;
                    cmd.Transaction = transaction;

                    try
                    {
                        cmd.CommandText = @"SELECT qty FROM tbl_reactclash_timeslot (UPDLOCK) WHERE tid = @Tid";
                        cmd.Parameters.Add("@Tid", SqlDbType.Int).Value = record.Tid;
                        int remainQty = (int)cmd.ExecuteScalar();
                        if (remainQty < 1)
                        {
                            return status = 2;
                        }
                        cmd.Parameters.Clear();

                        cmd.CommandText = @"UPDATE tbl_reactclash_timeslot SET qty = qty - 1 WHERE tid = @Tid";
                        cmd.Parameters.Add("@Tid", SqlDbType.Int).Value = record.Tid;
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

                        cmd.CommandText =
                            @"INSERT INTO tbl_reactclash_record
                            (
                                name, phone, email, birth, gender, tid, subscribe, created_at
                            ) VALUES (
                                @Name,
                                @EncryptedPhone,
                                @EncryptedEmail,
                                @Birth,
                                @Gender,
                                @Tid,
                                @Subscribe,
                                @CreatedAt
                            )";
                        cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = record.Name;
                        cmd.Parameters.Add("@EncryptedPhone", SqlDbType.VarChar, 255).Value = record.EncryptedPhone;
                        cmd.Parameters.Add("@EncryptedEmail", SqlDbType.VarChar, 255).Value = record.EncryptedEmail;
                        cmd.Parameters.Add("@Birth", SqlDbType.DateTime).Value = record.Birth ?? SqlDateTime.Null;
                        cmd.Parameters.Add("@Gender", SqlDbType.Bit).Value = record.IsMale ?? SqlBoolean.Null;
                        cmd.Parameters.Add("@Tid", SqlDbType.Int).Value = record.Tid;
                        cmd.Parameters.Add("@Subscribe", SqlDbType.Bit).Value = record.Subscribe;
                        cmd.Parameters.Add("@CreatedAt", SqlDbType.DateTime).Value = Now;
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

                        transaction.Commit();
                        status = 0;
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                    }
                }
            }
            return status;
        }

        public static List<Report> GetReports()
        {
            List<Report> reports = new List<Report>();

            string sql = "SELECT * FROM tbl_reactclash_timeslot INNER JOIN tbl_reactclash_record ON tbl_reactclash_timeslot.tid = tbl_reactclash_record.tid ORDER BY created_at";

            using (SqlConnection conn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            reports.Add(new Report
                            {
                                record = new Record
                                {
                                    Name = (string)reader["name"],
                                    Phone = Int32.Parse(CryptoHelper.decryptAES((string)reader["phone"], defaultSKey)),
                                    Email = CryptoHelper.decryptAES((string)reader["email"], defaultSKey),
                                    Birth = reader["birth"] == DBNull.Value ? null : (DateTime?)reader["birth"],
                                    IsMale = reader["gender"] == DBNull.Value ? null : (bool?)reader["gender"],
                                    Tid = (int)reader["tid"],
                                    //Lang = (bool)reader["lang"],
                                    Subscribe = (bool)reader["subscribe"],
                                    CreatedAt = (DateTime)reader["created_at"]
                                },
                                timeslot = new Timeslot
                                {
                                    Id = (int)reader["tid"],
                                    Name_tc = (string)reader["name_tc"],
                                    //Name_en = (string)reader["name_en"],
                                    Qty = (int)reader["qty"],
                                    Slot_group = (string)reader["slot_group"]
                                }
                            });
                        }
                    }
                }
            }
            return reports;
        }

        public static bool IsExistByPhone(string EncryptedPhone)
        {
            using (SqlConnection conn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"SELECT COUNT(rid) FROM tbl_reactclash_record WHERE phone = @EncryptedPhone";
                    cmd.Parameters.Add("@EncryptedPhone", SqlDbType.VarChar, 255).Value = EncryptedPhone;
                    conn.Open();

                    int count = (int)cmd.ExecuteScalar();
                    if (count > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
