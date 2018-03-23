
$(document).ready(function () {

});

function DeleteRecord(theLink) {
    

    var associateId = theLink.dataset.info;
    if (associateId) {
        bootbox.confirm({
            message: "Are you sure you want to delete this record?",
            buttons: {
                confirm: {
                    label: 'Yes',
                    className: 'btn-success'
                },
                cancel: {
                    label: 'No',
                    className: 'btn-danger'
                }
            },
            callback: function (result) {
                if (result == true) {
                    debugger;
                    $.ajax({
                        type: "POST",
                        url: '/Associate/DeleteConfirmed',
                        contentType: "application/json",
                        dataType: "json",
                        data: JSON.stringify({ id: associateId }),
                        success: function (response) {
                            if (response) {
                                debugger;
                                //  $('.modal.in').modal('hide') 
                                //if ($('#divUserList') != null) {
                                //    $('#divUserList').html(response);
                               // window.location = "/Associate/Index";
                                window.location.href = "http://localhost:59251/Associate/Index";
                                }
                                else {
                                    //check Url and then redirect, if at index page implement this
                                window.location = "/Associate/Index";
                                }
                            }
                          
                        ,
                        error: function (response) {
                            console.log(response);
                        }
                    });
                   
                }
                else {
                    debugger;
                    
                }
            }
        });
    }
}
