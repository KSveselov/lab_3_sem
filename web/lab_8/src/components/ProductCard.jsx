function ProductCard({ product }) {
  return (
    <div className="item">
      <img
        src={`${import.meta.env.BASE_URL}img/${product.image}`}
        alt={product.alt}
      />
      <h2>{product.name}</h2>
      <p>{product.description}</p>
      <span>{product.price}</span>
    </div>
  )
}

export default ProductCard
