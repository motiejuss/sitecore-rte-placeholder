$(function() {
    if (!$('.accordion').length) {
        return;
    }

    var _accordionItem = $(".accordion ul").find('input[type="checkbox"]');

    _accordionItem.on("change", function() {
        accordionToggle($(this));
    });

    var accordionToggle = function(obj) {

        var _isChecked = obj.prop("checked");

        for (var i = 0; i < _accordionItem.length; i++) {
            $(_accordionItem[i]).prop("checked", false);
            $(_accordionItem[i]).nextAll(".tab__content").css("max-height", 0);
        }

        var _state = _isChecked != false ? obj.prop("checked", true) : obj.prop("checked", false);

        var _setHeight = _isChecked == true ? obj.nextAll(".tab__content").css("max-height", _maxHeight) : obj.nextAll(".tab__content").css("max-height", 0);
    }

    var getItemHeights = function() {
        var _itemHeights = [];

        _accordionItem.each(function() {
            $(this).prop("checked", true);
            _itemHeights.push($(this).nextAll(".tab__content").find("p").height());
            if (!$(this).hasClass("checked")) {
                $(this).prop("checked", false);
            }
        });

        return _maxHeight = Math.max.apply(Math, _itemHeights) + 60 + 'px';
    }

    $(window).on("resize", function() {
        getItemHeights();
    });

    getItemHeights(); 
});
