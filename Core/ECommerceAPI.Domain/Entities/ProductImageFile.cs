﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Domain.Entities
{
    public class ProductImageFile : File
    {
        public string ImageType { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
