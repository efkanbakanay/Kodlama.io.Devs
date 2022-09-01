using Application.Features.Codings.Commands.CreateCoding;
using Application.Features.Codings.Dtos;
using Application.Features.Codings.Models;
using Application.Features.Codings.Queries.GetByIdCoding;
using Application.Features.Codings.Queries.GetListCoding;
using Core.Application.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodingsController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateCodingCommand createCodingCommand)
        {
            CreatedCodingDto result = await Mediator.Send(createCodingCommand);
            return Created("", result);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListCodingQuery getListCodingQuery = new() { PageRequest = pageRequest };
            CodingListModel result = await Mediator.Send(getListCodingQuery);
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdCodingQuery getByIdIdCodingQuery)
        {
            CodingGetByIdDto codingGetByIdDto = await Mediator.Send(getByIdIdCodingQuery);
            return Ok(codingGetByIdDto);
        }
    }
}
