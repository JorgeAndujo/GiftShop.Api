using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Shop_API.Models;

namespace Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public CarritoController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("GetCarrito")]
        public async Task<IEnumerable<Carrito>> GetCarrito()
        {
            return await _dbContext.Carrito.ToListAsync();
        }

        [HttpPost]
        [Route("AgregarCarrito")]
        public async Task<Carrito> AgregarCarrito(Carrito objCarrito)
        {
            _dbContext.Carrito.Add(objCarrito);
            await _dbContext.SaveChangesAsync();
            return objCarrito;
        }

        [HttpDelete]
        [Route("DeleteCarrito/{id}")]
        public bool DeleteCarrito(int id)
        {
            bool a = false;
            var carrito = _dbContext.Carrito.Find(id);
            if (carrito != null)
            {
                a = true;
                _dbContext.Entry(carrito).State = EntityState.Deleted;
                _dbContext.SaveChanges();
            }
            else
            {
                a = false;
            }
            return a;
        }

        [HttpDelete("{UsuarioID}")]
        //[Route("CleanCarrito/{UsuarioID}")]
        public async Task<IActionResult> CleanCarrito(int UsuarioID)
        {
            try {
                var cart = _dbContext.Carrito.Where((x)=>x.UsuarioID==UsuarioID).ToList();
                if (cart != null)
                {
                    _dbContext.Carrito.RemoveRange(cart);
                    _dbContext.SaveChanges();
                    return Ok(UsuarioID);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch(Exception error) {
                return NotFound(error.Message);
            }
        }
    }
}
