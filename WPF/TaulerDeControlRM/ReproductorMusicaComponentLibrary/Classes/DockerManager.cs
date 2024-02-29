using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorMusicaComponentLibrary.Classes
{
    internal class DockerManager
    {
        public static void DescarregarMicrosoftSqlServer()
        {
            ExecutarComandaDocker("pull mcr.microsoft.com/mssql/server:2022-latest");
        }

        public static void DescarregarMySqlServer()
        {
            ExecutarComandaDocker("");
        }

        public static void DescarregarPostgresSqlServer()
        {
            ExecutarComandaDocker("");
        }

        public static void ExecutarContenidorMicrosoftSqlServer(string nameContenidor, string saPassword)
        {
            // Ajusta la cadena de conexión según tus necesidades
            string cadenaConexio = $"-e \"ACCEPT_EULA=Y\" -e \"SA_PASSWORD={saPassword}\"";

            ExecutarComandaDocker($"run --name {nameContenidor} {cadenaConexio} -d mcr.microsoft.com/mssql/server:2022-latest");
        }



        private static void ExecutarComandaDocker(string comanda)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "docker",
                    Arguments = comanda,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process proces = new Process { StartInfo = startInfo })
                {
                    proces.Start();

                    string resultat = proces.StandardOutput.ReadToEnd();
                    Console.WriteLine(resultat);

                    proces.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al executar la comanda Docker: {ex.Message}");
            }
        }
    }
}
