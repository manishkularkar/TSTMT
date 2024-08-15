  
$(document).ready(function () {

    TransactionList();
    ddlDepartment();
    ddlVendor();
    ddlItemId();
    ddlItemQty();
    

});


// DDl Item id n name fetch
var ddlItemId = function () {
   
    $.ajax({
        url: "/Transaction/ddlItemId",
        method: "post",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: false,
        success: function (responce) {
            var html = "";
            $("#ddlItemId").empty();
            $.each(responce.model, function (Index, elementValue) {

                html += "<option value=" + elementValue.Item_id + ">" + elementValue.Item_name + "</option>";

            });

            $("#ddlItemId").append(html);

        }

    })
};
var ddlOnChange = function () {
    ddlItemQty();
}

// Item qty fhetch

var ddlItemQty = function () {

    //debugger;

    $.ajax({
        url: "/Transaction/ddlItemQty?Item_id=" + $("#ddlItemId").val(),
        method: "post",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: false,
        success: function (responce) {
            var html = "";
            $("#ddlItemQty").empty();
            $.each(responce.model, function (Index, elementValue) {

                html += "<option>" + elementValue.Balance_quantity + "</option>";
            });
            $("#ddlItemQty").append(html);
        }

    })
};
// ddl departmebt
var ddlDepartment = function () {

    //debugger;

    $.ajax({
        url: "/Transaction/ddlDepartment",
        method: "post",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: false,
        success: function (responce) {
            var html = "";
            $("#ddlDepart_Id").empty();
            html += "<option>Select</option>";
            $.each(responce.model, function (Index, elementValue) {
                html += "<option value=" + elementValue.Department_id + ">" + elementValue.Department_name + "</option>";

            });

            $("#ddlDepart_Id").append(html);
        }

    })
};

// ddl vendor
var ddlVendor = function () {
    //debugger;

    $.ajax({
        url: "/Transaction/ddlVendor",
        method: "post",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: false,
        success: function (responce) {
            var html = "";
            $("#ddlVendor_Name").empty();
            html += "<option value = NULL >Select</option>";
            $.each(responce.model, function (Index, elementValue) {
               
                html += "<option value=" + elementValue.Vendor_id + ">" + elementValue.Vendor_name + "</option>";

            });

            $("#ddlVendor_Name").append(html);
        }

    })
};


// save n update
var SaveTrans = function () {

    //debugger;

    var Transaction_id = $("#hdnId").val();
    var Item_id = $("#ddlItemId").val();
    var Department_id = $("#ddlDepart_Id").val();
    var Vendor_id = $("#ddlVendor_Name").val();
    var ItemQtyTotal = $("#txtTotalQty").val();

    var Quantity = $("#txtTransQty").val();
    var Balance_quantity = $("#ddlItemQty").val();

    if (Quantity == "") {
        alert("Please Enter Transaction Qty");
        $("#txtTransQty").focus();
        return false;
    }
    if ($('#rdoIssue').prop('checked') == false && ($('#rdoReceived').prop('checked') == false)) {
        alert("Please Select One of The Transaction Type");
        return false;
    }

    let TransType = "";
    if ($('#rdoIssue').prop('checked') == true) {
        //debugger;
        if (parseFloat(Balance_quantity) == 0) {
            alert("Items unavailable in ITem Stock");
        }
        else
            if ((parseFloat(Balance_quantity) < parseFloat(Quantity))) {
            alert("Available quantity is less than required");
        }
        else {
            ItemQtyTotal = Balance_quantity - Quantity;
            TransType = "Issue";
        }

    }

    else if ($('#rdoReceived').prop('checked') == true) {
        if (parseFloat(Balance_quantity) == 0) {
            alert("Items unavailable in ITem Stock");
            return;
        }
        else if ((parseFloat(Balance_quantity) < parseFloat(Quantity))) {
            alert("Available quantity is less than require");
            return;
        }
        else {
            ItemQtyTotal = Balance_quantity - Quantity;
            TransType = "Purchase";
        }
    }
    var model = { Transaction_id: Transaction_id, Item_id: Item_id, TransType: TransType, Quantity: Quantity, ItemQtyTotal: ItemQtyTotal, Vendor_id: Vendor_id, Department_id: Department_id }

    $.ajax({
        url: "/Transaction/SaveTrans",  // controller name / methode name
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (response)
        {
            alert("Transaction Save Successfully");
        },

    });
    TransactionList();
}

