import assert from 'node:assert/strict'
import test from 'node:test'
import filterProducts from '../src/filterProducts.js'

const products = [
  { name: 'Бесплатный товар', category: 'clothing', priceValue: 0, inStock: true },
  { name: 'Наушники', category: 'electronics', priceValue: 3490, inStock: true },
  { name: 'Рюкзак', category: 'clothing', priceValue: 2990, inStock: false },
  { name: 'Камера', category: 'electronics', priceValue: 54990, inStock: false },
]

const defaultFilters = {
  categories: [],
  minPrice: '',
  maxPrice: '',
  onlyInStock: false,
}

function names(overrides = {}) {
  return filterProducts(products, { ...defaultFilters, ...overrides })
    .map((product) => product.name)
}

test('без ограничений возвращает все товары и не изменяет исходный массив', () => {
  const original = structuredClone(products)
  const result = filterProducts(products, defaultFilters)
  assert.deepEqual(result, products)
  assert.notEqual(result, products)
  assert.deepEqual(products, original)
})

test('категории объединяются: можно выбрать одну, обе или ни одной', () => {
  assert.deepEqual(names({ categories: ['electronics'] }), ['Наушники', 'Камера'])
  assert.deepEqual(names({ categories: ['clothing'] }), ['Бесплатный товар', 'Рюкзак'])
  assert.deepEqual(names({ categories: ['electronics', 'clothing'] }), names())
})

test('границы цены включены, незаполненная граница не ограничивает выбор', () => {
  assert.deepEqual(names({ minPrice: '2990', maxPrice: '3490' }), ['Наушники', 'Рюкзак'])
  assert.deepEqual(names({ minPrice: '3490' }), ['Наушники', 'Камера'])
  assert.deepEqual(names({ maxPrice: '2990' }), ['Бесплатный товар', 'Рюкзак'])
  assert.deepEqual(names({ minPrice: '3490', maxPrice: '3490' }), ['Наушники'])
  assert.deepEqual(names({ maxPrice: '0' }), ['Бесплатный товар'])
  assert.deepEqual(names({ maxPrice: '2989.99' }), ['Бесплатный товар'])
})

test('наличие исключает отсутствующие товары', () => {
  assert.deepEqual(names({ onlyInStock: true }), ['Бесплатный товар', 'Наушники'])
})

test('все группы фильтров применяются одновременно', () => {
  assert.deepEqual(names({
    categories: ['electronics'],
    minPrice: '3000',
    maxPrice: '55000',
    onlyInStock: true,
  }), ['Наушники'])
})

test('несовместимые условия и обратный диапазон дают пустой результат', () => {
  assert.deepEqual(names({ minPrice: '60000' }), [])
  assert.deepEqual(names({ minPrice: '3500', maxPrice: '3000' }), [])
})
