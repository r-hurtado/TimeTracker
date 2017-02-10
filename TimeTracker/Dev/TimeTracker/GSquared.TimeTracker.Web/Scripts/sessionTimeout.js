var sessionHelper = function () {
    var INITIAL_TIMEOUT_IN_MS = 840000; // 14 Minutes
    var LAST_CHANCE_TIMEOUT = 60000;
    var logoffUrl = void 0
    var timer = void 0;
    var lastChanceTimer = void 0;

    function init() {
        logoffUrl = $.url() + "Account/Logoff";
        
        // Set up the Window
        $("#sessionTimeoutWarning").dialog({
            resizable: false,
            autoOpen: false,
            height: 140,
            width: 350,
            modal: true,
            buttons: {
                Ok: function() {
                    $(this).dialog("close");
                    resetTimeout();
                }
            }
        });
        
        // Reset the document
        resetTimeout();

        // Configure AJAX requests to reset the timeout
        $.ajaxSetup({
            complete: function () {
                return resetTimeout();
            }
        });

        // TODO: See if we need to catch a grid event
    }

    // Resets the Session
    function resetTimeout() {
        if (lastChanceTimer != null) {
            clearTimeout(lastChanceTimer);
        }
        if (timer != null) {
            clearInterval(timer);
        }
        timer = setInterval(showTimeoutWindow, INITIAL_TIMEOUT_IN_MS);
    }

    function redirectToLogoff() {
        return window.location = logoffUrl;
    }

    function showTimeoutWindow() {
        // Show the window
        $("#sessionTimeoutWarning").dialog("open");
        
        // Give the user a minute to respond
        lastChanceTimer = setTimeout(redirectToLogoff, LAST_CHANCE_TIMEOUT);
    }

    return {
        init: init
    };
}();