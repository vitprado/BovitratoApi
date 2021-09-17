using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BovitratoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LotesController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            string connectionString = "Data Source=localhost;Initial Catalog=Bovitrato;Integrated Security=True";

            string sql =
                "select TOP 10 [LoteNumero] as codigo " +
                ", LoteSexo as sexo " +
                ", [LoteDataEntrada] as dataEntrada " +
                ", [LoteDataSaida] as dataSaida " +
                ", [LotePesoCarcaca] as pesoCarcaca " +
                ", [LotePesoBrutoCarcaca] as pesoBrutoCarcaca " +
                ", [LoteManejoId] " +
                ", [LoteGMD] as gmd " +
                ", [LoteDataAbate] as dataAbate " +
                ", [LoteDiasCochoEsperado] as dcEsperado " +
                ", [LoteRendEsperado] as rendEsperado " +
                "from Lote " +
                "FOR JSON PATH";

            string retorno = "";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                try
                {
                    conn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            retorno += dr[0].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return retorno;
        }
    }
}


