

function deleteTask(button) {
    const item = button.parentElement;
    item.remove();
}



const form = document.querySelector("#todo-form");
const input = document.querySelector("#todo-input")

form.addEventListener("submit", (event) => {
    event.preventDefault();

    const taskText = input.value.trim();
    if (taskText !== "") {
        addTask(taskText);
        input.value = "";
    }

    form.reset();
})


function addTask(taskText) {
    const container = document.querySelector(".container");
    const item = document.createElement("div");
    item.classList.add("item");

    const checkbox = document.createElement("input");
    checkbox.type = "checkbox";

    const p = document.createElement("p");
    p.textContent = taskText;

    const deleteButton = document.createElement("button");
    deleteButton.classList.add("delete-btn");
    deleteButton.textContent = "Delete";
    deleteButton.onclick = () => deleteTask(deleteButton);

    item.appendChild(checkbox);
    item.appendChild(p);
    item.appendChild(deleteButton);
    container.appendChild(item);
}