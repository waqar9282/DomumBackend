using DomumBackend.Domain.Repositories.Query.Base;
using DomumBackend.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomumBackend.Infrastructure.Repository.Query.Base
{
    // Generic Query repository class
    public class QueryRepository<T> : DbConnector, IQueryRepository<T> where T : class
    {
        public QueryRepository(IConfiguration configuration)
            : base(configuration)
        {

        }
    }
}

