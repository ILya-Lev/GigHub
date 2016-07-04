var FollowingService = function () {
    var follow = function (followeeId, done, fail) {
        $.post("/api/following/", { FolloweeId: followeeId })
            .done(done)
            .fail(fail);
    };

    var unfollow = function (done, fail) {
        $.ajax({
                url: "/api/following/",
                method: "DELETE"
            })
            .done(done)
            .fail(fail);
    };

    return {
        follow: follow,
        unfollow: unfollow
    };
}();