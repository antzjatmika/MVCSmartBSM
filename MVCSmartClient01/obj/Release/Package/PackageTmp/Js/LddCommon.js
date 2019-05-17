//hack: disable spinEdit: mouseWheel + key up down event handler
$(document).ready(function () {
    if (typeof ASPxClientSpinEdit != "undefined") {
        ASPxClientSpinEdit.prototype.OnPageOrArrowKeyDown = function () { };
        ASPxClientSpinEdit.prototype.OnMouseWheel = function () { };
    }
});

/*
* Date Format 1.2.3
* (c) 2007-2009 Steven Levithan <stevenlevithan.com>
* MIT license
*
* Includes enhancements by Scott Trenda <scott.trenda.net>
* and Kris Kowal <cixar.com/~kris.kowal/>
*
* Accepts a date, a mask, or a date and a mask.
* Returns a formatted version of the given date.
* The date defaults to the current date/time.
* The mask defaults to dateFormat.masks.default.
*/

var dateFormat = function () {
    var token = /d{1,4}|m{1,4}|yy(?:yy)?|([HhMsTt])\1?|[LloSZ]|"[^"]*"|'[^']*'/g,
		timezone = /\b(?:[PMCEA][SDP]T|(?:Pacific|Mountain|Central|Eastern|Atlantic) (?:Standard|Daylight|Prevailing) Time|(?:GMT|UTC)(?:[-+]\d{4})?)\b/g,
		timezoneClip = /[^-+\dA-Z]/g,
		pad = function (val, len) {
		    val = String(val);
		    len = len || 2;
		    while (val.length < len) val = "0" + val;
		    return val;
		};

    // Regexes and supporting functions are cached through closure
    return function (date, mask, utc) {
        var dF = dateFormat;

        // You can't provide utc if you skip other args (use the "UTC:" mask prefix)
        if (arguments.length == 1 && Object.prototype.toString.call(date) == "[object String]" && !/\d/.test(date)) {
            mask = date;
            date = undefined;
        }

        // Passing date through Date applies Date.parse, if necessary
        date = date ? new Date(date) : new Date;
        if (isNaN(date)) throw SyntaxError("invalid date");

        mask = String(dF.masks[mask] || mask || dF.masks["default"]);

        // Allow setting the utc argument via the mask
        if (mask.slice(0, 4) == "UTC:") {
            mask = mask.slice(4);
            utc = true;
        }

        var _ = utc ? "getUTC" : "get",
			d = date[_ + "Date"](),
			D = date[_ + "Day"](),
			m = date[_ + "Month"](),
			y = date[_ + "FullYear"](),
			H = date[_ + "Hours"](),
			M = date[_ + "Minutes"](),
			s = date[_ + "Seconds"](),
			L = date[_ + "Milliseconds"](),
			o = utc ? 0 : date.getTimezoneOffset(),
			flags = {
			    d: d,
			    dd: pad(d),
			    ddd: dF.i18n.dayNames[D],
			    dddd: dF.i18n.dayNames[D + 7],
			    m: m + 1,
			    mm: pad(m + 1),
			    mmm: dF.i18n.monthNames[m],
			    mmmm: dF.i18n.monthNames[m + 12],
			    yy: String(y).slice(2),
			    yyyy: y,
			    h: H % 12 || 12,
			    hh: pad(H % 12 || 12),
			    H: H,
			    HH: pad(H),
			    M: M,
			    MM: pad(M),
			    s: s,
			    ss: pad(s),
			    l: pad(L, 3),
			    L: pad(L > 99 ? Math.round(L / 10) : L),
			    t: H < 12 ? "a" : "p",
			    tt: H < 12 ? "am" : "pm",
			    T: H < 12 ? "A" : "P",
			    TT: H < 12 ? "AM" : "PM",
			    Z: utc ? "UTC" : (String(date).match(timezone) || [""]).pop().replace(timezoneClip, ""),
			    o: (o > 0 ? "-" : "+") + pad(Math.floor(Math.abs(o) / 60) * 100 + Math.abs(o) % 60, 4),
			    S: ["th", "st", "nd", "rd"][d % 10 > 3 ? 0 : (d % 100 - d % 10 != 10) * d % 10]
			};

        return mask.replace(token, function ($0) {
            return $0 in flags ? flags[$0] : $0.slice(1, $0.length - 1);
        });
    };
} ();

