using ByteBank.Entities;
using ByteBank.Repositories;
using ByteBank.Views;

namespace ByteBank.Services
{
    public class ServicosClientes
    {
        public static void ListarClientes()
        {
            Console.Clear();
            foreach (Cliente cliente in RepositorioCliente.clientes)
            {
                Print.MostrarCliente(cliente);
            }
            Input.AcessarMenu();
        }

        public static void DetalharCliente()
        {
            string cpf;
            while (true)
            {
                cpf = Input.PegarCpf();
                if (!RepositorioCliente.ValidarCpf(cpf))
                {
                    Print.CpfNaoEcontrado();
                }
                else break;
            }
            Print.MostrarDetalhesCliente(RepositorioCliente.clientes.Find(c => c.Cpf == cpf));
        }

        public static void QuantiaTotalBanco()
        {
            double quantia = 0;
            foreach (Cliente cliente in RepositorioCliente.clientes)
            {
                quantia += cliente.Saldo;
            }
            Print.MostrarQuantiaTotal(quantia);
        }
    }
}