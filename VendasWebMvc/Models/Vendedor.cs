using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace VendasWebMvc.Models
{
    public class Vendedor
    {
        public int Id { get; set; }
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Salário base")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double Salario { get; set; }

        [Display(Name = "Data Nascimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
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