// list
var TransactionList = function () {
  
    var Search = $("#txtSearch").val();
    var model = {
        Search: Search
    }
    //debugger;
    $.ajax({
        url: "/Transaction/TransactionList",
        method: "post",
        data: JSON.stringify(Search),
        contentType: "application/json;charset=utf-8",
        dataType: "json",        // for search l liye
        async: false,

        success: function (responce) {
            
            var html = "";
            $("#Item_Transaction tbody").empty();
            $.each(responce.model, function (Index, elementValue) {
                /*html += "<tr><td style.display = 'none'>" + elementValue.Transaction_id +*/
                html += "<tr><td >" + elementValue.Transaction_id +
                    "<tr><td >" + elementValue.Item_name +
                    "</td><td>" + elementValue.Transaction_date +
                    "</td><td>" + elementValue.Department_name +
                    "</td><td>" + elementValue.Vendor_name +
                    "</td><td>" + elementValue.Balance_quantity +
                    "</td><td>" + elementValue.Quantity +
                    "</td><td>" + elementValue.TransType +

                    "</td><td></button>&nbsp;&nbsp;<button type='button' class='btn btn-info btn-sm' onclick='DetailTransaction(" + elementValue.Transaction_id + ")'><i class='bi bi-eye-fill' aria-hidden='true'></i></button> &nbsp;&nbsp;<button type='button' class='btn btn-danger btn-sm' onclick='DeleteTransaction(" + elementValue.Transaction_id + ")'><i class='bi bi-trash-fill' aria-hidden='true'></i></button></td></tr>";

            });

            $("#Item_Transaction tbody").append(html);
        }
    })
};
 
// Delete
var DeleteTransaction = function (Transaction_id) {
     //debugger;
    var confirmed = confirm("Are you sure you want to delete?");
    if (!confirmed) {
        alert("Deletion canceled.");
        return;
    }
    model = { Transaction_id: Transaction_id },
        $.ajax({
            url: "/Transaction/DeleteTransaction",
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
    TransactionList();
}

 

// Detail
var DetailTransaction = function (Transaction_id) {

   // debugger;
    var model = { Transaction_id: Transaction_id }
    $.ajax({
        url: "/Transaction/DetailTransaction",
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            $("#CategoryModal").modal('show');

            $("#DetailCategory").empty();

            var html = "";
            html += "<div class='row'>";
            html += "<div class='col-sm-6'>";
            html += "<b>transaction id:</b>&nbsp&nbsp&nbsp<span>" + response.model.Transaction_id + "</span>";
            html += "</br>";
            html += "<b>item Id:</b>&nbsp&nbsp&nbsp<span>" + response.model.Item_id + "</span>";
            html += "</br>";
            html += "<b>transaction Date:</b>&nbsp&nbsp&nbsp<span>" + response.model.Transaction_date + "</span>";
            html += "</br>";
            html += "<b>Department Id:</b>&nbsp&nbsp&nbsp<span>" + response.model.Department_id + "</span>";
            html += "</br>";
            html += "<b>Vendor Id:</b>&nbsp&nbsp&nbsp<span>" + response.model.Vendor_id + "</span>";
            html += "</br>";
            html += "<b>Quantuty:</b>&nbsp&nbsp&nbsp<span>" + response.model.Quantity + "</span>";
            html += "</br>";
            html += "<b>Transaction Type:</b>&nbsp&nbsp&nbsp<span>" + response.model.TransType + "</span>";
            html += "</br>";
            html += "</div>";
            html += "</div>";

            $("#DetailCategory").append(html);
        }
    });
};


var toggleVisibility = function () {
    //console.log("toggleVisibility function called");

    var issueRadio = document.getElementById('rdoIssue').checked;
    var purchaseRadio = document.getElementById('rdoReceived').checked;

    var departmentDiv = document.getElementById('departmentDiv');
    var vendorDiv = document.getElementById('vendorDiv');

    if (issueRadio) {
      //  console.log("Issue radio checked");
        vendorDiv.style.display = 'none';
        departmentDiv.style.display = 'block';
    } else if (purchaseRadio) {
      //  console.log("Purchase radio checked");
        departmentDiv.style.display = 'none';
        vendorDiv.style.display = 'block';
    } else {
       // console.log("No radio button checked");
    }
}

// Ensure visibility is set correctly on page load
document.addEventListener('DOMContentLoaded', function () {
    console.log("DOMContentLoaded event fired");
    toggleVisibility();
});