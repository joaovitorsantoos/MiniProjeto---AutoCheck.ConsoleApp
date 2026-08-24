# AutoCheck.ConsoleApp — Motor de Vistoria Veicular

Aplicação de console em C# (.NET) que simula o motor de processamento de vistoria técnica de uma rede de concessionárias. O sistema recebe os dados de um veículo (Carro, Moto ou Caminhão), aplica o checklist de inspeção correspondente, calcula a pontuação obtida, classifica o estado do veículo e gera um relatório com os serviços que a oficina precisa executar.

## O que o sistema faz

Para cada veículo cadastrado, o técnico avalia uma lista de itens obrigatórios atribuindo o status **Bom**, **Regular** ou **Ruim** a cada um. A partir dessas respostas, o sistema:

- Converte cada status em pontuação (Bom = 10, Regular = 5, Ruim = 0);
- Calcula o percentual de aprovação em relação à pontuação máxima possível;
- Classifica o veículo em uma de três faixas: *Aprovado com Excelência*, *Aprovado com Apontamentos* ou *Reprovado na Vistoria*;
- Lista separadamente os itens críticos (Ruim) e os itens de atenção (Regular);
- Recomenda os serviços que a oficina deve priorizar.

## Como executar

Pré-requisito: [.NET SDK](https://dotnet.microsoft.com/download) instalado.

```bash
git clone https://github.com/joaovitorsantoos/MiniProjeto---AutoCheck.ConsoleApp.git
cd MiniProjeto---AutoCheck.ConsoleApp
dotnet run
```

O menu principal aparece no terminal com três opções:

1. **1 — Realizar Nova Vistoria**: escolhe o tipo de veículo (Carro, Moto ou Caminhão), informa os dados cadastrais (marca, modelo, ano, quilometragem e o atributo específico do tipo) e responde o checklist item a item.
2. **2 — Exibir Relatório das Vistorias**: mostra o relatório completo de todas as vistorias já registradas na sessão atual (pontuação, percentual, classificação e pendências).
3. **0 — Sair**: encerra o programa.

> As vistorias ficam armazenadas apenas durante a execução (em memória). Ao fechar o programa, os dados são perdidos — não há persistência em arquivo ou banco de dados neste escopo do projeto.

## Regra de cálculo do percentual

Cada item vistoriado converte o status em pontos. A pontuação máxima possível é `total de itens × 10`. O percentual de aprovação é calculado como:

```
Percentual (%) = (Pontuação Obtida / Pontuação Máxima Possível) × 100
```

A divisão é feita com cast explícito para `double`, para evitar que o C# trunque o resultado para zero numa divisão de inteiros.

## Classificação final

| Percentual | Classificação | Ação |
|---|---|---|
| 90% a 100% | Aprovado com Excelência | Liberado para compra/revenda imediata |
| 60% a 89% | Aprovado com Apontamentos | Exige desconto para reparos na oficina |
| 0% a 59% | Reprovado na Vistoria | Veículo recusado pela concessionária |

## Conceitos do Módulo 01 aplicados

- **Tipos primitivos**: `string`, `int`, `double` nas propriedades dos modelos.
- **Listas (`List<T>`)**: `VistoriaRealizada` dentro de `Veiculo`, o checklist retornado por `ObterChecklistObrigatorio()` e a lista central de vistorias em `Program.cs`.
- **Laços tradicionais (`foreach`/`for`)**: toda a varredura de listas, cálculo de pontuação e filtragem de itens críticos/de atenção acontece sem LINQ, só com laços e `if`.
- **Estruturas condicionais (`if/else`)**: validação de status, mapeamento de pontuação e definição das faixas de classificação.
- **Programação Orientada a Objetos**:
  - Classe base `Veiculo`, com construtor explícito usando `this` para atribuição das propriedades;
  - Herança (`:`) nas subclasses `Carro`, `Moto` e `Caminhao`;
  - Polimorfismo via `virtual`/`override` no método `ObterChecklistObrigatorio()`, onde cada subclasse adiciona seus próprios itens ao checklist genérico herdado;
  - Encapsulamento na classe `ItemVistoria`, cujo `Status` só aceita os valores "Bom", "Regular" ou "Ruim" através de um setter customizado.

## Arquitetura cliente-servidor

O projeto não expõe rede, mas reproduz o mesmo princípio localmente: `Program.cs` funciona como o **cliente** — coleta a entrada do usuário via `Console.ReadLine()` e apresenta a saída — enquanto a classe `MotorVistoria` (em `Services/`) atua como o **servidor/engine**, concentrando toda a regra de negócio (cálculo de pontuação, classificação, geração do relatório) isolada da camada de interação com o usuário. É a mesma separação de responsabilidades que, numa aplicação web real, divide front-end (cliente) de back-end (que processa as regras e devolve o resultado).

## Estrutura do projeto

```
MiniProjeto---AutoCheck.ConsoleApp/
├── Models/
│   ├── ItemVistoria.cs
│   ├── Veiculo.cs
│   ├── Carro.cs
│   ├── Moto.cs
│   └── Caminhao.cs
├── Services/
│   └── MotorVistoria.cs
├── Program.cs
├── AutoCheck.csproj
└── README.md
```

## Link para vídeo de apresentação

https://drive.google.com/file/d/1RHjSeZPHCF23EHAKA8TZSa3P5QwyRgmn/view?usp=sharing
