using AutoMapper;
using CommunicationFiling.DAL.Entities;

namespace CommunicationFiling.DTO
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Action, ActionDTO>().ReverseMap();
            CreateMap<Audit, AuditDTO>().ReverseMap();
            CreateMap<CorrespondenceType, CorrespondenceTypeDTO>().ReverseMap();
            CreateMap<Filing, FilingDTO>().ReverseMap();
            CreateMap<RoleAction, RoleActionDTO>().ReverseMap();
            CreateMap<Role, RoleDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<UserRole, UserRoleDTO>().ReverseMap();
        }
    }
}