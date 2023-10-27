using System.Linq;

namespace VendasWebMvc.Models
{
    public class Vendedor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public double Salario { get; set; }
        public DateTime DataNascimento { get; set; }
        public Departamento Departamento { get; set; }
        public int DepartamentoId { get; set; }
        public ICollection<RegistrosDeVendas> Vendas { get; set; } = new List<RegistrosDeVendas>();

        public Vendedor()
        {
        }

        public Vendedor(int id, string nome, string email, double salario, DateTime dataNascimento, Departamento departamento)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Salario = salario;
            DataNascimento = dataNascimento;
            Departamento = departamento;
        }
        public void AddVenda(RegistrosDeVendas venda)
        {
            Vendas.Add(venda);
        }
        public void RemoveVenda(RegistrosDeVendas venda)
        {
            Vendas.Remove(venda);
        }
        public double TotalDeVendas(DateTime inicial,DateTime final)
        {
            return Vendas.Where(rd => rd.Data >= inicial && rd.Data <= final).Sum(rd => rd.Quantia);
        }
    }
}
