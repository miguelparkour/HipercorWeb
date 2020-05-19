

namespace HipercorWeb.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string Img { get; set; }
        public int Cantidad { get; set; }


        public override int GetHashCode()
        {
            return Id;
        }
        public override bool Equals(object obj)
        {
            return obj.GetHashCode() == this.GetHashCode();
        }
    }
}