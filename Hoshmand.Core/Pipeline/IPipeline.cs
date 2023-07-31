using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hoshmand.Core.Pipeline
{
    public interface IPipeline<TContext, TRequest, TResponse>
        where TContext : PipelineContext<TRequest, TResponse>, new()
        where TResponse : new()
    {
        Task<TResponse> ExecuteAsync(TRequest request, CancellationToken cancellationToken = default);
    }
}