var GetCSharpDateString = function (date) {
    if (date == null)
        return '';

    return dateFormat(date, "yyyy-mm-dd");
}

var GetDateString = function (date) {

    return dateFormat(date, "mm-dd-yyyy");

};

// Some common format strings
dateFormat.masks = {
    "default": "ddd mmm dd yyyy HH:MM:ss",
    shortDate: "m/d/yy",
    mediumDate: "mmm d, yyyy",
    longDate: "mmmm d, yyyy",
    fullDate: "dddd, mmmm d, yyyy",
    shortTime: "h:MM TT",
    mediumTime: "h:MM:ss TT",
    longTime: "h:MM:ss TT Z",
    isoDate: "yyyy-mm-dd",
    isoTime: "HH:MM:ss",
    isoDateTime: "yyyy-mm-dd'T'HH:MM:ss",
    isoUtcDateTime: "UTC:yyyy-mm-dd'T'HH:MM:ss'Z'"
};

// Internationalization strings
dateFormat.i18n = {
    dayNames: [
		"Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat",
		"Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"
	],
    monthNames: [
		"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec",
		"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"
	]
};

// For convenience...
Date.prototype.format = function (mask, utc) {
    return dateFormat(this, mask, utc);
};


    
        

//needed:
/*
- random string generator for url opened in popup window
- popup element placeholder
- namespace object

library untuk workflow popup balik ke gridview

master detail form:
- detail form
- master form
*/
LddCommonLib = function () {
    var self = this;
    this.gridInstance = null;
    this.selectedKey = null;

    this.OnGridSelectionChanged = function (s, e) {
        if (e.isSelected) {
            var key = s.GetRowKey(e.visibleIndex);
            self.selectedKey = key;
        }
    };

    this.OnOkClicked = function (s, e) {
        if (self.selectedKey == null) {
            alert('Please select an entry!');
            return;
        }
        alert('set return value+ close window');
        //alert(self.selectedKey);
    };

    this.OnCancelClicked = function (s, e) {
        self.selectedKey = null;
        window.close();
    };
};


/*
Usage:
>>> formatString("Hello {name}, {greeting}", {name: "Steve", greeting: "how's it going?"});
"Hello Steve, how's it going?"
*/
LddCommonLib.formatString = (function()
{
    var replacer = function(context)
    {
        return function(s, name)
        {
            return context[name];
        };
    };

    return function(input, context)
    {
        return input.replace(/\{(\w+)\}/g, replacer(context));
    };
})();

LddCommonLib.Validate_IsNotInt0 = function (s, e) {
    alert(s.GetValue());
    e.isValid = s.GetValue() != 0;
};

LddCommonLib.Validate_IsNotEmptyGuid = function(s, e) {
    e.isValid = s.GetValue() != '00000000-0000-0000-0000-000000000000';
};


LddCommonLib.Validate_CompareValue = function (s, e, otherVal) {
    //alert(otherVal);
    e.isValid = s.GetValue() == otherVal;
    //alert(e.isValid);
};

LddCommonLib.Validate_IsNotEqual = function (s, e, notEqualVal) {
    e.isValid = s.GetValue() != notEqualVal;
};

LddCommonLib.buildInfo = function (a, b, c, d) {
    var info = LddCommonLib.formatString("{0}-{1}-{2}-{3}", { 0: a, 1: b, 2: c, 3: d });
    return info;
};


