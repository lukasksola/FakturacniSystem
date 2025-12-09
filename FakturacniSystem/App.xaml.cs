using FakturacniSystem.EF;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Data;
using System.Windows;

namespace FakturacniSystem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            await InitializeDatabaseAsync();

        }

        private async Task InitializeDatabaseAsync()
        {
            try
            {
                using (var db = new SqliteContext())
                {
                    await db.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database initialization failed:\n\n" + ex.Message);
            }
        }
    }

}
