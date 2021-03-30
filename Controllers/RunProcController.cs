
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
    [Route("api/[controller]")]  
    [ApiController]  
    public class RunProcController : ControllerBase  
    {  
        private readonly IDapper _dapper;  
        public RunProcController(IDapper dapper)  
        {  
            _dapper = dapper;  
        }  

        [HttpPost(nameof(Create))]  
        public async Task<int> Create(Parameters data)  
        {  
            var dbparams = new DynamicParameters();  
            dbparams.Add("Id", data.Id, DbType.Int32);  
            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[SP_Add_Artic le]"  
                , dbparams,  
                commandType: CommandType.StoredProcedure));  
            return result;  
        }  
        [HttpGet(nameof(GetById))]  
        public async Task<Parameters> GetById(int Id)  
        {  
            var result = await Task.FromResult(_dapper.Get<Parameters>($"Select * from [widgets] where Id = {Id}", null, commandType: CommandType.Text));  
            return result;  
        }  
        [HttpDelete(nameof(Delete))]  
        public async Task<int> Delete(int Id)  
        {  
            var result = await Task.FromResult(_dapper.Execute($"Delete [widgets] Where Id = {Id}", null, commandType: CommandType.Text));  
            return result;  
        }  
        [HttpGet(nameof(Count))]  
        public Task<int> Count(int num)  
        {  
            var totalcount = Task.FromResult(_dapper.Get<int>($"select COUNT(*) from [widgets] WHERE Age like '%{num}%'", null,  
                    commandType: CommandType.Text));  
            return totalcount;  
        }  
        [HttpPatch(nameof(Update))]  
        public Task<int> Update(Parameters data)  
        {  
            var dbPara = new DynamicParameters();  
            dbPara.Add("Id", data.Id);  
            dbPara.Add("Name", data.Name, DbType.String);  
  
            var updateArticle = Task.FromResult(_dapper.Update<int>("[dbo].[SP_Update_Article]",  
                            dbPara,  
                            commandType: CommandType.StoredProcedure));  
            return updateArticle;  
        }  

        [HttpPost(nameof(Test))]  
        public List<dynamic> Test( JObject jsonInput)  
        {  
            dynamic input = jsonInput; 
            string count = input.count;
            string value = input.value;
            var dbPara = new DynamicParameters();  
            dbPara.Add("count", count);  
            dbPara.Add("value", value, DbType.String);  
  
            var result = 
                _dapper.GetDbconnection().Query(
                    "[dbo].[TEST_PROC]",  
                    dbPara,  
                    commandType: CommandType.StoredProcedure
                    ).ToList();
            
            return result;
        }  
    }  
}  