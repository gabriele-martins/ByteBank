using ByteBank1.Repositories;
using ByteBank1.Services;
using ByteBank1.Views;

namespace ByteBank1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Print.AplicarTituloConsole();

            Print.MostrarPaginaInicial();

            Input.AcessarMenu();

            int option;

            do
            {
                Menu.MostrarMenuPrincipal();

                option = Input.PegarOpcao(0, 6);

                switch (option)
                {
                    case 0:
                        Print.MostrarPaginaFinal();
                        break;
                    case 1:
                        RepositorioCliente.AdicionarCliente();
                        break;
                    case 2:
                        RepositorioCliente.DeletarCliente();
                        break;
                    case 3:
                        ServicosClientes.ListarClientes();
                        break;
                    case 4:
                        ServicosClientes.DetalharCliente();
                        break;
                    case 5:
                        ServicosClientes.QuantiaTotalBanco();
                        break;
                    case 6:
                        ServicosConta.ManipularConta();
                        break;
                }

            } while (option != 0);
        }
    }
}