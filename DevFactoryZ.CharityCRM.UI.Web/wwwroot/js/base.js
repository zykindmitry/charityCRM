export async function httpPost(url, data, contentTypeValue) {
    try {
        let response = await fetch(url, {
            method: 'POST',
            headers: {
                "Content-Type": contentTypeValue
            },
            body: JSON.stringify(data)
        });

        let result = await response.text();

        alert(`${response.status}: ${response.statusText}. ${result}.`);
    }
    catch (e) {
        alert(`Запрос завершился с ошибкой: [${e.name}]${e.message}.`);
    }
}
