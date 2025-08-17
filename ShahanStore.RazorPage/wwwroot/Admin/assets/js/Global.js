async function showAlert(options) {
    const defaultOptions = {
        buttonsStyling: false,
        confirmButtonText: "باشه"
    };
    const finalOptions = { ...defaultOptions, ...options };
    return Swal.fire(finalOptions);
}


function Success(title, description, options = {}) {
    if (typeof title === "undefined" || title === null) {
        title = "عملیات با موفقیت انجام شد";
    }
    if (typeof description === "undefined" || description === null) {
        description = "";
    }
    return showAlert({
        title,
        text: description,
        icon: "success",
        showCancelButton: true,
        customClass: {
            confirmButton: "btn btn-primary me-2 mt-2",
            cancelButton: "btn btn-danger mt-2",
        },
        buttonsStyling: false,
        showCloseButton: true,
        ...options
    });
}


function ErrorAlert(title, description, options = {}) {
    if (typeof title === "undefined" || title === null) {
        title = "عملیات با شکست مواجه شد";
    }
    if (typeof description === "undefined" || description === null) {
        description = "";
    }
    return showAlert({
        title,
        text: description,
        icon: "error",
        customClass: {
            confirmButton: "btn btn-primary me-2 mt-2",
        },
        buttonsStyling: false,
        showCloseButton: true,
        ...options
    });
}


$(document).ready(function () {
    $(document).on('click', '.open-modal-btn', async function () {
        const button = $(this);
        const url = button.data('url');
        const title = button.data('title');
        const modal = $('#defaultModal');

        if (!url) {
            console.error("No URL found on the button.");
            return;
        }

        try {
            const response = await fetch(url);
            if (!response.ok) {
                // اگر سرور یک پیام خطا فرستاده بود، آن را نمایش بده
                const errorData = await response.json();
                ErrorAlert(errorData.title, errorData.description);
                return; // <<-- از ادامه اجرا جلوگیری کن
            }

            const htmlContent = await response.text();

            modal.find('.modal-body').html(htmlContent);
            modal.find('.modal-title').html(title);

            // مودال را نمایش می‌دهیم
            modal.modal('show');

            // ولیدیشن را روی فرم جدید (اگر وجود داشت) فعال می‌کنیم
            const form = modal.find('form');
            if (form.length) {
                $.validator.unobtrusive.parse(form);
            }
        } catch (error) {
            console.error("Error opening modal:", error);
            ErrorAlert("خطای سیستمی", "مشکلی در بارگذاری اطلاعات رخ داده است.");
        }
    });
});


$(document).on('click', '.delete-item-btn', function (e) {
    e.preventDefault();

    var url = $(this).data('url');
    var description = $(this).data('title') || 'آیا از حذف مطمئن هستید؟';

    Swal.fire({
        title: "مطمئن هستی؟",
        text: description,
        icon: "warning",
        showCancelButton: !0,
        customClass: {
            confirmButton: "btn btn-primary me-2 mt-2",
            cancelButton: "btn btn-danger mt-2",
        },
        confirmButtonText: "بله حذف کنید!",
        buttonsStyling: !1,
        showCloseButton: !0,
        cancelButtonText: 'انصراف',
    }).then((result) => {
        if (result.isConfirmed) {
            var token = $('input[name="__RequestVerificationToken"]').val();

            $.ajax({
                url: url,
                type: 'POST',
                data: {
                    __RequestVerificationToken: token
                },
                success: function (response) {
                    if (response.Status === 1 || response.Status === 'Success') {
                        Success('حذف شد', response.Message);
                        if (response.ReloadPage) {
                            setTimeout(() => location.reload(), 1500);
                        }
                    } else {
                        ErrorAlert('خطا', response.Message);
                        if (response.ReloadPage) {
                            setTimeout(() => location.reload(), 1500);
                        }
                    }
                },
                error: function () {
                    ErrorAlert('خطا', 'خطا در سرور');
                }
            });
        }
    });
});




