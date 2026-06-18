using System;

namespace Application.UseCases.EVENT.Queries
{
    public class GetAllEventQuery 
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}