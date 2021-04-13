// This JS file now uses jQuery. Pls see here: https://jquery.com/
$(document).ready(function () {
    // see https://api.jquery.com/click/
    $("#add").click(function () {
        var newcomerName = $("#newcomer").val();

        // Remember string interpolation
        $.ajax({
            url: `/Home/AddMember?memberName=${newcomerName}`,
            success: function (data) {
                $("#newcomer").val("");
            },
            error: function (data) {
                alert(`Failed to add ${newcomerName}`);
            },
        });

    })

    $("#clear").click(function () {
        $("#newcomer").val("");
    })

    $("#list").on("click", ".delete", function () {

        var $li = $(this).closest('li');
        var id = $li.attr('member-id');

        $.ajax({
            method: "DELETE",
            url: `/Home/RemoveMember?index=${id}`,
            success: function (data) {

                $li.remove();

            },
            error: function (data) {
                alert(`Failed to remove`);
            },
        });
    })

    $("#list").on("click", ".startEdit", function () {
        var targetMemberTag = $(this).closest('li');
        var id = targetMemberTag.attr('member-id');
        var nameIndex = targetMemberTag.index();
        var currentName = targetMemberTag.find(".name").text();
        $('#editClassmate').attr("member-id", id);
        $('#editClassmate').attr("nameIndex", nameIndex);
        $('#classmateName').val(currentName);
    })

    $("#editClassmate").on("click", "#submit", function () {
        var newName = $('#classmateName').val();
        var index = $('#editClassmate').attr("member-id");
        var nameIndex = $('#editClassmate').attr("nameIndex");
        $.ajax({
            url: `/Home/UpdateMember?index=${index}&memberName=${newName}`,
            type: 'PUT',
            success: function (response) {
                $('.name').eq(nameIndex).replaceWith(newName);
            },
            error: function (data) {
                alert(`Failed to update`);
            },
        });
    })

    $("#editClassmate").on("click", "#cancel", function () {
        console.log('cancel changes');
    })

});