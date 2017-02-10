var lookupsHelper = function() {

    function init() {
        var lastsel;
        // Set up the grid
        $("#grdBillingTerms").jqGrid({
            url: $.url() + 'Lookups/GetBillingTerms',
            editurl: $.url() + 'Lookups/SaveBillingTerm',
            datatype: "json",
            colNames: ['Id', 'Billing Term', 'Days to Pay', ''],
            colModel: [
				{
				    name: 'BillingTermsId',
				    index: 'BillingTermsId',
				    key: true,
				    hidden: true
				},
				{
				    name: 'BillingTermsDescription',
				    index: 'BillingTermsDescription',
				    width: 150,
				    jsonmap: "BillingTermsDescription",
				    editable: true
				},
				{
				    name: 'NumberOfDaysToPay',
				    index: 'NumberOfDaysToPay',
				    jsonmap: 'NumberOfDaysToPay',
				    width: 150,
				    editable: true,
				    align: "right"
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
            pager: '#grdBillingTermsMap',
            viewrecords: true,
            jsonReader: {
                repeatitems: false,
                id: "0",
                page: "CurrentPage",
                total: "TotalPages",
                records: "RecordCount",
                root: "Rows"
            },
            caption: "Billing Terms",
            height: '300',
            onSelectRow: function (id) {
                if (id && id !== lastsel) {
                    jQuery('#grdBillingTerms').jqGrid('restoreRow', lastsel);
                    jQuery('#grdBillingTerms').jqGrid('editRow', id, true);
                    lastsel = id;
                }
            },
            gridComplete: function () {
                var ids = jQuery("#grdBillingTerms").jqGrid('getDataIDs');
                for (var i = 0; i < ids.length; i++) {
                    $("#grdBillingTerms").jqGrid('setRowData', ids[i], { Action: "<button class='deleteButton' data-rowId='" + ids[i] + "' ></button>" });
                }

                // Hook up the click events
                $(".deleteButton").on("click", function () {
                    var rowId = $(this).attr("data-rowId");
                    $("#grdBillingTerms").jqGrid("delGridRow", rowId);
                });
            }
        });

        // Set up editing on the grid
        $("#grdBillingTerms").jqGrid('navGrid', '#grdBillingTermsMap', { edit: false, add: false, del: false });
        $("#grdBillingTerms").jqGrid('inlineNav', "#grdBillingTermsMap", { add: false, edit: false });

        // Set up the Add Button
        $("#btnAddBillingTerm").on("click", function () {
            $("#grdBillingTerms").jqGrid('addRow', { addRowParams: { keys: true }, reloadAfterSubmit: true });
            var rowid = $("#grdBillingTerms").jqGrid('getGridParam', 'selrow');

            // Set the focus to the Description field
            $("#" + rowid + "_ClientName").focus();
        });

        // Set the focus to the Add button
        $("#btnAddBillingTerm").focus();
    }

    return {
        init: init
    };
}();