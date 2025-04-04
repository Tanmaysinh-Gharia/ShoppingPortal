// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
class AppToast {
    constructor() {
        this.toastEl = document.getElementById('appToast');
        this.toast = new bootstrap.Toast(this.toastEl);
        this.toastMessage = this.toastEl.querySelector('.toast-message');
        this.toastIcon = this.toastEl.querySelector('.toast-icon');
    }

    show(message, type = 'success') {
        // Clear previous classes
        this.toastEl.classList.remove('bg-success', 'bg-danger', 'bg-warning', 'bg-info');
        this.toastIcon.className = 'toast-icon me-2';

        // Set new classes based on type
        switch (type) {
            case 'success':
                this.toastEl.classList.add('bg-success');
                this.toastIcon.classList.add('bi', 'bi-check-circle-fill');
                break;
            case 'error':
                this.toastEl.classList.add('bg-danger');
                this.toastIcon.classList.add('bi', 'bi-exclamation-triangle-fill');
                break;
            case 'warning':
                this.toastEl.classList.add('bg-warning');
                this.toastIcon.classList.add('bi', 'bi-exclamation-circle-fill');
                break;
            case 'info':
                this.toastEl.classList.add('bg-info');
                this.toastIcon.classList.add('bi', 'bi-info-circle-fill');
                break;
        }

        this.toastMessage.textContent = message;
        this.toast.show();
    }
}

// Initialize toast system
const appToast = new AppToast();