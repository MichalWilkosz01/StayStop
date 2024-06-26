﻿using FluentValidation;
using StayStop.BLL.Pagination;
using StayStop.Model;


namespace StayStop.BLL.Validators
{
    public class HotelPaginationValidator : AbstractValidator<HotelPagination>
    {
        private int[] allowedPageSizes = { 5, 10, 15 };
        private string[] allowedSortByNames =
        {
            nameof(Hotel.Name), nameof(Hotel.Stars), nameof(Hotel.Country), nameof(Hotel.City),
            "Rating" 
        };


        public HotelPaginationValidator()
        {
            RuleFor(p => p.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(p => p.PageSize).Custom((value, context) =>
            {
                if (!allowedPageSizes.Contains(value))
                {
                    context.AddFailure("PageSize", $"PageSize must be in [{string.Join(",", allowedPageSizes)}]");
                }
            });
            RuleFor(p => p.HotelsSortBy).Must(value => string.IsNullOrEmpty(value) || allowedSortByNames.Contains(value))
            .WithMessage($"Sort by is optional or must be in [{string.Join(", ", allowedSortByNames)}]");
        }
    }
}
