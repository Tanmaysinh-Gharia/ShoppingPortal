function updateCartItem(productId, quantity) {
    $.post('/Cart/UpdateCartItem', { productId, quantity })
        .done(function (response) {
            if (response.success) {
                location.reload(); // Refresh to show updated cart
            } else {
                alert(response.message);
            }
        })
        .fail(function () {
            alert('Error updating cart item');
        });
}

function removeFromCart(productId) {
    $.post('/Cart/RemoveFromCart', { productId })
        .done(function (response) {
            if (response.success) {
                location.reload(); // Refresh to show empty cart
                if (response.itemCount != 0) {
                    $('tr[data-product-id="' + productId + '"]').remove();
                    updateCartSummary(response.cart);
                    updateCartCount(response.itemCount);
                }
            } else {
                alert(response.message);
            }
        })
        .fail(function () {
            alert('Error removing item from cart');
        });
}

function clearCart() {
    $.post('/Cart/ClearCart')
        .done(function (response) {
            if (response.success) {
                location.reload(); // Refresh to show empty cart
            } else {
                alert(response.message);
            }
        })
        .fail(function () {
            alert('Error clearing cart');
        });
}

function updateCartSummary(cart) {
    // Update the cart summary section with new totals
    // This would need to be implemented based on your cart summary structure
}

function updateCartCount(count) {
    // Update the cart count in the navbar
    $('.cart-count').text(count);
}




$(document).ready(function () {

    // Update quantity
    $('.quantity-input').on('change', function () {
        const productId = $(this).closest('tr').data('product-id');
        const newQuantity = parseInt($(this).val());

        if (newQuantity > 0) {
            updateCartItem(productId, newQuantity);
        } else {
            $(this).val(1);
        }
    });

    // Increase quantity
    $('.increase-quantity').on('click', function () {
        const input = $(this).siblings('.quantity-input');
        const newVal = parseInt(input.val()) + 1;
        input.val(newVal).trigger('change');
    });

    // Decrease quantity
    $('.decrease-quantity').on('click', function () {
        const input = $(this).siblings('.quantity-input');
        const newVal = parseInt(input.val()) - 1;
        if (newVal >= 1) {
            input.val(newVal).trigger('change');
        }
    });

    // Remove item
    $('.remove-item').on('click', function () {
        const productId = $(this).closest('tr').data('product-id');
        removeFromCart(productId);
    });

    // Clear cart
    $('#clear-cart').on('click', function () {
        if (confirm('Are you sure you want to clear your cart?')) {
            clearCart();
        }
    });
});