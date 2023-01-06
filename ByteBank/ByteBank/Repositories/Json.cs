using ByteBank.Entities;
using ByteBank.Views;
using Newtonsoft.Json;

namespace ByteBank.Repositories
{
    public class Json
    {
        private static string path = "Dados.json";

        private static void CriarJson()
        {
            if (!File.Exists(path)) File.Create(path).Close();
        }

        internal static void Serializar(List<Cliente> clientes)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(sw, clientes);
                }
            }
            catch (Exception e)
            {
                Print.AplicarVermelhoErro($"\n\t{e.Message}");
            }
        }

        internal static List<Cliente> Desserializar()
        {
            CriarJson();
            List<Cliente> clientes = new List<Cliente>();
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string jsonString = sr.ReadToEnd();
                    clientes = JsonConvert.DeserializeObject<List<Cliente>>(jsonString);
                }
            }
            catch (Exception e)
            {
                Print.AplicarVermelhoErro($"\n\t{e.Message}");
            }
            return clientes;
        }
    }
}