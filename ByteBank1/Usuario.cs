using System.Data.SqlClient;
using System.Data.SQLite;
using System.Globalization;

namespace ByteBank1
{
    public class Usuario
    {
        private string cpf;
        public string Cpf
        {
            get { return cpf; }
            set
            {

                if (value.Length != 11 || value.Where(c => char.IsLetter(c)).Count() > 0)
                {
                    throw new Exception("\n\tCPF inválido. O CPF deve conter 11 caractéres (números apenas).");
                }
                else
                {
                    SQLiteConnection cn = new SQLiteConnection(Conexao.sqLiteConnection);
                    cn.Open();
                    var sql = "SELECT COUNT(*) FROM Usuarios WHERE cpf=@cpf";
                    SQLiteCommand cmd = new SQLiteCommand(sql, cn);
                    cmd.Parameters.AddWithValue("@cpf", value);
                    string conte=cmd.ExecuteScalar().ToString(); 
                    if (int.Parse(conte) > 0)
                    {
                        throw new Exception("\n\tCPF já cadastrado.");
                    }
                    else cpf = value;
                    cn.Clone();
                }

            }
        }
        private string nome;
        public string Nome
        {
            get { return nome; }
            set
            {
                if (value.Length > 60 || value.Length < 2 || value.Where(c => char.IsNumber(c)).Count() > 0)
                {
                    throw new Exception("\n\tNome inválido. O nome deve conter no mínimo 2 e no máximo 60 caractéres (letras e espaços apenas).");
                }
                else
                {
                    string text = value;
                    nome = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text);
                }
            }
        }
        public double Saldo { get; set; }
        private string senha;
        public string Senha
        {
            get { return senha; }
            set
            {
                if (value.Length < 5 || value.Length > 50)
                {
                    throw new Exception("\n\tSenha inválida. O senha deve conter no mínimo 5 e no máximo 50 caractéres.");
                }
                else senha = value;
            }
        }

        public Usuario(string cpf)
        {
            this.Cpf = cpf;
            this.Saldo = 0.00;
        }

        private static string ValidarCPF(string cpf)
        {
            if (cpf.Length != 11 || cpf.Where(c => char.IsLetter(c)).Count() > 0)
            {
                throw new Exception("\n\tCPF inválido. O CPF deve conter 11 caractéres (números apenas).");
            }
            else
            {
                SQLiteConnection cn = new SQLiteConnection(Conexao.sqLiteConnection);
                cn.Open();
                var sql = "SELECT COUNT(*) FROM Usuarios WHERE cpf=" + cpf;
                SQLiteCommand cmd = new SQLiteCommand(sql, cn);
                string conte = cmd.ExecuteScalar().ToString();
                if (int.Parse(conte) == 0)
                {
                    throw new Exception("\n\tNão há conta registrada para o CPF informado.");
                }
                else return cpf;
            }
        }

        private static void ValidarSenha(string cpf)
        {
            SQLiteConnection cn = new SQLiteConnection(Conexao.sqLiteConnection);
            cn.Open();
            var sql = "SELECT senha FROM Usuarios WHERE cpf=" + cpf;
            SQLiteCommand cmd = new SQLiteCommand(sql, cn);

            Console.Write("\n\tDigite a senha: ");
            string senha = Console.ReadLine();

            while (senha != cmd.ExecuteScalar().ToString())
            {
                Console.Write("\n\tSenha incorreta. Tente novamente: ");
                senha = Console.ReadLine();
            }
        }

