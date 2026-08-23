using System;
using System.Collections.Generic;
using System.Globalization;
using AutoCheck.ConsoleApp.Models;
using AutoCheck.ConsoleApp.Services;
 
namespace AutoCheck.ConsoleApp
{
    class Program
    {
        static List<Veiculo> vistorias = new List<Veiculo>();
        static MotorVistoria motor = new MotorVistoria();
 
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            bool executando = true;

            // Loop para ficar executando enquanto a opção 'Sair' não for selecionada
            while (executando)
            {
                ExibirCabecalho();
                ExibirMenu();
 
                string opcao = Console.ReadLine();
 
                switch (opcao)
                {
                    case "1":
                        RealizarNovaVistoria();
                        break;
                    case "2":
                        ExibirRelatorioVistorias();
                        break;
                    case "0":
                        executando = false;
                        Console.WriteLine("\nEncerrando o AutoCheck. Até logo!");
                        break;
                    default:
                        Console.WriteLine("\nOpção inválida. Pressione ENTER para tentar novamente.");
                        Console.ReadLine();
                        break;
                }
            }
        }
 
        // Exibe o cabeçalho no console
        static void ExibirCabecalho()
        {
            Console.Clear();
            Console.WriteLine("=======================================================================");
            Console.WriteLine("                    AUTOCHECK .NET - MOTOR DE VISTORIA");
            Console.WriteLine("=======================================================================\n");
        }
  
        // Exibe o menu no console
        static void ExibirMenu()
        {
            Console.WriteLine("1 - Realizar Nova Vistoria");
            Console.WriteLine("2 - Exibir Relatório das Vistorias");
            Console.WriteLine("0 - Sair");
            Console.Write("\nEscolha uma opção: ");
        }
  
        // Exibe o menu de seleção de veiculo no console
        static void RealizarNovaVistoria()
        {
            Console.Clear();
            Console.WriteLine("=== REALIZAR NOVA VISTORIA ===\n");
            Console.WriteLine("Selecione o tipo de veículo:");
            Console.WriteLine("1 - Carro");
            Console.WriteLine("2 - Moto");
            Console.WriteLine("3 - Caminhão");
            Console.Write("\nOpção: ");
            string tipo = Console.ReadLine();
 
            Console.Write("\nMarca: ");
            string marca = Console.ReadLine();
 
            Console.Write("Modelo: ");
            string modelo = Console.ReadLine();
 
            Console.Write("Ano: ");
            int ano = LerInteiro();
 
            Console.Write("Quilometragem: ");
            double km = LerDouble();
 
            Veiculo veiculo = null;
 
            if (tipo == "1")
            {
                Console.Write("Quantidade de portas: ");
                int portas = LerInteiro();
                veiculo = new Carro(marca, modelo, ano, km, portas);
            }
            else if (tipo == "2")
            {
                Console.Write("Cilindradas: ");
                int cilindradas = LerInteiro();
                veiculo = new Moto(marca, modelo, ano, km, cilindradas);
            }
            else if (tipo == "3")
            {
                Console.Write("Quantidade de eixos: ");
                int eixos = LerInteiro();
                Console.Write("Capacidade de carga (toneladas): ");
                double capacidade = LerDouble();
                veiculo = new Caminhao(marca, modelo, ano, km, eixos, capacidade);
            }
            else
            {
                Console.WriteLine("\nTipo de veículo inválido. Operação cancelada.");
                Console.WriteLine("\nPressione ENTER para voltar ao menu.");
                Console.ReadLine();
                return;
            }
 
            Console.WriteLine("\n> CHECKLIST DE VISTORIA");
            Console.WriteLine("Para cada item, informe o status: Bom, Regular ou Ruim.\n");
 
            List<string> checklist = veiculo.ObterChecklistObrigatorio();
            for (int i = 0; i < checklist.Count; i++)
            {
                string status = LerStatus(checklist[i]);
                veiculo.AdicionarItemVistoriado(checklist[i], status);
            }
 
            vistorias.Add(veiculo);
 
            Console.WriteLine("\nVistoria registrada com sucesso!");
            Console.WriteLine("\nPressione ENTER para voltar ao menu.");
            Console.ReadLine();
        }
  
        // Exibe o relatório das vistorias cadastradas no console
        static void ExibirRelatorioVistorias()
        {
            Console.Clear();
            Console.WriteLine("=======================================================================");
            Console.WriteLine("                    RELATÓRIO DE VISTORIAS");
            Console.WriteLine("=======================================================================\n");
 
            if (vistorias.Count == 0)
            {
                Console.WriteLine("Nenhuma vistoria realizada até o momento.");
            }
            else
            {
                for (int i = 0; i < vistorias.Count; i++)
                {
                    motor.ExibirRelatorio(vistorias[i], i + 1, vistorias.Count);
                }
                Console.WriteLine("=======================================================================");
                Console.WriteLine("                 FIM DO PROCESSAMENTO DE VISTORIAS");
                Console.WriteLine("=======================================================================");
            }
 
            Console.WriteLine("\nPressione ENTER para voltar ao menu.");
            Console.ReadLine();
        }
 
        static int LerInteiro()
        {
            int valor;
            while (!int.TryParse(Console.ReadLine(), out valor))
            {
                Console.Write("Valor inválido. Digite um número inteiro: ");
            }
            return valor;
        }
 
        static double LerDouble()
        {
            double valor;
            while (!double.TryParse(Console.ReadLine(), NumberStyles.Any, CultureInfo.InvariantCulture, out valor))
            {
                Console.Write("Valor inválido. Digite um número (use ponto para decimais): ");
            }
            return valor;
        }
 
        static string LerStatus(string nomeItem)
        {
            string status;
            while (true)
            {
                Console.Write($"  - {nomeItem} [Bom/Regular/Ruim]: ");
                status = Console.ReadLine();
 
                if (status == "Bom" || status == "Regular" || status == "Ruim")
                {
                    return status;
                }
 
                Console.WriteLine("    Status inválido. Use exatamente: Bom, Regular ou Ruim.");
            }
        }
    }
}