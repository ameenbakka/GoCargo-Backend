﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoCargo.Application.Dto
{
    public class SendNotificationDto
    {
        public int RecipientId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }
}
