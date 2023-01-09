// const currencies = [
// 	{
// 		name: "Bitcoin",
// 		name2: "BTC",
// 		logo: "./imgs/bitcoin.png",
// 	},
// 	{
// 		name: "bbb",
// 		name2: "bbb2",
// 		logo: "./imgs/bitcoin.png",
// 	},
// 	{
// 		name: "ccc",
// 		name2: "ccc2",
// 		logo: "./imgs/bitcoin.png",
// 	},
// ];

let from = document.getElementById("chat-link");
let chat = document.getElementById("chat");
let section = document.getElementById("info-section");

from.onclick = showChat;

function showChat(e) {
	e.preventDefault();
	console.log("here showCurrenciesList");

	// chat.classList.remove("slide-out-top");

	if (chat.classList.contains("hidden")) {
		chat.classList.remove("hidden");
	} else {
		chat.classList.add("hidden");
	}

	// currency1.style.height = "fit-content";
}

function hideCurrenciesList() {}
