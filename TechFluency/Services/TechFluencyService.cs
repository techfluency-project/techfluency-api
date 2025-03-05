using TechFluency.Repository;

namespace TechFluency.Services
{
    public class TechFluencyService
    {
        private readonly TechFluencyRepository _repository;

        public TechFluencyService(TechFluencyRepository repository)
        {
            _repository = repository;
        }

    }
}
