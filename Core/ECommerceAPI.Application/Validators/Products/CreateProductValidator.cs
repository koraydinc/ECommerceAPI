﻿using ECommerceAPI.Application.Features.Commands.Product.CreateProduct;
using FluentValidation;

namespace ECommerceAPI.Application.Validators.Products
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommandRequest>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Lütfen ürün adını boş geçmeyiniz!")
                .Length(2, 150)
                    .WithMessage("Ürün adı 2 ile 150 karakter arasında olmalıdır!");
            RuleFor(p => p.Stock)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Lütfen stok bilgisini boş geçmeyiniz!")
                .Must(p => p >= 0)
                    .WithMessage("Stok bilgisi negatif olamaz!");
            RuleFor(p => p.Price)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Lütfen fiyat bilgisini boş geçmeyiniz!")
                .Must(p => p >= 0)
                    .WithMessage("Fiyat bilgisi negatif olamaz!");

        }
    }
}
