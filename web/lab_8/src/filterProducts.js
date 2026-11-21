export default function filterProducts(products, filters) {
  return products.filter((product) => {
    const matchesCategory =
      filters.categories.length === 0 ||
      filters.categories.includes(product.category)

    const matchesMinPrice =
      filters.minPrice === '' ||
      product.priceValue >= Number(filters.minPrice)

    const matchesMaxPrice =
      filters.maxPrice === '' ||
      product.priceValue <= Number(filters.maxPrice)

    const matchesStock =
      !filters.onlyInStock || product.inStock

    return (
      matchesCategory &&
      matchesMinPrice &&
      matchesMaxPrice &&
      matchesStock
    )
  })
}
