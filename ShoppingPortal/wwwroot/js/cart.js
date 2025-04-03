// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
class ShoppingCart {
    constructor() {
        this.cartItems = [];
        this.loadCart();
    }

    // Load cart from server
    async loadCart() {
        try {
            const response = await $.ajax({
                url: '/api/Cart',
                method: 'GET',
                dataType: 'json'
            });
            this.cartItems = response.items || [];
            this.updateUI();
        } catch (error) {
            console.error("Error loading cart:", error);
        }
    }

    // Add item to cart
    async addItem(productId, quantity) {
        try {
            const response = await $.ajax({
                url: '/api/Cart/AddItem',
                method: 'POST',
                contentType: 'application/json',
                data: JSON.stringify({ productId, quantity }),
                dataType: 'json'
            });

            this.cartItems = response.items;
            this.updateUI();
            return true;
        } catch (error) {
            console.error("Error adding item:", error);
            return false;
        }
    }

    // Update cart UI
    updateUI() {
        // Update cart count
        $('#cart-count').text(this.cartItems.reduce((sum, item) => sum + item.quantity, 0));

        // Update cart dropdown or modal
        const $cartDropdown = $('#cart-dropdown');
        $cartDropdown.empty();

        this.cartItems.forEach(item => {
            $cartDropdown.append(`
                <div class="cart-item" data-id="${item.productId}">
                    <span>${item.productName}</span>
                    <span>Qty: ${item.quantity}</span>
                    <button class="remove-item">×</button>
                </div>
            `);
        });
    }
}

// Initialize cart when DOM is ready
$(document).ready(function () {
    window.shoppingCart = new ShoppingCart();
});