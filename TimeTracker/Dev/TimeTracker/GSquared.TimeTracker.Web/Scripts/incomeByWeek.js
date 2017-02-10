var incomeByWeekHelper = function() {

    function init() {
        var dataUrl = $.url() + "Reports/GetWeeklyIncomeData";
        
        var ajaxDataRenderer = function (url) {
            var ret = null;
            $.ajax({
                async: false,
                url: url,
                dataType: "json",
                success: function (data) {
                    var dataLines = [];

                    var dataLabels = "";

                    $.each(data, function (entryindex, entry) {
                        dataLines.push(entry['data']);
                        dataLabels = dataLabels + entry['Name'];
                    });
                    ret = dataLines;
                }
            });
            return ret;
        };

        // Set up the chart
        $.jqplot("reportChart", dataUrl, {
            title: "Billable Time by Week",
            dataRenderer: ajaxDataRenderer,
            seriesDefaults: {
                renderer: $.jqplot.BarRenderer,
                showMarker:false,
                pointLabels: { show:true }
            },
            series: [{ pointLabels: { show: true, formatString: "$%#.2f" } }, { pointLabels: { show: true, formatString: "%#.2f"}}],
            axesDefaults: {
                tickRenderer: $.jqplot.CanvasAxisTickRenderer
            },
            axes: {
                xaxis: {
                    renderer: $.jqplot.CategoryAxisRenderer
                },
                yaxis: {
                    autoscale: true
                }
            }
        });
    }

    return {
        init: init
    };
}();