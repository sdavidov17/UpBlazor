﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Marten;
using UpBlazor.Core.Models;
using UpBlazor.Core.Repositories;

namespace UpBlazor.Infrastructure.Repositories
{
    public class TwoUpRequestRepository : GenericRepository<TwoUpRequest>, ITwoUpRequestRepository
    {
        public TwoUpRequestRepository(IDocumentStore store) : base(store) { }

        public async Task<TwoUpRequest> GetByRequesterAndRequesteeAsync(string requesterId, string requesteeId, CancellationToken cancellationToken = default)
        {
            await using var session = Store.QuerySession();

            return await session.Query<TwoUpRequest>()
                .SingleOrDefaultAsync(x => x.RequesterId == requesterId &&
                                      x.RequesteeId == requesteeId, cancellationToken);
        }

        public async Task<IReadOnlyList<TwoUpRequest>> GetAllByRequesterAsync(string requesterId, CancellationToken cancellationToken = default)
        {
            await using var session = Store.QuerySession();

            return await session.Query<TwoUpRequest>()
                .Where(x => x.RequesterId == requesterId)
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<TwoUpRequest>> GetAllByRequesteeAsync(string requesteeId, CancellationToken cancellationToken = default)
        {
            await using var session = Store.QuerySession();

            return await session.Query<TwoUpRequest>()
                .Where(x => x.RequesteeId == requesteeId)
                .ToListAsync(cancellationToken);        
        }
    }
}