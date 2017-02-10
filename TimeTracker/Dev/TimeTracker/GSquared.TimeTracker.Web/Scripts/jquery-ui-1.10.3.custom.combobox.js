(function ($) {
	$.widget("custom.combobox", {
		options: {
			source: null,
			width: "auto"
		},
		_create: function() {
			var self = this,
			    select = this.element.hide(),
			    selected = select.children(":selected"),
			    value = selected.val() ? selected.text() : "";

			var controlId = select.attr("id");
			select.attr("id", "combobox_" + controlId);

			var hidden = this.hidden = $("<input type='hidden'>").attr("id", controlId).val(value).insertAfter(select);

			var input = this.input = $("<input>").insertAfter(hidden).val(value).autocomplete({
				delay: 0,
				minLength: 0,
				source: self.options.source != null ?
								self.options.source :
								function (request, response ) {
									var matcher = new RegExp( $.ui.autocomplete.escapeRegex(request.term), "i" );
									response(select.children("option").map(function() {
										var text = $(this).text();
										if (this.value && (!request.term || matcher.test(text)))
											return {
												label: text,
												value: text,
												option: this
											};
									}));
								},
				//selected index
				select: function (event, ui) {
					//debugger;
					//alert(ui.item.value);
					event.preventDefault();
					$(hidden).val(ui.item.value);
					$(this).val(ui.item.label);
				},
			}).addClass("ui-widget ui-widget-content ui-corner-left ui-state-default custom-combobox-input").attr("style", "width:" + self.options.width + ";");

			this.button = $("<button type='button'>&nbsp;</button>").attr("tabIndex", -1).attr("title", "Show All Items").insertAfter(input).button({
				icons: {
					primary: "ui-icon-triangle-1-s"
				},
				text: false
			}).removeClass("ui-corner-all").addClass("ui-corner-right ui-button-icon custom-combobox-toggle").click(function () {
				// close if already visible
				if (input.autocomplete("widget").is(":visible")) {
					input.autocomplete("close");
					return;
				}

				// work around a bug (likely same cause as #5265)
				$(this).blur();

				// pass empty string as value to search for, displaying all results
				console.log($(this));
				console.log(input);
				//debugger;
				input.autocomplete("search", input.val());
				input.focus();
			});
		},
		
		destroy: function () {
			this.input.remove();
			this.button.remove();
			this.element.show();
			$.Widget.prototype.destroy.call(this);
		}
	});
})(jQuery)