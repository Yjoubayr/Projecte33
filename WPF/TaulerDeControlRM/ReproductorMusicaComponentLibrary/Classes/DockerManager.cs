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
        public static void DescargarSqlServer()
        {
            //EjecutarComandoDocker("pull mcr.microsoft.com/mssql/server:2022-latest");
        }

        public static void EjecutarSqlServerContenedor(string nombreContenedor, string saPassword)
        {
            // Ajusta la cadena de conexión según tus necesidades
            string cadenaConexion = $"-e \"ACCEPT_EULA=Y\" -e \"SA_PASSWORD={saPassword}\"";

            //EjecutarComandoDocker($"run --name {nombreContenedor} {cadenaConexion} -d mcr.microsoft.com/mssql/server:2022-latest");
        }
    }
}
