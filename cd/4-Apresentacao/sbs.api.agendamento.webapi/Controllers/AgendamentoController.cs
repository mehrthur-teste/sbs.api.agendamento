using Microsoft.AspNetCore.Mvc;
using sbs.api.agendamento.dominio.Interface;
using sbs.api.agendamento.dominio.Models;

namespace sbs.api.agendamento.webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AgendamentoController : Controller
    {
        private readonly IAgendamentoServices _agendamentoServices;
        public AgendamentoController(IAgendamentoServices agendamentoServices)
        {
            _agendamentoServices = agendamentoServices;
        }

        [HttpPost("criar-agendamento")]
        public async Task<IActionResult> CreateSchedulingAsync(Agendamento item)
        {

            var result = await _agendamentoServices.CreateAsync(item);
            if(result != null)
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Agendamento criado com sucesso!",
                    Data = result
                }); 
            else
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Agendamento não foi criado com sucesso!",
                    Data = result
                });
        }


        [HttpGet("listar-agendamentos")]
        public async Task<IActionResult> GetAgendamentos() 
        {
            var result = await _agendamentoServices.GetItemAllAsync();
            if (result != null)
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Agendamentos Listados com sucesso",
                    Data = result
                });
            else
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Agendamentos não Listados com sucesso!",
                    Data = result
                });
        }
    }
}
