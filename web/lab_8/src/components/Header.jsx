function Header() {
  return (
    <header>
      <img
        className="icon"
        src={`${import.meta.env.BASE_URL}favicon.svg`}
        alt=""
        width="32"
        height="32"
      />
      <h1>Market</h1>
    </header>
  )
}

export default Header
