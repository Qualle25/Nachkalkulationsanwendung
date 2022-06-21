using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Library
{
    public class SqliteMAKFZ
    {
        protected static string connectionString = "Data Source=DemoDB.db;Version=3;";

        public static int SaveMitarbeiterArbeitszeit(Mitarbeiter mitarbeiter, int AuftragsID)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                int result = -1;

                connection.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = "INSERT INTO Kalkulation_Mitarbeiter(Arbeitszeit, MAID, AuftragsID, Abschnitt) VALUES (@Arbeitszeit, @MAID, @AuftragsID, '0')";
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@Arbeitszeit", mitarbeiter.Arbeitszeit);
                    cmd.Parameters.AddWithValue("@MAID", mitarbeiter.Id);
                    cmd.Parameters.AddWithValue("@AuftragsID", AuftragsID);


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