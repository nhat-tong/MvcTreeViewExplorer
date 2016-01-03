/* Ref: https://github.com/mar10/fancytree/wiki/TutorialLoadData*/

var TreeView = (function () {
    // Define TreeView class
    TreeView.prototype = {
        properties: {
            checkbox: false,
            multiselection: false,
            hierarchical: false,
            treeId: ""
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
            selectMode: (that.config.multiselection ? 2 : that.config.hierarchical ? 3 : 1),
            // selectMode: 1 (single-selection), selectMode:2 (multi-selection), selectMode: 3 (hierarchical multi-selection)
            clickFolderMode: 1,
            debugLevel: 0,
            select: onSelect,
            click: onClick
        };

        $tree.fancytree(fancytreeConfig);
        $tree.removeAttr("style");
        //$tree.fancytree("getTree");
    }

    function onSelect(event, data) {
        // Display list of selected nodes
        var selectedNodes = data.tree.getSelectedNodes();
        // convert to title/key array
        var selKeys = $.map(selNodes, function (node) {
            return "[" + node.key + "]: '" + node.title + "'";
        });
    }

    function onClick(event, data) {}

    return TreeView;
})();