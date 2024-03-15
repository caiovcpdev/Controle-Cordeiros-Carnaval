using System.ComponentModel.DataAnnotations;

namespace ControleCordeirosCarnaval.Models
{
    public class CordeiroModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite o nome do Cordeiro!")]
        public required string Nome { get; set; }
        [Required(ErrorMessage = "Digite o CPF_CNPJ do Cordeiro!")]
        public required string CPF_CNPJ { get; set; }
        public string ? RG { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;
    }
}
