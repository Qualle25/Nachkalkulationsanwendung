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

        public static int SaveMitarbeiterArbeitszeit(Mitarbeiter mitarbeiter, int AuftragsID, string Abschnitt)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                int result = -1;

                connection.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = "INSERT INTO Kalkulation_Mitarbeiter(Arbeitszeit, MAID, AuftragsID, Abschnitt) VALUES (@Arbeitszeit, @MAID, @AuftragsID, @Abschnitt)";
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@Arbeitszeit", mitarbeiter.Arbeitszeit);
                    cmd.Parameters.AddWithValue("@MAID", mitarbeiter.Id);
                    cmd.Parameters.AddWithValue("@AuftragsID", AuftragsID);
                    cmd.Parameters.AddWithValue("@Abschnitt", Abschnitt);



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

        public static int UpdateMitarbeiterArbeitszeit(Mitarbeiter mitarbeiter, int AuftragsID, string Abschnitt)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                int result = -1;

                connection.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = @"UPDATE Kalkulation_Mitarbeiter 
                                        SET Arbeitszeit = @Arbeitszeit 
                                        WHERE MAID = @MAID 
                                            AND AuftragsID = @AuftragsID 
                                            AND Abschnitt = @Abschnitt";

                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@Arbeitszeit", mitarbeiter.Arbeitszeit);
                    cmd.Parameters.AddWithValue("@MAID", mitarbeiter.Id);
                    cmd.Parameters.AddWithValue("@AuftragsID", AuftragsID);
                    cmd.Parameters.AddWithValue("@Abschnitt", Abschnitt);

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