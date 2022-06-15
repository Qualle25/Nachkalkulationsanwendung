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
                    string sql = "SELECT IDErtrag,Ertrag,Wert_Ertrag FROM Erträge WHERE ID= " + eID;
                    if (eID == 0)
                    {
                        sql = "SELECT IDErtrag,Ertrag,Wert_Ertrag FROM Erträge";
                    }
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, cnn))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Erträge e = new()
                                {
                                    IDErtrag = int.Parse(reader["IDErtrag"].ToString()),
                                    Ertrag = reader["Ertrag"].ToString(),
                                    Ertrag_Wert= decimal.Parse(reader["Wert_Ertrag"].ToString())
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
                    cmd.CommandText = "INSERT INTO Erträge(IDErtrag, ID, Ertrag, Wert_Ertrag) VALUES (@IDErtrag, @ID, @Ertrag, @Wert_Ertrag)";
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@IDErtrag", ert.IDErtrag);
                    cmd.Parameters.AddWithValue("@ID", ert.ID);
                    cmd.Parameters.AddWithValue("@Ertrag", ert.Ertrag);
                    cmd.Parameters.AddWithValue("@Wert_Ertrag", ert.Ertrag_Wert);
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
                    cmd.CommandText = "DELETE FROM Erträge WHERE IDErtrag = @IDErtrag";
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@IDErtrag", ert.IDErtrag);
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
                    cmd.CommandText = "UPDATE Erträge SET Ertrag=@Ertrag, Wert_Ertrag=@Wert_Ertrag WHERE IDErtrag = @IDErtrag";
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@IDErtrag", ert.IDErtrag);
                    cmd.Parameters.AddWithValue("@Ertrag", ert.Ertrag);
                    cmd.Parameters.AddWithValue("Wert_Ertrag", ert.Ertrag_Wert);
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
                    string sql = "SELECT IDAufwand,Aufwand,Wert_Aufwand FROM Aufwände WHERE ID= " + aID;
                    if (aID == 0)
                    {
                        sql = "SELECT IDAufwand,Aufwand,Wert_Aufwand FROM Aufwände";
                    }
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, cnn))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Aufwände a = new()
                                {
                                    IDAufwand = Int32.Parse(reader["IDAufwand"].ToString()),
                                    Aufwand = reader["Aufwand"].ToString(),
                                    Wert_Aufwand = Int32.Parse(reader["Wert_Aufwand"].ToString()),
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
                    cmd.CommandText = "INSERT INTO Kalkulation(IDAufwand, Aufwand, Wert_Aufwand) VALUES (@IDAufwand, @Aufwand, @Wert_Aufwand)";                    
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@IDAufwand", auf.IDAufwand);
                    cmd.Parameters.AddWithValue("@Aufwand", auf.Aufwand);
                    cmd.Parameters.AddWithValue("@Wert_Aufwand", auf.Wert_Aufwand);
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
                    cmd.CommandText = "DELETE FROM Kalkulation WHERE IDAufwand = @IDAufwand";
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@IDAufwand", auf.IDAufwand);
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
                    cmd.CommandText = "UPDATE Kalkulation SET Aufwand=@Aufwand, Wert_Aufwand=@Wert_Aufwand WHERE IDAufwand = @IDAufwand";
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@IDAufwand", auf.IDAufwand);
                    cmd.Parameters.AddWithValue("@Aufwand", auf.Aufwand);
                    cmd.Parameters.AddWithValue("Wert_Aufwand", auf.Wert_Aufwand);
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

