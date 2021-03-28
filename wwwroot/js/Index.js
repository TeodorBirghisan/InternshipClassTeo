// This JS file now uses jQuery. Pls see here: https://jquery.com/
$(document).ready(function () {
    // see https://api.jquery.com/click/
    $("#add").click(function () {
        var newcomerName = $("#newcomer").val();

        // Remember string interpolation
        $.ajax({
            url: `/Home/AddMember?member=${newcomerName}`,
            success: function (data) {
                // Remember string interpolation
                $("#list").append(`<li class="member"><span class="name">${data}</span><span class="remove fa fa-remove"></span><i class="startEdit fa fa-pencil" data-toggle="modal" data-target="#editClassmate"></i>
		        </li>`);

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

    $("#list").on("click", ".remove", function () {

        var $li = $(this).closest('li');
        var index = $li.index();

        console.log(`index=${index}`);
        console.log(`$li=${$li}`);

        $.ajax({
            method: "DELETE",
            url: `/Home/RemoveMember?index=${index}`,
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
        var index = targetMemberTag.index();
        var currentName = targetMemberTag.find(".name").text();
        $('#editClassmate').attr("memberIndex", index);
        $('#classmateName').val(currentName);
    })

    $("#editClassmate").on("click", "#submit", function () {
        var newName = $('#classmateName').val();
        var index = $('#editClassmate').attr("memberIndex");
        console.log('submit changes to server');
        $.ajax({
            url: `/Home/UpdateMember?index=${index}&member=${newName}`,
            type: 'PUT',
            success: function (response) {
                $('.name').get(index).replaceWith(newName);
            },
            error: function (data) {
                alert(`Failed to remove`);
            },
        });
    })

    $("#editClassmate").on("click", "#cancel", function () {
        console.log('cancel changes');
    })

});