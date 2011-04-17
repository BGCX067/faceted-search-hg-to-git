/// <reference path="jquery-1.4.4.js" />
/// <reference path="jquery-ui.js" />

/*!
* New BSD license
* http://www.opensource.org/licenses/bsd-license.php
*
* Copyright 2011, AUTHORS.txt (http://code.google.com/p/faceted-search/wiki/About)
*
* http://code.google.com/p/faceted-search/
*
*
* jQuery Faceted Search 0.0.1
*
*
* http://code.google.com/p/faceted-search/wiki
*
* Depends:
*   jquery.ui.core.js
*   jquery.ui.widget.js
*/

/*jslint white: true, browser: true, onevar: true, undef: true, nomen: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, newcap: true, immed: true, strict: false */
/*global document: false, jQuery: false */

(function ($, undefined) {
    $.fs = $.fs || {};

    $.fs.manager = function () {
    };

    $.fs.manager.prototype = {
        options: {},
        init: function (options) {
            this.options = options;
            for (item in this.options.Items) {
                $.fs.params
            }
        }
    };

    $.fs.paramBase = {
        type: "",
        init: function (container, item) {
        },
        update: function () {
        },
        manager: $.fs.manager
    };

    $.fs.params = {
        text: $.extend(true, {}, $.fs.paramBase, {
            init: function (container, item) {
                container.bind("focusout", function () {
                    var text = $(this).val();
                });
            }
        }),
        checkbox: $.extend(true, {
        }, $.fs.paramBase)
    };


    $.widget("ui.facetedsearch", {
        options: {
            HtmlContainerName: null,
            HtmlData: null,
            Items: [],
            createUI: false,
            value: 0,
            max: 100
        },

        _init: function () {
        },

        min: 0,

        _create: function () {
            if (this.options.createUI) {
                this.valueDiv = $("<div class='ui-progressbar-value ui-widget-header ui-corner-left'></div>")
			        .appendTo(this.element);
            }

            var uiParams = this.element.find(".fs-param");

            this.oldValue = this._value();
        },

        destroy: function () {
            this.element
			.removeClass("ui-progressbar ui-widget ui-widget-content ui-corner-all")
			.removeAttr("role")
			.removeAttr("aria-valuemin")
			.removeAttr("aria-valuemax")
			.removeAttr("aria-valuenow");

            this.valueDiv.remove();

            $.Widget.prototype.destroy.apply(this, arguments);
        },

        value: function (newValue) {
            if (newValue === undefined) {
                return this._value();
            }

            this._setOption("value", newValue);
            return this;
        },

        _setOption: function (key, value) {
            if (key === "value") {
                this.options.value = value;
                this._refreshValue();
                if (this._value() === this.options.max) {
                    this._trigger("complete");
                }
            }

            $.Widget.prototype._setOption.apply(this, arguments);
        },

        _value: function () {
            var val = this.options.value;
            // normalize invalid value
            if (typeof val !== "number") {
                val = 0;
            }
            return Math.min(this.options.max, Math.max(this.min, val));
        },

        _percentage: function () {
            return 100 * this._value() / this.options.max;
        }


    });

    $.extend($.facetedsearch, {
        version: "0.0.1"
    });

})(jQuery);