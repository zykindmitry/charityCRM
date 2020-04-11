import { httpPost } from './base.js';

let user = {
    Login : "",
    Password: "",
}

let loginURL = "/api/login";
const contentTypeValue = 'application/json';

export function login() {

    user.login = document.getElementById("username").value;
    user.password = document.getElementById("password").value;

    if (validateUser(user)) {
        httpPost(loginURL, user, contentTypeValue);
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


