$(document).ready(function () {
    loadDataTable()
});

function loadDataTable() {
    $('#myTable').DataTable({
        "ajax": { url: "/Admin/Student/GetStudents" },
        "columns":
            [

                { data: "name", "width": "15%" },
                { data: "surname", "width": "15%" },
                { data: "studentNumber", "width": "15%" },
                { data: "age", "width": "15%" },
                
            ]
    });
}