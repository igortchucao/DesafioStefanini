namespace Questao5.Application.Handlers
{
    public interface IHandlerAsync<Request, Return>
    {
        Task<Return> ExecuteAsync(Request request);
    }
}
