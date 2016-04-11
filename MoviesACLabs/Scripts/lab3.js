$("#submitButton").on("click", addMovie);

$("#movieNameInput").on("keypress", function (event) {
    if (event.which == 13) {
        addMovie();
    }
});

//$("body").on("click", ".remove_button", removeMovie);

function addMovie() {
    var textbox = $("#movieNameInput");

    var deleteButton = $('<button class="remove_button"/>').html("X");
    deleteButton.click(removeMovie);
    var li = $('<li> ' + textbox.val() + '</li> ');
    li.append(deleteButton);
    $("ul").append(li);
    textbox.val('');
    textbox.focus();
}

function removeMovie(event) {
    console.log(event);
    $(event.currentTarget.parentElement).remove();
}

// Todo: buton cu ajax get care sa ia de pe server lista de filme si sa o afiseze in pagina
// this e intotdeauna element html, trebuie apelat $.

function addRetrievedMovie(movie) {
    var li = $('<li> ' + movie + '</li> ');
    $("#retrievedMovies").append(li);
}

$("#retrieveMovieList").click(function () {
    $.get("http://localhost:58431/api/Movies", function(data) {
        console.log(data);
        $(data).each(function (index, object) {
            addRetrievedMovie(object.Title);
        });
    });
});

//Todo: de trimis un film la server.