PdnLib = function() {
    var self = this;
    this.parentGrid = null; //window.parent.gvDataBinding
    this.parentPopup = null; //window.parent.popupElement
};//end pdnLib

PdnLib.AjaxError = function (x, settings, exception) {
    var message;
    var statusErrorMap = {
        '400': "Server understood the request but request content was invalid.",
        '401': "Unauthorised access.",
        '403': "Forbidden resouce can't be accessed",
        '500': "Internal Server Error.",
        '503': "Service Unavailable"
    };
    if (x.status) {
        message = statusErrorMap[x.status];
        if (!message) {
            message = "Unknow Error \n.";
        }
    } else if (exception == 'parsererror') {
        message = "Error.\nParsing JSON Request failed.";
    } else if (exception == 'timeout') {
        message = "Request Time out.";
    } else if (exception == 'abort') {
        message = "Request was aborted by the server";
    } else {
        message = "Unknow Error \n.";
    }
    alert(exception);
    //$(this).css("display", "inline");
    //$(this).html(message);
};


PdnLib.PreAddFormLib = function () {
    this.Url_PreAddForm = null;
    this.AreaPlaceHolderId = 'PdnFormContentId';
    this.AttachmentFormPlaceHolderId = 'AttachmentFormContentId';
    
    this.ShowPopup_Add = function (action, poId) {
        //var $form = $("#" + formId);
        //var url = $form.attr('action');
        //var formData = $form.serialize();
        var url = this.Url_PdnFormController + 'Add';
        var areaPlaceHoler = this.AreaPlaceHolderId;
        var loadingPanel = this.PdnFormLoadingPanelInstance;
        var popup = this.PdnFormPopupInstance;

        popup.Show();

        $.ajax({
            type: "get",
            url: url,
            data: { poId: poId },
            beforeSend: function (jqXHR, settings) { loadingPanel.Show(); },
            complete: function (jqXHR, textStatus) { loadingPanel.Hide(); },
            success: function (response) {
                if (response.ErrorMessage !== undefined) {
                    alert(response.ErrorMessage);
                    popup.Hide();
                    return;
                }


                $("#" + areaPlaceHoler).html(response);
                popup.Show();


                //pageMan.popupInstanceFollowUp.Show();

            }
        });
    };

    this.ShowPreAddForm = function () {
        $.ajax({
            type: "GET",
            url: this.Url_PreAddForm,
            data: null,
            //data: { dunningId: null },
            success: function (response) {
                $("#preAddFormContent").html(response);
                $("#preAddFormModal").modal('show');
                //pageMan.popupInstanceFollowUp.Show();

            }
        });
    };

    this.OnCancelClicked = function (s, e) {

    };

    this.PreAddForm_Submit = function (action, formId) {
        var $form = $("#" + formId);
        var url = $form.attr('action');
        var formData = $form.serialize();
        var areaPlaceHoler = this.AreaPlaceHolderId;
        $.ajax({
            type: "post",
            url: url,
            data: formData,
            success: function (response) {
                if(response.ErrorMessage !== undefined)
                {
                    alert(response.ErrorMessage);
                    return;
                }
                
                $("#preAddFormModal").modal('hide');
                $("#" + areaPlaceHoler).html(response);

                $("#preAddArea").hide();


//pageMan.popupInstanceFollowUp.Show();

            }
        });
    };

};//end PdnLib.PreAddFormLib

