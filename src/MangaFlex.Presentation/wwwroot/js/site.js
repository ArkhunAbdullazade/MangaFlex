async function Go(url) {
    await fetch(url, { method: "GET" }).
        then(e => {
            window.location.href = url;
        })
}

