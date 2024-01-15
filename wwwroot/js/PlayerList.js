var ctx = document.getElementById('playerChart').getContext('2d');

var playerData = @Html.Raw(Json.Serialize(Model));
// Group player data by week
var groupedData = playerData.reduce(function (acc, player) {
    var week = moment(player.createdDate).format('YYYY-WW');
    if (!acc[week]) {
        acc[week] = 0;
    }
    acc[week]++;
    return acc;
}, {});

var weeks = Object.keys(groupedData);
var playerCounts = weeks.map(function (week) {
    return groupedData[week];
});
var maxCount = Math.max(playerCounts);
var suggestedMax = Math.ceil(maxCount * 1.2);
// Perform necessary processing on playerData to get the weekly counts

var chart = new Chart(ctx, {
    type: 'bar',
    data: {
        labels: weeks,
        datasets: [{
            label: 'Number of Players Joining Each Week',
            data: playerCounts,
            backgroundColor: 'rgba(75, 192, 192, 0.2)',
            borderColor: 'rgba(75, 192, 192, 1)',
            borderWidth: 1
        }]
    },
    options: {
        scales: {
            y: {
                beginAtZero: true,
                suggestedMax: suggestedMax + 5
            }
        }
    }
});