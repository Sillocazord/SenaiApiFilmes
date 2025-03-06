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
    public class FilmeController : ControllerBase
    {
        private readonly IFilmeRepository _filmeRepository;
        public FilmeController(IFilmeRepository filmeRepositoty)
        {
            _filmeRepository = filmeRepositoty;
        }

        /// <summary>
        /// Endpoint para Listar Filmes.
        /// </summary>
        /// <returns>Listar Filmes</returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<Filme> listaDeFilmes = _filmeRepository.Listar();
                return Ok(listaDeFilmes);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);

            }
        }

        /// <summary>
        /// Endpoint para Cadastrar Filmes.
        /// </summary>
        /// <param name="novoFilme"></param>
        /// <returns>Cadastrar Filmes</returns>
        [Authorize]
        [HttpPost]
        public IActionResult Post(Filme novoFilme)
        {
            try
            {
                _filmeRepository.Cadastrar(novoFilme);
                return Created();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Endpoint para Buscar Filme por ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Buscar Filme por ID</returns>
        [HttpGet("BuscarPorId/{id}")]

        public IActionResult GetById(Guid id)
        {
            try
            {
                Filme filmeBuscado = _filmeRepository.BuscarPorId(id);

                return Ok(filmeBuscado);

            }
            catch (Exception)
            {

                return BadRequest();    
            }

        }

        /// <summary>
        /// Endpoint para Deletar Filmes pelo ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Deletar Filmes</returns>
        [Authorize]
        [HttpDelete("{id}")]

        public IActionResult Delete(Guid id)
        {
            try
            {
                _filmeRepository.Deletar(id);
                return NoContent();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Endpoint para Atualizar Filmes pelo ID.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="novoFilme"></param>
        /// <returns>Atualizar Filmes.</returns>
        [Authorize]
        [HttpPut("{id}")]

        public IActionResult Put(Guid id, Filme novoFilme)
        {
            try
            {
                _filmeRepository.Atualizar(id, novoFilme);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);

            }

        }

        /// <summary>
        /// Endpoint usado para Listar Filmes por Gênero.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Listar Filmes por Gênero</returns>
        [HttpGet("ListarPorGenero/{id}")]

        public IActionResult GetByGenero(Guid id)
        {
            try
            {
               List<Filme> listaDeFilmePorGenero = _filmeRepository.ListarPorGenero(id);
                return Ok(listaDeFilmePorGenero);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
            
        }
    }

}
