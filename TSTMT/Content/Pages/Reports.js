$(document).ready(function () {

    GetAllData();
});
$("#fetchCombinedDataButton").on("click", function () {
    $.ajax({
        url: "/Reports/GetAllData", // Correct URL
        method: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var tableBody = $("#Item_Transaction tbody"); // Ensure correct table ID
            tableBody.empty(); // Clear existing data

            // Populate the table with combined data
            $.each(response.model, function (index, item) {
                var row = "<tr>";
                row += "<td>" + item.Transaction_id + "</td>";
                row += "<td>" + item.Item_name + "</td>";
                row += "<td>" + item.Category + "</td>";
                row += "<td>" + item.Transaction_date + "</td>";
                row += "<td>" + item.Department_name + "</td>";
                row += "<td>" + item.Vendor_name + "</td>";
                row += "<td>" + item.Quantity + "</td>";
                row += "<td>" + item.Rate + "</td>";
                row += "<td>" + item.Balance_quantity + "</td>";
                row += "</tr>";

                tableBody.append(row); // Add the row to the table
            });
        },
        error: function (xhr, status, error) {
            Swal.fire("Error", "Failed to fetch data: " + error, "error");
        }
    });

}
