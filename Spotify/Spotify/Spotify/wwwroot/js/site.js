
//$(document).ready(function () {
//    $('.like').click(function () {
//        $(this).find('.icon-heart').toggleClass('like-active');
//    });
//});


$(document).ready(function () {

    $('#search').val('');
    $("#search").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#songs-list .song-card").each(function () {
            var songTitle = $(this).find(".player-info li:first-child").text().toLowerCase();
            if (value === "" || songTitle.indexOf(value) > -1) {
                $(this).show();
            } else {
                $(this).hide();
            }
        });
    });
});
$(document).ready(function () {
    $('.like').click(function (e) {
        e.preventDefault(); // Prevent the default link behavior

        var heartImg = $(this).find('img'); // Get the heart image element

        var songId = $(this).children('a').data("song-id");

        console.log(songId);
        // Perform the AJAX request
        $.ajax({
            url: '/Songs/AddFavoriteSongs',
            type: 'POST',
            data: { songId: songId },
            success: function (response) {
                // Handle the success response
                heartImg.attr('src', heartImg.attr('src') === '/img/heart-red.svg' ? '/img/heart-white.svg' : '/img/heart-red.svg');
                console.log('Action executed successfully');
            },
            error: function (xhr, status, error) {
                // Handle the error response
                console.error(error);
            }
        });
    });
});
$(function () {
    var audio = $("#audio")[0]; // Get the native audio element

    var playerTrack = $("#player-track"),
        albumArt = $("#album-art"),
        sArea = $("#s-area"),
        seekBar = $("#seek-bar"),
        insTime = $("#ins-time"),
        sHover = $("#s-hover"),
        playPauseButton = $("#play-pause-button"),
        tProgress = $("#current-time"),
        tTime = $("#track-length"),
        seekT,
        seekLoc,
        seekBarPos,
        cM,
        ctMinutes,
        ctSeconds,
        curMinutes,
        curSeconds,
        durMinutes,
        durSeconds,
        playProgress,
        bTime,
        nTime = 0,
        buffInterval = null;
        

    playPauseButton.on("click", function () {
        if (audio.paused) {
            playerTrack.addClass("active");
            albumArt.addClass("active");
            checkBuffering();
            playPauseButton.attr("src", "/img/pause.svg");
            audio.play();
        } else {
            playerTrack.removeClass("active");
            albumArt.removeClass("active");
            clearInterval(buffInterval);
            albumArt.removeClass("buffering");
            playPauseButton.attr("src", "/img/play.svg");
            audio.pause();
        }
    });
    function playPause() {
        setTimeout(function () {
            if (audio.paused) {
                playerTrack.addClass("active");
                albumArt.addClass("active");
                checkBuffering();
                playPauseButton.attr("src", "/img/pause.svg");
                audio.play();
            } else {
                playerTrack.removeClass("active");
                albumArt.removeClass("active");
                clearInterval(buffInterval);
                albumArt.removeClass("buffering");
                playPauseButton.attr("src", "/img/play.svg");
                audio.pause();
            }
        }, 300);
    }

    function showHover(event) {
        seekBarPos = sArea.offset();
        seekT = event.clientX - seekBarPos.left;
        seekLoc = audio.duration * (seekT / sArea.outerWidth());

        sHover.width(seekT);

        cM = seekLoc / 60;

        ctMinutes = Math.floor(cM);
        ctSeconds = Math.floor(seekLoc - ctMinutes * 60);

        if (ctMinutes < 0 || ctSeconds < 0) return;

        if (ctMinutes < 0 || ctSeconds < 0) return;

        if (ctMinutes < 10) ctMinutes = "0" + ctMinutes;
        if (ctSeconds < 10) ctSeconds = "0" + ctSeconds;

        if (isNaN(ctMinutes) || isNaN(ctSeconds)) insTime.text("--:--"); else
            insTime.text(ctMinutes + ":" + ctSeconds);

        insTime.css({ left: seekT, "margin-left": "-21px" }).fadeIn(0);
    }

    function hideHover() {
        sHover.width(0);
        insTime.text("00:00").css({ left: "0px", "margin-left": "0px" }).fadeOut(0);
    }

    function playFromClickedPos() {
        audio.currentTime = seekLoc;
        seekBar.width(seekT);
        hideHover();
    }

    function updateCurrTime() {
        nTime = new Date();
        nTime = nTime.getTime();

        if (!tFlag) {
            tFlag = true;
            trackTime.addClass("active");
        }

        curMinutes = Math.floor(audio.currentTime / 60);
        curSeconds = Math.floor(audio.currentTime - curMinutes * 60);

        durMinutes = Math.floor(audio.duration / 60);
        durSeconds = Math.floor(audio.duration - durMinutes * 60);

        playProgress = audio.currentTime / audio.duration * 100;

        if (curMinutes < 10) curMinutes = "0" + curMinutes;
        if (curSeconds < 10) curSeconds = "0" + curSeconds;

        if (durMinutes < 10) durMinutes = "0" + durMinutes;
        if (durSeconds < 10) durSeconds = "0" + durSeconds;

        if (isNaN(curMinutes) || isNaN(curSeconds)) tProgress.text("00:00"); else
            tProgress.text(curMinutes + ":" + curSeconds);

        if (isNaN(durMinutes) || isNaN(durSeconds)) tTime.text("00:00"); else
            tTime.text(durMinutes + ":" + durSeconds);

        if (
            isNaN(curMinutes) ||
            isNaN(curSeconds) ||
            isNaN(durMinutes) ||
            isNaN(durSeconds))

            trackTime.removeClass("active"); else
            trackTime.addClass("active");

        seekBar.width(playProgress + "%");

        if (playProgress == 100) {
            playPauseButton.attr("src", "/img/play.svg");
            seekBar.width(0);
            tProgress.text("00:00");
            albumArt.removeClass("buffering").removeClass("active");
            clearInterval(buffInterval);
        }
    }

    function checkBuffering() {
        clearInterval(buffInterval);
        buffInterval = setInterval(function () {
            if (nTime == 0 || bTime - nTime > 1000) albumArt.addClass("buffering"); else
                albumArt.removeClass("buffering");

            bTime = new Date();
            bTime = bTime.getTime();
        }, 100);
    }

    sArea.mousemove(function (event) {
        showHover(event);
    });

    sArea.mouseout(hideHover);

    sArea.on("click", playFromClickedPos);

    $(audio).on("timeupdate", updateCurrTime);

    $(".play-btn").on("click", function () {
        var downloadButton = document.getElementById('download-button');
        var songUrl = $(this).data("song-url");
        var imageURL = $(this).data("img");
        var name = $(this).data("name");
        var singer = $(this).data("singer");
        var duration = $(this).data("duration");
        var SongName = $("#track-name");
        SongName.text(name);
        var SingerName = $("#album-name");
        SingerName.text(singer);
        var albumArtImg = document.getElementById("album-art").querySelector("img");
        downloadButton.setAttribute('href', songUrl);

        // Update the src attribute of the img element
        albumArtImg.src = imageURL;
        var TrackTime = $("#track-length");
        TrackTime.text(duration);

        // Set the songUrl as the audio source
        audio.src = songUrl;

        playPause(); // Trigger play/pause function
    });

    // Initialize audio player with default trackUrl
    audio.src = ""; // You can set a default audio source if needed
});