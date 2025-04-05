document.addEventListener('DOMContentLoaded', function () {
    // Load more orders
    $('#load-more').on('click', function () {
        const button = $(this);
        const nextPage = parseInt(button.data('current-page')) + 1;
        const totalPages = parseInt(button.data('total-pages'));

        button.prop('disabled', true);
        button.html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...');

        $.get(`/Order/Index?page=${nextPage}`, function (data) {
            const newOrders = $(data).find('#orders-container').html();
            $('#orders-container').append(newOrders);

            button.data('current-page', nextPage);
            if (nextPage >= totalPages) {
                button.hide();
            }
        })
            .always(function () {
                button.prop('disabled', false);
                button.text('Load More Orders');
            });
    });

    // Cancel order
    $(document).on('click', '.cancel-order', function () {
        const button = $(this);
        const orderId = button.data('order-id');
        const orderCard = button.closest('.order-card');

        if (!confirm('Are you sure you want to cancel this order?')) {
            return;
        }

        button.prop('disabled', true);
        button.html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Processing...');

        $.post('/Order/CancelOrder', { orderId: orderId })
            .done(function (response) {
                if (response.success) {
                    appToast.show('X Order cancelled successfully', 'success');
                    orderCard.find('.badge')
                        .removeClass('bg-warning')
                        .addClass('bg-secondary')
                        .text('Cancelled');
                    button.remove();
                } else {
                    appToast.show(response.message || 'Failed to cancel order', 'error');
                }
            })
            .fail(function () {
                appToast.show('Error cancelling order', 'error');
            })
            .always(function () {
                button.prop('disabled', false);
                button.text('Cancel Order');
            });
    });
});