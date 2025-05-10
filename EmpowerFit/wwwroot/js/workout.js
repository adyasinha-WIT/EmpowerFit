var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/Basic/Workout/Getall', dataSrc: 'data' },
        "columns": [
            { "data": "goals" },
            { "data": "workouts" },
            { "data": "type" },
            { "data": "weeklyReport" },
            {
                "data": "mediaUrl",
                "render": function (data, type, row) {
                    if (!data) return '';
                    var ext = data.split('.').pop().toLowerCase();
                    if (["mp4", "webm"].includes(ext)) {
                        return `<video width="80" controls><source src="${data}" type="video/${ext}"></video>`;
                    } else {
                        return `<img src="${data}" width="80" class="rounded shadow" />`;
                    }
                },
                "orderable": false,
                "searchable": false
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                <a href="/Basic/Workout/Upsert/${data}" class="btn btn-success btn-sm mx-1">Edit</a>
                <a onclick=Delete('/Basic/Workout/Delete/${data}') class="btn btn-danger btn-sm mx-1">Delete</a>
            `;
                },
                "orderable": false,
                "searchable": false
            }
        ]

    });
}
function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            });
        }
    });
}