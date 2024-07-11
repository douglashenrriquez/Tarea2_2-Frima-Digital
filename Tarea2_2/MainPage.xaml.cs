using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Syncfusion.Maui.SignaturePad;
using SQLite;
using Microsoft.Maui.Graphics;
using Syncfusion.Maui.Core.Internals;

namespace Tarea2_2
{
    public partial class MainPage : ContentPage
    {
        private SQLiteConnection _db;
        //private ObservableCollection<Firma> _firmas;

        public MainPage()
        {
            InitializeComponent();

            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "firmas.db3");
            _db = new SQLiteConnection(dbPath);
            _db.CreateTable<Firma>();

           // _firmas = new ObservableCollection<Firma>(_db.Table<Firma>());
           //firmasListView.ItemsSource = _firmas;
        }

        private async void OnGuardarFirmaClicked(object sender, EventArgs e)
        {
            string nombre = nombreEntry.Text;
            string descripcion = descripcionEntry.Text;

            var imageStream = await signaturePad.GetStreamAsync(Syncfusion.Maui.Core.ImageFileFormat.Png);

            if (imageStream != null)
            {
                string fileName = $"{nombre}_{DateTime.Now.Ticks}.png";
                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    await imageStream.CopyToAsync(fileStream);
                }

                var firma = new Firma
                {
                    Nombre = nombre,
                    Descripcion = descripcion,
                    ImagenPath = filePath
                };

                _db.Insert(firma);

                nombreEntry.Text = string.Empty;
                descripcionEntry.Text = string.Empty;
                signaturePad.Clear();
            }
            else
            {
                await DisplayAlert("Error", "No se pudo obtener la imagen de la firma", "OK");
            }
        }

        private async void OnVerFirmasClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListaFirmasPage());
        }
    }
}
