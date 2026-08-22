using System.Collections.Generic;


namespace AutoCheck.ConsoleApp.Models
{
    // Forma para qualquer veiculo que passar no sistema
    public abstract  class Veiculo
    {
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }
        public double Quilometragem { get; set; }
        public List<ItemVistoria> VistoriaRealizada { get; set; }
 
        // Define os dados que serão coletados ao cadastrar um veículo
        protected Veiculo(string marca, string modelo, int ano, double quilometragem)
        {
            this.Marca = marca;
            this.Modelo = modelo;
            this.Ano = ano;
            this.Quilometragem = quilometragem;
            this.VistoriaRealizada = new List<ItemVistoria>();
        }
 
        // Pega o nome e o status que o usuario passou e cria a ficha do item
        public void AdicionarItemVistoriado(string nome, string status)
        {
            VistoriaRealizada.Add(new ItemVistoria(nome, status));
        }

        // Dados genéricos da inspeção que funcionam em qualquer veículo
        public virtual List<string> ObterChecklistObrigatorio()
        {
            return new List<string>
            {
                "Nível de Óleo do Motor",
                "Bateria e Sistema Elétrico",
                "Documentação Regularizada"
            };
        }
 
        public abstract string ObterAtributoEspecifico();
 
        public abstract string ObterTipo();
    }
}