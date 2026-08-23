using System;
using System.Collections.Generic;
using System.Globalization;
using AutoCheck.ConsoleApp.Models;
 
namespace AutoCheck.ConsoleApp.Services
{
    public class MotorVistoria
    {

        // Soma a pontuação de todos os itens
        public int CalcularPontuacaoAtingida(Veiculo veiculo)
        {
            int pontuacao = 0;
            foreach (ItemVistoria item in veiculo.VistoriaRealizada)
            {
                pontuacao += item.ObterPontuacao();
            }
            return pontuacao;
        }
 
        public int CalcularPontuacaoMaximaPossivel(Veiculo veiculo)
        {
            return veiculo.VistoriaRealizada.Count * 10;
        }
 
        // Percentual de aprovação
        public double CalcularPercentualAprovacao(Veiculo veiculo)
        {
            int obtida = CalcularPontuacaoAtingida(veiculo);
            int maxima = CalcularPontuacaoMaximaPossivel(veiculo);
 
            if (maxima == 0) return 0;
 
            return ((double)obtida / maxima) * 100;
        }
 
        // Classificação final do veículo
        public string ClassificarVeiculo(double percentual)
        {
            if (percentual >= 90)
            {
                return "Aprovado com Excelência";
            }
            else if (percentual >= 60)
            {
                return "Aprovado com Apontamentos";
            }
            else
            {
                return "Reprovado na Vistoria";
            }
        }
 
        // Filtra os itens com status "Ruim"
        public List<ItemVistoria> ObterItensCriticos(Veiculo veiculo)
        {
            var criticos = new List<ItemVistoria>();
            foreach (ItemVistoria item in veiculo.VistoriaRealizada)
            {
                if (item.Status == "Ruim")
                {
                    criticos.Add(item);
                }
            }
            return criticos;
        }
 
        // Filtra os itens com status "Regular" (mesma logica do anterior)
        public List<ItemVistoria> ObterItensAtencao(Veiculo veiculo)
        {
            var atencao = new List<ItemVistoria>();
            foreach (ItemVistoria item in veiculo.VistoriaRealizada)
            {
                if (item.Status == "Regular")
                {
                    atencao.Add(item);
                }
            }
            return atencao;
        }
 
        /// Texto de recomendação para um serviço critico 
        private string ObterRecomendacaoServico(string nomeItem, bool critico)
        {
            if (critico)
            {
                return $"{nomeItem}: Repor equipamento obrigatório ausente/danificado.";
            }
            return $"{nomeItem}: Realizar revisão preventiva e checagem detalhada.";
        }
 
        // Imprime o relatório completo no console
        public void ExibirRelatorio(Veiculo veiculo, int indiceAtual, int totalVistorias)
        {
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine($"[{indiceAtual}/{totalVistorias}] PROCESSANDO VISTORIA");
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("> DADOS DO VEÍCULO:");
            Console.WriteLine($"  - Tipo: {veiculo.ObterTipo()}");
            Console.WriteLine($"  - Modelo: {veiculo.Marca} {veiculo.Modelo}");
            Console.WriteLine($"  - Ano: {veiculo.Ano} | Quilometragem: {veiculo.Quilometragem:N0} km");
            Console.WriteLine($"  - Atributo Específico: {veiculo.ObterAtributoEspecifico()}");
            Console.WriteLine();
 
            Console.WriteLine($"> AVALIAÇÃO DOS ITENS INSPECIONADOS ({veiculo.VistoriaRealizada.Count} ITENS):");
            for (int i = 0; i < veiculo.VistoriaRealizada.Count; i++)
            {
                ItemVistoria item = veiculo.VistoriaRealizada[i];
                string marcador;
                if (item.Status == "Bom") marcador = "[OK]";
                else if (item.Status == "Regular") marcador = "[ ! ]";
                else marcador = "[ X ]";
 
                Console.WriteLine($"  {marcador} {item.Nome} ---- Status: {item.Status} ({item.ObterPontuacao()} pts)");
            }
            Console.WriteLine();
 
            int obtida = CalcularPontuacaoAtingida(veiculo);
            int maxima = CalcularPontuacaoMaximaPossivel(veiculo);
            double percentual = CalcularPercentualAprovacao(veiculo);
            string classificacao = ClassificarVeiculo(percentual);
 
            Console.WriteLine("> RESUMO DA PONTUAÇÃO:");
            Console.WriteLine($"  - Pontuação Atingida: {obtida} de {maxima} pontos possíveis");
            Console.WriteLine($"  - Percentual de Aprovação: {percentual.ToString("0.0", CultureInfo.InvariantCulture)}%");
            Console.WriteLine($"  - Classificação Final: [ {classificacao.ToUpper()} ]");
            Console.WriteLine();
 
            List<ItemVistoria> criticos = ObterItensCriticos(veiculo);
            List<ItemVistoria> atencao = ObterItensAtencao(veiculo);
 
            Console.WriteLine("> RELATÓRIO DE MANUTENÇÃO E RECOMENDAÇÕES DA OFICINA:");
            if (criticos.Count == 0 && atencao.Count == 0)
            {
                Console.WriteLine("  Nenhuma pendência mecânica identificada. Veículo liberado para operação!");
            }
            else
            {
                if (criticos.Count > 0)
                {
                    Console.WriteLine("  ITENS CRÍTICOS / REPROVADOS (AÇÃO IMEDIATA):");
                    foreach (ItemVistoria item in criticos)
                    {
                        Console.WriteLine($"    - {ObterRecomendacaoServico(item.Nome, true)}");
                    }
                    Console.WriteLine();
                }
 
                if (atencao.Count > 0)
                {
                    Console.WriteLine("  ITENS DE ATENÇÃO (REVISÃO PREVENTIVA):");
                    foreach (ItemVistoria item in atencao)
                    {
                        Console.WriteLine($"    - {ObterRecomendacaoServico(item.Nome, false)}");
                    }
                }
            }
            Console.WriteLine();
        }
    }
}