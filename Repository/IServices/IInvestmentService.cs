using CNaturalApi.Models;

namespace CNaturalApi.Repository.IServices
{
    public interface IInvestmentService
    {
        public Task<Investment> GetInvestment(int investmentId);
        public Task<IEnumerable<Investment>> GetAllInvestments();
        public Task<Investment> AddInvestment(Investment investment);
        //No me queda claro el update.
        public Task<Investment> UpdateInvestment(int investmentId, Investment investment);
        public Task<bool> DeleteInvestment(int investmentId);
        //Solo se puede borrar una inversion si no han 24 horas de realizada.
        
    }
}
