var menuHelper = function() {

	function init() {
		// Set up the Main Menu
		$("#mainMenu").menu({ position: { my: "left top", at: "left bottom" } });
		$("#mainMenu").removeClass("ui-corner-all");
	}

	return {
		init: init
	};
}();