var projectHelper = function () {

	function init() {
		var lastsel;
		// Set up the grid
		$("#projects").jqGrid({
			url: $.url() + 'Project/GetProjects',
			editurl: $.url() + 'Project/SaveProject',
			datatype: "json",
			colNames: ['Project Id', 'Client', 'Project Name', 'Hourly Rate', 'Billing Code', 'Quickbooks Project', 'Is Active', ''],
			colModel: [
				{
					name: 'ProjectId',
					index: 'ProjectId',
					key: true,
					hidden: true
				},
				{
					name: 'ClientId',
					index: 'ClientName',
					width: 200,
					jsonmap: "ClientName",
					editable: true,
					edittype: "select",
					editoptions: {
						dataUrl: $.url() + "Project/GetClientsDropDown"
					},
					editrules: { required: true }
				},
				{
					name: 'ProjectName',
					index: 'ProjectName',
					width: 300,
					jsonmap: "ProjectName",
					editable: true,
					editrules: { required: true }
				},
				{
					name: 'HourlyBillingRate',
					index: 'HourlyBillingRate',
					jsonmap: 'HourlyBillingRate',
					width: 100,
					formatter: "currency",
					editable: true,
					align: "right",
					editrules: { required: true }
				},
				{
					name: 'BillingCode',
					index: 'BillingCode',
					width: 100,
					jsonmap: "BillingCode",
					editable: true
				},
				{
					name: 'QuickbooksProjectId',
					index: 'QuickbooksProjectId',
					width: 300,
					jsonmap: "QuickbooksProjectId",
					editable: true
				},
				{
					name: 'IsActive',
					index: 'IsActive',
					jsonmap: "IsActive",
					width: 75,
					sortable: false,
					editable: true,
					align: "center",
					formatter: "checkbox",
					edittype: "checkbox",
					editoptions: { value: "true:false", defaultValue: "true" }
				},
				{
					name: "Action",
					width: 26,
					sortable: false,
					editable: false,
					align: "center"
				}
			],
			rowNum: 25,
			rowList: [25, 50, 100, 200],
			pager: '#projectsMap',
			viewrecords: true,
			jsonReader: {
				repeatitems: false,
				id: "0",
				page: "CurrentPage",
				total: "TotalPages",
				records: "RecordCount",
				root: "Rows"
			},
			caption: "Clients",
			height: '500',
			onSelectRow: function (id) {
				if (id && id !== lastsel) {
					jQuery('#projects').jqGrid('restoreRow', lastsel);
					jQuery('#projects').jqGrid('editRow', id, true);
					lastsel = id;
				}
			},
			gridComplete: function () {
				var ids = jQuery("#projects").jqGrid('getDataIDs');
				for (var i = 0; i < ids.length; i++) {
					$("#projects").jqGrid('setRowData', ids[i], { Action: "<button class='deleteButton' data-rowId='" + ids[i] + "' ></button>" });
				}

				// Hook up the click events
				$(".deleteButton").on("click", function () {
					var rowId = $(this).attr("data-rowId");
					$("#projects").jqGrid("delGridRow", rowId);
				});
			}
		});

		// Set up editing on the grid
		$("#projects").jqGrid('navGrid', '#projectsMap', { edit: false, add: false, del: false });
		$("#projects").jqGrid('inlineNav', "#projectsMap", { add: false, edit: false });

		// Set up the Add Button
		$("#btnAddProject").on("click", function () {
			$("#projects").jqGrid('addRow', { addRowParams: { keys: true }, reloadAfterSubmit: true });
			var rowid = $("#projects").jqGrid('getGridParam', 'selrow');

			// Set the focus to the Description field
			$("#" + rowid + "_ClientName").focus();
		});

		// Set the focus to the Add button
		$("#btnAddProject").focus();
	}

	return {
		init: init
	};
}();