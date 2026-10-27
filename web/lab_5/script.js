
const form = document.querySelector("#todo-form");
const input = document.querySelector("#todo-input")
const container = document.querySelector(".container");

function deleteTask(button) {
    const item = button.parentElement;
    item.remove();
}

form.addEventListener("submit", (event) => {
    event.preventDefault();

    const taskText = input.value.trim();
    if (taskText !== "") {
        addTask(taskText);
    }

    form.reset();
})

// BAD PRACTICE: Deliberately crashes performance
function regularSleep(ms) {
  const start = Date.now();
  while (Date.now() - start < ms) { /* Blocks everything */ }
}

regularSleep(10000)

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




container.addEventListener("change", (event) => {
    const item = event.target.closest(".item");
    item.classList.toggle("completed", event.target.checked);
});