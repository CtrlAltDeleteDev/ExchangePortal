const coll = document.getElementById("collapse_navbar");
let i;
const content = document.getElementById("collapse_navbar_content");

const sections = document.getElementsByTagName("section");

Array.from(sections).forEach(element => {
	element.addEventListener("click", function () {
		content.style.display = "none";
	});
});

coll.addEventListener("click", function () {
	this.classList.toggle("active");
	if (content.style.display === "block") {
		content.style.display = "none";
	} else {
		content.style.display = "block";
	}
});
