var itimHelper = function() {

	function init() {
		// Set up the Comboboxes
		$("#ClientId").combobox({
			source: function (request, response) {
				$.ajax({
					url: $.url() + "Invoicing/GetClientList",
					dataType: "json",
					type: "POST",
					data: {
						contains: request.term
					},
					success: function(data) {
						response($.map(data, function(client) {
							return {
								label: client.label,
								value: client.value
							};
						}));
					}
				});
			},
			minLength: 2
		});
		$("#ItimeMonth").combobox({
			width: 35
		});
		$("#ItimeYear").combobox({
			width: 50
		});

		// Set up the button
		$("#btnGetItimeData").button().click(function(e) {
			var url = $.url() + "Invoicing/GetITIMData";
			$.ajax({
				url: url,
				type: "POST",
				data: {
					clientId: $("#ClientId").val(),
					month: $("#ItimeMonth").val(),
					year: $("#ItimeYear").val()
				},
				success: function(data) {
					$("#itimData").val(data);
				}
			});
		});
	}

	return {
		init: init
	};
}();