//[Preview Menu Javascript]

//Project:	Venusx Admin - Responsive Admin Template
//Primary use:   This file is for demo purposes only.

$(function () {
  'use strict'


  /**
   * Get access to plugins
   */

  $('[data-toggle="control-sidebar"]').controlSidebar()
  $('[data-toggle="push-menu"]').pushMenu()

  var $pushMenu       = $('[data-toggle="push-menu"]').data('lte.pushmenu')
  var $controlSidebar = $('[data-toggle="control-sidebar"]').data('lte.controlsidebar')
  var $layout         = $('body').data('lte.layout')

  /**
   * List of all the available themes
   *
   * @type Array
   */
  var mySkins = [
    'theme-primary',
	'theme-secondary',
	'theme-info',
	'theme-success',
	'theme-danger',
	'theme-warning',
  ]

  /**
   * Get a prestored setting
   *
   * @param String name Name of of the setting
   * @returns String The value of the setting | null
   */
  function get(name) {
    if (typeof (Storage) !== 'undefined') {
      return localStorage.getItem(name)
    } else {
      window.alert('Please use a modern browser to properly view this template!')
    }
  }

  /**
   * Store a new settings in the browser
   *
   * @param String name Name of the setting
   * @param String val Value of the setting
   * @returns void
   */
  function store(name, val) {
    if (typeof (Storage) !== 'undefined') {
      localStorage.setItem(name, val)
    } else {
      window.alert('Please use a modern browser to properly view this template!')
    }
  }

  /**
   * Toggles layout classes
   *
   * @param String cls the layout class to toggle
   * @returns void
   */
  function changeLayout(cls) {
    $('body').toggleClass(cls)
    if ($('body').hasClass('fixed') && cls == 'fixed') {
      $pushMenu.expandOnHover()
      $layout.activate()
    }
    $controlSidebar.fix()
  }

  /**
   * Replaces the old skin with the new skin
   * @param String cls the new skin class
   * @returns Boolean false to prevent link's default action
   */
  function changeSkin(cls) {
    $.each(mySkins, function (i) {
      $('body').removeClass(mySkins[i])
    })

    $('body').addClass(cls)
    store('theme', cls)
    return false
  }

  /**
   * Retrieve default settings and apply them to the template
   *
   * @returns void
   */
  function setup() {
    var tmp = get('theme')
    if (tmp && $.inArray(tmp, mySkins))
      changeSkin(tmp)

    // Add the change skin listener
    $('[data-theme]').on('click', function (e) {
      if ($(this).hasClass('knob'))
        return
      e.preventDefault()
      changeSkin($(this).data('theme'))
    })

    // Add the layout manager
    $('[data-layout]').on('click', function () {
      changeLayout($(this).data('layout'))
    })

    $('[data-controlsidebar]').on('click', function () {
      changeLayout($(this).data('controlsidebar'))
      var slide = !$controlSidebar.options.slide

      $controlSidebar.options.slide = slide
      if (!slide)
        $('.control-sidebar').removeClass('control-sidebar-open')
    })


    $('[data-enable="expandOnHover"]').on('click', function () {
      $(this).attr('disabled', true)
      $pushMenu.expandOnHover()
      if (!$('body').hasClass('sidebar-collapse'))
        $('[data-layout="sidebar-collapse"]').click()
    })

    $('[data-enable="rtl"]').on('click', function () {
      $(this).attr('disabled', true)
      $pushMenu.expandOnHover()
      if (!$('body').hasClass('rtl'))
        $('[data-layout="rtl"]').click()
    })

	  	

    $('[data-mainsidebarskin="toggle"]').on('click', function () {
      var $sidebar = $('body')
      if ($sidebar.hasClass('dark-skin')) {
        $sidebar.removeClass('dark-skin')
        $sidebar.addClass('light-skin')
      } else {
        $sidebar.removeClass('light-skin')
        $sidebar.addClass('dark-skin')
      }
    })

    //  Reset options
    if ($('body').hasClass('fixed')) {
      $('[data-layout="fixed"]').attr('checked', 'checked')
    }
    if ($('body').hasClass('layout-boxed')) {
      $('[data-layout="layout-boxed"]').attr('checked', 'checked')
    }
    if ($('body').hasClass('sidebar-collapse')) {
      $('[data-layout="sidebar-collapse"]').attr('checked', 'checked')
    }
    if ($('body').hasClass('rtl')) {
      $('[data-layout="rtl"]').attr('checked', 'checked')
    }
   // if ($('body').hasClass('dark')) {
//      $('[data-layout="dark"]').attr('checked', 'checked')
//    }

  }

  // Create the new tab
  var $tabPane = $('<div />', {
    'id'   : 'control-sidebar-theme-demo-options-tab',
    'class': 'tab-pane active'
  })

  // Create the tab button
  var $tabButton = $('<li />', { 'class': 'nav-item' })
    .html('<a href=\'#control-sidebar-theme-demo-options-tab\' class=\'active\' data-toggle=\'tab\' title=\'Setting\'>'
      + '<i class="mdi mdi-settings"></i>'
      + '</a>')

  // Add the tab button to the right sidebar tabs
  $('[href="#control-sidebar-home-tab"]')
    .parent()
    .before($tabButton)

  // Create the menu
  var $demoSettings = $('<div />')
  
  var $skinsList = $('<ul />', { 'class': 'list-inline clearfix theme-switch' })

  // Dark sidebar skins
  var $themePrimary =
        $('<li />', { style: 'padding: 5px;line-height: 25px;' })
          .append('<a href="javascript:void(0)" data-theme="theme-primary" style="background: #5A8DEE; display: inline-block;vertical-align: middle;" class="clearfix rounded-circle w-30 h-30" title="Theme primary">'
            + '</a>')
  $skinsList.append($themePrimary)

  var $themeSecondary =
        $('<li />', { style: 'padding: 5px;line-height: 25px;' })
          .append('<a href="javascript:void(0)" data-theme="theme-secondary" style="background: #475F7B; display: inline-block;vertical-align: middle;" class="clearfix rounded-circle w-30 h-30" title="Theme secondary">'
            + '</a>')
  $skinsList.append($themeSecondary)

  var $themeInfo =
        $('<li />', { style: 'padding: 5px;line-height: 25px;' })
          .append('<a href="javascript:void(0)" data-theme="theme-info" style="background: #00CFDD; display: inline-block;vertical-align: middle;" class="clearfix rounded-circle w-30 h-30" title="Theme info">'
            + '</a>')
  $skinsList.append($themeInfo)

  var $themeSuccess =
        $('<li />', { style: 'padding: 5px;line-height: 25px;' })
          .append('<a href="javascript:void(0)" data-theme="theme-success" style="background: #39DA8A; display: inline-block;vertical-align: middle;" class="clearfix rounded-circle w-30 h-30" title="Theme success">'
            + '</a>')
  $skinsList.append($themeSuccess)

  var $themeDanger =
        $('<li />', { style: 'padding: 5px;line-height: 25px;' })
          .append('<a href="javascript:void(0)" data-theme="theme-danger" style="background: #FF5B5C; display: inline-block;vertical-align: middle;" class="clearfix rounded-circle w-30 h-30" title="Theme danger">'
            + '</a>')
  $skinsList.append($themeDanger)

  var $themeWarning =
        $('<li />', { style: 'padding: 5px;line-height: 25px;' })
          .append('<a href="javascript:void(0)" data-theme="theme-warning" style="background: #FDAC41; display: inline-block;vertical-align: middle;" class="clearfix rounded-circle w-30 h-30" title="Theme warning">'
            + '</a>')
  $skinsList.append($themeWarning)
	
  
	

  

  $demoSettings.append('<h4 class="control-sidebar-heading">Theme Colors</h4>')
  $demoSettings.append($skinsList)
	
  
  // Layout options
  $demoSettings.append(
    '<h4 class="control-sidebar-heading">'
    + 'Dark or Light Skin'
    + '</h4>'
	  
    // Theme Skin Toggle	  
	+ '<div class="flexbox mb-10 pb-10 bb-1 light-on-off">'
	+ '<label for="toggle_left_sidebar_skin" class="control-sidebar-subheading">'
    + 'Light On/Off'
    + '</label>'
	+ '<label class="switch">'
	+ '<input type="checkbox" data-mainsidebarskin="toggle" id="toggle_left_sidebar_skin">'
	+ '<span class="switch-on font-size-30"><i class="mdi mdi-lightbulb-on"></i></span>'
	+ '<span class="switch-off font-size-30"><i class="mdi mdi-lightbulb"></i></span>'
	+ '</label>'
	+ '</div>'  
  )
	
  // Layout options
  $demoSettings.append(
    '<h4 class="control-sidebar-heading">'
    + 'RTL or LTR'
    + '</h4>'
	  
    // rtl layout
	+ '<div class="flexbox mb-10 pb-10 bb-1">'
	+ '<label for="rtl" class="control-sidebar-subheading">'
    + 'Turn RTL/LTR'
    + '</label>'
	+ '<label class="switch switch-border switch-danger">'
	+ '<input type="checkbox" data-layout="rtl" id="rtl">'
	+ '<span class="switch-indicator"></span>'
	+ '<span class="switch-description"></span>'
	+ '</label>'
	+ '</div>'
  )


  // Layout options
  $demoSettings.append(
    '<h4 class="control-sidebar-heading">'
    + 'Layout Options'
    + '</h4>' 
    // Fixed layout
	+ '<div class="flexbox mb-10">'
	+ '<label for="layout_fixed" class="control-sidebar-subheading">'
    + 'Fixed layout'
    + '</label>'
	+ '<label class="switch switch-border switch-danger">'
	+ '<input type="checkbox" data-layout="fixed" id="layout_fixed">'
	+ '<span class="switch-indicator"></span>'
	+ '<span class="switch-description"></span>'
	+ '</label>'
	+ '</div>'	
    // Boxed layout
	+ '<div class="flexbox mb-10">'
	+ '<label for="layout_boxed" class="control-sidebar-subheading">'
    + 'Boxed Layout'
    + '</label>'
	+ '<label class="switch switch-border switch-danger">'
	+ '<input type="checkbox" data-layout="layout-boxed" id="layout_boxed">'
	+ '<span class="switch-indicator"></span>'
	+ '<span class="switch-description"></span>'
	+ '</label>'
	+ '</div>'
    // Sidebar Toggle
	+ '<div class="flexbox mb-10">'
	+ '<label for="toggle_sidebar" class="control-sidebar-subheading">'
    + 'Toggle Sidebar'
    + '</label>'
	+ '<label class="switch switch-border switch-danger">'
	+ '<input type="checkbox" data-layout="sidebar-collapse" id="toggle_sidebar">'
	+ '<span class="switch-indicator"></span>'
	+ '<span class="switch-description"></span>'
	+ '</label>'
	+ '</div>'  
    // Control Sidebar Toggle
	+ '<div class="flexbox mb-10">'
	+ '<label for="toggle_right_sidebar" class="control-sidebar-subheading">'
    + 'Toggle Right Sidebar Slide'
    + '</label>'
	+ '<label class="switch switch-border switch-danger">'
	+ '<input type="checkbox" data-controlsidebar="control-sidebar-open" id="toggle_right_sidebar">'
	+ '<span class="switch-indicator"></span>'
	+ '<span class="switch-description"></span>'
	+ '</label>'
	+ '</div>'	  
	
  )
  
  

  $tabPane.append($demoSettings)
  $('#control-sidebar-home-tab').after($tabPane)

  setup()

  $('[data-toggle="tooltip"]').tooltip()
});// End of use strict

$(function () {
  'use strict'
	
	$('.theme-switch li a').click(function () {
		$('.theme-switch li a').removeClass('active').addClass('inactive');
		$(this).toggleClass('active inactive');
	});
	
});// End of use strict

