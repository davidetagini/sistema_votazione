using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace SistemaVotazione
{
    /// <summary>
    /// Interaction logic for PortaleVoto.xaml
    /// </summary>
    public partial class PortaleVoto : Window
    {
        public PortaleVoto()
        {
            InitializeComponent();
        }

        string conndb = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        void carica_voto()
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(conndb);

                if (sqlConnection.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }

                    SqlCommand sqlCommand = new SqlCommand
                        (
                           "INSERT INTO log_votazioni (personaggio) VALUES (@personaggio)", sqlConnection
                        );

                    sqlCommand.Parameters.AddWithValue("@personaggio", personaggio.SelectedValue.ToString());

                    sqlCommand.ExecuteNonQuery();

                    MessageBox.Show("Hai votato con successo", "Vota ora", MessageBoxButton.OK, MessageBoxImage.Information);

                    sqlConnection.Close();
            }

            catch(Exception exc)
            {
                MessageBox.Show($"Errore: {exc.Message}", "Vota ora", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void disconnetti_albo_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void invia_scelta_Click(object sender, RoutedEventArgs e)
        {
            carica_voto();
        }
    }
}
