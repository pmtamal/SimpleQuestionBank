using QuestionBank.Model.Domain;

namespace QuestionBank.Mapper.DomainEntityMapper
{
    public partial class DomainEntityProfile
    {
        public void RoleDomainEntityProfile()
        {
            CreateMap<Persistence.Entity.Role, Role>().ReverseMap();
        }
    }
}
