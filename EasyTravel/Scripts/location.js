var placeID = 'ChIJT608vzr5sUARKKacfOMyBqw';

function initMap() {
    var map = new google.maps.Map(document.getElementById('map'), {
        center: { lat: -33.8688, lng: 151.2195 },
        zoom: 13
    });

    var input = document.getElementById('pac-input');

    var autocomplete = new google.maps.places.Autocomplete(input);
    autocomplete.bindTo('bounds', map);

    map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);

    var infowindow = new google.maps.InfoWindow();
    var infowindowContent = document.getElementById('infowindow-content');
    infowindow.setContent(infowindowContent);
    var marker = new google.maps.Marker({
        map: map
    });
    marker.addListener('click', function () {
        infowindow.open(map, marker);
    });

    autocomplete.addListener('place_changed', function () {
        infowindow.close();
        var place = autocomplete.getPlace();
        if (!place.geometry) {
            return;
        }

        if (place.geometry.viewport) {
            map.fitBounds(place.geometry.viewport);
        } else {
            map.setCenter(place.geometry.location);
            map.setZoom(17);
        }

        // Set the position of the marker using the place ID and location.
        marker.setPlace({
            placeId: place.place_id,
            location: place.geometry.location
        });

        marker.setVisible(true);

        placeID = place.place_id; // update global variable
        setForecast(place.place_id);

        infowindowContent.children['place-name'].textContent = place.name;
        infowindowContent.children['place-id'].textContent = place.place_id;
        infowindowContent.children['place-address'].textContent =
            place.formatted_address;
        infowindow.open(map, marker);
    });
}

function getForecastUrl(place_id) {
    var request = new XMLHttpRequest();
    request.open('GET', 'https://forecast7.com/api/getUrl/' + place_id, false);
    request.send(null);
    return request.responseText;
}

function setForecast(place_id) {
    var js, fjs = document.getElementById('weather_script');

    var id = 'weatherwidget-io-js';
    var location = getForecastUrl(place_id);

    var x = document.getElementById('weather-widget');

    x.setAttribute("data-label_1", location.split("/")[1].toUpperCase());
    x.href = "https://forecast7.com/en/" + location + "/";

    // restart the weather widget
    document.getElementById(id).remove();
    if (!document.getElementById(id)) {
        js = document.createElement('script');
        js.id = id;
        js.src = 'https://weatherwidget.io/js/widget.min.js';
        fjs.parentNode.insertBefore(js, fjs);
    }
}
