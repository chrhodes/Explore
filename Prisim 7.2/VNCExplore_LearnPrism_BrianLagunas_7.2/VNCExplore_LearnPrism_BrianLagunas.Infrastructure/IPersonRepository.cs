using PrismDemo.Business;

namespace VNCExplore_LearnPrism_BrianLagunas.Infrastructure
{
    public interface IPersonRepository
    {
        int SavePerson(Person person);
    }
}
