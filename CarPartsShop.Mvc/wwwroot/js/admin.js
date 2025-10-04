
//category
function toggleDeleteCategory(categoryId, categoryTitle, deleteStatus) {
    
    let title;
    if (deleteStatus === true) {
        title = 'بازگردانی';

    } else {
        title = 'حذف';
    }

    Swal.fire({
        title: `آیا از ${title} ${categoryTitle} مطمئن هستید؟`,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "بله",
        cancelButtonText: "لغو"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "/admin/categories/ToggleDeleteCategory/",
                type: "get",
                data: {
                    categoryId: categoryId
                },
                beforeSend: function () {
                    startWaiting();
                },
                success: function (response) {
                    closeWaiting();
                    if (response.status === 200) {
                        toastr.success("عملیات با موفقیت انجام شد");
                        location.reload();
                    } else {
                        toastr.error("عملیات با شکست مواجه شد");
                    }
                },
                error: function () {
                    closeWaiting();
                }
            });
        }
    });
}

function loadCreateCategoryModal(parentId, parentName) {
    $.ajax({
        url: "/Admin/Categories/LoadCreateCategoryModal/",
        type: "get",
        data: {
            parentId: parentId,
            parentName: parentName
        },
        beforeSend: function () {
            startWaiting();
        },
        success: function (response) {
            closeWaiting();
            $(".modal-content").html(response);

            $('#create-category-form').data('validator', null);
            $.validator.unobtrusive.parse('#create-category-form');

            $("#basicModal").modal("show");
        },
        error: function () {
            closeWaiting();
        }
    });
}

function submitCreateCategoryModal(response) {
    closeWaiting();
    if (response.status === 200) {
        toastr.success('عملیات با موفقیت انجام شد');
        $("#basicModal").modal("hide");
        $("#categories-div").load(location.href + " #categories-div");
    } else {
        toastr.error(response.message);
    }
}

function loadEditCategoryModal(id) {
    $.ajax({
        url: "/Admin/Categories/LoadEditCategoryModal/",
        type: "get",
        data: {
            id: id,
        },
        beforeSend: function () {
            startWaiting();
        },
        success: function (response) {
            closeWaiting();
            $(".modal-content").html(response);

            $('#edit-category-form').data('validator', null);
            $.validator.unobtrusive.parse('#edit-category-form');

            $("#basicModal").modal("show");
        },
        error: function () {
            closeWaiting();
        }
    });
}

function submitEditCategoryModal(response) {
    closeWaiting();
    if (response.status === 200) {
        toastr.success('عملیات با موفقیت انجام شد');
        $("#basicModal").modal("hide");
        $("#categories-div").load(location.href + " #categories-div");
    } else {
        toastr.error(response.message);
    }
}

//brand

function toggleDeleteBrand(brandId, brandTitle, deleteStatus) {

    let title;
    if (deleteStatus === true) {
        title = 'بازگردانی';

    } else {
        title = 'حذف';
    }

    Swal.fire({
        title: `آیا از ${title} ${brandTitle} مطمئن هستید؟`,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "بله",
        cancelButtonText: "لغو"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "/admin/CarBrands/ToggleDeleteBrand/",
                type: "get",
                data: {
                    brandId: brandId
                },
                beforeSend: function () {
                    startWaiting();
                },
                success: function (response) {
                    closeWaiting();
                    if (response.status === 200) {
                        toastr.success("عملیات با موفقیت انجام شد");
                        location.reload();
                    } else {
                        toastr.error("عملیات با شکست مواجه شد");
                    }
                },
                error: function () {
                    closeWaiting();
                }
            });
        }
    });
}

function loadCreateBrandModal(parentId, parentName) {
    $.ajax({
        url: "/Admin/CarBrands/LoadCreateBrandModal/",
        type: "get",
        data: {
            parentId: parentId,
            parentName: parentName
        },
        beforeSend: function () {
            startWaiting();
        },
        success: function (response) {
            closeWaiting();
            $(".modal-content").html(response);

            $('#create-brand-form').data('validator', null);
            $.validator.unobtrusive.parse('#create-brand-form');

            $("#basicModal").modal("show");
        },
        error: function () {
            closeWaiting();
        }
    });
}

function submitCreateBrandModal(response) {
    closeWaiting();
    if (response.status === 200) {
        toastr.success('عملیات با موفقیت انجام شد');
        $("#basicModal").modal("hide");
        $("#brands-div").load(location.href + " #brands-div");
    } else {
        toastr.error(response.message);
    }
}

function loadEditBrandModal(id) {
    $.ajax({
        url: "/Admin/CarBrands/LoadEditBrandModal/",
        type: "get",
        data: {
            id: id,
        },
        beforeSend: function () {
            startWaiting();
        },
        success: function (response) {
            closeWaiting();
            $(".modal-content").html(response);

            $('#edit-brand-form').data('validator', null);
            $.validator.unobtrusive.parse('#edit-brand-form');

            $("#basicModal").modal("show");
        },
        error: function () {
            closeWaiting();
        }
    });
}

function submitEditBrandModal(response) {
    closeWaiting();
    if (response.status === 200) {
        toastr.success('عملیات با موفقیت انجام شد');
        $("#basicModal").modal("hide");
        $("#brands-div").load(location.href + " #brands-div");
    } else {
        toastr.error(response.message);
    }
}

//user

