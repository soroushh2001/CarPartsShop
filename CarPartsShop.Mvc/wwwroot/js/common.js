//handle paging-form
function submitPagingForm(pageIndex){
    $("#PageIndex").val(pageIndex);
    $('#filter-form').submit();
}

//ajax wating
function startWaiting(element = 'body') {
    $(element).waitMe({
        effect: 'bounce',
        text: 'لطفا صبر کنید...',
        bg: 'rgba(255, 255, 255, 0.7)',
        color: '#000'
    });
}

function closeWaiting(element = 'body') {
    $(element).waitMe('hide');
}

//toastr-config
toastr.options = {
    "closeButton": true,
    "debug": false,
    "newestOnTop": false,
    "progressBar": true,
    "positionClass": "toast-bottom-right",
    "preventDuplicates": false,
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "5000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
};

//preview-image
document.addEventListener('DOMContentLoaded', function () {
    const fileInputs = document.querySelectorAll('input[type="file"]');

    fileInputs.forEach(input => {
        input.addEventListener('change', function () {
            const previewId = this.getAttribute('image-input');
            const preview = document.querySelector(`[image-preview="${previewId}"]`);

            if (this.files && this.files[0]) {
                const reader = new FileReader();
                reader.onload = function (e) {
                    preview.src = e.target.result;
                    preview.style.display = 'block';
                };
                reader.readAsDataURL(this.files[0]);
            }
        });
    });
});

