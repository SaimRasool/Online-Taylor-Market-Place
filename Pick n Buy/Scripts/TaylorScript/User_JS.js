function Login() {
    // SELECTING ALL ERROR DISPLAY ELEMENTS
    var email_error = document.getElementById('email_error');
    var password_error = document.getElementById('password_error');
    var Email = $('#Email').val();
    var password = $('#Password').val();
    if (Email == "admin@gmail.com" && password == "admin") {
        return window.location.replace('/Admin/Index');
    }
    var formdata = new FormData();
    if (Email == "") {
        document.getElementById('email_div').style.color = "red";
        email_error.innerHTML = '<div class="alert alert-danger"><strong>Email is required!</strong></div>';
        document.getElementById("Email").focus();
        return false;
    }
    else {
        document.getElementById('email_div').style.color = "black";
        email_error.innerHTML = '';
    }
    if (password == "") {
        document.getElementById('password_div').style.color = "red";
        password_error.innerHTML = '<div class="alert alert-danger"><strong>Password is required!</strong></div>';
        document.getElementById('Password').focus();
        return false;
    }
    else {
        document.getElementById('password_div').style.color = "black";
        password_error.innerHTML = '';
    }   
    formdata.append("Email", Email);
    formdata.append("Password", password);
    $.ajax({
        type: "POST",
        url: "/Home/SignIn",
        data: formdata,
        cache: false,
        contentType: false,
        processData: false,
        async: true,
        success: function (status) {
            window.location.replace('/Home/Index');
        },
        error: function () {
            document.getElementById('error').innerHTML = '<div class="alert alert-danger"><strong>Invalid User Name or Password!</strong></div>';
        }
        });
};



function CreateNewUser() {
    // SELECTING ALL ERROR DISPLAY ELEMENTS 
    var name_error = document.getElementById('name_error');
    var Cnfrm_password_error = document.getElementById('Cnfrm_password_error');
    var City_error = document.getElementById('city_error');
    var email_error = document.getElementById('eml_error');
    var password_error = document.getElementById('pwd_error');

    var User = $('#Unme').val();
    var password = $("#pwd").val();
    var Email = $("#eml").val();
    var City = $("#city").val();
    var cnfrmpassword = $("#cnfrmpwd").val();
    var formdata = new FormData();
    if (User == "") {
        document.getElementById('name_div').style.color = "red";
        name_error.innerHTML = '<div class="alert alert-danger"><strong>Name is required!</strong></div>';
        document.getElementById("name").focus();
        return false;
    }
    else if (User.length>25){
        document.getElementById('name_div').style.color = "red";
        name_error.innerHTML = '<div class="alert alert-danger"><strong>Your name is Not More than 25 Character Long!</strong></div>';
        document.getElementById("name").focus();
        return false;
    }
    else {
        document.getElementById('name_div').style.color = "black";
        name_error.innerHTML = '';
    }

    if (Email == "") {
        document.getElementById('eml_div').style.color = "red";
        email_error.innerHTML = '<div class="alert alert-danger"><strong>Email is required!</strong></div>';
        document.getElementById("eml").focus();
        return false;
    }
    else if (Email.length > 50) {
        document.getElementById('eml_div').style.color = "red";
        email_error.innerHTML = '<div class="alert alert-danger"><strong>You Email is not more than 50 Character!</strong></div>';
        document.getElementById("eml").focus();
        return false;
    }
    else {
        document.getElementById('email_div').style.color = "black";
        email_error.innerHTML = '';
    }

    if (City == "") {
        document.getElementById('city_div').style.color = "red";
        City_error.innerHTML = '<div class="alert alert-danger"><strong>City is required!</strong></div>';
        document.getElementById("city").focus();
        return false;
    }
    else if (City.length > 25) {
        document.getElementById('city_div').style.color = "red";
        City_error.innerHTML = '<div class="alert alert-danger"><strong>City name is Not More than 25 Character Long!</strong></div>';
        document.getElementById("city").focus();
        return false;
    }
    else {
        document.getElementById('city_div').style.color = "black";
        City_error.innerHTML = '';
    }

    if (password == "") {
        document.getElementById('pwd_div').style.color = "red";
        password_error.innerHTML = '<div class="alert alert-danger"><strong>Password is required!</strong></div>';
        document.getElementById('pwd').focus();
        return false;
    }
    else
        if (password.length < 8 ) {
            document.getElementById('pwd_div').style.color = "red";
            password_error.innerHTML = '<div class="alert alert-danger"><strong>Password Shoud be Greater than 8 Character!</strong></div>';
            document.getElementById('pwd').focus();
            return false;
        }
        else if (cnfrmpassword == "") {
            document.getElementById('pwd_div').style.color = "black";
            password_error.innerHTML = '';
            document.getElementById('cnfrmpwd_div').style.color = "red";
            Cnfrm_password_error.innerHTML = '<div class="alert alert-danger"><strong>Please Confirm The Password!</strong></div>';
            document.getElementById('Cnfrm_password_error').focus();
            return false;
        }
        else if (cnfrmpassword != password) {
            password_error.innerHTML = '';
            document.getElementById('cnfrmpwd_div').style.color = "red";
            document.getElementById('pwd_div').style.color = "red";
            Cnfrm_password_error.innerHTML = '<div class="alert alert-danger"><strong>Password Does not Match!</strong></div>';
            document.getElementById('Cnfrm_password_error').focus();
            return false;
        }
    else {
        document.getElementById('cnfrmpwd_div').style.color = "black";
        Cnfrm_password_error.innerHTML = '';
    }
    formdata.append("Email", Email);
    formdata.append("City", City);
    formdata.append("Name", User);
    formdata.append("Password", password);
    $.ajax({
        type: "POST",
        url: "/Home/SignUp",
        data: formdata,
        cache: false,
        contentType: false,
        processData: false,
        async: true,
        success: function (status) {
            window.location.replace('/Home/Index');
        },
        error: function (error) {
            if (error.status == 404)
                swal({
                    title: "Email Already Exist!",
                    text: "This Email is Already Exist Please Enter Diffrent Email!",
                    type: "error",
                });
            else
            document.getElementById('error').innerHTML = '<div class="alert alert-danger"><strong>Some thing happens not SignUp Correctly!</strong></div>';
        }
    });
}

