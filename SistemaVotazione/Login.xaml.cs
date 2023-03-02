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

namespace SistemaVotazione
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        string conndb = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        PortaleVoto portaleVoto = new PortaleVoto();

        void accessovotante()
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(conndb);

                if (sqlConnection.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }

                if (nome_utente.Text != "" && cognome_utente.Text != "")
                {
                    SqlCommand sqlCommand = new SqlCommand
                    (
                       "SELECT * from cont_votanti WHERE nome_utente='" + nome_utente.Text.Trim() + "'", sqlConnection
                    );

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            MessageBox.Show($"Buon voto {nome_utente} {cognome_utente}");

                            Close();

                            portaleVoto.Show();
                        }
                    }

                    else
                    {
                        MessageBox.Show("Errore, non è possibile accedere. L'utente non è presente nei Log", "Vota ora", MessageBoxButton.OK, MessageBoxImage.Error);

                        nome_utente.Text = "";
                        cognome_utente.Text = "";
                    }
                }

                else
                {
                    MessageBox.Show("Si prega di compilare tutti i campi per accedere", "Vota ora", MessageBoxButton.OK, MessageBoxImage.Error);

                    nome_utente.Text = "";
                    cognome_utente.Text = "";
                }
                
            }

            catch(Exception exc)
            {
                MessageBox.Show($"{exc.Message}", "Vota ora", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public Login()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void accesso_utente_Click(object sender, RoutedEventArgs e)
        {
            accessovotante();
        }
    }
}
