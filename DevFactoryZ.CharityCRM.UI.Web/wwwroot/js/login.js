"USE STRICT"

let user = {
    Login : "",
    Password: "",
}

let loginURL = "/api/login";
let httpPost = "POST";
const headerContentTypeName = 'Content-Type';
const headerContentTypeValue = 'application/json';

function login() {

    user.login = document.getElementById("username").value;
    user.password = document.getElementById("password").value;

    if (validateUser(user)) {
        //httpPostByXMLHttpRequest(loginURL, user);
        httpPostByFetch(loginURL, user);
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

function httpPostByXMLHttpRequest(url, data) {

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

    request.setRequestHeader(headerContentTypeName, headerContentTypeName);

    request.send(JSON.stringify(data));
}

async function httpPostByFetch(url, data) {
    try {
        let response = await fetch(url, {
            method: httpPost,
            headers: {
                "Content-Type": headerContentTypeValue
            },
            body: JSON.stringify(data)
        });

        let result = await response.text();

        alert(`${response.status}: ${response.statusText}. ${result}.`);
    }
    catch (e) {
        alert(`Запрос завершился с ошибкой: [${e.name}]${e.message}.`)
    }
}

