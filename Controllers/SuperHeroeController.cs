using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace DapperCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroeController : ControllerBase
    {
        private readonly IConfiguration _config;

        public SuperHeroeController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHeroe>>> GetAllSuperHeroes()
        {
            try
            {
                using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
                var heroes = await connection.QueryAsync<SuperHeroe>("select * from heroes");
                return Ok(heroes);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{heroId}")]
        public async Task<ActionResult<List<SuperHeroe>>> GetSuperHeroe(int heroId)
        {
            try
            {
                using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
                var hero = await connection.QueryFirstOrDefaultAsync<SuperHeroe>("select * from heroes where id = @Id",
                    new { Id = heroId });
                return Ok(hero);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHeroe>>> CreateSuperHeroe(SuperHeroe heroe)
        {
            try
            {
                using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
                await connection.ExecuteAsync("insert into heroes (heroeName, firstName, lastName, city) values (@heroeName, @firstName, @lastName, @city)", heroe);
                return Ok(await SelectAllHeroes(connection));
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static async Task<IEnumerable<SuperHeroe>> SelectAllHeroes(SqlConnection connection)
        {
            return await connection.QueryAsync<SuperHeroe>("select * from heroes");
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHeroe>>> UpdateSuperHeroe(SuperHeroe heroe)
        {
            try
            {
                using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
                await connection.ExecuteAsync("update heroes set heroeName=@heroeName, firstName=@firstName, lastName=@lastName, city=@city, registrationDate=@registrationDate where id=@id", heroe);
                return Ok(await SelectAllHeroes(connection));
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("{heroId}")]
        public async Task<ActionResult<List<SuperHeroe>>> DeleteSuperHeroe(int heroId)
        {
            try
            {
                using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
                await connection.ExecuteAsync("delete from heroes where id = @id", new { Id = heroId });
                return Ok(await SelectAllHeroes(connection));
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
