using api_filmes_senai.Domains;
using api_filmes_senai.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_filmes_senai.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneroController : ControllerBase
    {
        private readonly IGeneroRepository _generoRepository;

        public GeneroController(IGeneroRepository generoRepository)
        {
            _generoRepository = generoRepository;
        }

        /// <summary>
        /// Endpoint para Listar os Gêneros.
        /// </summary>
        /// <returns>Listar Gêneros</returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_generoRepository.Listar());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);

            }
        }
        
        /// <summary>
        /// Endpoint para Cadastrar um Gênero.
        /// </summary>
        /// <param name="novoGenero"></param>
        /// <returns>Cadastrar Gênero</returns>
        [Authorize]
        [HttpPost]
        public IActionResult Post(Genero novoGenero)
        {
            try
            {
                _generoRepository.Cadastrar(novoGenero);
                return Created();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Endpoint para buscar um Gênero pelo seu ID.
        /// </summary>
        /// <param name="id">id do Gênero buscado</param>
        /// <returns>Gênero Buscado</returns>
        [HttpGet("BuscarPorId/{id}")]

        public IActionResult GetById(Guid id)
        {
            try
            {
                Genero generoBuscado = _generoRepository.BuscarPorId(id);

                return Ok(generoBuscado);

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }

        /// <summary>
        /// Endpoint para Deletar um Gênero pelo seu ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Deletar Gênero</returns>
        [Authorize]
        [HttpDelete("{id}")]

        public IActionResult Delete(Guid id) {
            try
            {
                _generoRepository.Deletar(id);
                return NoContent();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Endpoint para Atualizar o Gênero pelo ID.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="genero"></param>
        /// <returns>Atualizar Gênero</returns>
        [Authorize]
        [HttpPut("{id}")]

        public IActionResult Put(Guid id, Genero genero)
        {
            try
            {
                _generoRepository.Atualizar(id, genero);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                
            }

        }
    }
}
