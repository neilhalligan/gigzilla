const GigsController = ((attendanceService) => {
    let button;

    const fail = () => {
        alert("Something Failed!");
    }

    const done = () => {
        const text = button.text() === "Going" ? "Going ?" : "Going";

        button.toggleClass("btn-default").toggleClass("btn-info").text(text);
    }

    const toggleAttendance = (e) => {
        button = $(e.target);
        const gigId = button.attr("data-gig-id");

        if (button.hasClass("btn-default")) {
            attendanceService.createAttendance(gigId, done, fail);
        } else {
            attendanceService.deleteAttendance(gigId, done, fail);
        }
    }

    const init = (container) => {
        $(container).on("click", ".js-toggle-attendance", toggleAttendance);
    }

    return {
        init
    }
})(AttendanceService);
