
using System;  
using System.Collections.Generic;  
using System.Data;  
using System.Linq;  
using System.Threading.Tasks;  
using Dapper;  
using RunProcApi.Models;  
using RunProcApi.Services;  
using Microsoft.AspNetCore.Http;  
using Microsoft.AspNetCore.Mvc;  
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
  
namespace RunProcApi.Controllers  
{  
    [Route("sql/api/{procName}")]  
    [ApiController]  
    public class RunProcController : ControllerBase  
    {  
        private readonly IDapper _dapper;  
        public RunProcController(IDapper dapper)  
        {  
            _dapper = dapper;  
        }  

        [HttpPost("Query")]
        public List<dynamic> Query( JObject jsonInput, string procName)  
        {  
            var dbPara = new DynamicParameters();  
            string name = "";
            string value = "";
            foreach (JProperty property in jsonInput.Properties())
            {
                name = property.Name;
                 value = (string)property.Value;
                dbPara.Add(name, value, DbType.String);  
            }

            List<dynamic> result = _dapper.GetDbconnection().Query(
                "[dbo].[" + procName + "]",  
                dbPara,  
                commandType: CommandType.StoredProcedure
                ).ToList();
            
            return result;
        }  
    }  
}  