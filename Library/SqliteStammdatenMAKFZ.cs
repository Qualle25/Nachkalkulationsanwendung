using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Library;


namespace Library
{
    public class SqliteStammdatenMAKFZ
    {
        
        protected static string connectionString = "Data Source=DemoDB.db;Version=3;";
        
        public static Mitarbeiter GetMitarbeiterByID(int id)
        {
            Mitarbeiter model = null;

            try
            {
                using (SQLiteConnection cnn = new SQLiteConnection(connectionString))
                {
                    cnn.Open();

                    string sql = "SELECT IDMA,Vorname,Nachname,Kostenfaktor FROM Mitarbeiter WHERE IDMA = " + id;

                    using (SQLiteCommand cmd = new SQLiteCommand(sql, cnn))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            if(reader.HasRows && reader.Read())
                            {
                                model = new Mitarbeiter()
                                {
                                    Id = reader.GetInt32(0),
                                    Vorname = reader.GetString(1),
                                    Nachname = reader.GetString(2),
                                    Kostenfaktor = reader.GetDecimal(3),
                                };
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

            return model;
        }

        public static DataTable LadenMitarbeiterDT(int maID)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SQLiteConnection cnn = new SQLiteConnection(connectionString))
                {
                    cnn.Open();

                    string sql = "SELECT IDMA,Vorname,Nachname,Kostenfaktor FROM Mitarbeiter WHERE IDMA = " + maID;

                    if (maID == 0)
                    {
                        sql = "SELECT IDMA,Vorname,Nachname,Kostenfaktor FROM Mitarbeiter";
                    }

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

        public static int SaveMitarbeiter(Mitarbeiter mitarbeiter)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                int result = -1;

                connection.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = "INSERT INTO Mitarbeiter(Vorname, Nachname, Kostenfaktor) VALUES (@Vorname, @Nachname, @Kostenfaktor)";
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@Vorname", mitarbeiter.Vorname);
                    cmd.Parameters.AddWithValue("@Nachname", mitarbeiter.Nachname);
                    cmd.Parameters.AddWithValue("@Kostenfaktor", mitarbeiter.Kostenfaktor);
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
        

        public static int updateMitarbeiter(Mitarbeiter mitarbeiter)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                int result = -1;

                connection.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = "UPDATE Mitarbeiter SET Vorname=@Vorname, Nachname=@Nachname, Kostenfaktor=@Kostenfaktor WHERE IDMA=@Id";
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@Vorname",mitarbeiter.Vorname);
                    cmd.Parameters.AddWithValue("@Nachname",mitarbeiter.Nachname);
                    cmd.Parameters.AddWithValue("@Kostenfaktor",mitarbeiter.Kostenfaktor);
                    cmd.Parameters.AddWithValue("@Id",mitarbeiter.Id);
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
        public static int delMitarbeiter(int mitarbeiterID)
        {
            int result = -1;
            using (SQLiteConnection cnn = new SQLiteConnection(connectionString))
            {
                cnn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(cnn))
                {
                    cmd.CommandText = "DELETE FROM Mitarbeiter WHERE IDMA = @Id";
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@Id", mitarbeiterID);
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



        public static DataTable LadenKfzDT(int kfzID)
        {
            DataTable dtk = new DataTable();
            try
            {
                using (SQLiteConnection cnn = new SQLiteConnection(connectionString))
                {
                    cnn.Open();
                    string sql = "SELECT IDKfz,Kennzeichen,Faktor FROM Kfz WHERE Id = " + kfzID;
                    if (kfzID == 0)
                    {
                        sql = "SELECT IDKfz,Kennzeichen,Faktor FROM Kfz";
                    }

                    using (SQLiteCommand cmd = new SQLiteCommand(sql, cnn))
                    {
                        using (SQLiteDataAdapter k = new SQLiteDataAdapter(cmd))
                        {
                                    k.Fill(dtk);
                            
                            
                        }
                    }
                    cnn.Close();
                }
            }
            catch (SQLiteException e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return dtk;
        }
        public static int SaveKfz(Kfz kfz)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                int result = -1;

                connection.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = "INSERT INTO Kfz(Kennzeichen, Faktor) VALUES (@Kennzeichen, @Faktor)";
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@Kennzeichen", kfz.Kennzeichen);
                    cmd.Parameters.AddWithValue("@Faktor", kfz.Faktor);
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
        public static int delKfz(int kfzID)
        {
            int result = -1;
            using (SQLiteConnection cnn = new SQLiteConnection(connectionString))
            {
                cnn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(cnn))
                {
                    cmd.CommandText = "DELETE FROM Kfz WHERE IDKfz = @Id";
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@Id", kfzID);
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
        public static int updateKfz(Kfz kfz)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                int result = -1;

                connection.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = "UPDATE Kfz SET Kennzeichen = @Kennzeichen, Faktor = @Faktor WHERE (IDKfz = @ID)";
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@Kennzeichen", kfz.Kennzeichen);
                    cmd.Parameters.AddWithValue("@Faktor", kfz.Faktor);
                    cmd.Parameters.AddWithValue("@ID", kfz.ID);
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

    }
}
