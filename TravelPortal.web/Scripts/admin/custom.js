function fn_autocomplete(txtfieldId, url, hdnfieldId) {
    
    $(txtfieldId).autocomplete({
        source: url,
        minLength: 0,
        delay: 300,
        autoFocus: true,
        appendTo: $(txtfieldId).parent(), // optional: attach to a specific element
        position: { my: "left top", at: "left bottom" },
        disabled: false,

        // Events
        search: function (event) {
            //$(".ui-menu").show();
            //console.log("Search started");
        },
        open: function (event) {
            //console.log("Menu opened");
        },
        close: function (event) {
            //console.log("Menu closed");
        },
        focus: function (event, ui) {
            //console.log("Item focused: " + ui.item.label);
            return false; // prevents the value from being inserted automatically
        },
        select: function (event, ui) {
            //console.log("Selected: " + ui.item.label + " (ID: " + ui.item.id + ")");
            // Optionally store the ID in a hidden field

            $(hdnfieldId).val(ui.item.id);
            $(txtfieldId).autocomplete("close");
        },
        change: function (event, ui) {
            if (!ui.item) {
                // console.log("No item selected, value was changed manually.");

            }
        },
        response: function (event, ui) {
            //console.log("Received " + ui.content.length + " results.");
        }
    }).focus(function () {
        // Trigger search on focus
        $(this).autocomplete("search", "");
    });
}