/* Ref: https://github.com/mar10/fancytree/wiki/TutorialLoadData*/

var TreeView = (function () {
    // Define TreeView class
    TreeView.prototype = {
        properties: {
            checkbox: false,
            multiselection: false,
            treeId: "",
        },
        config: {},
        initialize: initialize
    };

    //Constructor
    function TreeView(options) {
        this.config = $.extend({}, this.properties, options);
    }
    
    function initialize() {
        var that = this,
            $tree = $(that.config.treeId);

        // Initialize the fancytree
        var fancytreeConfig = {
            minExpandLevel: 2,
            checkbox: that.config.checkbox,
            clickFolderMode: 1,
            debugLevel: 0
        };

        $tree.fancytree(fancytreeConfig);
        $tree.removeAttr("style");
        //$tree.fancytree("getTree");
    }

    return TreeView;
})();