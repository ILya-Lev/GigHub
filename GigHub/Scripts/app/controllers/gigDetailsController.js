// IIFE - kludge for a class; client-side controller
var GigDetailsController = function (followingService) {
    var button;

    var init = function (container) {
        $(container).on("click", ".js-toggle-following", toggleFollowing);
    };

    var toggleFollowing = function (e) {
        button = $(e.target);

        var followeeId = button.attr("data-artist-id");
        console.info(followeeId);

        if (button.hasClass("btn-default"))
            followingService.follow(followeeId, done, fail);
        else 
            followingService.unfollow(followeeId, done, fail);
    };

    var done = function () {
        var text = (button.text() == "Following") ? "Follow?" : "Following";
        button.toggleClass("btn-default").toggleClass("btn-info").text(text);
    };

    var fail = function () { alert("Something failed!"); };

    return {
        init: init
    };
}(FollowingService);
