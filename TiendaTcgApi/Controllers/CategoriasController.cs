using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaTcgApi.Data;
using TiendaTcgApi.Entidades;

namespace TiendaTcgApi.Controllers
{
    [ApiController]
    [Route("categorias")]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext context;

        public CategoriasController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Categoria>> Get()
        {
            var categorias = await context.Categorias.ToListAsync();
            return categorias;
        }

        [HttpGet("{id:int}", Name = "ObtenerCategoria")]
        public async Task<ActionResult<Categoria>> GetId(int id)
        {
            var categoria = await context.Categorias
                .Include(x => x.productos)
                .FirstOrDefaultAsync(x => x.id == id);
            if (categoria == null)
            {
                return NotFound();
            }
            return categoria;
        }

        [HttpPost]

        public async Task<ActionResult<Categoria>> Post(Categoria categoria)
        {
            context.Add(categoria);
            await context.SaveChangesAsync();
            return CreatedAtRoute("ObtenerCategoria", new { id = categoria.id }, categoria);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Categoria>> Put(int id, Categoria categoria)
        {
            if (categoria.id != id)
            {
                return BadRequest();
            }
            context.Update(categoria);
            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Categorias.Where(x => x.id == id).ExecuteDeleteAsync();
            if (existe == 0)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}
