using sbs.api.agendamento.dominio.Interface;
using sbs.api.agendamento.dominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbs.api.agendamento.aplicacao.Services
{
    public class AgendamentoServices : IAgendamentoServices
    {
        private readonly IAgendamentoRepository _agendamentoRepository;
        public AgendamentoServices(IAgendamentoRepository agendamentoRepository)
        {
            _agendamentoRepository = agendamentoRepository;
        }

        public async Task<Agendamento> CreateAsync(Agendamento item)
        {
           item.id = Guid.NewGuid().ToString();
           await _agendamentoRepository.AddItemAsync(item);
           return (Agendamento)await _agendamentoRepository.GetItemAsync(item.id);
        }

        public async Task<Agendamento> GetItemAsync(string id) 
        {
            return (Agendamento)await _agendamentoRepository.GetItemAsync(id);
        }

        public async Task<IEnumerable<Agendamento>> GetItemAllAsync()
        {
            return (IEnumerable<Agendamento>)await _agendamentoRepository.GetItemsAsync("SELECT * FROM c");
        }
    }
}
