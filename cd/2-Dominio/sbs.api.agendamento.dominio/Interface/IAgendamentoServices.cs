using sbs.api.agendamento.dominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbs.api.agendamento.dominio.Interface
{
    public interface IAgendamentoServices
    {
         Task<Agendamento> CreateAsync(Agendamento item);
         Task<Agendamento> GetItemAsync(string id);
         Task<IEnumerable<Agendamento>> GetItemAllAsync();
    }
}
