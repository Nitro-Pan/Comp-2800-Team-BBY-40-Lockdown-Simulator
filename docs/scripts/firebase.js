var firebaseConfig = {
    apiKey: "AIzaSyA5KXbGRt_3OjIo8xtPLbFF8ywoTCffuvU",
    authDomain: "lockdown-simulator.firebaseapp.com",
    databaseURL: "https://lockdown-simulator.firebaseio.com",
    projectId: "lockdown-simulator",
    storageBucket: "lockdown-simulator.appspot.com",
    messagingSenderId: "135884795125",
    appId: "1:135884795125:web:1fdf9ab3ecb48a359da9bc"
};
// Initialize Firebase
firebase.initializeApp(firebaseConfig);
const db = firebase.database();

let unsortedUsers = [];
let sortedUsers = [];

function printUsers() {
    let rootRef = db.ref().orderByChild("score");
    rootRef.on('value', function (snapshot) {
        snapshot.forEach(function (childSnapshot) {
            let parent = document.getElementById("end").parentNode;
            let childData = childSnapshot.val();
            let score = childData["name"] + " ------- " + childData["score"];
            let node = document.createTextNode(score);
            let h4 = document.createElement("h4");
            h4.append(node);
            parent.prepend(h4);
        });
    });
}

printUsers();
