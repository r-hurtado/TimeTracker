var clientHelper = function() {

	function init() {
		var lastsel;
		// Set up the grid
		$("#clients").jqGrid({
			url: $.url() + 'Client/GetClients',
			editurl: $.url() + 'Client/SaveClient',
			datatype: "json",
			colNames: ['Client Id', 'Client Name', 'Billing Terms', 'Billing Cycle', 'Is Active', ''],
			colModel: [
				{
					name: 'ClientId',
					index: 'ClientId',
					key: true,
					hidden: true
				},
				{
					name: 'ClientName',
					index: 'ClientName',
					width: 350,
					jsonmap: "ClientName",
					editable: true
				},
				{
					name: 'BillingTermsId',
					index: 'BillingTermsDescription',
					jsonmap: 'BillingTermsDescription',
					width: 100,
					editable: true,
					editrules: { required: true},
					edittype: "select",
					editoptions: {
						dataUrl: $.url() + "Client/GetBillingTermsDropDown"
					}
				},
				{
					name: 'BillingCycleId',
					index: 'BillingCycleDescription',
					width: 150,
					jsonmap: "BillingCycleDescription",
					editable: true,
					edittype: "select",
					editoptions: {
						dataUrl: $.url() + "Client/GetBillingCyclesDropDown"
					}
				},
				{
					name: 'IsActive',
					index: 'IsActive',
					jsonmap: "IsActive",
					width: 100,
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
			pager: '#clientsMap',
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
			height: '300',
			onSelectRow: function(id) {
				if (id && id !== lastsel) {
					jQuery('#clients').jqGrid('restoreRow', lastsel);
					jQuery('#clients').jqGrid('editRow', id, true);
					lastsel = id;
				}
			},
			gridComplete: function() {
				var ids = jQuery("#clients").jqGrid('getDataIDs');
				for (var i = 0; i < ids.length; i++) {
					$("#clients").jqGrid('setRowData', ids[i], { Action: "<button class='deleteButton' data-rowId='" + ids[i] + "' ></button>" });
				}

				// Hook up the click events
				$(".deleteButton").on("click", function () {
					var rowId = $(this).attr("data-rowId");
					$("#clients").jqGrid("delGridRow", rowId);
				});
			}
		});

		// Set up editing on the grid
		$("#clients").jqGrid('navGrid', '#clientsMap', { edit: false, add: false, del: false });
		$("#clients").jqGrid('inlineNav', "#clientsMap", {add: false, edit: false });
		
		// Set up the Add Button
		$("#btnAddClient").on("click", function() {
			$("#clients").jqGrid('addRow', { addRowParams: { keys: true }, reloadAfterSubmit: true });
			var rowid = $("#clients").jqGrid('getGridParam', 'selrow');
			
			// Set the focus to the Description field
			$("#" + rowid + "_ClientName").focus();
		});
		
		// Set the focus to the Add button
		$("#btnAddClient").focus();
	}

	return {
		init: init
	};
}();