function loadManageUserRolesModal(email) {
    $.ajax({
        url: "/Admin/Users/LoadManageUserRolesModal/",
        type: "get",
        data: {
            email: email,
        },
        beforeSend: function () {
            startWaiting();
        },
        success: function (response) {
            closeWaiting();
            $(".modal-content").html(response);

            $('#ManageUserRoles-form').data('validator', null);
            $.validator.unobtrusive.parse('#ManageUserRoles-form');

            $("#basicModal").modal("show");
        },
        error: function () {
            closeWaiting();
        }
    });
}

function submitManageUserRolesModal(response) {
    closeWaiting();
    if (response.status === 200) {
        toastr.success(response.message);
        $("#basicModal").modal("hide");
        $("#user-div").load(location.href + " #user-div");
    } else {
        toastr.error(response.message);
    }
}


function  loadChangeUserPasswordModal(email) {
    $.ajax({
        url: "/Admin/Users/LoadChangeUserPasswordModal/",
        type: "get",
        data: {
            email: email,
        },
        beforeSend: function () {
            startWaiting();
        },
        success: function (response) {
            closeWaiting();
            $(".modal-content").html(response);

            $('#ChangeUserPassword-form').data('validator', null);
            $.validator.unobtrusive.parse('#ChangeUserPassword-form');

            $("#basicModal").modal("show");
        },
        error: function () {
            closeWaiting();
        }
    });
}

function submitChangeUserPasswordModal(response) {
    closeWaiting();
    if (response.status === 200) {
        toastr.success(response.message);
        $("#basicModal").modal("hide");
        $("#user-div").load(location.href + " #user-div");
    } else {
        toastr.error(response.message);
    }
}

//product
function changeProductAvailabilityStatus(productId, title) {


    Swal.fire({
        title: `آیا از تغییر وضعیت ${title} مطمئن هستید؟`,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "بله",
        cancelButtonText: "لغو"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "/admin/Products/ToggleProductStatus",
                type: "get",
                data: {
                    productId: productId
                },
                beforeSend: function () {
                    startWaiting();
                },
                success: function (response) {
                    closeWaiting();
                    if (response.status === 200) {
                        toastr.success("عملیات با موفقیت انجام شد");
                        location.reload();
                    } else {
                        toastr.error("عملیات با شکست مواجه شد");
                    }
                },
                error: function () {
                    closeWaiting();
                }
            });
        }
    });
}

//input mask

$(document).ready(function() {
    $('#Price').inputmask('numeric', {
        alias: 'numeric',
        groupSeparator: ',',
        autoGroup: true,
        digits: 0,
        digitsOptional: false,
        placeholder: '0',
        removeMaskOnSubmit: true 
    });
});

//order
function loadRecipientInfoModal(refCode) {
    $.ajax({
        url: "/Admin/Orders/LoadRecipientInfoModal/",
        type: "get",
        data: {
            refCode: refCode,
        },
        beforeSend: function () {
            startWaiting();
        },
        success: function (response) {
            closeWaiting();
            $(".modal-content").html(response);

            $("#basicModal").modal("show");
        },
        error: function () {
            closeWaiting();
        }
    });
}
function loadChangeOrderStatusModal(refCode) {
    $.ajax({
        url: "/Admin/Orders/LoadChangeOrderStatusModal/",
        type: "get",
        data: {
            refCode: refCode,
        },
        beforeSend: function () {
            startWaiting();
        },
        success: function (response) {
            closeWaiting();
            $(".modal-content").html(response);

            $('#edit-order-form').data('validator', null);
            $.validator.unobtrusive.parse('#edit-order-form');

            $("#basicModal").modal("show");
        },
        error: function () {
            closeWaiting();
        }
    });
}

function submitChangeOrderStatusModal(response) {
    closeWaiting();
    if (response.status === 200) {
        toastr.success(response.message);
        $("#basicModal").modal("hide");
        $("#orders-div").load(location.href + " #orders-div");
    } else {
        toastr.error(response.message);
    }
}

//slider
function loadAddSliderModal() {
    $.ajax({
        url: "/Admin/Sliders/LoadAddSliderModal/",
        type: "get",
        beforeSend: function () {
            startWaiting();
        },
        success: function (response) {
            closeWaiting();
            $(".modal-content").html(response);

            $('#add-slider-form').data('validator', null);
            $.validator.unobtrusive.parse('#add-slider-form');

            $("#basicModal").modal("show");
        },
        error: function () {
            closeWaiting();
        }
    });
}
function submitAddSliderModal(response) {
    closeWaiting();
    if (response.status === 200) {
        toastr.success('عملیات با موفقیت انجام شد');
        $("#basicModal").modal("hide");
        $("#slider-div").load(location.href + " #slider-div");
    } else {
        toastr.error(response.message);
    }
}
function removeSlider(sliderId) {

    Swal.fire({
        title: `آیا از حذف این مورد اطمینان دارید؟`,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "بله",
        cancelButtonText: "لغو"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "/admin/Sliders/RemoveSlider/",
                type: "get",
                data: {
                    sliderId: sliderId
                },
                beforeSend: function () {
                    startWaiting();
                },
                success: function (response) {
                    closeWaiting();
                    if (response.status === 200) {
                        toastr.success(response.message);
                        location.reload();
                    } else {
                        toastr.error(response.message);
                    }
                },
                error: function () {
                    closeWaiting();
                }
            });
        }
    });
}
