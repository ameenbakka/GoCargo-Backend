﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.ServiceInterfaces
{
    public interface ICloudinaryService
    {
        Task<string> UploadImage(IFormFile file);

    }
}