/**
 * sendAjaxRequest: ارسال درخواست Ajax به صورت کلی و قابل استفاده مجدد
 * @param {object} options
 *  - url: آدرس درخواست
 *  - method: نوع متد HTTP (default: "POST")
 *  - data: داده‌هایی که ارسال می‌شود (default: {})
 *  - sendAsJson: ارسال داده‌ها به صورت JSON (default: false)
 *  - reloadOnSuccess: ریلود صفحه پس از موفقیت (default: false)
 *  - reloadOnError: ریلود صفحه پس از خطا (default: false)
 *  - redirectOnSuccess: آدرس ریدایرکت پس از موفقیت (default: null)
 *  - redirectOnError: آدرس ریدایرکت پس از خطا (default: null)
 *  - customSuccess: تابع callback سفارشی برای موفقیت (default: null)
 *  - customError: تابع callback سفارشی برای خطا (default: null)
 */
function sendAjaxRequest(options) {
    const {
        url,
        method = "POST",
        data = {},
        sendAsJson = false,
        reloadOnSuccess = false,
        reloadOnError = false,
        redirectOnSuccess = null,
        redirectOnError = null,
        customSuccess = null,
        customError = null,
        headers = {},
        beforeSend = null,
        complete = null,
    } = options;

    // افزودن توکن آنتی‌فورگ‌ری
    const tokenElement = $("input[name='__RequestVerificationToken']");
    if (tokenElement.length > 0) {
        data.__RequestVerificationToken = tokenElement.val();
    }

    $.ajax({
        url: url,
        method: method,
        data: sendAsJson ? JSON.stringify(data) : data,
        contentType: sendAsJson ? "application/json; charset=utf-8" : "application/x-www-form-urlencoded; charset=UTF-8",
        processData: !sendAsJson,
        headers: headers,
        beforeSend: function (jqXHR, settings) {
            if (typeof beforeSend === "function") beforeSend(jqXHR, settings);
        },
        complete: function (jqXHR, textStatus) {
            if (typeof complete === "function") complete(jqXHR, textStatus);
        },
        success: function (res) {
            if (res.Status && res.Status.toLowerCase() === "success") {
                Success("موفق", res.Message || "عملیات با موفقیت انجام شد");
                if (redirectOnSuccess) {
                    setTimeout(() => window.location.href = redirectOnSuccess, 1500);
                } else if (reloadOnSuccess || res.ReloadPage) {
                    setTimeout(() => location.reload(), 1500);
                }
                if (typeof customSuccess === "function") customSuccess(res);
            } else {
                ErrorAlert("خطا", res.Message || "عملیات با خطا مواجه شد");
                if (redirectOnError) {
                    setTimeout(() => window.location.href = redirectOnError, 1500);
                } else if (reloadOnError || res.ReloadPage) {
                    setTimeout(() => location.reload(), 1500);
                }
                if (typeof customError === "function") customError(res);
            }
        },

        error: function (xhr) {
            let message = "مشکلی در ارتباط با سرور رخ داده است";
            if (xhr.responseJSON && xhr.responseJSON.Message) {
                message = xhr.responseJSON.Message;
            } else if (xhr.responseText) {
                message = xhr.responseText;
            }
            ErrorAlert("خطا در ارتباط با سرور", message);
            if (typeof customError === "function") {
                customError({ xhr });
            }
        },
    });
}

//function changePageId(pageId) {
//    if (pageId < 1) return; // جلوگیری از ارسال درخواست با صفحه نامعتبر

//    var params = {
//        pageId: pageId,
//        take: 10, // یا مقدار دلخواهت
//        search: $("#searchInput").val(), // اگر فیلتر جستجو داری
//        status: $("#statusSelect").val() // اگر فیلتر وضعیت داری
//    };

//    $.ajax({
//        url: window.location.pathname,
//        type: 'GET',
//        data: params,
//        headers: { "X-Requested-With": "XMLHttpRequest" },
//        success: function (res) {
//            $('#items-container').html(res.dataHtml);
//            $('#pagination-container').html(res.paginationHtml);
//        },
//        error: function () {
//            Swal.fire('خطا', 'خطا در بارگذاری داده‌ها', 'error');
//        }
//    });
//}

function changePageId(pageId) {
    var url = new URL(window.location.href);
    var search_params = url.searchParams;
    search_params.set('pageId', pageId);
    url.search = search_params.toString();
    var new_url = url.toString();

    $.ajax({
        url: new_url,
        type: "GET"
    }).done(function (data) {
        window.location.replace(new_url);
    });
}