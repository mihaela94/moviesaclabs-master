var images = ["http://cdn.paper4pc.com/images/little-cat-wallpaper-1.jpg",
    "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcQaSJ7EdW5I942dzjZUzNu_csIsnxr_RGrp4ZfhYPHXm0SRgT5HQA",
    "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcQrdnZUFlPESAxzSI8qOXCvOZNeUU-S4nt2fxWN26Yyt_KLQGWQ",
    "http://2.bp.blogspot.com/-UqBx4e-4XjY/URjh367j3EI/AAAAAAAABFs/GSEWUobZ-eU/s1600/ws_Cute_cat_in_Santa_hat_1024x1024.jpg",
    "https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcSh1EqGyCrD1hWE0m6MC_-4Rhh37RMzZnXTPxLgo1RBnlOZTwMUnw"];

var noImg = images.length;
var currentIndex = 0;
var previousIndex = 0;
//jQuery object
var image = $("#image");
var bullets = $("#bullet_points");
var activeDot = '/images/dot_active.png';
var inactiveDot = '/images/dot_inactive.png';

$("#prev").on("click", prevImage);
$("#next").on("click", nextImage);

function prevImage() {
    previousIndex = currentIndex;
    if (currentIndex == 0) {
        currentIndex = noImg - 1;
    } else {
        currentIndex--;
    }
    image.attr("src", images[currentIndex]);
    setBullets();
}

function nextImage() {
    previousIndex = currentIndex;
    if (currentIndex == noImg - 1) {
        currentIndex = 0;
    } else {
        currentIndex++;
    }
    image.attr("src", images[currentIndex]);
    setBullets();
}

function setBullets() {
    $("#bullet_points li:nth-child(" + (previousIndex + 1) + ")").children().first().attr("src", inactiveDot);
    $("#bullet_points li:nth-child(" + (currentIndex + 1) + ")").children().first().attr("src", activeDot);
}

function loadImage() {
    //console.log("In loadImage()");
    image.attr("src", images[0]);
    var li = $('<li> <img src="' + activeDot + '" width="12px"/> </li>');
    bullets.append(li);
    for (i = 1; i < noImg; i++) {
        li = $('<li> <img src="' + inactiveDot + '" width="12px"/> </li>');
        bullets.append(li);
    }
}

//http://www.bennadel.com/blog/1474-ask-ben-iterating-over-an-array-in-jquery-one-index-per-click.htm