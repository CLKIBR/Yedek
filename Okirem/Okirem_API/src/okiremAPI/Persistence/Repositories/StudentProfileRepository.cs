using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class StudentProfileRepository : EfRepositoryBase<StudentProfile, Guid, BaseDbContext>, IStudentProfileRepository
{
    public StudentProfileRepository(BaseDbContext context) : base(context)
    {
    }
}