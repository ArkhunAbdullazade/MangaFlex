async function Go(url) {
    await fetch(url, { method: "GET" }).
        then(e => {
            window.location.href = url;
        })
}

async function GoWithId(url, id) {
    await fetch(url + '?id=' + id, { method: "GET" })
        .then(data => {
            window.location.href = url + '?id=' + id;
        })
}

async function PostWithId(url, id) {
    console.log(url);
    await fetch(url + '?id=' + id, { method: "POST" })
}

async function Reload() {
    location.reload();
}