PdnLib.RejectionLetterLib = function () {
    var self = this;
    this.IsCloseOnSuccess = false; //false = habis sucess save/update, form di replace dengan yang baru
    this.AreaPlaceHolderId = 'PdnFormContentId';
    this.PdnFormLoadingPanelInstance = null;
    this.PdnFormPopupInstance = null;
    this.SchedulingFormPopupInstance = null;
    this.AttachmentFormPopupInstance = null;
    this.AttachmentGridInstanceName = null;

    this.ItemSelectionFormPopupInstance = null;
    this.ItemSelectionFormPlaceHolderId = null;

    this.Url_PdnFormController = null;
    this.Url_RejectionFormController = null;

    this.ErrorValidationMessage = "Error validating, make sure all required fields are not empty";

    this.View = function (action, pdnId) {
        //var contFunction = this.AreaControlFunction;
        var url = this.Url_PdnFormController + 'PdnRejectedView';
        var areaPlaceHoler = this.AreaPlaceHolderId;
        var loadingPanel = this.PdnFormLoadingPanelInstance;
        var popup = this.PdnFormPopupInstance;

        //popup.Show();

        $.ajax({
            type: "get",
            url: url,
            data: { pdnId: pdnId },
            beforeSend: function (jqXHR, settings) { loadingPanel.Show(); },
            complete: function (jqXHR, textStatus) { loadingPanel.Hide(); },
            success: function (response) {
                if (response.ErrorMessage !== undefined) {
                    alert(response.ErrorMessage);
                    return;
                }

                if (response.AlertAndClose !== undefined) {
                    alert(response.AlertAndClose);
                    return;
                }

                $("#" + areaPlaceHoler).html(response);
                popup.Show();
            }
        });
    };

    this.UpdateManualRlNcr = function (action, rlId) {
        //var contFunction = this.AreaControlFunction;
        var url = this.Url_PdnFormController + '_UpdateManualRlNcr';
        var areaPlaceHoler = this.AreaPlaceHolderId;
        var loadingPanel = this.PdnFormLoadingPanelInstance;
        var popup = this.PdnFormPopupInstance;

        //popup.Show();

        $.ajax({
            type: "get",
            url: url,
            data: { rejectionLetterId: rlId },
            beforeSend: function (jqXHR, settings) { loadingPanel.Show(); },
            complete: function (jqXHR, textStatus) { loadingPanel.Hide(); },
            success: function (response) {
                if (response.ErrorMessage !== undefined) {
                    alert(response.ErrorMessage);
                    return;
                }

                if (response.AlertAndClose !== undefined) {
                    alert(response.AlertAndClose);
                    return;
                }

                $("#" + areaPlaceHoler).html(response);
                popup.Show();
            }
        });
    };

    this.AddManualRlNcr = function (action) {
        //var contFunction = this.AreaControlFunction;
        var url = this.Url_PdnFormController + '_AddManualRlNcr';
        var areaPlaceHoler = this.AreaPlaceHolderId;
        var loadingPanel = this.PdnFormLoadingPanelInstance;
        var popup = this.PdnFormPopupInstance;

        //popup.Show();

        $.ajax({
            type: "get",
            url: url,
            data: { },
            beforeSend: function (jqXHR, settings) { loadingPanel.Show(); },
            complete: function (jqXHR, textStatus) { loadingPanel.Hide(); },
            success: function (response) {
                if (response.ErrorMessage !== undefined) {
                    alert(response.ErrorMessage);
                    return;
                }

                if (response.AlertAndClose !== undefined) {
                    alert(response.AlertAndClose);
                    return;
                }

                $("#" + areaPlaceHoler).html(response);
                popup.Show();
            }
        });
    };

    this.ViewCompleteRejection = function (action, rejectionLetterId) {
        //var contFunction = this.AreaControlFunction;
        var url = this.Url_PdnFormController + 'PdnRejectedViewComplete';
        var areaPlaceHoler = this.AreaPlaceHolderId;
        var loadingPanel = this.PdnFormLoadingPanelInstance;
        var popup = this.PdnFormPopupInstance;

        //popup.Show();

        $.ajax({
            type: "get",
            url: url,
            data: { rejectionLetterId: rejectionLetterId },
            beforeSend: function (jqXHR, settings) { loadingPanel.Show(); },
            complete: function (jqXHR, textStatus) { loadingPanel.Hide(); },
            success: function (response) {
                if (response.ErrorMessage !== undefined) {
                    alert(response.ErrorMessage);
                    return;
                }

                if (response.AlertAndClose !== undefined) {
                    alert(response.AlertAndClose);
                    return;
                }

                $("#" + areaPlaceHoler).html(response);
                popup.Show();
            }
        });
    };


    this.RejectionForm_TabDocumentAttachmentVisible = function (isTabAttachmentVisible) {

        if (isTabAttachmentVisible) {
            btnPrintRejectionManual.SetVisible(false);
            btnPrintRejection.SetVisible(false);
        } else {
            //detect which btnPrint to be set visible
            var pdnId = $('#pdnRejectLogistic input[name="PdnHeader.Id"]').val();
            
            if (pdnId == "00000000-0000-0000-0000-000000000000") {
                btnPrintRejectionManual.SetVisible(true);
            } else {
                btnPrintRejection.SetVisible(true);
            }

        }

    };
    this.RejectionForm_BtnSaveUpdate_Clicked = function (s, e) {
        var isFormValid = ASPxClientEdit.ValidateGroup("");

        if (!isFormValid) {
            alert(self.ErrorValidationMessage);
            return;
        }

        var formId = 'pdnRejectLogistic';

        self.RejectionForm_SaveUpdate_defered(null,formId, 'Update').then(function (response) {
            if (response.ErrorMessage !== undefined) {
                alert(response.ErrorMessage);
                return;
            }

            //self.TestThen(response, 'update', formId);

        });

    };

    this.RejectionForm_BtnSaveUpdateManual_Clicked = function (s, e) {
        var isFormValid = ASPxClientEdit.ValidateGroup("");

        if (!isFormValid) {
            alert(self.ErrorValidationMessage);
            return;
        }

        var formId = 'pdnRejectLogistic';

        self.RejectionForm_SaveUpdateManual_defered(null, formId, 'Update').then(function (response) {
            if (response.ErrorMessage !== undefined) {
                alert(response.ErrorMessage);
                return;
            }
            //self.TestThen(response, 'update', formId);

        });

    };


    this.RejectionForm_OnPopupClosing = function () {
        if (typeof (gridMasterDetail) !== 'undefined') gridMasterDetail.Refresh();
        if (typeof (ManualRlNcrGrid) !== 'undefined') ManualRlNcrGrid.Refresh();
        if (typeof (CompleteRlNcrGrid) !== 'undefined') CompleteRlNcrGrid.Refresh();
    };


    this.RejectionForm_SaveUpdate_defered = function (response, formIdSelector, action) {
        var formObj = $("#" + formIdSelector);
        var url = formObj.attr("action");
        var formData = formObj.serialize();
        var popup = self.PdnFormPopupInstance;
        var loadingPanel = this.PdnFormLoadingPanelInstance;
        var prevLoadingPanelHeader = loadingPanel.GetText();

        
        return $.ajax({
            url: url,
            type: "POST",
            data: formData,
            //dataType: "json",
            beforeSend: function (jqXHR, settings) {
                loadingPanel.SetText("Saving..."); loadingPanel.Show(); },
            complete: function (jqXHR, textStatus) { loadingPanel.Hide(); loadingPanel.SetText(prevLoadingPanelHeader); },
            success: function (resp) {
                //if (resp.isSuccess == true) {
                //    //alert("Save success.");
                //    var prId = resp.PrId;
                //    var plId = resp.PlId;
                //    $('#pdnRejectLogistic input[name="PdnRej.PrId"]').val(prId);
                //    $('#pdnRejectLogistic input[name="PdnLog.PlId"]').val(plId);
                //} else {
                //    alert("Error processing request.");
                //}
                
                if (resp.isSuccess == false) {
                    alert("Error processing request.");
                    ////alert("Save success.");
                    //var prId = resp.PrId;
                    //var plId = resp.PlId;
                    //$('#pdnRejectLogistic input[name="PdnRej.PrId"]').val(prId);
                    //$('#pdnRejectLogistic input[name="PdnLog.PlId"]').val(plId);
                } else {
                    $("#PdnFormContentId").html(resp);
                    //alert("Error processing request.");
                }
            }
        });
    };

    this.RejectionForm_SaveUpdateManual_defered = function (response, formIdSelector, action) {
        var url = this.Url_PdnFormController + '_UpdateManualRlNcr';
        var formObj = $("#" + formIdSelector);
        //var url = formObj.attr("action");
        var formData = formObj.serialize();
        var popup = self.PdnFormPopupInstance;
        var loadingPanel = this.PdnFormLoadingPanelInstance;
        var prevLoadingPanelHeader = loadingPanel.GetText();


        return $.ajax({
            url: url,
            type: "POST",
            data: formData,
            //dataType: "json",
            beforeSend: function (jqXHR, settings) {
                loadingPanel.SetText("Saving..."); loadingPanel.Show();
            },
            complete: function (jqXHR, textStatus) { loadingPanel.Hide(); loadingPanel.SetText(prevLoadingPanelHeader); },
            success: function (resp) {
                if (resp.isSuccess == false) {
                    alert("Error processing request.");
                    ////alert("Save success.");
                    //var prId = resp.PrId;
                    //var plId = resp.PlId;
                    //$('#pdnRejectLogistic input[name="PdnRej.PrId"]').val(prId);
                    //$('#pdnRejectLogistic input[name="PdnLog.PlId"]').val(plId);
                } else {
                    $("#PdnFormContentId").html(resp);
                    //alert("Error processing request.");
                }
            }
        });
    };

    this.RejectionForm_OnCancelClicked = function(s, e) {

    };

    //untuk non manual rejection
    this.RejectionForm_OpenPrintWindow = function () {
        var pdnId = $('#pdnRejectLogistic input[name="PdnHeader.Id"]').val();

        var rejectionTab = $('li.active [href=#PdnContent]');

        var url = '';

        if (rejectionTab[0] != undefined) {
            url = self.Url_RejectionFormController + 'RlReport?pdnId=' + pdnId;
        } else {
            url = self.Url_RejectionFormController + 'NcrReport?pdnId=' + pdnId;
        }


        window.open(url, "_blank", "toolbar=no, scrollbars=yes, resizable=yes");
    }

    this.RejectionForm_OpenPrintWindow_Manual = function () {
        var prId = $('#pdnRejectLogistic input[name="PdnRej.PrId"]').val();
        var plId = $('#pdnRejectLogistic input[name="PdnLog.PlId"]').val();

        var rejectionTab = $('li.active [href=#PdnContent]');

        var url = '';

        if (rejectionTab[0] != undefined) {
            url = self.Url_RejectionFormController + 'RlReport?prId=' + prId;
        } else {
            url = self.Url_RejectionFormController + 'NcrReport?plId=' + plId;
        }

        window.open(url, "_blank", "toolbar=no, scrollbars=yes, resizable=yes");
    }

    //save sebelum print (kalau authorized by tab), kecuali kalau sudah complete
    this.RejectionForm_BtnPrint_Clicked = function (s, e) {
        
        var formId = 'pdnRejectLogistic';

        var rejectionTab = $('li.active [href=#PdnContent]');

        var url = '';
        var isComplete = false;
        var isRejectionTab = false;
        var isLogisticTab = false;

        if (rejectionTab[0] != undefined) {

            isComplete = $('#pdnRejectLogistic input[name="PdnRej.IsCompleted"]').val().toUpperCase() == "TRUE" ? true : false;
            isRejectionTab = true;

        } else {
            isComplete = $('#pdnRejectLogistic input[name="PdnLog.IsCompleted"]').val().toUpperCase() == "TRUE" ? true : false;
            isLogisticTab = true;
        }

        //validate tab content before printing
        var isFormValid = false;

        if (isRejectionTab) {
            isFormValid = ASPxClientEdit.ValidateEditorsInContainerById("PdnContent");
        }
        else if (isLogisticTab) {
            isFormValid = ASPxClientEdit.ValidateEditorsInContainerById("PdnLogisticContentId");
        }

        if (!isFormValid) {
            alert(self.ErrorValidationMessage);
            return;
        }

        //alert(isComplete + " debug isComplete");
        var currentUserRole = $('#pdnRejectLogistic input[name="CurrentUserRole"]').val();
        
        var isNotAuthToSave = true;
        if (currentUserRole == "RCV" && isRejectionTab) {
            isNotAuthToSave = false;
        }
        else if (currentUserRole == "QAC" && isLogisticTab) {
            isNotAuthToSave = false;
        }


        //kalau complete jangan di save
        //atau kalau bukan yang berhak: 
        //     - bukan receiving di rejectionLetter
        //     - bukan QAC di Logistic
        if (isComplete || isNotAuthToSave) {
            self.RejectionForm_OpenPrintWindow();
            return false;
        }

        //saving
        self.RejectionForm_SaveUpdate_defered(null, formId, 'Update').then(function (response) {
            if (response.ErrorMessage !== undefined) {
                alert(response.ErrorMessage);
                return;
            }
            
            //sesudah save, baru print
            self.RejectionForm_OpenPrintWindow();
        });

        
        return false;
    };

    
    //save sebelum print (kalau authorized by tab)
    this.RejectionForm_BtnPrintManual_Clicked = function (s, e) {

        var formId = 'pdnRejectLogistic';

        var rejectionTab = $('li.active [href=#PdnContent]');

        var isRejectionTab = false;
        var isLogisticTab = false;

        if (rejectionTab[0] != undefined) {
            isRejectionTab = true;
        } else {
            isLogisticTab = true;
        }

        //validate tab content before printing
        var isFormValid = false;

        if (isRejectionTab) {
            isFormValid = ASPxClientEdit.ValidateEditorsInContainerById("PdnContent");
        }
        else if (isLogisticTab) {
            isFormValid = ASPxClientEdit.ValidateEditorsInContainerById("PdnLogisticContentId");
        }

        if (!isFormValid) {
            alert(self.ErrorValidationMessage);
            return;
        }

        var currentUserRole = $('#pdnRejectLogistic input[name="CurrentUserRole"]').val();

        var isNotAuthToSave = true;
        if (currentUserRole == "RCV" && isRejectionTab) {
            isNotAuthToSave = false;
        }
        else if (currentUserRole == "QAC" && isLogisticTab) {
            isNotAuthToSave = false;
        }

        if (isNotAuthToSave) {
            self.RejectionForm_OpenPrintWindow_Manual();
            return false;
        }
        
        
        self.RejectionForm_SaveUpdateManual_defered(null, formId, 'Update').then(function (response) {
            if (response.ErrorMessage !== undefined) {
                alert(response.ErrorMessage);
                return;
            }

            //sesudah save, baru print
            self.RejectionForm_OpenPrintWindow_Manual();

            //self.TestThen(response, 'update', formId);

        });

        
        return false;
    };

    this.RejectionForm_SubmitReject_Clicked = function (s, e) {
        if (confirm("If you continue, you will not be able to edit the document anymore. Continue?") == true) {
            var isFormValid = ASPxClientEdit.ValidateGroup("");

            if (!isFormValid) {
                alert(self.ErrorValidationMessage);
                return;
            }

            var formId = 'pdnRejectLogistic';

            self.RejectionForm_SaveUpdate_defered(null, formId, 'Update').then(function (response) {
                if (response.ErrorMessage !== undefined) {
                    alert(response.ErrorMessage);
                    return;
                }
                self.Wf_Rejection_deferred(formId, '_SubmitRejection');

            });
            return;
        } else {
            return;
        }
    };

    this.Wf_Rejection_deferred = function (formId, action) {
        var pdnId = $('#' + formId + ' input[name="PdnHeader.Id"]').val();
        
        var pdnStatus = "";
        var $form = $("#" + formId);
        //var url = $form.attr('action') + 'WorkflowEvent_GenericState_RoleAction';
        var url = this.Url_RejectionFormController + '_SubmitRejection';

        var formData = { pdnId: pdnId };
        //var formData = { pdnId: pdnId }
        var areaPlaceHoler = this.AreaPlaceHolderId;

        return $.ajax({
            type: "post",
            url: url,
            data: formData,
            success: function (response) {
                if (response.ErrorMessage !== undefined) {
                    alert(response.ErrorMessage);
                    return;
                }

                if (!this.isCloseOnSuccess) {

                    $("#" + areaPlaceHoler).html(response);
                    return;
                }
            },
            error: PdnLib.AjaxError
        });

    };

    //hanya untuk close popup saja, grid refresh dihandle di popupClosingEvent
    this.PdnForm_ClosePopup = function () {
        var loadingPanel = this.PdnFormLoadingPanelInstance;
        var popup = this.PdnFormPopupInstance;

        popup.Hide();
        
        ASPxClientEdit.ClearEditorsInContainerById(this.AreaPlaceHolderId);
    };

    this.SetPopupHeight = function () {
        var popupInstance = PdnFormPopupInstance;

        var browserHeight = $(window).height();
        popupInstance.SetHeight(browserHeight - 20);
        popupInstance.SetWidth(1200);

        if (!popupInstance.IsVisible()) return;
        popupInstance.UpdatePosition();
    }
    this.OnPopupInit = function (s, e) {
        this.SetPopupHeight();
    }

    this.PrepareAutomaticPopup = function () {

        var pdnId = $('input[name="AutoLoadPdnId"]').val();
        //var pdnId = 'FDB656DB-896D-411D-B53F-A41100C98B5E';

        if (pdnId == '') return;

        var loadingPanel = this.PdnFormLoadingPanelInstance;

        loadingPanel.Show();
        setTimeout(function () {
            loadingPanel.Hide();
            self.View(null, pdnId );
        }, 5000);
        //alert("Show popup populated by ncr or rejection letter");
        
    };


    this.OnIsMaterialStoredCheckboxChanged = function (s, e) {
        var isCheckedByUser = s.GetChecked();
        //alert('1' + isCheckedByUser);
        if (isCheckedByUser) {
            var checkBox = ASPxClientControl.GetControlCollection().GetByName("PdnRej.StatusMaterialReturned");
            checkBox.SetChecked(false);
            return;
        }
    };

    this.OnIsMaterialReturnedCheckboxChanged = function (s, e) {
        var isCheckedByUser = s.GetChecked();
        //alert('2' + isCheckedByUser);
        if (isCheckedByUser) {
            var checkBox = ASPxClientControl.GetControlCollection().GetByName("PdnRej.StatusMaterialStored");
            checkBox.SetChecked(false);
            return;
        }
    };
    
    this.OnIsDocumentStoredCheckboxChanged = function (s, e) {
        var isCheckedByUser = s.GetChecked();
        //alert('2' + isCheckedByUser);
        if (isCheckedByUser) {
            var checkBox = ASPxClientControl.GetControlCollection().GetByName("PdnRej.StatusDocumentReturned");
            checkBox.SetChecked(false);
            return;
        }
    };

    this.OnIsDocumentReturnedCheckboxChanged = function (s, e) {
        var isCheckedByUser = s.GetChecked();
        //alert('2' + isCheckedByUser);
        if (isCheckedByUser) {
            var checkBox = ASPxClientControl.GetControlCollection().GetByName("PdnRej.StatusDocumentStored");
            checkBox.SetChecked(false);
            return;
        }
    };

    this.OnProposedAcceptableCheckboxChanged = function (s, e) {
        var isCheckedByUser = s.GetChecked();
        //alert('2' + isCheckedByUser);
        if (isCheckedByUser) {
            var checkBox = ASPxClientControl.GetControlCollection().GetByName("PdnLog.ProposedRepair");
            var checkBox2 = ASPxClientControl.GetControlCollection().GetByName("PdnLog.ProposedReject");
            checkBox.SetChecked(false);
            checkBox2.SetChecked(false);
            return;
        }
    };

    this.OnProposedRepairCheckboxChanged = function (s, e) {
        var isCheckedByUser = s.GetChecked();
        //alert('2' + isCheckedByUser);
        if (isCheckedByUser) {
            var checkBox = ASPxClientControl.GetControlCollection().GetByName("PdnLog.ProposedAcceptable");
            var checkBox2 = ASPxClientControl.GetControlCollection().GetByName("PdnLog.ProposedReject");
            checkBox.SetChecked(false);
            checkBox2.SetChecked(false);
            return;
        }
    };

    this.OnProposedRejectCheckboxChanged = function (s, e) {
        var isCheckedByUser = s.GetChecked();
        //alert('2' + isCheckedByUser);
        if (isCheckedByUser) {
            var checkBox = ASPxClientControl.GetControlCollection().GetByName("PdnLog.ProposedAcceptable");
            var checkBox2 = ASPxClientControl.GetControlCollection().GetByName("PdnLog.ProposedRepair");
            checkBox.SetChecked(false);
            checkBox2.SetChecked(false);
            return;
        }
    };

    //attachment form
    this.ShowAddNewAttachment = function (s, e, optionalParams) {
        var prId = $('#pdnRejectLogistic input[name="PdnRej.PrId"]').val();
        if (prId == "00000000-0000-0000-0000-000000000000") {
            alert('please save your document before adding attachment');
            return;
        }

        var popup = self.AttachmentFormPopupInstance;
        if (optionalParams !== undefined) {
            popup.SetHeaderText("Upload Partial Quantity Approval Letter");
        } else {
            popup.SetHeaderText("Supporting Document");
        }

        var parentId = prId;

        var url = this.Url_PdnFormController + '_AddAttachment';
        //var formData = $form.serialize();
        var formData = { parentId: parentId, optionalParams: optionalParams }
        var areaPlaceHoler = this.AttachmentFormPlaceHolderId;
        var loadingPanel = this.PdnFormLoadingPanelInstance;



        popup.Show();

        $.ajax({
            type: "get",
            url: url,
            data: formData,
            beforeSend: function (jqXHR, settings) { loadingPanel.Show(); },
            complete: function (jqXHR, textStatus) { loadingPanel.Hide(); },
            success: function (response) {
                if (response.ErrorMessage !== undefined) {
                    alert(response.ErrorMessage);
                    return;
                }

                if (!this.isCloseOnSuccess) {

                    $("#" + areaPlaceHoler).html(response);
                    return;
                }
            },
            error: PdnLib.AjaxError
        });
    };

    this.ShowEditAttachment = function (s, e, id) {
        //alert(id);
        //AttachmentFormPopupInstance.Show();

        var url = this.Url_PdnFormController + '_UpdateAttachment';
        //var formData = $form.serialize();
        var formData = { id: id }
        var areaPlaceHoler = this.AttachmentFormPlaceHolderId;
        var loadingPanel = this.PdnFormLoadingPanelInstance;

        var popup = this.AttachmentFormPopupInstance;

        popup.Show();

        $.ajax({
            type: "get",
            url: url,
            data: formData,
            beforeSend: function (jqXHR, settings) { loadingPanel.Show(); },
            complete: function (jqXHR, textStatus) { loadingPanel.Hide(); },
            success: function (response) {
                if (response.ErrorMessage !== undefined) {
                    alert(response.ErrorMessage);
                    return;
                }

                if (!this.isCloseOnSuccess) {

                    $("#" + areaPlaceHoler).html(response);
                    return;
                }


            },
            error: PdnLib.AjaxError
        });
    };

    this.AttachmentForm_BtnCloseClicked = function () {

        var popup = this.AttachmentFormPopupInstance;

        popup.Hide();

        ASPxClientEdit.ClearEditorsInContainerById(this.AttachmentFormPlaceHolderId);

        //refresh grid if necessary
        //self.AttachmentGrid_Refresh(); already handled by popup closing event
        //AttachmentGrid.Refresh();
    };

    this.AttachmentForm_OnPopupClosing = function () {
        //var loadingPanel = this.PdnFormLoadingPanelInstance;
        //var popup = this.AttachmentFormPopupInstance;

        //popup.Hide();// jangan hide di si ni lagi bisa infinite loop

        ASPxClientEdit.ClearEditorsInContainerById(this.AttachmentFormPlaceHolderId);

        //refresh grid if necessary
        self.AttachmentGrid_Refresh();
        //AttachmentGrid.Refresh();
    };

    this.AttachmentGrid_Refresh = function () {
        var grid = ASPxClientControl.GetControlCollection().GetByName(this.AttachmentGridInstanceName);
        grid.Refresh();
    }

    this.DeleteAttachment = function (id) {
        if (!confirm("Delete Entry?")) {
            return;
        }


        var url = this.Url_PdnFormController + '_DeleteAttachment';
        $.ajax({
            url: url, type: 'POST', dataType: 'json',
            data: { id: id },
            success: function (status) {

                if (status.ErrorMessage != null) {
                    alert(status.ErrorMessage);
                }
                else {
                    self.AttachmentGrid_Refresh();
                }
            }
        });
    };

    this.ShowBinaryAttachment = function (s, e, id) {

        var url = this.Url_PdnFormController + 'ShowBinaryAttachment?id=' + id;

        window.open(
          url,
          '_blank' // <- This is what makes it open in a new window.
        );


    };

    this.OnAttachmentGridBeginCallback = function (s, e) {
        var prId = $('#pdnRejectLogistic input[name="PdnRej.PrId"]').val();
        //var isFormEnabled = $('#pdnMainFormId input[name="IsFormEnabled"]').val();

        e.customArgs["parentId"] = prId;
        //e.customArgs["isFormenabled"] = isFormEnabled;
    };

    this.AttachmentForm_OnFileUploadComplete = function (s, e, optionalParams) {
        if (e.callbackData == '') return;

        var splitted = e.callbackData.split('|');
        //        $('#previewImage').attr('src', splitted[2] + "?refresh=" + (new Date()).getTime()); 
        //        $('#NewUploadedPictureUrl').val(splitted[4]);

        var contentType = splitted[8];

        $("#AttachmentContentType").val(contentType);
        var originalFileNameNoExt = splitted[7];
        var savedTempFile = splitted[4];

        DocUrl.SetText(savedTempFile);

        //dipakai untuk upload change request
        if (optionalParams === undefined) {
            Label.SetText(originalFileNameNoExt);
        }

        if (e.isValid) {
            toggleUploadImage();
        }
        //toggleUploadImage();
    };

    this.AttachmentForm_Submit = function (action, formId) {
        var areaPlaceHoler = this.AttachmentFormPlaceHolderId;

        var isFormValid = ASPxClientEdit.ValidateEditorsInContainerById(areaPlaceHoler);
        if (!isFormValid) {
            return;
        }

        var $form = $("#" + formId);
        var url = $form.attr('action') + action;
        var formData = $form.serialize();
        var popup = self.AttachmentFormPopupInstance;


        $.ajax({
            type: "post",
            url: url,
            data: formData,
            success: function (response) {
                if (response.ErrorMessage !== undefined) {
                    alert(response.ErrorMessage);
                    return;
                }

                //hide popup on success
                popup.Hide();
                self.AttachmentGrid_Refresh();
            },
            error: PdnLib.AjaxError
        });
    };

    //only get triggered when form is in manual mode
    this.RlForm_TxtVendorName_TextChanged = function (s, e) {
        var supplierTextbox = ASPxClientControl.GetControlCollection().GetByName("PdnRej.Supplier");
        supplierTextbox.SetText(s.GetText());
    };

    this.RlForm_BtnSendNotification_Clicked = function (s, e) {
        var prId = $('#pdnRejectLogistic input[name="PdnRej.PrId"]').val();
        
        var formData = { prId: prId }
        var url = this.Url_PdnFormController + '_SendNotification';
        var loadingPanel = this.PdnFormLoadingPanelInstance;

        var isFormValid = ASPxClientEdit.ValidateEditorsInContainerById('PdnContent');
        if (!isFormValid) {
            alert(self.ErrorValidationMessage);
            return;
        }


        //sesudah save, baru send notification, 
        //make sure yang bisa save itu...
        $.ajax({
            type: "post",
            url: url,
            data: formData,
            beforeSend: function (jqXHR, settings) { loadingPanel.Show(); },
            complete: function (jqXHR, textStatus) { loadingPanel.Hide(); },
            success: function (response) {
                if (response.ErrorMessage !== undefined) {
                    alert(response.ErrorMessage);
                    return;
                }

                alert(response.Result);
                $('#ManualNotificationIndicator').html(response.Result);
            },
            error: PdnLib.AjaxError
        });

        return false; //below code is not used

        //saving
        var formId = 'pdnRejectLogistic';
        self.RejectionForm_SaveUpdateManual_defered(null, formId, 'Update').then(function (response) {
            if (response.ErrorMessage !== undefined) {
                alert(response.ErrorMessage);
                return;
            }

            //sesudah save, baru send notification, 
            //make sure yang bisa save itu...
            $.ajax({
                type: "post",
                url: url,
                data: formData,
                beforeSend: function (jqXHR, settings) { loadingPanel.Show(); },
                complete: function (jqXHR, textStatus) { loadingPanel.Hide(); },
                success: function (response) {
                    if (response.ErrorMessage !== undefined) {
                        alert(response.ErrorMessage);
                        return;
                    }

                    alert("Notification Queued at: " + response.Result);
                },
                error: PdnLib.AjaxError
            });
            
        });
    };

    //item selection
    this.NcrForm_BtnSelectItems_Clicked = function (s, e, formPurpose) {

        var selectionItemMappingString = "";

        if (formPurpose == 'ncr') {
            selectionItemMappingString = $('#pdnRejectLogistic input[name="PdnLog.ItemSelectionMappingString"]').val();
        } else {
            selectionItemMappingString = $('#pdnRejectLogistic input[name="PdnRej.ItemSelectionMappingString"]').val();
        }
        
        
        self.ShowItemSelection(s, e, selectionItemMappingString, formPurpose);

        return false;
    };

    this.ShowItemSelection = function (s, e, selectionItemMappingString, formPurpose) {

        if (formPurpose == 'ncr') {
        } else {
        }
        var pdnId = $('#pdnRejectLogistic input[name="PdnHeader.Id"]').val();
        if (pdnId == "00000000-0000-0000-0000-000000000000") {
            alert('this form has no pdn');
            return;
        }

        

        var popup = self.ItemSelectionFormPopupInstance;
        
        var url = this.Url_PdnFormController + '_ShowItemSelectionForm';
        //var formData = $form.serialize();
        var formData = { pdnId: pdnId, selectionItemMappingString: selectionItemMappingString, formPurpose: formPurpose }
        var areaPlaceHoler = this.ItemSelectionFormPlaceHolderId;
        var loadingPanel = this.PdnFormLoadingPanelInstance;

        var browserHeight = $(window).height();
        var browserWidth = 1200;
        popup.SetHeight(browserHeight - 80);
        popup.SetWidth(browserWidth - 60);

        popup.Show();

        $.ajax({
            type: "post",
            url: url,
            data: formData,
            beforeSend: function (jqXHR, settings) { loadingPanel.Show(); },
            complete: function (jqXHR, textStatus) { loadingPanel.Hide(); },
            success: function (response) {
                if (response.ErrorMessage !== undefined) {
                    alert(response.ErrorMessage);
                    return;
                }

                if (!this.isCloseOnSuccess) {

                    $("#" + areaPlaceHoler).html(response);
                    return;
                }
            },
            error: PdnLib.AjaxError
        });
    };

    this.NcrForm_SelectionItemForm_Closed = function(s, e) {
        var popup = self.ItemSelectionFormPopupInstance;
        popup.Hide();
    };

    this.NcrForm_SelectionItemForm_Submit = function(s, e, formPurpose) {
        var counter = $('#ItemSelectionFormId input[name="SelectionForm.PdnItemCount"]').val();
        var selectionMapping = "";
        var selectedItemsResult = "";
        var selectedItemsDescriptionResult = "";

        for (var i = 0; i < counter; i++) {
            var currentRowString = "MasterPoLineItems[{enum}]".replace(/{enum}/g, i);
            var checkBox = ASPxClientControl.GetControlCollection().GetByName(currentRowString + ".Ui_IsChecked_Rejection");
            var poItem = ASPxClientControl.GetControlCollection().GetByName(currentRowString + ".PoItem");
            var rejectedQuantity = ASPxClientControl.GetControlCollection().GetByName(currentRowString + ".Ui_RejectedQuantity");
            var uom = ASPxClientControl.GetControlCollection().GetByName(currentRowString + ".MaterialUnit");

            var materialDesc = ASPxClientControl.GetControlCollection().GetByName(currentRowString + ".MaterialDescription");

            //prepare values: mapping, items, items description
            if (checkBox.GetChecked()) {
                var selectionMappingTemp = "[" + poItem.GetValue() + "|" + rejectedQuantity.GetValue() + "]";

                var selectedItems = poItem.GetValue() + "=" + rejectedQuantity.GetValue() + uom.GetValue() + ", ";

                var selectedDescription = poItem.GetValue() + "="+ materialDesc.GetValue() + "; ";

                selectionMapping += selectionMappingTemp;
                selectedItemsResult += selectedItems;

                selectedItemsDescriptionResult += selectedDescription;

            }

            //alert(temp);
        }

        selectedItemsResult = selectedItemsResult.replace(/, $/, '.');
        selectedItemsDescriptionResult = selectedItemsDescriptionResult.replace(/; $/, '.\n\n');

        //setValue
        if (formPurpose == 'ncr') {
            var items = ASPxClientControl.GetControlCollection().GetByName("PdnLog.ItemNumber");
            items.SetText(selectedItemsResult);

            $('#pdnRejectLogistic input[name="PdnLog.ItemSelectionMappingString"]').val(selectionMapping);

            var itemsDesc = ASPxClientControl.GetControlCollection().GetByName("PdnLog.NonConfenance");
            itemsDesc.SetText(selectedItemsDescriptionResult);

        } else {// rejectionletter
            var items = ASPxClientControl.GetControlCollection().GetByName("PdnRej.ItemNumbers");
            items.SetText(selectedItemsResult);

            $('#pdnRejectLogistic input[name="PdnRej.ItemSelectionMappingString"]').val(selectionMapping);

            var itemsDesc = ASPxClientControl.GetControlCollection().GetByName("PdnRej.SelectedItemDescription");
            itemsDesc.SetText(selectedItemsDescriptionResult);
        }

        

        var popup = self.ItemSelectionFormPopupInstance;
        popup.Hide();
    }

}//end PdnLib.RejectionLetterLib

