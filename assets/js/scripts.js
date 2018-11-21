 $(document).ready(function() {
        // Initializes search overlay plugin.
        // Replace onSearchSubmit() and onKeyEnter() with 
        // your logic to perform a search and display results
        $('[data-pages="search"]').search({
            searchField: '#overlay-search',
            closeButton: '.overlay-close',
            suggestions: '#overlay-suggestions',
            brand: '.brand',
            onSearchSubmit: function(searchString) {
                console.log("Search for: " + searchString);
            },
            onKeyEnter: function(searchString) {
                console.log("Live search for: " + searchString);
                var searchField = $('#overlay-search');
                var searchResults = $('.search-results');
                clearTimeout($.data(this, 'timer'));
                searchResults.fadeOut("fast");
                var wait = setTimeout(function() {
                    searchResults.find('.result-name').each(function() {
                        if (searchField.val().length != 0) {
                            $(this).html(searchField.val());
                            searchResults.fadeIn("fast");
                        }
                    });
                }, 500);
                $(this).data('timer', wait);
            }
        });
})

 //Input mask - Input helper
 $(function ($) {
	 $("#date").mask("99/99/9999");
	 $(".phone").mask("(999) 999-9999");
	 $(".postCode").mask("(9999)");
	 $("#tin").mask("99-9999999");
	 $("#ssn").mask("999-99-9999");
 });
 //Autonumeric plug-in - automatic addition of dollar signs,etc controlled by tag attributes
 $('.autonumeric').autoNumeric('init');