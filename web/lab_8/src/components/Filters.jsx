function Filters({ filters, onChange }) {
  const hasInvalidPriceRange =
    filters.minPrice !== '' &&
    filters.maxPrice !== '' &&
    Number(filters.minPrice) > Number(filters.maxPrice)

  function handleCategoryChange(event) {
    const { value, checked } = event.target

    onChange((previous) => ({
      ...previous,
      categories: checked
        ? [...previous.categories, value]
        : previous.categories.filter((category) => category !== value),
    }))
  }

  function handlePriceChange(event) {
    const { name, value, validity } = event.target

    if (!validity.valid) return

    onChange((previous) => ({ ...previous, [name]: value }))
  }

  function handleStockChange(event) {
    const { checked } = event.target

    onChange((previous) => ({ ...previous, onlyInStock: checked }))
  }

  return (
    <aside className="filters" aria-labelledby="filters-title">
      <h2 id="filters-title">Фильтры</h2>
      <fieldset>
        <legend>Категория</legend>
        <label>
          <input
            type="checkbox"
            name="category"
            value="electronics"
            checked={filters.categories.includes('electronics')}
            onChange={handleCategoryChange}
          />
          Электроника
        </label>
        <label>
          <input
            type="checkbox"
            name="category"
            value="clothing"
            checked={filters.categories.includes('clothing')}
            onChange={handleCategoryChange}
          />
          Одежда и аксессуары
        </label>
      </fieldset>
      <fieldset>
        <legend>Цена, руб.</legend>
        <label htmlFor="price-min">От</label>
        <input
          id="price-min"
          type="number"
          name="minPrice"
          min="0"
          step="any"
          placeholder="0"
          value={filters.minPrice}
          onChange={handlePriceChange}
        />
        <label htmlFor="price-max">До</label>
        <input
          id="price-max"
          type="number"
          name="maxPrice"
          min="0"
          step="any"
          placeholder="60 000"
          value={filters.maxPrice}
          onChange={handlePriceChange}
        />
        {hasInvalidPriceRange && (
          <p id="price-error" role="alert">
            Цена «От» не должна превышать цену «До».
          </p>
        )}
      </fieldset>
      <fieldset>
        <legend>Наличие</legend>
        <label>
          <input
            type="checkbox"
            name="in-stock"
            checked={filters.onlyInStock}
            onChange={handleStockChange}
          />
          В наличии
        </label>
      </fieldset>
    </aside>
  )
}

export default Filters
