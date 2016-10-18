define([
    "dojo/_base/declare",
    "dojo/parser",
     "dojo/when",
     "dojo/_base/lang",
     "dojo/window",
     "dojox/layout/ScrollPane",

    "dijit/_Widget",
    "dijit/_TemplatedMixin",

    "epi-cms/contentediting/StandardToolbar",

    "epi/dependency",

    "dojo/text!./templates/contactinformationtemplate.html",
    "xstyle/css!./templates/styles/custom-styles.css",

    "app/editors/gmapLoader!http://maps.google.com/maps/api/js?key=AIzaSyDiD2Z6dHg-PGNCYQQ0v0pWZyecX40A9cw"

], function (
    declare,
    parser,
    when,
    lang,
    window,
    ScrollPane,

    Widget,
    TemplatedMixin,

    StandardToolbar,

    dependency,

    template
) {
    return declare("app.editors.contactinformation", [Widget, TemplatedMixin], {

        templateString: template,
        contactStore: null,

        postCreate: function () {
            this.inherited(arguments);

            var height = window.getBox().h - 200;
            this.containerScroll.style.height = height + "px";

            parser.parse(this.containerRoot);

            console.log("Contact information widget loaded");

            this.toolbar = new StandardToolbar();
            this.toolbar.placeAt(this.toolbarArea, "first");

            var registry = dependency.resolve("epi.storeregistry");
            this.contactStore = registry.get("contactstore");

            var contextService = epi.dependency.resolve("epi.shell.ContextService");
            var currentContext = contextService.currentContext;

            this.initContactInformation(currentContext);

            
        },

        initContactInformation: function(context) {
            var res = context.id.split("_");

            var currentContentId = res[0];
            var providerName = "";
            if (res.length > 2) {
                providerName = res[2];
            }

            dojo.when(this.contactStore.query(
                    {
                        id: currentContentId,
                        providerName: providerName
                    }),
                    lang.hitch(this, function (data) {
                        this.fullNameHeader.innerText = data.fullName;
                        this.fullNameLabel.innerText = data.fullName;
                        this.addressLabel.innerText = data.addressText;
                        this.emailLabel.innerText = data.email;
                        this.phoneNumberLabel.innerText = data.phonenumber;
                        this.companyLabel.innerText = data.company;
                        this.functionLabel.innerText = data.function;

                        this.initGoogleMap(data);
                    }));
        },

        initGoogleMap: function (data) {
            var geocoder = new google.maps.Geocoder();
            geocoder.geocode({ 'address': data.city + ", " + data.country }, lang.hitch(this, function (results, status) {
                if (status == 'OK') {
                    var latLng = new google.maps.LatLng(results[0].geometry.location.lat(), results[0].geometry.location.lng()); {; }

                    console.log("Lookup coordinates: " + latLng);

                    var map = new google.maps.Map(this.map, {
                        center: latLng,
                        zoom: 8
                    });

                    var marker = new google.maps.Marker({
                         map: map,
                         position: latLng});
                }
            }));
        },

        updateView: function(data, context, additionalParams) {
            // summary:
            //      Called by the menu item
            // tags:
            //      public
            if (data && data.skipUpdateView) {
                return;
            }

            this.initContactInformation(context);

            this.toolbar.update({
                currentContext: context,
                viewConfigurations: {
                    availableViews: data.availableViews,
                    viewName: data.viewName
                }
            });
        }
    });
});