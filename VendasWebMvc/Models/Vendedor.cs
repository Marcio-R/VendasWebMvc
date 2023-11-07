using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace VendasWebMvc.Models
{
    public class Vendedor
    {
        public int Id { get; set; }
        [Display(Name = "Nome")]
        [Required (ErrorMessage = "{0} requerido")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "O nome precisa de no minimo 3 letras.")]
        public string Nome { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "{0} requerido")]
        [EmailAddress(ErrorMessage = "Entre com email valido!")]
        public string Email { get; set; }

        [Display(Name = "Salário base")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        [Required(ErrorMessage = "{0} requerido")]
        [Range(0,100.000, ErrorMessage = "{0} O salário tem que ser no minimo {1} maximo {2}!")]
        public double Salario { get; set; }

        [Display(Name = "Data Nascimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "{0} requerido")]
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
