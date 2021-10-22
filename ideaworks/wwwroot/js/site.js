// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


let valY = $('#IsCanadianBasedY').is(':checked');
let valN = $('#IsCanadianBasedN').is(':checked');

if (valY) {
    conditional('Y', false);
}
if (valN) {
    conditional('N', false);
}

$('.isCanadianBased').change(function () {
    conditional(this.value, true);
});


let thirdStageApplicableY = $('#IsApplicableY').is(':checked');
let thirdStageApplicableN = $('#IsApplicableN').is(':checked');


if (thirdStageApplicableY) {
    conditionalThirdStage('Y');
}
if (thirdStageApplicableN) {
    conditionalThirdStage('N');
}

$('.isApplicable').change(function () {
    conditionalThirdStage(this.value);
});


function conditionalThirdStage(value) {
    if (value == "Y") {
        $("#thirdStageInputs :input").prop("disabled", false);
    }
    if (value == "N") {
        $("#thirdStageInputs :input").prop("disabled", true);
    }
}





function conditional(value, resetValue) {
    if (value == "Y") {
        $("#isOntarioBased").prop('disabled', false);
        $("#location").prop('disabled', true);
        $("#location").val('Not Applicable');
        $("#location").prop('required', false);

        if (resetValue) {
            $("#IsOntarioBasedN").attr('checked', false);
            $("#IsOntarioBasedY").attr('checked', false);
        }
        

        $("#isOntarioBased").prop("required", true);
        $("#IsOntarioBasedY").prop('required', true);
    }
    if (value == "N") {
        $("#location").prop('disabled', false);
        $("#isOntarioBased").prop("disabled", true);

        $("#location").prop('required', true);
        $("#IsOntarioBasedN").prop('required', false);
        $("#IsOntarioBasedN").attr('checked', true);

        if (resetValue) {
            $("#location").val('');
        }
        
    }
}

$(function () {
    $('[data-toggle="tooltip"]').tooltip()
})


$("#feedbackForm").submit(function (event) {
    $("#emailAlert").show("slow").delay(3000).fadeOut();
});