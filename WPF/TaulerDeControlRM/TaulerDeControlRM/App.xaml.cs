using ReproductorMusicaComponentLibrary.Classes;
using System.Configuration;
using System.Data;
using System.Windows;

namespace TaulerDeControlRM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // És només per fer probes de primera execució no ho mudifiquis encara youssef
            AppConfig config = new AppConfig { PrimeraExecucio = true };

            ConfiguracioWindow configWindow = new ConfiguracioWindow();
            configWindow.ShowDialog();

            config.PrimeraExecucio = false;

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            /*AppConfig config = ConfigManager.ObtenirConfiguracio();

            if (config.PrimeraExecucio)
            {
                ConfiguracioWindow configWindow = new ConfiguracioWindow();
                configWindow.ShowDialog();

                config.PrimeraExecucio = false;
                ConfigManager.ActualitzarConfiguracio(config);
            }
            else
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
            }*/
        }
    }
}