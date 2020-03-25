"USE STRICT"

let user = {
    Login : "",
    Password: "",
}

let loginURL = "/api/login";
let httpPost = "POST";

function login() {

    user.login = document.getElementById("username").value;
    user.password = document.getElementById("password").value;

    if (validateUser(user)) {
        httpPostRequest(loginURL, user);
    }
    else {
        alert("Не указано имя пользователя или пароль.");
    }
}

function validateLength(value) {
    return value.length > 0;
}

function validateUser(user){
    return validateLength(user.login) && validateLength(user.password);
}

function httpPostRequest(url, data) {

    var request = new XMLHttpRequest();
    
    request.open(httpPost, url, false);

    request.onreadystatechange = function () {
        if (this.readyState == this.DONE) {
            alert(`${this.status}: ${this.statusText}. ${this.responseText}`);
        }
        else {
            alert('Запрос не выполнен.');
        }
    }
    
    request.setRequestHeader('Content-Type', "application/json");

    request.send(JSON.stringify(data));
}

