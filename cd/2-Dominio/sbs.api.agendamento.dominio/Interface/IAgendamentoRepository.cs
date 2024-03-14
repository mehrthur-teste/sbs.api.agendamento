using sbs.api.agendamento.dominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbs.api.agendamento.dominio.Interface
{
    public interface IAgendamentoRepository
    {
        Task<IEnumerable<Agendamento>> GetItemsAsync(string query);
        Task<Agendamento> GetItemAsync(string id);
        Task AddItemAsync(Agendamento item);
        Task UpdateItemAsync(string id, Agendamento item);
        Task DeleteItemAsync(string id);
    }
}
