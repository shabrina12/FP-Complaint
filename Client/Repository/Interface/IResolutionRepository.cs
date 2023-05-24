using Client.Models;
using Client.ViewModels;

namespace Client.Repository.Interface
{
    public interface IResolutionRepository : IGeneralRepository<Resolution, int>
    {
        public Task<ResponseMessageVM> PostResolution(ResolutionVM entity, string token);
    }
}
