using System.Collections.Generic;
 
namespace AutoCheck.ConsoleApp.Models
{
    public class Moto : Veiculo
    {
        public int Cilindradas { get; set; }
 
        public Moto(string marca, string modelo, int ano, double quilometragem, int cilindradas)
            : base(marca, modelo, ano, quilometragem)
        {
            this.Cilindradas = cilindradas; // Armazena quantas cilindradas (propriedade especifica da moto) a moto tem
        }
 
        public override List<string> ObterChecklistObrigatorio()
        {
            var checklist = base.ObterChecklistObrigatorio(); // Reaproveitamento de código do Model 'Veiculo'
            checklist.Add("Kit Transmissão/Corrente");
            checklist.Add("Manetes de Freio/Embreagem");
            checklist.Add("Pezinho Lateral");
            return checklist;
        }
 
        public override string ObterAtributoEspecifico()
        {
            return $"{Cilindradas} Cilindradas";
        }
 
        public override string ObterTipo()
        {
            return "Moto";
        }
    }
}
 