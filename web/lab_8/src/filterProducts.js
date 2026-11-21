export default function filterProducts(products, filters, searchQuery = '') {
  const normalizedQuery = searchQuery.trim().toLowerCase()

  return products.filter((product) => {
    const matchesSearch = product.name.toLowerCase().includes(normalizedQuery)

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
      matchesSearch &&
      matchesCategory &&
      matchesMinPrice &&
      matchesMaxPrice &&
      matchesStock
    )
  })
}
