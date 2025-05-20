using TechFluency.Models;
using TechFluency.Repository;

namespace TechFluency.Services
{
    public class ProgressService
    {
        private readonly UserProgresRepository _userProgresRepository;

        public ProgressService(UserProgresRepository userProgresRepository)
        {
            _userProgresRepository = userProgresRepository;
        }

        public UserProgress GetUserProgressByUserId(string userId)
        {
            return _userProgresRepository.GetUserProgressByUserId(userId);
        }
    }
}
