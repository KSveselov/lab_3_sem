

let surveyData;
let currentQuestion = 0;
function loadtQuestion(numberAnswer, surveyData) {
        
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



    const lastAnswerContainer = document.querySelector(".answers");
    lastAnswerContainer?.remove();

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

function nextQuestion(){
    if (!surveyData || currentQuestion >= surveyData.questions.length) return;

    currentQuestion++;
    if (currentQuestion < surveyData.questions.length) {
        loadtQuestion(currentQuestion, surveyData);
    } else {
        loadLastSurvey();
    }
    updateStatusBar();
}

function loadLastSurvey() {
    const questionContainer = document.querySelector(".question-container");
    questionContainer.querySelector(".answers")?.remove();
    questionContainer.querySelector("h2").textContent = "Опрос завершён";

    const message = document.createElement("p");
    message.textContent = "Спасибо за участие в опросе!";
    questionContainer.appendChild(message);

    document.querySelector(".button-container").classList.add("hidden");
}

function updateStatusBar(){
    const progress = Math.round(currentQuestion / surveyData.questions.length * 100);
    document.querySelector(".progress-bar-fill").style.width = `${progress}%`;
    document.querySelector(".progress-bar").setAttribute("aria-valuenow", progress);
}

async function startSurvey() {

    try{
        const data = await fetch("static/opros/1.json");

        if(!data.ok) {
            throw new Error("Ошибка при загрузке данных: " + data.status);
        }
        
        surveyData = await data.json();
        currentQuestion = 0;
        loadtQuestion(0, surveyData);
        updateStatusBar();

    } catch (error) {
        console.error("Ошибка при загрузке данных:", error);
    }
}
