using ByteBank.Views;

namespace ByteBank.Services
{
    public class Input
    {
        public static void AcessarMenu()
        {
            Console.Write("\n\tPressione qualquer tecla para acessar o menu ");
            Console.ReadKey();
        }

        public static void TentarNovamente()
        {
            Console.Write("\n\tPressione qualquer tecla para tentar novamente ");
            Console.ReadKey();
        }

        public static int PegarOpcao(int min, int max)
        {
            string stringOption = Console.ReadLine();

            if (!stringOption.All(char.IsDigit) || string.IsNullOrEmpty(stringOption))
            {
                Print.LetraOuCharInvalido();
                return 10;
            }
            else if (int.Parse(stringOption) < min || int.Parse(stringOption) > max)
            {
                Print.OpcaoInvalida();
                return 10;
            }
            else
            {
                return int.Parse(stringOption);
            }
        }

        public static string PegarCpf()
        {
            Print.PedirCpf();
            string cpf = Console.ReadLine();
            return cpf;
        }

        public static string PegarCpf(string message)
        {
            Print.PedirCpf(message);
            string cpf = Console.ReadLine();
            return cpf;
        }

        public static string PegarNome()
        {
            Print.PedirNome();
            string nome = Console.ReadLine();
            return nome;
        }

        public static string PegarSenha()
        {
            Print.PedirSenha();
            string senha = Console.ReadLine();
            return senha;
        }

        public static double PegarValor(double saldo)
        {
            string valor;
            double resultado;
            while (true)
            {
                Print.PedirValor(saldo);
                valor = Console.ReadLine();
                if (!double.TryParse(valor, out resultado) || resultado <= 0)
                {
                    Print.ValorIncorreto();
                }
                else break;
            }
            resultado = double.Parse(valor.Replace('.', ','));
            return resultado;
        }
    }
}