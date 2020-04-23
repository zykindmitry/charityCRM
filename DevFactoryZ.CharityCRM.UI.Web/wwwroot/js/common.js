const contentTypeValue = 'application/json';

export async function httpPost(url, data) {
    return fetch(url, {
        method: 'POST',
        headers: {
            "Content-Type": contentTypeValue
        },
        body: JSON.stringify(data)
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
