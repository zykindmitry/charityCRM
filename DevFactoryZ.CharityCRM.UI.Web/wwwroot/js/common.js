'use strict'
const contentTypeValue = 'application/json';

export async function httpPost(url, data) {
    return httpRequest('POST', url, data);
};

export async function httpGet(url) {
    return httpRequest('GET', url, null);
};

export async function httpPut(url, data) {
    return httpRequest('PUT', url, data)
};

async function httpRequest(method, url, data){
    return fetch(url, {
        method: method,
        headers: {
            "Content-Type": contentTypeValue
        },
        body: data == null ? data : JSON.stringify(data)
    }).then((response) => {
        if (response.ok) {
            return response.text();
        }
        else {
            return response.text()
                .then((text) => {
                    return text;
                }).then((message) => {
                    return response.status < 500
                        ? `[${response.status}]${response.statusText}. ${message}`
                        : `[${response.status}]${response.statusText}`;
                }).catch((error) => {
                    return `Запрос завершился с ошибкой: ${error.message}.`;
                });
        }
    }).then((text) => {
        return text;
    }).catch((error) => {
        return `Запрос завершился с ошибкой: ${error.name}: ${error.message}.`;
    });
};