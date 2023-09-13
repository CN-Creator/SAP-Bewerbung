const APOD_API_KEY = "DEMO_KEY"

async function init() {
    await getNewDeck();
    await shuffleCards();
    await drawNextCard();
    getAstronomyPictureOfToday();
}

function getAstronomyPictureOfToday() {
    var url = `https://api.nasa.gov/planetary/apod?api_key=${APOD_API_KEY}`
    fetch(url).then(function (response) {
        return response.json();
    }).then(function (data) {
        var elem = document.getElementById("card_img_prev");
        elem.setAttribute("src", data["url"])
        elem = document.getElementById("prev_img_title");
        elem.innerText = data["title"]
    }).catch(function () {
        console.log("There was an error!");
    });
}

init();