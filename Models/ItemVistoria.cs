using System;


namespace AutoCheck.ConsoleApp.Models
{
    public class ItemVistoria
    {
        public string Nome { get; set; }

        private static readonly string[] StatusValidos = { "Bom", "Regular", "Ruim" };

        private string _status;
        public string Status
        {
            get => _status;
            set
            {
                bool valido = false;
                for (int i = 0; i < StatusValidos.Length; i++)
                {
                    if (StatusValidos[i] == value)
                    {
                        valido = true;
                        break;
                    }
                }

                // Validação para garantir que o sistema não rode com informações erradas
                if (!valido)
                {
                    throw new ArgumentException($"Status inválido: '{value}'. Use 'Bom', 'Regular' ou 'Ruim'.");
                }

                _status = value;
            }
        }

        public ItemVistoria(string nome, string status)
        {
            this.Nome = nome;
            this.Status = status;
        }

        // Le o Status do item e retorna a pontuação
        // 10 = Bom, 5 = Regular, 0 = Ruim
        public int ObterPontuacao()
        {
            if (Status == "Bom") return 10;
            if (Status == "Regular") return 5;
            return 0;
        }
    }
}