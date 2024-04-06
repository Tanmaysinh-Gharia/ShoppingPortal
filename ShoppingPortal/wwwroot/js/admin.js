class AdminDashboard {
    static init() {
        this.setupRefreshButton();
        this.formatTimestamps();
        this.setupDeleteButtons('.delete-category', '/Admin/DeleteCategory');
        this.setupDeleteButtons('.delete-product', '/Admin/DeleteProduct');
        this.setupStatusDropdowns();
    }

    static setupRefreshButton() {
        $('#refreshDashboard').click(function () {
            const btn = $(this);
            btn.prop('disabled', true);
            btn.html('<span class="spinner-border spinner-border-sm" role="status"></span> Refreshing...');

            $.get(window.location.href, function (data) {
                $('main').html($(data).find('main').html());
                AdminDashboard.init(); // Reinitialize after refresh
                appToast.show('Dashboard refreshed', 'success');
            }).always(function () {
                btn.prop('disabled', false);
                btn.html('<i class="bi bi-arrow-clockwise"></i> Refresh');
            });
        });
    }

    static formatTimestamps() {
        $('[data-timestamp]').each(function () {
            const timestamp = new Date($(this).data('timestamp'));
            $(this).text(this.timeSince(timestamp));
        });
    }

    static setupDeleteButtons(selector, url) {
        $(selector).click(function () {
            const id = $(this).data('id');
            if (confirm('Are you sure you want to delete this item?')) {
                $.post(url, { id: id }, function () {
                    location.reload();
                }).fail(function () {
                    appToast.show('Error deleting item', 'error');
                });
            }
        });
    }

    static setupStatusDropdowns() {
        $('.order-status').change(function () {
            const orderId = $(this).data('order-id');
            const newStatus = $(this).val();

            $.post('/Admin/UpdateOrderStatus', {
                orderId: orderId,
                newStatus: newStatus
            }, function (response) {
                if (response.success) {
                    appToast.show('Order status updated', 'success');
                } else {
                    appToast.show(response.message || 'Error updating status', 'error');
                }
            }).fail(function () {
                appToast.show('Error updating order status', 'error');
            });
        });
    }

    static timeSince(date) {
        const seconds = Math.floor((new Date() - date) / 1000);
        let interval = Math.floor(seconds / 31536000);

        if (interval >= 1) return interval + " years ago";
        interval = Math.floor(seconds / 2592000);
        if (interval >= 1) return interval + " months ago";
        interval = Math.floor(seconds / 86400);
        if (interval >= 1) return interval + " days ago";
        interval = Math.floor(seconds / 3600);
        if (interval >= 1) return interval + " hours ago";
        interval = Math.floor(seconds / 60);
        if (interval >= 1) return interval + " minutes ago";
        return Math.floor(seconds) + " seconds ago";
    }

    static setupProductForms() {
        // Image preview for product forms
        // Image preview for product forms
        $('input[type="file"][name="imageFile"]').on('change', (function () {
            const file = this.files[0];
            if (file) {
                const reader = new FileReader();
                reader.onload = function (e) {
                    $('.image-preview').remove();
                    $(this).closest('.mb-3').append(`
                    <div class="mt-2">
                        <img src="${e.target.result}" class="img-thumbnail image-preview" style="max-height: 200px;">
                    </div>
                `);
                }.bind(this);
                reader.readAsDataURL(file);
            }
        }));

        // Form validation
        $('form').on('submit', function (e) {
            const form = $(this);
            if (form[0].checkValidity() === false) {
                e.preventDefault();
                e.stopPropagation();
            }
            form.addClass('was-validated');
        });
    }


    //static setupOrderViewButtons() {
    //    $(document).on('click', '.view-order-btn', function () {
    //        const orderId = $(this).data('order-id');
    //        const detailsRow = $(`#order-details-${orderId}`);
    //        const detailsContainer = detailsRow.find('.order-details-container');
    //        const button = $(this);

    //        if (detailsRow.is(':visible')) {
    //            detailsRow.slideUp();
    //            button.html('<i class="bi bi-eye"></i> View');
    //        } else {
    //            // Check if content is already loaded
    //            if (detailsContainer.is(':empty')) {
    //                // Load the order details via AJAX
    //                button.html('<i class="bi bi-hourglass"></i> Loading...');
    //                $.get(`/Admin/OrderDetailsPartial/${orderId}`, function (data) {
    //                    detailsContainer.html(data);
    //                    detailsRow.slideDown();
    //                    button.html('<i class="bi bi-eye-slash"></i> Hide');
    //                }).fail(function () {
    //                    button.html('<i class="bi bi-eye"></i> View');
    //                    appToast.show('Failed to load order details', 'error');
    //                });
    //            } else {
    //                detailsRow.slideToggle();
    //                button.html(detailsRow.is(':visible') ?
    //                    '<i class="bi bi-eye-slash"></i> Hide' :
    //                    '<i class="bi bi-eye"></i> View');
    //            }
    //        }
    //    });
    //}

}


// Initialize when document is ready
$(document).ready(function () {
    AdminDashboard.init();


    //Order Management
    $('.view-order-btn').on('click', function () {
        const orderId = $(this).data('order-id');
        const detailsRow = $(`#order-details-${orderId}`);
        const detailsContainer = detailsRow.find('.order-details-container');

        if (detailsRow.is(':visible')) {
            detailsRow.slideUp();
            $(this).html('<i class="bi bi-eye"></i> View');
        } else {
            // Check if content is already loaded
            if (detailsContainer.is(':empty')) {
                // Load the order details via AJAX
                $.get(`/Admin/OrderDetailsPartial/${orderId}`, function (data) {
                    detailsContainer.html(data);
                    detailsRow.slideDown();
                    $(this).html('<i class="bi bi-eye-slash"></i> Hide');
                });
            } else {
                detailsRow.slideToggle();
                $(this).html(detailsRow.is(':visible') ?
                    '<i class="bi bi-eye-slash"></i> Hide' :
                    '<i class="bi bi-eye"></i> View');
            }
        }
    });

    //Delete Order
    $('.delete-product').on('click', (function () {
        const productId = $(this).data('id');
        if (confirm('Are you sure you want to delete this product?')) {
            $.post('/Admin/DeleteProduct', { id: productId })
                .done(function () {
                    appToast.show('X Product Deleted Successfully !', 'success');
                    location.reload();
                })
                .fail(function () {
                    appToast.show('Error deleting product', 'error');
                });
        }
    }));
});