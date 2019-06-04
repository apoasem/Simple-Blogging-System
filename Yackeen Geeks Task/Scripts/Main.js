$(document).ready(function () {
    $(".datepicker").datepicker();

    $("#articlesTable").DataTable();
    $("#categoriesTable").DataTable();

    $(document).bind("ajaxStart", function () {
        $("#spinner-loader").removeClass("d-none");
    }).bind("ajaxStop", function () {
        $("#spinner-loader").addClass("d-none")
    });

});


function renameTabTitle(tabId, title) {
    $("#" + tabId).html(title);
}


function redirectToEditCategoryTab(url) {
    $.ajax({
        url: url,
        method: "GET",
        success: function (response) {
            $("#categoryTab").html(response);
            $('.datepicker').datepicker(); //Initialise date pickers
            $("#categoryConfigTab").tab("show");
        },
        error: function () {
            $.notify("Some thing went wrong!", "error");
        }
    })
}

function redirectToEditArticleTab(url) {
    $.ajax({
        url: url,
        method: "GET",
        success: function (response) {
            $("#articleTab").html(response);
            $('.datepicker').datepicker(); //Initialise date pickers
            $("#articleConfigTab").tab("show");
        },
        error: function () {
            $.notify("Some thing went wrong!", "error");
        }
    })
}

function refreshCategoryForm() {
    $.ajax({
        url: "/Categories/Create",
        method: "GET",
        success: function (response) {
            $("#categoryTab").html(response);
            $('.datepicker').datepicker(); //Initialise date pickers
        },
        error: function () {
            $.notify("Some thing went wrong!", "error");
        }
    });
}

function refreshArticleForm() {
    $.ajax({
        url: "/Articles/Create",
        method: "GET",
        success: function (response) {
            $("#articleTab").html(response);
            $('.datepicker').datepicker(); //Initialise date pickers
        },
        error: function () {
            $.notify("Some thing went wrong!", "error");
        }
    });
}


function postCategory(form) {

    $.validator.unobtrusive.parse(form);

    if ($(form).valid()) {

        var formData = new FormData(form);

        $.ajax({
            url: form.action,
            method: form.method,
            data: formData,
            contentType: false,
            processData: false,
            success: function (response) {

                if (response.toString().search("table") == -1) {
                    $("#categoryTab").html(response);
                    return false;
                }

                $("#categoriesTableContainer").html(response);
                $("#allCategoriesTab").tab("show");
                renameTabTitle('categoryConfigTab', 'New Category')
                refreshCategoryForm(); // refresh the form
                $("#categoriesTable").DataTable();
                $.notify("Category added successfully", "success");
            },
            error: function () {
                $.notify("Some thing went wrong!", "error");
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
                alert(response);
                console.log(response);
                //$("#articlesTableContainer").html(response);
                //$("#allArticlesTab").tab("show");
                //renameTabTitle('articleConfigTab', 'New Article')
                //refreshArticleForm(); // refresh the form
                //$("#articlesTable").DataTable();
                //$.notify("Article added successfully", "success");
            },
            error: function () {
                $.notify("Some thing went wrong!", "error");
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
                        $.notify("Some thing went wrong!", "error");
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
        },
        error: function () {
            $.notify("Some thing went wrong!", "error");
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
                $.notify("Some thing went wrong!", "error");
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