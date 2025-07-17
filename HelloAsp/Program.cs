


using System.Text.RegularExpressions;
 
// начальные данные
List<Person> users = new List<Person> 
{ 
    new() { Id = Guid.NewGuid().ToString(), Name = "Tom", Age = 37 },
    new() { Id = Guid.NewGuid().ToString(), Name = "Bob", Age = 41 },
    new() { Id = Guid.NewGuid().ToString(), Name = "Sam", Age = 23 }
    new() { Id = Guid.NewGuid().ToString(), Name = "Ann", Age = 33 }
    new() { Id = Guid.NewGuid().ToString(), Name = "Cos", Age = 23 }                 
    <!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title>METANIT.COM</title>
<style>
td {padding:5px;}
button{margin: 5px;}
</style>
</head>
<body>
    <h2>Список пользователей</h2>
    <div>
        <input type="hidden" id="userId" />
        <p>
            Имя:<br/>
            <input id="userName" />
        </p>
        <p>
            Возраст:<br />
            <input id="userAge" type="number" />
        </p>
         <p>
            <button id="saveBtn">Сохранить</button>
            <button id="resetBtn">Сбросить</button>
        </p>
    </div>
    <table>
        <thead><tr><th>Имя</th><th>Возраст</th><th></th></tr></thead>
        <tbody>
        </tbody>
    </table>
  
    <script>
    // Получение всех пользователей
        async function getUsers() {
            // отправляет запрос и получаем ответ
            const response = await fetch("/api/users", {
                method: "GET",
                headers: { "Accept": "application/json" }
            });
            // если запрос прошел нормально
            if (response.ok === true) {
                // получаем данные
                const users = await response.json();
                const rows = document.querySelector("tbody");
                // добавляем полученные элементы в таблицу
                users.forEach(user => rows.append(row(user)));
            }
        }
        // Получение одного пользователя
        async function getUser(id) {
            const response = await fetch(`/api/users/${id}`, {
                method: "GET",
                headers: { "Accept": "application/json" }
            });
            if (response.ok === true) {
                const user = await response.json();
                document.getElementById("userId").value = user.id;
                document.getElementById("userName").value = user.name;
                document.getElementById("userAge").value = user.age;
            }
            else {
                // если произошла ошибка, получаем сообщение об ошибке
                const error = await response.json();
                console.log(error.message); // и выводим его на консоль
            }
        }
        // Добавление пользователя
        async function createUser(userName, userAge) {
  
            const response = await fetch("api/users", {
                method: "POST",
                headers: { "Accept": "application/json", "Content-Type": "application/json" },
                body: JSON.stringify({
                    name: userName,
                    age: parseInt(userAge, 10)
                })
            });
            if (response.ok === true) {
                const user = await response.json();
                document.querySelector("tbody").append(row(user));
            }
            else {
                const error = await response.json();
                console.log(error.message);
            }
        }
        // Изменение пользователя
        async function editUser(userId, userName, userAge) {
            const response = await fetch("api/users", {
                method: "PUT",
                headers: { "Accept": "application/json", "Content-Type": "application/json" },
                body: JSON.stringify({
                    id: userId,
                    name: userName,
                    age: parseInt(userAge, 10)
                })
            });
            if (response.ok === true) {
                const user = await response.json();
                document.querySelector(`tr[data-rowid='${user.id}']`).replaceWith(row(user));
            }
            else {
                const error = await response.json();
                console.log(error.message);
            }
        }
        // Удаление пользователя
        async function deleteUser(id) {
            const response = await fetch(`/api/users/${id}`, {
                method: "DELETE",
                headers: { "Accept": "application/json" }
            });
            if (response.ok === true) {
                const user = await response.json();
                document.querySelector(`tr[data-rowid='${user.id}']`).remove();
            }
            else {
                const error = await response.json();
                console.log(error.message);
            }
        }
  
        // сброс данных формы после отправки
        function reset() {
            document.getElementById("userId").value = 
            document.getElementById("userName").value = 
            document.getElementById("userAge").value = "";
        }
        // создание строки для таблицы
        function row(user) {
  
            const tr = document.createElement("tr");
            tr.setAttribute("data-rowid", user.id);
  
            const nameTd = document.createElement("td");
            nameTd.append(user.name);
            tr.append(nameTd);
  
            const ageTd = document.createElement("td");
            ageTd.append(user.age);
            tr.append(ageTd);
  
            const linksTd = document.createElement("td");
  
            const editLink = document.createElement("button"); 
            editLink.append("Изменить");
            editLink.addEventListener("click", async() => await getUser(user.id));
            linksTd.append(editLink);
  
            const removeLink = document.createElement("button"); 
            removeLink.append("Удалить");
            removeLink.addEventListener("click", async () => await deleteUser(user.id));
  
            linksTd.append(removeLink);
            tr.appendChild(linksTd);
  
            return tr;
        }
        // сброс значений формы
        document.getElementById("resetBtn").addEventListener("click", () =>  reset());
  
        // отправка формы
        document.getElementById("saveBtn").addEventListener("click", async () => {
             
            const id = document.getElementById("userId").value;
            const name = document.getElementById("userName").value;
            const age = document.getElementById("userAge").value;
            if (id === "")
                await createUser(name, age);
            else
                await editUser(id, name, age);
            reset();
        });
  
        // загрузка пользователей
        getUsers();
    </script>
</body>
</html>
Основная логика здесь заключена в коде javascript. При загрузке страницы в браузере получаем все объекты из БД с помощью функции getUsers():

1
2
3
4
5
6
7
8
9
10
11
12
13
14
15
async function getUsers() {
    // отправляет запрос и получаем ответ
    const response = await fetch("/api/users", {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    // если запрос прошел нормально
    if (response.ok === true) {
        // получаем данные
        const users = await response.json();
        const rows = document.querySelector("tbody");
        // добавляем полученные элементы в таблицу
        users.forEach(user => rows.append(row(user)));
    }
}
};
 
var builder = WebApplication.CreateBuilder();
var app = builder.Build();
 
app.Run(async (context) =>
{
    var response = context.Response;
    var request = context.Request;
    var path = request.Path;
    //string expressionForNumber = "^/api/users/([0-9]+)$";   // если id представляет число
 
    // 2e752824-1657-4c7f-844b-6ec2e168e99c
    string expressionForGuid = @"^/api/users/\w{8}-\w{4}-\w{4}-\w{4}-\w{12}$";
    if (path == "/api/users" && request.Method=="GET")
    {
        await GetAllPeople(response); 
    }
    else if (Regex.IsMatch(path, expressionForGuid) && request.Method == "GET")
    {
        // получаем id из адреса url
        string? id = path.Value?.Split("/")[3];
        await GetPerson(id, response);
    }
    else if (path == "/api/users" && request.Method == "POST")
    {
        await CreatePerson(response, request);
    }
    else if (path == "/api/users" && request.Method == "PUT")
    {
        await UpdatePerson(response, request);
    }
    else if (Regex.IsMatch(path, expressionForGuid) && request.Method == "DELETE")
    {
        string? id = path.Value?.Split("/")[3];
        await DeletePerson(id, response);
    }
    else
    {
        response.ContentType = "text/html; charset=utf-8";
        await response.SendFileAsync("html/index.html");
    }
});
 
app.Run();
 
// получение всех пользователей
async Task GetAllPeople(HttpResponse response)
{
    await response.WriteAsJsonAsync(users);
}
// получение одного пользователя по id
async Task GetPerson(string? id, HttpResponse response)
{
    // получаем пользователя по id
    Person? user = users.FirstOrDefault((u) => u.Id == id);
    // если пользователь найден, отправляем его
    if (user != null)
        await response.WriteAsJsonAsync(user);
    // если не найден, отправляем статусный код и сообщение об ошибке
    else
    {
        response.StatusCode = 404;
        await response.WriteAsJsonAsync(new { message = "Пользователь не найден" });
    }
}
 
async Task DeletePerson(string? id, HttpResponse response)
{
    // получаем пользователя по id
    Person? user = users.FirstOrDefault((u) => u.Id == id);
    // если пользователь найден, удаляем его
    if (user != null)
    {
        users.Remove(user);
        await response.WriteAsJsonAsync(user);
    }
    // если не найден, отправляем статусный код и сообщение об ошибке
    else
    {
        response.StatusCode = 404;
        await response.WriteAsJsonAsync(new { message = "Пользователь не найден" });
    }
}
 
async Task CreatePerson(HttpResponse response, HttpRequest request)
{
    try
    {
        // получаем данные пользователя
        var user = await request.ReadFromJsonAsync<Person>();
        if (user != null)
        {
            // устанавливаем id для нового пользователя
            user.Id = Guid.NewGuid().ToString();
            // добавляем пользователя в список
            users.Add(user);
            await response.WriteAsJsonAsync(user);
        }
        else
        {
            throw new Exception("Некорректные данные");
        }
    }
    catch (Exception)
    {
        response.StatusCode = 400;
        await response.WriteAsJsonAsync(new { message = "Некорректные данные" });
    }
}
 
async Task UpdatePerson(HttpResponse response, HttpRequest request)
{
    try
    {
        // получаем данные пользователя
        Person? userData = await request.ReadFromJsonAsync<Person>();
        if (userData != null)
        {
            // получаем пользователя по id
            var user = users.FirstOrDefault(u => u.Id == userData.Id);
            // если пользователь найден, изменяем его данные и отправляем обратно клиенту
            if (user != null)
            {
                user.Age = userData.Age;
                user.Name = userData.Name;
                await response.WriteAsJsonAsync(user);
            }
            else
            {
                response.StatusCode = 404;
                await response.WriteAsJsonAsync(new { message = "Пользователь не найден" });
            }
        }
        else
        {
            throw new Exception("Некорректные данные");
        }
    }
    catch (Exception)
    {
        response.StatusCode = 400;
        await response.WriteAsJsonAsync(new { message = "Некорректные данные" });
    }
}
public class Person
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public int Age { get; set; }
}