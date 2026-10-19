


async function loadtQuestion(numberAnswer, surveyData) {
        
    const questionElement = document.querySelector(".question-container h2");
    const questionContainer = document.querySelector(".question-container");
    const startButton = document.querySelector(".start-btn");
    const buttonContainer = document.querySelector(".button-container");
    const buttonLast = document.querySelector(".prev-btn");
    const typeQuestion = surveyData.questions[numberAnswer].type;
    
    questionElement.textContent = surveyData.questions[numberAnswer].question;
    startButton.classList.add("hidden");
    buttonContainer.classList.remove("hidden");
    buttonLast.classList.add("hidden");

    const answersContainer = document.createElement("div");
    answersContainer.classList.add("answers");

    for (const answer of surveyData.questions[numberAnswer].answers) {
        const buttonQuestion = document.createElement("input");
        buttonQuestion.type = typeQuestion === "single" ? "radio" : "checkbox";
        buttonQuestion.name = `question-${numberAnswer}`;

        const label = document.createElement("label");
        label.append(buttonQuestion, document.createTextNode(answer));
        answersContainer.appendChild(label);
    }

    questionContainer.appendChild(answersContainer);
}

async function startSurvey() {

    try{
        const data = await fetch("static/opros/1.json");

        if(!data.ok) {
            throw new Error("Ошибка при загрузке данных: " + data.status);
        }
        
        const surveyData = await data.json();
        loadtQuestion(0, surveyData);

    } catch (error) {
        console.error("Ошибка при загрузке данных:", error);
    }
}
