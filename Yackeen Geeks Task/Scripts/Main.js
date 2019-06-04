$(document).ready(function () {
    $(".datepicker").datepicker();

    $("#articlesTable").DataTable();
    $("#categoriesTable").DataTable();

    $("#submitCommentButton").bind("ajaxStart", function () {
        $("#comment-spinner-loader").removeClass("d-none");
        $("#submitCommentButton").addClass("d-none");
    }).bind("ajaxStop", function () {
        $("#comment-spinner-loader").addClass("d-none");
        $("#submitCommentButton").removeClass("d-none");
    })

});

function postCategory(form) {
    $.validator.unobtrusive.parse(form);

    if ($(form).valid()) {

        var formData = new FormData(form);

        $.ajax({
            url: form.action,
            method: form.method,
            data: formData,
            processData: false,
            contentType: false,
            success: function (response) {
                $("#categoriesTable").html(response);
                window.location.href = "/Categories/";
            },
            error: function () {
                alert("error");
            }
        })

    }

    return false;
}

function postArticle(form) {

    $.validator.unobtrusive.parse(form);

    if ($(".imageFile").val() == "") {
        $(".imageFileError").empty().append("The Image File is required");
        return false;
    }

    if ($(form).valid()) {

        var formData = new FormData(form);

        $.ajax({
            url: form.action,
            method: form.method,
            data: formData,
            processData: false,
            contentType: false,
            success: function (response) {
                $("#articlesTable").html(response);
                window.location.href = "/Articles/";
            },
            error: function () {
                alert("error");
            }
        });

    }

    return false;
}

function deleteCategory(category) {

    var categoryId = $(category).attr("data-category-id");

    var row = $(category).closest("tr");

    $.confirm({
        title: 'Delete Category',
        buttons: {
            confirm: function () {
                $.ajax({
                    url: "/Categories/Delete/" + categoryId,
                    method: "POST",
                    contentType: false,
                    processData: false,
                    success: function () {
                        row.remove();
                        $.alert('Category Deleted Successfully!');
                    },
                    error: function () {
                        $.notify("some thing went wrong!", "error");
                    }
                })
            },
            cancel: function () {
                $.alert('Category Deletion Canceled!');
            }
        }
    });
}

function deleteArticle(article) {

    var articleId = $(article).attr("data-article-id");

    var row = $(article).closest("tr");

    console.log(articleId);
    console.log(row);

    $.confirm({
        title: 'Delete Article',
        buttons: {
            confirm: function () {
                $.ajax({
                    url: "/Articles/Delete/" + articleId,
                    method: "POST",
                    contentType: false,
                    processData: false,
                    success: function () {
                        row.remove();
                        $.alert('Article deleted successfully!');
                    },
                    error: function () {
                        $.notify("Some thing went wrong!", "error");
                    }
                })
            },
            cancel: function () {
                $.alert('Article Deletion Canceled!');
            }
        }
    });
}

function showImagePreview(imageUploader) {  
    console.log("change");
    if (imageUploader.files && imageUploader.files[0]) {
        $(".imageFileError").empty();

        var fileReader = new FileReader();

        fileReader.onload = function (e) {
            $(".imagePreview").attr('src', e.target.result);
        }

        fileReader.readAsDataURL(imageUploader.files[0]);
    }
}

function filterArticlesByCategory(filter) {
    $.ajax({
        url: "/Articles/FilterByCategory/" + filter.value.toString(),
        method: "GET",
        processData: false,
        contentType: false,
        success: function (response) {
            $("#articlesContainer").html(response);
            console.log("success");
        },
        error: function () {
            alert("error");
        }
    });
}

function postComment(form) {
    $.validator.unobtrusive.parse(form);

    if ($(form).valid()) {

        var formData = new FormData(form);

        $.ajax({
            url: form.action,
            method: form.method,
            data: formData,
            cache: false,
            processData: false,
            contentType: false,
            success: function (response) {
                $("#oldComments").html(response);
                resetForm();
            },
            error: function () {
                alert("error");
            }
        });
    }

    return false;
}

function resetForm() {
    $("#commentVisitorName").val("");
    $("#commentContent").val("");
}

function deleteComment(comment) {
    var commentId = $(comment).attr("data-comment-id");

    var commentDiv = $(comment).closest("div.comment");

    console.log(commentId);
    console.log(commentDiv);

    $.confirm({
        title: 'Delete Comment',
        buttons: {
            confirm: function () {
                $.ajax({
                    url: "/Comments/Delete/" + commentId,
                    method: "POST",
                    contentType: false,
                    processData: false,
                    success: function () {
                        commentDiv.remove();
                        $.alert('Comment deleted successfully!');
                    },
                    error: function () {
                        $.notify("Some thing went wrong!", "error");
                    }
                })
            },
            cancel: function () {
                $.alert('Comment Deletion Canceled!');
            }
        }
    });
}