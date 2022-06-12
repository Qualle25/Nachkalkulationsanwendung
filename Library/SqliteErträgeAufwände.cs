using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Library
{
    public class SqliteErträgeAufwände
    {
        protected static string connectionString = "Data Source=DemoDB.db;Version=3;";
        public static List<Erträge> LadenErtragsListe(int eID)
        {
            List<Erträge> listert = new();
            try
            {
                using (SQLiteConnection cnn = new SQLiteConnection(connectionString))
                {
                    cnn.Open();
                    string sql = "SELECT ID,Ertrag,Wert FROM Erträge WHERE ID = " + eID;
                    if (eID == 0)
                    {
                        sql = "SELECT ID,Ertrag,Wert FROM Erträge";
                    }
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, cnn))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Erträge e = new()
                                {
                                    ID = Int32.Parse(reader["ID"].ToString()),
                                    Ertrag = reader["Ertrag"].ToString(),
                                    Wert= Int32.Parse(reader["Wert"].ToString()),
                                };
                                listert.Add(e);
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
            return listert;

        }
        public static int SaveErtrag(Erträge ert)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                int result = -1;

                connection.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = "INSERT INTO Erträge(ID, Ertrag, Wert) VALUES (@ID, @Ertrag, @Wert)";
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@ID", ert.ID);
                    cmd.Parameters.AddWithValue("@Ertrag", ert.Ertrag);
                    cmd.Parameters.AddWithValue("@Wert", ert.Wert);
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
        public static int DelErtrag(Erträge ert)
        {
            int result = -1;
            using (SQLiteConnection cnn = new SQLiteConnection(connectionString))
            {
                cnn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(cnn))
                {
                    cmd.CommandText = "DELETE FROM Erträge WHERE ID = @ID";
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@ID", ert.ID);
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
        public static int UpdateErtrag(Erträge ert)
        {
            int result = -1;
            using (SQLiteConnection cnn = new SQLiteConnection(connectionString))
            {
                cnn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(cnn))
                {
                    cmd.CommandText = "UPDATE Erträge SET Ertrag=@Ertrag, Wert=@Wert WHERE ID = @ID";
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@ID", ert.ID);
                    cmd.Parameters.AddWithValue("@Ertrag", ert.Ertrag);
                    cmd.Parameters.AddWithValue("Wert", ert.Wert);
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
        public static List<Aufwände> LadenAufwandsListe(int aID)
        {
            List<Aufwände> listauf = new();
            try
            {
                using (SQLiteConnection cnn = new SQLiteConnection(connectionString))
                {
                    cnn.Open();
                    string sql = "SELECT ID,Aufwand,Wert FROM Aufwände WHERE ID = " + aID;
                    if (aID == 0)
                    {
                        sql = "SELECT ID,Aufwand,Wert FROM Aufwände";
                    }
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, cnn))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Aufwände a = new()
                                {
                                    ID = Int32.Parse(reader["ID"].ToString()),
                                    Aufwand = reader["Aufwand"].ToString(),
                                    Wert = Int32.Parse(reader["Wert"].ToString()),
                                };
                                listauf.Add(a);
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
            return listauf;

        }
        public static int SaveAufwand(Aufwände auf)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                int result = -1;

                connection.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = "INSERT INTO Aufwäde(ID, Aufwand, Wert) VALUES (@ID, @Aufwand, @Wert)";                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@ID", auf.ID);
                    cmd.Parameters.AddWithValue("@Aufwand", auf.Aufwand);
                    cmd.Parameters.AddWithValue("@Wert", auf.Wert);
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
        public static int DelAufwände(Aufwände auf)
        {
            int result = -1;
            using (SQLiteConnection cnn = new SQLiteConnection(connectionString))
            {
                cnn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(cnn))
                {
                    cmd.CommandText = "DELETE FROM Aufwände WHERE ID = @ID";
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@ID", auf.ID);
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
        public static int UpdateAufwand(Aufwände auf)
        {
            int result = -1;
            using (SQLiteConnection cnn = new SQLiteConnection(connectionString))
            {
                cnn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(cnn))
                {
                    cmd.CommandText = "UPDATE Aufwände SET Aufwand=@Aufwand, Wert=@Wert WHERE ID = @ID";
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@ID", auf.ID);
                    cmd.Parameters.AddWithValue("@Ertrag", auf.Aufwand);
                    cmd.Parameters.AddWithValue("Wert", auf.Wert);
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

