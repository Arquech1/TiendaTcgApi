using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaTcgApi.Data;
using TiendaTcgApi.Entidades;

namespace TiendaTcgApi.Controllers
{
    [ApiController]
    [Route("productos")]
    public class ProductosController : ControllerBase
    {
        private readonly AppDbContext context;

        public ProductosController(AppDbContext context)
        {
            this.context = context;
        }


        [HttpGet]
        public async Task<IEnumerable<Producto>> Get()
        {
            var productos = await context.Productos.ToListAsync();

            return productos;

        }

        [HttpGet("{id:int}", Name = "ObtenerProducto")]
        public async Task<ActionResult<Producto>> GetId(int id)
        {
            var producto = await context.Productos.FirstOrDefaultAsync(x => x.id == id);

            if (producto == null)
            {
                return NotFound();
            }

            return producto;
        }

        [HttpPost]
        public async Task<ActionResult<Producto>> Post(Producto producto)
        {
            context.Add(producto);
            await context.SaveChangesAsync();
            return CreatedAtRoute("ObtenerProducto", new { id = producto.id }, producto);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, Producto producto)
        {


            if (producto.id != id)
            {
                return BadRequest();
            }

            context.Update(producto);
            await context.SaveChangesAsync();
            return NoContent();



        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Productos.Where(x => x.id == id).ExecuteDeleteAsync();
            if (existe == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
