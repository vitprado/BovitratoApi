using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace BovitratoApi.Controllers
{
    [ApiController]
    [Route("api/lotes")]
    public class LotesController : ControllerBase
    {
        [HttpGet]
        public ContentResult GetLotes()
        {

            string sql =
                "select TOP 1000 [LoteNumeroSequencia] as codigo " +
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
                "ORDER BY codigo ASC " +
                "FOR JSON PATH, INCLUDE_NULL_VALUES";

            StringBuilder jsonResult = new();

            using (SqlConnection conn = new(Global.ConnectionString))
            {
                SqlCommand cmd = new(sql, conn);
                try
                {
                    conn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (!dr.HasRows)
                        {
                            jsonResult.Append("[]");
                        }
                        else
                        {
                            while (dr.Read())
                            {
                                jsonResult.Append(dr.GetValue(0).ToString());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return Content(jsonResult.ToString(), "application/json");;
        }

        [HttpGet("{id}")]
        public ContentResult GetLoteId(string id)
        {

            string sql =
                "SELECT [LoteNumeroSequencia] as codigo " +
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
                "FROM Lote " +
                "WHERE LoteNumeroSequencia = @id " +
                "FOR JSON PATH, INCLUDE_NULL_VALUES";

            SqlParameter param = new();
            param.ParameterName = "@id";
            param.Value = id.ToString();

            StringBuilder jsonResult = new();

            using (SqlConnection conn = new(Global.ConnectionString))
            {
                SqlCommand cmd = new(sql, conn);
                cmd.Parameters.Add(param);

                try
                {
                    conn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (!dr.HasRows)
                        {
                            jsonResult.Append("[]");
                        }
                        else
                        {
                            while (dr.Read())
                            {
                                jsonResult.Append(dr.GetValue(0).ToString());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return Content(jsonResult.ToString(), "application/json"); ;
        }
    }

}


