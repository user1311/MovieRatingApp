var dataTable;

$(document).ready(function () {
    loadDataTableMovies();
    setTimeout(PopulateStars, 3000);
});

function loadDataTableMovies() {
    dataTable = $('#tblData').DataTable({
        "order": [[4, "desc"]],
        "ajax": {
            "url": "/Home/GetAllMovies",
            "type": "GET",
            "datatype": "json",
            "async": "false",
        },
        "columns": [
            {
                "data": "imageURL",
                "render": function(data) {
                    return `<img src="${data}">
                            `;
                },
                "width": "25%"
            },
            { "data": "title", "width": "15%" },
            { "data": "description", "width": "20%" },
            { "data": "actors", "width": "20%" },
            {
                "data": "rating",
                "render": function(data) {
                    return `<div class="rating-number d-inline-block d-inline-flex" >
                                <ul class="rating" data-stars=${data}>
                                    <li class="rating-item" data-rate="1"></li>
                                    <li class="rating-item" data-rate="2"></li>
                                    <li class="rating-item" data-rate="3"></li>
                                    <li class="rating-item" data-rate="4"></li>
                                    <li class="rating-item" data-rate="5"></li>
                                </ul>
                        
                            <span class='text-center  align-content-center'>${data}</span>
                        </div>
                            `;
                },
                "width": "30%"
            },
            {
                "data": "id",
                "render": function(data) {
                    return `<input value="${data}" hidden class="movieId"/>
                            `;
                }, "hide": true,
                "width":"0%"
            }
        ]
    });
}

function loadDataTableShows() {
    dataTable = $('#tblData').DataTable({
        "order": [[4, "desc"]],
        "ajax": {
            "url": "/Home/GetAllShows",
            "type": "GET",
            "datatype": "json",
            "async": "false",
        },
        "columns": [
            {
                "data": "imageURL",
                "render": function (data) {
                    return `<img src="${data}">
                            `;
                },
                "width": "25%"
            },
            { "data": "title", "width": "15%" },
            { "data": "description", "width": "20%" },
            { "data": "actors", "width": "20%" },
            {
                "data": "rating",
                "render": function (data) {
                    return `<div class="rating-number d-inline-block d-inline-flex text-center  align-content-center" >

                                <ul class="rating" data-stars=${data}>
                                    <li class="rating-item" data-rate="1"></li>
                                    <li class="rating-item" data-rate="2"></li>
                                    <li class="rating-item" data-rate="3"></li>
                                    <li class="rating-item" data-rate="4"></li>
                                    <li class="rating-item" data-rate="5"></li>
                                </ul>
                        <span>${data}</span>
                        
                            
                        </div>
                            `;
                },
                "width": "30%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `<input value="${data}" hidden class="showId"/>
                            `;
                }, "hide": true,
                "width": "0%"
            }
        ]
    });
}

function PopulateStars() {
    var rowsStars = $('tr .rating-number span');
    var rows = document.querySelectorAll('.rating');

    for (var i = 0; i < rows.length; i++) {
        var rating = parseInt(rowsStars[i].innerText);

        var stars = rows[i].querySelectorAll('.rating-item');

        for (var j = 0; j < stars.length; j++) {
            if (stars[j].classList.contains('active')) {
                stars[j].classList.remove('active');
            }
        }

        stars[rating - 1].classList.add('active');
    }

}

$('#tblData').on('draw.dt', function () {
    PopulateStars();
});

$('#tblData').on('order.dt', function () {
    PopulateStars();
});

$('#tblData').on('page.dt', function () {
    PopulateStars();
});

$('#movies').click(() => {
    $('#tvShows').removeClass('text-primary');
    $('#movies').addClass('text-primary');

    $('#tblData').DataTable().destroy();

    loadDataTableMovies();

})

$('#tvShows').click(() => {
    $('#movies').removeClass('text-primary');
    $('#tvShows').addClass('text-primary');

    $('#tblData').DataTable().destroy();

    loadDataTableShows();

})