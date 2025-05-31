$(document).ready(function () {
    var map = L.map('map').setView([51.505, -0.09], 13);

    L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 19,
        attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
    }).addTo(map);

    var marker;

    if (navigator.geolocation) {
        navigator.geolocation.watchPosition(
            function (position) {
                var lat = position.coords.latitude;
                var long = position.coords.longitude;
                var accuracy = position.coords.accuracy;

                // If marker already exists, just update its position
                if (marker) {
                    marker.setLatLng([lat, long]);
                } else {
                    marker = L.marker([lat, long]).addTo(map);
                }

                // Center map on current location
                map.setView([lat, long], 16);

                // Optionally add/update accuracy circle or other features here

            },
            function (error) {
                alert("Unable to retrieve your location: " + error.message);
                console.error("Geolocation error:", error.message);
            },
            {
                enableHighAccuracy: true,
                maximumAge: 0,
                timeout: 30000
            }
        );
    } else {
        alert("Geolocation is not supported by this browser.");
    }
});
