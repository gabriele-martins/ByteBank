using ByteBank.Entities;
using ByteBank.Services;

namespace ByteBank.Views
{
    public class Print
    {
        public static void AplicarTituloConsole()
        {
            Console.Title = "Byte Bank";
        }

        public static void AplicarVermelhoErro(string message)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void AplicarVerdeSucesso(string message)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void MostrarPaginaInicial()
        {
            Console.WriteLine("\n\tOlá, bem-vindo(a) ao Byte Bank.");
        }

        public static void MostrarPaginaFinal()
        {
            Console.Clear();
            Console.WriteLine("\n\tEncerrando o programa.");
            Console.WriteLine("\n\tObrigado por usar.\n");
        }

        public static void MostrarCliente(Cliente cliente)
        {
            Console.WriteLine($"\n\tNome: {cliente.Nome}");
            Console.WriteLine($"\tCPF: {cliente.Cpf.ToString().Insert(9, "-").Insert(6, ".").Insert(3, ".")}");
            Console.WriteLine("\tSaldo: R${0:F2}\n", cliente.Saldo);
        }

        public static void MostrarDetalhesCliente(Cliente cliente)
        {
            Console.Clear();
            Console.WriteLine($"\n\tNome: {cliente.Nome}");
            Console.WriteLine($"\tCPF: {cliente.Cpf.ToString().Insert(9, "-").Insert(6, ".").Insert(3, ".")}");
            Console.WriteLine($"\tSenha: {cliente.Senha}");
            Console.WriteLine("\tSaldo: R${0:F2}\n", cliente.Saldo);
            Input.AcessarMenu();
        }

        public static void MostrarQuantiaTotal(double valor)
        {
            Console.Clear();
            Console.WriteLine($"\n\tA quantidade total armazenada no banco é R${valor:F2}");
            Input.AcessarMenu();
        }

        public static void MostrarSaldo(double saldo)
        {
            Console.Clear();
            Console.WriteLine("\n\tSaldo Atual: R${0:F2}", saldo);
        }

        public static void PedirCpf()
        {
            Console.Clear();
            Console.Write("\n\tDigite o CPF do titular: ");
        }

        public static void PedirCpf(string message)
        {
            Console.Clear();
            Console.Write($"\n\tDigite o CPF do titular {message}: ");
        }

        public static void PedirNome()
        {
            Console.Clear();
            Console.Write("\n\tDigite o nome do titular: ");
        }

        public static void PedirSenha()
        {
            Console.Clear();
            Console.Write("\n\tDigite a senha: ");
        }

        public static void PedirValor(double saldo)
        {
            Console.Clear();
            Console.WriteLine("\n\tSaldo Atual: R${0:F2}", saldo);
            Console.Write("\n\tDigite o valor: ");
        }

        public static void ClienteAdicionadoSucesso()
        {
            AplicarVerdeSucesso("\n\tCliente registrado com sucesso.");
        }
        public static void ClienteRemovidoSucesso()
        {
            AplicarVerdeSucesso("\n\tCliente removido com sucesso.");
        }

        public static void DepositoSucesso(double saldo)
        {
            string message = $"\n\tValor depositado com sucesso. Saldo: R${saldo:F2}";
            AplicarVerdeSucesso(message);
            Input.AcessarMenu();
        }

        public static void SaqueSucesso(double saldo)
        {
            string message = $"\n\tValor sacado com sucesso. Saldo: R${saldo:F2}";
            AplicarVerdeSucesso(message);
            Input.AcessarMenu();
        }

        public static void TransferenciaSucesso(double saldo)
        {
            string message = $"\n\tValor transferido com sucesso. Saldo: R${saldo:F2}";
            AplicarVerdeSucesso(message);
            Input.AcessarMenu();
        }

        public static void LetraOuCharInvalido()
        {
            AplicarVermelhoErro("\n\tLetras ou caractéres não são válidos.");
            Input.AcessarMenu();
        }

        public static void OpcaoInvalida()
        {
            AplicarVermelhoErro("\n\tOpção inserida inválida.");
            Input.AcessarMenu();
        }

        public static void CpfNaoEcontrado()
        {
            AplicarVermelhoErro("\n\tO CPF digitado não é válido ou não foi encontrado.");
            Input.TentarNovamente();
        }

        public static void SenhaIncorreta()
        {
            AplicarVermelhoErro("\n\tSenha incorreta.");
            Input.TentarNovamente();
        }

        public static void ValorIncorreto()
        {
            Console.Clear();
            AplicarVermelhoErro("\n\tValor incorreto. O valor deve ser numérico e maior que zero.");
            Input.TentarNovamente();
        }

        public static void ValorInvalido()
        {
            Console.Clear();
            AplicarVermelhoErro("\n\tValor invalido. O valor deve ser menor que o saldo.");
            Input.TentarNovamente();
        }

        public static void SaldoZero()
        {
            AplicarVermelhoErro("\n\tSeu saldo é nulo ou negativo, portanto não é possível realizar a operação.");
            Input.AcessarMenu();
        }
    }
}