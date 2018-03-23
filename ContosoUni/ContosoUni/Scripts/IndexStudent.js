
$(document).ready(function () {

});

function DeleteRecord(theLink) {
    debugger;
    

    var studentId = theLink.dataset.info;
    if (studentId) {
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
                debugger;
                if (result == true) {
                    debugger;
                    $.ajax({
                        type: "POST",
                        url: '/Student/DeleteConfirmed',
                        contentType: "application/json",
                        dataType: "json",
                        data: JSON.stringify({ id: studentId }),
                        success: function (response) {
                            if (response) {
                                debugger;
                                //  $('.modal.in').modal('hide') 
                                //if ($('#divUserList') != null) {
                                //    $('#divUserList').html(response);
                               // window.location = "/student/Index";
                                window.location.href = "http://localhost:52717/student/Index";
                                }
                                else {
                                    //check Url and then redirect, if at index page implement this
                                window.location = "/student/Index";
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
