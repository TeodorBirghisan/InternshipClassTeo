function refreshWeatherForecast() {
    $(document).ready(function () {
        $.ajax({
            url: '/WeatherForecast',
            success: function (data) {
                let tommorrow = data[0];
                let date = new Date(tommorrow.date).toDateString();
                $('#date').text(date);
                $('#temperature').text(tommorrow.temperatureC + ' C');
                $('#summary').text(tommorrow.summary);
            },
            error: function (data) {
                alert('Failed to load data');
            }
        });
    });
}


setInterval(refreshWeatherForecast, 3000);