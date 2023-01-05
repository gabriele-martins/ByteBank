namespace ByteBank1.Views
{
    public class Menu
    {
        public static void MostrarMenuPrincipal()
        {
            Console.Clear();
            Console.WriteLine("\n\tEscolha uma opção para continuar: ");
            Console.WriteLine("\n\t1 - Inserir novo Usuário");
            Console.WriteLine("\t2 - Deletar um Usuário");
            Console.WriteLine("\t3 - Listar todas as contas registradas");
            Console.WriteLine("\t4 - Detalhes de um Usuário");
            Console.WriteLine("\t5 - Quantia armazenada no banco");
            Console.WriteLine("\t6 - Manipular a conta");
            Console.WriteLine("\t0 - Para sair do programa");
            Console.Write("\n\tDigite a opção desejada: ");
        }

        public static void MostrarMenuManipularConta()
        {
            Console.Clear();
            Console.WriteLine("\n\tEscolha uma opção para continuar: ");
            Console.WriteLine("\n\t1 - Depositar");
            Console.WriteLine("\t2 - Sacar");
            Console.WriteLine("\t3 - Transferir");
            Console.WriteLine("\t4 - Voltar para o menu principal");
            Console.Write("\n\tDigite a opção desejada: ");
        }
    }
}
