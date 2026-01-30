$(document).ready(function() {
    console.log('Customer page loaded');
    
    // Click customer to load details
    $('.customer-item').click(function() {
        var id = $(this).data('id');
        loadCustomer(id);
        
        // Highlight selected
        $('.customer-item').removeClass('active');
        $(this).addClass('active');
    });

    // New button
    $('#btnNew').click(function() {
        clearForm();
        $('#formTitle').text('New Customer');
    });

    // Clear button
    $('#btnClear').click(function() {
        clearForm();
    });

    // Save button
    $('#btnSave').click(function() {
        saveCustomer();
    });

    // Delete button
    $('#btnDelete').click(function() {
        deleteCustomer();
    });

    // Load customer details
    function loadCustomer(id) {
        $.ajax({
            url: '/Customer/GetCustomer',
            type: 'GET',
            data: { id: id },
            success: function(data) {
                $('#customerId').val(data.id);
                $('#name').val(data.name);
                $('#email').val(data.email);
                $('#phone').val(data.phone || '');
                
                if(data.birthday) {
                    var date = new Date(data.birthday);
                    var formatted = date.toISOString().split('T')[0];
                    $('#birthday').val(formatted);
                } else {
                    $('#birthday').val('');
                }
                
                $('#btnDelete').show();
                $('#formTitle').text('Edit Customer');
            },
            error: function(xhr) {
                alert('Error loading customer');
                console.error(xhr);
            }
        });
    }

    // Save customer (Create or Update)
    function saveCustomer() {
        var customerId = $('#customerId').val();
        var customer = {
            Id: customerId,
            Name: $('#name').val().trim(),
            Email: $('#email').val().trim(),
            Phone: $('#phone').val().trim(),
            Birthday: $('#birthday').val()
        };

        // Validation
        if (!customer.Name) {
            alert('Please enter a name');
            $('#name').focus();
            return;
        }
        if (!customer.Email) {
            alert('Please enter an email');
            $('#email').focus();
            return;
        }

        var url = customerId == '0' ? '/Customer/Create' : '/Customer/Update';

        $.ajax({
            url: url,
            type: 'POST',
            data: customer,
            success: function(response) {
                if (response.success) {
                    alert('Customer saved successfully!');
                    location.reload();
                } else {
                    alert('Error saving customer');
                }
            },
            error: function(xhr) {
                alert('Error saving customer');
                console.error(xhr);
            }
        });
    }

    // Delete customer
    function deleteCustomer() {
        if(confirm('Are you sure you want to delete this customer?')) {
            var id = $('#customerId').val();
            
            $.ajax({
                url: '/Customer/Delete',
                type: 'POST',
                data: { id: id },
                success: function(response) {
                    if (response.success) {
                        alert('Customer deleted successfully!');
                        location.reload();
                    } else {
                        alert('Error deleting customer');
                    }
                },
                error: function(xhr) {
                    alert('Error deleting customer');
                    console.error(xhr);
                }
            });
        }
    }

    // Clear form
    function clearForm() {
        $('#customerId').val('0');
        $('#name').val('');
        $('#email').val('');
        $('#phone').val('');
        $('#birthday').val('');
        $('#btnDelete').hide();
        $('#formTitle').text('Customer Details');
        $('.customer-item').removeClass('active');
    }
});
