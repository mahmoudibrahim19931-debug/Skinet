using System;
using System.Linq;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductSpecification : BaseSpecification<Product>
    {
        // =========================
        // FOR PRODUCTS LIST
        // =========================
        public ProductSpecification(ProductSpecParams specParams)
            : base(x =>
                // Search by name
                (string.IsNullOrEmpty(specParams.Search) ||
                    x.Name.ToLower().Contains(specParams.Search)) &&

                // Brand filter (list)
                (!specParams.Brands.Any() ||
                    specParams.Brands.Contains(x.Brand)) &&

                // Type filter (list)
                (!specParams.Types.Any() ||
                    specParams.Types.Contains(x.Type))
            )
        {
            // Default sort
            AddOrderBy(x => x.Name);

            // Paging
            ApplyPaging(
                specParams.PageSize * (specParams.PageIndex - 1),
                specParams.PageSize
            );

            // Sorting
            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(x => x.Price);
                        break;

                    case "priceDesc":
                        AddOrderByDescending(x => x.Price);
                        break;

                    default:
                        AddOrderBy(x => x.Name);
                        break;
                }
            }
        }

        // =========================
        // FOR COUNT ONLY
        // (no paging / no sorting)
        // =========================
        public ProductSpecification(ProductSpecParams specParams, bool forCount)
            : base(x =>
                (string.IsNullOrEmpty(specParams.Search) ||
                    x.Name.ToLower().Contains(specParams.Search)) &&

                (!specParams.Brands.Any() ||
                    specParams.Brands.Contains(x.Brand)) &&

                (!specParams.Types.Any() ||
                    specParams.Types.Contains(x.Type))
            )
        {
        }
    }
}
