using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dymj.ReproductorMusica.API_SQL.Model;
using dymj.ReproductorMusica.API_SQL.Services;
using dymj.ReproductorMusica.API_SQL.Data;

namespace dymj.ReproductorMusica.API_SQL.Controller
{
    [Route("api/")]
    [ApiController]
    public class ConfigDocker : ControllerBase
    {
        public string Database = "DefaultConnection";

        [HttpGet("database/{database}")]
        public async Task<ActionResult<string>> GetDatabase(string database)
        {
            if (database == "PostgreSQL")
            {
                Database = "PostgresSQLConnection";
            }
            else if (database == "MySQL")
            {
                Database = "MySQLConnection";
            }
            else if (database == "Microsoft SQL Server")
            {
                Database = "DefaultConnection";
            }
            return Database;

        }
    }
}