function Search() {
  return (
    <div className="search">
      <input
        type="text"
        placeholder="Поиск по каталогу"
        aria-label="Поиск по каталогу"
      />
      <button className="search-button" type="button" aria-label="Найти">
        🔍
      </button>
    </div>
  )
}

export default Search
