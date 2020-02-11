using LVT.Data.Context;
using LVT.Data.RepositoryBase;
using LVT.Models.Entities;

namespace LVT.Data.ConcreteRepositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext):base(repositoryContext)
        {

        }
    }
}