        public void RegistrarNovoUsuario()
        {
            try
            {
                SQLiteConnection cn = new SQLiteConnection(Conexao.sqLiteConnection);
                cn.Open();
                var sql = "INSERT INTO Usuarios (cpf, titular, saldo, senha) VALUES (@cpf, @nome, @saldo, @senha)";
                SQLiteCommand cmd = new SQLiteCommand(sql, cn);

                cmd.Parameters.AddWithValue("@cpf", this.Cpf);
                cmd.Parameters.AddWithValue("@nome", this.Nome);
                cmd.Parameters.AddWithValue("@saldo", this.Saldo);
                cmd.Parameters.AddWithValue("@senha", this.Senha);

                cmd.ExecuteNonQuery();

                Console.WriteLine("\n\tUsuário inserido com sucesso.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"\n\tFalha ao registrar novo usuário: {e.Message}");
            }
        }

        public static void DeletarUsuario(string cpf)
        {
            cpf = ValidarCPF(cpf);
            try
            {
                ValidarSenha(cpf);

                SQLiteConnection cn = new SQLiteConnection(Conexao.sqLiteConnection);
                cn.Open();
                var sql = "DELETE FROM Usuarios WHERE cpf=@cpf";
                SQLiteCommand cmd = new SQLiteCommand(sql, cn);

                cmd.Parameters.AddWithValue("@cpf", cpf);

                cmd.ExecuteNonQuery();

                Console.WriteLine("\n\tConta deletada com sucesso.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"\n\tFalha ao deletar usuário: {e.Message}");
            }

        }

        public static void ListarContas()
        {
            try
            {
                SQLiteConnection cn = new SQLiteConnection(Conexao.sqLiteConnection);
                cn.Open();
                var sql = "SELECT titular,cpf,saldo FROM Usuarios";
                SQLiteCommand cmd = new SQLiteCommand(sql, cn);
                SQLiteDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Console.WriteLine($"\tNome: {dr["titular"].ToString()}");
                    Console.WriteLine($"\tCPF: {dr["cpf"].ToString().Insert(9, "-").Insert(6, ".").Insert(3, ".")}");
                    Console.WriteLine("\tSaldo: R${0:F2}\n", dr["saldo"]);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"\n\tFalha ao mostrar dados dos usuários: {e.Message}");
            }
        }

        public static void DetalharUsuario(string cpf)
        {
            cpf = ValidarCPF(cpf);
            try
            {
                SQLiteConnection cn = new SQLiteConnection(Conexao.sqLiteConnection);
                cn.Open();
                var sql = "SELECT titular,saldo,senha FROM Usuarios WHERE cpf=" + cpf;
                SQLiteCommand cmd = new SQLiteCommand(sql, cn);
                SQLiteDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    Console.WriteLine($"\n\tNome: {dr["titular"].ToString()}");
                    Console.WriteLine($"\tSaldo: R${dr["saldo"]:F2}");
                    Console.WriteLine($"\tSenha: {dr["senha"].ToString()}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"\n\tFalha ao mostrar dados do usuário: {e.Message}");
            }
        }

        public static void QuantiaArmazenada()
        {
            try
            {
                SQLiteConnection cn = new SQLiteConnection(Conexao.sqLiteConnection);
                cn.Open();
                var sql = "SELECT SUM(saldo) FROM Usuarios";
                SQLiteCommand cmd = new SQLiteCommand(sql, cn);

                Console.WriteLine($"\tA quantidade total armazenada no banco é R${cmd.ExecuteScalar():F2}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"\n\tFalha ao somar saldo dos usuários: {e.Message}");
            }
        }

        private static void ShowMenuManipulacao()
        {
            Console.WriteLine("\tEscolha uma opção para continuar: ");
            Console.WriteLine("\n\t1 - Depositar");
            Console.WriteLine("\t2 - Sacar");
            Console.WriteLine("\t3 - Transferir");
            Console.WriteLine("\t4 - Voltar para o menu principal");
            Console.Write("\n\tDigite a opção desejada: ");
        }

        public static void ManipularConta(string cpf)
        {
            cpf = ValidarCPF(cpf);
            ValidarSenha(cpf);
            int option = 10;
            do
            {
                Console.Clear();
                Console.WriteLine("\n");
                ShowMenuManipulacao();
                try
                {
                    option = int.Parse(Console.ReadLine());
                    if (option < 1 || option > 4)
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
                    case 1:
                        try
                        {
                            Console.Clear();
                            Console.WriteLine("\n");
                            Depositar(cpf);
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
                    case 2:
                        try
                        {
                            Console.Clear();
                            Console.WriteLine("\n");
                            Sacar(cpf);
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
                    case 3:
                        try
                        {
                            Console.Clear();
                            Console.WriteLine("\n");
                            Transferir(cpf);
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
                    case 4:
                        break;
                }
            } while (option != 4);
        }

        private static void Depositar(string cpf)
        {
            try
            {
                SQLiteConnection cn = new SQLiteConnection(Conexao.sqLiteConnection);
                cn.Open();
                var sql1 = "SELECT saldo FROM Usuarios WHERE cpf=" + cpf;
                SQLiteCommand cmd1 = new SQLiteCommand(sql1, cn);

                double saldoAtual = Convert.ToDouble(cmd1.ExecuteScalar());
                Console.WriteLine("\n\tSaldo Atual: R${0:F2}",saldoAtual);

                var sql2 = "UPDATE Usuarios SET saldo=@saldo WHERE cpf=" + cpf;
                SQLiteCommand cmd2 = new SQLiteCommand(sql2, cn);

                Console.Write("\n\tDigite o valor a ser depositado: ");
                double deposito = double.Parse(Console.ReadLine());

                while (deposito <= 0.0)
                {
                    Console.WriteLine("\n\tNão é possível depositar valor nulo ou negativo.");
                    Console.Write("\n\tDigite o valor novamente:");
                    deposito = double.Parse(Console.ReadLine());
                }

                saldoAtual += deposito;

                cmd2.Parameters.AddWithValue("@saldo", saldoAtual);

                cmd2.ExecuteNonQuery();

                Console.WriteLine("\n\tValor depositado com sucesso. Saldo: R${0:F2}",saldoAtual);
            }
            catch (Exception e)
            {
                if (e.Message.Contains("Input string was not in a correct format."))
                {
                    Console.WriteLine("\n\tValor inserido inválido.");
                }
            }
        }

        private static void Sacar(string cpf)
        {
            try
            {
                SQLiteConnection cn = new SQLiteConnection(Conexao.sqLiteConnection);
                cn.Open();
                var sql1 = "SELECT saldo FROM Usuarios WHERE cpf=" + cpf;
                SQLiteCommand cmd1 = new SQLiteCommand(sql1, cn);

                double saldoAtual = Convert.ToDouble(cmd1.ExecuteScalar());
                Console.WriteLine("\n\tSaldo Atual: R${0:F2}", saldoAtual);

                if (saldoAtual <= 0.0)
                {
                    throw new Exception("\n\tSeu saldo é nulo ou negativo, portanto não é possível realizar saque.");
                }

                var sql2 = "UPDATE Usuarios SET saldo=@saldo WHERE cpf=" + cpf;
                SQLiteCommand cmd2 = new SQLiteCommand(sql2, cn);

                Console.Write("\n\tDigite o valor a ser sacado: ");
                double saque = double.Parse(Console.ReadLine());

                while (saque <= 0.0)
                {
                    Console.Write("\n\tNão é possível sacar valor nulo ou negativo. Digite o valor novamente: ");
                    saque = double.Parse(Console.ReadLine());
                }

                saldoAtual -= saque;

                cmd2.Parameters.AddWithValue("@saldo", saldoAtual);

                cmd2.ExecuteNonQuery();

                Console.WriteLine("\n\tValor sacado com sucesso. Saldo: R${0:F2}",saldoAtual);
            }
            catch (Exception e)
            {
                if (e.Message.Contains("Input string was not in a correct format."))
                {
                    Console.WriteLine("\n\tValor inserido inválido.");
                }
                else
                {
                    Console.WriteLine($"{e.Message}");
                }
            }
        }

        private static void Transferir(string cpf)
        {
            try
            {
                SQLiteConnection cn = new SQLiteConnection(Conexao.sqLiteConnection);
                cn.Open();
                var sql1 = "SELECT saldo FROM Usuarios WHERE cpf=" + cpf;
                SQLiteCommand cmd1 = new SQLiteCommand(sql1, cn);

                double saldoPagador = Convert.ToDouble(cmd1.ExecuteScalar());
                Console.WriteLine("\n\tSaldo Atual: R${0:F2}", saldoPagador);

                if (saldoPagador <= 0.0)
                {
                    throw new Exception("\n\tSeu saldo é nulo ou negativo, portanto não é possível realizar transferência.");
                }

                var sql2 = "UPDATE Usuarios SET saldo=@saldo WHERE cpf=" + cpf;
                SQLiteCommand cmd2 = new SQLiteCommand(sql2, cn);

                Console.Write("\n\tDigite o valor a ser transferido: ");
                double valor = double.Parse(Console.ReadLine());

                while (valor<=0.0)
                {
                    Console.Write("\n\tNão é possível transferir valor nulo ou negativo. Digite o valor novamente: ");
                    valor = double.Parse(Console.ReadLine());
                }

                saldoPagador -= valor;

                cmd2.Parameters.AddWithValue("@saldo", saldoPagador);

                cmd2.ExecuteNonQuery();

                Console.Write("\n\tDigite o CPF do titular que receberá a transferência: ");
                string cpfDestino = Console.ReadLine();

                while (cpfDestino == cpf)
                {
                    Console.WriteLine("\n\tNão é possível transferir para si mesmo.");
                    Console.Write("\n\tDigite o CPF do titular que receberá a transferência novamente: ");
                    cpfDestino = Console.ReadLine();
                }

                cpfDestino = ValidarCPF(cpfDestino);

                var sql3 = "SELECT saldo FROM Usuarios WHERE cpf=" + cpfDestino;
                SQLiteCommand cmd3 = new SQLiteCommand(sql3, cn);

                double saldoRecebedor = Convert.ToDouble(cmd3.ExecuteScalar());

                var sql4 = "UPDATE Usuarios SET saldo=@saldo WHERE cpf=" + cpfDestino;
                SQLiteCommand cmd4 = new SQLiteCommand(sql4, cn);

                saldoRecebedor += valor;

                cmd4.Parameters.AddWithValue("@saldo", saldoRecebedor);

                cmd4.ExecuteNonQuery();

                Console.WriteLine("\n\tValor transferido com sucesso. Saldo: R${0:F2}",saldoPagador);
            }
            catch (Exception e)
            {
                if (e.Message.Contains("Input string was not in a correct format."))
                {
                    Console.WriteLine("\n\tValor inserido inválido.");
                }
                else
                {
                    Console.WriteLine($"{e.Message}");
                }
            }
        }
    }
}
