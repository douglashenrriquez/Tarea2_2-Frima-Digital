using SQLite;

namespace Tarea2_2
{
    public class Firma
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string ImagenPath { get; set; }
    }
}
