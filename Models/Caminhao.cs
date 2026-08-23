using System.Collections.Generic;
using System.Globalization;
 
namespace AutoCheck.ConsoleApp.Models
{
    public class Caminhao : Veiculo
    {
        public int QuantidadeEixos { get; set; }
        public double CapacidadeCargaToneladas { get; set; }
 
        public Caminhao(string marca, string modelo, int ano, double quilometragem,
                         int quantidadeEixos, double capacidadeCargaToneladas)
            : base(marca, modelo, ano, quilometragem)
        {
            this.QuantidadeEixos = quantidadeEixos; // Armazena quantos eixos (propriedade especifica do caminhão) o caminhão tem
            this.CapacidadeCargaToneladas = capacidadeCargaToneladas; // Armazena a capacidade de carga (propriedade especifica do caminhão) o caminhão possui
        }
 
        public override List<string> ObterChecklistObrigatorio() 
        {
            var checklist = base.ObterChecklistObrigatorio(); // Reaproveitamento de código do Model 'Veiculo'
            checklist.Add("Funcionamento do Tacógrafo");
            checklist.Add("Sistema de Freios a Ar");
            checklist.Add("Trava e Lona da Caçamba");
            return checklist;
        }
 
        public override string ObterAtributoEspecifico()
        {
            string capacidade = CapacidadeCargaToneladas.ToString("0.0", CultureInfo.InvariantCulture); // Formatação para exibir sempre uma casa decimal
            return $"{QuantidadeEixos} Eixos | Cap. Carga: {capacidade} Toneladas";
        }
 
        public override string ObterTipo()
        {
            return "Caminhão";
        }
    }
}
