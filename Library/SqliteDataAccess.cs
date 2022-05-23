using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;
using static Library.Globale;

namespace Library
{
    public class SqliteDataAccess
    {
        private SQLiteConnection cnn;
        private SQLiteCommand cmd;
        private SQLiteDataAdapter DB;
        private DataSet ds=new DataSet();
        private DataTable dt=new DataTable();
        protected static string connectionString = "Data Source=library.db;Version=3;";

        private void SetConnection()
        {
            cnn=new SQLiteConnection("Data Source=DemoDB.db;Version=3,New=False;Compress=True;");
        }
        private void Ausführen(string text) 
        {
            SetConnection();
            cnn.Open();
            cmd=cnn.CreateCommand();
            cmd.CommandText=text;
            cmd.ExecuteNonQuery();
            cnn.Close();
        }
        
        public static List<MitarbeiterModel> LadenMitarbeiterListe()
        {
            List<MitarbeiterModel> listma = new List<MitarbeiterModel>();
            try
            {
                using (SQLiteConnection cnn = new SQLiteConnection(connectionString))
                {
                    cnn.Open();
                    string sql = "SELECT * FROM Mitarberiter";
                    
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, cnn))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MitarbeiterModel ma = new MitarbeiterModel();
                                ma.Vorname = reader["Vorname"].ToString();
                                ma.Nachname = reader["Nachname"].ToString();
                                ma.Kostenfaktor = Convert.ToDecimal(reader["Kfaktor"]);
                                listma.Add(ma);
                            }
                        }
                    }
                    cnn.Close();
                }
            }
            catch (SQLiteException e)
            {
                return e;
            }
            return listma;

        }
        public static void SaveMitarbeiter(MitarbeiterModel mitarbeiter)
        {
            string Text = "insert into Mitarbeiter (Vorname,Nachname,Kostenfaktor)values(@Vorname,) "
        }
        public static List<KfzModel> LadenKfzListe()
        {

        }
        public static void SaveKfz(KfzModel kfz)
        {

        }

        public static void delmitarbeiter(MitarbeiterModel mitarbeiter)
        {

            cnn.Execute("delete from Mitarbeiter where IDMA = @Id", mitarbeiter);

        }

        public static void updateKfz(KfzModel kfz)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("update Kfz set ");
            }
        }

    }
}
