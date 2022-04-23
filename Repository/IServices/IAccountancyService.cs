using CNaturalApi.Models;

namespace CNaturalApi.Repository.IServices
{
    public interface IAccountancyService
    {
        public Task<Accountancy> GetAccountancy(int accountancyId);
        public Task<Accountancy> GetAccountancyByDate(DateTime date);
        public Task<IEnumerable<Accountancy>> GetAllAccountancies();
        /// <summary>
        /// Just when the month start
        /// </summary>
        /// <param name="accountancy"></param>
        /// <returns></returns>
        public Task<Accountancy> AddAccountancy(Accountancy accountancy);
        public Task<Accountancy> UpdateAccountancy(int accountancyId, Accountancy accountancy);
    }
}
