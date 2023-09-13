let deck_info = {
    deck_id: "",
    remaining: 1,
    shuffled: false,
    success: false
};

let next_card = {};
let previous_card = {};

let current_points = 0;
let record_points = 0;

const tooltip_phrases_higher = [
    "Are you sure?",
    "Isn't it lower after all?",
    "I would think again...",
    "The next click decides a lot..."
];

const tooltip_phrases_lower = [
    "Are you sure?",
    "Isn't it higher after all?",
    "I would think again...",
    "The next click decides a lot..."
];

const values = {
    "ACE": 12,
    "KING": 11,
    "QUEEN": 10,
    "JACK": 9,
    "10": 8,
    "9": 7,
    "8": 6,
    "7": 5,
    "6": 4,
    "5": 3,
    "4": 2,
    "3": 1,
    "2": 0
}

const suits = {
    "CLUBS": 3,
    "SPADES": 2,
    "HEARTS": 1,
    "DIAMONDS": 0
}

/**
 * Method creates a new deck and returns deck_info
 */
async function getNewDeck() {
    if (deck_info["deck_id"] != "") {
        return;
    }

    var url = `http://deckofcardsapi.com/api/deck/new/`;

    var response = await fetch(url).then(function (response) {
        return response.json();
    }).then(function (data) {
        deck_info = data;
    }).catch(function () {
        console.log("There was an error!");
    });
}

/**
 * Method compares prev and next card with selection if higher or lower
 * @param {string} selection Selection from user if card is higher or lower
 */
async function isHigherOrLower(selection) {
    var elem = document.getElementById("tooltipHigher");
    elem.classList.add("invisible");
    elem = document.getElementById("tooltipLower");
    elem.classList.add("invisible");

    await drawNextCard();
    await getRandomFact();

    if (deck_info["remaining"] == 1) {
        await shuffleCards();
    }

    var prev_card_values = {
        "value": values[previous_card["value"]],
        "suit": suits[previous_card["suit"]]
    }
    var next_card_values = {
        "value": values[next_card["value"]],
        "suit": suits[next_card["suit"]]
    }

    if (selection == "lower") {
        if (prev_card_values["value"] > next_card_values["value"]) {
            editColorNavbar("right");
            setCurrentPoints("right");
        } else if ((prev_card_values["value"] == next_card_values["value"]) && (prev_card_values["suit"] > next_card_values["suit"])) {
            editColorNavbar("right");
            setCurrentPoints("right");
        } else {
            editColorNavbar("wrong");
            setCurrentPoints("wrong");
        }
    } else if (selection == "higher") {
        if (prev_card_values["value"] < next_card_values["value"]) {
            editColorNavbar("right");
            setCurrentPoints("right");
        } else if (((prev_card_values["value"] == next_card_values["value"])) && prev_card_values["suit"] < next_card_values["suit"]) {
            editColorNavbar("right");
            setCurrentPoints("right");
        } else {
            editColorNavbar("wrong");
            setCurrentPoints("wrong");
        }
    } else {
        console.log("Error! Falsche Eingabe!");
    }

}

/**
 * Method edits the visual representation of points
 * @param {string} rightOrWrong String if anwser is right or wrong
 */
function setCurrentPoints(rightOrWrong) {
    if (rightOrWrong == "right") {
        current_points = current_points + 1;
        var elem = document.getElementById("current_points");
        elem.innerText = current_points;
    } else if (rightOrWrong == "wrong") {
        if (current_points > record_points) {
            record_points = current_points;
            var elem = document.getElementById("record_points");
            elem.innerText = record_points;
        }
        current_points = 0;
        var elem = document.getElementById("current_points");
        elem.innerText = current_points;
    } else {
        console.log("Error! Current Points could not be set!");
    }
}

/**
 * Method edits Navbar color in regarts to if the answer is correct or not
 * @param {string} rightOrWrong String representing right or wrong
 */
function editColorNavbar(rightOrWrong) {
    if (rightOrWrong == "right") {
        var elem = document.getElementById("navbar");
        elem.classList.remove("bg-dark");
        elem.classList.remove("bg-danger");
        elem.classList.add("bg-success");

        window.speechSynthesis.speak(new SpeechSynthesisUtterance('Yes! Total richtig!'))
    } else if (rightOrWrong == "wrong") {
        var elem = document.getElementById("navbar");
        elem.classList.remove("bg-dark");
        elem.classList.remove("bg-success");
        elem.classList.add("bg-danger");

        window.speechSynthesis.speak(new SpeechSynthesisUtterance('Hmmm! Leider falsch!'))
    } else {
        var elem = document.getElementById("navbar");
        elem.classList.add("bg-dark");
        elem.classList.remove("bg-success");
        elem.classList.remove("bg-danger");
    }
}

/**
 * Method calls Cards API and shuffles selected deck
 */
async function shuffleCards() {
    if (deck_info["deck_id"] == "") {
        getNewDeck();
    }

    var url = `https://deckofcardsapi.com/api/deck/${deck_info["deck_id"]}/shuffle/`;

    var response = await fetch(url).then(function (response) {
        return response.json();
    }).then(function (data) {
        deck_info = data;
    }).catch(function () {
        console.log("There was an error!");
    });
}

/**
 * Method calls Cards API and returns one card
 */
async function drawNextCard() {
    if (deck_info["deck_id"] == "") {
        getNewDeck();
    }

    var url = `https://deckofcardsapi.com/api/deck/${deck_info["deck_id"]}/draw/?count=1`;

    var response = await fetch(url).then(function (response) {
        return response.json();
    }).then(function (data) {
        previous_card = next_card;
        next_card = data["cards"][0];
        deck_info["remaining"] = data["remaining"];
        var elem = document.getElementById("card_img_new");
        elem.setAttribute("src", next_card["image"]);
        elem = document.getElementById("card_img_prev");
        elem.setAttribute("src", previous_card["image"]);
        elem = document.getElementById("prev_img_title");
        elem.innerText = "Vorherige Karte";
    }).catch(function () {
        console.log("There was an error!");
    });
}

/**
 * Creates tooltips for higher Button with random selection from words
 */
function tooltipHigher() {
    var elem = document.getElementById("tooltipHigher");
    elem.setAttribute("data-toggle", "tooltip");
    elem.setAttribute("title", tooltip_phrases_higher[Math.floor(Math.random() * tooltip_phrases_higher.length)]);
}

/**
 * Creates tooltips for lower Button with random selection from words
 */
function tooltipLower() {
    var elem = document.getElementById("tooltipLower");
    elem.setAttribute("data-toggle", "tooltip");
    elem.setAttribute("title", tooltip_phrases_lower[Math.floor(Math.random() * tooltip_phrases_lower.length)]);
}

/**
 * Method calls Random Facts API and returns one random fact
 */
async function getRandomFact() {
    // API works but limit of 50k/month
    var url = `https://api.api-ninjas.com/v1/facts`;

    var response = await fetch(url, {
        headers: {
            'X-Api-Key': 'BOmrYruYU3UjGaGA9ih4gw==P7RoEDzc4MNGd4ua'
        }
    }).then(function (response) {
        return response.json();
    }).then(function (data) {
        console.log(data);
        var elem = document.getElementById("random_fact");
        elem.innerText = data[0]["fact"];
        var elem = document.getElementById("tooltipHigher");
        elem.classList.remove("invisible");
        elem = document.getElementById("tooltipLower");
        elem.classList.remove("invisible");
    }).catch(function () {
        console.log("There was an error!");
    });
}

function reloadSite() {
    window.location.reload();
}