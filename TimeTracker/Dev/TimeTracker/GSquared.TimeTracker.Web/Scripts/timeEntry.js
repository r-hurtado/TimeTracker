var timeEntryHelper = function() {

	function init() {
		var lastsel;
		// Set up the grid
		$("#timeEntries").jqGrid({
			url: $.url() + 'TimeEntry/GetTimeEntries',
			editurl: $.url() + 'TimeEntry/SaveTimeEntry',
			datatype: "json",
			colNames: ['Time Entry Id', 'Project', 'Description', 'Date Worked', 'From Time', 'To Time', 'Total Time', 'Billable', ''],
			colModel: [
				{
					name: 'TimeEntryId',
					index: 'TimeEntryId',
					key: true,
					hidden: true
				},
				{
					name: 'ProjectId',
					index: 'ProjectDisplayName',
					width: 375,
					jsonmap: "ProjectDisplayName",
					editable: true,
					edittype: "select",
					editoptions: {
						dataUrl: $.url() + "TimeEntry/GetProjectClientDropDown"
					}
				},
				{
					name: 'Description',
					index: 'Description',
					width: 325,
					editable: true,
					editrules: { required: true}
				},
				{
					name: 'DateWorked',
					index: 'DateWorked',
					width: 100,
					jsonmap: "DateWorked",
					align: "right",
					sorttype: "date",
					editable: true,
					editoptions: { style: "text-align: right !important", defaultValue: (new Date()).toString("MM/dd/yyyy") },
					datefmt: "M/d/yyyy",
					editrules: { required: true, date: true }
				},
				{
					name: 'FromTime',
					index: 'FromTime',
					width: 75,
					align: "right",
					editable: true,
					editoptions: { style: "text-align: right !important", defaultValue: (new Date()).toString("h:mm tt"), dataEvents: [{ type: "blur", fn: calculateTotalTime }] },
					editrules: { required: true }
				},
				{
					name: 'ToTime',
					index: 'ToTime',
					width: 75,
					align: "right",
					editable: true,
					editoptions: { style: "text-align: right !important", dataEvents: [{ type: "blur", fn: calculateTotalTime }] }
				},
				{
					name: 'TotalTime',
					index: 'TotalTime',
					width: 75,
					align: "right",
					editable: true,
					editoptions: { style: "text-align: right !important", defaultValue: "0" },
					editrules: { required: true, number: true }
				},
				{
					name: 'Billable',
					index: 'Billable',
					jsonmap: "IsBillable",
					width: 50,
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
			pager: '#timeEntriesMap',
			viewrecords: true,
			jsonReader: {
				repeatitems: false,
				id: "0",
				page: "CurrentPage",
				total: "TotalPages",
				records: "RecordCount",
				root: "Rows"
			},
			caption: "Time Entries",
			height: '300',
			onSelectRow: function(id) {
				if (id && id !== lastsel) {
					jQuery('#timeEntries').jqGrid('restoreRow', lastsel);
					jQuery('#timeEntries').jqGrid('editRow', id, true, addDatePicker);
					lastsel = id;
				}
			},
			gridComplete: function() {
				var ids = jQuery("#timeEntries").jqGrid('getDataIDs');
				for (var i = 0; i < ids.length; i++) {
					$("#timeEntries").jqGrid('setRowData', ids[i], { Action: "<button class='deleteButton' data-rowId='" + ids[i] + "' ></button>" });
				}

				// Hook up the click events
				$(".deleteButton").on("click", function () {
				    var rowId = $(this).attr("data-rowId");
					$("#timeEntries").jqGrid("delGridRow", rowId);
				});
			}
		});

		// Set up editing on the grid
		$("#timeEntries").jqGrid('navGrid', '#timeEntriesMap', { edit: false, add: false, del: false });
		$("#timeEntries").jqGrid('inlineNav', "#timeEntriesMap", { add: false, edit: false });
		$("#timeEntries").jqGrid('saveRow', lastsel, {
		    
		});
		
		// Set up the Add Button
		$("#btnAddTimeEntry").on("click", function() {
		    $("#timeEntries").jqGrid('addRow', {
		        addRowParams: {
		            keys: true,
		            aftersavefunc: function () {
		                // Refresh the grid after editing
		                $("#timeEntries").trigger("reloadGrid");
		                return true;
		            }
		        }
		    });
			var rowid = $("#timeEntries").jqGrid('getGridParam', 'selrow');
			
			// Set the focus to the Description field
			$("#" + rowid + "_Description").focus();
		});
		
		// Set the focus to the Add button
		$("#btnAddTimeEntry").focus();
	}

	function addDatePicker(id) {
		// Add the Date Picker control
	    $("#" + id + "_DateWorked", "#timeEntries").datepicker({ dateFormat: "m/d/yy" });
	    
	    // Set up the Combobox
	    $("#" + id + "_ProjectId", "#timeEntries").combobox({
	        minLength: 2
	    });
	}

	// Calculate the total time worked
	function calculateTotalTime() {
		var rowid = $("#timeEntries").jqGrid('getGridParam', 'selrow');
		var totalField = $("#" + rowid + "_TotalTime");
		var fromTime = convertInputToTime($("#" + rowid + "_FromTime").val());
		var toTime = convertInputToTime($("#" + rowid + "_ToTime").val());

		// Update the fields
		$("#" + rowid + "_FromTime").val(fromTime);
		$("#" + rowid + "_ToTime").val(toTime);

		if (fromTime == '' || toTime == '') {
			totalField.val(0);
		}
		else {

			var span = new TimeSpan(Date.parse(toTime) - Date.parse(fromTime));
			var roundedTime = span.getHours() + roundToQuarterHour(span.getMinutes());
			totalField.val(roundedTime);
		}
	}
	
	// Convert potential input to a valid date and time
	function convertInputToTime(input) {
		var formattedInput = input.trim().toLowerCase().replace(" ", "");

		if (input.toLowerCase() == "now") {
			return (new Date()).toString("h:mm tt");
		}
		
		switch(formattedInput.length) {
			case 3:
				if (formattedInput.slice(-1) == "p" || formattedInput.slice(-1) == "a") {
					return formattedInput.substring(0, 1) + formattedInput.slice(-3).replace("p", " PM").replace("a", " AM");
				} else {
					return formattedInput.substring(0, 1) + ":" + formattedInput.slice(-2) + " AM";
				}
			case 4:
				if (formattedInput.slice(-1) == "p" || formattedInput.slice(-1) == "p") {
					return formattedInput.substring(0, 1) + ":" + formattedInput.slice(-3).replace("p", " PM");
				} else {
					if (formattedInput.substring(0, 2) > 12) {
						return formattedInput.substring(0, 2) - 12 + ":" + formattedInput.slice(-2) + " PM";
					} else {
						return formattedInput.substring(0, 2) + ":" + formattedInput.slice(-2) + " AM";
					}
				}
			case 5:
				if (formattedInput.slice(-1) == "p") {
					return formattedInput.substring(0, 2) + ":" + formattedInput.slice(-3).replace("p", " PM");
				} else {
					return formattedInput.substring(0, 2) + ":" + formattedInput.slice(-3).replace("a", "") + " AM";
				}
			default:
				return input.replace("  ", " ").trim();
		}
	}

	// Rounds the number to the nearest quarter hour
	function roundToQuarterHour(minutes) {
		if (minutes <= 7) {
			return .00;
		}
		else if (minutes <= 22) {
			return .25;
		}
		else if (minutes <= 37) {
			return .5;
		}
		else if (minutes <= 52) {
			return .75;
		}
		else {
			return 1;
		}
	}

	return {
		init: init
	};
}();