LddCommonLib.ShowModalBasic = function (options, objParameter) {
    var defaults = {
        url: '',
        options: 'dialogHeight: 700px; dialogWidth: 900px; edge: Raised; center: Yes; resizable: Yes; scroll: Yes; status: No;help: No; location: No;'
    };

    // combine options with default values
    var opt = $.extend({}, defaults, options); // If you're not using jQuery you need different function here

    var modal = window.showModalDialog(opt.url, objParameter, opt.options);
    return modal;
};
/////////////////////////////////////////
//instance; harus di new sebelum dipakai
LddCommonLib.ChecklistManagerLib = function () {
    var self = this;

    this.gridInstance = null;

    this.CloseWindowOnSuccess = function () {
        if (self.mode == 'frame') {
            self.parentGrid.Refresh();
            self.parentPopup.Hide();
            return;
        }
        alert("mocking should close windows");
    };

    this.OnCancelClicked = function (s, e) {
        if (self.mode == 'frame') {
            self.parentGrid.Refresh();
            self.parentPopup.Hide();
            return;
        }
        alert("mocking should close windows");
    };

    this.OnAddTemplateClicked = function (s, e) {

    };

    this.OnUpdateTemplateClicked = function(s, e) {

    };

};

/////////////////////////////////////////

//instance; harus di new sebelum dipakai
LddCommonLib.popupManagerLib = function (mode) {
    var self = this;
    this.mode = mode; //"modal","frame", "none" , null, undefined

    this.parentGrid = window.parent.pageMan.gridInstance; //window.parent.gvDataBinding
    this.parentPopup = window.parent.pageMan.popupInstance; //window.parent.popupElement

    this.CloseWindowOnSuccess = function () {
        if (self.mode == 'frame') {
            self.parentGrid.Refresh();
            self.parentPopup.Hide();
            return;
        }
        alert("mocking should close windows");
    };

    this.CloseWindowOnSuccess_Custom = function () {
        if (self.mode == 'frame') {
            //self.parentGrid.Refresh();
            window.parent.pageMan.CloseWindowOnSuccess_Custom();
            self.parentPopup.Hide();
            return;
        }
        alert("mocking should close windows");
    };

    this.OnCancelClicked = function (s, e) {
        ASPxClientEdit.ClearEditorsInContainer();

        //troublesome: handling unobtrusive validation cancel
        //e.processOnServer = false;
        //$('form').validate().cancelSubmit = true;
        //var settngs = $.data($('form')[0], 'validator').settings;
        //settngs.ignore = ".ignore";
        //return;

        if (self.mode == 'frame') {
            self.parentGrid.Refresh();
            self.parentPopup.Hide();
            return;
        }
        alert("mocking should close windows");
    };

    //tidak perlu refresh karena grid hide sudah handle refresh
    this.OnCloseDiscardingChanges_NoRefresh = function (s, e) {
        if (self.mode == 'frame') {
            //self.parentGrid.Refresh();
            self.parentPopup.Hide();
            return;
        }
        alert("mocking should close windows");
    };

    this.ResizePopupOnload = function (extraWidth, extraHeight) {

        //alert(contentHeight);
        var parentMan = parent.pageMan;
        //alert(pageMan);
        if (typeof parentMan === "undefined") {
            return;
        }
        if (typeof parentMan.ResizePopupFromChild === "undefined") {
            return;
        }

        //search for available roundPanelControl
        var roundPanel = null;
        if (typeof rpFeatures === "undefined") {
            //temporary alert:
            alert("temporary warning: div dengan id=rpFeatures belum diset");
            roundPanel = rpCommand;
        }
        else {
            roundPanel = rpFeatures;
        }

        //
        //search for available rpShowHide
        var roundPanelOptional = null;

        if (typeof rpShowHide !== "undefined") {

            roundPanelOptional = rpShowHide;
        }


        //var contentHeight = $('#main').height();
        var contentHeight = roundPanel.scrollHeight;
        //var tempH = $('#main').outerHeight(true);
        var contentWidth = roundPanel.scrollWidth;
        //var tempW = $(roundPanel).outerWidth(true);

        if (roundPanelOptional != null) {
            if ($('#AgentFr').is(":visible")) {
                contentWidth = contentWidth + roundPanelOptional.scrollWidth;
            }
        }

        var positionMain = $('#main').offset();
        var positionBody = $(roundPanel).offset();
        var leftPos = positionBody.left;

        parentMan.ResizePopupFromChild(contentWidth + (leftPos * 2), contentHeight + positionMain.top);
    };


};

