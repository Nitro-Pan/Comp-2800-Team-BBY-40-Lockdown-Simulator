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

