


// Cart Operations
function updateCartItem(productId, quantity) {
    $.post('/Cart/UpdateCartItem', { productId, quantity })
        .done(function (response) {
            if (response.success) {
                appToast.show('Cart updated successfully', 'success');
                location.reload(); // Refresh to show updated cart
            } else {
                appToast.show(response.message || 'Error updating cart', 'error');
            }
        })
        .fail(function () {
            appToast.show('Error updating cart item', 'error');
        });
}

function removeFromCart(productId) {
    $.post('/Cart/RemoveFromCart', { productId })
        .done(function (response) {
            if (response.success) {
                appToast.show('Item removed from cart', 'success');
                if (response.itemCount !== 0) {
                    $('tr[data-product-id="' + productId + '"]').remove();
                    updateCartSummary(response.cart);
                    updateCartCount(response.itemCount);
                } else {
                    location.reload(); // Refresh to show empty cart
                }
            } else {
                appToast.show(response.message || 'Error removing item', 'error');
            }
        })
        .fail(function () {
            appToast.show('Error removing item from cart', 'error');
        });
}

function clearCart() {
    if (!confirm('Are you sure you want to clear your cart?')) return;

    $.post('/Cart/ClearCart')
        .done(function (response) {
            if (response.success) {
                appToast.show('Cart cleared successfully', 'success');
                location.reload(); // Refresh to show empty cart
            } else {
                appToast.show(response.message || 'Error clearing cart', 'error');
            }
        })
        .fail(function () {
            appToast.show('Error clearing cart', 'error');
        });
}

function updateCartSummary(cart) {
    // Update the cart summary section with new totals
    $('#subtotal').text('$' + cart.subtotal.toFixed(2));
    $('#tax').text('$' + cart.tax.toFixed(2));
    $('#shipping').text('$' + cart.shipping.toFixed(2));
    $('#total').text('$' + cart.total.toFixed(2));
}

function updateCartCount(count) {
    // Update the cart count in the navbar
    $('.cart-count').text(count);
}

// Checkout Handler
function handleCheckout() {
    const proceedBtn = document.getElementById('proceedToCheckoutBtn');
    if (!proceedBtn) return;

    const buttonText = proceedBtn.querySelector('.button-text');
    const buttonSpinner = proceedBtn.querySelector('.spinner-border');

    proceedBtn.addEventListener('click', async function () {
        // Show loading state
        proceedBtn.disabled = true;
        buttonText.textContent = 'Processing...';
        buttonSpinner.classList.remove('d-none');

        try {
            const response = await fetch('/Order/PlaceOrder', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                }
            });

            const data = await response.json();

            if (!response.ok) {
                throw new Error(data.message || 'Failed to place order');
            }

            if (data.success) {
                appToast.show('Order placed successfully! Redirecting...', 'success');
                setTimeout(() => {
                    window.location.href = '/Product/Index';
                }, 1500);
            } else {
                appToast.show(data.message || 'Could not complete your order', 'error');
            }
        } catch (error) {
            appToast.show(error.message || 'An error occurred during checkout', 'error');
            console.error('Checkout error:', error);
        } finally {
            // Reset button state
            proceedBtn.disabled = false;
            buttonText.textContent = 'Proceed to Checkout';
            buttonSpinner.classList.add('d-none');
        }
    });
}

// Document Ready
$(document).ready(function () {
    // Initialize checkout handler
    handleCheckout();

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
    $('#clear-cart').on('click', function (e) {
        e.preventDefault();
        clearCart();
    });
});