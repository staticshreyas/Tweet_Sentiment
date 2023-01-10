
window.addEventListener("load", myMain, false);

function myMain() {
    var jsInitChecktimer = setInterval(checkForJS_Finish, 111);
    var localStorage = {};

    function checkForJS_Finish() {
        if (typeof SOME_GLOBAL_VAR != "undefined"
            || document.querySelector('[data-testid="tweetText"]')
        ) {
            clearInterval(jsInitChecktimer);

            var i;
            var tweets = [];
            var elements = document.querySelectorAll('[data-testid="tweetText"]');

            for (i = 0; i < elements.length; i++) {
                var x = elements[i];
                var tweet = (x.textContent === undefined) ? x.innerText : x.textContent;
                var obj = { "Tweet_text": tweet };
                if (localStorage[tweet] != undefined) {
                    continue;
                }
                else {
                    localStorage[tweet] = elements[i].id;
                    tweets.push(obj);
                }
            }

            if (tweets.length >= 1) {
                $.ajax({
                    url: "https://localhost:7159/api/language-detection",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify(tweets),
                    success: function (result) {
                        successLanguageDetection(result);
                    },
                    error: function (xhr, textStatus) {
                        if (xhr.status == 401) {
                            alert("Session Expired!");
                        }
                        else {
                            alert('Content load failed!', "info");
                        }
                    }
                });


                function successLanguageDetection(result) {
                    var englishTweets = [];

                    result.forEach(tweet => {
                        if (tweet['is_english'] == true) {
                            englishTweets.push({ 'tweet_text': tweet['tweet_text'] });
                        }
                    })

                    $.ajax({
                        url: "https://localhost:7159/api/sentiment-score",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: JSON.stringify(englishTweets),
                        success: function (result) {
                            successSentimentDetection(result);
                        },
                        error: function (xhr, textStatus) {
                            if (xhr.status == 401) {
                                alert("Session Expired!");
                            }
                            else {
                                alert('Content load failed!', "info");
                            }
                        }
                    });

                    function successSentimentDetection(result) {
                        result.forEach(res => {
                            var id = localStorage[res.tweet_text]
                            var mood = res.detected_mood;
                            var emoji = "";
                            if (mood === "POSITIVE") emoji = "128522"
                            else if (mood === "NEUTRAL") emoji = "128528"
                            else emoji = "128577"
                            var tweetToEdit = document.getElementById(id).closest('article').querySelector('[data-testid="User-Names"]');
                            var newDiv =
                                `
                            <div class="css-1dbjc4n r-18u37iz r-1wbh5a2 r-13hce6t">
                                <div class="css-1dbjc4n r-1d09ksm r-18u37iz r-1wbh5a2">
                                    <div dir="ltr" aria-hidden="true"
                                        class="css-901oao r-1bwzh9t r-1q142lx r-37j5jr r-a023e6 r-16dba41 r-rjixqe r-bcqeeo r-s1qlax r-qvutc0"><span
                                            class="css-901oao css-16my406 r-poiln3 r-bcqeeo r-qvutc0">·</span></div>
                                    <div class="css-1dbjc4n r-1wbh5a2 r-dnmrzs"><a href="#" role="link" tabindex="-1"
                                            class="css-4rbku5 css-18t94o4 css-1dbjc4n r-1loqt21 r-1wbh5a2 r-dnmrzs r-1ny4l3l">
                                            <div dir="ltr"
                                                class="css-901oao css-1hf3ou5 r-1bwzh9t r-18u37iz r-37j5jr r-a023e6 r-16dba41 r-rjixqe r-bcqeeo r-qvutc0">
                                                <span class="css-901oao css-16my406 r-poiln3 r-bcqeeo r-qvutc0">Detected Mood:  </span></div>
                                        </a></div>
                                    <div class="css-1dbjc4n r-18u37iz r-1q142lx"><a href="#" dir="ltr" aria-label="2 minutes ago" role="link"
                                            class="css-4rbku5 css-18t94o4 css-901oao r-1bwzh9t r-1loqt21 r-xoduu5 r-1q142lx r-1w6e6rj r-37j5jr r-a023e6 r-16dba41 r-9aw3ui r-rjixqe r-bcqeeo r-3s2u2q r-qvutc0"><time
                                                datetime="2023-01-10T02:18:53.000Z"> &#`+ emoji + `;</time></a></div>
                                </div>
                            </div>
                        `
                            tweetToEdit.innerHTML += newDiv
                        })
                    }
                }


            }

        }
        window.onscroll = checkForJS_Finish;
    }
}