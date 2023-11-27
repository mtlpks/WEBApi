using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEBApi.Data;
using WEBApi.Models;

namespace WEBApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClienteController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Cliente>> CriaCliente(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();


            return Ok(cliente);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> RetornaCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound("Cliente nao encontrado.");
            }

            return Ok(cliente);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> RetornaClientes()
        {
            return await _context.Clientes.ToListAsync();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizaCliente(int id, Cliente cliente)
        {
            Console.WriteLine(id);
            Console.WriteLine(id.GetType());
            Console.WriteLine(cliente.Id);
            Console.WriteLine(cliente.Id.GetType());
            if (id != cliente.Id)
            {
                return BadRequest("IDs não coincidem.");
            }


            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) 
            {
                if(!_context.ClienteExists(id))
                {
                    return NotFound($"Cliente com ID {id} nao encontrado.");
                }
                else
                {
                    throw;
                }
            }

            return Ok("Cliente atualizado.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletaCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null) 
            {
            return NotFound("Cliente nao encontrado.");
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return Ok("Cliente deletado.");
        }
    }
}
