let map;
let marker;
let coordinates = []; 
let currentCoordinateIndex = 0;

window.initializeMap = function (apiKey, defaultCoordinates) {

    return new Promise((resolve, reject) => {
        if (typeof google !== 'undefined' && typeof google.maps !== 'undefined') {
            resolve();
            return;
        }

        const script = document.createElement('script');

        script.src = `https://maps.googleapis.com/maps/api/js?key=${apiKey}&libraries=geometry&callback=onMapApiLoaded`;
        script.async = true;
        script.defer = true;
        document.head.appendChild(script);

        window.onMapApiLoaded = () => {
            map = new google.maps.Map(document.getElementById("map"), {
                center: new google.maps.LatLng(defaultCoordinates.lat, defaultCoordinates.lng),
                zoom: 18,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            });
            resolve();
        };

        script.onerror = () => reject(new Error('Google Maps script failed to load'));
    });
}

window.initializeMarker = function (initialCoord) {
    var busIcon = 'images/busicon.webp';
    marker = new google.maps.Marker({
        position: new google.maps.LatLng(initialCoord.lat, initialCoord.lng),
        map: map,
        title: "Bus Location",
        icon: busIcon
    });
}

window.loadRoute = function (routeCoords) {
    coordinates = routeCoords;
    currentCoordinateIndex = 0;
    moveMarkerToNextCoord();
}

window.moveMarkerToNextCoord = function () {
    if (currentCoordinateIndex < coordinates.length) {
        let nextCoord = coordinates[currentCoordinateIndex];
        animateMarkerToPosition(nextCoord, () => {
            currentCoordinateIndex++;
            if (currentCoordinateIndex < coordinates.length) {
                setTimeout(moveMarkerToNextCoord, 1000);
            }
        });
    }
}

window.animateMarkerToPosition = function (newPosition, onComplete) {
    var startPos = marker.getPosition();
    var endPos = new google.maps.LatLng(newPosition.lat, newPosition.lng);
    var duration = calculateDuration(startPos, endPos, 50);
    var start = new Date().getTime();

    var animate = function () {
        var now = new Date().getTime();
        var elapsed = now - start;
        var fraction = elapsed / duration;

        if (fraction < 1) {
            var lat = startPos.lat() + (endPos.lat() - startPos.lat()) * fraction;
            var lng = startPos.lng() + (endPos.lng() - startPos.lng()) * fraction;
            var nextPos = new google.maps.LatLng(lat, lng);

            marker.setPosition(nextPos);
            map.setCenter(nextPos);

            requestAnimationFrame(animate);
        } else {
            marker.setPosition(endPos);
            map.setCenter(endPos);

            if (onComplete && typeof onComplete === 'function') {
                onComplete();
            }
        }
    };

    animate();
}

window.calculateDuration = function (startPos, endPos, speed) {
    var distance = google.maps.geometry.spherical.computeDistanceBetween(startPos, endPos);
    var durationInSeconds = distance / speed;
    return durationInSeconds * 1000;
}
