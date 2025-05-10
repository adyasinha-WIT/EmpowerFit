$(document).ready(function () {
    console.log("Tracking.js loaded");
    loadLocation();
});

function loadLocation() {
    console.log("Getting location...");
    navigator.geolocation.getCurrentPosition(
        function (position) {
            console.log("Location retrieved:", position.coords.latitude, position.coords.longitude);
            document.getElementById('latitude').innerText = position.coords.latitude;
            document.getElementById('longitude').innerText = position.coords.longitude;
        },
        function (error) {
            console.error("Location information is unavailable", error);
            alert("Unable to retrieve location data");
        }
    );
}