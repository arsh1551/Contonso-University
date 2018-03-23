
var userCode;
var userId;
function addAndList()
{
    userId = $('#Id').val();    
    userCode = $('#user_UserCode').val();
    if (userId == undefined)
    {    
         validateUserCode();
    }
    else
    {
        addEdit();

    }
   
}
function addEdit() {
    var name = $('#user_Name').val();
    var profession = $('#user_Profession').val();
    var title = $('#user_Title').val();
    var email = $('#user_Email').val();
    var roleId = $('#UserRoles').val();
    var password = $('#user_Password').val();
    $.ajax({
        type: "POST",
        url: '/User/Create',
        contentType: "application/json",
        dataType: "html",
        data: JSON.stringify({ Name: name, Profession: profession, Id: userId, Title: title, UserCode: userCode, Email: email, Password: password, UserRoleId: roleId }),
        success: function (response) {
            if (response) {
                var newresponse = JSON.parse(response);
                $('#divUserList').html(newresponse.Data);
                if (newresponse.HasError) {
                    $('#spanErrorMessage').show();

                    //  $('#erorMessageSummary').html(newresponse.Error);
                    $('#spanErrorMessage').html(newresponse.Error);
                }
                else {
                    $("input:text").val(' ');
                    $('#user_Email').val(' ');
                    $('#UserRoles').find('option:first').attr('selected', 'selected');
                    $('#spanErrorMessage').hide();

                }


            }
            else {

                $('#dvAlertDanger').css("display", "none");
                $('#lblAlertDanger').text(" ");
                $('#btnUpdate').prop("disabled", false);
            }
        },
        error: function (response) {
            console.log(response);
        }


    });
}

function validateUserCode() {
            code = userCode;
            $.ajax({
                type: "POST",
                url: '/User/IsUserCodeExists',
                contentType: "application/json",
                dataType: "json",
                data: JSON.stringify({ userCode: code }),
                success: function (response) {
                    if (response.message == true) {
                      
                        //$('#dvAlertDanger').css("display", "block");
                        //$('#lblAlertDanger').text("Firm Already Exists");
                        //$('#dvAlertSuccess').hide();
                        //$('#btnUpdate').prop("disabled", true);
                        $('#spanErrorMessage').show();
                        $('#spanErrorMessage').text("Usercode already exists");
                        // $('#buttonAddUser').prop("disabled", true);           

                    }
                    else {
                       

                        $('#spanErrorMessage').hide();
                        $('#buttonAddUser').prop("disabled", false);
                        addEdit();

                    }
                },
                error: function (response) {
                    console.log(response);
                }
            });
                }
        

function DeleteRecord(theLink) {
    debugger;
   
    var userId = $("#deleteLink").attr("data-info");
    //("#hdnDelUserId").val(userId);
    $("input[id=hdnDelUserId]").val(userId);
    $('#myModal').modal('show');
}

function DeleteUser() {
   // $('#myModal').modal('hide');
    debugger;
    

    
    var userId = $("input[id=hdnDelUserId]").val();
    $.ajax({
        type: "POST",
        url: '/User/DeleteConfirmed',
        contentType: "application/json",
        dataType: "html",
        data: JSON.stringify({ id: userId}),
        success: function (response) {
            if (response) {
                debugger;


              //  $('.modal.in').modal('hide') 
                if ($('#divUserList') != null) {
                    $('#divUserList').html(response);
                }
                else {
                    //check Url and then redirect, if at index page implement this
                    window.location = "/User/Index";
                }
                            }
            else {

                $('#dvAlertDanger').css("display", "none");
                $('#lblAlertDanger').text(" ");
                $('#btnUpdate').prop("disabled", false);
            }
        },
        error: function (response) {
            console.log(response);
        }
    });

}
