﻿using Dormitory.Admin.Application.Catalog.StudentRepository.Dtos;
using Dormitory.Admin.Application.CommonDto;
using Dormitory.Domain.AppEntities;
using Dormitory.EntityFrameworkCore.AdminEntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Admin.Application.Catalog.StudentRepository
{
    public class StudentRepo : IStudentRepo
    {
        private readonly AdminSolutionDbContext _dbContext;
        public StudentRepo(AdminSolutionDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> CreateOrUpdate(StudentEntity request)
        {
            var student = new StudentEntity()
            {
                Id = request.Id,
                Name = request.Name,
                DOB = request.DOB,
                BaseAdress = request.BaseAdress,
                Adress = request.Adress,
                Class = request.Class,
                Phone = request.Phone,
                Email = request.Email,
                Major = request.Major,
                Gender = request.Gender,
                AcademicYear = request.AcademicYear,
                Ethnic = request.Ethnic,
                RelativeName = request.RelativeName,
                RelativePhone = request.RelativePhone,
                Religion = request.Religion,
                StudentCode = request.StudentCode,
                Point = request.Point.HasValue ? request.Point.Value : 0,
            };
            if (student.Id == 0)
            {
                _dbContext.StudentEntities.Add(student);
                return await _dbContext.SaveChangesAsync();
            }
            else if (student.Id > 0)
            {
                _dbContext.StudentEntities.Update(student);
                return await _dbContext.SaveChangesAsync();
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> Delete(int id)
        {
            var student = await _dbContext.StudentEntities.FindAsync(id);
            if (student != null)
            {
                _dbContext.StudentEntities.Remove(student);
            }
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<PageResult<StudentDto>> GetList(PageRequestBase request)
        {
            var query = from s in _dbContext.StudentEntities
                        select s;

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.Name.Contains(request.Keyword));
            }

            int totalRow = await query.CountAsync();

            var data = await query
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new StudentDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Phone = x.Phone,
                    Email = x.Email,
                    Major = x.Major,
                    Ethnic = x.Ethnic,
                    AcademicYear = x.AcademicYear,
                    Adress = x.Adress,
                    BaseAdress = x.BaseAdress,
                    DOB = x.DOB,
                    RelativeName = x.RelativeName,
                    RelativePhone = x.RelativePhone,
                    Religion = x.Religion,
                    Class = x.Class,
                    Gender = x.Gender,
                    StudentCode = x.StudentCode,
                    Point = x.Point,
                }).ToListAsync();

            var pageResult = new PageResult<StudentDto>()
            {
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                TotalRecords = totalRow,
                Items = data
            };
            return pageResult;
        }
    }
}
