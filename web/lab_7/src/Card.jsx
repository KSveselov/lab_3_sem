
function Card(){
    const [count, setCount] = React.useState(0);
    
    function handleButtonClick(type) {
        if(type === "reset"){
            setCount(0);
            return;
        }
        setCount(count + 1);
    }

    return (
        <div className="card">
            <h2>Заголовок карточки</h2>
            <p>Счетчик: {count}</p>
            <Button value="Увеличить" onClick={() => handleButtonClick("increment")} />
            <Button value="Сброс" onClick={() => handleButtonClick("reset")} />
        </div>
    );
}
  