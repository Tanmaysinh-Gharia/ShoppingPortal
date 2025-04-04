function showInCartControls(productId) {
    $(`[data-product-id="${productId}"] .cart-controls-not-in-cart`).hide();
    $(`[data-product-id="${productId}"] .cart-controls-in-cart`).show();
}

function showNotInCartControls(productId) {
    $(`[data-product-id="${productId}"] .cart-controls-in-cart`).hide();
    $(`[data-product-id="${productId}"] .cart-controls-not-in-cart`).show();
}


$(document).ready(function () {
    // Add to cart
    $(document).on('click', '.add-to-cart', function () {
        const productId = $(this).data('product-id');
        const quantity = 1; // Default quantity

        $.post('/Cart/AddToCart', {
            ProductId: productId,
            Quantity: quantity
        })
            .done(function (response) {
                if (response.success) {
                    // Update UI
                    showInCartControls(productId);
                    const card = $(`[data-product-id="${productId}"]`).closest('.card');
                    card.find('.cart-controls-in-cart').show();
                    card.find('.cart-controls-not-in-cart').hide();
                    card.find('.quantity-input').val(quantity);

                    // Update cart count
                    $('.cart-count').text(response.itemCount);
                }
            })
            .fail(function () {
                alert('Error adding to cart');
            });
    });

    // Remove from cart
    $(document).on('click', '.remove-from-cart', function () {
        const productId = $(this).data('product-id');

        $.post('/Cart/RemoveFromCart', {
            productId: productId
        })
            .done(function (response) {
                if (response.success) {
                    // Update UI

                    showNotInCartControls(productId);
                    const card = $(`[data-product-id="${productId}"]`).closest('.card');
                    //card.find('.add-to-cart').show();
                    //card.find('.input-group').hide();
                    //card.find('.remove-from-cart').hide();
                    card.find('.cart-controls-in-cart').hide();
                    card.find('.cart-controls-not-in-cart').show();

                    // Update cart count
                    $('.cart-count').text(response.itemCount);
                }
            })
            .fail(function () {
                alert('Error removing from cart');
            });
    });

    // Update quantity
    $(document).on('change', '.quantity-input', function () {
        const productId = $(this).data('product-id');
        const quantity = $(this).val();

        $.post('/Cart/UpdateCartItem', {
            ProductId: productId,
            Quantity: quantity
        })
            .fail(function () {
                alert('Error updating quantity');
            });
    });

    // Quantity buttons
    $(document).on('click', '.increase-quantity', function () {
        const input = $(this).siblings('.quantity-input');
        const newVal = parseInt(input.val()) + 1;
        input.val(newVal).trigger('change');
    });

    $(document).on('click', '.decrease-quantity', function () {
        const input = $(this).siblings('.quantity-input');
        const newVal = parseInt(input.val()) - 1;
        if (newVal >= 1) {
            input.val(newVal).trigger('change');
        }
    });


    // Quantity change handler
    $(document).on('change', '.quantity-input', function () {
        const productId = $(this).data('product-id');
        const newQuantity = parseInt($(this).val());

        // Validate quantity
        if (newQuantity < 1 || newQuantity > 10) {
            $(this).val($(this).data('prev-value') || 1);
            return;
        }

        $.post('/Cart/UpdateCartItem', {
            ProductId: productId,
            Quantity: newQuantity
        }).fail(function () {
            alert('Error updating quantity');
            $(this).val($(this).data('prev-value') || 1);
        });
    });

    // Store previous value before change
    $(document).on('focus', '.quantity-input', function () {
        $(this).data('prev-value', $(this).val());
    });

    // Place order
    $(document).on('click', '.place-order', function () {
        const productId = $(this).data('product-id');

        if (confirm('Are you sure you want to place an order for this item?')) {
            $.post('/Cart/PlaceOrder', {
                productId: productId
            })
                .done(function (response) {
                    if (response.success) {
                        alert('Order placed successfully!');
                        // Optionally redirect to orders page
                        window.location.href = '/Orders';
                    }
                })
                .fail(function () {
                    alert('Error placing order');
                });
        }
    });
});