function ChangePassword() {
    var formdata = new FormData();
    var password_error = document.getElementById('pwd_error');
    var Cnfrm_password_error = document.getElementById('Cnfrm_password_error');
    var cnfrmpassword = $("#cnfrmpwd").val();
    var password = $("#pwd").val();
    if (password == "") {
        document.getElementById('pwd_div').style.color = "red";
        password_error.innerHTML = '<div class="alert alert-danger"><strong>Password is required!</strong></div>';
        document.getElementById('pwd').focus();
        return false;
    }
    else
        if (password.length < 8) {
            document.getElementById('pwd_div').style.color = "red";
            password_error.innerHTML = '<div class="alert alert-danger"><strong>Password Shoud be Greater than 8 Character!</strong></div>';
            document.getElementById('pwd').focus();
            return false;
        }
        else if (cnfrmpassword == "") {
            document.getElementById('pwd_div').style.color = "black";
            password_error.innerHTML = '';
            document.getElementById('cnfrmpwd_div').style.color = "red";
            Cnfrm_password_error.innerHTML = '<div class="alert alert-danger"><strong>Please Confirm The Password!</strong></div>';
            document.getElementById('Cnfrm_password_error').focus();
            return false;
        }
        else if (cnfrmpassword != password) {
            password_error.innerHTML = '';
            document.getElementById('cnfrmpwd_div').style.color = "red";
            document.getElementById('pwd_div').style.color = "red";
            Cnfrm_password_error.innerHTML = '<div class="alert alert-danger"><strong>Password Does not Match!</strong></div>';
            document.getElementById('Cnfrm_password_error').focus();
            return false;
        }
        else {
            document.getElementById('cnfrmpwd_div').style.color = "black";
            Cnfrm_password_error.innerHTML = '';
        }
    formdata.append("Password", password);
    $.ajax({
        type: "POST",
        url: "/Account/Resetpassword",
        data: formdata,
        cache: false,
        contentType: false,
        processData: false,
        async: true,
        success: function (status) {
            window.location.replace('/Home/Index');
        },
        error: function (error) {
                swal({
                    title: "Error!",
                    text: "Error Are Occur Password Does not Change!",
                    type: "error",
                });
        }
    });
}