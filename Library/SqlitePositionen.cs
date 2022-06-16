using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Library
{
    public class SqlitePositionen
    {
        protected static string connectionString = "Data Source=DemoDB.db;Version=3;";
        public static List<Positionen> LadenPositionsListe(int eID)
        {
            List<Positionen> listpos = new();
            try
            {
                using (SQLiteConnection cnn = new SQLiteConnection(connectionString))
                {
                    cnn.Open();
                    string sql = "SELECT Position, PErtrag, PAufwand, PositionID FROM Positionen WHERE ID= " + eID;
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, cnn))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Positionen p = new()
                                {
                                    PositionID = int.Parse(reader["PositionID"].ToString()),
                                    Position = reader["Position"].ToString(),
                                    PErtrag = decimal.Parse(reader["PErtrag"].ToString()),
                                    PAufwand = decimal.Parse(reader["PAufwand"].ToString())

                                };
                                listpos.Add(p);
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
            return listpos;

        }
        public static int SavePosition(Positionen p)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                int result = -1;

                connection.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = "INSERT INTO Positionen(PositionID, ID, Position, PErtrag, PAufwand) VALUES (@PositionID, @ID, @Position, @Pertrag, @PAufwand)";
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@PositionID", p.PositionID);
                    cmd.Parameters.AddWithValue("@ID", p.ID);
                    cmd.Parameters.AddWithValue("@Position", p.Position);
                    cmd.Parameters.AddWithValue("@PErtrag", p.PErtrag);
                    cmd.Parameters.AddWithValue("@PAufwand", p.PAufwand);
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
        public static int DelPosition(Positionen p)
        {
            int result = -1;
            using (SQLiteConnection cnn = new SQLiteConnection(connectionString))
            {
                cnn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(cnn))
                {
                    cmd.CommandText = "DELETE FROM Positionen WHERE PositionID = @PositionID";
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@PositionID", p.PositionID);
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
        public static int UpdatePosition(Positionen p)
        {
            int result = -1;
            using (SQLiteConnection cnn = new SQLiteConnection(connectionString))
            {
                cnn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(cnn))
                {
                    cmd.CommandText = "UPDATE Positionen SET Position=@Position, PErtrag=PErtrag, PAufwand=@PAufwand WHERE PositionID = @PositionID";
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@PositionID", p.PositionID);
                    cmd.Parameters.AddWithValue("@Position", p.Position);
                    cmd.Parameters.AddWithValue("@PErtrag", p.PErtrag);
                    cmd.Parameters.AddWithValue("@PAufwand", p.PAufwand);
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
