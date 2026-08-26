(function () {
    if (typeof $ === "undefined" || !$.validator || !$.validator.unobtrusive) return;

    $.validator.addMethod("notfutureyear", function (value, element, param) {
        return this.optional(element) || parseInt(value, 10) <= parseInt(param, 10);
    });
    $.validator.unobtrusive.adapters.addSingleVal("notfutureyear", "max");

    $.validator.addMethod("posterrequired", function (value, element) {
        var form = element.form;
        var fileInput = form ? form.querySelector('input[type="file"]') : null;
        var hasFile = !!(fileInput && fileInput.files && fileInput.files.length > 0);
        return (value && value.trim().length > 0) || hasFile;
    });
    $.validator.unobtrusive.adapters.add("posterrequired", [], function (options) {
        options.rules["posterrequired"] = {};
        options.messages["posterrequired"] = options.message;
    });

    $(document).on("change", 'input[type="file"]', function () {
        var $form = $(this).closest("form");
        $form.find('[data-val-posterrequired]').valid();
    });
})();
