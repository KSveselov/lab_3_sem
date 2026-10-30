

let surveyData;
let currentQuestion = 0;
let curAnswers = [];

function loadtQuestion(numberAnswer, surveyData) {
        
    const questionElement = document.querySelector(".question-container h2");
    const questionContainer = document.querySelector(".question-container");
    const startButton = document.querySelector(".start-btn");
    const buttonContainer = document.querySelector(".button-container");
    const buttonLast = document.querySelector(".prev-btn");
    const typeQuestion = surveyData.questions[numberAnswer].type;
    const counter = document.querySelector(".counter");

    questionElement.textContent = surveyData.questions[numberAnswer].question;
    startButton.classList.add("hidden");
    counter.classList.remove("hidden");
    buttonContainer.classList.remove("hidden");
    if(currentQuestion == 0)
        buttonLast.classList.add("hidden");
    else
        buttonLast.classList.remove("hidden");



    const lastAnswerContainer = document.querySelector(".answers");
    lastAnswerContainer?.remove();

    const answersContainer = document.createElement("div");
    answersContainer.classList.add("answers");

    for (const answer of surveyData.questions[numberAnswer].answers) {
        const buttonQuestion = document.createElement("input");
        buttonQuestion.type = typeQuestion === "single" ? "radio" : "checkbox";
        buttonQuestion.name = `question-${numberAnswer}`;
        buttonQuestion.value = answer;
        buttonQuestion.checked = curAnswers[numberAnswer]?.includes(answer) ?? false;

        const label = document.createElement("label");
        label.append(buttonQuestion, document.createTextNode(answer));
        answersContainer.appendChild(label);
    }

    questionContainer.appendChild(answersContainer);
}

function saveAnswers(){
    const selected = document.querySelectorAll(".answers input:checked");
    curAnswers[currentQuestion] = Array.from(selected, input => input.value);
}

function ShowRightAnswers() {
    const questionContainer = document.querySelector(".question-container");
    const heading = document.createElement("h3");
    heading.classList.add("results-heading");
    heading.textContent = "Разбор ответов";

    const answersList = document.createElement("div");
    answersList.classList.add("results-list");

    surveyData.questions.forEach((question, index) => {
        const answerItem = document.createElement("div");
        const title = document.createElement("h4");
        title.textContent = `${index + 1}. ${question.question}`;
        answerItem.appendChild(title);

        const selectedAnswers = curAnswers[index] ?? [];
        for (const [label, answers] of [
            ["Ваш ответ", selectedAnswers],
            ["Правильно", question.correctAnswers],
        ]) {
            const row = document.createElement("div");
            row.classList.add("result-row");
            const caption = document.createElement("div");
            caption.classList.add("result-label");
            caption.textContent = label;
            row.appendChild(caption);

            const values = document.createElement("div");
            values.classList.add("result-values");
            if (answers.length === 0) {
                values.textContent = "Не выбран";
            }
            for (const answer of answers) {
                const value = document.createElement("div");
                const isCorrect = question.correctAnswers.includes(answer);
                value.classList.add("result-answer", isCorrect ? "is-correct" : "is-wrong");
                value.textContent = isCorrect ? answer : `${answer} — ошибка`;
                values.appendChild(value);
            }
            row.appendChild(values);
            answerItem.appendChild(row);
        }
        answersList.appendChild(answerItem);
    });

    questionContainer.append(heading, answersList);
}

function nextQuestion(){

    if (!surveyData || currentQuestion >= surveyData.questions.length) return;

    saveAnswers();
    currentQuestion++;
    if (currentQuestion < surveyData.questions.length) {
        loadtQuestion(currentQuestion, surveyData);
    } else {
        loadLastSurvey();
    }
    updateStatusBar();
    console.log(currentQuestion);
}


function lastQuestion(){
    if (!surveyData ||
        currentQuestion >= surveyData.questions.length ||
        currentQuestion <= 0)
        return;

    saveAnswers();
    currentQuestion--;
    if (currentQuestion >= 0) {
        loadtQuestion(currentQuestion, surveyData);
    } 
    updateStatusBar();
}

function loadLastSurvey() {
    const questionContainer = document.querySelector(".question-container");
    questionContainer.querySelector(".answers")?.remove();
    questionContainer.querySelector("h2").textContent = "Опрос завершён";

    const score = surveyData.questions.filter((question, index) => {
        const selectedAnswers = curAnswers[index] ?? [];
        return selectedAnswers.length === question.correctAnswers.length &&
            question.correctAnswers.every(answer => selectedAnswers.includes(answer));
    }).length;
    const total = surveyData.questions.length;
    const percentage = Math.round(score / total * 100);

    const message = document.createElement("p");
    message.textContent = `Правильных ответов: ${score} из ${total} (${percentage}%).`;
    questionContainer.appendChild(message);
    ShowRightAnswers();

    document.querySelector(".button-container").classList.add("hidden");
}

function updateStatusBar(){
    const progress = Math.round(currentQuestion / surveyData.questions.length * 100);
    document.querySelector(".progress-bar-fill").style.width = `${progress}%`;
    document.querySelector(".progress-bar").setAttribute("aria-valuenow", progress);
    if((currentQuestion+1) <= surveyData.questions.length)
        document.querySelector(".counter").textContent = (currentQuestion+1) + "\\" + surveyData.questions.length;
}


async function startSurvey() {

    try{
        const data = await fetch("static/opros/2.json");

        if(!data.ok) {
            throw new Error("Ошибка при загрузке данных: " + data.status);
        }
        
        surveyData = await data.json();
        currentQuestion = 0;
        curAnswers = [];
        loadtQuestion(0, surveyData);
        updateStatusBar();

    } catch (error) {
        console.error("Ошибка при загрузке данных:", error);
    }
}
