// Получение всех пользователей



document.addEventListener("DOMContentLoaded", function () {

    const form = document.querySelector("#cars-form");
    
    document.body.addEventListener("click", e => {
        const action = e.target.getAttribute("data-action");
        if(action === "Delete")
        {
            const id = e.target.getAttribute("data-value");
            if(id){
                deleteUser(id);                
            }
        }
         if(action === "Edit")
        {
            const id = e.target.getAttribute("data-value");
        console.log(action,id);
            if(id){
                console.log(id)
                getCar(id);                
            }
        }

    })


    async function getCars() {
        // отправляет запрос и получаем ответ
        const response = await fetch("/api/cars", {
            method: "GET",
            headers: { "Accept": "application/json" }
        });
        // если запрос прошел нормально
        if (response.ok === true) {
            // получаем данные
            const cars = await response.json();
            
            const htmlTable = createCarsTable(cars);
            // добавляем полученные элементы в таблицу
            document.querySelector("#root").append(htmlTable);
        }
    }
    function createCarsTable(cars) {
        // Create table element
        const table = document.createElement("table");
        table.classList.add("table");

        // Create table header
        const header = table.createTHead();
        const headerRow = header.insertRow(0);
        const headers = ["Brand","Model", "Year", "Price"];
        headers.forEach((headerText, index) => {
            const cell = headerRow.insertCell(index);
            cell.textContent = headerText;
        });

        // Create table body
        const tbody = table.createTBody();
        cars.forEach(car => {
            
           const r =  addCarRow(car, tbody)
            tbody.append(r)

            });

        return table;
    }
// Получение одного пользователя
    async function getCar(id) {
        const response = await fetch("/api/cars/" + id, {
            method: "GET",
            headers: { "Accept": "application/json" }
        });
        if (response.ok === true) {
            const user = await response.json();
            form.elements["id"].value = user.id;
            form.elements["brand"].value = user.brand;
            form.elements["model"].value = user.model;
            form.elements["year"].value = user.year;
            form.elements["price"].value = user.price;
        }
        else {
            // если произошла ошибка, получаем сообщение об ошибке
            const error = await response.json();
            console.log(error.message); // и выводим его на консоль
        }
    }
// Добавление пользователя
    async function createCar(car) {
        const response = await fetch("api/cars", {
            method: "POST",
            headers: { "Accept": "application/json", "Content-Type": "application/json" },
            body: JSON.stringify(car)
        });
        if (response.ok === true) {
            const user = await response.json();
            reset();
            const tbody = document.querySelector("tbody");
            const r = addCarRow(user, tbody);
            tbody.append(r);
        }
        else {
            const error = await response.json();
            console.log(error.message);
        }
    }
// Изменение пользователя
    async function editCar(car) {
        const response = await fetch("api/cars", {
            method: "PUT",
            headers: { "Accept": "application/json", "Content-Type": "application/json" },
            body: JSON.stringify({
                id: car.id,
                brand: car.brand,
                model: car.model,
                year: parseInt(car.year, 10),
                price: parseInt(car.price, 10)
            })
        });
        if (response.ok === true) {
            const car = await response.json();
            reset();
            document.querySelector("tr[data-rowid='" + car.id + "']").replaceWith(addCarRow(car));
        }
        else {
            const error = await response.json();
            console.log(error.message);
        }
    }
// Удаление пользователя
    async function deleteUser(id) {
        const response = await fetch("/api/cars/" + id, {
            method: "DELETE",
            headers: { "Accept": "application/json" }
        });
        if (response.ok === true) {
            const carId = await response.json();
            console.log(carId);
            document.querySelector(`tr[data-row-car-id='${carId}']`).remove();
        }
        else {
            const error = await response.json();
            console.log(error.message);
        }
    }

// сброс данных формы после отправки
    function reset() {
        form.reset();
        form.elements["id"].value = 0;
    }
// создание строки для таблицы
   
    function addCarRow(car, tbody) {
        // Find the table body

        // Create a new row
        const row = document.createElement("tr");//tbody.insertRow();
        
        row.setAttribute("data-row-car-id", car.id);

        // Create and append cells for each car property
        const brandCell = row.insertCell(0);
        brandCell.textContent = car.brand;

        const modelCell = row.insertCell(1);
        modelCell.textContent = car.model;

        const yearCell = row.insertCell(2);
        yearCell.textContent = car.year;

        const priceCell = row.insertCell(3);
        priceCell.textContent = car.price;

        // Create and append Delete button cell
        const deleteCell = row.insertCell(4);
        const deleteButton = document.createElement("button");
        deleteButton.setAttribute("data-action", "Delete");
        deleteButton.setAttribute("data-value", car.id);
        deleteButton.textContent = "Delete";
        deleteCell.appendChild(deleteButton);

        // Create and append Edit button cell
        const editCell = row.insertCell(5);
        const editButton = document.createElement("button");
        editButton.setAttribute("data-action", "Edit");
        editButton.setAttribute("data-value", car.id);
        editButton.textContent = "Edit";
        editCell.appendChild(editButton);
        
        return row;
    }
// сброс значений формы
//     document.getElementById("reset").addEventListener("click", e => {
//
//         e.preventDefault();
//         reset();
//     })

// отправка формы


    form.addEventListener("submit", e => {
        e.preventDefault();
        console.log(form.elements);
        const id = form.elements["id"]?.value;
        const brand = form.elements["brand"].value;
        const model = form.elements["model"].value;
        const year = form.elements["year"].value;
        const price = form.elements["price"].value;
        if (!id)
            createCar({ brand, model, year, price});
        else
            editCar({id, brand, model, year, price});
    });

// // загрузка пользователей

    getCars();
})
