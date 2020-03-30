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
    
    request.open(httpPost, url, true);

    request.onreadystatechange = function () {
        switch (this.readyState) {
            case this.DONE:
                alert(`${this.status}: ${this.statusText}. ${this.responseText}`);
                break;
            case this.HEADERS_RECEIVED:
            case this.LOADING:
            case this.OPENED:
            case this.UNSENT:
                break;
            default:
                alert(`Неизвестный статус запроса: '${this.readyState} ${this.statusText}'. Обратитесь в службу поддержки.`);
        };
    };

    request.setRequestHeader('Content-Type', "application/json");

    request.send(JSON.stringify(data));
}