LddCommonLib.GridPopupControlLib = function () {
    var self = this;
    this.showPopup = true;
    this.iframe = null;

    this.lp = null;
    this.popupInstance = null;
    this.gridInstance = null;
    this.globalUrl = null;
    this.deleteUrl = null;

    this.popupHeader = null;

    this.OnContentLoaded = function (e) {
        //showPopup = false;
        self.lp.Hide();
    };
    this.OnPopupShown = function (s, e) {
        self.lp.ShowInElement(self.iframe);
        //self.iframe.onload = self.ResizePopup(s, e);
    };

    this.OnPopupShown_AjaxForm = function (s, e) {
        //self.lp.SetPopupElementID("");
        //self.lp.Show();
        //self.iframe.onload = self.ResizePopup(s, e);
    };

    this.OnPopupClosed = function(s, e) {
        self.lp.Hide();
    };

    this.ResizePopup = function (s, e) {
        //alert('resizing popup');
        var contentHeight = self.iframe.scrollHeight;
        contentHeight = self.iframe.contentDocument.body.scrollHeight;
        var popupHeight = self.popupInstance.GetHeight();
        self.popupInstance.SetHeight(contentHeight + 10 + 15);
    };

    this.ResizePopupFromChild = function (width, height) {
        //alert('resizing popup');
        //var contentHeight = self.iframe.scrollHeight;
        //contentHeight = self.iframe.contentDocument.body.scrollHeight;
        //var popupHeight = self.popupInstance.GetHeight();
        //var tempH1 = $('#popupInstance_PWC-1').outerHeight(true);

        //handle minimumWeight
        var minHeight = self.PopupMinHeight;
        if (minHeight !== undefined) {
            if (height < minHeight) {
                height = minHeight;
            }
        }

        //detect max height dari visible window's height
        var maxHeight = $(window.top).height() - 40;
        if (height > maxHeight) {
            height = maxHeight;
            width = width + 15;
        }

        self.popupInstance.SetWidth(width + 6);
        self.popupInstance.SetHeight(height + 30);
        //var tempH = $('#popupInstance_PWC-1').outerHeight(true);


        $('#popupInstance_PWC-1').height(height + 4);
        $('#popupInstance_PWC-1').width(width + 4);

        if (self.PopupMode !== undefined) {
            if (self.popupInstance.GetVisible()) {
                self.popupInstance.UpdateWindowPosition();
            }
        }
    };

    this.OnPopupInit = function (s, e) {
        try {
            self.iframe = self.popupInstance.GetContentIFrame();

            /* the "load" event is fired when the content has been already loaded */
            ASPxClientUtils.AttachEventToElement(self.iframe, 'load', this.OnContentLoaded);
            ASPxClientUtils.AttachEventToElement(self.iframe, 'error', this.OnContentLoaded);
        }
        catch (e) {
            alert("exception at OnPopupInit");
            alert(e);

        }
    };


    this.AddNewEntry = function (s, e, param) {

        if (param === null || param === undefined) {
            param = '';
        }

        self.popupInstance.SetContentUrl(self.globalUrl + 'Add/' + param);

        if (self.popupHeader == null) {
            self.popupHeader = self.popupInstance.GetHeaderText();
        }
        self.popupInstance.SetHeaderText("Add " + self.popupHeader);

        if (self.PopupMode !== undefined) {
            self.popupInstance.Show();
            return false;
        }

        var x = ASPxClientUtils.GetAbsoluteX(s);
        var y = ASPxClientUtils.GetAbsoluteY(s);

        self.popupInstance.ShowAtPos(x, y);

        return false;
    };

    this.AddNewEntry2 = function (s, e, action, param) {

        if (param === null || param === undefined) {
            param = '';
        }

        self.popupInstance.SetContentUrl(self.globalUrl + action + '/' + param);

        if (self.popupHeader == null) {
            self.popupHeader = self.popupInstance.GetHeaderText();
        }
        self.popupInstance.SetHeaderText("Add " + self.popupHeader);

        var x = ASPxClientUtils.GetAbsoluteX(s);
        var y = ASPxClientUtils.GetAbsoluteY(s);

        self.popupInstance.ShowAtPos(x, y);
    };



    this.EditEntry = function (s, e, id, param) {

        if (param === null || param === undefined) {
            param = '';
        }

        self.popupInstance.SetContentUrl(self.globalUrl + 'Update/' + id + param);

        if (self.popupHeader == null) {
            self.popupHeader = self.popupInstance.GetHeaderText();
        }
        self.popupInstance.SetHeaderText("Update " + self.popupHeader);

        if (self.PopupMode !== undefined) {
            self.popupInstance.Show();
            return false;
        }

        var x = ASPxClientUtils.GetAbsoluteX(s);
        var y = ASPxClientUtils.GetAbsoluteY(s);

        self.popupInstance.ShowAtPos(x, y + 20);

        return false;

    };

    this.EditEntry2 = function (s, e, action, id, param) {

        if (param === null || param === undefined) {
            param = '';
        }

        self.popupInstance.SetContentUrl(self.globalUrl + action + '/' + id + param);

        if (self.popupHeader == null) {
            self.popupHeader = self.popupInstance.GetHeaderText();
        }
        self.popupInstance.SetHeaderText("Update " + self.popupHeader);

        var x = ASPxClientUtils.GetAbsoluteX(s);
        var y = ASPxClientUtils.GetAbsoluteY(s);

        self.popupInstance.ShowAtPos(x, y + 20);

        return false;

    };

    this.DeleteEntry = function (id, param) {
        if (!confirm("Delete Entry?")) {
            return;
        }

        //var url = '/References/Delete/';
        var url = self.deleteUrl; //sudah action delete; tinggal pasang parameter
        $.ajax({ url: url, type: 'POST', dataType: 'json',
            data: { id: id, param: param },
            success: function (status) {

                if (status.ErrorMessage != null) {
                    alert(status.ErrorMessage);
                }
                else {
                    self.gridInstance.Refresh();
                }
            }

        });
    };

    this.CustomEntry = function (s, e, headerText, action, param) {

        if (param === null || param === undefined) {
            param = '';
        }

        self.popupInstance.SetContentUrl(self.globalUrl + action + '/' + param);

        if (self.popupHeader == null) {
            self.popupHeader = self.popupInstance.GetHeaderText();
        }
        self.popupInstance.SetHeaderText(headerText);

        //var x = ASPxClientUtils.GetAbsoluteX(s);
        //var y = ASPxClientUtils.GetAbsoluteY(s);

        //self.popupInstance.ShowAtPos(x, y);
        self.popupInstance.Show();

    };

};

