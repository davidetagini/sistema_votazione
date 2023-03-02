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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SistemaVotazione
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

        string conndb = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        Login login = new Login();

        void aggiuntavotante()
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(conndb);

                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }

                if (nomeutente.Text != "" && cognomeutente.Text != "")
                {
                    SqlCommand sqlCommand = new SqlCommand
                    (
                       "INSERT INTO cont_votanti (nome_utente,cognome_utente) VALUES (@nome_utente,@cognome_utente)", sqlConnection
                    );

                    sqlCommand.Parameters.AddWithValue("@nome_utente", nomeutente.Text.Trim());
                    sqlCommand.Parameters.AddWithValue("@cognome_utente", cognomeutente.Text.Trim());

                    sqlCommand.ExecuteNonQuery();

                    MessageBox.Show("Ottimo, il votante è stato inserito", "Vota ora", MessageBoxButton.OK, MessageBoxImage.Information);

                    nomeutente.Text = "";
                    cognomeutente.Text = "";

                    sqlConnection.Close();
                }

                else
                {
                    MessageBox.Show("Errore, compila i campi per registrarti all'albo", "Vota ora", MessageBoxButton.OK, MessageBoxImage.Error);

                    nomeutente.Text = "";
                    cognomeutente.Text = "";
                }
            }

            catch(Exception exc)
            {
                MessageBox.Show($"Errore: {exc.Message}", "Vota ora", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        bool controlla_utente()
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(conndb);

                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }

                SqlCommand sqlCommand = new SqlCommand
                    (
                       "SELECT * FROM cont_votanti WHERE nome_utente='" + nomeutente.Text.Trim() + "'", sqlConnection
                    );

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                DataTable dataTable = new DataTable();

                sqlDataAdapter.Fill(dataTable);

                if (dataTable.Rows.Count >= 1)
                {
                    sqlConnection.Close();

                    return true;
                }

                else
                {
                    return false;
                }
            }

            catch(Exception exc)
            {
                MessageBox.Show($"Errore: {exc.Message}", "Vota ora", MessageBoxButton.OK, MessageBoxImage.Error);

                return false;
            } 
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void registratialbo_Click(object sender, RoutedEventArgs e)
        {
            if (controlla_utente())
            {
                MessageBox.Show("Errore, il votante è già presente", "Vota ora", MessageBoxButton.OK, MessageBoxImage.Error);

                nomeutente.Text = "";
                cognomeutente.Text = "";
            }

            else
            {
                aggiuntavotante();
            }
        }

        private void accedialbo_Click(object sender, RoutedEventArgs e)
        {
            login.Show();
        }
    }
}
