using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Library
{
    public class SqliteKalkulation
    {
        protected static string connectionString = "Data Source=DemoDB.db;Version=3;";
        public static List<Kalkulation> LadenKalkListe(int kID)
        {
            List<Kalkulation> listk = new();
            try
            {
                using (SQLiteConnection cnn = new SQLiteConnection(connectionString))
                {
                    cnn.Open();
                    string sql = "SELECT ID,Kunde FROM Kalkulation WHERE ID = " + kID;
                    if (kID == 0)
                    {
                        sql = "SELECT ID,Kunde FROM Kalkulation";
                    }
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, cnn))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Kalkulation k = new()
                                {
                                    ID = Int32.Parse(reader["ID"].ToString()),
                                    Kunde = reader["Kunde"].ToString()
                                };
                                listk.Add(k);
                            }
                        }
                    }
                    cnn.Close();
                }
            }
            catch (SQLiteException e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return listk;

        }
        public static int SaveKalk(Kalkulation k)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                int result = -1;

                connection.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = "INSERT INTO Kalkulation(ID, Kunde) VALUES (@ID, @Kunde)";
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@ID", k.ID);
                    cmd.Parameters.AddWithValue("@Kunde", k.Kunde);
                    try
                    {
                        result = cmd.ExecuteNonQuery();
                    }
                    catch (SQLiteException e)
                    {
                        MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return -1;
                    }
                }
                connection.Close();

                return result;
            }
        }
        public static int UpdateKalkulation(Kalkulation kalk)
        {
            int result = -1;
            using (SQLiteConnection cnn = new SQLiteConnection(connectionString))
            {
                cnn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(cnn))
                {
                    cmd.CommandText = "UPDATE Kalkulation SET Kunde=@Kunde WHERE ID = @ID";
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@ID", kalk.ID);
                    cmd.Parameters.AddWithValue("@Kunde", kalk.Kunde);
                    try
                    {
                        result = cmd.ExecuteNonQuery();
                    }
                    catch (SQLiteException e)
                    {
                        MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return -1;
                    }
                }
                cnn.Close();
            }
            return result;
        }
        public static int delKalk(Kalkulation kalk)
        {
            int result = -1;
            using (SQLiteConnection cnn = new SQLiteConnection(connectionString))
            {
                cnn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(cnn))
                {
                    cmd.CommandText = "DELETE FROM Kalkulation WHERE ID = @ID";
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@ID", kalk.ID);
                    try
                    {
                        result = cmd.ExecuteNonQuery();
                    }
                    catch (SQLiteException e)
                    {
                        MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return -1;
                    }
                }
                cnn.Close();
            }
            return result;
        }
        public static DataTable LadenMAAufwandAuschreibungAnfordern(string Abschnitt,int auftragsid)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SQLiteConnection cnn = new SQLiteConnection(connectionString))
                {
                    cnn.Open();
                    
                    string sql = string.Format(@"SELECT
                                                    T0.Arbeitszeit AS ""Stunden"",
                                                    T1.IDMA,
                                                    T1.Vorname,
                                                    T1.Nachname,
                                                    T1.Kostenfaktor
                                                FROM Kalkulation_Mitarbeiter T0 
                                                    LEFT JOIN Mitarbeiter T1 ON T0.MAID=T1.IDMA 
                                                WHERE T0.AuftragsID = {0} AND T0.Abschnitt = '{1}' 

                                                UNION ALL 

                                                SELECT
                                                    '0' AS ""Stunden"",
                                                    T0.IDMA,
                                                    T0.Vorname,
                                                    T0.Nachname,
                                                    T0.Kostenfaktor
                                                FROM Mitarbeiter T0
                                                    WHERE T0.IDMA NOT IN
                                                    (
                                                        SELECT
                                                            TX.MAID
                                                        FROM Kalkulation_Mitarbeiter TX
                                                        WHERE TX.AuftragsID = {0}
                                                                    AND TX.Abschnitt = '{1}'
                                                    )", auftragsid,Abschnitt);


                    using (SQLiteCommand cmd = new SQLiteCommand(sql, cnn))
                    {
                        using (SQLiteDataAdapter a = new SQLiteDataAdapter(cmd))
                            a.Fill(dt);
                    }

                    cnn.Close();
                }
            }
            catch (SQLiteException e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return dt;
        }
    }
        
}
