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
GridPopupControlLib = function () {
    var self = this;
    this.showPopup = true;
    this.iframe = null;
    this.lp = null;
    this.popupInstance = null;
    this.gridInstance = null;
    this.globalUrl = null;
    this.deleteUrl = null;
    
    this.OnContentLoaded = function (e) {
        //showPopup = false;
        lp.Hide();
    };
    this.OnPopupShown = function (s, e) {
        lp.ShowInElement(iframe);
    };

    this.OnPopupInit = function (s, e) {
        iframe = popupInstance.GetContentIFrame();

        /* the "load" event is fired when the content has been already loaded */
        ASPxClientUtils.AttachEventToElement(iframe, 'load', this.OnContentLoaded);
        ASPxClientUtils.AttachEventToElement(iframe, 'error', this.OnContentLoaded);
    };

    
    
    this.AddNewEntry = function (s, e) {

        popupInstance.SetContentUrl(self.globalUrl + 'Add');
        popupInstance.SetHeaderText("Add");

        var x = ASPxClientUtils.GetAbsoluteX(s);
        var y = ASPxClientUtils.GetAbsoluteY(s);

        popupInstance.ShowAtPos(x, y);

    };

    this.EditEntry = function (s, e, id) {

        popupInstance.SetContentUrl(this.globalUrl + 'Update/' + id);

        popupInstance.SetHeaderText("Edit");

        var x = ASPxClientUtils.GetAbsoluteX(s);
        var y = ASPxClientUtils.GetAbsoluteY(s);

        popupInstance.ShowAtPos(x, y + 20);
    };

    this.DeleteEntry = function (id) {
        if (!confirm("Delete Entry?")) {
            return;
        }

        //var url = '/References/Delete/';
        var url = self.deleteUrl; //sudah action delete; tinggal pasang parameter
        $.ajax({ url: url, type: 'POST', dataType: 'json',
            data: { id: id },
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

};

DetailPopupLib = function () {
    var self = this;
    this.parentGrid = null;//window.parent.gvDataBinding
    this.parentPopup = null; //window.parent.popupElement

    this.CloseWindowOnSuccess = function () {
        self.parentGrid.Refresh();
        self.parentPopup.Hide();
    };

    this.OnCancelClicked = function (s, e) {
        settngs = $.data($('form')[0], 'validator').settings;
        settngs.ignore = ".ignore";
        self.parentGrid.Refresh();
        self.parentPopup.Hide();
    };
};





