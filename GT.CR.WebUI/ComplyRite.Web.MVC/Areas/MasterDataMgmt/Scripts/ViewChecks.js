$(function () {
    // SEARCH CHECK ON LOAD:
    if ($('#search_check').val().length != 0) {
        var searchTerm = $('#search_check').val().toLowerCase();
        filterResults(searchTerm);
    }

    $('#search_check').removeAttr('disabled');

    // SEARCH EVENT:
    $('#search_check').on('keyup change', $.debounce(250, function () {
        var searchTerm = $(this).val().toLowerCase();
        filterResults(searchTerm);
    }));

    function filterResults(searchTerm) {
        if (searchTerm == "") {
            $('#result_body').find('tr').each(function (index, row) {
                $(row).find('td.search-this .highlight-search-term').removeClass('highlight-search-term');
                $(row).css('display', '');
            });
        } else {
            $('#result_body').find('tr').each(function (index, row) {
                $(row).css('display', '');
                var notFound = true;

                $(row).find('td.search-this').each(function (jindex, cell) {
                    var rowTitle = $(cell).text().trim();
                    var searchTitle = rowTitle.toLowerCase();
                    var searchTermIndex = searchTitle.indexOf(searchTerm, 0);

                    if (searchTermIndex > -1) {
                        var beforeHightlight = rowTitle.substring(0, searchTermIndex);
                        var highlight = '<span class="highlight-search-term">' + rowTitle.substring(searchTermIndex, searchTermIndex + searchTerm.length) + '</span>';
                        var afterHighlight = rowTitle.substring(searchTermIndex + searchTerm.length, rowTitle.length);
                        var newTitleHtml = beforeHightlight + highlight + afterHighlight;

                        $(cell).html(newTitleHtml);
                        notFound = false;
                    } else {
                        $(cell).find('.highlight-search-term').removeClass('highlight-search-term');
                    }
                });
                if (notFound) {
                    $(row).css('display', 'none');
                }
            });
        }
    }
});