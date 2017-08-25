(function ($) {
    var $body = $("body"),
        $document = $(document),
		$wSidebar = $body.find(".wrapper-sidebar"),
        $navProfileToggle = $body.find(".nav-profile-toggle"),
        $navProfile = $body.find(".nav-profile"),
        $infoBlock = $body.find(".info-block"),
        $infoBlockBtn = $body.find(".info-block-btn"),
        $infoBlockTabs = $infoBlock.find(".tabs-vertical"),

        dataValDates = $("input[data-val-date]");

    // document click
    $body.on("click", function (e) {
       // e.stopPropagation();

        // if nav profile has class open then remove
        $navProfile.find("li.is-open").removeClass("is-open");

    });
		
	/* Scrollbar */
	$('#sidebar-scroll').slimscroll({
		height: 'auto',
		color: '#999'
	});
	
	/* Sidebar close */
	$('[data-toggle="sidebar-close"]').on("click",function(e) {
		e.stopPropagation();
		$wSidebar.toggleClass("is-open");
	});
	
	/* Sidebar open */
	$('[data-toggle="sidebar-open"]').on("click",function(e) {
		e.stopPropagation();
		$wSidebar.toggleClass("is-open");
	});
	
	/* Submenu */
	$('[data-toggle="nav-submenu"]').on("click", function(e){
		 e.stopPropagation();
		 var submenu = $(this),
	 		 submenuParent = submenu.parent("li");
	     submenuParent.hasClass("is-open") ? submenuParent.removeClass("is-open") :
		 (submenu.closest("ul").find("> li").removeClass("is-open"),
			 submenuParent.addClass("is-open"));
	});
	
	/* Navbar dropdown menu */
	$navProfileToggle.on("click", function (e) {
		e.stopPropagation();
		var toggle = $(this),
 		 parent = toggle.parent("li");
		 parent.toggleClass("is-open");
	});

    // info block slideup and down
	$infoBlockBtn.on("click", function (e) {
	    e.stopPropagation();
	    $infoBlockTabs.slideToggle();
	});

    // delete table row
	function deleteRow() {
	    $(this).closest('tr').remove();
	}

    // تبدیل پیغام های انگلیسی به فارسی
	dataValDates.each(function () {
	    $(this).attr('data-val-date', 'لطفا یک تاریخ صحیح وارد کنید');
	});
	
})(jQuery);