import ProductCard from './ProductCard.jsx'

function Catalog({ products }) {
  if (products.length === 0) {
    return <p role="status">По выбранным фильтрам ничего не найдено.</p>
  }

  return (
    <div className="catalog">
      {products.map((product) => (
        <ProductCard key={product.image} product={product} />
      ))}
    </div>
  )
}

export default Catalog
