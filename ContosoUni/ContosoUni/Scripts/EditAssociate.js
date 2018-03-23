
$(document).ready(function () {

    $('#specializationIds').multiselect(
        {
            includeSelectAllOption: true,
            nonSelectedText: 'Select specialization!',
            buttonWidth: '30%',
            maxHeight: 150,
            dropUp: true,
            dropRight: true
            
        })

    var values = $('#SpecializationSummary').val();
    if (values != "Not specified") {
        var dataArray = values.split(",");
        $("#specializationIds").val(dataArray);
        $("#specializationIds").multiselect("refresh");
    };

});
