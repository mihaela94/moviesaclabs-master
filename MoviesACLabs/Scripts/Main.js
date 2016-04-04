var images = ["http://cdn.paper4pc.com/images/little-cat-wallpaper-1.jpg",
    "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcQaSJ7EdW5I942dzjZUzNu_csIsnxr_RGrp4ZfhYPHXm0SRgT5HQA",
    "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcQrdnZUFlPESAxzSI8qOXCvOZNeUU-S4nt2fxWN26Yyt_KLQGWQ",
    "http://2.bp.blogspot.com/-UqBx4e-4XjY/URjh367j3EI/AAAAAAAABFs/GSEWUobZ-eU/s1600/ws_Cute_cat_in_Santa_hat_1024x1024.jpg",
    "https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcSh1EqGyCrD1hWE0m6MC_-4Rhh37RMzZnXTPxLgo1RBnlOZTwMUnw"];

var noImg = images.length;
var currentIndex = 0;

var img = document.getElementById("image");
var prevButton = document.getElementById("prev");
var nextButton = document.getElementById("next");

prevButton.addEventListener("click", previous);
nextButton.addEventListener("click", next);
document.addEventListener("keydown", keyPressed);

function previous() {
    if (currentIndex == 0) {
        currentIndex = noImg - 1;
    } else {
        currentIndex--;
    }
    img.src = images[currentIndex];
}

function next() {
    if (currentIndex == noImg - 1) {
        currentIndex = 0;
    } else {
        currentIndex++;
    }
    img.src = images[currentIndex];
    nextButton.style.color = "red";
}

function keyPressed(event) {
    if (event.keyCode == 39) {
        next();
    } else if (event.keyCode == 37) {
        previous();
    }
}

var stuff = function () {
    
};