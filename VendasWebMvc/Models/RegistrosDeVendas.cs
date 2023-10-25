using VendasWebMvc.Models.Enums;

namespace VendasWebMvc.Models
{
    public class RegistrosDeVendas
    {
        public int Id { get; set; }
        public DateTime Data{ get; set; }
        public double Quantia { get; set; }
        public StatusVenda Status { get; set; }
        public Vendedor Vendedor { get; set; }

        public RegistrosDeVendas()
        {
        }

        public RegistrosDeVendas(int id, DateTime data, double quantia, StatusVenda status, Vendedor vendedor)
        {
            Id = id;
            Data = data;
            Quantia = quantia;
            Status = status;
            Vendedor = vendedor;
        }
    }
}
