let HTTPRequest = {
    post: (url, data) => {
        let requestHeaders = new Headers();
        requestHeaders.append("Content-Type", "application/json");

        let requestConfiguration = {
            method: "POST",
            body: JSON.stringify(data),
            headers: requestHeaders,
        }

        return fetch(url, requestConfiguration);
    },

    get: (url) => {
        let requestHeaders = new Headers();
        requestHeaders.append("Content-Type", "application/json");

        let requestConfiguration = {
            method: "GET",
            headers: requestHeaders,
        }

        return fetch(url, requestConfiguration);
    }
};

export { HTTPRequest };