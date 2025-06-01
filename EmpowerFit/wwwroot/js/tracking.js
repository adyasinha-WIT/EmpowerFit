$(document).ready(function () {
    var map = L.map('map').setView([51.505, -0.09], 13);

    L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 19,
        attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
    }).addTo(map);

    var marker, currentLat, currentLong;

    if (navigator.geolocation) {
        navigator.geolocation.watchPosition(
            function (position) {
                var lat = position.coords.latitude;
                var long = position.coords.longitude;
                var accuracy = position.coords.accuracy;
                currentLat = lat;
                currentLong = long;


                if (marker) {
                    marker.setLatLng([lat, long]);
                } else {
                    marker = L.marker([lat, long]).addTo(map);
                }


                map.setView([lat, long], 16);

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

    document.getElementById('sendLocation').addEventListener('click', function () {
        if (currentLat !== undefined && currentLong !== undefined) {
            const url = '/Premium/Tracking/SendLocation';
            const data = {
                latitude: currentLat,
                longitude: currentLong
            };
            fetch(url, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(data)
            })
                .then(response => {
                    if (response.ok) {
                        alert("Alert message sent out");
                    } else {
                        alert("Error sending message");
                    }
                })
                .catch(() => {
                    alert("Error sending message");
                });
        } else {
            alert('Location not ready yet, please wait.');
        }
    });

}); 