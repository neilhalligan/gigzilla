const GigDetailsController = ((followingsService) => {
    let button;

    const fail = () => {
        alert("Something Failed!");
    }

    const done = () => {
        const text = button.text() === "Following" ? "Following ?" : "Following";

        button.toggleClass("btn-default").toggleClass("btn-info").text(text);;
    }

    const toggleFollow = e => {
        const followeeId = button.attr("data-followee-id");
        if (button.hasClass("btn-default")) {
            followingsService.createFollow(followeeId, done, fail);
        } else {
            followingsService.deleteFollow(followeeId, done, fail);
        }
    }

    const setInitialButton = () => {
        const followeeId = button.attr("data-followee-id");

        const isDone = isFollowing => {
            button.removeClass("hide");
            if (isFollowing)
                done();
        }

        followingsService.ifFollow(followeeId, isDone, fail);
    }

    const init = () => {
        button = $(".js-toggle-follow");

        setInitialButton();
        button.click(toggleFollow);
    }

    return {
        init
    }
})(FollowingsService);

