$(function () {
    var term = $("#search-term").val();
    var tagId = $("#search-tagId").val();
    var categoryId = $("#search-categoryId").val();
    var page = 1;
    $("#load-more-post").on('click', function (e) {
        e.preventDefault();
        getSearchResult();

    });
    function getSearchResult() {
      $('#post-end-message').html('<div class="text-danger"></div>');
      $("#load-more-post").html('دریافت ...');
        $.ajax({
            type: "POST",
            url: '/blog/searcharticles',
            data: JSON.stringify({ term: term, tagId: tagId, categoryId: categoryId, page: page }),
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (data) {
                $("#load-more-post").html('بیشتر');
                if (data == "no-more-info") {
                    $("#load-more-post").remove();
                    $('#post-end-message').html('<div class="end main-color">پایان</div>');
                }
                else {
                    $('#show-more-post').append(data);
                    $("div.blog-post:hidden").fadeIn(400);
                    page++;
                }
            },
            error: function () {
                $("#load-more-post").html('بیشتر');
                $('#post-end-message').html('<div class="text-danger">مشکلی پیش اومده! دوباره تست کن</div>');
            }
        });
    }

});