var firebaseConfig = {
<<<<<<< HEAD
  apiKey: "AIzaSyA5KXbGRt_3OjIo8xtPLbFF8ywoTCffuvU",
  authDomain: "lockdown-simulator.firebaseapp.com",
  databaseURL: "https://lockdown-simulator.firebaseio.com",
  projectId: "lockdown-simulator",
  storageBucket: "lockdown-simulator.appspot.com",
  messagingSenderId: "135884795125",
  appId: "1:135884795125:web:1fdf9ab3ecb48a359da9bc"
=======
    apiKey: "AIzaSyA5KXbGRt_3OjIo8xtPLbFF8ywoTCffuvU",
    authDomain: "lockdown-simulator.firebaseapp.com",
    databaseURL: "https://lockdown-simulator.firebaseio.com",
    projectId: "lockdown-simulator",
    storageBucket: "lockdown-simulator.appspot.com",
    messagingSenderId: "135884795125",
    appId: "1:135884795125:web:1fdf9ab3ecb48a359da9bc"
>>>>>>> 7f7e5c301df029501076ba02d36425d201a932e7
};
// Initialize Firebase
firebase.initializeApp(firebaseConfig);
const db = firebase.database();

let unsortedUsers = [];
let sortedUsers = [];

<<<<<<< HEAD
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

function logout() {
  firebase.auth().signOut();
  window.location.replace("index.html");
}

function deleteAccount() {
  let user = firebase.auth().currentUser;

  user.delete().then(function () {
    // User deleted.
  }).catch(function (error) {
    // An error happened.
  });
}

firebase.auth().onAuthStateChanged((user) => {
  if (user) {
    // User logged in already or has just logged in.
    //Change log in button text to log out
    let btn = document.getElementById('loginbutton');
    btn.innerHTML = "LOG OUT";
    btn.onclick = logout;
    //Fill out profile page
    db.ref(user.uid).once('value').then(function (snapshot) {
      let email = document.createTextNode(snapshot.val().name);
      let h1 = document.createElement("h1");
      h1.append(email);
      document.getElementById('profile').append(h1);
      let score = document.createTextNode("Score: " + snapshot.val().score);
      let h2 = document.createElement("h2");
      h2.append(score);
      document.getElementById('profile').append(h2);
    });
  } else {
    // User not logged in or has just logged out
  }
});

document.getElementById('deletebutton').onclick = deleteAccount;

=======
function grabUserData() {
    let users = db.ref('Players').orderByKey();
    users.once("value")
        .then(function (snapshot) {
            snapshot.forEach(function (childSnapshot) {
                let key = childSnapshot.key;
                //console.log(key); //userid
                let data = childSnapshot.val();
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
>>>>>>> 7f7e5c301df029501076ba02d36425d201a932e7