var HeightCalculator = function () {
    var MINIMUM_HEIGHT = 520;
    var self = this;
    this.iframeInTab = null;
    //self.iframePosition = null;
    //self.effectiveHeight = null;
    this.divContentHeight = null;
    this.topMargin = null;
    this.scrollHeight = null;
    this.effectiveTabHeigth = null;

    this.setup = function (iframeInsideTab) {
        self.iframeInTab = iframeInsideTab;
        //self.iframePosition = iframeInsideTab.position();

        //selalu dikasih minimum height
        self.divContentHeight = self.getDivContentHeight(iframeInsideTab);
        if (self.divContentHeight < MINIMUM_HEIGHT) {
            self.divContentHeight = MINIMUM_HEIGHT;
        }

        //self.effectiveHeight = self.divContentHeight + self.iframePosition.y;
        self.topMargin = iframeInsideTab.offsetTop;
        //self.scrollHeight = iframeInsideTab.offsetHeight;//jangan pakai ini bisa nambah terus
        self.effectiveTabHeigth = self.divContentHeight + self.topMargin + 2;


    };

    self.getDivContentHeight = function (iframe, option) {
        var divContentSelector = null;
        if (option == 1) {
            divContentSelector = '#main';
        } else {
            divContentSelector = '#tabContent';
        }

        var divContent = $(iframe).contents().find(divContentSelector);

        var contentHeight = divContent.height();

        return contentHeight;
    };
};  //end height calculator

