const FollowingsService = (() => {
    const createFollow = (followeeId, done, fail) => {
        $.post("/api/followings", { FolloweeId: followeeId })
            .done(done)
            .fail(fail)
    }

    const deleteFollow = (followeeId, done, fail) => {
        $.ajax({
                url: "/api/followings/" + followeeId,
                method: "DELETE"
            })
            .done(done)
            .fail(fail);
    }

    const ifFollow = (followeeId, done, fail) => {
        $.get("/api/followings", { FolloweeId: followeeId })
            .done(done)
            .fail(fail);
    }

    return {
        createFollow,
        deleteFollow,
        ifFollow
    }
})();
