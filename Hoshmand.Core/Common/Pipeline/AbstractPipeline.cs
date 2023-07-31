namespace Hoshmand.Core.Pipeline;

public delegate Task PipelineOperation<TContext>(TContext cntx);

public delegate Task PipelineOperationMethod<TContext>(TContext cntx, PipelineOperation<TContext> next);

public abstract class AbstractPipeline<TContext, TRequest, TResponse> : IPipeline<TContext, TRequest, TResponse>
    where TContext : PipelineContext<TRequest, TResponse>, new()
    where TResponse : new()
{
    protected IList<PipelineOperationMethod<TContext>> Operations { get; }

    public AbstractPipeline()
    {
        Operations = new List<PipelineOperationMethod<TContext>>();
    }

    public virtual void AddOperation(PipelineOperationMethod<TContext> pipelineOperation)
    {
        Operations.Add(pipelineOperation);
    }

    protected virtual void OnExecuted(TContext context) { }
    protected virtual async Task RanToCompletion(TContext context) { await Task.CompletedTask; }
    protected virtual void OnException(TContext context, Exception exception)
    {
        throw exception;
    }

    public async Task<TResponse> ExecuteAsync(TRequest request, CancellationToken cancellationToken = default)
    {
        var context = new TContext { Request = request, CancellationToken = cancellationToken };

        try
        {
            await InternalExecute(context, 0);

            await RanToCompletion(context);
        }
        catch (Exception ex)
        {
            //todo : logException

            OnException(context, ex);
        }

        OnExecuted(context);
        return context.Response;
    }

    private async Task InternalExecute(TContext context, int index)
    {
        if (context.CancellationToken.IsCancellationRequested)
        {
            await Task.FromCanceled(context.CancellationToken);
        }

        if (Operations.ElementAtOrDefault(index) != null)
        {
            await Operations[index].Invoke(context, async cntx => await InternalExecute(cntx, index + 1));
        }
        else
            await Task.CompletedTask;
    }
}
