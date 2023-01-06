using ByteBank.Repositories;
using ByteBank.Exceptions;
using System.Globalization;

namespace ByteBank.Entities
{
    public class Cliente
    {
        private string cpf;
        public string Cpf
        {
            get { return cpf; }
            set
            {
                if (value.Length != 11 || !value.All(char.IsDigit))
                {
                    throw new ExcecaoCpfInvalido("\n\tCPF inválido. O CPF deve conter 11 caractéres (números apenas).");
                }
                else if (RepositorioCliente.ValidarCpf(value))
                {
                    throw new ExcecaoCpfInvalido("\n\tCliente já cadastrado.");
                }
                else
                {
                    cpf = value;
                }
            }
        }

        private string nome;
        public string Nome
        {
            get { return nome; }
            set
            {
                if (value.Length > 60 || value.Length < 2 || value.Where(c => char.IsDigit(c)).Count() > 0 || value.Where(c => char.IsPunctuation(c)).Count() > 0 || value.Where(c => char.IsSymbol(c)).Count() > 0 || value.Where(c => char.IsNumber(c)).Count() > 0)
                {
                    throw new ExcecaoNomeInvalido("\n\tNome inválido. O nome deve conter no mínimo 2 e no máximo 60 caractéres (letras e espaços apenas).");
                }
                else
                {
                    nome = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value);
                }
            }
        }

        private string senha;
        public string Senha
        {
            get { return senha; }
            set
            {
                if (value.Length < 5 || value.Length > 50)
                {
                    throw new ExcecaoSenhaInvalida("\n\tSenha inválida. O senha deve conter no mínimo 5 e no máximo 50 caractéres.");
                }
                else
                {
                    senha = value;
                }
            }
        }

        public double Saldo { get; set; }
    }
}