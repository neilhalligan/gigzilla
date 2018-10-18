const AttendanceService = (() => {
    const createAttendance = (gigId, done, fail) => {
        $.post("/api/attendances", { GigId: gigId })
            .done(done)
            .fail(fail);
    }

    const deleteAttendance = (gigId, done, fail) => {
        $.ajax({
                url: "/api/attendances/" + gigId,
                method: "DELETE"
            })
            .done(done)
            .fail(fail);
    }

    return {
        createAttendance,
        deleteAttendance
    }
})();