PdnLib.MainPdnFormLib = function () {
    var self = this;
    this.IsCloseOnSuccess = false; //false = habis sucess save/update, form di replace dengan yang baru
    this.AreaPlaceHolderId = 'PdnFormContentId';
    this.AttachmentFormPlaceHolderId = 'AttachmentFormContentId';
    this.SchedulingFormPlaceHolderId = 'SchedulingFormContentId';
    this.PdnFormLoadingPanelInstance = null;
    this.PdnFormPopupInstance = 'PdnFormPopupInstance';
    this.SchedulingFormPopupInstance = null;
    this.AttachmentFormPopupInstance = null;
    this.AttachmentGridInstanceName = null;

    this.WorkflowHistoryPopupInstance = null;
    this.WorkflowHistoryPlaceHolderId = "WorkflowHistoryContentId";

    this.Url_PdnFormController = null;

    this.ErrorValidationMessage = "Error validating; Please make sure all required fields are not empty";

    this.PdnForm_Submit = function (action, formId) {
        var areaPlaceHoler = this.AreaPlaceHolderId;
        var loadingPanel = this.PdnFormLoadingPanelInstance;

        var isFormValid = ASPxClientEdit.ValidateEditorsInContainerById(areaPlaceHoler);
        if (!isFormValid) {
            alert(self.ErrorValidationMessage);
            return;
        }
        
        var $form = $("#" + formId);
        var url = $form.attr('action')+action;
        var formData = $form.serialize();

        $.ajax({
            type: "post",
            url: url,
            data: formData,
            beforeSend: function (jqXHR, settings) { loadingPanel.Show(); },
            complete: function (jqXHR, textStatus) { loadingPanel.Hide(); },
            success: function (response) {
                if (response.ErrorMessage !== undefined) {
                    alert(response.ErrorMessage);
                    return;
                }

                if (!this.isCloseOnSuccess) {
                    
                    $("#" + areaPlaceHoler).html(response);
                    self.PdnForm_PrepareUiAfterLoading();
                    return;
                }
            },
            error: PdnLib.AjaxError
        });
    };

    this.PdnForm_DeletePdn = function (formId) {
        if (!confirm("Delete Entry?")) {
            return;
        }


        var pdnId = $('#' + formId + ' input[name="PdnId"]').val();
        var $form = $("#" + formId);
        var url = $form.attr('action') + "_DeletePdn";
        //var formData = $form.serialize();
        var formData = { pdnId: pdnId }
        var areaPlaceHoler = this.AreaPlaceHolderId;
        var loadingPanel = this.PdnFormLoadingPanelInstance;
        var popup = this.PdnFormPopupInstance;

       

        var ajaxShowEditMode = $.ajax({
            type: "get",
            url: url,
            data: formData,
            beforeSend: function (jqXHR, settings) { loadingPanel.Show(); },
            complete: function (jqXHR, textStatus) { loadingPanel.Hide(); },
            success: function (response) {
                if (response.ErrorMessage !== undefined) {
                    alert(response.ErrorMessage);
                    return;
                }

                popup.Hide();
                gridMasterDetail.Refresh();
                return;
            },
            error: PdnLib.AjaxError
        });

        
    };

    this.PdnForm_Submit_Deferred = function (action, formId) {
        var areaPlaceHoler = this.AreaPlaceHolderId;
        var loadingPanel = this.PdnFormLoadingPanelInstance;

        var isFormValid = ASPxClientEdit.ValidateEditorsInContainerById(areaPlaceHoler);
        if (!isFormValid) {
            alert(self.ErrorValidationMessage);
            return;
        }

        var $form = $("#" + formId);
        var url = $form.attr('action') + action;
        var formData = $form.serialize();

        return $.ajax({
            type: "post",
            url: url,
            data: formData,
            beforeSend: function (jqXHR, settings) { loadingPanel.Show(); },
            complete: function (jqXHR, textStatus) { loadingPanel.Hide(); },
            success: function (response) {
                if (response.ErrorMessage !== undefined) {
                    alert(response.ErrorMessage);
                    return;
                }

                if (!this.isCloseOnSuccess) {

                    $("#" + areaPlaceHoler).html(response);
                    return;
                }
            },
            error: PdnLib.AjaxError
        });
    };

    this.PdnForm_GoToEditMode = function (formId) {
        var pdnId = $('#' + formId + ' input[name="PdnId"]').val();
        var $form = $("#" + formId);
        var url = $form.attr('action')+"_ShowEditMode";
        //var formData = $form.serialize();
        var formData = { pdnId: pdnId }
        var areaPlaceHoler = this.AreaPlaceHolderId;
        var loadingPanel = this.PdnFormLoadingPanelInstance;

        var ajaxShowEditMode = $.ajax({
            type: "get",
            url: url,
            data: formData,
            beforeSend: function (jqXHR, settings) { loadingPanel.Show(); },
            complete: function (jqXHR, textStatus) {
                self.PdnForm_LineItem_Scan_ForEdit(); loadingPanel.Hide(); },
            success: function(response) {
                if (response.ErrorMessage !== undefined) {
                    alert(response.ErrorMessage);
                    return;
                }

                if (!this.isCloseOnSuccess) {

                    $("#" + areaPlaceHoler).html(response);
                    return;
                }
            },
            error: PdnLib.AjaxError
        });

        $.when(ajaxShowEditMode).done(function (r) {
            ////always update form 
            //if (r.ErrorMessage !== undefined) {
            //    alert(r.ErrorMessage);
            //    return;
            //}
            self.PdnForm_PrepareUiAfterLoading();
        });
    };

    this.PdnForm_GoToViewMode = function (formId) {
        var pdnId = $('#' + formId + ' input[name="PdnId"]').val();
        var $form = $("#" + formId);
        var url = $form.attr('action') + "View";
        //var formData = $form.serialize();
        var formData = { pdnId: pdnId }
        var areaPlaceHoler = this.AreaPlaceHolderId;
        var loadingPanel = this.PdnFormLoadingPanelInstance;

        var ajaxShowViewMode = $.ajax({
            type: "get",
            url: url,
            data: formData,
            beforeSend: function (jqXHR, settings) { loadingPanel.Show(); },
            complete: function (jqXHR, textStatus) { loadingPanel.Hide(); },
            success: function (response) {
                if (response.ErrorMessage !== undefined) {
                    alert(response.ErrorMessage);
                    return;
                }

                if (!this.isCloseOnSuccess) {

                    $("#" + areaPlaceHoler).html(response);
                    return;
                }

            },
            error: PdnLib.AjaxError
        });

        $.when(ajaxShowViewMode).done(function (r) {
            ////always update form 
            //if (r.ErrorMessage !== undefined) {
            //    alert(r.ErrorMessage);
            //    return;
            //}
            self.PdnForm_PrepareUiAfterLoading();
        });

    };

    

    this.ShowPopup_Add = function (action, poId) {
        //var $form = $("#" + formId);
        //var url = $form.attr('action');
        //var formData = $form.serialize();
        var url = this.Url_PdnFormController +'Add';
        var areaPlaceHoler = this.AreaPlaceHolderId;
        var loadingPanel = this.PdnFormLoadingPanelInstance;
        var popup = this.PdnFormPopupInstance;

        popup.Show();

        $.ajax({
            type: "get",
            url: url,
            data: {poId: poId},
            beforeSend: function(jqXHR, settings) { loadingPanel.Show(); },
            complete: function(jqXHR, textStatus ){ loadingPanel.Hide(); },
            success: function (response) {
                if (response.ErrorMessage !== undefined) {
                    alert(response.ErrorMessage);
                    popup.Hide();
                    return;
                }


                $("#" + areaPlaceHoler).html(response);
                popup.Show();


                //pageMan.popupInstanceFollowUp.Show();

            }
        });
    };

    this.ShowPopup_View = function (action, pdnId) {
        //var $form = $("#" + formId);
        //var url = $form.attr('action');
        //var formData = $form.serialize();
        var url =  this.Url_PdnFormController + 'View';
        var areaPlaceHoler = this.AreaPlaceHolderId;
        var loadingPanel = this.PdnFormLoadingPanelInstance;
        var popup = this.PdnFormPopupInstance;

        popup.Show();

        var ajaxShowViewMode = $.ajax({
            type: "get",
            url: url,
            data: { pdnId: pdnId },
            beforeSend: function (jqXHR, settings) { loadingPanel.Show(); },
            complete: function (jqXHR, textStatus) { loadingPanel.Hide(); },
            success: function (response) {
                if (response.ErrorMessage !== undefined) {
                    alert(response.ErrorMessage);
                    return;
                }


                $("#" + areaPlaceHoler).html(response);
                popup.Show();


                //pageMan.popupInstanceFollowUp.Show();

            }
        });

        $.when(ajaxShowViewMode).done(function (r) {
            ////always update form 
            //if (r.ErrorMessage !== undefined) {
            //    alert(r.ErrorMessage);
            //    return;
            //}
            self.PdnForm_PrepareUiAfterLoading();
        });
    };

    //hanya handle popup closing, grid refresh dihandle di popup closing
    this.PdnForm_ClosePopup = function() {
        var loadingPanel = this.PdnFormLoadingPanelInstance;
        var popup = this.PdnFormPopupInstance;

        popup.Hide();
        
        ASPxClientEdit.ClearEditorsInContainerById(this.AreaPlaceHolderId);
        //refresh grid if necessary
    };

    this.PdnForm_OnPopupClosing = function() {
        if (typeof (gridMasterDetail) !== 'undefined') gridMasterDetail.Refresh();
        if (typeof (GridPdnOnly) !== 'undefined') GridPdnOnly.Refresh();
    };

    this.SetPopupHeight = function () {
        var popupInstance = PdnFormPopupInstance;
        
        var browserHeight = $(window).height();
        var browserWidth = $(window).width();
        popupInstance.SetHeight(browserHeight - 20);
        popupInstance.SetWidth(browserWidth -20);
        //popupInstance.height = browserHeight - 20;
        //popupInstance.width = browserWidth - 20;
        popupInstance.left = 10;
        popupInstance.top = 10;

        if (!popupInstance.IsVisible()) return;
        popupInstance.UpdatePosition();
    }
    this.OnPopupInit = function (s, e) {
        this.SetPopupHeight();
    }

    ////workflow
    this.Wf_VendorSubmit = function (formId, action) {
        self.PdnForm_Submit_Deferred('Update', formId).then(function (response) {
            if (response.ErrorMessage !== undefined) {
                alert(response.ErrorMessage);
                return;
            }
            self.Wf_VendorSubmit_defered(formId, '');
        });


    };

    this.Wf_VendorSubmit_defered = function (formId, action) {
        var pdnId = $('#' + formId + ' input[name="PdnId"]').val();
        var $form = $("#" + formId);
        var url = $form.attr('action') + 'WorkflowEvent_GenericState_RoleAction';
        var formData = $form.serialize() + '&Ui_WorkflowAction=' + 'Submit';
        //formData.Ui_WorkflowAction = 'Submit';
        //var formData = { pdnId: pdnId }
        var areaPlaceHoler = this.AreaPlaceHolderId;
        var loadingPanel = this.PdnFormLoadingPanelInstance;

        //validation:
        var deliveryDateTime = ForwarderPlannedDeliveryDatetime.GetDate();
        if (deliveryDateTime == null) {
            alert('Planned Delivery Date cannot be empty');
            return;
        }

      
        var today = new Date();
        if (deliveryDateTime < today) {
            alert('Planned Delivery Date cannot be earlier than today');
            return;
        }


        return $.ajax({
            type: "post",
            url: url,
            data: formData,
            beforeSend: function (jqXHR, settings) { loadingPanel.Show(); },
            complete: function (jqXHR, textStatus) { loadingPanel.Hide(); },
            success: function (response) {
                if (response.ErrorMessage !== undefined) {
                    alert(response.ErrorMessage);
                    return;
                }

                if (!this.isCloseOnSuccess) {
                    loadingPanel.Show();
                    $("#" + areaPlaceHoler).html(response);
                    loadingPanel.Hide();
                    return;
                }
            },
            error: PdnLib.AjaxError
        });

    };

    this.Wf_GenericApproveRejectAction_old_punya = function (formId, action) {
        var pdnId = $('#' + formId + ' input[name="PdnId"]').val();
        var $form = $("#" + formId);
        var url = $form.attr('action') + 'WorkflowEvent_GenericState_RoleAction';
        var formData = $form.serialize();
        var formData = $form.serialize() + '&Ui_WorkflowAction=' +  action;
        //var formData = { pdnId: pdnId }
        var areaPlaceHoler = this.AreaPlaceHolderId;

        $.ajax({
            type: "post",
            url: url,
            data: formData,
            success: function (response) {
                if (response.ErrorMessage !== undefined) {
                    alert(response.ErrorMessage);
                    return;
                }

                if (!this.isCloseOnSuccess) {

                    $("#" + areaPlaceHoler).html(response);
                    return;
                }
            },
            error: PdnLib.AjaxError
        });

    };

    this.Wf_GenericApproveRejectAction = function (formId, action) {
        self.PdnForm_Submit_Deferred('Update', formId).then(function (response) {
            if (response.ErrorMessage !== undefined) {
                alert(response.ErrorMessage);
                return;
            }
            self.Wf_GenericApproveRejectAction_deferred(formId, action);
        });
    };

    this.Wf_GenericApproveRejectAction_deferred = function (formId, action) {
        var pdnId = $('#' + formId + ' input[name="PdnId"]').val();
        var $form = $("#" + formId);
        var url = $form.attr('action') + 'WorkflowEvent_GenericState_RoleAction';
        var formData = $form.serialize();
        var formData = $form.serialize() + '&Ui_WorkflowAction=' + action;
        //var formData = { pdnId: pdnId }
        var areaPlaceHoler = this.AreaPlaceHolderId;

        var loadingPanel = this.PdnFormLoadingPanelInstance;

        return $.ajax({
            type: "post",
            url: url,
            data: formData,
            beforeSend: function (jqXHR, settings) { loadingPanel.Show(); },
            complete: function (jqXHR, textStatus) { loadingPanel.Hide(); },
            success: function (response) {
                if (response.ErrorMessage !== undefined) {
                    alert(response.ErrorMessage);
                    return;
                }

                if (!this.isCloseOnSuccess) {
                    loadingPanel.Show();
                    $("#" + areaPlaceHoler).html(response);
                    loadingPanel.Hide();
                    return;
                }
            },
            error: PdnLib.AjaxError
        });

    };

    this.View = function (action, pdnId) {
        //var contFunction = this.AreaControlFunction;
        var url = this.Url_PdnFormController + 'PdnRejectedView';
        var areaPlaceHoler = this.AreaPlaceHolderId;
        var loadingPanel = this.PdnFormLoadingPanelInstance;
        var popup = this.PdnFormPopupInstance;

        popup.Show();

        $.ajax({
            type: "get",
            url: url,
            data: { pdnId: pdnId },
            beforeSend: function (jqXHR, settings) { loadingPanel.Show(); },
            complete: function (jqXHR, textStatus) { loadingPanel.Hide(); },
            success: function (response) {
                if (response.ErrorMessage !== undefined) {
                    alert(response.ErrorMessage);
                    return;
                }

                $("#" + areaPlaceHoler).html(response);
                popup.Show();
            }
        });
    };

    ////end workflow

    this.OnIsCompleteDeliveryCheckboxChanged = function (s, e) {
        var isCheckedByUser = s.GetChecked();

        if (!isCheckedByUser) {
            IsPartialItemDelivery.SetChecked(true);
            var isSuccess = this.Strategy_PartialItemDeliveryCheckedChanged(true);
            return;
        }

        this.Strategy_CompleteItemDeliveryCheckedChanged(true);


        ////if user is checked the checkbox;
        ////- validasi complete Delivery still available:
        ////    - check jumlah allocated:  kalau sudah terisi, tidak bisa complete delivery

        //var isEligible = this.IsEligibleForCompleteDelivery();

        //if (!isEligible) {
        //    alert("Cannot assign as Complete Delivery!\nLine item has been allocated.");
        //    s.SetChecked(false);
        //    return;
        //}


        //IsPartialItemDelivery.SetChecked(false);

        //this.SetFormAsCompleteDelivery();

        ////call partial quantity funct

        //var isChecked = IsPartialQuantityDelivery.GetChecked();
        //this.DisplayLabel(isChecked);

    };

    this.OnIsPartialItemDeliveryCheckboxChanged = function (s, e) {
        var isCheckedByUser = s.GetChecked();

        if (!isCheckedByUser) {
            IsCompleteDelivery.SetChecked(true);
            
            var isSuccess = this.Strategy_CompleteItemDeliveryCheckedChanged(true);
            return;
        }

        IsCompleteDelivery.SetChecked(false);

        this.ClearLineItems();

        var isChecked = IsPartialQuantityDelivery.GetChecked();
        //this.DisplayLabel(isChecked);


    };

    this.OnIsPartialQuantityDeliveryCheckboxChanged = function (s, e) {
        var isChecked = s.GetChecked();
        
        if (!isChecked && !IsPartialItemDelivery.GetChecked()) {
            s.SetChecked(true);
            //this.DisplayLabel(true);
            return;
        }

        IsCompleteDelivery.SetChecked(false);
        this.ClearLineItems();

    };

    this.OnIsChangeQuantityRequestedCheckboxChanged = function(s, e) {
        var isChecked = s.GetChecked();

        this.DisplayLabel(isChecked);
    };

    this.SetFormAsCompleteDelivery = function () {
        var poLineCount = $('#Ui_PoLineCount').val();
        //alert(poLineCount);
        //alert('handle delivery');
        for (var i = 0; i < poLineCount; i++) {
            this.SetMaxQuantity(i);
        }
    };

    this.SetMaxQuantity = function (i) {
        var editorClientId = "MasterPoLineItems[{enum}].Ui_IsChecked";
        var temp = editorClientId.replace(/{enum}/g, i);
        var currentRowString = "MasterPoLineItems[{enum}]".replace(/{enum}/g, i);
        var checkBox = ASPxClientControl.GetControlCollection().GetByName(currentRowString + ".Ui_IsChecked");

        checkBox.SetChecked(true);

        var remainingQuantity = ASPxClientControl.GetControlCollection().GetByName(currentRowString + ".UI_RemainingQuantity");

        var poMaterialQuantity = ASPxClientControl.GetControlCollection().GetByName(currentRowString + ".PoMaterialQuantity");



        var materialQuantity = ASPxClientControl.GetControlCollection().GetByName(currentRowString + ".MaterialQuantity");

        materialQuantity.SetValue(poMaterialQuantity.GetValue());

        remainingQuantity.SetValue(0);
    };

    this.SetMaxQuantity_Possible = function (i, params) {

        var lineModel = PdnLib.PdnLineItem_GetLineModel(i);


        var currentRowString = "MasterPoLineItems[{enum}]".replace(/{enum}/g, i);
        var checkBox = ASPxClientControl.GetControlCollection().GetByName(currentRowString + ".Ui_IsChecked");

        checkBox.SetChecked(true);
        
        var materialQuantity = ASPxClientControl.GetControlCollection().GetByName(currentRowString + ".MaterialQuantity");
        var remainingQuantity = ASPxClientControl.GetControlCollection().GetByName(currentRowString + ".UI_RemainingQuantity");

        //tidak ada bedanya quantity editable atau tidak: selalu harus saldo sisa
        if (lineModel.isQuantityEditable == true) {
            materialQuantity.SetValue(lineModel.val_PoQuantity - lineModel.val_Allocated);
            remainingQuantity.SetValue(0);
        } else {
            //materialQuantity.SetValue(lineModel.val_PoQuantity);
            materialQuantity.SetValue(lineModel.val_PoQuantity - lineModel.val_Allocated);
            remainingQuantity.SetValue(0);
        }
    };

    this.ResetMaterialLine = function (i) {
        var currentRowString = "MasterPoLineItems[{enum}]".replace(/{enum}/g, i);
        var checkBox = ASPxClientControl.GetControlCollection().GetByName(currentRowString + ".Ui_IsChecked");

        checkBox.SetChecked(false);

        var remainingQuantity = ASPxClientControl.GetControlCollection().GetByName(currentRowString + ".UI_RemainingQuantity");

        var allocatedQuantity = ASPxClientControl.GetControlCollection().GetByName(currentRowString + ".AllocatedQuantity");

        var poMaterialQuantity = ASPxClientControl.GetControlCollection().GetByName(currentRowString + ".PoMaterialQuantity");



        var materialQuantity = ASPxClientControl.GetControlCollection().GetByName(currentRowString + ".MaterialQuantity");

        

        var val_PoQuantity = PdnLib.TryGetNumber(poMaterialQuantity);
        
        var val_Allocated = PdnLib.TryGetNumber(allocatedQuantity);

        var temp = val_PoQuantity - val_Allocated - 0;

        remainingQuantity.SetValue(temp);
        materialQuantity.SetValue(0);

    };

    this.OnLineItemCheckedChanged = function (s, e, i) {
        var isChecked = s.GetChecked();
    
        var isSuccess = this.Strategy_OnLineItemCheckedChanged(s,e,i, isChecked);
    };

    this.CustomValidation_LineItemInputRequired = function (s, e, i) {

        var currentRowString = "MasterPoLineItems[{enum}]".replace(/{enum}/g, i);

        var checkbox = ASPxClientControl.GetControlCollection().GetByName(currentRowString + ".Ui_IsChecked");
        var isChecked = checkbox.GetChecked();



        var input = e.value;
        if(!isChecked)
            return;


        if (input==null || input == 0 || input==''  ) {
            e.isValid = false;
            e.errorText = "*";
        }

    }

   //untuk validasi format email
    this.CustomValidation_emailformat = function (s, e) {
       
        var input = e.value; //change form to id or containment selector
        //var re = /^(([^<>()[]\.,;:s@"]+(.[^<>()[]\.,;:s@"]+)*)|(".+"))@(([[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}])|(([a-zA-Z-0-9]+.)+[a-zA-Z]{2,}))$/igm;
        var re = /\S+@\S+\.\S+/;
        if (re.test(input)) {
            return re.test(input);
        } else {
            e.isValid = false;
            e.errorText = "*";
        }
    }

    //untuk addtional email
    this.CustomValidation_addemailformat = function (s, e) {

        var input = e.value; //change form to id or containment selector
        //var re = /^(([^<>()[]\.,;:s@"]+(.[^<>()[]\.,;:s@"]+)*)|(".+"))@(([[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}])|(([a-zA-Z-0-9]+.)+[a-zA-Z]{2,}))$/igm;
        var re = /\S+@\S+\.\S+/;
        if (re.test(input) || input == null || input == 0 || input == '') {
            return re.test(input);
        } else {
            e.isValid = false;
            e.errorText = "*";
        }
    }

    this.OnLineItemMaterialQuantityChanged = function (s, e, i) {
        var materialQuantityValue = PdnLib.TryGetNumber(s);

        //allow unchecked //checked
        var currentRowString = "MasterPoLineItems[{enum}]".replace(/{enum}/g, i);

        var allocatedQuantity = ASPxClientControl.GetControlCollection().GetByName(currentRowString + ".AllocatedQuantity");
        var remainingQuantity = ASPxClientControl.GetControlCollection().GetByName(currentRowString + ".UI_RemainingQuantity");
        var poMaterialQuantity = ASPxClientControl.GetControlCollection().GetByName(currentRowString + ".PoMaterialQuantity");
        var allocatedQuantity = ASPxClientControl.GetControlCollection().GetByName(currentRowString + ".AllocatedQuantity");



        var val_PoQuantity = PdnLib.TryGetNumber(poMaterialQuantity);
        
        var val_Allocated = PdnLib.TryGetNumber(allocatedQuantity);

        var temp = val_PoQuantity - val_Allocated - materialQuantityValue;

        remainingQuantity.SetValue(temp);

        this.CalculateDeliveryOption();
    };


    this.ClearLineItems = function() {
        var poLineCount = $('#Ui_PoLineCount').val();
        //alert(poLineCount);
        //alert('handle delivery');
        for (var i = 0; i < poLineCount; i++) {
            this.ResetMaterialLine(i);
        }
    };


    this.IsEligibleForCompleteDelivery = function () {
        var poLineCount = $('#Ui_PoLineCount').val();
        //alert(poLineCount);
        //alert('handle delivery');
        var allocatedQuantity = 0;
        for (var i = 0; i < poLineCount; i++) {
            allocatedQuantity += this.GetAllocatedQuantity(i);
        }

        if (allocatedQuantity > 0)
            return false;

        return true;
    }

    this.GetAllocatedQuantity = function (i) {

        var currentRowString = "MasterPoLineItems[{enum}]".replace(/{enum}/g, i);

        var allocatedQuantity = ASPxClientControl.GetControlCollection().GetByName(currentRowString + ".AllocatedQuantity");

        return allocatedQuantity.GetValue();
    };

    //scan each line items etc.
    this.CalculateDeliveryOption = function () {
        var summary = { totalQuantity: 0.00, totalAllocated: 0.00, totalPoQuantity: 0.00, isPartialQuantityChecked: false };

        var poLineCount = $('#Ui_PoLineCount').val();

        for (var i = 0; i < poLineCount; i++) {
            this.CalculateSummary(i,summary);
        }

        if (summary.totalQuantity > 0 &&
            summary.totalQuantity == summary.totalPoQuantity) {

            IsCompleteDelivery.SetChecked(true);
            IsPartialItemDelivery.SetChecked(false);
        } else {
            IsCompleteDelivery.SetChecked(false);
            IsPartialItemDelivery.SetChecked(true);
        }

        if (summary.isPartialQuantityChecked) {
            IsPartialQuantityDelivery.SetChecked(true);
        } else {
            IsPartialQuantityDelivery.SetChecked(false);
        }

    };

    this.CalculateSummary = function (i, summary) {
        var currentRowString = "MasterPoLineItems[{enum}]".replace(/{enum}/g, i);

        var allocatedQuantity = ASPxClientControl.GetControlCollection().GetByName(currentRowString + ".AllocatedQuantity");
        
        var poMaterialQuantity = ASPxClientControl.GetControlCollection().GetByName(currentRowString + ".PoMaterialQuantity");

        var materialQuantity = ASPxClientControl.GetControlCollection().GetByName(currentRowString + ".MaterialQuantity");

        val_PoQuantity = PdnLib.TryGetNumber(poMaterialQuantity);
        val_MatQuantity = PdnLib.TryGetNumber(materialQuantity);
        val_Allocated = PdnLib.TryGetNumber(allocatedQuantity);

        if(val_MatQuantity >0 && val_MatQuantity < val_PoQuantity)
        {
            summary.isPartialQuantityChecked = true;
        }


        summary.totalQuantity += val_MatQuantity;
        summary.totalPoQuantity += val_PoQuantity;
        summary.totalAllocated += val_Allocated;
    };

    //checkboxes etc
    this.Strategy_PartialItemDeliveryCheckedChanged = function(isChecked) {
        if (isChecked) {
            this.ClearLineItems();
        }


        return true; //isSuccess
    };

    //checkboxes etc
    this.Strategy_CompleteItemDeliveryCheckedChanged = function (isChecked) {
        if (isChecked) {
            var isEligible = this.IsEligibleForCompleteDelivery();

            if (!isEligible) {
                alert("Cannot assign as Complete Delivery!\nLine item has been allocated.");
                IsCompleteDelivery.SetChecked(false);
                IsPartialItemDelivery.SetChecked(true);
                return false;
            }


            IsPartialItemDelivery.SetChecked(false);
            IsPartialQuantityDelivery.SetChecked(false);

            this.SetFormAsCompleteDelivery();

            //call partial quantity funct

            var isChecked = IsChangeQuantityRequested.GetChecked();
            this.DisplayLabel(isChecked);
        }


        return true; //isSuccess
    };

    this.Strategy_OnLineItemCheckedChanged = function (s, e, i, isChecked) {

        if (!isChecked) { //unchecked => set as partial item delivery
            IsPartialItemDelivery.SetChecked(true);
            IsCompleteDelivery.SetChecked(false);

            this.ResetMaterialLine(i);

        } else {
            //IsPartialItemDelivery.SetChecked(false);
            //IsCompleteDelivery.SetChecked(true);
        }

        //allow unchecked //checked
        var currentRowString = "MasterPoLineItems[{enum}]".replace(/{enum}/g, i);

        var allocatedQuantity = ASPxClientControl.GetControlCollection().GetByName(currentRowString + ".AllocatedQuantity");

        var poMaterialQuantity = ASPxClientControl.GetControlCollection().GetByName(currentRowString + ".PoMaterialQuantity");

        var temp = allocatedQuantity.GetValue() - poMaterialQuantity.GetValue();

        //prevent check if already allocated 
        if (temp >= 0 && isChecked) {
            s.SetChecked(false);
            return;
        }

        var materialQuantity = ASPxClientControl.GetControlCollection().GetByName(currentRowString + ".MaterialQuantity");

        var params = self.PdnForm_GetPartialQuantity_FormInfo();

        //if already checked, then uncheded:
        if (!isChecked) {
            var allocated = allocatedQuantity.GetValue();
            var current = materialQuantity.GetValue();

            var temp2 = allocated - current;

            //allocatedQuantity.SetValue(temp2);

            materialQuantity.SetValue(0);

        } else {

            self.SetMaxQuantity_Possible(i, params);

            //allocatedQuantity.SetValue(poMaterialQuantity.GetValue());

            //enable/disable partialQuantity
        }

        this.CalculateDeliveryOption(); //kalau checkbox sudah full, tulis complete delivery

        this.PdnFormLineItem_SetEnabled(i, params);
    };

    //Send Note
    this.PdnForm_OnBtnSendNoteSubmitClicked = function (s, e) {
        var pdnId = $('#pdnMainFormId input[name="PdnId"]').val();

        var formData = { pdnId: pdnId }
        var url = this.Url_PdnFormController + '_SendNotes';
        var loadingPanel = this.PdnFormLoadingPanelInstance;

        //sesudah save, baru send notification, 
        //make sure yang bisa save itu...
        $.ajax({
            type: "post",
            url: url,
            data: formData,
            beforeSend: function (jqXHR, settings) { loadingPanel.Show(); },
            complete: function (jqXHR, textStatus) { loadingPanel.Hide(); },
            success: function (response) {
                if (response.ErrorMessage !== undefined) {
                    alert(response.ErrorMessage);
                    return;
                }

                 alert(response.Result);
               
            },
            error: PdnLib.AjaxError
        });

        return false;
    };


    //notes:
    this.PdnForm_OnBtnNoteSubmitClicked = function (s, e, role) {
        var pdnId = $('#pdnMainFormId input[name="PdnId"]').val();

        var notes = "";
        if (role == 'ven') {
            notes = VendorNoteInput.GetText();
        } else {
            notes = WarehouseNoteInput.GetText();
        }

        var formData = { pdnId: pdnId, notes: notes }
        var loadingPanel = this.PdnFormLoadingPanelInstance;

        var url = this.Url_PdnFormController + '_AddNotes';
        var areaPlaceHoler = 'VendorNoteDisplayPlaceholderId';

        $.ajax({
            type: "post",
            url: url,
            data: formData,
            beforeSend: function (jqXHR, settings) { loadingPanel.Show(); },
            complete: function (jqXHR, textStatus) { loadingPanel.Hide(); },
            success: function (response) {
                if (response.ErrorMessage !== undefined) {
                    alert(response.ErrorMessage);
                    return;
                }

                if (role == "ven") {
                    areaPlaceHoler = 'VendorNoteDisplayPlaceholderId';
                    $("#" + areaPlaceHoler).html(response);
                    VendorNoteInput.SetText("");
                } else {
                    areaPlaceHoler = 'WarehouseNoteDisplayPlaceholderId';
                    $("#" + areaPlaceHoler).html(response);
                    WarehouseNoteInput.SetText("");
                }

                var objDiv = document.getElementById(areaPlaceHoler);
               objDiv.scrollTop = objDiv.scrollHeight;
                //self.AttachmentGrid_Refresh();
                //refresh notes
            },
            error: PdnLib.AjaxError
        });
    }

    //attachments:
    this.AttachmentItem_SetOkNotOk = function (s, e, attachmentId,okNotOk) {

        var isChecked = s.GetChecked();

        var formData = { attachmentId: attachmentId, okNotOk: okNotOk, isChecked:isChecked }
        var loadingPanel = this.PdnFormLoadingPanelInstance;

        var url = this.Url_PdnFormController + '_AttachmentSetOkNotOk';

        $.ajax({
            type: "post",
            url: url,
            data: formData,
            beforeSend: function (jqXHR, settings) { loadingPanel.Show(); },
            complete: function (jqXHR, textStatus) { loadingPanel.Hide(); },
            success: function (response) {
                if (response.ErrorMessage !== undefined) {
                    alert(response.ErrorMessage);
                    return;
                }

                self.AttachmentGrid_Refresh();

            },
            error: PdnLib.AjaxError
        });
    };

    this.OnAttachmentGridBeginCallback = function(s, e) {
        var pdnId = $('#pdnMainFormId input[name="PdnId"]').val();
        var isFormEnabled = $('#pdnMainFormId input[name="IsFormEnabled"]').val();

        e.customArgs["pdnId"] = pdnId;
        e.customArgs["isFormenabled"] = isFormEnabled;
    };

    this.OnBtnShowHistoryClicked = function () {
        var url = this.Url_PdnFormController + '_ShowWorkflowHistory';
        var pdnId = $('#pdnMainFormId input[name="PdnId"]').val();
        var formData = { pdnId: pdnId }
        var areaPlaceHoler = this.WorkflowHistoryPlaceHolderId;
        var loadingPanel = this.PdnFormLoadingPanelInstance;

        var popup = this.WorkflowHistoryPopupInstance;
        popup.SetHeaderText("History");
        popup.SetWidth(950);
        popup.SetHeight(540);


        popup.Show();

        $.ajax({
            type: "post",
            url: url,
            data: formData,
            beforeSend: function (jqXHR, settings) { loadingPanel.Show(); },
            complete: function (jqXHR, textStatus) { loadingPanel.Hide(); },
            success: function (response) {
                if (response.ErrorMessage !== undefined) {
                    alert(response.ErrorMessage);
                    return;
                }

                if (!this.isCloseOnSuccess) {

                    $("#" + areaPlaceHoler).html(response);
                    return;
                }
            },
            error: PdnLib.AjaxError
        });
    };

    //pakai popup yang sama dengan workflow history
    this.OnBtnShowPartialQuantityHistoryClicked = function (poItem) {
        var url = this.Url_PdnFormController + '_ShowPartialQuantityHistory';
        var pdnId = $('#pdnMainFormId input[name="PdnId"]').val();
        var poNo = $('#pdnMainFormId input[name="PoNo"]').val();

        var formData = { pdnId: pdnId, poItem: poItem, poNo: poNo }
        var areaPlaceHoler = this.WorkflowHistoryPlaceHolderId;
        var loadingPanel = this.PdnFormLoadingPanelInstance;

        var popup = this.WorkflowHistoryPopupInstance;
        popup.SetHeaderText("Partial Quantity History");
        popup.SetWidth(980);
        popup.SetHeight(400);

        popup.Show();

        $.ajax({
            type: "post",
            url: url,
            data: formData,
            beforeSend: function (jqXHR, settings) { loadingPanel.Show(); },
            complete: function (jqXHR, textStatus) { loadingPanel.Hide(); },
            success: function (response) {
                if (response.ErrorMessage !== undefined) {
                    alert(response.ErrorMessage);
                    return;
                }

                if (!this.isCloseOnSuccess) {

                    $("#" + areaPlaceHoler).html(response);
                    return;
                }
            },
            error: PdnLib.AjaxError
        });

    }

    this.ShowEditAttachment = function (s, e, id) {
        //alert(id);
        //AttachmentFormPopupInstance.Show();

        var url = this.Url_PdnFormController +'_UpdateAttachment';
        //var formData = $form.serialize();
        var formData = { id: id }
        var areaPlaceHoler = this.AttachmentFormPlaceHolderId;
        var loadingPanel = this.PdnFormLoadingPanelInstance;

        var popup = this.AttachmentFormPopupInstance;

        popup.Show();

        $.ajax({
            type: "get",
            url: url,
            data: formData,
            beforeSend: function (jqXHR, settings) { loadingPanel.Show(); },
            complete: function (jqXHR, textStatus) { loadingPanel.Hide(); },
            success: function (response) {
                if (response.ErrorMessage !== undefined) {
                    alert(response.ErrorMessage);
                    return;
                }

                if (!this.isCloseOnSuccess) {

                    $("#" + areaPlaceHoler).html(response);
                    return;
                }


            },
            error: PdnLib.AjaxError
        });
    };

    this.AttachmentGrid_Refresh = function () {
        var grid = ASPxClientControl.GetControlCollection().GetByName(this.AttachmentGridInstanceName);
        grid.Refresh();
    }

    this.DeleteAttachment = function (id) {
        if (!confirm("Delete Entry?")) {
            return;
        }
        

        var url = this.Url_PdnFormController + '_DeleteAttachment';
        $.ajax({
            url: url, type: 'POST', dataType: 'json',
            data: { id: id },
            success: function (status) {

                if (status.ErrorMessage != null) {
                    alert(status.ErrorMessage);
                }
                else {
                    self.AttachmentGrid_Refresh();
                }
            }
        });
    };

    this.ShowBinaryAttachment = function(s,e,id) {
        
        var url = this.Url_PdnFormController + 'ShowBinaryAttachment?id='+id;

        window.open(
          url,
          '_blank' // <- This is what makes it open in a new window.
        );


    };

    //show popup for uploading attachment
    this.OnbtnUploadChangeQuantityRequest_Clicked = function (s, e) {
        //bisa complex:

        //save dulu sebelum show attachment
        self.PdnForm_Submit_Deferred('Update', 'pdnMainFormId').then(function (response) {
            if (response.ErrorMessage !== undefined) {
                alert(response.ErrorMessage);
                return;
            }

            self.ShowAddNewAttachment(s, e, "changeRequest");
            
        });
        
    };
    
    this.ShowAddNewAttachment = function (s, e, optionalParams) {
        var pdnStatus = $('#pdnMainFormId input[name="PdnStatus"]').val();
        if (pdnStatus == "New") {
            alert('please save yout PDN before adding attachment');
            return;
        }

        var popup = self.AttachmentFormPopupInstance;
        if (optionalParams !== undefined ) {
            popup.SetHeaderText("Upload Partial Quantity Approval Letter");
        } else {
            popup.SetHeaderText("Supporting Document");
        }

        var pdnId = $('#pdnMainFormId input[name="PdnId"]').val();
        
        var url = this.Url_PdnFormController + '_AddAttachment';
        //var formData = $form.serialize();
        var formData = { pdnId: pdnId, optionalParams: optionalParams }
        var areaPlaceHoler = this.AttachmentFormPlaceHolderId;
        var loadingPanel = this.PdnFormLoadingPanelInstance;

        

        popup.Show();

        $.ajax({
            type: "get",
            url: url,
            data: formData,
            beforeSend: function (jqXHR, settings) { loadingPanel.Show(); },
            complete: function (jqXHR, textStatus) { loadingPanel.Hide(); },
            success: function (response) {
                if (response.ErrorMessage !== undefined) {
                    alert(response.ErrorMessage);
                    return;
                }

                if (!this.isCloseOnSuccess) {

                    $("#" + areaPlaceHoler).html(response);
                    return;
                }
            },
            error: PdnLib.AjaxError
        });
    };

    this.AttachmentForm_BtnCloseClicked = function () {
        
        var popup = this.AttachmentFormPopupInstance;

        popup.Hide();

        ASPxClientEdit.ClearEditorsInContainerById(this.AttachmentFormPlaceHolderId);

        //refresh grid if necessary
        //self.AttachmentGrid_Refresh(); already handled by popup closing event
        //AttachmentGrid.Refresh();
    };

    this.AttachmentForm_OnPopupClosing = function () {
        //var loadingPanel = this.PdnFormLoadingPanelInstance;
        //var popup = this.AttachmentFormPopupInstance;

        //popup.Hide();// jangan hide di si ni lagi bisa infinite loop
        
        ASPxClientEdit.ClearEditorsInContainerById(this.AttachmentFormPlaceHolderId);
        
        //refresh grid if necessary
        self.AttachmentGrid_Refresh();
        //AttachmentGrid.Refresh();
    };

    this.PdnFormSchedule_OnPopupClosing = function () {
        ASPxClientEdit.ClearEditorsInContainerById(this.SchedulingFormPlaceHolderId);
    };

    this.PdnFormSchedule_btnShowScheduleFormClicked = function (schedulingAction) {
        
        var url = this.Url_PdnFormController + '_AddSchedule';
        var areaPlaceHoler = this.SchedulingFormPlaceHolderId;
        var loadingPanel = this.PdnFormLoadingPanelInstance;
        var popup = this.SchedulingFormPopupInstance;
        var pdnId = $('#pdnMainFormId input[name="PdnId"]').val();

        popup.Show();

        $.ajax({
            type: "get",
            url: url,
            data: { pdnId: pdnId, schedulingAction: schedulingAction },
            beforeSend: function (jqXHR, settings) { loadingPanel.Show(); },
            complete: function (jqXHR, textStatus) { loadingPanel.Hide(); },
            success: function (response) {
                if (response.ErrorMessage !== undefined) {
                    alert(response.ErrorMessage);
                    popup.Hide();
                    return;
                }


                $("#" + areaPlaceHoler).html(response);
                popup.Show();


                //pageMan.popupInstanceFollowUp.Show();

            }
        });

    };

    this.PdnFormSchedule_btnAccept_Clicked = function (roleAction) {
        var areaPlaceHoler = this.SchedulingFormPlaceHolderId;

        var isFormValid = ASPxClientEdit.ValidateEditorsInContainerById(areaPlaceHoler);
        if (!isFormValid) {
            return;
        }

        if (!confirm("Submit Schedule And Save PDN?")) {
            return;
        }


        var url = this.Url_PdnFormController + '_AcceptSchedule';
        var areaPlaceHoler = this.SchedulingFormPlaceHolderId;
        var loadingPanel = this.PdnFormLoadingPanelInstance;
        var popup = this.SchedulingFormPopupInstance;
        var pdnId = $('#pdnMainFormId input[name="PdnId"]').val();
        var pdnStatus = $('#pdnMainFormId input[name="PdnStatus"]').val();
        
        var formData = { pdnId: pdnId,pdnStatus:pdnStatus, schedulingAction: roleAction };

        var submit = $.ajax({
            type: "post",
            url: url,
            data: formData,
            success: function (response) {
                if (response.ErrorMessage !== undefined) {
                    alert(response.ErrorMessage);
                    return;
                }
            },
            error: PdnLib.AjaxError
        });

        $.when(submit).done(function (r) {

            self.PdnForm_GoToViewMode('pdnMainFormId');
        });

    }

    this.PdnFormSchedule_btnSubmitSchedule_Clicked = function (action, formId) {
        var areaPlaceHoler = this.SchedulingFormPlaceHolderId;

        var isFormValid = ASPxClientEdit.ValidateEditorsInContainerById(areaPlaceHoler);
        if (!isFormValid) {
            return;
        }

        if (!confirm("Ensure that the Planned Delivery Date you've entered is correct.\nDo you want to submit the Delivery Date now?")) {
            return;
        }


        var url = this.Url_PdnFormController + '_AddSchedule';
        var areaPlaceHoler = this.SchedulingFormPlaceHolderId;
        var loadingPanel = this.PdnFormLoadingPanelInstance;
        var popup = this.SchedulingFormPopupInstance;
        var pdnId = $('#pdnMainFormId input[name="PdnId"]').val();
        var $form = $("#" + formId);
        var formData = $form.serialize();
        
        var submit = $.ajax({
            type: "post",
            url: url,
            data: formData,
            success: function (response) {
                if (response.ErrorMessage !== undefined) {
                    alert(response.ErrorMessage);
                    return;
                }

                //hide popup on success
                popup.Hide();
            },
            error: PdnLib.AjaxError
        });

        $.when(submit).done(function (r) {
            
            self.PdnForm_GoToViewMode('pdnMainFormId');
        });
        
    };

    this.PdnFormSchedule_BtnCloseClicked = function () {

        var popup = this.SchedulingFormPopupInstance;

        popup.Hide();

        ASPxClientEdit.ClearEditorsInContainerById(this.SchedulingFormPlaceHolderId);

        //refresh grid if necessary
        //self.AttachmentGrid_Refresh(); already handled by popup closing event
        //AttachmentGrid.Refresh();
    };


    //bedanya dengan attachment biasa: 
    //habis complete, bisa refresh main form
    this.UploadChangeRequest_Submit = function (action, formId) {
        var areaPlaceHoler = this.AttachmentFormPlaceHolderId;

        var isFormValid = ASPxClientEdit.ValidateEditorsInContainerById(areaPlaceHoler);
        if (!isFormValid) {
            return;
        }

        var $form = $("#" + formId);
        var url = $form.attr('action') + action;
        var formData = $form.serialize();
        var popup = self.AttachmentFormPopupInstance;

        //bedanya dengan attachment biasa: 
        //habis complete, bisa refresh main form karena pdnHeader sudah berubah +
        // go to edit mode + continue change quantity
        //+ bisa save update before uploading

        var submit_uploadChangeRequest = $.ajax({
            type: "post",
            url: url,
            data: formData,
            success: function (response) {
                if (response.ErrorMessage !== undefined) {
                    alert(response.ErrorMessage);
                    return;
                }

                //hide popup on success
                popup.Hide();
                self.AttachmentGrid_Refresh();
            },
            error: PdnLib.AjaxError
        });

        //todo: harus handel save di sini instead of before show attachment
        $.when(submit_uploadChangeRequest).done(function (r) {
            ////always update form 
            //if (r.ErrorMessage !== undefined) {
            //    alert(r.ErrorMessage);
            //    return;
            //}
            self.PdnForm_GoToEditMode('pdnMainFormId');
        });


    };

    this.AttachmentForm_Submit = function (action, formId) {
        var areaPlaceHoler = this.AttachmentFormPlaceHolderId;
        
        var isFormValid = ASPxClientEdit.ValidateEditorsInContainerById(areaPlaceHoler);
        if (!isFormValid) {
            return;
        }

        var $form = $("#" + formId);
        var url = $form.attr('action') + action;
        var formData = $form.serialize();
        var popup = self.AttachmentFormPopupInstance;
        

        $.ajax({
            type: "post",
            url: url,
            data: formData,
            success: function (response) {
                if (response.ErrorMessage !== undefined) {
                    alert(response.ErrorMessage);
                    return;
                }

                //hide popup on success
                popup.Hide();
                self.AttachmentGrid_Refresh();
            },
            error: PdnLib.AjaxError
        });
    };
    
    this.AttachmentForm_OnFileUploadComplete = function (s, e, optionalParams) {
        if (e.callbackData == '') return;

        var splitted = e.callbackData.split('|');
        //        $('#previewImage').attr('src', splitted[2] + "?refresh=" + (new Date()).getTime()); 
        //        $('#NewUploadedPictureUrl').val(splitted[4]);

        var contentType = splitted[8];

        $("#AttachmentContentType").val(contentType);
        var originalFileNameNoExt = splitted[7];
        var savedTempFile = splitted[4];

        DocUrl.SetText(savedTempFile);

        //dipakai untuk upload change request
        if (optionalParams === undefined) {
            Label.SetText(originalFileNameNoExt);
        }

        if (e.isValid) {
            toggleUploadImage();
        }
        //toggleUploadImage();
    };

    this.DisplayLabel = function(isChangeQuantityRequestChecked) {
        if (!isChangeQuantityRequestChecked) {
            $("#lblInfoRequest").hide();
            $("#lblInfoUploaded").hide();
            $("#lblInfoAllocated").hide();

            btnUploadChangeQuantityRequest.SetVisible(false);
            return;
        }

        var isQuantityAllocated = $('#pdnMainFormId input[name="IsPartialQuantityAllocated"]').val();
        var isUploaded = $('#pdnMainFormId input[name="IsChangeQuantityRequestUploaded"]').val();

        if (isQuantityAllocated == "True") {
            $("#lblInfoRequest").hide();
            $("#lblInfoUploaded").hide();
            $("#lblInfoAllocated").show();
            btnUploadChangeQuantityRequest.SetText("Re-upload Letter");
        } else if (isUploaded == "True") {
            $("#lblInfoRequest").hide();
            $("#lblInfoUploaded").show();
            $("#lblInfoAllocated").hide();
            btnUploadChangeQuantityRequest.SetText("Re-upload Letter");
        } else {
            $("#lblInfoRequest").show();
            $("#lblInfoUploaded").hide();
            $("#lblInfoAllocated").hide();
            btnUploadChangeQuantityRequest.SetText("Upload Letter");
        }

        btnUploadChangeQuantityRequest.SetVisible(true);

        //var temp = 
    }

    this.PdnForm_GetPartialQuantity_FormInfo = function () {
        var isEditMode = true;
        var isPartialQuantityChecked = IsPartialQuantityDelivery.GetChecked();

        var isQuantityAllocated = $('#pdnMainFormId input[name="IsPartialQuantityAllocated"]').val();
        var isUploaded = $('#pdnMainFormId input[name="IsChangeQuantityRequestUploaded"]').val();

        var isChangeQuantityRequestUploaded = isUploaded == "True"? true: false;
        var isPartialQuantityAllocated = isQuantityAllocated == "True" ? true : false;

        var poLineCount = $('#Ui_PoLineCount').val();

        var params = {
            isEditMode: isEditMode,
            isPartialQuantityChecked: isPartialQuantityChecked,
            isChangeQuantityRequestUploaded: isChangeQuantityRequestUploaded,
            isPartialQuantityAllocated: isPartialQuantityAllocated,
            poLineCount: poLineCount,

            totalQuantity: 0,
            totalPoQuantity: 0,
            totalAllocated: 0
        };

        return params
    };

    this.PdnForm_PrepareUiAfterLoading = function() {
        var pdnStatus = $('#pdnMainFormId input[name="PdnStatus"]').val();

        //vendor + Warehouse notes:
        var areaPlaceHoler = 'VendorNoteDisplayPlaceholderId';
        var objDiv = document.getElementById(areaPlaceHoler);
        objDiv.scrollTop = objDiv.scrollHeight;

        areaPlaceHoler = 'WarehouseNoteDisplayPlaceholderId';
        objDiv = document.getElementById(areaPlaceHoler);
        objDiv.scrollTop = objDiv.scrollHeight;


        //pdn scheduling workflow
        if (pdnStatus == "PDN_Approved" ||
            pdnStatus == "Delivery_Scheduled" ||
            pdnStatus == "Delivery_Rescheduled") {
            //show delivery schedule tab
            
            $("#tabHeaderContainerId > .active").removeClass('active');
            $("#tabHeaderContainerId > :nth-child(3)").addClass('active');

            $("#tabContentContainerId > .active").removeClass('active');
            $("#tabContentContainerId > :nth-child(3)").addClass('active');
            return;
        }
    };

    this.PdnForm_LineItem_Scan_ForEdit = function () {
        var params = self.PdnForm_GetPartialQuantity_FormInfo();

        for (var i = 0; i < params.poLineCount; i++) {
            this.PdnFormLineItem_SetEnabled(i, params);
        }
    };

    //executed per line item
    this.PdnFormLineItem_SetEnabled = function (i, params) {
        var canModifyUI = false;
        var temp = $('input[name="CanModifyUI"]').val();
        if (temp.toUpperCase() == "TRUE") {
            canModifyUI = true;
        } else {
            canModifyUI = false;
        }
 
        var currentRowString = "MasterPoLineItems[{enum}]".replace(/{enum}/g, i);

        var checkBox = ASPxClientControl.GetControlCollection().GetByName(currentRowString + ".Ui_IsChecked");

        var allocatedQuantity = ASPxClientControl.GetControlCollection().GetByName(currentRowString + ".AllocatedQuantity");

        //var remainingQuantity = ASPxClientControl.GetControlCollection().GetByName(currentRowString + ".RemainingQuantity");

        var poMaterialQuantity = ASPxClientControl.GetControlCollection().GetByName(currentRowString + ".PoMaterialQuantity");

        var materialQuantity = ASPxClientControl.GetControlCollection().GetByName(currentRowString + ".MaterialQuantity");

        var materialQuantity = ASPxClientControl.GetControlCollection().GetByName(currentRowString + ".MaterialQuantity");

        var weight = ASPxClientControl.GetControlCollection().GetByName(currentRowString + ".Weight");
        var materialDimension = ASPxClientControl.GetControlCollection().GetByName(currentRowString + ".MaterialDimension");
        var palletCartonDimension = ASPxClientControl.GetControlCollection().GetByName(currentRowString + ".PalletCartonDimension");

        var isChecked = checkBox.GetChecked();

        var val_PoQuantity = poMaterialQuantity.GetValue(); //hati 2 bisa undefined or null
        var val_MatQuantity = materialQuantity.GetValue();
        var val_Allocated = allocatedQuantity.GetValue();

        var val_remainingQuota = val_PoQuantity - val_Allocated;

        var isLocked = val_remainingQuota <= 0;
        var isQuantityEditable = !isLocked
            && params.isChangeQuantityRequestUploaded
           // && params.isPartialQuantityChecked;

        if (isChecked) {
            //if partial quantity// enable textbox if still has quota or PdnState is still allowed
            if (isQuantityEditable) {
                //materialQuantity.SetEnabled(true);
                materialQuantity.GetInputElement().readOnly = false || !canModifyUI;
            } else {
                //materialQuantity.SetEnabled(false); //kagak bisa setEnabled = false;
                materialQuantity.GetInputElement().readOnly = true;

            }

            materialQuantity.SetMaxValue(val_PoQuantity);

            weight.GetInputElement().readOnly = false || !canModifyUI;
            materialDimension.GetInputElement().readOnly = false || !canModifyUI;
            palletCartonDimension.GetInputElement().readOnly = false || !canModifyUI;
            

        } else {
            //materialQuantity.SetEnabled(false);
            materialQuantity.GetInputElement().readOnly = true;
            weight.GetInputElement().readOnly = true;
            materialDimension.GetInputElement().readOnly = true;
            palletCartonDimension.GetInputElement().readOnly = true;

            //materialQuantity.GetInputElement().setAttribute('style','background:#CCCCCC;');
            //weight.GetInputElement().setAttribute('style','background:#CCCCCC;');
            //materialDimension.GetInputElement().setAttribute('style','background:#CCCCCC;');
            //palletCartonDimension.GetInputElement().setAttribute('style', 'background:#CCCCCC;');
        }
        
        //params.totalQuantity += materialQuantity.GetValue();
        //params.totalPoQuantity += poMaterialQuantity.GetValue();
        //params.totalAllocated += allocatedQuantity.GetValue();

    };

    PdnLib.PdnLineItemUiModel = function () {
        var self = this;
        self.isChecked = false;

        self.val_PoQuantity = 0;
        self.val_MatQuantity = 0;
        self.val_Allocated = 0;

        self.val_remainingQuota = 0;

        self.isLocked = false;
        self.isQuantityEditable = false;
    };

    PdnLib.PdnLineItem_GetLineModel = function (i) {
        var result = new PdnLib.PdnLineItemUiModel();

        var params = self.PdnForm_GetPartialQuantity_FormInfo();


        var currentRowString = "MasterPoLineItems[{enum}]".replace(/{enum}/g, i);
        var checkBox = ASPxClientControl.GetControlCollection().GetByName(currentRowString + ".Ui_IsChecked");
        var allocatedQuantity = ASPxClientControl.GetControlCollection().GetByName(currentRowString + ".AllocatedQuantity");

        var poMaterialQuantity = ASPxClientControl.GetControlCollection().GetByName(currentRowString + ".PoMaterialQuantity");

        var materialQuantity = ASPxClientControl.GetControlCollection().GetByName(currentRowString + ".MaterialQuantity");

        var weight = ASPxClientControl.GetControlCollection().GetByName(currentRowString + ".Weight");
        var materialDimension = ASPxClientControl.GetControlCollection().GetByName(currentRowString + ".MaterialDimension");
        var palletCartonDimension = ASPxClientControl.GetControlCollection().GetByName(currentRowString + ".PalletCartonDimension");

        result.isChecked = checkBox.GetChecked();

        result.val_PoQuantity = PdnLib.TryGetNumber(poMaterialQuantity);
        result.val_MatQuantity = PdnLib.TryGetNumber(materialQuantity);
        result.val_Allocated = PdnLib.TryGetNumber(allocatedQuantity);

        result.val_remainingQuota = result.val_PoQuantity - result.val_Allocated;

        result.isLocked = result.val_remainingQuota <= 0;
        result.isQuantityEditable = !result.isLocked
            && params.isChangeQuantityRequestUploaded
            && params.isPartialQuantityChecked;

        return result;

    };

    

    PdnLib.TryGetNumber = function (spinEditor) {
        var val = spinEditor.GetValue();

        if (val == null) {
            spinEditor.SetValue(0);
            return 0;
        }

        return val;
    }


}//end PdnLib.MainPdnFormLib

