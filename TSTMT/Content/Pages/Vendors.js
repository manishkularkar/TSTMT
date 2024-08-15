$(document).ready(function () { 
    VendorList();   // For List View  

});


var SaveVendor = function () {
    //debugger;

    var Vendor_id = $("#hdnId").val();
    var Vendor_name = $("#txtVendor").val();

    if (Vendor_name == "") {
        alert("Please Enter Vendor Name");
        $("#txtVendor").focus();
        return false;
    }

    var model = {
        Vendor_id: Vendor_id, Vendor_name: Vendor_name
    };
    $.ajax({
        url: "/Vendor/SaveVendor",
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json;charset=utf-8",
        dataType: "json", // Corrected 'datatype' to 'dataType'
        success: function (response) {

            Swal.fire({
                title: 'Submited!',
                text: 'Data Save successfully.',
                icon: 'success'
            });
            alert("Vendor Data Save Successfully");

            VendorList();
        },

    });
    
}

// list
var VendorList = function () {
    //debugger;
    var Search = $("#txtSearch").val();
    var model = {
        Search: Search
    }
    $.ajax({
        url: "/Vendor/VendorList",
        method: "post",
        data: JSON.stringify(Search),
        contentType: "application/json;charset=utf-8",
        dataType: "json",        // for search l liye
        async: false,
        success: function (responce) {
            //debugger;
            var html = "";
            $("#Vendor_mast tbody").empty();
            $.each(responce.model, function (Index, elementValue) {
                html += "<tr><td>" + elementValue.Vendor_id +
                    "</td><td>" + elementValue.Vendor_name +
                    "</td><td><button type='button' class='btn btn-danger'onclick='EditVendor(" + elementValue.Vendor_id + ")'>Edit</button></td><td><button type='button' class='btn btn-danger'onclick='DeleteVendor(" + elementValue.Vendor_id + ")'>Delete</button></td><td><button type='button'class='btn btn-primary'onclick='DetailVendor(" + elementValue.Vendor_id + ")'>Detail</button></td></tr>";

});

            $("#Vendor_mast tbody").append(html);
        }
    })
};


// Delete
var DeleteVendor = function (Vendor_id) {
   // debugger;
    var confirmed = confirm("Are you sure you want to delete?");
    if (!confirmed) {
        alert("Deletion canceled.");
        return;
    }
    model = { Vendor_id: Vendor_id },
        $.ajax({
            url: "/Vendor/DeleteVendor",
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
                VendorList();
            },
        });
    
    //location.reload();
}



// Detail
var DetailVendor = function (Vendor_id) {

    //debugger;
    var model = { Vendor_id: Vendor_id }
    $.ajax({
        url: "/Vendor/DetailVendor",
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            $("#CategoryModal").modal('show');
            $("#Vendor_mast").empty();

            var html = "";
            html += "<div class='row'>";
            html += "<div class='col-sm-6'>";
            html += "<b>ItemId:</b>&nbsp&nbsp&nbsp<span>" + response.model.Vendor_id + "</span>";
            html += "</br>";
            html += "<b>ItemDescr:</b>&nbsp&nbsp&nbsp<span>" + response.model.Vendor_name + "</span>";
            html += "</br>";
            html += "</div>";
            html += "</div>";
            $("#Vendor_mast").append(html);
        }
    });
};


// Edit
var EditVendor = function (Vendor_id)
{
    //debugger;
    var model = { Vendor_id: Vendor_id };
    $.ajax({
        url: "/Vendor/EditVendor",
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response)
        {
            $("#hdnId").val(response.model.Vendor_id);
            $("#txtVendor").val(response.model.Vendor_name);

            VendorList();

        }
    });
}
