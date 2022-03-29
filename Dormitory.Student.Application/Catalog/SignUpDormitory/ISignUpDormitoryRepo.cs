﻿using Dormitory.Student.Application.Catalog.SignUpDormitory.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Student.Application.Catalog.SignUpDormitory
{
    public interface ISignUpDormitoryRepo
    {
        Task<int> SignUp(SignUpRequest request);
        Task<int> SetStudentPoint(SetStudentPointRepuest request);
    }
}