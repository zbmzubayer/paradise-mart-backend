using AutoMapper;
using BLL.DTOs.Category;
using DAL.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOs.Review;

namespace BLL.Services
{
    public class ReviewService
    {
        public static bool Create(ReviewDTO review)
        {
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<ReviewDTO, Review>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<Review>(review);
            return DataAccessFactory.ReviewData().Create(mapped);
        }
        public static List<ReviewDTO> Get()
        {
            var data = DataAccessFactory.ReviewData().Get();
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<Review, ReviewDTO>();
            });
            var mapper = new Mapper(cfg);
            return mapper.Map<List<ReviewDTO>>(data);
        }
        public static ReviewDTO Get(int id)
        {
            var data = DataAccessFactory.ReviewData().Get(id);
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<Review, ReviewDTO>();
            });
            var mapper = new Mapper(cfg);
            return mapper.Map<ReviewDTO>(data);
        }
        public static bool Update(ReviewDTO review)
        {
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<ReviewDTO, Review>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<Review>(review);
            return DataAccessFactory.ReviewData().Update(mapped);
        }
        public static bool Delete(int id)
        {
            return DataAccessFactory.ReviewData().Delete(id);
        }
    }
}
