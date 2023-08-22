using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop_API.Models;

namespace Shop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public ProductosController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("GetProductos")]
        public async Task<IEnumerable<Productos>> GetProductos()
        {
            return await _dbContext.Productos.ToListAsync();
        }

        [HttpPost]
        [Route("AddProductos")]
        public async Task<Productos> AddProductos(Productos objProductos)
        {
            _dbContext.Productos.Add(objProductos);
            await _dbContext.SaveChangesAsync();
            return objProductos;
        }

        [HttpPatch]
        [Route("UpdateProductos/{id}")]
        public async Task<Productos> UpdateProductos(Productos objProductos)
        {
            _dbContext.Entry(objProductos).State=EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return objProductos;
        }

        [HttpDelete]
        [Route("DeleteProductos/{id}")]
        public bool DeleteProductos(int id)
        {
            bool a = false;
            var producto = _dbContext.Productos.Find(id);
            if (producto != null)
            {
                a = true;
                _dbContext.Entry(producto).State=EntityState.Deleted;
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
