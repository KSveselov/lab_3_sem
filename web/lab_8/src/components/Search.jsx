function Search({ value, onChange }) {
  return (
    <div className="search">
      <input
        type="text"
        value={value}
        onChange={(event) => onChange(event.target.value)}
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
