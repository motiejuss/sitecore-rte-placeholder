(function () {
    if (typeof (Telerik) != 'undefined' && Type.isNamespace(Telerik.Web)) {

        var pathToDialog = "/Editor/InsertAccordion/";
        var rteButtonCommand = "InsertAccordion"; // Register your click command in Sitecore (InsertNotification): /sitecore/system/Settings/Html Editor Profiles/SDS Rich Text Full/Toolbar 1/Insert Notification
        var dialogTitle = "Insert Accordion";
        var dialogCssFile = pathToDialog + "toolbar-button.css";        
        var dialogLayout = "/sitecore/shell/default.aspx";        
        var dialogArguments = {
            xmlcontrol: "RichText.InsertAccordion",
            selectedItemId: ""
        };
        var safetyCheckMessage = "Make sure you are in the empty line and the cursor is at the beginning. Otherwise use Format Stripper.";

        // helper func
        if (!String.prototype.format) {
            String.prototype.format = function () {
                var args = arguments;
                return this.replace(/{(\d+)}/g, function (match, number) {
                    return typeof args[number] != 'undefined'
                      ? args[number]
                      : match
                    ;
                });
            };
        }

        jQuery(document).ready(function ($) {            
            var $buttonStyles = $("<style>.reTool ." + rteButtonCommand + " {background-image:url('" + pathToDialog + "toolbar-button.png') !important;}</style>");
            $("head").append($buttonStyles);
            $("span." + rteButtonCommand).attr("title", dialogTitle);
        });        

        Telerik.Web.UI.Editor.CommandList[rteButtonCommand] = function (commandName, editor, args) {
            
            var elem = editor.getSelectedElement(); //returns the selected element.
            var utils = Telerik.Web.UI.Editor.Utils;

            var dialogCallback = function (sender, args) {                
                var template = "";
                template += "<div data-id=\"{0}\" class=\"editor-control editor-control-accordion\" contenteditable=\"false\" unselectable=\"on\">";
                template += "[accordion]{1}[/accordion]";
                template += "</div>";                    
                if (args.dialogContentItemId == "") {
                    alert("Du valgte ikke en komponent eller den komponent du valgte var ikke en fulde-ud tabel");
                } else {
                    jQuery(elem).replaceWith(String.format(template, args.dialogContentItemId, args.dialogContentItemId));
                }
            };            

            if ((utils.isNodeEmpty(elem) && utils.emptyNodeRegExp.test(elem.innerHTML)) || jQuery(elem).hasClass("editor-control-accordion")) {
                editor.showExternalDialog(
                    dialogLayout + "?" + jQuery.param(dialogArguments),
                    null,
                    500,
                    390,
                    dialogCallback,
                    null,
                    dialogTitle,
                    true,
                    Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Move,
                    false,
                    false
                );
            } else {
                alert(safetyCheckMessage);
            }

        };
        
        window.OnClientLoad = function (editor) {

            var button = editor.getToolByName(rteButtonCommand);

            editor.attachEventHandler("click", function (e) {                

                var $doc = jQuery(editor.get_document());
                $doc.find(".editor-control-accordion").removeClass("selected");
                dialogArguments.selectedItemId = "";
                button.setState(0);

                var $elem = jQuery(editor.getSelectedElement());
                if ($elem.hasClass("editor-control-accordion")) {
                    $elem.addClass("selected");
                    button.setState(1);
                    dialogArguments.selectedItemId = $elem.data("id");
                }

            });

            editor.attachEventHandler("keydown", function (e) {                
                // Delete
                if (e.keyCode == 46) {                    
                    var $elem = jQuery(editor.getSelectedElement());
                    if ($elem.hasClass("editor-control-accordion")) {
                        e.preventDefault();
                        $elem.remove();
                        button.setState(0);
                        return false;
                    }
                }
            });

        };
        
    } else {
        alert('Telerik Undefined');
    }
})();