/** Easter Egg */
let apt = document.getElementById("apartment");
let count = 0;
let dudeWidth = 192;
let dudeHeight = 192;
let width = window.innerWidth;
let height = window.innerHeight;
apt.onclick = countEgg;

function countEgg() {
    count++;
    if (count == 5) {
        startEgg();
    }
}

function startEgg() {
    //Append dudes
    for (let i = 0; i < 5; i++) {
        let dude = document.createElement("IMG");
        dude.className = "person";
        dude.id = "person" + (i + 1);
        dude.src = "images/" + "person" + (i + 1) + ".png";
        dude.style.position = 'absolute';
        dude.style.padding = 0;
        dude.style.top = (height - dudeHeight - (i * 192)) + "px";
        document.body.appendChild(dude);
    }
}



