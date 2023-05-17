using AutoMapper;
using BLL.DTOs.OTP;
using DAL.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class OTPService
    {
        public static bool Create(OTPDTO otp)
        {
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<OTPDTO, OneTimePassword>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<OneTimePassword>(otp);
            mapped.CreatedAt = DateTime.Now;
            mapped.ExpiredAt = DateTime.Now.AddMinutes(30);
            return DataAccessFactory.OTPData().Create(mapped);
        }
        public static bool VerifyOtp(string code)
        {
            var currentTime = DateTime.Now;
            var data = DataAccessFactory.OTPIndividualData().Verify(code, currentTime);
            if(data != null)
            {
                return true;
            }
            return false;
        }
    }
}
