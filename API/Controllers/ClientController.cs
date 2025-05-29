using Application.Interfaces;
using Application.Requests;
using Application.Validators;
using Microsoft.AspNetCore.Mvc;

namespace ModuloCadastro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IGetClientByIdHandler _getClientByIdHandler;
        private readonly IUpdateClientHandler _updateClientHandler;
        private readonly ClientValidator _validator;
        private readonly ICreateClientHandler _createClientHandler;

        public ClientController(ClientValidator validator, ICreateClientHandler createClientHandler, IGetClientByIdHandler getClientByIdHandler, IUpdateClientHandler updateClientHandler)
        {
            _validator = validator;
            _createClientHandler = createClientHandler;
            _getClientByIdHandler = getClientByIdHandler;
            _updateClientHandler = updateClientHandler;
        }

        [HttpPost]
        public IActionResult CreateClient(ClientRequest request)
        {
            if (!_validator.Validate(request, out var errorMessage))
                return BadRequest(new { Message = errorMessage });
            
            var response = _createClientHandler.Handle(request);
            return Ok(response);
        }
        
        [HttpGet("{id}")]
        public IActionResult GetClientById(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest(new { Message = "O Id do cliente é obrigatório." });
            var response = _getClientByIdHandler.Handle(id);
            return Ok(response);
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateClient(Guid id, UpdateClientRequest request)
        {
            try
            {
                _updateClientHandler.Handle(id, request);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}