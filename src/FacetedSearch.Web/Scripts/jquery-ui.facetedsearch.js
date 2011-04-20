/// <reference path="jquery-1.5.1.js" />
/// <reference path="jquery-ui-1.8.11.js" />
/// <reference path="goog/base.js" />
/// <reference path="goog/json/json.js" />

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
    $.fn.stringify = function (obj, basePath) {
        if (!window['JSON']) {
            window['JSON'] = {};
            var jsFilename = "jquery-ui.facetedsearch.js";
            basePath = basePath || $('script[src*="' + jsFilename + '"]')
                .map(function () {
                    var src = $(this).attr("src");
                    return src.substring(0, src.indexOf(jsFilename));
                }).get(0);

            if (typeof goog == "undefined") {
                $.getScript(basePath + "goog/base.js");
                goog.require('goog.json');
            }
        }
        if (typeof window['JSON']['stringify'] !== 'function') {
            window['JSON']['stringify'] = goog.json.serialize;
        }
        return window.JSON.stringify(obj, "");
    }

    $.fs = $.fs || {};

    $.fs.manager = function (element) {
        this.element = element;
    };

    $.fs.manager.prototype = {
        options: null,
        _onUpdate: function (e, updatedItem) {
            var that = this;
            $.each(this.options.searchOptions.Items, function (ind, opt) {
                if (opt.Name == updatedItem.Name) {
                    that.options.searchOptions.Items[ind] = updatedItem;
                    return false;
                }
            });
            if (!this.options.deferredUpdate) {
                this.query();
            }
        },
        query: function () {
            var that = this;
            $.ajax({
                type: "POST",
                url: this.options.url,
                data: this._getData(),
                dataType: "json",
                contentType: this.options.contentType || "application/json; charset=utf-8"
            })
            .success(function (searchOptions, textStatus, jqXHR) {
                $.proxy(that.update, that)(searchOptions);
            });
        },
        _getData: function () {
            var htmlData = this.options.searchOptions.HtmlData;
            // PREVENT FROM being send to the server a huge amount of html data
            this.options.searchOptions.HtmlData = null;
            var json = $.fn.stringify(this.options.searchOptions);
            //restore html data
            this.options.searchOptions.HtmlData = htmlData;
            return json;
        },
        init: function (options) {
            this.options = options;
            this.update(options.searchOptions);
            var uiParams = this.element.find(".fs-param");
            var manager = this;
            $.each(this.options.searchOptions.Items, function (ind, item) {
                uiParams.filter("#" + item.Name).each(function (index, container) {
                    $.fs.params[item.Type]
                        .init($(container), item, manager)
                        .bind("stateUpdated", $.proxy(manager._onUpdate, manager));
                });
            });
        },
        update: function (searchOptions) {
            this.options.searchOptions = searchOptions;
            var uiParams = this.element.find(".fs-param");
            $.each(this.options.searchOptions.Items, function (ind, item) {
                uiParams.filter("#" + item.Name).each(function (index, container) {
                    var param = $.fs.params[item.Type];
                    $.proxy(param.update, param)($(container), item);
                });
            });
        }
    };

    $.fs.paramBase = {
        type: "",
        init: function (container, item, manager) {
            this.container = container;
            this.item = item;
            this.manager = manager;

            return this._init(container, item, manager);
        },
        _init: function (container, item, manager) {
        },
        update: function (container, item) {
            return this._update(container, item);
        },
        _update: function (container, item) {
        },
        manager: null,
        container: null,
        item: null,
        _getPreviousValue: function (element) {
            return element.data("previousValue") ||
                element.data("previousValue", {
                    value: null
                });
        },
        _setPreviousValue: function (element, val) {
            return element.data("previousValue", {
                value: val
            });
        }
    };

    $.fs.params = {
        text: $.extend(true, $({}), $.fs.paramBase, {
            _init: function (container, item, manager) {
                that = this;
                container.bind("focusout", function () {
                    var text = $(this).val();
                    var oldText = that._getPreviousValue(container);
                    if (oldText.value !== text) {
                        that.item.Text = text;
                        that._setPreviousValue(container, text);
                        that.trigger("stateUpdated", that.item);
                    }
                });
                container.watermark(item.Watermark)
                return this;
            },
            _update: function (container, item) {
                container.val(item.Text);
                this._setPreviousValue(container, item.Text);
            }
        }),
        checkbox: $.extend(true, {
        }, $.fs.paramBase)
    };


    $.widget("ui.facetedsearch", {
        options: {
            searchOptions: null,
            createUI: false,
            deferredUpdate: false,
            url: "",    //current page
            value: 0,
            max: 100,
            contentType: null
        },

        _init: function () {
        },

        min: 0,
        manager: null,

        _create: function () {
            if (this.options.createUI) {
                this.valueDiv = $("<div class='ui-progressbar-value ui-widget-header ui-corner-left'></div>")
			        .appendTo(this.element);
            }

            this.manager = new $.fs.manager(this.element);
            this.manager.init(this.options);
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