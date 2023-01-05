using ByteBank1.Entities;
using ByteBank1.Repositories;
using ByteBank1.Views;

namespace ByteBank1.Services
{
    public class ServicosConta
    {
        public static void ManipularConta()
        {
            string cpf, senha;
            int option;
            while (true)
            {
                cpf = Input.PegarCpf();
                if (!RepositorioCliente.ValidarCpf(cpf))
                {
                    Print.CpfNaoEcontrado();
                }
                else break;
            }
            while (true)
            {
                senha = Input.PegarSenha();
                if (!RepositorioCliente.ValidarSenha(cpf, senha))
                {
                    Print.SenhaIncorreta();
                }
                else break;
            }
            do
            {
                Menu.MostrarMenuManipularConta();

                option = Input.PegarOpcao(1, 4);

                switch (option)
                {
                    case 1:
                        Depositar(cpf);
                        break;
                    case 2:
                        Sacar(cpf);
                        break;
                    case 3:
                        Transferir(cpf);
                        break;
                }

            } while (option != 4);
        }
        
        public static void Depositar(string cpf)
        {
            Cliente cliente = RepositorioCliente.clientes.Find(c => c.Cpf == cpf);
            
            Print.MostrarSaldo(cliente.Saldo);

            double deposito = Input.PegarValor(cliente.Saldo);

            cliente.Saldo += deposito;

            RepositorioCliente.AtualizarClientes();

            Print.DepositoSucesso(cliente.Saldo);
        }

        public static void Sacar(string cpf)
        {
            Cliente cliente = RepositorioCliente.clientes.Find(c => c.Cpf == cpf);

            if (cliente.Saldo == 0)
            {
                Print.SaldoZero();
                return;
            }

            double saque = Input.PegarValor(cliente.Saldo);

            while (saque > cliente.Saldo)
            {
                Print.ValorInvalido();
                saque = Input.PegarValor(cliente.Saldo);
            }

            cliente.Saldo -= saque;

            RepositorioCliente.AtualizarClientes();

            Print.SaqueSucesso(cliente.Saldo);
        }

        public static void Transferir(string cpf)
        {
            Cliente cliente1 = RepositorioCliente.clientes.Find(c => c.Cpf == cpf);

            if (cliente1.Saldo == 0)
            {
                Print.SaldoZero();
                return;
            }

            string cpfRecebedor;
            while (true)
            {
                cpfRecebedor = Input.PegarCpf("a receber a transferência");
                if (!RepositorioCliente.ValidarCpf(cpfRecebedor) || cpfRecebedor == cpf)
                {
                    Print.CpfNaoEcontrado();
                }
                else break;
            }

            Cliente cliente2 = RepositorioCliente.clientes.Find(c => c.Cpf == cpfRecebedor);

            Print.MostrarSaldo(cliente1.Saldo);

            double transferencia = Input.PegarValor(cliente1.Saldo);

            while (transferencia > cliente1.Saldo)
            {
                Print.ValorInvalido();
                transferencia = Input.PegarValor(cliente1.Saldo);
            }

            cliente1.Saldo -= transferencia;
            cliente2.Saldo += transferencia;

            RepositorioCliente.AtualizarClientes();

            Print.TransferenciaSucesso(cliente1.Saldo);
        }
    }
}
