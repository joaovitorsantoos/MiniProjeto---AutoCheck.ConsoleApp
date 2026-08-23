using System.Collections.Generic;
 
namespace AutoCheck.ConsoleApp.Models
{
    public class Carro : Veiculo
    {
        public int QuantidadePortas { get; set; } 
 
        public Carro(string marca, string modelo, int ano, double quilometragem, int quantidadePortas)
            : base(marca, modelo, ano, quilometragem)
        {
            this.QuantidadePortas = quantidadePortas; // Armazena quantas portas (propriedade especifica do carro) o carro tem
        }
 
        public override List<string> ObterChecklistObrigatorio()
        {
            var checklist = base.ObterChecklistObrigatorio(); // Reaproveitamento de código do Model 'Veiculo'
            checklist.Add("Estepe e Macaco");
            checklist.Add("Triângulo de Sinalização");
            checklist.Add("Ar Condicionado Funcional");
            return checklist;
        }
 
        public override string ObterAtributoEspecifico()
        {
            return $"{QuantidadePortas} Portas";
        }
 
        public override string ObterTipo()
        {
            return "Carro";
        }
    }
}
 