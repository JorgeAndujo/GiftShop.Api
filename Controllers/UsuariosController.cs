using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop_API.Models;

namespace Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public UsuariosController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("GetUsuarios")]
        public async Task<IEnumerable<Usuarios>> GetUsuarios()
        {
            return await _dbContext.Usuarios.ToListAsync();
        }

        [HttpPost]
        [Route("AddUsuarios")]
        public async Task<Usuarios> AddUsuarios(Usuarios objUsuarios)
        {
            _dbContext.Add(objUsuarios);
            await _dbContext.SaveChangesAsync();
            return objUsuarios;
        }

        [HttpPatch]
        [Route("UpdateUsuarios/{id}")]
        public async Task<Usuarios> UpdateUsuarios(Usuarios objUsuarios)
        {
            _dbContext.Entry(objUsuarios).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return objUsuarios;
        }

        [HttpDelete]
        [Route("DeleteUsuarios/{id}")]
        public bool DeleteUsuarios(int id)
        {
            bool a = false;
            var usuario = _dbContext.Usuarios.Find(id);
            if (usuario != null)
            {
                a = true;
                _dbContext.Entry(usuario).State = EntityState.Deleted;
                _dbContext.SaveChanges();
            }
            else
            {
                a = false;
            }
            return a;
        }
    }
}
