using ByteBank1.Entities;
using ByteBank1.Services;
using ByteBank1.Views;

namespace ByteBank1.Repositories
{
    public class RepositorioCliente 
    {
        public static List<Cliente> clientes = Json.Desserializar();

        public static bool ValidarCpf(string cpf)
        {
            if (clientes == null)
            {
                clientes = new List<Cliente>();
                return false;
            }
            if (clientes.Exists(cliente => cliente.Cpf == cpf)) return true;
            else return false;
        }

        public static bool ValidarSenha(string cpf, string senha)
        {
            if (clientes.Find(cliente => cliente.Cpf == cpf).Senha != senha) return false;
            else return true;
        }

        public static void AdicionarCliente()
        {
            Cliente novoCliente = new Cliente();
            while (true)
            {
                try
                {
                    novoCliente.Cpf = Input.PegarCpf();
                    break;
                }
                catch (Exception e)
                {
                    Print.AplicarVermelhoErro($"\n\t{e.Message}");
                    Input.TentarNovamente();
                }
            }
            while (true)
            {
                try
                {
                    novoCliente.Nome = Input.PegarNome();
                    break;
                }
                catch (Exception e)
                {
                    Print.AplicarVermelhoErro($"\n\t{e.Message}");
                    Input.TentarNovamente();
                }
            }
            while (true)
            {
                try
                {
                    novoCliente.Senha = Input.PegarSenha();
                    break;
                }
                catch (Exception e)
                {
                    Print.AplicarVermelhoErro($"\n\t{e.Message}");
                    Input.TentarNovamente();
                }
            }
            clientes.Add(novoCliente);
            Json.Serializar(clientes);
            Print.ClienteAdicionadoSucesso();
            Input.AcessarMenu();
        }

        public static void DeletarCliente()
        {
            string cpf, senha;
            while (true)
            {
                cpf = Input.PegarCpf();
                if (!ValidarCpf(cpf))
                {
                    Print.CpfNaoEcontrado();
                }
                else break;
            }
            while (true)
            {
                senha = Input.PegarSenha();
                if (!ValidarSenha(cpf, senha))
                {
                    Print.SenhaIncorreta();
                }
                else break;
            }
            clientes.RemoveAll(cliente => cliente.Cpf == cpf);
            Json.Serializar(clientes);
            Print.ClienteRemovidoSucesso();
            Input.AcessarMenu();
        }

        public static void AtualizarClientes()
        {
            Json.Serializar(clientes);
        }
    }
}