MainTabLib = function (id) {
    var self = this;
    self.id = id;

    self.tabControl = null; //tabControl
    self.splitterControl = null;
    self.contentPane = null; //splitterControl

    self.setup = function (mainSplitter, mainTabControl) {
        
        self.splitterControl = mainSplitter;
        self.contentPane = mainSplitter.GetPane(1);
        self.tabControl = mainTabControl;
        
    };

    self.GetAvailbleHeight = function () {
        return self.contentPane.GetClientHeight();
    };


};
MainTabLib.Test = function (s, e) {
    var visibleWindowHeight = $(window).height();
    var split = MainSplitter.GetHeight();
    var paneA = MainSplitter.GetPane(0);
    var paneB = MainSplitter.GetPane(1);


    var info = LddCommonLib.formatString("window {0} header {1} content {2}", { 0: visibleWindowHeight, 1: paneA.GetClientHeight(), 2: paneB.GetClientHeight() });
    
    $('#' + s.id).val(info);

};



MainTabLib.tab1Info = function (s, e) {
    var visibleWindowHeight = $(window).height();
    var bodyHeight = $("body").height();
    //var split = MainSplitter.GetHeight();
    //var paneA = MainSplitter.GetPane(0);
    //var paneB = MainSplitter.GetPane(1);


    var info = LddCommonLib.buildInfo(visibleWindowHeight, bodyHeight);
    //var info = visibleWindowHeight;

    $('#' + s.id).val(info);

};

MainTabLib.tab1ParentAccess = function (s, e) {
    var rootSplitterObj = parent.MainSplitter;
    var headerSplitter = rootSplitterObj.GetPane(0);
    var contentSplitter = rootSplitterObj.GetPane(1);

    var rootTabControlObj = parent.MainTabControl;

    if (rootSplitterObj === undefined) {
        return;
    }

    var activeTab = rootTabControlObj.GetActiveTab();
    var activeIframe = rootTabControlObj.GetTabContentHTML(activeTab);
    //var activeIframe = rootTabControlObj.GetMainElement();
    var code = $(activeIframe);

    var iframe = MainTabLib.getActiveTabIframe(rootTabControlObj);
    var test = iframe.position();
    //alert(activeIframe);
    var divContent = $('#tabContent');

    var contentHeight = divContent.height(); // +headerSplitter.GetClientHeight();
    iframe.height(contentHeight + 5);
    rootTabControlObj.SetHeight(contentHeight + 50);
    //contentSplitter.SetHeight(contentHeight);
    //rootTabControlObj.AdjustSize();
    //contentSplitter.AdjustSize();

    var info = LddCommonLib.buildInfo(rootSplitterObj, rootTabControlObj, activeTab);
    //var info = visibleWindowHeight;
    //activeTab ambil active IFrame seperti PopUp

    $('#' + s.id).val(info);
};

MainTabLib.getActiveTabIframe = function (tabControl) {
    var activeTab = tabControl.GetActiveTab();
    var iframeContent = tabControl.GetTabContentHTML(activeTab);

    var dummyIframe = $(iframeContent)[0];

    var tabControlContext = tabControl.GetMainElement();

    //alert(dummyIframe.id);
    var iframeResult = $('#' + dummyIframe.id, tabControlContext);
    return iframeResult;

};



MainTabLib.tab1Resize = function (iframe, tabControl, option) {
    var divContentSelector = null;
    if (option == 1) {
        divContentSelector = '#main';
    } else {
        divContentSelector = '#tabContent';
    }

    //window size
    var rootSplitterObj = parent.MainSplitter;
    var contentSplitter = rootSplitterObj.GetPane(1);
    var splitterContentHeight = contentSplitter.GetClientHeight();

    var heightCalculator = new HeightCalculator();
    heightCalculator.setup(iframe); //ambil effective height content dari iframe

    var contentHeightOverall = heightCalculator.effectiveTabHeigth;

    var finalIframeHeight = null;
    
    //masih musti diadjust untuk level 1; level 2 ok--untuk di edge cases
    if (splitterContentHeight - heightCalculator.topMargin + 8 > contentHeightOverall) {//offset di sini
        //$(iframe).height(heightCalculator.divContentHeight); //bisa error saat new

        finalIframeHeight = splitterContentHeight - heightCalculator.topMargin - 22;
        $(iframe).height(finalIframeHeight);
        tabControl.SetHeight(splitterContentHeight - 6);
    }
    else {
        finalIframeHeight = heightCalculator.divContentHeight;
        $(iframe).height(finalIframeHeight);
        tabControl.SetHeight(contentHeightOverall);
    }

    return finalIframeHeight;
};


