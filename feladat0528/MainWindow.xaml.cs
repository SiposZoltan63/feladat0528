using System.Data;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace feladat0528
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private string ConnectionString = "Server=localhost;Database=siposz;Uid=root;password=;Sslmode=None";

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var connection = new MySqlConnection(ConnectionString))
                {
                    string sql = "SELECT * FROM filmek";
                    connection.Open();

                    using (var cmd = new MySqlCommand(sql, connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            List<string> adatok = new List<string>();

                            while (reader.Read())
                            {
                                adatok.Add($"{reader["filmazon"]};{reader["cim"]};{reader["ev"]};{reader["szines"]};{reader["mufaj"]};{reader["hossz"]}");
                            }

                            lbAdatok.ItemsSource = adatok;
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void lbAdatok_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbAdatok.SelectedItem != null)
            {
                var selectedItem = lbAdatok.SelectedItem as string;
                if (selectedItem != null)
                {
                    var adatok = selectedItem.Split(';');

                    lbFilmAzon.Content = adatok[0];
                    tb1.Text = adatok[1];
                    tb2.Text = adatok[2];
                    tb3.Text = adatok[3];
                    tb4.Text = adatok[4];
                    tb5.Text = adatok[5];
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            try
            {
                using (var connection = new MySqlConnection(ConnectionString))
                {
                    string sql = $"UPDATE filmek SET " +
                                 $"cim = '{tb1.Text}', " +
                                 $"ev = {tb2.Text}, " +
                                 $"szines = '{tb3.Text}', " +
                                 $"mufaj = '{tb4.Text}', " +
                                 $"hossz = {tb5.Text} " +
                                 $"WHERE filmazon = '{lbFilmAzon.Content}'";

                    connection.Open();

                    using (var cmd = new MySqlCommand(sql, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    connection.Close();
                    MessageBox.Show("A módosítás sikerült!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}