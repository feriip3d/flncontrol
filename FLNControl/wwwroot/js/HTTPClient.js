let HTTPClient = {
    post: function (url, dados) {

        let config = {
            method: "POST",
            body: JSON.stringify(dados),
            headers: {
                "Content-Type": "application/json"
            }
        };

        let promes = fetch(url, config);

        return promes;

    },
    get: function (url) {

        let config = {
            method: "GET",
            headers: {
                "Content-Type": "application/json"
            }
        };

        let promes = fetch(url, config);

        return promes;

    }
}