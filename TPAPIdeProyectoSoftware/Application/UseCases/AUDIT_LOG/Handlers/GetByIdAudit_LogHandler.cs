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
    public class GetByIdAudit_LogHandler : IGetByIdAudit_LogHandler
    {
        private readonly IGetByIdAudit_LogQuery _query;

        public GetByIdAudit_LogHandler(IGetByIdAudit_LogQuery query)
        {
            _query = query;
        }
        public async Task<(Audit_LogResponseDto Audit_Logs, string message)> Handle(Guid id)
        {
            var user = await _query.GetById(id);

            if (user == null)
                return (new Audit_LogResponseDto(), "No hay Audit_Log registrados");

            return (user, "OK");
        }

    }
}
