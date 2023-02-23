using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Application.Interfaces;

using Domain.DTOs.Requests;

using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UsuarioController(IUsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _service.Read();
            if (response.Status is false)
                return BadRequest("Ocorreu um erro ao tentar obter todos os usuários");

            return Ok(response.Usuarios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var response = await _service.Read(id);
            if (response.Status is false)
                return BadRequest("Ocorreu um erro ao tentar obter o usuário");

            return Ok(response.Usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserDto dto)
        {
            var response = await _service.Create(dto);

            if (response)
                return NoContent();

            return BadRequest("Ocorreu um erro ao tentar criar o usuário");
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateUserDto dto)
        {
            var response = await _service.Update(dto);

            if (response.Status is false)
                return BadRequest("Ocorreu um erro ao tentar atualizar o usuário");

            return Ok(response.Usuario);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _service.Delete(id);

            if (response)
                return NoContent();

            return BadRequest("Ocorreu um erro ao tentar excluir o usuário");
        }
    }
}

