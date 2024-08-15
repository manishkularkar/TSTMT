$(document).ready(function () {
    DepartmentList();   // For List View  

});

var SaveDepart = function () {
    //debugger;
    var Department_id = $("#hdnId").val();
    var Department_name = $("#txtDepartment").val();



    if (Department_name == "") {
        alert("Please Enter Department  Name");
        $("#txtDepartment").focus();
        return false;
    } 

    var model = {
        Department_id: Department_id, Department_name: Department_name
    };
    $.ajax({
        url: "/Department/SaveDepart",
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json;charset=utf-8",
        dataType: "json", // Corrected 'datatype' to 'dataType'

        success: function (response) {
            //debugger;
            Swal.fire({
                position: "top-end",
                icon: "success",
                title: "Department Submited",
                showConfirmButton: false,
                timer: 1500
            });
            alert("Department Submited");
        },

    });
    DepartmentList();
}

// list
var DepartmentList = function () {  
    //debugger;
    var Search = $("#txtSearch").val();
    var model = {
        Search: Search
    }
    $.ajax({
        url: "/Department/DepartmentList",
        method: "post",
        data: JSON.stringify(Search),
        contentType: "application/json;charset=utf-8",
        dataType: "json",        // for search l liye
        async: false,
        success: function (responce) {
            //debugger;
            var html = "";
            $("#Department_mast tbody").empty();
            $.each(responce.model, function (Index, elementValue) {

                html += "<tr><td>" + elementValue.Department_id +
                    "</td><td>" + elementValue.Department_name +

                    //"</td><td><button type='button' class='btn btn-primary btn-sm' onclick='EditDepartment(" + elementValue.Department_id + ")'><i class='bi bi-pencil-square' aria-hidden='true'></i></button>&nbsp;&nbsp;<button type='button' class='btn btn-info btn-sm' onclick='DetailDepartment(" + elementValue.Department_id + ")'><i class='bi bi-eye-fill' aria-hidden='true'></i></button> &nbsp;&nbsp;<button type='button' class='btn btn-danger btn-sm' onclick='DeleteDepartment(" + elementValue.Department_id + ")'><i class='bi bi-trash-fill' aria-hidden='true'></i></button></td></tr>";

                    "</td><td><button type='button' class='btn btn-primary'onclick='EditDepartment(" + elementValue.Department_id + ")'>Edit</button></td><td><button type='button' class='btn btn-danger'onclick='DeleteDepartment(" + elementValue.Department_id + ")'>Delete</button></td><td><button type='button'class='btn btn-primary'onclick='DetailDepartment(" + elementValue.Department_id + ")'>Detail</button></td></tr>";
            });

            $("#Department_mast tbody").append(html);
        }
    })
};

// Delete
var DeleteDepartment = function (Department_id) {
    //debugger;
    var confirmed = confirm("Are you sure you want to delete?");
    if (!confirmed) {
        alert("Deletion canceled.");
        return;
    }
    model = { Department_id: Department_id },
        $.ajax({
            url: "/Department/DeleteDepartment",
            method: "Post",
            data: JSON.stringify(model),
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (response) {
                Swal.fire({
                    title: 'Delete!',
                    text: 'Data Delete successfully.',
                    icon: 'success'
                });
            },
        });
    DepartmentList();
}


// Edit
var EditDepartment = function (Department_id) {
    //debugger;
    var model = { Department_id: Department_id };
    $.ajax({
        url: "/Department/EditDepartment",
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            $("#hdnId").val(response.model.Department_id);
            $("#txtDepartment").val(response.model.Department_name);

        }
    });
}


// Detail
var DetailDepartment = function (Department_id) {

    //debugger;
    var model = { Department_id: Department_id }
    $.ajax({
        url: "/Department/DetailDepartment",
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            $("#CategoryModal").modal('show');

            $("#Department_Detail").empty();

            var html = "";
            html += "<div class='row'>";
            html += "<div class='col-sm-6'>";
            html += "<b>ItemId:</b>&nbsp&nbsp&nbsp<span>" + response.model.Department_id + "</span>";
            html += "</br>";
            html += "<b>ItemDescr:</b>&nbsp&nbsp&nbsp<span>" + response.model.Department_name + "</span>";
            html += "</br>";
            html += "</div>";
            html += "</div>";

            $("#Department_Detail").append(html);
        }
    });
};
 