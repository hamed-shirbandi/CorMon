/**
 *  ایجاد و ویرایش مطلب و صفحه
 */

(function ($) {
    var ALERT = {
            IMAGE_CONFIRM: "تصویر با موفقیت انتخاب شد.",
            OPERATION_FAILED: "متاسفانه خطایی در پردازش درخواست شما رخ داد. لطفا در صورت تکرار با پشتیبانی تماس بگیرید"
        };

    // Auto compilation.
    CORMON.autoCompilation();

    // Add tags to hidden input
    $("#add-tag").click(function () {
        $(".tm-input").tagsManager('pushTag', $(".tm-input").val());
    });


})(jQuery);