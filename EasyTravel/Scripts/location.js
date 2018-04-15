var placeID = 'ChIJT608vzr5sUARKKacfOMyBqw';
var type_preferences = ["-", "-", "-", "-"];

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

        type_preferences = place_preferences.split(",");

        var request0 = {
            location: place.geometry.location,
            radius: '500',
            type: type_preferences[0]
        };

        var request1 = {
            location: place.geometry.location,
            radius: '500',
            type: type_preferences[1]
        };

        var request2 = {
            location: place.geometry.location,
            radius: '500',
            type: type_preferences[2]
        };

        var request3 = {
            location: place.geometry.location,
            radius: '500',
            type: type_preferences[3]
        };

        service = new google.maps.places.PlacesService(map);
        service.nearbySearch(request0, callback0);
        service.nearbySearch(request1, callback1);
        service.nearbySearch(request2, callback2);
        service.nearbySearch(request3, callback3);

        infowindowContent.children['place-name'].textContent = place.name;
        //   infowindowContent.children['place-id'].textContent = place.place_id;
        infowindowContent.children['place-address'].textContent =
            place.formatted_address;
        infowindow.open(map, marker);
    });
    function callback0(results, status) {
        if (status == google.maps.places.PlacesServiceStatus.OK) {
            document.getElementById("category0").innerHTML = type_preferences[0];
            document.getElementById("place0").innerHTML = results[0].name;
            document.getElementById("rating0").innerHTML = "Rating: " + results[0].rating;
        }
    }
    function callback1(results, status) {
        if (status == google.maps.places.PlacesServiceStatus.OK) {
            document.getElementById("category1").innerHTML = type_preferences[1];
            document.getElementById("place1").innerHTML = results[0].name;
            document.getElementById("rating1").innerHTML = "Rating: " + results[0].rating;
        }
    }
    function callback2(results, status) {
        if (status == google.maps.places.PlacesServiceStatus.OK) {
            document.getElementById("category2").innerHTML = type_preferences[2];
            document.getElementById("place2").innerHTML = results[0].name;
            document.getElementById("rating2").innerHTML = "Rating: " + results[0].rating;
        }
    }
    function callback3(results, status) {
        if (status == google.maps.places.PlacesServiceStatus.OK) {
            document.getElementById("category3").innerHTML = type_preferences[3];
            document.getElementById("place3").innerHTML = results[0].name;
            document.getElementById("rating3").innerHTML = "Rating: " + results[0].rating;
        }
    }
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