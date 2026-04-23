using Application.DTOs;
using Application.DTOs.User;
using Application.Interfaces.Command.User;
using Application.Interfaces.Handlers.User;
using Application.Interfaces.Queries.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.USER.Handlers
{
    public class GetAllAudit_LogHandler : IGetAllAudit_LogHandler
    {
        private readonly IGetAllAudit_LogQuery _query;

        public GetAllAudit_LogHandler(IGetAllAudit_LogQuery query)
        {
            _query = query;
        }
        public async Task<(List<Audit_LogResponseDto> Audit_Logs, string message)> Handle()
        {
            var Audit_Logs = await _query.GetAll();

            if (Audit_Logs == null || Audit_Logs.Count == 0)
                return (new List<Audit_LogResponseDto>(), "No hay Audit_Log registrados");

            return (Audit_Logs, "OK");
        }
    }
}
