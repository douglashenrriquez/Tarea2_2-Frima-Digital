using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using SQLite;
using System.IO;

namespace Tarea2_2
{
    public partial class ListaFirmasPage : ContentPage
    {
        private SQLiteConnection _db;
        private ObservableCollection<Firma> _firmas;

        public ListaFirmasPage()
        {
            InitializeComponent();

            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "firmas.db3");
            _db = new SQLiteConnection(dbPath);
            _db.CreateTable<Firma>();

            _firmas = new ObservableCollection<Firma>(_db.Table<Firma>());
            firmasListView.ItemsSource = _firmas;
        }

        private async void OnFirmaSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var firma = e.SelectedItem as Firma;
            if (firma != null)
            {
                var imageSource = ImageSource.FromFile(firma.ImagenPath);

                var imageView = new Image
                {
                    Source = imageSource,
                    Aspect = Aspect.AspectFit,
                    WidthRequest = 200,
                    HeightRequest = 200
                };

                var stackLayout = new StackLayout
                {
                    Children =
                    {
                        imageView,
                        new Label { Text = $"Nombre: {firma.Nombre}" },
                        new Label { Text = $"Descripción: {firma.Descripcion}" }
                    }
                };

                var contentPage = new ContentPage
                {
                    Content = stackLayout,
                };

                await Navigation.PushAsync(contentPage);
            }
        }
    }
}
