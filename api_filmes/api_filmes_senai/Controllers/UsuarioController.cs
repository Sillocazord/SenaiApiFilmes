using api_filmes_senai.Domains;
using api_filmes_senai.Interfaces;
using api_filmes_senai.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_filmes_senai.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        /// <summary>
        /// Endpoint usada pra Cadastrar Usuario.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns>Cadastrar Usuario</returns>
        /// 
        [Authorize]
        [HttpPost]
        public IActionResult Post(Usuario usuario)
        {
            try
            {
                _usuarioRepository.Cadastrar(usuario);

                return StatusCode(201, usuario);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        /// <summary>
        /// Endpoint para Buscar Usuario por ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Buscar Usuario por ID</returns>
        [HttpGet("{id}")]

        public IActionResult GetById(Guid id)
        {
            try
            {
                Usuario usuarioBuscado = _usuarioRepository.BuscarPorID(id);
                if (usuarioBuscado != null)
                {
                    return Ok(usuarioBuscado);
                }

                return null!;

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
         
        }

    }
}