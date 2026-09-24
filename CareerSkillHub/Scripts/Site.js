
function confirmDelete(message) {
    return window.confirm(message || 'Are you sure you want to delete this?');
}

// Validate the registration form on the client before it is posted.
function validateRegistration(form) {
    var fullName = document.getElementById('txtFullName');
    var email = document.getElementById('txtEmail');
    var password = document.getElementById('txtPassword');
    var confirm = document.getElementById('txtConfirmPassword');
    var errorBox = document.getElementById('clientError');

    var errors = [];
    if (fullName && fullName.value.trim() === '') errors.push('Full name is required.');
    if (email && !/^[^@\s]+@[^@\s]+\.[^@\s]+$/.test(email.value.trim())) errors.push('A valid email address is required.');
    if (password && password.value.length < 6) errors.push('Password must be at least 6 characters.');
    if (password && confirm && password.value !== confirm.value) errors.push('Passwords do not match.');

    if (errors.length > 0) {
        if (errorBox) {
            errorBox.textContent = errors.join(' ');
            errorBox.style.display = 'block';
        }
        return false;
    }
    return true;
}

// Toggle visibility of password fields (show/hide password).
function togglePassword(checkboxId, passwordId) {
    var box = document.getElementById(checkboxId);
    var field = document.getElementById(passwordId);
    if (box && field) {
        field.type = box.checked ? 'text' : 'password';
    }
}

// Live client-side feedback while choosing a rating value.
function showRating(value) {
    var label = document.getElementById('ratingLabel');
    if (label) label.textContent = 'Rating: ' + value + ' / 5';
}

// Small helper: confirm before an <a> based delete link is followed.
function registerConfirm(linkId, message) {
    var el = document.getElementById(linkId);
    if (el) {
        el.onclick = function (e) {
            if (!confirmDelete(message)) {
                e.preventDefault();
                return false;
            }
            return true;
        };
    }
}