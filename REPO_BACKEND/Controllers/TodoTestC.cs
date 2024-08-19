using backnc.Data.Context;
using backnc.Data.POCOEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace backnc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoTestC : ControllerBase
    {
        private readonly AppDbContext _context;
        public TodoTestC(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("create-todo")]
        public async Task<IActionResult> createtest([FromBody]TodoTest todo)
        {               
            _context.TodoTests.Add(todo);
            await _context.SaveChangesAsync();
            return Ok(todo.Id);
        }

        [HttpGet("getall-todo-sin-autorizacion")]
        public  IActionResult getalltodo1()
        {
            return Ok(_context.TodoTests.ToList());
        }

        [HttpGet("getall-todo-autenticacion-requerida")]
        [Authorize]
        public IActionResult getalltodo2()
        {
            return Ok(_context.TodoTests.ToList());
        }

        [Authorize(Roles = ("Cliente"))]
        [HttpGet("getall-todo-autenticacion-rol-test")]
        public IActionResult getalltodo3()
        {
            return Ok(_context.TodoTests.ToList());
        }

        [Authorize(Roles = ("Admin"))]
        [HttpGet("getall-todo-autenticacion-rol-admin")]
        public IActionResult getalltodo4()
        {
            return Ok(_context.TodoTests.ToList());
        }
    }
}
