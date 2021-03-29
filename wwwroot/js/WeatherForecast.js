$(document).ready(function () {
    $.ajax({
        url: '/WeatherForecast',
        success: function (data) {
            let tommorrow = data[0];
            console.log(tommorrow);
            let date = new Date(tommorrow.date);
            $('#date').text(date.toDateString());
            $('#temperature').text(tommorrow.temperatureC + ' C');
            $('#summary').text(tommorrow.summary);
        },
        error: function (data) {
            alert('Failed to load data');
        }
    });
});