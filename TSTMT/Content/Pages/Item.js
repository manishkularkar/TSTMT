$(document).ready(function () {
    ItemList();
});
var SaveItem = function () {

    var Item_id = $("#hdnId").val();

    var Item_name = $("#txtItem_Name").val();
    var Category = $("#ddlCate").val();
    var Rate = parseFloat($("#txtRate").val());
    var Balance_quantity = $("#txtBal_Qty").val();

    if (Item_name == "") {
        alert("Please Enter Item Name");
        $("#txtItem_Name").focus();
        return false;
    }
    else if (Category == "") {
        alert("Please Select Category");
        $("#txtCategory").focus();
        return false;
    }

    else if (Rate == "") {
        alert("Please Enter Class Name");
        $("#txtRate").focus();
        return false;
    }
    else if (Balance_quantity == "") {
        alert("Please Enter Balance Quantity");
        $("#txtBal_Qty").focus();
        return false;
    }

    var model = {
        Item_id: Item_id,
        Item_name: Item_name,
        Category: Category,
        Rate: Rate,
        Balance_quantity: Balance_quantity
    };
    //debugger;
    $.ajax({

        url: "/Item/SaveItem",
        method: "Post",
        data: JSON.stringify(model),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (response)
        {
            alert("Save successfully");
            location.reload();            
            ItemList();            
        },
        error: function (response) {
            alert(response.model);
}
    });
}


// List View //
var ItemList = function () {

    var Search = $("#txtSearch").val();
    var model = {
        Search: Search
    }
    $.ajax({
        url: "/Item/ItemList",
        method: "post",
        data: JSON.stringify(model),//Search
        contentType: "application/json;charset=utf-8",
        dataType: "json",        // for search l liye
        async: false,
        success: function (responce) {
           
            var html = "";
            $("#Item_master tbody").empty();
            $.each(responce.model, function (Index, elementValue) {

                html += "<tr><td>" + elementValue.Item_id +
                    "</td><td>" + elementValue.Item_name +
                    "</td><td>" + elementValue.Category +
                    "</td><td>" + elementValue.Rate +
                    "</td><td>" + elementValue.Balance_quantity +

                //    //"</td><td><input type='button' value='edit' onclick='EditItem(" + elementValue.Item_id + ")'/></td > <td><input type='button' value='delete' onclick='DeleteItem("+elementvalue.Item_id+")'/></td><td><input type='button' value='details' onclick='DetailItem(" + elementValue.Item_id + ")'/></td></tr>";
                //    "</td> <td>< button type='button' class='btn btn-primary btn-sm' onclick='EditItem(" + elementValue.Item_id + ")' ><i class='bi bi-pencil-square' aria-hidden='true'></i></button ></td > <td><button type='button' class='btn btn-info btn-sm' onclick='DetailItem(" + elementValue.Item_id + ")'><i class='bi bi-eye-fill' aria-hidden='true'></i></button></td > <td> & nbsp;& nbsp;<button type='button' class='btn btn-danger btn-sm' onclick='DeleteItem(" + elementValue.Item_id + ")'><i class='bi bi-trash-fill' aria-hidden='true'></i></button></td ></tr > ";
                    "</td><td><button type='button' class='btn btn-danger'onclick='EditItem(" + elementValue.Item_id + ")'>Edit</button></td><td><button type='button' class='btn btn-danger'onclick='DeleteItem(" + elementValue.Item_id + ")'>Delete</button></td><td><button type='button'class='btn btn-primary'onclick='DetailItem(" + elementValue.Item_id + ")'>Detail</button></td></tr>";
            });

            $("#Item_master tbody").append(html);
        }
    })
};


var DeleteItem = function (Item_id) {
    //debugger;
    var confirmed = confirm("Are you sure you want to delete?");
    if (!confirmed)
    {
        alert("Deletion canceled.");
        return;
    }
    var model = { Item_id: Item_id };

        $.ajax({
            url: "/Item/DeleteItem",
            method: "Post",
            data: JSON.stringify(model),
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (response)
            {
                alert("Delete successfuly");
                location.reload();
                ItemList();
            },
            
        }); 
}

// Detail
var DetailItem = function (Item_id) {

    //debugger;
    var model = { Item_id: Item_id }
    $.ajax({
        url: "/Item/DetailItem",
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        //async: false,
        success: function (response) {
            $("#CategoryModal").modal('show');

            $("#Item_master").empty();

            var html = "";
            html += "<div class='row'>";
            html += "<div class='col-sm-6'>";
            html += "<b>ItemId:</b>&nbsp&nbsp&nbsp<span>" + response.model.Item_id + "</span>";
            html += "</br>";
            html += "<b>Item Name:</b>&nbsp&nbsp&nbsp<span>" + response.model.Item_name + "</span>";
            html += "</br>";
            html += "<b>Category:</b>&nbsp&nbsp&nbsp<span>" + response.model.Category + "</span>";
            html += "</br>";
            html += "<b>Rate:</b>&nbsp&nbsp&nbsp<span>" + response.model.Rate + "</span>";
            html += "</br>";
            html += "<b>Quantity:</b>&nbsp&nbsp&nbsp<span>" + response.model.Balance_quantity + "</span>";
            html += "</div>";
            html += "</div>";

            $("#Item_master").append(html);
        }
    });
    ItemList();
}

// Edit
var EditItem = function (Item_id) {
  
    var model = { Item_id: Item_id };

    $.ajax({
        url: "/Item/EditItem",
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            $("#hdnId").val(response.model.Item_id);
            $("#txtItem_Name").val(response.model.Item_name);
            $("#ddlCate").val(response.model.Category);
            $("#txtRate").val(response.model.Rate);
            $("#txtBal_Qty").val(response.model.Balance_quantity);

           //alert("Data Edited Successfully");
        }
    });
    ItemList();
}
