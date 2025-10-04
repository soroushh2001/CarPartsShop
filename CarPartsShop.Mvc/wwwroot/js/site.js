//add to cart
function addToCart(id) {
    var count = parseInt($('#product-count-input').val());
    $.ajax({
        url: "/Order/Home/AddToCart/",
        type: "get",
        data: {
            id: id,
            count: count
        },
        beforeSend: function () {
            startWaiting();
        },
        success: function (response) {
            closeWaiting();
            if (response.status === 401) {
                toastr.info(response.message);

            } else {
                toastr.success(response.message);
                $("#header").load(location.href + " #header");
            }
        },
        error: function () {
            closeWaiting();
        }
    });
}

//remove form cart

function removeFormCart(orderDetailId) {
    $.ajax({
        url: "/Order/Home/RemoveItemFromCart/",
        type: "get",
        data: {
            orderDetailId: orderDetailId,
        },
        beforeSend: function () {
            startWaiting();
        },
        success: function (response) {
            closeWaiting();
            toastr.success(response.message);
            location.reload();

        },
        error: function () {
            closeWaiting();
        }
    });
}

//increase or decrease cart item
function increaseDecreaseCartItem(orderDetailId,op) {
    $.ajax({
        url: "/Order/Home/IncreaseDecreaseCartItem/",
        type: "get",
        data: {
            orderDetailsId: orderDetailId,
            op:op
        },
        beforeSend: function () {
            startWaiting();
        },
        success: function (response) {
            closeWaiting();
            toastr.success(response.message);
            location.reload();
        },
        error: function () {
            closeWaiting();
        }
    });
}

//comment
function addComment(response) {
    closeWaiting();
    if (response.status === 200) {
        toastr.success(response.message);
    } else {
        toastr.error(response.message);
    }
}
