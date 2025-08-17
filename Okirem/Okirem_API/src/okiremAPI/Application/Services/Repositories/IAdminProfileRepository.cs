using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IAdminProfileRepository : IAsyncRepository<AdminProfile, Guid>, IRepository<AdminProfile, Guid>
{
}