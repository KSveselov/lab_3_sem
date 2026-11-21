import { useState } from 'react'
import filterProducts from './filterProducts.js'
import Header from './components/Header.jsx'
import Search from './components/Search.jsx'
import Filters from './components/Filters.jsx'
import Map from './components/Map.jsx'
import Catalog from './components/Catalog.jsx'
import Footer from './components/Footer.jsx'
import products from './data/products.js'
import './App.css'

export default function App() {
  const [filters, setFilters] = useState({
    categories: [],
    minPrice: '',
    maxPrice: '',
    onlyInStock: false,
  })

  const filteredProducts = filterProducts(products, filters)

  return (
    <>
      <Header />
      <main>
        <Search />
        <div className="catalog-layout">
          <Filters filters={filters} onChange={setFilters} />
          <div className="catalog-content">
            <Map />
            <Catalog products={filteredProducts} />
          </div>
        </div>
      </main>
      <Footer />
    </>
  )
}

