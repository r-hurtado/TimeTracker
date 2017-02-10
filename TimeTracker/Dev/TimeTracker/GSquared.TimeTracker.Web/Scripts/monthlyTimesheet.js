var monthlyTimesheetHelper = function() {

    function init() {
        // Set up the Date Pickers
        $("#fromDate").datepicker();
        $("#toDate").datepicker();

        // Set up the button
        $("#btnGetMonthlyTimeSheet").button().click(function (e) {
            $.fileDownload($.url() + "Reports/GetMonthlyTimesheet", {
                //preparingMessageHtml: "Preparing your Timesheet, please wait...",
                failMessageHtml: "There was a problem generating your Timesheet, please try again.",
                httpMethod: "POST",
                data: {
                    fromDate: $("#fromDate").val(),
                    toDate: $("#toDate").val()
                }
            });

            e.preventDefault(); //otherwise a normal form submit would occur
        });
    }

    return {
        init: init
    };
}();