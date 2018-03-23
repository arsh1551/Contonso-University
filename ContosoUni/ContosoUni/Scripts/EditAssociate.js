
$(document).ready(function () {
    debugger;

    $('#SelectedCourses').multiselect(
        {

            includeSelectAllOption: true,
            nonSelectedText: 'Select Courses!',
            buttonWidth: '30%',
            maxHeight: 150,
            dropUp: true,
            dropRight: true
            
        })
    
    var values = $('#CourseSummary').val();
    if (values != "Not specified") {
        var dataArray = values.split(",");
        $("#SelectedCourses").val(dataArray);
        $("#SelectedCourses").multiselect("refresh");
    };

});
