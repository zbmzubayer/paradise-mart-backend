using AutoMapper;
using BLL.DTOs;
using DAL.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOs.Admin;
using BLL.DTOs.Customer;

namespace BLL.Services
{
    public class AdminService
    {
        public static bool Create(AdminCreateDTO admin)
        {
            // MailService.AdminRegistration(admin.Name, admin.Email);
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<AdminCreateDTO, Admin>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<Admin>(admin);
            mapped.Guid = Guid.NewGuid().ToString("N");
            mapped.CreatedAt = DateTime.Now;
            return DataAccessFactory.AdminData().Create(mapped);
        }
        public static List<AdminDTO> Get()
        {
            var data = DataAccessFactory.AdminData().Get();
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<Admin, AdminDTO>();
            });
            var mapper = new Mapper(cfg);
            return mapper.Map<List<AdminDTO>>(data);
        }
        public static AdminDTO Get(string guid)
        {
            var data = DataAccessFactory.AdminData().Get(guid);
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<Admin, AdminDTO>();
            });
            var mapper = new Mapper(cfg);
            return mapper.Map<AdminDTO>(data);
        }
        public static bool Update(AdminDTO admin)
        {
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<AdminDTO, Admin>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<Admin>(admin);
            mapped.ModifiedAt = DateTime.Now;
            mapped.CreatedAt = DateTime.Now;
            return DataAccessFactory.AdminData().Update(mapped);
        }
        public static bool Delete(string guid)
        {
            return DataAccessFactory.AdminData().Delete(guid);
        }
        // Others
        public static AdminDTO GetByEmail(string email)
        {
            var data = DataAccessFactory.AdminOthersData().GetByEmail(email);
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<Admin, AdminDTO>();
            });
            var mapper = new Mapper(cfg);
            return mapper.Map<AdminDTO>(data);
        }
        public static bool UploadPhoto(string guid, string photo)
        {
            return DataAccessFactory.AdminOthersData().UploadPhoto(guid, photo);
        }
        public static bool DeletePhoto(string guid)
        {
            return DataAccessFactory.AdminOthersData().DeletePhoto(guid);
        }
        public static bool ChangePassword(string guid, ChangePassDTO changePasswordDTO)
        {
            var dbAdmin = DataAccessFactory.CustomerData().Get(guid);
            if (changePasswordDTO.CurrentPassword == dbAdmin.Password)
            {
                return DataAccessFactory.AdminOthersData().ChangePassword(dbAdmin.Guid, changePasswordDTO.NewPassword);
            }
            return false;
        }
    }
}
