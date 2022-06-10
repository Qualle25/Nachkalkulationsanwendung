using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Library
{
    public class SQLiteCalc
    {
        protected static string connectionString = "Data Source=DemoDB.db;Version=3;";
        public static List<KalkModel> LadenKalkListe(int kID)
        {
            List<KalkModel> listk = new();
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
                                KalkModel k = new()
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
        public static int SaveKalk(KalkModel k)
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
        public static int delKalk(KalkModel kalk)
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
    }
        
}