MainTabLib.tabLevel2Resize = function (iframe, tabControl, option) {
    var divContentSelector = null;
    if (option == 1) {
        divContentSelector = '#main';
    } else {
        divContentSelector = '#tabContent';
    }

    //1nd level tab
    var l1Dom = parent;
    //var l1Container = l1Dom.GetTabAndIframe();

    //level 2nd = current level
    var heightCalculator = new HeightCalculator();
    heightCalculator.setup(iframe); //ambil effective height content dari iframe

    var contentHeightOverall = heightCalculator.effectiveTabHeigth;

    //set minimum height; paling tidak management tab's height dibuat lebih tinggi dari iframe
    $(iframe).height(heightCalculator.divContentHeight);

    if (tabControl === undefined) {
        alert('warning- tabControl is undefined--trace javascript');
    }
    tabControl.SetHeight(contentHeightOverall);

    var outerIframeHeight = null;
    outerIframeHeight = parent.parent.resizeIframeCalledFromChild(parent.frameElement);

    var finalIframeHeight = outerIframeHeight - heightCalculator.topMargin;
    if (outerIframeHeight - heightCalculator.topMargin + 30 > contentHeightOverall) {
        finalIframeHeight = outerIframeHeight - heightCalculator.topMargin;
        $(iframe).height(finalIframeHeight);
        tabControl.SetHeight(outerIframeHeight);
    }

    return finalIframeHeight;
};

MainTabLib.tabLevel3Resize = function (iframe, tabControl, option) {
    var divContentSelector = null;

    //2nd level tab
    var l2Dom = parent;
    var l2Container = l2Dom.GetTabAndIframe();

    //1st level tab
    var l1Dom = l2Dom.parent;
    var l1Container = l1Dom.GetTabAndIframe();

    ///level 3 
    var heightCalculator = new HeightCalculator();
    heightCalculator.setup(iframe); //ambil effective height content dari iframe

    var contentHeightOverall = heightCalculator.effectiveTabHeigth;

    //set minimum height; paling tidak management tab's height dibuat lebih tinggi dari iframe
    $(iframe).height(heightCalculator.divContentHeight);
    tabControl.SetHeight(contentHeightOverall);

    var outerIframeHeight = null;
    outerIframeHeight = parent.resizeIframeCalledFromChild(parent.frameElement);

    var finalIframeHeight = outerIframeHeight - heightCalculator.topMargin;
    if (outerIframeHeight - heightCalculator.topMargin + 20> contentHeightOverall) {
        finalIframeHeight = outerIframeHeight - heightCalculator.topMargin;
        $(iframe).height(finalIframeHeight);
        tabControl.SetHeight(outerIframeHeight);
    }

    //return finalIframeHeight;//belum perlu level4

};

//$(document).on("keydown", function (e) {
//    if (e.which === 8 && !$(e.target).is("input, textarea")) {
//        e.preventDefault();
//    }
//});

//$(document).keydown(function (e) {
//    var nodeName = e.target.nodeName.toLowerCase();

//    if (e.which === 8) {
//        if ((nodeName === 'input' && e.target.type === 'text') ||
//            nodeName === 'textarea') {
//            // do nothing
//        } else {
//            e.preventDefault();
//        }
//    }
//});

$(document).keydown(function (e) {

    //console.log(e.keyCode + "\n");

    var typeName = e.target.type;//typeName should end up being things like 'text', 'textarea', 'radio', 'undefined' etc.
    //console.log(typeName + "\n");



    // Prevent Backspace as navigation backbutton
    if ((e.keyCode == 8 && typeName != "text" && typeName != "textarea")||
        $(e.target).attr("readonly")) {
        //console.log("Prevent Backbutton as Navigation Back" + typeName + "\n");
        e.preventDefault();
    } 
    

})