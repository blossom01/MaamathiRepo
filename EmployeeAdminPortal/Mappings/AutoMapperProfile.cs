using AutoMapper;
using EmployeeAdminPortal.Models;

namespace EmployeeAdminPortal.Mappings
{
    public class AutoMapperProfile :Profile
    {
        public AutoMapperProfile() {

            CreateMap<Employee,EmployeeDTO>();
            CreateMap<AddEmployeeDTO, Employee>();
            CreateMap<UpdateEmploeeDTO, Employee>();


        }
    }
}