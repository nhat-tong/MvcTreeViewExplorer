/* Ref: https://github.com/mar10/fancytree/wiki/TutorialLoadData*/

var TreeView = (function () {

    function TreeView(options) {
        this.config = $.extend({}, this.defaults, options);
    }

    TreeView.prototype = {
        defaults: {
            checkbox: false,
            multiselection: false,
            treeSelector: "",
        },
        config: {},
        isInitializing: true,
        initialize: initialize
    };
    
    function initialize() {
        var that = this,
            $tree = $(that.config.treeSelector);

        // Initialize the tree
        var fancytreeConfig = {
            minExpandLevel: 2,
            checkbox: that.config.checkbox,
            clickFolderMode: 1,
            debugLevel: 0
        };

        // Create fancytree from HTML Markup inside $tree and configuration from fancytreeConfig
        $tree.fancytree(fancytreeConfig);

        // Display tree when it's available
        $tree.removeAttr("style");

        // Get tree instance
        var tree = $tree.fancytree("getTree");

        // Initialization finished
        that.isInitializing = false;
    }

    return TreeView;
})();