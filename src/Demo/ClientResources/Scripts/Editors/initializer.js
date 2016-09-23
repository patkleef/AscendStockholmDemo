define([
    "dojo/_base/declare",

    "epi",
    "epi/_Module",
    "epi/routes",
    "epi/dependency"
],
function (
    declare,

    epi,
    _Module,
    routes,
    dependency
) {
    return declare([], {

        initialize: function () {
            this.inherited(arguments);

            console.log("Initialize module called");

            var registry = dependency.resolve("epi.storeregistry");
    
            registry.create("contactstore", this._getRestPath("contactstore"));
        },

        _getRestPath: function (name) {
            return routes.getRestPath({ moduleArea: "app", storeName: name });
        }
    });
});