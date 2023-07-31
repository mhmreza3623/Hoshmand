using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Hoshmand.Core.Pipeline
{
    public class PipelineContext<TRequest, TResponse>
        where TResponse : new()
    {
        public TRequest Request { get; set; }
        public TResponse Response { get; set; }
        public CancellationToken CancellationToken { get; set; }
        public List<int> Operations { get; set; }
        public PipelineContext()
        {
            Response = new TResponse() { };
            Operations = new List<int>();
        }
    }
}
