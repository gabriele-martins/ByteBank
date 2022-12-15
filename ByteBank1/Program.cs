namespace ByteBank1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "Byte Bank 1";
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();

            Console.WriteLine("\n");
            Console.WriteLine("\tOlá, bem-vindo(a) ao Byte Bank.");
            Console.Write("\n\tPressione qualquer tecla para acessar o menu ");
            Console.ReadKey();

            int option=10;
            do
            {
                ShowMenu();
                try
                {
                    option = int.Parse(Console.ReadLine());
                    if (option<0 || option>6)
                    {
                        Console.Clear();
                        Console.WriteLine("\n");
                        Console.WriteLine("\tOpção inserida inválida. Tente novamente.");
                        Console.Write("\n\tPressione qualquer tecla para voltar ao menu ");
                        Console.ReadKey();
                    }
                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine("\n");
                    Console.WriteLine("\tLetras ou caractéres não são válidos. Tente novamente.");
                    Console.Write("\n\tPressione qualquer tecla para voltar ao menu ");
                    Console.ReadKey();
                }

                switch (option)
                {
                    case 0:
                        Console.Clear();
                        Console.WriteLine("\n");
                        Console.WriteLine("\tEncerrando o programa.\n\n\tObrigado por usar.");
                        Console.WriteLine("\n");
                        break;
                    case 1:
                        try
                        {
                            Console.Clear();
                            Console.WriteLine("\n");
                            Console.Write("\tDigite o CPF do titular: ");
                            string cpf = Console.ReadLine();
                            Usuario usuario = new Usuario(cpf);
                            Console.Write("\n\tDigite o nome do titular: ");
                            usuario.Nome = Console.ReadLine();
                            Console.Write("\n\tDigite uma senha para a conta: ");
                            usuario.Senha = Console.ReadLine();
                            usuario.RegistrarNovoUsuario();
                            Console.Write("\n\tPressione qualquer tecla para voltar ao menu ");
                            Console.ReadKey();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"{e.Message}");
                            Console.Write("\n\tPressione qualquer tecla para voltar ao menu ");
                            Console.ReadKey();
                        }
                        break;
                    case 2:
                        try
                        {
                            Console.Clear();
                            Console.WriteLine("\n");
                            Console.Write("\tDigite o CPF do titular da conta a ser deletada: ");
                            string cpf = Console.ReadLine();
                            Usuario.DeletarUsuario(cpf);
                            Console.Write("\n\tPressione qualquer tecla para voltar ao menu ");
                            Console.ReadKey();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"{e.Message}");
                            Console.Write("\n\tPressione qualquer tecla para voltar ao menu ");
                            Console.ReadKey();
                        }
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("\n");
                        Usuario.ListarContas();
                        Console.Write("\n\tPressione qualquer tecla para voltar ao menu.");
                        Console.ReadKey();
                        break;
                    case 4:
                        try
                        {
                            Console.Clear();
                            Console.WriteLine("\n");
                            Console.Write("\tDigite o CPF do titular da conta a ser detalhada: ");
                            string cpf = Console.ReadLine();
                            Usuario.DetalharUsuario(cpf);
                            Console.Write("\n\tPressione qualquer tecla para voltar ao menu.");
                            Console.ReadKey();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"{e.Message}");
                            Console.Write("\n\tPressione qualquer tecla para voltar ao menu.");
                            Console.ReadKey();
                        }
                        break;
                    case 5:
                        try
                        {
                            Console.Clear();
                            Console.WriteLine("\n");
                            Usuario.QuantiaArmazenada();
                            Console.Write("\n\tPressione qualquer tecla para voltar ao menu.");
                            Console.ReadKey();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"{e.Message}");
                            Console.Write("\n\tPressione qualquer tecla para voltar ao menu.");
                            Console.ReadKey();
                        }
                        break;
                    case 6:
                        try
                        {
                            Console.Clear();
                            Console.WriteLine("\n");
                            Console.Write("\tDigite o CPF do titular da conta a ser manipulada: ");
                            string cpf = Console.ReadLine();
                            Usuario.ManipularConta(cpf);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"{e.Message}");
                            Console.Write("\n\tPressione qualquer tecla para voltar ao menu.");
                            Console.ReadKey();
                        }
                        break;
                }

            } while (option!=0);
        }

        static void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("\n");
            Console.WriteLine("\tEscolha uma opção para continuar: ");
            Console.WriteLine("\n\t1 - Inserir novo Usuário");
            Console.WriteLine("\t2 - Deletar um Usuário");
            Console.WriteLine("\t3 - Listar todas as contas registradas");
            Console.WriteLine("\t4 - Detalhes de um Usuário");
            Console.WriteLine("\t5 - Quantia armazenada no banco");
            Console.WriteLine("\t6 - Manipular a conta");
            Console.WriteLine("\t0 - Para sair do programa");
            Console.Write("\n\tDigite a opção desejada: ");
        }
    }
}