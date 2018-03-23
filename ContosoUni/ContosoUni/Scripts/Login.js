function validateUser()
{
    debugger;
    var code = $('#UserCode').val();
    var pass = $('#Password').val();
    $.ajax({
        type: "POST",
        url: '/Account/Login',
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify({ UserCode: code, Password: pass }),
        success: function (response) {
            debugger;
            if (response.message == true) {
                
                $('#spanErrorMessage').hide();
                window.location ='/User/Index'
            
            }
          
            else {
                debugger;
               
                $('#spanErrorMessage').show();
                $('#spanErrorMessage').text("InValid User!");
               // $("#buttonLoginUser").attr("disabled", "disabled")
            
            }
        }
    });

}

function loginSuccess()
{
    alert("method called!");
    
    if (response.message == true) {
        debugger;

        $('#spanErrorMessage').hide();
        window.location = '/User/Index'

    }

    else {
        debugger;

        $('#spanErrorMessage').show();
        $('#spanErrorMessage').text("InValid User!");
        // $("#buttonLoginUser").attr("disabled", "disabled")

    }

}