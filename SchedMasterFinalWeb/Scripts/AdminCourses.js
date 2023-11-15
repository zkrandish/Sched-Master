document.addEventListener("DOMContentLoaded", function () {
    let courses = document.querySelectorAll(".course");
    let delay = 0;

    courses.forEach(function (course) {
        setTimeout(function () {
            course.style.transform = "scale(1)"; // Make the course visible
        }, delay);
        delay += 150; // Delay of 500ms for each course
    });
});
