﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.ProductImage.GetProductImages
{
    public class GetProductImagesQueryResponse
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }
    }
}
