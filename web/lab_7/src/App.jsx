

function App() {
    const [count, setCount] = React.useState(0);
    const [inputValue, setInputValue] = React.useState("");


    function handleButtonClick(type) {
        if(type === "reset"){
            setCount(0);
            return;
        }
        setCount(count + 1);
    }

    function handleInputChange(event) {
        setInputValue(event.target.value);
    }

    return (
        <main>
            <div>
                <h2>Button - кнопки</h2>
                <Button value="Кнопка 1" onClick={() => handleButtonClick("increment")} />
                <Button value="Кнопка 2" onClick={() => handleButtonClick("increment")} />
                <Button value="Сброс" onClick={() => handleButtonClick("reset")} />
                <p>Счетчик: {count}</p>
            </div>
            <div>
                <h2>Input - поля ввода</h2>
                <Input onChange={handleInputChange} />
                <p>Вы ввели: {inputValue}</p>
            </div>
            <div>
                <h2>Card - карточки</h2>
                <Card />
                <Card />
                <Card />
            </div>
        </main>
    );
}
