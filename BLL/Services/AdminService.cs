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
using BLL.Helpers;

namespace BLL.Services
{
    public class AdminService
    {
        public static int iteration = 4;
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
            mapped.Salt = PasswordHash.GenerateSalt();
            mapped.Password = PasswordHash.GenerateHash(mapped.Password, mapped.Salt, iteration);
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
            var dbUser = DataAccessFactory.AdminData().Get(guid);
            changePasswordDTO.CurrentPassword = PasswordHash.GenerateHash(changePasswordDTO.CurrentPassword, dbUser.Salt, iteration);
            if (changePasswordDTO.CurrentPassword == dbUser.Password)
            {
                changePasswordDTO.NewPassword = PasswordHash.GenerateHash(changePasswordDTO.NewPassword, dbUser.Salt, iteration);
                return DataAccessFactory.CustomerOthersData().ChangePassword(dbUser.Guid, changePasswordDTO.NewPassword);
            }
            return false;
        }
        public static bool ChangeEmail(string guid, ChangeEmailDTO changeEmailDTO)
        {
            var dbUser = DataAccessFactory.AdminData().Get(guid);
            changeEmailDTO.Password = PasswordHash.GenerateHash(changeEmailDTO.Password, dbUser.Salt, iteration);
            if (changeEmailDTO.Password == dbUser.Password)
            {
                return DataAccessFactory.AdminOthersData().ChangeEmail(guid, changeEmailDTO.Email);
            }
            return false;
        }
    }
}
