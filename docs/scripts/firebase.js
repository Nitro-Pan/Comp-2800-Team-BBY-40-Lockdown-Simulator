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

function grabUserData() {
    let users = db.ref('Players').orderByKey();
    users.once("value")
        .then(function (snapshot) {
            snapshot.forEach(function (childSnapshot) {
                let key = childSnapshot.key;
                //console.log(key); //userid
                let data = childSnapshot.val();
                console.log(data);
                //console.log(data); //user score and username
                unsortedUsers.push(data);
            })
        });
    console.log("abc");
    console.log(unsortedUsers);
    sortedUsers = unsortedUsers.sort(function (a, b) {
        console.log("abcd");
        console.log(a.score);
        return a.score - b.score;
    });
}

grabUserData();
console.log(unsortedUsers);
console.log(unsortedUsers[0]);




////////////////////////////////////////////////
//Test function for writing data into database
function writeUserData(userId, name, score) {
    db.ref('Players/' + userId).set({
        username: name,
        score: score
    });
}
/**
writeUserData("player1", "bob", 20);
writeUserData("player2", "dylan", 30);
writeUserData("player3", "jon", 7);
*/
////////////////////////////////////////////////


/**
 * Show leaderboard
 
function showLeaderboard() {
    let docRef = db.collection("Players");
    docRef.orderBy("score", "desc").limit(10)
        .get()
        .then(function (querySnapshot) {
            querySnapshot.forEach(function (doc) {
                let score = doc.data().name + "-----" + doc.data().score;
                let p = document.createElement("p");
                let node = document.createTextNode(score);
                p.appendChild(node);
                document.body.appendChild(p);
            });
        });
}
showLeaderboard();
*/