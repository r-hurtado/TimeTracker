var quickbooksTimesheetExportHelper = function() {
	function init() {
		// Set up the Date Pickers
		$("#FromDate").datepicker();
		$("#ToDate").datepicker();

		// Set up the button
		$("#btnExportTimesheetData").button().click(function() {
			var url = $.url() + "Invoicing/GetQuickbooksTimesheetExport?fromDate=" + $("#FromDate").val() + "&toDate=" + $("#ToDate").val();
			window.location = url;
		});
	}

	return {
		init: init
	};
}();