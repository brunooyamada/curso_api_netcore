using Domain.Dtos.Cep;
using Domain.Interfaces.Services.Cep;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Application.Controllers
{
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class CepsController : ControllerBase
    {
        public ICepService _service;

        public CepsController(ICepService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{id}", Name = "GetCepWithId")]
        public async Task<ActionResult> Get(long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _service.Get(id);
                if (result == null)
                {
                    return NotFound("Registro não encontrado");
                }
                return Ok(result);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("byCep/{cep}")]
        public async Task<ActionResult> Get(string cep)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _service.Get(cep);
                if (result == null)
                {
                    return NotFound("Registro não encontrado");
                }
                return Ok(result);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CepDtoCreate cep)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _service.Post(cep);
                if (result != null)
                {
                    return Created(new Uri(Url.Link("GetCepWithId", new { id = result.Id })), result);
                }
                else 
                {
                    return NotFound("Registro não encontrado");
                }
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] CepDtoUpdate cep)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _service.Put(cep);
                if (result == null)
                {
                    return NotFound("Registro não encontrado");
                }

                return Ok(result);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await _service.Delete(id));
            }
            catch(ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

    }
}
